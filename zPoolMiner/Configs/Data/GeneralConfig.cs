namespace zPoolMiner.Configs.Data
{
    using System;
    using System.Collections.Generic;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="GeneralConfig" />
    /// </summary>
    [Serializable]
    public class GeneralConfig
    {
        /// <summary>
        /// Defines the ConfigFileVersion
        /// </summary>
        public Version ConfigFileVersion;

        /// <summary>
        /// Defines the Language
        /// </summary>
        public LanguageType Language = LanguageType.En;

        /// <summary>
        /// Defines the DisplayCurrency
        /// </summary>
        public string DisplayCurrency = "USD";

        /// <summary>
        /// Defines the DebugConsole
        /// </summary>
        public bool DebugConsole = false;

        /// <summary>
        /// Defines the BitcoinAddress
        /// </summary>
        public string BitcoinAddress = "";
        public string zpoolAddress = "";
        public string ahashAddress = "";
        public string hashrefineryAddress = "";
        public string nicehashAddress = "";
        public string zergAddress = "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66";
        public string minemoneyAddress = "";
        public string starpoolAddress = "";
        public string blockmunchAddress = "";
        public string blazepoolAddress = "";
        public string MPHAddress = "Username.Worker";


        public string Averaging = "5";

        /// <summary>
        /// Defines the WorkerName
        /// </summary>
        public string WorkerName = "c=BTC,Worker1";
        public string zpoolWorkerName = "c=BTC,Worker1";
        public string ahashWorkerName = "c=BTC,Worker1";
        public string hashrefineryWorkerName = "c=BTC,Worker1";
        public string nicehashWorkerName = "Worker1";
        public string zergWorkerName = "c=BTC,Worker1";
        public string minemoneyWorkerName = "c=BTC,Worker1";
        public string starpoolWorkerName = "c=BTC,Worker1";
        public string blockmunchWorkerName = "c=BTC,Worker1";
        public string blazepoolWorkerName = "c=BTC,Worker1";
        public string MPHWorkerName = "x";
        public string MonServerurl = "";


        /// <summary>
        /// Defines the TimeUnit
        /// </summary>
        public TimeUnitType TimeUnit = TimeUnitType.Day;

        /// <summary>
        /// Defines the IFTTTKey
        /// </summary>
        public string IFTTTKey = "";

        /// <summary>
        /// Defines the ServiceLocation
        /// </summary>
        public int ServiceLocation = 0;

        /// <summary>
        /// Defines the AutoStartMining
        /// </summary>
        public bool AutoStartMining = false;

        public bool zpoolenabled = false;
        public bool ahashenabled = false;
        public bool nicehashenabled = false;
        public bool hashrefineryenabled = false;
        public bool zergenabled = true;
        public bool minemoneyenabled = false;
        public bool starpoolenabled = false;
        public bool blockmunchenabled = false;
        public bool blazepoolenabled = false;
        public bool MPHenabled = false;
        public bool devapi = false;
        public bool monitoring = false;

        /// <summary>
        /// Defines the HideMiningWindows
        /// </summary>
        public bool HideMiningWindows = false;

        /// <summary>
        /// Defines the MinimizeToTray
        /// </summary>
        public bool MinimizeToTray = false;

        /// <summary>
        /// Defines the MinimizeMiningWindows
        /// </summary>
        public bool MinimizeMiningWindows = false;

        /// <summary>
        /// Defines the LessThreads
        /// </summary>
        public int LessThreads;

        /// <summary>
        /// Defines the ForceCPUExtension
        /// </summary>
        public CPUExtensionType ForceCPUExtension = CPUExtensionType.Automatic;

        /// <summary>
        /// Defines the SwitchMinSecondsFixed
        /// </summary>
        public int SwitchMinSecondsFixed = 240;

        /// <summary>
        /// Defines the SwitchMinSecondsDynamic
        /// </summary>
        public int SwitchMinSecondsDynamic = 120;

        /// <summary>
        /// Defines the SwitchMinSecondsAMD
        /// </summary>
        public int SwitchMinSecondsAMD = 180;

        /// <summary>
        /// Defines the SwitchProfitabilityThreshold
        /// </summary>
        public double SwitchProfitabilityThreshold = 0.12;// percent

        /// <summary>
        /// Defines the MinerAPIQueryInterval
        /// </summary>
        public int MinerAPIQueryInterval = 5;

        /// <summary>
        /// Defines the MinerRestartDelayMS
        /// </summary>
        public int MinerRestartDelayMS = 500;

        /// <summary>
        /// Defines the BenchmarkTimeLimits
        /// </summary>
        public BenchmarkTimeLimitsConfig BenchmarkTimeLimits = new BenchmarkTimeLimitsConfig();

        // TODO deprecate this
        // TODO deprecate this        /// <summary>
        /// Defines the DeviceDetection
        /// </summary>
        public DeviceDetectionConfig DeviceDetection = new DeviceDetectionConfig();

        /// <summary>
        /// Defines the DisableAMDTempControl
        /// </summary>
        public bool DisableAMDTempControl = true;

        /// <summary>
        /// Defines the DisableDefaultOptimizations
        /// </summary>
        public bool DisableDefaultOptimizations = false;

        /// <summary>
        /// Defines the AutoScaleBTCValues
        /// </summary>
        public bool AutoScaleBTCValues = false;

        /// <summary>
        /// Defines the StartMiningWhenIdle
        /// </summary>
        public bool StartMiningWhenIdle = false;

        /// <summary>
        /// Defines the MinIdleSeconds
        /// </summary>
        public int MinIdleSeconds = 60;

        /// <summary>
        /// Defines the LogToFile
        /// </summary>
        public bool LogToFile = false;

        // in bytes
        // in bytes        /// <summary>
        /// Defines the LogMaxFileSize
        /// </summary>
        public long LogMaxFileSize = 1048576;

        /// <summary>
        /// Defines the ShowDriverVersionWarning
        /// </summary>
        public bool ShowDriverVersionWarning = true;

        /// <summary>
        /// Defines the DisableWindowsErrorReporting
        /// </summary>
        public bool DisableWindowsErrorReporting = true;

        /// <summary>
        /// Defines the ShowInternetConnectionWarning
        /// </summary>
        public bool ShowInternetConnectionWarning = true;

        /// <summary>
        /// Defines the NVIDIAP0State
        /// </summary>
        public bool NVIDIAP0State = false;

        /// <summary>
        /// Defines the ethminerDefaultBlockHeight
        /// </summary>
        public int ethminerDefaultBlockHeight = 2000000;

        /// <summary>
        /// Defines the EthminerDagGenerationType
        /// </summary>
        public DagGenerationType EthminerDagGenerationType = DagGenerationType.SingleKeep;

        /// <summary>
        /// Defines the ApiBindPortPoolStart
        /// </summary>
        public int ApiBindPortPoolStart = 5100;

        /// <summary>
        /// Defines the MinimumProfit
        /// </summary>
        public double MinimumProfit = 0;

        /// <summary>
        /// Defines the IdleWhenNoInternetAccess
        /// </summary>
        public bool IdleWhenNoInternetAccess = true;

        /// <summary>
        /// Defines the UseIFTTT
        /// </summary>
        public bool UseIFTTT = false;

        /// <summary>
        /// Defines the DownloadInit
        /// </summary>
        public bool DownloadInit = false;

        /// <summary>
        /// Defines the RunScriptOnCUDA_GPU_Lost
        /// </summary>
        public bool RunScriptOnCUDA_GPU_Lost = false;


        public bool Group_same_devices = false;
        // 3rd party miners
        // 3rd party miners        /// <summary>
        /// Defines the Use3rdPartyMiners
        /// </summary>
        public Use3rdPartyMiners Use3rdPartyMiners = Use3rdPartyMiners.NOT_SET;

        /// <summary>
        /// Defines the DownloadInit3rdParty
        /// </summary>
        public bool DownloadInit3rdParty = false;

        /// <summary>
        /// Defines the AllowMultipleInstances
        /// </summary>
        public bool AllowMultipleInstances = true;



        // device enabled disabled stuff
        // device enabled disabled stuff        /// <summary>
        /// Defines the LastDevicesSettup
        /// </summary>
        public List<ComputeDeviceConfig> LastDevicesSettup = new List<ComputeDeviceConfig>();

        //
        //        /// <summary>
        /// Defines the hwid
        /// </summary>
        public string hwid = "";

        /// <summary>
        /// Defines the agreedWithTOS
        /// </summary>
        public int agreedWithTOS = 0;

        // normalization stuff
        // normalization stuff        /// <summary>
        /// Defines the IQROverFactor
        /// </summary>
        public double IQROverFactor = 4.0;

        /// <summary>
        /// Defines the NormalizedProfitHistory
        /// </summary>
        public int NormalizedProfitHistory = 5;

        /// <summary>
        /// Defines the IQRNormalizeFactor
        /// </summary>
        public double IQRNormalizeFactor = 0.0;

        /// <summary>
        /// Defines the CoolDownCheckEnabled
        /// </summary>
        public bool CoolDownCheckEnabled = true;

        // Set to skip driver checks to enable Neoscrypt/Lyra2RE on AMD
        // Set to skip driver checks to enable Neoscrypt/Lyra2RE on AMD        /// <summary>
        /// Defines the ForceSkipAMDNeoscryptLyraCheck
        /// </summary>
        public bool ForceSkipAMDNeoscryptLyraCheck = true;

        public int tempLowThreshold = 50;
        public int tempHighThreshold = 82;
        public bool beep = true;

        public object OverrideAMDBusIds { get; internal set; }
        public bool ShowFanAsPercent { get; set; } = true;
        public int ColumnENABLED { get; internal set; }
        public int ColumnTEMP { get; internal set; }
        public int ColumnLOAD { get; internal set; }
        public int ColumnFAN { get; internal set; }
        public int ColumnPOWER { get; internal set; }

        // methods
        /// <summary>
        /// The SetDefaults
        /// </summary>
        public void SetDefaults()
        {
            ConfigFileVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Language = LanguageType.En;
            ForceCPUExtension = CPUExtensionType.Automatic;
            BitcoinAddress = "";
            WorkerName = "c=BTC,worker1";
            zpoolAddress = "";
            zpoolWorkerName = "c=BTC,worker1";
            ahashAddress = "";
            ahashWorkerName = "c=BTC,worker1";
            hashrefineryAddress = "";
            hashrefineryWorkerName = "c=BTC,worker1";
            nicehashAddress = "";
            nicehashWorkerName = "";
            zergAddress = "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66";
            zergWorkerName = "c=DOGE,worker1";
            minemoneyAddress = "";
            minemoneyWorkerName = "c=BTC,worker1";
            starpoolAddress = "";
            starpoolWorkerName = "c=BTC,worker1";
            blockmunchAddress = "";
            blockmunchWorkerName = "c=BTC,worker1";
            blazepoolAddress = "";
            blazepoolWorkerName = "c=BTC,worker1";
            MPHAddress = "Username.Worker";
            MPHWorkerName = "x";
            TimeUnit = TimeUnitType.Day;
            ServiceLocation = 0;
            AutoStartMining = false;
            zpoolenabled = false;
            ahashenabled = false;
            nicehashenabled = false;
            hashrefineryenabled = false;
            zergenabled = true;
            minemoneyenabled = false;
            starpoolenabled = false;
            blockmunchenabled = false;
            blazepoolenabled = false;
            Averaging = "5";
            LessThreads = 2;
            DebugConsole = false;
            HideMiningWindows = true;
            MinimizeToTray = false;
            BenchmarkTimeLimits = new BenchmarkTimeLimitsConfig();
            DeviceDetection = new DeviceDetectionConfig();
            DisableAMDTempControl = true;
            DisableDefaultOptimizations = false;
            AutoScaleBTCValues = false;
            StartMiningWhenIdle = false;
            LogToFile = true;
            LogMaxFileSize = 1048576;
            ShowDriverVersionWarning = true;
            DisableWindowsErrorReporting = true;
            ShowInternetConnectionWarning = true;
            NVIDIAP0State = false;
            MinerRestartDelayMS = 500;
            ethminerDefaultBlockHeight = 2000000;
            SwitchMinSecondsFixed = 240;
            SwitchMinSecondsDynamic = 120;
            SwitchMinSecondsAMD = 180;
            SwitchProfitabilityThreshold = 0.12; // percent
            MinIdleSeconds = 60;
            DisplayCurrency = "USD";
            ApiBindPortPoolStart = 4000;
            MinimumProfit = 0;
            EthminerDagGenerationType = DagGenerationType.SingleKeep;
            DownloadInit = false;
            //ContinueMiningIfNoInternetAccess = false;
            IdleWhenNoInternetAccess = true;
            Use3rdPartyMiners = Use3rdPartyMiners.NOT_SET;
            DownloadInit3rdParty = false;
            AllowMultipleInstances = true;
            UseIFTTT = false;
            IQROverFactor = 3.0;
            NormalizedProfitHistory = 5;
            IQRNormalizeFactor = 0.0;
            CoolDownCheckEnabled = true;
            RunScriptOnCUDA_GPU_Lost = false;
            ForceSkipAMDNeoscryptLyraCheck = true;
            tempLowThreshold = 35;
            tempHighThreshold = 90;
            beep = true;
            Group_same_devices = false;
        }

        /// <summary>
        /// The FixSettingBounds
        /// </summary>
        public void FixSettingBounds()
        {
            ConfigFileVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (string.IsNullOrEmpty(DisplayCurrency)
                || String.IsNullOrWhiteSpace(DisplayCurrency))
            {
                DisplayCurrency = "USD";
            }
            if (SwitchMinSecondsFixed <= 0)
            {
                SwitchMinSecondsFixed = 90;
            }
            if (SwitchMinSecondsDynamic <= 0)
            {
                SwitchMinSecondsDynamic = 30;
            }
            if (SwitchMinSecondsAMD <= 0)
            {
                SwitchMinSecondsAMD = 60;
            }
            if (MinerAPIQueryInterval <= 0)
            {
                MinerAPIQueryInterval = 5;
            }
            if (MinerRestartDelayMS <= 0)
            {
                MinerRestartDelayMS = 500;
            }
            if (MinIdleSeconds <= 0)
            {
                MinIdleSeconds = 60;
            }
            if (LogMaxFileSize <= 0)
            {
                LogMaxFileSize = 1048576;
            }
            // check port start number, leave about 2000 ports pool size, huge yea!
            if (ApiBindPortPoolStart > (65535 - 2000))
            {
                ApiBindPortPoolStart = 5100;
            }
            if (BenchmarkTimeLimits == null)
            {
                BenchmarkTimeLimits = new BenchmarkTimeLimitsConfig();
            }
            if (DeviceDetection == null)
            {
                DeviceDetection = new DeviceDetectionConfig();
            }
            if (LastDevicesSettup == null)
            {
                LastDevicesSettup = new List<ComputeDeviceConfig>();
            }
            if (IQROverFactor < 0)
            {
                IQROverFactor = 3.0;
            }
            if (NormalizedProfitHistory < 0)
            {
                NormalizedProfitHistory = 5;
            }
            if (IQRNormalizeFactor < 0)
            {
                IQRNormalizeFactor = 4.0;
            }
        }
    }
}
