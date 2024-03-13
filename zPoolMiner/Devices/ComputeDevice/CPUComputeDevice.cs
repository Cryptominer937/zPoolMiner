using System;
using System.Diagnostics;
using zPoolMiner.Enums;
//using LibreHardwareMonitor.Collections;
using LibreHardwareMonitor.Hardware;

namespace zPoolMiner.Devices
{
    internal class CPUComputeDevice : ComputeDevice
    {
        private PerformanceCounter cpuCounter;
        private Computer c = new Computer();
        public override float Load
        {
            get
            {
                try
                {
                    if (cpuCounter != null) return cpuCounter.NextValue();
                }
                catch (Exception e) { Helpers.ConsolePrint("CPUDIAG", e.ToString()); }
                return 0;
            }
        }
        public override float Temp
        {
            get
            {
                foreach(IHardware h in c.Hardware)
                {
                    h.Update();
                    float highest = 0;
                    foreach(ISensor s in h.Sensors)
                    {
                        if(s.SensorType == SensorType.Temperature && s.Name == "Package")
                        {
                            return s.Value??-1;
                        }else if(s.SensorType == SensorType.Temperature)
                        {
                            if(highest < s.Value)
                            {
                                highest = s.Value??-1;
                            }
                        }
                    }
                    return highest;
                }
                return 0;
            }
        }

        public CPUComputeDevice(int id, string group, string name, int threads, ulong affinityMask, int CPUCount)
            : base(id,
                  name,
                  true,
                  DeviceGroupType.CPU,
                  false,
                  DeviceType.CPU,
                  String.Format(International.GetText("ComputeDevice_Short_Name_CPU"), CPUCount),
                  0)
        {
            Threads = threads;
            AffinityMask = affinityMask;
            UUID = GetUUID(ID, GroupNames.GetGroupName(DeviceGroupType, ID), Name, DeviceGroupType);
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            Index = ID;  // Don't increment for CPU
            c.IsCpuEnabled = true;
            c.Open();
            cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };
        }
    }

   
}