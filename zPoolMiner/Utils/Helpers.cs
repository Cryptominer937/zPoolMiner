﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Management;
using zPoolMiner.Configs;
using zPoolMiner.Enums;
using zPoolMiner.PInvoke;

namespace zPoolMiner
{
    internal class Helpers : PInvokeHelpers
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("HELPERS");
        private static bool is64BitProcess = (IntPtr.Size == 8);
        public static bool Is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    if (!IsWow64Process(p.Handle, out bool retVal))
                    {
                        return false;
                    }

                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        public static void ConsolePrint(string grp, string text)
        {
            // Console.WriteLine does nothing on x64 while debugging with VS, so use Debug. Console.WriteLine works when run from .exe
#if DEBUG
            // Debug.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + grp + "] " + text);
#endif
#if !DEBUG
            //Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + grp + "] " + text);
#endif

            if (ConfigManager.GeneralConfig.LogToFile && Logger.IsInit)
                Logger.log.Debug("[" + grp + "] " + text);
        }

        public static void ConsolePrint(string grp, string text, params object[] arg)
        {
            ConsolePrint(grp, string.Format(text, arg));
        }

        public static void ConsolePrint(string grp, string text, object arg0)
        {
            ConsolePrint(grp, string.Format(text, arg0));
        }

        public static void ConsolePrint(string grp, string text, object arg0, object arg1)
        {
            ConsolePrint(grp, string.Format(text, arg0, arg1));
        }

        public static void ConsolePrint(string grp, string text, object arg0, object arg1, object arg2)
        {
            ConsolePrint(grp, string.Format(text, arg0, arg1, arg2));
        }

        public static uint GetIdleTime()
        {
            var lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        public static void DisableWindowsErrorReporting(bool en)
        {
            // bool failed = false;

            log.Error("Trying to enable/disable Windows error reporting");

            // CurrentUser
            try
            {
                using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\Windows Error Reporting"))
                {
                    if (rk != null)
                    {
                        var o = rk.GetValue("DontShowUI");

                        if (o != null)
                        {
                            var val = (int)o;
                            log.Info("Current DontShowUI value: " + val);

                            if (val == 0 && en)
                            {
                                log.Debug("Setting register value to 1.");
                                rk.SetValue("DontShowUI", 1);
                            }
                            else if (val == 1 && !en)
                            {
                                log.Debug("Setting register value to 0.");
                                rk.SetValue("DontShowUI", 0);
                            }
                        }
                        else
                        {
                            log.Debug("Registry key not found .. creating one..");
                            rk.CreateSubKey("DontShowUI", RegistryKeyPermissionCheck.Default);
                            log.Debug("Setting register value to 1..");
                            rk.SetValue("DontShowUI", en ? 1 : 0);
                        }
                    }
                    else
                        log.Warn("Unable to open SubKey.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Unable to access registry. Error: " + ex.Message);
            }
        }

        public static string FormatSpeedOutput(double speed, string separator = " ")
        {
            var ret = "";

            if (speed < 1000)
                ret = (speed).ToString("F3", CultureInfo.InvariantCulture) + separator;
            else if (speed < 100000)
                ret = (speed * 0.001).ToString("F3", CultureInfo.InvariantCulture) + separator + "k";
            else if (speed < 100000000)
                ret = (speed * 0.000001).ToString("F3", CultureInfo.InvariantCulture) + separator + "M";
            else
                ret = (speed * 0.000000001).ToString("F3", CultureInfo.InvariantCulture) + separator + "G";

            return ret;
        }

        public static string FormatDualSpeedOutput(AlgorithmType algorithmID, double primarySpeed, double secondarySpeed = 0)
        {
            string ret;

            if (secondarySpeed > 0)
            {
                ret = FormatSpeedOutput(primarySpeed, "") + "/" + FormatSpeedOutput(secondarySpeed, "") + " ";
            }
            else
            {
                ret = FormatSpeedOutput(primarySpeed);
            }

            if (algorithmID == AlgorithmType.equihash)
                return ret + "Sols/s ";
            else
                return ret + "H/s ";
        }

        public static string GetMotherboardID()
        {
            var mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            var moc = mos.Get();
            var serial = "";

            foreach (ManagementObject mo in moc)
                serial = (string)mo["SerialNumber"];

            return serial;
        }

        // TODO could have multiple cpus
        public static string GetCpuID()
        {
            var id = "N/A";

            try
            {
                ManagementObjectCollection mbsList = null;
                var mbs = new ManagementObjectSearcher("Select * From Win32_processor");
                mbsList = mbs.Get();

                foreach (ManagementObject mo in mbsList)
                    id = mo["ProcessorID"].ToString();
            }
            catch { }

            return id;
        }

        public static bool WebRequestTestGoogle()
        {
            var url = "http://www.google.com";

            try
            {
                var myRequest = System.Net.WebRequest.Create(url);
                myRequest.Timeout = Globals.FirstNetworkCheckTimeoutTimeMS;
                var myResponse = myRequest.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return false;
            }

            return true;
        }

        // Checking the version using >= will enable forward compatibility,
        // however you should always compile your code on newer versions of
        // the framework to ensure your app works the same.
        private static bool Is45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393295)
            {
                // return "4.6 or later";
                return true;
            }

            if ((releaseKey >= 379893))
            {
                // return "4.5.2 or later";
                return true;
            }

            if ((releaseKey >= 378675))
            {
                // return "4.5.1 or later";
                return true;
            }

            if ((releaseKey >= 378389))
            {
                // return "4.5 or later";
                return true;
            }
            // This line should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            // return "No 4.5 or later version detected";
            return false;
        }

