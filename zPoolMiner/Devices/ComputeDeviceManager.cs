using ATI.ADL;
using ManagedCuda.Nvml;
using Newtonsoft.Json;
using NVIDIA.NVAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.Interfaces;

namespace zPoolMiner.Devices
{
    /// <summary>
    /// ComputeDeviceManager class is used to query ComputeDevices avaliable on the system.
    /// Query CPUs, GPUs [Nvidia, AMD]
    /// </summary>
    public class ComputeDeviceManager
    {
        public static class Query
        {
            private const string TAG = "ComputeDeviceManager.Query";

            // format 372.54;
            private class NVIDIA_SMI_DRIVER
            {
                public NVIDIA_SMI_DRIVER(int left, int right)
                {
                    leftPart = left;
                    rightPart = right;
                }

                public bool IsLesserVersionThan(NVIDIA_SMI_DRIVER b)
                {
                    if (leftPart < b.leftPart) return true;

                    if (leftPart == b.leftPart && GetRightVal(rightPart) < GetRightVal(b.rightPart))
                    {
                        return true;
                    }

                    return false;
                }

                public override string ToString() => string.Format("{0}.{1}", leftPart, rightPart);

                public int leftPart;
                public int rightPart;

                private int GetRightVal(int val)
                {
                    if (val >= 10) return val;
                    return val * 10;
                }
            }

            private static readonly NVIDIA_SMI_DRIVER NVIDIA_RECOMENDED_DRIVER = new NVIDIA_SMI_DRIVER(372, 54); // 372.54;
            private static readonly NVIDIA_SMI_DRIVER NVIDIA_MIN_DETECTION_DRIVER = new NVIDIA_SMI_DRIVER(362, 61); // 362.61;
            private static NVIDIA_SMI_DRIVER _currentNvidiaSMIDriver = new NVIDIA_SMI_DRIVER(-1, -1);
            private static NVIDIA_SMI_DRIVER INVALID_SMI_DRIVER = new NVIDIA_SMI_DRIVER(-1, -1);

            // naming purposes
            private static int CPUCount;

            private static int GPUCount;

