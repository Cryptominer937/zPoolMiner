﻿using System;
using System.Collections.Generic;
using System.Text;
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Enums;

namespace zPoolMiner.Miners.Grouping
{
    public static class GroupSetupUtils
    {
        private static readonly string TAG = "GroupSetupUtils";

        public static bool IsAlgoMiningCapable(Algorithm algo)
        {
            return algo != null && algo.Enabled && algo.BenchmarkSpeed > 0;
        }

        public static Tuple<ComputeDevice, DeviceMiningStatus> GetDeviceMiningStatus(ComputeDevice device)
        {
            var status = DeviceMiningStatus.CanMine;

            if (device == null)
            { // C# is null happy
                status = DeviceMiningStatus.DeviceNull;
            }
            else if (device.Enabled == false)
            {
                status = DeviceMiningStatus.Disabled;
            }
            else
            {
                var hasEnabledAlgo = false;

                foreach (Algorithm algo in device.GetAlgorithmSettings())
                {
                    hasEnabledAlgo |= IsAlgoMiningCapable(algo) && MinerPaths.IsValidMinerPath(algo.MinerBinaryPath);
                }

                if (hasEnabledAlgo == false)
                {
                    status = DeviceMiningStatus.NoEnabledAlgorithms;
                }
            }

            return new Tuple<ComputeDevice, DeviceMiningStatus>(device, status);
        }

        private static Tuple<List<MiningDevice>, List<Tuple<ComputeDevice, DeviceMiningStatus>>> GetMiningAndNonMiningDevices(List<ComputeDevice> devices)
        {
            var nonMiningDevStatuses = new List<Tuple<ComputeDevice, DeviceMiningStatus>>();
            var miningDevices = new List<MiningDevice>();

            foreach (var dev in devices)
            {
                var devStatus = GetDeviceMiningStatus(dev);

                if (devStatus.Item2 == DeviceMiningStatus.CanMine)
                {
                    miningDevices.Add(new MiningDevice(dev));
                }
                else
                {
                    nonMiningDevStatuses.Add(devStatus);
                }
            }

            return new Tuple<List<MiningDevice>, List<Tuple<ComputeDevice, DeviceMiningStatus>>>(miningDevices, nonMiningDevStatuses);
        }

        private static string GetDisabledDeviceStatusString(Tuple<ComputeDevice, DeviceMiningStatus> devStatusTuple)
        {
            var dev = devStatusTuple.Item1;
            var status = devStatusTuple.Item2;

            if (DeviceMiningStatus.DeviceNull == status)
            {
                return "Passed Device is NULL";
            }
            else if (DeviceMiningStatus.Disabled == status)
            {
                return "DISABLED: " + dev.GetFullName();
            }
            else if (DeviceMiningStatus.NoEnabledAlgorithms == status)
            {
                return "No Enabled Algorithms: " + dev.GetFullName();
            }

            return "Invalid status Passed";
        }

        private static void LogMiningNonMiningStatuses(List<MiningDevice> enabledDevices, List<Tuple<ComputeDevice, DeviceMiningStatus>> disabledDevicesStatuses)
        {
            // print statuses
            if (disabledDevicesStatuses.Count > 0)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("Disabled Devices:");

                foreach (var deviceStatus in disabledDevicesStatuses)
                    stringBuilder.AppendLine("\t" + GetDisabledDeviceStatusString(deviceStatus));

                Helpers.ConsolePrint(TAG, stringBuilder.ToString());
            }

            if (enabledDevices.Count > 0)
            {
                // print enabled
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("Enabled Devices for Mining session:");

                foreach (var miningDevice in enabledDevices)
                {
                    var device = miningDevice.Device;
                    stringBuilder.AppendLine(string.Format("\tENABLED ({0})", device.GetFullName()));

                    foreach (var algo in device.GetAlgorithmSettings())
                    {
                        var isEnabled = IsAlgoMiningCapable(algo) && MinerPaths.IsValidMinerPath(algo.MinerBinaryPath);

                        stringBuilder.AppendLine(string.Format("\t\tALGORITHM {0} ({1})",
                            isEnabled ? "ENABLED " : "DISABLED", // ENABLED/DISABLED
                            algo.AlgorithmStringID));
                    }
                }

                Helpers.ConsolePrint(TAG, stringBuilder.ToString());
            }
        }

        public static List<MiningDevice> GetMiningDevices(List<ComputeDevice> devices, bool log)
        {
            var miningNonMiningDevs = GetMiningAndNonMiningDevices(devices);

            if (log)
            {
                LogMiningNonMiningStatuses(miningNonMiningDevs.Item1, miningNonMiningDevs.Item2);
            }

            return miningNonMiningDevs.Item1;
        }

