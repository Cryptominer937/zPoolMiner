﻿using System;

namespace zPoolMiner.Devices
{
    [Serializable]
    public class AmdGpuDevice
    {
        public static readonly string DefaultParam = "";

        public static readonly string TemperatureParam = " --gpu-fan 30-95 --temp-cutoff 95 --temp-overheat 90 " +
                                        " --temp-target 75 --auto-fan --auto-gpu ";

        public int DeviceID => (int)_openClSubset.DeviceID;
        public int BusID => (int)_openClSubset.AMD_BUS_ID;
        public string DeviceName; // init this with the ADL
        public string UUID; // init this with the ADL, use PCI_VEN & DEV IDs
        public ulong DeviceGlobalMemory => _openClSubset._CL_DEVICE_GLOBAL_MEM_SIZE;

        // public bool UseOptimizedVersion { get; private set; }
        private OpenCLDevice _openClSubset = new OpenCLDevice();

        public readonly string InfSection; // has arhitecture string

        // new drivers make some algorithms unusable 21.19.164.1 => driver not working with NeoScrypt and
        public bool DriverDisableAlgos { get; private set; }

        public string Codename => _openClSubset._CL_DEVICE_NAME;

        public int AdapterIndex;  // init this with the ADL

        public AmdGpuDevice(OpenCLDevice openClSubset, bool isOldDriver, string infSection, bool driverDisableAlgo)
        {
            DriverDisableAlgos = driverDisableAlgo;
            InfSection = infSection;

            if (openClSubset != null)
            {
                _openClSubset = openClSubset;
            }
            // Check for optimized version
            // first if not optimized
            Helpers.ConsolePrint("AmdGpuDevice", "List: " + _openClSubset._CL_DEVICE_NAME);
            // if (isOldDriver) {
            //    UseOptimizedVersion = false;
            //    Helpers.ConsolePrint("AmdGpuDevice", "GPU (" + _openClSubset._CL_DEVICE_NAME + ") is optimized => NOO! OLD DRIVER.");
            // } else if (!( _openClSubset._CL_DEVICE_NAME.Contains("Bonaire")
            //    || _openClSubset._CL_DEVICE_NAME.Contains("Fiji")
            //    || _openClSubset._CL_DEVICE_NAME.Contains("Hawaii")
            //    || _openClSubset._CL_DEVICE_NAME.Contains("Pitcairn")
            //    || _openClSubset._CL_DEVICE_NAME.Contains("Tahiti")
            //    || _openClSubset._CL_DEVICE_NAME.Contains("Tonga"))) {
            //    UseOptimizedVersion = false;
            //    Helpers.ConsolePrint("AmdGpuDevice", "GPU (" + _openClSubset._CL_DEVICE_NAME + ") is optimized => NOO!");
            // } else {
            //    UseOptimizedVersion = true;
            //    Helpers.ConsolePrint("AmdGpuDevice", "GPU (" + _openClSubset._CL_DEVICE_NAME + ") is optimized => YES!");
            // }
        }

        public bool IsEtherumCapable()
        {
            return _openClSubset._CL_DEVICE_GLOBAL_MEM_SIZE >= ComputeDevice.MEMORY_3GB;
        }
    }
}