        public static bool Is45NetOrHigher()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    return Is45DotVersion((int)ndpKey.GetValue("Release"));
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsConnectedToInternet()
        {
            var returnValue = false;

            try
            {
                returnValue = InternetGetConnectedState(out int Desc, 0);
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }

        // parsing helpers
        public static int ParseInt(string text)
        {
            if (int.TryParse(text, out int tmpVal))
            {
                return tmpVal;
            }

            return 0;
        }

        public static long ParseLong(string text)
        {
            if (long.TryParse(text, out long tmpVal))
            {
                return tmpVal;
            }

            return 0;
        }

        public static double ParseDouble(string text)
        {
            try
            {
                var parseText = text.Replace(',', '.');
                return double.Parse(parseText, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        // IsWMI enabled
        public static bool IsWMIEnabled()
        {
            try
            {
                var moc = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get();
                log.Debug("WMI service seems to be running, ManagementObjectSearcher returned success.");
                return true;
            }
            catch
            {
                log.Debug("ManagementObjectSearcher not working need WMI service to be running");
            }

            return false;
        }

        public static void InstallVcRedist()
        {
            var CudaDevicesDetection = new Process();
            CudaDevicesDetection.StartInfo.FileName = @"bin\vc_redist.x64.exe";
            CudaDevicesDetection.StartInfo.Arguments = "/q /norestart";
            CudaDevicesDetection.StartInfo.UseShellExecute = false;
            CudaDevicesDetection.StartInfo.RedirectStandardError = false;
            CudaDevicesDetection.StartInfo.RedirectStandardOutput = false;
            CudaDevicesDetection.StartInfo.CreateNoWindow = false;

            // const int waitTime = 45 * 1000; // 45seconds
            // CudaDevicesDetection.WaitForExit(waitTime);
            CudaDevicesDetection.Start();
        }

        public static void SetDefaultEnvironmentVariables()
        {
            log.Debug("Setting environment variables");

            var envNameValues = new Dictionary<string, string>() {
                { "GPU_MAX_ALLOC_PERCENT",      "100" },
                { "GPU_USE_SYNC_OBJECTS",       "1" },
                { "GPU_SINGLE_ALLOC_PERCENT",   "100" },
                { "GPU_MAX_HEAP_SIZE",          "100" },
                { "GPU_FORCE_64BIT_PTR",        "0" }
            };

            foreach (var kvp in envNameValues)
            {
                var envName = kvp.Key;
                var envValue = kvp.Value;
                // Check if all the variables is set
                if (Environment.GetEnvironmentVariable(envName) == null)
                {
                    try { Environment.SetEnvironmentVariable(envName, envValue); }
                    catch (Exception e) { log.Error(e.ToString()); }
                }

                // Check to make sure all the values are set correctly
                if (!Environment.GetEnvironmentVariable(envName).Equals(envValue))
                {
                    try { Environment.SetEnvironmentVariable(envName, envValue); }
                    catch (Exception e) { log.Error(e.ToString()); }
                }
            }
        }

        public static void SetNvidiaP0State()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "nvidiasetp0state.exe",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                var p = Process.Start(psi);
                p.WaitForExit();

                if (p.ExitCode != 0)
                    log.Debug("nvidiasetp0state returned error code: " + p.ExitCode.ToString());
                else
                    log.Debug("nvidiasetp0state all OK");
            }
            catch (Exception ex)
            {
                log.Error("nvidiasetp0state error: " + ex.Message);
            }
        }
    }
}