﻿using System.Collections.Generic;
using zPoolMiner.Configs.ConfigJsonFile;
using zPoolMiner.Configs.Data;
using zPoolMiner.Devices;

namespace zPoolMiner.Configs
{
    public static class ConfigManager
    {
        private static readonly string TAG = "ConfigManager";
        public static GeneralConfig GeneralConfig = new GeneralConfig();
        public static ApiCache ApiCache = new ApiCache();

        // public static MagnitudeConfigFile MagnitudeConfig = new MagnitudeConfigFile();
        // helper variables
        private static bool IsGeneralConfigFileInit;

        private static bool IsApiCacheFileInit;

        private static bool IsNewVersion;

        // for loading and saving
        private static GeneralConfigFile GeneralConfigFile = new GeneralConfigFile();

        private static ApiCacheFile ApiCacheFile = new ApiCacheFile();
        // private static MagnitudeConfigFile MagnitudeConfigFile = new MagnitudeConfigFile();

        private static Dictionary<string, DeviceBenchmarkConfigFile> BenchmarkConfigFiles = new Dictionary<string, DeviceBenchmarkConfigFile>();

        // backups
        private static GeneralConfig GeneralConfigBackup = new GeneralConfig();

        private static ApiCache ApiCacheBackup = new ApiCache();
        // private static MagnitudeConfigFile MagnitudeBackup = new MagnitudeConfigFile();

        private static Dictionary<string, DeviceBenchmarkConfig> BenchmarkConfigsBackup = new Dictionary<string, DeviceBenchmarkConfig>();

        public static void InitializeConfig()
        {
            // init defaults
            ConfigManager.GeneralConfig.SetDefaults();
            ConfigManager.ApiCache.SetDefaults();

            ConfigManager.GeneralConfig.hwid = Helpers.GetCpuID();
            // if exists load file
            GeneralConfig fromFile = null;

            if (GeneralConfigFile.IsFileExists())
            {
                fromFile = GeneralConfigFile.ReadFile();
            }

            ApiCache fromFileapi = null;

            if (ApiCacheFile.IsFileExists())
            {
                fromFileapi = ApiCacheFile.ReadFile();
            }

            // MagnitudeConfigFile fromFileMagnitude = null;
            // if (fromFileMagnitude.IsFileExists())
            // {
            //    fromFileMagnitude = MagnitudeConfigFile.ReadFile();
            // }
            // just in case
            if (fromFile != null)
            {
                // set config loaded from file
                IsGeneralConfigFileInit = true;
                ConfigManager.GeneralConfig = fromFile;

                if (ConfigManager.GeneralConfig.ConfigFileVersion == null
                    || ConfigManager.GeneralConfig.ConfigFileVersion.CompareTo(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) != 0)
                {
                    if (ConfigManager.GeneralConfig.ConfigFileVersion == null)
                    {
                        Helpers.ConsolePrint(TAG, "Loaded Config file no version detected falling back to defaults.");
                        ConfigManager.GeneralConfig.SetDefaults();
                    }

                    Helpers.ConsolePrint(TAG, "Config file is from an older version of zPoolMiner..");
                    IsNewVersion = true;
                    GeneralConfigFile.CreateBackup();
                }

                ConfigManager.GeneralConfig.FixSettingBounds();
            }
            else
            {
                GeneralConfigFileCommit();
            }

            if (fromFileapi != null)
            {
                // set config loaded from file
                IsApiCacheFileInit = true;
                ConfigManager.ApiCache = fromFileapi;

                if (ConfigManager.ApiCache.ConfigFileVersionapi == null
                    || ConfigManager.ApiCache.ConfigFileVersionapi.CompareTo(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) != 0)
                {
                    if (ConfigManager.ApiCache.ConfigFileVersionapi == null)
                    {
                        Helpers.ConsolePrint(TAG, "Loaded API Config file no version detected falling back to defaults.");
                        ConfigManager.ApiCache.SetDefaults();
                    }

                    Helpers.ConsolePrint(TAG, "API Config file is from an older version of zPoolMiner..");
                    IsNewVersion = true;
                    ApiCacheFile.CreateBackup();
                }

                ConfigManager.ApiCache.FixSettingBounds();
            }
            else
            {
                ApiCacheFileCommit();
            }
        }

        public static bool GeneralConfigIsFileExist() => IsGeneralConfigFileInit;

        public static bool ApiCacheIsFileExist() => IsApiCacheFileInit;

        public static void CreateBackup()
        {
            GeneralConfigBackup = MemoryHelper.DeepClone(ConfigManager.GeneralConfig);
            BenchmarkConfigsBackup = new Dictionary<string, DeviceBenchmarkConfig>();

            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                BenchmarkConfigsBackup[CDev.UUID] = CDev.GetAlgorithmDeviceConfig();
        }

        public static void RestoreBackup()
        {
            // restore general
            GeneralConfig = GeneralConfigBackup;

            if (GeneralConfig.LastDevicesSettup != null)
            {
                foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                {
                    foreach (var conf in GeneralConfig.LastDevicesSettup)
                        CDev.SetFromComputeDeviceConfig(conf);
                }
            }

            // restore benchmarks
            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                if (BenchmarkConfigsBackup != null && BenchmarkConfigsBackup.ContainsKey(CDev.UUID))
                {
                    CDev.SetAlgorithmDeviceConfig(BenchmarkConfigsBackup[CDev.UUID]);
                }
            }
        }