            private static NVIDIA_SMI_DRIVER GetNvidiaSMIDriver()
            {
                if (WindowsDisplayAdapters.HasNvidiaVideoController())
                {
                    string stdOut, stdErr, args, smiPath;
                    stdOut = stdErr = args = string.Empty;
                    smiPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\NVIDIA Corporation\\NVSMI\\nvidia-smi.exe";
                    if (smiPath.Contains(" (x86)")) smiPath = smiPath.Replace(" (x86)", "");

                    try
                    {
                        var P = new Process();
                        P.StartInfo.FileName = smiPath;
                        P.StartInfo.UseShellExecute = false;
                        P.StartInfo.RedirectStandardOutput = true;
                        P.StartInfo.RedirectStandardError = true;
                        P.StartInfo.CreateNoWindow = true;
                        P.Start();
                        P.WaitForExit();

                        stdOut = P.StandardOutput.ReadToEnd();
                        stdErr = P.StandardError.ReadToEnd();

                        const string FIND_STRING = "Driver Version: ";

                        using (StringReader reader = new StringReader(stdOut))
                        {
                            var line = string.Empty;

                            do
                            {
                                line = reader.ReadLine();

                                if (line != null)
                                {
                                    if (line.Contains(FIND_STRING))
                                    {
                                        var start = line.IndexOf(FIND_STRING);
                                        var driverVer = line.Substring(start, start + 7);
                                        driverVer = driverVer.Replace(FIND_STRING, "").Substring(0, 7).Trim();
                                        var drVerDouble = double.Parse(driverVer, CultureInfo.InvariantCulture);
                                        var dot = driverVer.IndexOf(".");
                                        var leftPart = int.Parse(driverVer.Substring(0, 3));
                                        var rightPart = int.Parse(driverVer.Substring(4, 2));
                                        return new NVIDIA_SMI_DRIVER(leftPart, rightPart);
                                    }
                                }
                            } while (line != null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helpers.ConsolePrint(TAG, "GetNvidiaSMIDriver Exception: " + ex.Message);
                        return INVALID_SMI_DRIVER;
                    }
                }

                return INVALID_SMI_DRIVER;
            }

            private static void ShowMessageAndStep(string infoMsg)
            {
                if (MessageNotifier != null) MessageNotifier.SetMessageAndIncrementStep(infoMsg);
            }

            public static IMessageNotifier MessageNotifier { get; private set; }

            public static bool CheckVideoControllersCountMismath()
            {
                // this function checks if count of CUDA devices is same as it was on application start, reason for that is
                // because of some reason (especially when algo switching occure) CUDA devices are dissapiring from system
                // creating tons of problems e.g. miners stop mining, lower rig hashrate etc.

                /* commented because when GPU is "lost" windows still see all of them
                // first check windows video controlers
                List<VideoControllerData> currentAvaliableVideoControllers = new List<VideoControllerData>();
                WindowsDisplayAdapters.QueryVideoControllers(currentAvaliableVideoControllers, false);

                int GPUsOld = AvaliableVideoControllers.Count;
                int GPUsNew = currentAvaliableVideoControllers.Count;

                Helpers.ConsolePrint("ComputeDeviceManager.CheckCount", "Video controlers GPUsOld: " + GPUsOld.ToString() + " GPUsNew:" + GPUsNew.ToString());
                */

                // check CUDA devices
                var currentCUDA_Devices = new List<CudaDevice>();

                if (!NVIDIA.IsSkipNVIDIA())
                    NVIDIA.QueryCudaDevices(ref currentCUDA_Devices);

                var GPUsOld = CUDA_Devices.Count;
                var GPUsNew = currentCUDA_Devices.Count;

                Helpers.ConsolePrint("ComputeDeviceManager.CheckCount", "CUDA GPUs count: Old: " + GPUsOld.ToString() + " / New: " + GPUsNew.ToString());

                return (GPUsNew < GPUsOld);
            }

            public static void QueryDevices(IMessageNotifier messageNotifier)
            {
                // check NVIDIA nvml.dll and copy over scope
                {
                    var nvmlPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\NVIDIA Corporation\\NVSMI\\nvml.dll";
                    if (nvmlPath.Contains(" (x86)")) nvmlPath = nvmlPath.Replace(" (x86)", "");

                    if (File.Exists(nvmlPath))
                    {
                        var copyToPath = Directory.GetCurrentDirectory() + "\\nvml.dll";

                        try
                        {
                            File.Copy(nvmlPath, copyToPath, true);
                            Helpers.ConsolePrint(TAG, string.Format("Copy from {0} to {1} done", nvmlPath, copyToPath));
                        }
                        catch (Exception e)
                        {
                            Helpers.ConsolePrint(TAG, "Copy nvml.dll failed: " + e.Message);
                        }
                    }
                }

                MessageNotifier = messageNotifier;
                // #0 get video controllers, used for cross checking
                WindowsDisplayAdapters.QueryVideoControllers();
                // Order important CPU Query must be first
                // #1 CPU
                // We skip CPUs because zPool does not have cryptonight
                CPU.QueryCPUs();
                // #2 CUDA
                if (NVIDIA.IsSkipNVIDIA())
                {
                    Helpers.ConsolePrint(TAG, "Skipping NVIDIA device detection, settings are set to disabled");
                }
                else
                {
                    ShowMessageAndStep(International.GetText("Compute_Device_Query_Manager_CUDA_Query"));
                    NVIDIA.QueryCudaDevices();
                }

                // OpenCL and AMD
                if (ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionAMD)
                {
                    Helpers.ConsolePrint(TAG, "Skipping AMD device detection, settings set to disabled");
                    ShowMessageAndStep(International.GetText("Compute_Device_Query_Manager_AMD_Query_Skip"));
                }
                else
                {
                    // #3 OpenCL
                    ShowMessageAndStep(International.GetText("Compute_Device_Query_Manager_OpenCL_Query"));
                    OpenCL.QueryOpenCLDevices();
                    // #4 AMD query AMD from OpenCL devices, get serial and add devices
                    AMD.QueryAMD();
                }

                // #5 uncheck CPU if GPUs present, call it after we Query all devices
                Group.UncheckedCPU();

                // TODO update this to report undetected hardware
                // #6 check NVIDIA, AMD devices count
                var nvidiaCount = 0;

                {
                    var amdCount = 0;

                    foreach (var vidCtrl in AvaliableVideoControllers)
                    {
                        if (vidCtrl.Name.ToLower().Contains("nvidia") && CUDA_Unsupported.IsSupported(vidCtrl.Name))
                        {
                            nvidiaCount += 1;
                        }
                        else if (vidCtrl.Name.ToLower().Contains("nvidia"))
                        {
                            Helpers.ConsolePrint(TAG,
                                "Device not supported NVIDIA/CUDA device not supported " + vidCtrl.Name);
                        }

                        amdCount += (vidCtrl.Name.ToLower().Contains("amd")) ? 1 : 0;
                    }

                    Helpers.ConsolePrint(TAG,
                        nvidiaCount == CUDA_Devices.Count
                            ? "Cuda NVIDIA/CUDA device count GOOD"
                            : "Cuda NVIDIA/CUDA device count BAD!!!");

                    Helpers.ConsolePrint(TAG,
                        amdCount == AMD_Devices.Count ? "AMD GPU device count GOOD" : "AMD GPU device count BAD!!!");
                }

                // allerts
                _currentNvidiaSMIDriver = GetNvidiaSMIDriver();
                // if we have nvidia cards but no CUDA devices tell the user to upgrade driver
                var isNvidiaErrorShown = false; // to prevent showing twice
                var showWarning = ConfigManager.GeneralConfig.ShowDriverVersionWarning && WindowsDisplayAdapters.HasNvidiaVideoController();

                if (showWarning && CUDA_Devices.Count != nvidiaCount && _currentNvidiaSMIDriver.IsLesserVersionThan(NVIDIA_MIN_DETECTION_DRIVER))
                {
                    isNvidiaErrorShown = true;
                    var minDriver = NVIDIA_MIN_DETECTION_DRIVER.ToString();
                    var recomendDrvier = NVIDIA_RECOMENDED_DRIVER.ToString();

                    MessageBox.Show(string.Format(International.GetText("Compute_Device_Query_Manager_NVIDIA_Driver_Detection"),
                        minDriver, recomendDrvier),
                                                          International.GetText("Compute_Device_Query_Manager_NVIDIA_RecomendedDriver_Title"),
                                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // recomended driver
                if (showWarning && _currentNvidiaSMIDriver.IsLesserVersionThan(NVIDIA_RECOMENDED_DRIVER) && !isNvidiaErrorShown && _currentNvidiaSMIDriver.leftPart > -1)
                {
                    var recomendDrvier = NVIDIA_RECOMENDED_DRIVER.ToString();

                    var nvdriverString = _currentNvidiaSMIDriver.leftPart > -1 ? string.Format(International.GetText("Compute_Device_Query_Manager_NVIDIA_Driver_Recomended_PART"), _currentNvidiaSMIDriver.ToString())
                    : "";

                    MessageBox.Show(string.Format(International.GetText("Compute_Device_Query_Manager_NVIDIA_Driver_Recomended"),
                        recomendDrvier, nvdriverString, recomendDrvier),
                                                          International.GetText("Compute_Device_Query_Manager_NVIDIA_RecomendedDriver_Title"),
                                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // no devices found
                if (Available.AllAvaliableDevices.Count <= 0)
                {
                    var result = MessageBox.Show(International.GetText("Compute_Device_Query_Manager_No_Devices"),
                                                          International.GetText("Compute_Device_Query_Manager_No_Devices_Title"),
                                                          MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(Links.NHM_NoDev_Help);
                    }
                }

                // create AMD bus ordering for Claymore
                var amdDevices = Available.AllAvaliableDevices.FindAll((a) => a.DeviceType == DeviceType.AMD);
                amdDevices.Sort((a, b) => a.BusID.CompareTo(b.BusID));

                for (var i = 0; i < amdDevices.Count; i++)
                    amdDevices[i].IDByBus = i;

                // create NV bus ordering for Claymore
                var nvDevices = Available.AllAvaliableDevices.FindAll((a) => a.DeviceType == DeviceType.NVIDIA);
                nvDevices.Sort((a, b) => a.BusID.CompareTo(b.BusID));

                for (var i = 0; i < nvDevices.Count; i++)
                    nvDevices[i].IDByBus = i;

                // get GPUs RAM sum
                // bytes
                Available.NVIDIA_RAM_SUM = 0;
                Available.AMD_RAM_SUM = 0;

                foreach (var dev in Available.AllAvaliableDevices)
                {
                    if (dev.DeviceType == DeviceType.NVIDIA)
                    {
                        Available.NVIDIA_RAM_SUM += dev.GpuRam;
                    }
                    else if (dev.DeviceType == DeviceType.AMD)
                    {
                        Available.AMD_RAM_SUM += dev.GpuRam;
                    }
                }

                // Make gpu ram needed not larger than 4GB per GPU
                var total_GPU_RAM = Math.Min((Available.NVIDIA_RAM_SUM + Available.AMD_RAM_SUM) * 0.6 / 1024, (double)Available.AvailGPUs * 4 * 1024 * 1024);
                double total_Sys_RAM = SystemSpecs.FreePhysicalMemory + SystemSpecs.FreeVirtualMemory;
                // check
                if (ConfigManager.GeneralConfig.ShowDriverVersionWarning && total_Sys_RAM < total_GPU_RAM)
                {
                    Helpers.ConsolePrint(TAG, "virtual memory size BAD");

                    MessageBox.Show(International.GetText("VirtualMemorySize_BAD"),
                                International.GetText("Warning_with_Exclamation"),
                                MessageBoxButtons.OK);
                }
                else
                {
                    Helpers.ConsolePrint(TAG, "virtual memory size GOOD");
                }

                // #x remove reference
                MessageNotifier = null;
            }

            #region Helpers

            private class VideoControllerData
            {
                public string Name { get; set; }
                public string Description { get; set; }
                public string PNPDeviceID { get; set; }
                public string DriverVersion { get; set; }
                public string Status { get; set; }
                public string InfSection { get; set; } // get arhitecture
                public ulong AdapterRAM { get; set; }
            }

            private static List<VideoControllerData> AvaliableVideoControllers = new List<VideoControllerData>();

            private static class WindowsDisplayAdapters
            {
                private static string SafeGetProperty(ManagementBaseObject mbo, string key)
                {
                    try
                    {
                        var o = mbo.GetPropertyValue(key);
                        if (o != null) return o.ToString();
                    }
                    catch { }

                    return "key is null";
                }

                public static void QueryVideoControllers()
                {
                    QueryVideoControllers(AvaliableVideoControllers, true);
                }

                public static void QueryVideoControllers(List<VideoControllerData> avaliableVideoControllers, bool warningsEnabled)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("");
                    stringBuilder.AppendLine("QueryVideoControllers: ");
                    var moc = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController WHERE PNPDeviceID LIKE 'PCI%'").Get();
                    var allVideoContollersOK = true;

                    foreach (var manObj in moc)
                    {
                        // Int16 ram_Str = manObj["ProtocolSupported"] as Int16; manObj["AdapterRAM"] as string
                        ulong.TryParse(SafeGetProperty(manObj, "AdapterRAM"), out ulong memTmp);

                        var vidController = new VideoControllerData()
                        {
                            Name = SafeGetProperty(manObj, "Name"),
                            Description = SafeGetProperty(manObj, "Description"),
                            PNPDeviceID = SafeGetProperty(manObj, "PNPDeviceID"),
                            DriverVersion = SafeGetProperty(manObj, "DriverVersion"),
                            Status = SafeGetProperty(manObj, "Status"),
                            InfSection = SafeGetProperty(manObj, "InfSection"),
                            AdapterRAM = memTmp
                        };

                        stringBuilder.AppendLine("\tWin32_VideoController detected:");
                        stringBuilder.AppendLine(string.Format("\t\tName {0}", vidController.Name));
                        stringBuilder.AppendLine(string.Format("\t\tDescription {0}", vidController.Description));
                        stringBuilder.AppendLine(string.Format("\t\tPNPDeviceID {0}", vidController.PNPDeviceID));
                        stringBuilder.AppendLine(string.Format("\t\tDriverVersion {0}", vidController.DriverVersion));
                        stringBuilder.AppendLine(string.Format("\t\tStatus {0}", vidController.Status));
                        stringBuilder.AppendLine(string.Format("\t\tInfSection {0}", vidController.InfSection));
                        stringBuilder.AppendLine(string.Format("\t\tAdapterRAM {0}", vidController.AdapterRAM));

                        // check if controller ok
                        if (allVideoContollersOK && !vidController.Status.ToLower().Equals("ok"))
                        {
                            allVideoContollersOK = false;
                        }

                        avaliableVideoControllers.Add(vidController);
                    }

                    Helpers.ConsolePrint(TAG, stringBuilder.ToString());

                    if (warningsEnabled)
                    {
                        if (ConfigManager.GeneralConfig.ShowDriverVersionWarning && !allVideoContollersOK)
                        {
                            var msg = International.GetText("QueryVideoControllers_NOT_ALL_OK_Msg");

                            foreach (var vc in avaliableVideoControllers)
                            {
                                if (!vc.Status.ToLower().Equals("ok"))
                                {
                                    msg += Environment.NewLine
                                        + string.Format(International.GetText("QueryVideoControllers_NOT_ALL_OK_Msg_Append"), vc.Name, vc.Status, vc.PNPDeviceID);
                                }
                            }

                            MessageBox.Show(msg,
                                            International.GetText("QueryVideoControllers_NOT_ALL_OK_Title"),
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                public static bool HasNvidiaVideoController()
                {
                    foreach (var vctrl in AvaliableVideoControllers)
                        if (vctrl.Name.ToLower().Contains("nvidia")) return true;

                    return false;
                }
            }

            private static class CPU
            {
                public static void QueryCPUs()
                {
                    Helpers.ConsolePrint(TAG, "QueryCPUs START");
                    // get all CPUs
                    Available.CPUsCount = CPUID.GetPhysicalProcessorCount();
                    Available.IsHyperThreadingEnabled = CPUID.IsHypeThreadingEnabled();

                    Helpers.ConsolePrint(TAG, Available.IsHyperThreadingEnabled ? "HyperThreadingEnabled = TRUE" : "HyperThreadingEnabled = FALSE");

                    // get all cores (including virtual - HT can benefit mining)
                    var ThreadsPerCPU = CPUID.GetVirtualCoresCount() / Available.CPUsCount;

                    if (!Helpers.Is64BitOperatingSystem)
                    {
                        MessageBox.Show(International.GetText("Form_Main_msgbox_CPUMining64bitMsg"),
                                        International.GetText("Warning_with_Exclamation"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        Available.CPUsCount = 0;
                    }

                    if (ThreadsPerCPU * Available.CPUsCount > 64)
                    {
                        MessageBox.Show(International.GetText("Form_Main_msgbox_CPUMining64CoresMsg"),
                                        International.GetText("Warning_with_Exclamation"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        Available.CPUsCount = 0;
                    }

                    // TODO important move this to settings
                    var ThreadsPerCPUMask = ThreadsPerCPU;
                    Globals.ThreadsPerCPU = ThreadsPerCPU;

                    if (CPUUtils.IsCPUMiningCapable())
                    {
                        if (Available.CPUsCount == 1)
                        {
                            Available.AllAvaliableDevices
                                .Add(
                                new CPUComputeDevice(0, "CPU0", CPUID.GetCPUName().Trim(), ThreadsPerCPU, (ulong)0, ++CPUCount)
                            );
                        }
                        else if (Available.CPUsCount > 1)
                        {
                            for (int i = 0; i < Available.CPUsCount; i++)
                            {
                                Available.AllAvaliableDevices
                                    .Add(
                                    new CPUComputeDevice(i, "CPU" + i, CPUID.GetCPUName().Trim(), ThreadsPerCPU, CPUID.CreateAffinityMask(i, ThreadsPerCPUMask), ++CPUCount)
                                );
                            }
                        }
                    }

                    Helpers.ConsolePrint(TAG, "QueryCPUs END");
                }
            }

            private static List<CudaDevice> CUDA_Devices = new List<CudaDevice>();

            private static class NVIDIA
            {
                private static string QueryCudaDevicesString = "";

                private static void QueryCudaDevicesOutputErrorDataReceived(object sender, DataReceivedEventArgs e)
                {
                    if (e.Data != null)
                    {
                        QueryCudaDevicesString += e.Data;
                    }
                }

                public static bool IsSkipNVIDIA()
                {
                    return ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionNVIDIA;
                }

                public static void QueryCudaDevices()
                {
                    Helpers.ConsolePrint(TAG, "QueryCudaDevices START");
                    QueryCudaDevices(ref CUDA_Devices);

                    if (CUDA_Devices != null && CUDA_Devices.Count != 0)
                    {
                        Available.HasNVIDIA = true;
                        var stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine("");
                        stringBuilder.AppendLine("CudaDevicesDetection:");

                        // Enumerate NVAPI handles and map to busid
                        var idHandles = new Dictionary<int, NvPhysicalGpuHandle>();

                        if (NVAPI.IsAvailable)
                        {
                            var handles = new NvPhysicalGpuHandle[NVAPI.MAX_PHYSICAL_GPUS];

                            if (NVAPI.NvAPI_EnumPhysicalGPUs == null)
                            {
                                Helpers.ConsolePrint("NVAPI", "NvAPI_EnumPhysicalGPUs unavailable");
                            }
                            else
                            {
                                var status = NVAPI.NvAPI_EnumPhysicalGPUs(handles, out int count);

                                if (status != NvStatus.OK)
                                {
                                    Helpers.ConsolePrint("NVAPI", "Enum physical GPUs failed with status: " + status);
                                }
                                else
                                {
                                    foreach (var handle in handles)
                                    {
                                        var id = -1;
                                        var idStatus = NVAPI.NvAPI_GPU_GetBusID(handle, out id);

                                        if (idStatus != NvStatus.EXPECTED_PHYSICAL_GPU_HANDLE)
                                        {
                                            if (idStatus != NvStatus.OK)
                                            {
                                                Helpers.ConsolePrint("NVAPI", "Bus ID get failed with status: " + idStatus);
                                            }
                                            else
                                            {
                                                Helpers.ConsolePrint("NVAPI", "Found handle for busid " + id);
                                                idHandles[id] = handle;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        var nvmlInit = false;

                        try
                        {
                            var ret = NvmlNativeMethods.nvmlInit();

                            if (ret != nvmlReturn.Success)
                                throw new Exception($"NVML init failed with code {ret}");

                            nvmlInit = true;
                        }
                        catch (Exception e)
                        {
                            Helpers.ConsolePrint("NVML", e.ToString());
                        }

                        foreach (var cudaDev in CUDA_Devices)
                        {
                            // check sm vesrions
                            bool isUnderSM21;

                            {
                                var isUnderSM2_major = cudaDev.SM_major < 2;
                                var isUnderSM1_minor = cudaDev.SM_minor < 1;
                                isUnderSM21 = isUnderSM2_major && isUnderSM1_minor;
                            }

                            // bool isOverSM6 = cudaDev.SM_major > 6;
                            var skip = isUnderSM21;
                            var skipOrAdd = skip ? "SKIPED" : "ADDED";
                            var isDisabledGroupStr = ""; // TODO remove
                            var etherumCapableStr = cudaDev.IsEtherumCapable() ? "YES" : "NO";
                            stringBuilder.AppendLine(string.Format("\t{0} device{1}:", skipOrAdd, isDisabledGroupStr));
                            stringBuilder.AppendLine(string.Format("\t\tID: {0}", cudaDev.DeviceID.ToString()));
                            stringBuilder.AppendLine(string.Format("\t\tBusID: {0}", cudaDev.pciBusID.ToString()));
                            stringBuilder.AppendLine(string.Format("\t\tNAME: {0}", cudaDev.GetName()));
                            stringBuilder.AppendLine(string.Format("\t\tVENDOR: {0}", cudaDev.VendorName));
                            stringBuilder.AppendLine(string.Format("\t\tUUID: {0}", cudaDev.UUID));
                            stringBuilder.AppendLine(string.Format("\t\tSM: {0}", cudaDev.SMVersionString));
                            stringBuilder.AppendLine(string.Format("\t\tMEMORY: {0}", cudaDev.DeviceGlobalMemory.ToString()));
                            stringBuilder.AppendLine(string.Format("\t\tETHEREUM: {0}", etherumCapableStr));

                            if (!skip)
                            {
                                DeviceGroupType group;

                                switch (cudaDev.SM_major)
                                {
                                    case 2:
                                        group = DeviceGroupType.NVIDIA_2_1;
                                        break;

                                    case 3:
                                        group = DeviceGroupType.NVIDIA_3_x;
                                        break;

                                    case 5:
                                        group = DeviceGroupType.NVIDIA_5_x;
                                        break;

                                    case 6:
                                        group = DeviceGroupType.NVIDIA_6_x;
                                        break;

                                    default:
                                        group = DeviceGroupType.NVIDIA_6_x;
                                        break;
                                }

                                var nvmlHandle = new ManagedCuda.Nvml.nvmlDevice();

                                if (nvmlInit)
                                {
                                    var ret = NvmlNativeMethods.nvmlDeviceGetHandleByUUID(cudaDev.UUID, ref nvmlHandle);

                                    stringBuilder.AppendLine(
                                        "\t\tNVML HANDLE: " +
                                        $"{(ret == nvmlReturn.Success ? nvmlHandle.Pointer.ToString() : $"Failed with code ret {ret}")}");
                                }

                                idHandles.TryGetValue(cudaDev.pciBusID, out var handle);

                                Available.AllAvaliableDevices
                                    .Add(
                                    new CudaComputeDevice(cudaDev, group, ++GPUCount, handle, nvmlHandle)
                                );
                            }
                        }

                        Helpers.ConsolePrint(TAG, stringBuilder.ToString());
                    }

                    Helpers.ConsolePrint(TAG, "QueryCudaDevices END");
                }

                public static void QueryCudaDevices(ref List<CudaDevice> cudaDevices)
                {
                    QueryCudaDevicesString = "";

                    var CudaDevicesDetection = new Process();
                    CudaDevicesDetection.StartInfo.FileName = "CudaDeviceDetection.exe";
                    CudaDevicesDetection.StartInfo.UseShellExecute = false;
                    CudaDevicesDetection.StartInfo.RedirectStandardError = true;
                    CudaDevicesDetection.StartInfo.RedirectStandardOutput = true;
                    CudaDevicesDetection.StartInfo.CreateNoWindow = true;
                    CudaDevicesDetection.OutputDataReceived += QueryCudaDevicesOutputErrorDataReceived;
                    CudaDevicesDetection.ErrorDataReceived += QueryCudaDevicesOutputErrorDataReceived;

                    const int waitTime = 30 * 1000; // 30seconds

                    try
                    {
                        if (!CudaDevicesDetection.Start())
                        {
                            Helpers.ConsolePrint(TAG, "CudaDevicesDetection process could not start");
                        }
                        else
                        {
                            CudaDevicesDetection.BeginErrorReadLine();
                            CudaDevicesDetection.BeginOutputReadLine();

                            if (CudaDevicesDetection.WaitForExit(waitTime))
                            {
                                CudaDevicesDetection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO
                        Helpers.ConsolePrint(TAG, "CudaDevicesDetection threw Exception: " + ex.Message);
                    }
                    finally
                    {
                        if (QueryCudaDevicesString != "")
                        {
                            try
                            {
                                cudaDevices = JsonConvert.DeserializeObject<List<CudaDevice>>(QueryCudaDevicesString, Globals.JsonSettings);
                            }
                            catch { }

                            if (CUDA_Devices == null || CUDA_Devices.Count == 0)
                                Helpers.ConsolePrint(TAG, "CudaDevicesDetection found no devices. CudaDevicesDetection returned: " + QueryCudaDevicesString);
                        }
                    }
                }
            }

            private class OpenCLJSONData_t
            {
                public string PlatformName = "NONE";
                public int PlatformNum;
                public List<OpenCLDevice> Devices = new List<OpenCLDevice>();
            }

            private static List<OpenCLJSONData_t> OpenCLJSONData = new List<OpenCLJSONData_t>();
            private static bool IsOpenCLQuerrySuccess;

            private static class OpenCL
            {
                private static string QueryOpenCLDevicesString = "";

                private static void QueryOpenCLDevicesOutputErrorDataReceived(object sender, DataReceivedEventArgs e)
                {
                    if (e.Data != null)
                    {
                        QueryOpenCLDevicesString += e.Data;
                    }
                }

                public static void QueryOpenCLDevices()
                {
                    Helpers.ConsolePrint(TAG, "QueryOpenCLDevices START");
                    var OpenCLDevicesDetection = new Process();
                    OpenCLDevicesDetection.StartInfo.FileName = "AMDOpenCLDeviceDetection.exe";
                    OpenCLDevicesDetection.StartInfo.UseShellExecute = false;
                    OpenCLDevicesDetection.StartInfo.RedirectStandardError = true;
                    OpenCLDevicesDetection.StartInfo.RedirectStandardOutput = true;
                    OpenCLDevicesDetection.StartInfo.CreateNoWindow = true;
                    OpenCLDevicesDetection.OutputDataReceived += QueryOpenCLDevicesOutputErrorDataReceived;
                    OpenCLDevicesDetection.ErrorDataReceived += QueryOpenCLDevicesOutputErrorDataReceived;

                    const int waitTime = 30 * 1000; // 30seconds

                    try
                    {
                        if (!OpenCLDevicesDetection.Start())
                        {
                            Helpers.ConsolePrint(TAG, "AMDOpenCLDeviceDetection process could not start");
                        }
                        else
                        {
                            OpenCLDevicesDetection.BeginErrorReadLine();
                            OpenCLDevicesDetection.BeginOutputReadLine();

                            if (OpenCLDevicesDetection.WaitForExit(waitTime))
                            {
                                OpenCLDevicesDetection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO
                        Helpers.ConsolePrint(TAG, "AMDOpenCLDeviceDetection threw Exception: " + ex.Message);
                    }
                    finally
                    {
                        if (QueryOpenCLDevicesString != "")
                        {
                            try
                            {
                                OpenCLJSONData = JsonConvert.DeserializeObject<List<OpenCLJSONData_t>>(QueryOpenCLDevicesString, Globals.JsonSettings);
                            }
                            catch
                            {
                                OpenCLJSONData = null;
                            }
                        }
                    }

                    if (OpenCLJSONData == null)
                    {
                        Helpers.ConsolePrint(TAG, "AMDOpenCLDeviceDetection found no devices. AMDOpenCLDeviceDetection returned: " + QueryOpenCLDevicesString);
                    }
                    else
                    {
                        IsOpenCLQuerrySuccess = true;
                        var stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine("");
                        stringBuilder.AppendLine("AMDOpenCLDeviceDetection found devices success:");

                        foreach (var oclElem in OpenCLJSONData)
                        {
                            stringBuilder.AppendLine(string.Format("\tFound devices for platform: {0}", oclElem.PlatformName));

                            foreach (var oclDev in oclElem.Devices)
                            {
                                stringBuilder.AppendLine("\t\tDevice:");
                                stringBuilder.AppendLine(string.Format("\t\t\tDevice ID {0}", oclDev.DeviceID));
                                stringBuilder.AppendLine(string.Format("\t\t\tDevice NAME {0}", oclDev._CL_DEVICE_NAME));
                                stringBuilder.AppendLine(string.Format("\t\t\tDevice TYPE {0}", oclDev._CL_DEVICE_TYPE));
                            }
                        }

                        Helpers.ConsolePrint(TAG, stringBuilder.ToString());
                    }

                    Helpers.ConsolePrint(TAG, "QueryOpenCLDevices END");
                }
            }

            public static List<OpenCLDevice> AMD_Devices = new List<OpenCLDevice>();

            private static class AMD
            {
                public static void QueryAMD()
                {
                    const int AMD_VENDOR_ID = 1002;
                    Helpers.ConsolePrint(TAG, "QueryAMD START");

                    #region AMD driver check, ADL returns 0

                    // check the driver version bool EnableOptimizedVersion = true;
                    var deviceDriverOld = new Dictionary<string, bool>();
                    var deviceDriverNO_neoscrypt_lyra2re = new Dictionary<string, bool>();
                    var ShowWarningDialog = false;

                    foreach (var vidContrllr in AvaliableVideoControllers)
                    {
                        Helpers.ConsolePrint(TAG, string.Format("Checking AMD device (driver): {0} ({1})", vidContrllr.Name, vidContrllr.DriverVersion));

                        deviceDriverOld[vidContrllr.Name] = false;
                        deviceDriverNO_neoscrypt_lyra2re[vidContrllr.Name] = false;
                        var sgminer_NO_neoscrypt_lyra2re = new Version("21.19.164.1");
                        // TODO checking radeon drivers only?
                        if ((vidContrllr.Name.Contains("AMD") || vidContrllr.Name.Contains("Radeon")) && ShowWarningDialog == false)
                        {
                            var AMDDriverVersion = new Version(vidContrllr.DriverVersion);

                            if (!ConfigManager.GeneralConfig.ForceSkipAMDNeoscryptLyraCheck)
                            {
                                var greaterOrEqual = AMDDriverVersion.CompareTo(sgminer_NO_neoscrypt_lyra2re) >= 0;

                                if (greaterOrEqual)
                                {
                                    deviceDriverNO_neoscrypt_lyra2re[vidContrllr.Name] = true;

                                    Helpers.ConsolePrint(TAG,
                                        "Driver version seems to be " + sgminer_NO_neoscrypt_lyra2re.ToString() +
                                        " or higher. NeoScrypt and Lyra2REv2 will be removed from list");
                                }
                            }

                            if (AMDDriverVersion.Major < 15)
                            {
                                ShowWarningDialog = true;
                                deviceDriverOld[vidContrllr.Name] = true;

                                Helpers.ConsolePrint(TAG, "WARNING!!! Old AMD GPU driver detected! All optimized versions disabled, mining " +
                                    "speed will not be optimal. Consider upgrading AMD GPU driver. Recommended AMD GPU driver version is 15.7.1.");
                            }
                        }
                    }

                    if (ConfigManager.GeneralConfig.ShowDriverVersionWarning && ShowWarningDialog == true)
                    {
                        Form WarningDialog = new DriverVersionConfirmationDialog();
                        WarningDialog.ShowDialog();
                        WarningDialog = null;
                    }

                    #endregion AMD driver check, ADL returns 0

                    // get platform version
                    ShowMessageAndStep(International.GetText("Compute_Device_Query_Manager_AMD_Query"));
                    var amdOCLDevices = new List<OpenCLDevice>();
                    var AMDOpenCLPlatformStringKey = "";

                    if (IsOpenCLQuerrySuccess)
                    {
                        var amdPlatformNumFound = false;

                        foreach (var oclEl in OpenCLJSONData)
                        {
                            if (oclEl.PlatformName.Contains("AMD") || oclEl.PlatformName.Contains("amd"))
                            {
                                amdPlatformNumFound = true;
                                AMDOpenCLPlatformStringKey = oclEl.PlatformName;
                                Available.AmdOpenCLPlatformNum = oclEl.PlatformNum;
                                amdOCLDevices = oclEl.Devices;

                                Helpers.ConsolePrint(TAG, string.Format("AMD platform found: Key: {0}, Num: {1}",
                                    AMDOpenCLPlatformStringKey,
                                    Available.AmdOpenCLPlatformNum.ToString()));

                                break;
                            }
                        }

                        if (amdPlatformNumFound)
                        {
                            // get only AMD gpus
                            {
                                foreach (var oclDev in amdOCLDevices)
                                {
                                    if (oclDev._CL_DEVICE_TYPE.Contains("GPU"))
                                    {
                                        AMD_Devices.Add(oclDev);
                                    }
                                }
                            }

                            var isBusID_OK = true;
                            // check if buss ids are unique and different from -1
                            {
                                var bus_ids = new HashSet<int>();

                                foreach (var amdOclDev in AMD_Devices)
                                {
                                    if (amdOclDev.AMD_BUS_ID < 0)
                                    {
                                        isBusID_OK = false;
                                        break;
                                    }

                                    bus_ids.Add(amdOclDev.AMD_BUS_ID);
                                }

                                // check if unique
                                isBusID_OK = isBusID_OK && bus_ids.Count == AMD_Devices.Count;
                            }

                            if (AMD_Devices.Count == 0)
                            {
                                Helpers.ConsolePrint(TAG, "AMD GPUs count is 0");
                            }
                            else
                            {
                                // print BUS id status
                                if (isBusID_OK)
                                {
                                    Helpers.ConsolePrint(TAG, "AMD Bus IDs are unique and valid. OK");
                                }
                                else
                                {
                                    Helpers.ConsolePrint(TAG, "AMD Bus IDs IS INVALID. Using fallback AMD detection mode");
                                }

                                Helpers.ConsolePrint(TAG, "AMD GPUs count : " + AMD_Devices.Count.ToString());
                                Helpers.ConsolePrint(TAG, "AMD Getting device name and serial from ADL");
                                // ADL
                                var isAdlInit = true;
                                // ADL does not get devices in order map devices by bus number
                                // bus id, <name, uuid>
                                var _busIdsInfo = new Dictionary<int, Tuple<string, string, string, int>>();
                                var _amdDeviceName = new List<string>();
                                var _amdDeviceUUID = new List<string>();

                                try
                                {
                                    var ADLRet = -1;
                                    var NumberOfAdapters = 0;

                                    if (null != ADL.ADL_Main_Control_Create)
                                        // Second parameter is 1: Get only the present adapters
                                        ADLRet = ADL.ADL_Main_Control_Create(ADL.ADL_Main_Memory_Alloc, 1);

                                    if (ADL.ADL_SUCCESS == ADLRet)
                                    {
                                        ADL.ADL_Adapter_NumberOfAdapters_Get?.Invoke(ref NumberOfAdapters);
                                        Helpers.ConsolePrint(TAG, "Number Of Adapters: " + NumberOfAdapters.ToString());

                                        if (0 < NumberOfAdapters)
                                        {
                                            // Get OS adpater info from ADL
                                            ADLAdapterInfoArray OSAdapterInfoData;
                                            OSAdapterInfoData = new ADLAdapterInfoArray();

                                            if (null != ADL.ADL_Adapter_AdapterInfo_Get)
                                            {
                                                var AdapterBuffer = IntPtr.Zero;
                                                var size = Marshal.SizeOf(OSAdapterInfoData);
                                                AdapterBuffer = Marshal.AllocCoTaskMem((int)size);
                                                Marshal.StructureToPtr(OSAdapterInfoData, AdapterBuffer, false);

                                                if (null != ADL.ADL_Adapter_AdapterInfo_Get)
                                                {
                                                    ADLRet = ADL.ADL_Adapter_AdapterInfo_Get(AdapterBuffer, size);

                                                    if (ADL.ADL_SUCCESS == ADLRet)
                                                    {
                                                        OSAdapterInfoData = (ADLAdapterInfoArray)Marshal.PtrToStructure(AdapterBuffer, OSAdapterInfoData.GetType());
                                                        var IsActive = 0;

                                                        for (int i = 0; i < NumberOfAdapters; i++)
                                                        {
                                                            // Check if the adapter is active
                                                            if (null != ADL.ADL_Adapter_Active_Get)
                                                                ADLRet = ADL.ADL_Adapter_Active_Get(OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex, ref IsActive);

                                                            if (ADL.ADL_SUCCESS == ADLRet)
                                                            {
                                                                // we are looking for amd
                                                                // TODO check discrete and integrated GPU separation
                                                                var vendorID = OSAdapterInfoData.ADLAdapterInfo[i].VendorID;
                                                                var devName = OSAdapterInfoData.ADLAdapterInfo[i].AdapterName;

                                                                if (vendorID == AMD_VENDOR_ID
                                                                    || devName.ToLower().Contains("amd")
                                                                    || devName.ToLower().Contains("radeon")
                                                                    || devName.ToLower().Contains("firepro"))
                                                                {
                                                                    var PNPStr = OSAdapterInfoData.ADLAdapterInfo[i].PNPString;
                                                                    // find vi controller pnp
                                                                    var infSection = "";

                                                                    foreach (var v_ctrl in AvaliableVideoControllers)
                                                                    {
                                                                        if (v_ctrl.PNPDeviceID == PNPStr)
                                                                        {
                                                                            infSection = v_ctrl.InfSection;
                                                                        }
                                                                    }

                                                                    var backSlashLast = PNPStr.LastIndexOf('\\');
                                                                    var serial = PNPStr.Substring(backSlashLast, PNPStr.Length - backSlashLast);
                                                                    var end_0 = serial.IndexOf('&');
                                                                    var end_1 = serial.IndexOf('&', end_0 + 1);
                                                                    // get serial
                                                                    serial = serial.Substring(end_0 + 1, (end_1 - end_0) - 1);

                                                                    var udid = OSAdapterInfoData.ADLAdapterInfo[i].UDID;
                                                                    var pciVen_id_strSize = 21; // PCI_VEN_XXXX&DEV_XXXX
                                                                    var uuid = udid.Substring(0, pciVen_id_strSize) + "_" + serial;
                                                                    var budId = OSAdapterInfoData.ADLAdapterInfo[i].BusNumber;
                                                                    var index = OSAdapterInfoData.ADLAdapterInfo[i].AdapterIndex;

                                                                    if (!_amdDeviceUUID.Contains(uuid))
                                                                    {
                                                                        try
                                                                        {
                                                                            Helpers.ConsolePrint(TAG, string.Format("ADL device added BusNumber:{0}  NAME:{1}  UUID:{2}"),
                                                                                budId,
                                                                                devName,
                                                                                uuid);
                                                                        }
                                                                        catch { }

                                                                        _amdDeviceUUID.Add(uuid);
                                                                        // _busIds.Add(OSAdapterInfoData.ADLAdapterInfo[i].BusNumber);
                                                                        _amdDeviceName.Add(devName);

                                                                        if (!_busIdsInfo.ContainsKey(budId))
                                                                        {
                                                                            var nameUuid = new Tuple<string, string, string, int>(devName, uuid, infSection, index);
                                                                            _busIdsInfo.Add(budId, nameUuid);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Helpers.ConsolePrint(TAG, "ADL_Adapter_AdapterInfo_Get() returned error code " + ADLRet.ToString());
                                                        isAdlInit = false;
                                                    }
                                                }

                                                // Release the memory for the AdapterInfo structure
                                                if (IntPtr.Zero != AdapterBuffer)
                                                    Marshal.FreeCoTaskMem(AdapterBuffer);
                                            }
                                        }

                                        if (null != ADL.ADL_Main_Control_Destroy && NumberOfAdapters <= 0)
                                            // Close ADL if it found no AMD devices
                                            ADL.ADL_Main_Control_Destroy();
                                    }
                                    else
                                    {
                                        // TODO
                                        Helpers.ConsolePrint(TAG, "ADL_Main_Control_Create() returned error code " + ADLRet.ToString());
                                        Helpers.ConsolePrint(TAG, "Check if ADL is properly installed!");
                                        isAdlInit = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Helpers.ConsolePrint(TAG, "AMD ADL exception: " + ex.Message);
                                    isAdlInit = false;
                                }

                                // /////
                                // AMD device creation (in NHM context)
                                if (isAdlInit && isBusID_OK)
                                {
                                    Helpers.ConsolePrint(TAG, "Using AMD device creation DEFAULT Reliable mappings");

                                    if (AMD_Devices.Count == _amdDeviceUUID.Count)
                                    {
                                        Helpers.ConsolePrint(TAG, "AMD OpenCL and ADL AMD query COUNTS GOOD/SAME");
                                    }
                                    else
                                    {
                                        Helpers.ConsolePrint(TAG, "AMD OpenCL and ADL AMD query COUNTS DIFFERENT/BAD");
                                    }

                                    var stringBuilder = new StringBuilder();
                                    stringBuilder.AppendLine("");
                                    stringBuilder.AppendLine("QueryAMD [DEFAULT query] devices: ");

                                    for (int i_id = 0; i_id < AMD_Devices.Count; ++i_id)
                                    {
                                        Available.HasAMD = true;

                                        var busID = AMD_Devices[i_id].AMD_BUS_ID;

                                        if (busID != -1 && _busIdsInfo.ContainsKey(busID))
                                        {
                                            var deviceName = _busIdsInfo[busID].Item1;

                                            var newAmdDev = new AmdGpuDevice(AMD_Devices[i_id], deviceDriverOld[deviceName], _busIdsInfo[busID].Item3, deviceDriverNO_neoscrypt_lyra2re[deviceName])
                                            {
                                                DeviceName = deviceName,
                                                UUID = _busIdsInfo[busID].Item2,
                                                AdapterIndex = _busIdsInfo[busID].Item4
                                            };

                                            var isDisabledGroup = ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionAMD;
                                            var skipOrAdd = isDisabledGroup ? "SKIPED" : "ADDED";
                                            var isDisabledGroupStr = isDisabledGroup ? " (AMD group disabled)" : "";
                                            var etherumCapableStr = newAmdDev.IsEtherumCapable() ? "YES" : "NO";

                                            Available.AllAvaliableDevices
                                                .Add(
                                                new AmdComputeDevice(newAmdDev, ++GPUCount, false));
                                            // just in case
                                            try
                                            {
                                                stringBuilder.AppendLine(string.Format("\t{0} device{1}:", skipOrAdd, isDisabledGroupStr));
                                                stringBuilder.AppendLine(string.Format("\t\tID: {0}", newAmdDev.DeviceID.ToString()));
                                                stringBuilder.AppendLine(string.Format("\t\tNAME: {0}", newAmdDev.DeviceName));
                                                stringBuilder.AppendLine(string.Format("\t\tCODE_NAME: {0}", newAmdDev.Codename));
                                                stringBuilder.AppendLine(string.Format("\t\tUUID: {0}", newAmdDev.UUID));
                                                stringBuilder.AppendLine(string.Format("\t\tMEMORY: {0}", newAmdDev.DeviceGlobalMemory.ToString()));
                                                stringBuilder.AppendLine(string.Format("\t\tETHEREUM: {0}", etherumCapableStr));
                                            }
                                            catch { }
                                        }
                                        else
                                        {
                                            stringBuilder.AppendLine(string.Format("\tDevice not added, Bus No. {0} not found:", busID));
                                        }
                                    }

                                    Helpers.ConsolePrint(TAG, stringBuilder.ToString());
                                }
                                else
                                {
                                    Helpers.ConsolePrint(TAG, "Using AMD device creation FALLBACK UnReliable mappings");
                                    var stringBuilder = new StringBuilder();
                                    stringBuilder.AppendLine("");
                                    stringBuilder.AppendLine("QueryAMD [FALLBACK query] devices: ");

                                    // get video AMD controllers and sort them by RAM
                                    // (find a way to get PCI BUS Numbers from PNPDeviceID)
                                    var AMDVideoControllers = new List<VideoControllerData>();

                                    foreach (var vcd in AvaliableVideoControllers)
                                    {
                                        if (vcd.Name.ToLower().Contains("amd")
                                            || vcd.Name.ToLower().Contains("radeon")
                                            || vcd.Name.ToLower().Contains("firepro"))
                                        {
                                            AMDVideoControllers.Add(vcd);
                                        }
                                    }

                                    // sort by ram not ideal
                                    AMDVideoControllers.Sort((a, b) => (int)(a.AdapterRAM - b.AdapterRAM));
                                    AMD_Devices.Sort((a, b) => (int)(a._CL_DEVICE_GLOBAL_MEM_SIZE - b._CL_DEVICE_GLOBAL_MEM_SIZE));
                                    var minCount = Math.Min(AMDVideoControllers.Count, AMD_Devices.Count);

                                    for (int i = 0; i < minCount; ++i)
                                    {
                                        Available.HasAMD = true;

                                        var deviceName = AMDVideoControllers[i].Name;
                                        if (AMDVideoControllers[i].InfSection == null) AMDVideoControllers[i].InfSection = "";

                                        var newAmdDev = new AmdGpuDevice(AMD_Devices[i], deviceDriverOld[deviceName], AMDVideoControllers[i].InfSection, deviceDriverNO_neoscrypt_lyra2re[deviceName])
                                        {
                                            DeviceName = deviceName,
                                            UUID = "UNUSED"
                                        };

                                        var isDisabledGroup = ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionAMD;
                                        var skipOrAdd = isDisabledGroup ? "SKIPED" : "ADDED";
                                        var isDisabledGroupStr = isDisabledGroup ? " (AMD group disabled)" : "";
                                        var etherumCapableStr = newAmdDev.IsEtherumCapable() ? "YES" : "NO";

                                        Available.AllAvaliableDevices
                                            .Add(
                                            new AmdComputeDevice(newAmdDev, ++GPUCount, true));
                                        // just in case
                                        try
                                        {
                                            stringBuilder.AppendLine(string.Format("\t{0} device{1}:", skipOrAdd, isDisabledGroupStr));
                                            stringBuilder.AppendLine(string.Format("\t\tID: {0}", newAmdDev.DeviceID.ToString()));
                                            stringBuilder.AppendLine(string.Format("\t\tNAME: {0}", newAmdDev.DeviceName));
                                            stringBuilder.AppendLine(string.Format("\t\tCODE_NAME: {0}", newAmdDev.Codename));
                                            stringBuilder.AppendLine(string.Format("\t\tUUID: {0}", newAmdDev.UUID));
                                            stringBuilder.AppendLine(string.Format("\t\tMEMORY: {0}", newAmdDev.DeviceGlobalMemory.ToString()));
                                            stringBuilder.AppendLine(string.Format("\t\tETHEREUM: {0}", etherumCapableStr));
                                        }
                                        catch { }
                                    }

                                    Helpers.ConsolePrint(TAG, stringBuilder.ToString());
                                }
                            }
                        } // end is amdPlatformNumFound
                    } // end is OpenCLSuccess

                    Helpers.ConsolePrint(TAG, "QueryAMD END");
                }
            }

            #endregion Helpers
        }

        public static class SystemSpecs
        {
            public static ulong FreePhysicalMemory;
            public static ulong FreeSpaceInPagingFiles;
            public static ulong FreeVirtualMemory;
            public static uint LargeSystemCache;
            public static uint MaxNumberOfProcesses;
            public static ulong MaxProcessMemorySize;

            public static uint NumberOfLicensedUsers;
            public static uint NumberOfProcesses;
            public static uint NumberOfUsers;
            public static uint OperatingSystemSKU;

            public static ulong SizeStoredInPagingFiles;

            public static uint SuiteMask;

            public static ulong TotalSwapSpaceSize;
            public static ulong TotalVirtualMemorySize;
            public static ulong TotalVisibleMemorySize;

            public static void QueryAndLog()
            {
                var winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

                var searcher = new ManagementObjectSearcher(winQuery);

                foreach (ManagementObject item in searcher.Get())
                {
                    if (item["FreePhysicalMemory"] != null) ulong.TryParse(item["FreePhysicalMemory"].ToString(), out FreePhysicalMemory);
                    if (item["FreeSpaceInPagingFiles"] != null) ulong.TryParse(item["FreeSpaceInPagingFiles"].ToString(), out FreeSpaceInPagingFiles);
                    if (item["FreeVirtualMemory"] != null) ulong.TryParse(item["FreeVirtualMemory"].ToString(), out FreeVirtualMemory);
                    if (item["LargeSystemCache"] != null) uint.TryParse(item["LargeSystemCache"].ToString(), out LargeSystemCache);
                    if (item["MaxNumberOfProcesses"] != null) uint.TryParse(item["MaxNumberOfProcesses"].ToString(), out MaxNumberOfProcesses);
                    if (item["MaxProcessMemorySize"] != null) ulong.TryParse(item["MaxProcessMemorySize"].ToString(), out MaxProcessMemorySize);
                    if (item["NumberOfLicensedUsers"] != null) uint.TryParse(item["NumberOfLicensedUsers"].ToString(), out NumberOfLicensedUsers);
                    if (item["NumberOfProcesses"] != null) uint.TryParse(item["NumberOfProcesses"].ToString(), out NumberOfProcesses);
                    if (item["NumberOfUsers"] != null) uint.TryParse(item["NumberOfUsers"].ToString(), out NumberOfUsers);
                    if (item["OperatingSystemSKU"] != null) uint.TryParse(item["OperatingSystemSKU"].ToString(), out OperatingSystemSKU);
                    if (item["SizeStoredInPagingFiles"] != null) ulong.TryParse(item["SizeStoredInPagingFiles"].ToString(), out SizeStoredInPagingFiles);
                    if (item["SuiteMask"] != null) uint.TryParse(item["SuiteMask"].ToString(), out SuiteMask);
                    if (item["TotalSwapSpaceSize"] != null) ulong.TryParse(item["TotalSwapSpaceSize"].ToString(), out TotalSwapSpaceSize);
                    if (item["TotalVirtualMemorySize"] != null) ulong.TryParse(item["TotalVirtualMemorySize"].ToString(), out TotalVirtualMemorySize);
                    if (item["TotalVisibleMemorySize"] != null) ulong.TryParse(item["TotalVisibleMemorySize"].ToString(), out TotalVisibleMemorySize);
                    // log
                    Helpers.ConsolePrint("SystemSpecs", string.Format("FreePhysicalMemory = {0}", FreePhysicalMemory));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("FreeSpaceInPagingFiles = {0}", FreeSpaceInPagingFiles));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("FreeVirtualMemory = {0}", FreeVirtualMemory));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("LargeSystemCache = {0}", LargeSystemCache));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("MaxNumberOfProcesses = {0}", MaxNumberOfProcesses));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("MaxProcessMemorySize = {0}", MaxProcessMemorySize));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("NumberOfLicensedUsers = {0}", NumberOfLicensedUsers));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("NumberOfProcesses = {0}", NumberOfProcesses));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("NumberOfUsers = {0}", NumberOfUsers));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("OperatingSystemSKU = {0}", OperatingSystemSKU));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("SizeStoredInPagingFiles = {0}", SizeStoredInPagingFiles));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("SuiteMask = {0}", SuiteMask));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("TotalSwapSpaceSize = {0}", TotalSwapSpaceSize));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("TotalVirtualMemorySize = {0}", TotalVirtualMemorySize));
                    Helpers.ConsolePrint("SystemSpecs", string.Format("TotalVisibleMemorySize = {0}", TotalVisibleMemorySize));
                }
            }
        }

        public static class Available
        {
            public static bool HasNVIDIA;
            public static bool HasAMD;
            public static bool HasCPU;
            public static int CPUsCount;

            public static int AvailCPUs => AllAvaliableDevices.Count(d => d.DeviceType == DeviceType.CPU);

            public static int AvailNVGPUs => AllAvaliableDevices.Count(d => d.DeviceType == DeviceType.NVIDIA);

            public static int AvailAMDGPUs => AllAvaliableDevices.Count(d => d.DeviceType == DeviceType.AMD);

            public static int AvailGPUs => AvailAMDGPUs + AvailNVGPUs;

            public static int AmdOpenCLPlatformNum = -1;
            public static bool IsHyperThreadingEnabled;

            public static ulong NVIDIA_RAM_SUM = 0;
            public static ulong AMD_RAM_SUM = 0;

            public static List<ComputeDevice> AllAvaliableDevices = new List<ComputeDevice>();

            // methods
            public static ComputeDevice GetDeviceWithUUID(string uuid)
            {
                foreach (var dev in AllAvaliableDevices)
                    if (uuid == dev.UUID) return dev;

                return null;
            }

            public static List<ComputeDevice> GetSameDevicesTypeAsDeviceWithUUID(string uuid)
            {
                var sameTypes = new List<ComputeDevice>();
                var compareDev = GetDeviceWithUUID(uuid);

                foreach (var dev in AllAvaliableDevices)
                {
                    if (uuid != dev.UUID && compareDev.DeviceType == dev.DeviceType)
                    {
                        sameTypes.Add(GetDeviceWithUUID(dev.UUID));
                    }
                }

                return sameTypes;
            }

            public static ComputeDevice GetCurrentlySelectedComputeDevice(int index, bool unique)
            {
                return AllAvaliableDevices[index];
            }

            public static int GetCountForType(DeviceType type)
            {
                var count = 0;

                foreach (var device in Available.AllAvaliableDevices)
                    if (device.DeviceType == type) ++count;

                return count;
            }
        }

        public static class Group
        {
            public static void DisableCpuGroup()
            {
                foreach (var device in Available.AllAvaliableDevices)
                {
                    if (device.DeviceType == DeviceType.CPU)
                    {
                        device.Enabled = false;
                    }
                }
            }

            public static bool ContainsAMD_GPUs
            {
                get
                {
                    foreach (var device in Available.AllAvaliableDevices)
                    {
                        if (device.DeviceType == DeviceType.AMD)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            public static bool ContainsGPUs
            {
                get
                {
                    foreach (var device in Available.AllAvaliableDevices)
                    {
                        if (device.DeviceType == DeviceType.NVIDIA
                            || device.DeviceType == DeviceType.AMD)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            public static void UncheckedCPU()
            {
                // Auto uncheck CPU if any GPU is found
                if (ContainsGPUs) DisableCpuGroup();
            }
        }
    }
}