        // avarage passed in benchmark values
        public static void AvarageSpeeds(List<MiningDevice> miningDevs)
        {
            // calculate avarage speeds, to ensure mining stability
            // device name, algo key, algos refs list
            var allAvaragers = new Dictionary<string, AvaragerGroup>();
            // Dictionary<string, AvaragerGroup> allAvaragers = new Dictionary<string, AvaragerGroup>();

            // init empty avarager
            var devName = "";

            foreach (var device in miningDevs)
            {
                if (ConfigManager.GeneralConfig.Group_same_devices)
                {
                    devName = device.Device.Name;
                }
                else
                {
                    devName = device.Device.UUID;
                }

                allAvaragers[devName] = new AvaragerGroup();
                // string devName = device.Device.Name;
                // allAvaragers[devName] = new AvaragerGroup();
            }

            // fill avarager
            foreach (var device in miningDevs)
            {
                if (ConfigManager.GeneralConfig.Group_same_devices)
                {
                    devName = device.Device.Name;
                }
                else
                {
                    devName = device.Device.UUID;
                }

                // add UUID
                allAvaragers[devName].UUIDsList.Add(device.Device.UUID);
                allAvaragers[devName].AddAlgorithms(device.Algorithms);
            }

            // calculate and set new AvarageSpeeds for miningDeviceReferences
            foreach (var curAvaragerKvp in allAvaragers)
            {
                var curAvarager = curAvaragerKvp.Value;
                var calculatedAvaragers = curAvarager.CalculateAvarages();

                foreach (var uuid in curAvarager.UUIDsList)
                {
                    var minerDevIndex = miningDevs.FindIndex((dev) => dev.Device.UUID == uuid);

                    if (minerDevIndex > -1)
                    {
                        foreach (var avgKvp in calculatedAvaragers)
                        {
                            var algo_id = avgKvp.Key;
                            var avaragedSpeed = avgKvp.Value[0];
                            var secondaryAveragedSpeed = avgKvp.Value[1];
                            var index = miningDevs[minerDevIndex].Algorithms.FindIndex((a) => a.AlgorithmStringID == algo_id);

                            if (index > -1)
                            {
                                miningDevs[minerDevIndex].Algorithms[index].AvaragedSpeed = avaragedSpeed;
                                miningDevs[minerDevIndex].Algorithms[index].SecondaryAveragedSpeed = double.IsNaN(secondaryAveragedSpeed) ? 0 : secondaryAveragedSpeed;
                            }
                        }
                    }
                }
            }
        }
    }

    internal class SpeedSumCount
    {
        public double speed = 0;
        public double secondarySpeed = 0;
        public int count;

        public double GetAvarage()
        {
            if (count > 0) return speed / (double)count;
            return 0;
        }

        public double GetSecondaryAverage()
        {
            if (count > 0)
            {
                return secondarySpeed / (double)count;
            }

            return 0;
        }
    }

    internal class AvaragerGroup
    {
        public string DeviceName;
        public List<string> UUIDsList = new List<string>();

        // algo_id, speed_sum, speed_count
        public Dictionary<string, SpeedSumCount> BenchmarkSums = new Dictionary<string, SpeedSumCount>();

        public Dictionary<string, List<double>> CalculateAvarages()
        {
            var ret = new Dictionary<string, List<double>>();

            foreach (var kvp in BenchmarkSums)
            {
                var algo_id = kvp.Key;
                var ssc = kvp.Value;
                ret[algo_id] = new List<double> { ssc.GetAvarage(), ssc.GetSecondaryAverage() };
            }

            return ret;
        }

        public void AddAlgorithms(List<Algorithm> algos)
        {
            foreach (var algo in algos)
            {
                var algo_id = algo.AlgorithmStringID;

                if (BenchmarkSums.ContainsKey(algo_id) == false)
                {
                    var ssc = new SpeedSumCount
                    {
                        count = 1,
                        speed = algo.BenchmarkSpeed,
                        secondarySpeed = algo.SecondaryBenchmarkSpeed
                    };

                    BenchmarkSums[algo_id] = ssc;
                }
                else
                {
                    BenchmarkSums[algo_id].count++;
                    BenchmarkSums[algo_id].speed += algo.BenchmarkSpeed;
                    BenchmarkSums[algo_id].secondarySpeed += algo.SecondaryBenchmarkSpeed;
                }
            }
        }
    }
}