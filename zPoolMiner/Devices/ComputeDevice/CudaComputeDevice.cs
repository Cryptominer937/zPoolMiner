﻿/*
* This is an open source non-commercial project. Dear PVS-Studio, please check it.
* PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
*/
using ManagedCuda.Nvml;
using NVIDIA.NVAPI;
using System;
using zPoolMiner.Enums;
using zPoolMiner.Configs;
using static zPoolMiner.Devices.ComputeDeviceManager;
using System.Diagnostics;
using System.Windows.Forms;
using zPoolMiner.Devices;
using zPoolMiner;

namespace zPoolMiner.Devices
{
    internal class CudaComputeDevice : ComputeDevice
    {
        private readonly NvPhysicalGpuHandle _nvHandle; // For NVAPI
        private readonly nvmlDevice nvmlDevice; // For NVML
        private const int GpuCorePState = 0; // memcontroller = 1, videng = 2

        protected int SMMajor;
        protected int SMMinor;
        public readonly bool ShouldRunEthlargement;
        public override float Load
        {
            get
            {
                var load = -1;

                try
                {
                    var rates = new nvmlUtilization();
                    var ret = NvmlNativeMethods.nvmlDeviceGetUtilizationRates(nvmlDevice, ref rates);
                    if (ret != nvmlReturn.Success)
                        throw new Exception($"NVML get load failed with code: {ret}");

                    load = (int)rates.gpu;
                }
                catch (Exception e)
                {
                    //Helpers.ConsolePrint("NVML", e.ToString());
                }

                return load;
            }
        }


        public override float Temp
        {
            get
            {
                var temp = -1f;

                try
                {
                    var utemp = 0u;
                    var ret = NvmlNativeMethods.nvmlDeviceGetTemperature(nvmlDevice, nvmlTemperatureSensors.Gpu,
                        ref utemp);
                    if (ret != nvmlReturn.Success)
                    {
                        Form_Main.needRestart = true;
                        //ComputeDeviceManager.Query.Nvidia.QueryCudaDevices();
                        // throw new Exception($"NVML get temp failed with code: {ret}");
                    }
                    temp = utemp;
                }
                catch (Exception e)
                {
                    Helpers.ConsolePrint("NVML", e.ToString());
                }

                return temp;
            }
        }

        private NvPhysicalGpuHandle? _NvPhysicalGpuHandle;
        private NvPhysicalGpuHandle? GetNvPhysicalGpuHandle()
        {
            if (_NvPhysicalGpuHandle.HasValue) return _NvPhysicalGpuHandle.Value;
            if (NVAPI.NvAPI_EnumPhysicalGPUs == null)
            {
                Helpers.ConsolePrint("NVAPI", "NvAPI_EnumPhysicalGPUs unavailable ");
                return null;
            }
            if (NVAPI.NvAPI_GPU_GetBusID == null)
            {
                Helpers.ConsolePrint("NVAPI", "NvAPI_GPU_GetBusID unavailable");
                return null;
            }


            var handles = new NvPhysicalGpuHandle[NVAPI.MAX_PHYSICAL_GPUS];
            var status = NVAPI.NvAPI_EnumPhysicalGPUs(handles, out _);
            if (status != NvStatus.OK)
            {
                Helpers.ConsolePrint("NVAPI", $"Enum physical GPUs failed with status: {status}", TimeSpan.FromMinutes(5));
            }
            else
            {
                foreach (var handle in handles)
                {
                    var idStatus = NVAPI.NvAPI_GPU_GetBusID(handle, out var id);

                    if (idStatus == NvStatus.EXPECTED_PHYSICAL_GPU_HANDLE) continue;

                    if (idStatus != NvStatus.OK)
                    {
                        Helpers.ConsolePrint("NVAPI", "Bus ID get failed with status: " + idStatus, TimeSpan.FromMinutes(5));
                    }
                    else if (id == BusID)
                    {
                        Helpers.ConsolePrint("NVAPI", "Found handle for busid " + id, TimeSpan.FromMinutes(5));
                        _NvPhysicalGpuHandle = handle;
                        return handle;
                    }
                }
            }
            return null;
        }

