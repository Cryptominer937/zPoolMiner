﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace zPoolMiner
{
    internal class CPUID
    {
        [DllImport("cpuid.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr _GetCPUName();

        [DllImport("cpuid.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr _GetCPUVendor();

        [DllImport("cpuid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SupportsAVX2();

        [DllImport("cpuid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SupportsAES();

        [DllImport("cpuid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetPhysicalProcessorCount();

        public static string GetCPUName()
        {
            var a = _GetCPUName();
            return Marshal.PtrToStringAnsi(a);
        }

        public static string GetCPUVendor()
        {
            var a = _GetCPUVendor();
            return Marshal.PtrToStringAnsi(a);
        }

        public static int GetVirtualCoresCount()
        {
            var coreCount = 0;

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                coreCount += int.Parse(item["NumberOfLogicalProcessors"].ToString());

            return coreCount;
        }

        public static int GetNumberOfCores()
        {
            var coreCount = 0;

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                coreCount += int.Parse(item["NumberOfCores"].ToString());

            return coreCount;
        }

        public static bool IsHypeThreadingEnabled() => GetVirtualCoresCount() > GetNumberOfCores();

        public static ulong CreateAffinityMask(int index, int percpu)
        {
            ulong mask = 0;
            ulong one = 0x0000000000000001;

            for (int i = index * percpu; i < (index + 1) * percpu; i++)
                mask = mask | (one << i);

            return mask;
        }

        public static void AdjustAffinity(int pid, ulong mask)
        {
            var ProcessHandle = new Process();
            ProcessHandle.StartInfo.FileName = "setcpuaff.exe";
            ProcessHandle.StartInfo.Arguments = pid.ToString() + " " + mask.ToString();
            ProcessHandle.StartInfo.CreateNoWindow = true;
            ProcessHandle.StartInfo.UseShellExecute = false;
            ProcessHandle.Start();
        }
    }
}