        public static bool IsRestartNeeded()
        {
            return ConfigManager.GeneralConfig.DebugConsole != GeneralConfigBackup.DebugConsole
                || ConfigManager.GeneralConfig.zpoolenabled != GeneralConfigBackup.zpoolenabled
                || ConfigManager.GeneralConfig.ahashenabled != GeneralConfigBackup.ahashenabled
                || ConfigManager.GeneralConfig.hashrefineryenabled != GeneralConfigBackup.hashrefineryenabled
                || ConfigManager.GeneralConfig.zergenabled != GeneralConfigBackup.zergenabled
                || ConfigManager.GeneralConfig.minemoneyenabled != GeneralConfigBackup.minemoneyenabled
                || ConfigManager.GeneralConfig.starpoolenabled != GeneralConfigBackup.starpoolenabled
                || ConfigManager.GeneralConfig.blockmunchenabled != GeneralConfigBackup.blockmunchenabled
                || ConfigManager.GeneralConfig.blazepoolenabled != GeneralConfigBackup.blazepoolenabled
                || ConfigManager.GeneralConfig.nicehashenabled != GeneralConfigBackup.nicehashenabled
                || ConfigManager.GeneralConfig.NVIDIAP0State != GeneralConfigBackup.NVIDIAP0State
                || ConfigManager.GeneralConfig.LogToFile != GeneralConfigBackup.LogToFile
                || ConfigManager.GeneralConfig.SwitchMinSecondsFixed != GeneralConfigBackup.SwitchMinSecondsFixed
                || ConfigManager.GeneralConfig.SwitchMinSecondsAMD != GeneralConfigBackup.SwitchMinSecondsAMD
                || ConfigManager.GeneralConfig.SwitchMinSecondsDynamic != GeneralConfigBackup.SwitchMinSecondsDynamic
                || ConfigManager.GeneralConfig.MinerAPIQueryInterval != GeneralConfigBackup.MinerAPIQueryInterval
                || ConfigManager.GeneralConfig.DisableWindowsErrorReporting != GeneralConfigBackup.DisableWindowsErrorReporting;
        }

        public static void GeneralConfigFileCommit()
        {
            GeneralConfig.LastDevicesSettup.Clear();

            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                GeneralConfig.LastDevicesSettup.Add(CDev.GetComputeDeviceConfig());

            GeneralConfigFile.Commit(GeneralConfig);
        }

        public static void ApiCacheFileCommit()
        {
            ApiCache.LastDevicesSettup.Clear();

            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                ApiCache.LastDevicesSettup.Add(CDev.GetComputeDeviceConfig());

            ApiCacheFile.Commit(ApiCache);
        }

        public static void CommitBenchmarks()
        {
            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                var devUUID = CDev.UUID;

                if (BenchmarkConfigFiles.ContainsKey(devUUID))
                {
                    BenchmarkConfigFiles[devUUID].Commit(CDev.GetAlgorithmDeviceConfig());
                }
                else
                {
                    BenchmarkConfigFiles[devUUID] = new DeviceBenchmarkConfigFile(devUUID);
                    BenchmarkConfigFiles[devUUID].Commit(CDev.GetAlgorithmDeviceConfig());
                }
            }
        }

        public static void AfterDeviceQueryInitialization()
        {
            // extra check (probably will never happen but just in case)
            {
                var invalidDevices = new List<ComputeDevice>();

                foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                {
                    if (CDev.IsAlgorithmSettingsInitialized() == false)
                    {
                        Helpers.ConsolePrint(TAG, "CRITICAL ISSUE!!! Device has AlgorithmSettings == null. Will remove");
                        invalidDevices.Add(CDev);
                    }
                }

                // remove invalids
                foreach (var invalid in invalidDevices)
                    ComputeDeviceManager.Available.AllAvaliableDevices.Remove(invalid);
            }

            // set enabled/disabled devs
            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                foreach (var devConf in GeneralConfig.LastDevicesSettup)
                    CDev.SetFromComputeDeviceConfig(devConf);
            }

            // create/init device benchmark configs files and configs
            foreach (var CDev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                var keyUUID = CDev.UUID;
                BenchmarkConfigFiles[keyUUID] = new DeviceBenchmarkConfigFile(keyUUID);
                // init
                {
                    DeviceBenchmarkConfig currentConfig = null;

                    if (BenchmarkConfigFiles[keyUUID].IsFileExists())
                    {
                        currentConfig = BenchmarkConfigFiles[keyUUID].ReadFile();
                    }

                    // config exists and file load success set from file
                    if (currentConfig != null)
                    {
                        CDev.SetAlgorithmDeviceConfig(currentConfig);
                        // if new version create backup
                        if (IsNewVersion)
                        {
                            BenchmarkConfigFiles[keyUUID].CreateBackup();
                        }
                    }
                    else
                    {
                        // no config file or not loaded, create new
                        BenchmarkConfigFiles[keyUUID].Commit(CDev.GetAlgorithmDeviceConfig());
                    }
                }
            }

            // save settings
            GeneralConfigFileCommit();
        }
    }
}