        public override int FanSpeed
        {
            get
            {
                if (!ConfigManager.GeneralConfig.ShowFanAsPercent)
                {
                    var fanSpeed = -1;


                    // we got the lock
                    var nvHandle = GetNvPhysicalGpuHandle();
                    if (!nvHandle.HasValue)
                    {
                        Helpers.ConsolePrint("NVAPI", $"FanSpeed nvHandle == null", TimeSpan.FromMinutes(5));
                        return -1;
                    }

                    if (NVAPI.NvAPI_GPU_GetTachReading != null)
                    {
                        //var result = NVAPI.NvAPI_GPU_GetTachReading(_nvHandle, out fanSpeed);
                        var result = NVAPI.NvAPI_GPU_GetTachReading(nvHandle.Value, out fanSpeed);
                        if (result != NvStatus.OK && result != NvStatus.NOT_SUPPORTED)
                        {
                            // GPUs without fans are not uncommon, so don't treat as error and just return -1
                            Helpers.ConsolePrint("NVAPI", "Tach get failed with status: " + result);
                            return -1;
                        }
                    }

                    return fanSpeed;
                }
                else
                {
                    var fan = -1;

                    try
                    {
                        var ufan = 0u;
                        var ret = NvmlNativeMethods.nvmlDeviceGetFanSpeed(nvmlDevice, ref ufan);
                        if (ret != nvmlReturn.Success)
                        {
                            Form_Main.needRestart = true;
                            //ComputeDeviceManager.Query.Nvidia.QueryCudaDevices();
                            //throw new Exception($"NVML get fan speed failed with code: {ret}");
                        }
                        fan = (int)ufan;
                    }
                    catch (Exception e)
                    {
                        Helpers.ConsolePrint("NVML", e.ToString());
                    }

                    return fan;
                }
                return 0;
            }
        }
        private nvmlDevice GetNvmlDevice()
        {
            var nvmlHandle = new nvmlDevice();
            var nvmlRet = NvmlNativeMethods.nvmlDeviceGetHandleByUUID(UUID, ref nvmlHandle);
            if (nvmlRet != nvmlReturn.Success)
            {
                //throw new NvmlException("nvmlDeviceGetHandleByUUID", nvmlRet);
            }
            return nvmlHandle;
        }
    public override double PowerUsage
        {
            get
            {
                try
                {
                    var nvmlDevice = GetNvmlDevice();
                    var power = 0u;
                    var ret = NvmlNativeMethods.nvmlDeviceGetPowerUsage(nvmlDevice, ref  power);
                    if (ret != nvmlReturn.Success)
                        throw new Exception($"NVML power get failed with status: {ret}");

                    return power * 0.001;
                }
                catch (Exception e)
                {
                    // Helpers.ConsolePrint("NVML", e.ToString());
                }

                return -1;
            }
        }

        public CudaComputeDevice(CudaDevice cudaDevice, DeviceGroupType group, int gpuCount,
            NvPhysicalGpuHandle nvHandle, nvmlDevice nvmlHandle)
            : base((int)cudaDevice.DeviceID,
                cudaDevice.GetName(),
                true,
                group,
                cudaDevice.IsEtherumCapable(),
                DeviceType.NVIDIA,
                string.Format(International.GetText("ComputeDevice_Short_Name_NVIDIA_GPU"), gpuCount),
                cudaDevice.DeviceGlobalMemory)
        {
            BusID = cudaDevice.pciBusID;
            SMMajor = cudaDevice.SM_major;
            SMMinor = cudaDevice.SM_minor;
            UUID = cudaDevice.UUID;
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            Index = ID + ComputeDeviceManager.Avaliable.AvailCPUs; // increment by CPU count

            _nvHandle = nvHandle;
            nvmlDevice = nvmlHandle;
            ShouldRunEthlargement = cudaDevice.DeviceName.Contains("1080") || cudaDevice.DeviceName.Contains("Titan Xp");
        }
    }
}
