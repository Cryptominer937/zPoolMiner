namespace zPoolMiner
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Management;
    using System.Windows.Forms;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Forms;
    using zPoolMiner.Forms.Components;
    using zPoolMiner.Interfaces;
    using zPoolMiner.Miners;
    using zPoolMiner.Utils;
    using SystemTimer = System.Timers.Timer;
    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// Defines the <see cref="Form_Main" />
    /// </summary>
    public partial class Form_Main : Form, Form_Loading.IAfterInitializationCaller, IMainFormRatesComunication
    {
        /// <summary>
        /// Defines the VisitURLNew
        /// </summary>
        private string VisitURLNew = Links.VisitURLNew;

        /// <summary>
        /// Defines the MinerStatsCheck
        /// </summary>
        private Timer MinerStatsCheck;

        /// <summary>
        /// Defines the DeviceStatsCheck
        /// </summary>
        private Timer DeviceStatsCheck;

        /// <summary>
        /// Defines the SMAMinerCheck
        /// </summary>
        private Timer SMAMinerCheck;

        /// <summary>
        /// Defines the BitcoinExchangeCheck
        /// </summary>
        private Timer BitcoinExchangeCheck;

        /// <summary>
        /// Defines the StartupTimer
        /// </summary>
        private Timer StartupTimer;

        /// <summary>
        /// Defines the IdleCheck
        /// </summary>
        private Timer IdleCheck;

        /// <summary>
        /// Defines the ComputeDevicesCheckTimer
        /// </summary>
        private SystemTimer ComputeDevicesCheckTimer;

        public static bool needRestart;

        /// <summary>
        /// Defines the ShowWarningNiceHashData
        /// </summary>
        private bool ShowWarningNiceHashData;

        /// <summary>
        /// Defines the DemoMode
        /// </summary>
        private bool DemoMode;

        /// <summary>
        /// Defines the R
        /// </summary>
        private Random R;

        /// <summary>
        /// Defines the LoadingScreen
        /// </summary>
        private Form_Loading LoadingScreen;

        /// <summary>
        /// Defines the BenchmarkForm
        /// </summary>
        private Form_Benchmark BenchmarkForm;

        /// <summary>
        /// Defines the flowLayoutPanelVisibleCount
        /// </summary>
        private int flowLayoutPanelVisibleCount;

        /// <summary>
        /// Defines the flowLayoutPanelRatesIndex
        /// </summary>
        private int flowLayoutPanelRatesIndex;

        /// <summary>
        /// Defines the _betaAlphaPostfixString
        /// </summary>
        private const string _betaAlphaPostfixString = " Beta";

        /// <summary>
        /// Defines the _isDeviceDetectionInitialized
        /// </summary>
        private bool _isDeviceDetectionInitialized;

        /// <summary>
        /// Defines the IsManuallyStarted
        /// </summary>
        private bool IsManuallyStarted;

        /// <summary>
        /// Defines the IsNotProfitable
        /// </summary>
        private bool IsNotProfitable;

        /// <summary>
        /// Defines the isSMAUpdated
        /// </summary>
        private bool isSMAUpdated;

        /// <summary>
        /// Defines the factorTimeUnit
        /// </summary>
        private double factorTimeUnit = 1.0;

        /// <summary>
        /// Defines the MainFormHeight
        /// </summary>
        private int MainFormHeight;

        /// <summary>
        /// Defines the EmtpyGroupPanelHeight
        /// </summary>
        private int EmtpyGroupPanelHeight;

        private string updateText = "";
        internal static Color _backColor;
        internal static Color _foreColor;
        internal static Color _textColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form_Main"/> class.
        /// </summary>
        public Form_Main()
        {
            InitializeComponent();
            Icon = zPoolMiner.Properties.Resources.logo;

            InitLocalization();

            ComputeDeviceManager.SystemSpecs.QueryAndLog();

            // Log the computer's amount of Total RAM and Page File Size
            var moc = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get();

            foreach (ManagementObject mo in moc)
            {
                var TotalRam = long.Parse(mo["TotalVisibleMemorySize"].ToString()) / 1024;
                var PageFileSize = (long.Parse(mo["TotalVirtualMemorySize"].ToString()) / 1024) - TotalRam;
                Helpers.ConsolePrint("NICEHASH", "Total RAM: " + TotalRam + "MB");
                Helpers.ConsolePrint("NICEHASH", "Page File Size: " + PageFileSize + "MB");
            }

            R = new Random((int)DateTime.Now.Ticks);

            Text += " v" + Application.ProductVersion + _betaAlphaPostfixString;

            label_NotProfitable.Visible = false;

            InitMainConfigGUIData();

            // for resizing
            InitFlowPanelStart();

            if (groupBox1.Size.Height > 0 && Size.Height > 0)
            {
                EmtpyGroupPanelHeight = groupBox1.Size.Height;
                MainFormHeight = Size.Height - EmtpyGroupPanelHeight;
            }
            else
            {
                EmtpyGroupPanelHeight = 59;
                MainFormHeight = 330 - EmtpyGroupPanelHeight;
            }

            ClearRatesALL();
        }

        public DevicesListViewEnableControl getDevicesListControl() => devicesListViewEnableControl1;

        /// <summary>
        /// The InitLocalization
        /// </summary>
        private void InitLocalization()
        {
            MessageBoxManager.Unregister();
            MessageBoxManager.Yes = International.GetText("Global_Yes");
            MessageBoxManager.No = International.GetText("Global_No");
            MessageBoxManager.OK = International.GetText("Global_OK");
            MessageBoxManager.Cancel = International.GetText("Global_Cancel");
            MessageBoxManager.Retry = International.GetText("Global_Retry");
            MessageBoxManager.Register();

            labelServiceLocation.Text = International.GetText("Service_Location") + ":";

            {
                var i = 0;

                foreach (string loc in Globals.MiningLocation)
                    comboBoxLocation.Items[i++] = International.GetText("LocationName_" + loc);
            }

            labelBitcoinAddress.Text = International.GetText("BitcoinAddress") + ":";
            labelWorkerName.Text = International.GetText("WorkerName") + ":";

            linkLabelChooseBTCWallet.Text = International.GetText("Form_Main_choose_bitcoin_wallet");

            toolStripStatusLabelGlobalRateText.Text = International.GetText("Form_Main_global_rate") + ":";
            toolStripStatusLabelBTCDayText.Text = "BTC/" + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
            toolStripStatusLabelBalanceText.Text = (ExchangeRateAPI.ActiveDisplayCurrency + "/") + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());

            devicesListViewEnableControl1.InitLocale();

            buttonBenchmark.Text = "";// International.GetText("Form_Main_benchmark");
            buttonSettings.Text = ""; //International.GetText("Form_Main_settings");
            buttonStartMining.Text = "";// International.GetText("Form_Main_start");
            buttonStopMining.Text = ""; //International.GetText("Form_Main_stop");
            buttonHelp.Text = ""; //International.GetText("Form_Main_help");

            label_NotProfitable.Text = International.GetText("Form_Main_MINING_NOT_PROFITABLE");
            groupBox1.Text = string.Format(International.GetText("Form_Main_Group_Device_Rates"), International.GetText("Form_Main_SMA_Update_NEVER"));
        }

        /// <summary>
        /// The InitMainConfigGUIData
        /// </summary>
        private void InitMainConfigGUIData()
        {
            if (ConfigManager.GeneralConfig.ServiceLocation >= 0 && ConfigManager.GeneralConfig.ServiceLocation < Globals.MiningLocation.Length)
                comboBoxLocation.SelectedIndex = ConfigManager.GeneralConfig.ServiceLocation;
            else
                comboBoxLocation.SelectedIndex = 0;

            textBoxBTCAddress.Text = ConfigManager.GeneralConfig.BitcoinAddress;
            textBoxWorkerName.Text = ConfigManager.GeneralConfig.WorkerName;

            tempLower.Value = ConfigManager.GeneralConfig.tempLowThreshold;
            tempUpper.Value = ConfigManager.GeneralConfig.tempHighThreshold;
            beepToggle.Checked = ConfigManager.GeneralConfig.beep;

            ShowWarningNiceHashData = true;
            DemoMode = false;

            // init active display currency after config load
            ExchangeRateAPI.ActiveDisplayCurrency = ConfigManager.GeneralConfig.DisplayCurrency;

            // init factor for Time Unit
            switch (ConfigManager.GeneralConfig.TimeUnit)
            {
                case TimeUnitType.Hour:
                    factorTimeUnit = 1.0 / 24.0;
                    break;

                case TimeUnitType.Day:
                    factorTimeUnit = 1;
                    break;

                case TimeUnitType.Week:
                    factorTimeUnit = 7;
                    break;

                case TimeUnitType.Month:
                    factorTimeUnit = 30;
                    break;

                case TimeUnitType.Year:
                    factorTimeUnit = 365;
                    break;
            }

            toolStripStatusLabelBalanceDollarValue.Text = "(" + ExchangeRateAPI.ActiveDisplayCurrency + ")";
            toolStripStatusLabelBalanceText.Text = (ExchangeRateAPI.ActiveDisplayCurrency + "/") + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
            BalanceCallback(null, null); // update currency changes

            if (_isDeviceDetectionInitialized)
            {
                devicesListViewEnableControl1.ResetComputeDevices(ComputeDeviceManager.Available.AllAvaliableDevices);
            }
        }

        /// <summary>
        /// The AfterLoadComplete
        /// </summary>
        public void AfterLoadComplete()
        {
            LoadingScreen = null;
            Enabled = true;

            IdleCheck = new Timer();
            IdleCheck.Tick += IdleCheck_Tick;
            IdleCheck.Interval = 500;
            IdleCheck.Start();
        }

        /// <summary>
        /// The IdleCheck_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void IdleCheck_Tick(object sender, EventArgs e)
        {
            if (!ConfigManager.GeneralConfig.StartMiningWhenIdle || IsManuallyStarted) return;

            var MSIdle = Helpers.GetIdleTime();

            if (MinerStatsCheck.Enabled)
            {
                if (MSIdle < (ConfigManager.GeneralConfig.MinIdleSeconds * 1000))
                {
                    StopMining();
                    Helpers.ConsolePrint("NICEHASH", "Resumed from idling");
                }
            }
            else
            {
                if (BenchmarkForm == null && (MSIdle > (ConfigManager.GeneralConfig.MinIdleSeconds * 1000)))
                {
                    Helpers.ConsolePrint("NICEHASH", "Entering idling state");

                    if (StartMining(false) != StartMiningReturnType.StartMining)
                    {
                        StopMining();
                    }
                }
            }
        }

        // This is a single shot _benchmarkTimer
        /// <summary>
        /// The StartupTimer_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void StartupTimer_Tick(object sender, EventArgs e)
        {
            StartupTimer.Stop();
            StartupTimer = null;

            // Internals Init
            // TODO add loading step
            MinersSettingsManager.Init();

            if (!Helpers.Is45NetOrHigher())
            {
                MessageBox.Show(International.GetText("NET45_Not_Installed_msg"),
                                International.GetText("Warning_with_Exclamation"),
                                MessageBoxButtons.OK);

                Close();
                return;
            }

            if (!Helpers.Is64BitOperatingSystem)
            {
                MessageBox.Show(International.GetText("Form_Main_x64_Support_Only"),
                                International.GetText("Warning_with_Exclamation"),
                                MessageBoxButtons.OK);

                Close();
                return;
            }
            // 3rdparty miners check scope #1
            {
                // check if setting set
                if (ConfigManager.GeneralConfig.Use3rdPartyMiners == Use3rdPartyMiners.NOT_SET)
                {
                    // Show TOS
                    // Form tos = new Form_3rdParty_TOS();
                    // tos.ShowDialog(this);
                }
            }

            // Query Avaliable ComputeDevices
            ComputeDeviceManager.Query.QueryDevices(LoadingScreen);
            _isDeviceDetectionInitialized = true;

            // ///////////////////////////////////////////
            // ///// from here on we have our devices and Miners initialized
            ConfigManager.AfterDeviceQueryInitialization();
            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_SaveConfig"));

            // All devices settup should be initialized in AllDevices
            devicesListViewEnableControl1.ResetComputeDevices(ComputeDeviceManager.Available.AllAvaliableDevices);
            // set properties after
            devicesListViewEnableControl1.SaveToGeneralConfig = true;

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_CheckLatestVersion"));

            MinerStatsCheck = new Timer();
            MinerStatsCheck.Tick += MinerStatsCheck_Tick;
            MinerStatsCheck.Interval = ConfigManager.GeneralConfig.MinerAPIQueryInterval * 1000;

            devicesListViewEnableControl1.ResetComputeDevices(ComputeDeviceManager.Available.AllAvaliableDevices);
            devicesListViewEnableControl1.SaveToGeneralConfig = false;
            devicesListViewEnableControl1.IsMining = true;
            DeviceStatsCheck = new Timer();
            DeviceStatsCheck.Tick += DeviceStatsCheck_Tick;
            DeviceStatsCheck.Interval = 1000;
            DeviceStatsCheck.Start();

            SMAMinerCheck = new Timer();
            SMAMinerCheck.Tick += SMAMinerCheck_Tick;
            SMAMinerCheck.Interval = ConfigManager.GeneralConfig.SwitchMinSecondsFixed * 1000 + R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 1000);

            if (ComputeDeviceManager.Group.ContainsAMD_GPUs)
            {
                SMAMinerCheck.Interval = (ConfigManager.GeneralConfig.SwitchMinSecondsAMD + ConfigManager.GeneralConfig.SwitchMinSecondsFixed) * 1000 + R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 1000);
            }

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_GetNiceHashSMA"));
            // Init ws connection
            CryptoStats.OnBalanceUpdate += BalanceCallback;
            CryptoStats.OnSMAUpdate += SMACallback;
            CryptoStats.OnVersionUpdate += VersionUpdateCallback;
            CryptoStats.OnConnectionLost += ConnectionLostCallback;
            CryptoStats.OnConnectionEstablished += ConnectionEstablishedCallback;
            CryptoStats.OnVersionBurn += VersionBurnCallback;
            CryptoStats.StartConnection(Links.NHM_Socket_Address);

            // increase timeout
            if (Globals.IsFirstNetworkCheckTimeout)
            {
                while (!Helpers.WebRequestTestGoogle() && Globals.FirstNetworkCheckTimeoutTries > 0)
                    --Globals.FirstNetworkCheckTimeoutTries;
            }

            var ghv = CryptoStats.GetVersion("");
            // Helpers.ConsolePrint("GITHUB", ghv);
            if (ghv != null)
            {
                CryptoStats.SetVersion(ghv);
            }

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_GetBTCRate"));

            BitcoinExchangeCheck = new Timer();
            BitcoinExchangeCheck.Tick += BitcoinExchangeCheck_Tick;
            BitcoinExchangeCheck.Interval = 1000 * 3601; // every 1 hour and 1 second
            BitcoinExchangeCheck.Start();
            BitcoinExchangeCheck_Tick(null, null);

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_SetEnvironmentVariable"));
            Helpers.SetDefaultEnvironmentVariables();

            LoadingScreen.IncreaseLoadCounterAndMessage(International.GetText("Form_Main_loadtext_SetWindowsErrorReporting"));

            Helpers.DisableWindowsErrorReporting(ConfigManager.GeneralConfig.DisableWindowsErrorReporting);

            LoadingScreen.IncreaseLoadCounter();

            if (ConfigManager.GeneralConfig.NVIDIAP0State)
            {
                LoadingScreen.SetInfoMsg(International.GetText("Form_Main_loadtext_NVIDIAP0State"));
                Helpers.SetNvidiaP0State();
            }

            LoadingScreen.FinishLoad();
            // Use last saved rates if exist
            if (Globals.CryptoMiner937Data == null && ConfigManager.ApiCache.CryptoMiner937Data != null)
            {
                Globals.CryptoMiner937Data = ConfigManager.ApiCache.CryptoMiner937Data;
                groupBox1.Text = string.Format(International.GetText("Form_Main_Group_Device_Rates"), ConfigManager.ApiCache.CryptoMiner937DataTimeStamp);
            }

            var runVCRed = !MinersExistanceChecker.IsMinersBinsInit() && !ConfigManager.GeneralConfig.DownloadInit;
            // standard miners check scope
            {
                // check if download needed
                if (!MinersExistanceChecker.IsMinersBinsInit() && !ConfigManager.GeneralConfig.DownloadInit)
                {
                    var downloadUnzipForm = new Form_Loading(new MinersDownloader(MinersDownloadManager.StandardDlSetup));
                    SetChildFormCenter(downloadUnzipForm);
                    downloadUnzipForm.ShowDialog();
                }

                // check if files are mising
                if (!MinersExistanceChecker.IsMinersBinsInit())
                {
                    var result = MessageBox.Show(International.GetText("Form_Main_bins_folder_files_missing"),
                        International.GetText("Warning_with_Exclamation"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        ConfigManager.GeneralConfig.DownloadInit = false;
                        ConfigManager.GeneralConfigFileCommit();
                        var PHandle = new Process();
                        PHandle.StartInfo.FileName = Application.ExecutablePath;
                        PHandle.Start();
                        Close();
                        return;
                    }
                }
                else if (!ConfigManager.GeneralConfig.DownloadInit)
                {
                    // all good
                    ConfigManager.GeneralConfig.DownloadInit = true;
                    ConfigManager.GeneralConfigFileCommit();
                }
            }
            // 3rdparty miners check scope #2
            {
                // check if download needed
                if (ConfigManager.GeneralConfig.Use3rdPartyMiners == Use3rdPartyMiners.YES)
                {
                    if (!MinersExistanceChecker.IsMiners3rdPartyBinsInit() && !ConfigManager.GeneralConfig.DownloadInit3rdParty)
                    {
                        var download3rdPartyUnzipForm = new Form_Loading(new MinersDownloader(MinersDownloadManager.ThirdPartyDlSetup));
                        SetChildFormCenter(download3rdPartyUnzipForm);
                        download3rdPartyUnzipForm.ShowDialog();
                    }

                    // check if files are mising
                    if (!MinersExistanceChecker.IsMiners3rdPartyBinsInit())
                    {
                        var result = MessageBox.Show(International.GetText("Form_Main_bins_folder_files_missing"),
                            International.GetText("Warning_with_Exclamation"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            ConfigManager.GeneralConfig.DownloadInit3rdParty = false;
                            ConfigManager.GeneralConfigFileCommit();
                            var PHandle = new Process();
                            PHandle.StartInfo.FileName = Application.ExecutablePath;
                            PHandle.Start();
                            Close();
                            return;
                        }
                    }
                    else if (!ConfigManager.GeneralConfig.DownloadInit3rdParty)
                    {
                        // all good
                        ConfigManager.GeneralConfig.DownloadInit3rdParty = true;
                        ConfigManager.GeneralConfigFileCommit();
                    }
                }
            }

            if (runVCRed) Helpers.InstallVcRedist();

            if (ConfigManager.GeneralConfig.AutoStartMining)
            {
                // well this is started manually as we want it to start at runtime
                IsManuallyStarted = true;

                if (StartMining(true) != StartMiningReturnType.StartMining)
                {
                    IsManuallyStarted = false;
                    StopMining();
                }
            }
        }

        private void DeviceStatsCheck_Tick(object sender, EventArgs e)
        {
            devicesListViewEnableControl1.ResetComputeDevices(ComputeDeviceManager.Available.AllAvaliableDevices);

            if (MiningSession.DONATION_SESSION)
            {
                labelDevfeeStatus.Text = "Mining For: Developer";
                labelDevfeeStatus.ForeColor = Color.Salmon;
            }
            else
            {
                // labelDevfeeStatus.Text = "Mining For: User";
                labelDevfeeStatus.Text = "Mining For: User";
                labelDevfeeStatus.ForeColor = Color.LightGreen;
            }
        }

        /// <summary>
        /// The SetChildFormCenter
        /// </summary>
        /// <param name="form">The <see cref="Form"/></param>
        private void SetChildFormCenter(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Location.X + (Width - form.Width) / 2, Location.Y + (Height - form.Height) / 2);
        }

        /// <summary>
        /// The Form_Main_Shown
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void Form_Main_Shown(object sender, EventArgs e)
        {
            // general loading indicator
            var TotalLoadSteps = 11;

            LoadingScreen = new Form_Loading(this,
                International.GetText("Form_Loading_label_LoadingText"),
                International.GetText("Form_Main_loadtext_CPU"), TotalLoadSteps);

            SetChildFormCenter(LoadingScreen);
            LoadingScreen.Show();

            StartupTimer = new Timer();
            StartupTimer.Tick += StartupTimer_Tick;
            StartupTimer.Interval = 200;
            StartupTimer.Start();
        }

        /// <summary>
        /// The SMAMinerCheck_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private async void SMAMinerCheck_Tick(object sender, EventArgs e)
        {
            SMAMinerCheck.Interval = ConfigManager.GeneralConfig.SwitchMinSecondsFixed * 1000 + R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 1000);

            if (ComputeDeviceManager.Group.ContainsAMD_GPUs)
            {
                SMAMinerCheck.Interval = (ConfigManager.GeneralConfig.SwitchMinSecondsAMD + ConfigManager.GeneralConfig.SwitchMinSecondsFixed) * 1000 + R.Next(ConfigManager.GeneralConfig.SwitchMinSecondsDynamic * 1000);
            }

#if (SWITCH_TESTING)
            SMAMinerCheck.Interval = MiningDevice.SMAMinerCheckInterval;
#endif
            if (isSMAUpdated)
            {  // Don't bother checking for new profits unless SMA has changed
                isSMAUpdated = false;
                await MinersManager.SwichMostProfitableGroupUpMethod(Globals.CryptoMiner937Data);
            }

            groupBox1.Text = updateText;
        }

        /// <summary>
        /// The MinerStatsCheck_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private async void MinerStatsCheck_Tick(object sender, EventArgs e)
        {
            await MinersManager.MinerStatsCheck(Globals.CryptoMiner937Data);
        }

        /// <summary>
        /// The ComputeDevicesCheckTimer_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ComputeDevicesCheckTimer_Tick(object sender, EventArgs e)
        {
            if (ComputeDeviceManager.Query.CheckVideoControllersCountMismath())
            {
                // less GPUs than before, ACT!
                try
                {
                    var onGPUsLost = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\OnGPUsLost.bat")
                    {
                        WindowStyle = ProcessWindowStyle.Minimized
                    };

                    System.Diagnostics.Process.Start(onGPUsLost);
                }
                catch (Exception ex)
                {
                    Helpers.ConsolePrint("NICEHASH", "OnGPUsMismatch.bat error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// The InitFlowPanelStart
        /// </summary>
        private void InitFlowPanelStart()
        {
            flowLayoutPanelRates.Controls.Clear();
            // add for every cdev a
            foreach (var cdev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                if (cdev.Enabled)
                {
                    var newGroupProfitControl = new GroupProfitControl
                    {
                        Visible = false
                    };

                    flowLayoutPanelRates.Controls.Add(newGroupProfitControl);
                }
            }
        }

        /// <summary>
        /// The ClearRatesALL
        /// </summary>
        public void ClearRatesALL()
        {
            HideNotProfitable();
            ClearRates(-1);
        }

        /// <summary>
        /// The ClearRates
        /// </summary>
        /// <param name="groupCount">The <see cref="int"/></param>
        public void ClearRates(int groupCount)
        {
            if (flowLayoutPanelVisibleCount != groupCount)
            {
                flowLayoutPanelVisibleCount = groupCount;
                // hide some Controls
                var hideIndex = 0;

                foreach (var control in flowLayoutPanelRates.Controls)
                {
                    ((GroupProfitControl)control).Visible = hideIndex < groupCount ? true : false;
                    ++hideIndex;
                }
            }

            flowLayoutPanelRatesIndex = 0;
            var visibleGroupCount = 1;
            if (groupCount > 0) visibleGroupCount += groupCount;

            var groupBox1Height = EmtpyGroupPanelHeight;

            if (flowLayoutPanelRates.Controls != null && flowLayoutPanelRates.Controls.Count > 0)
            {
                var control = flowLayoutPanelRates.Controls[0];
                var panelHeight = ((GroupProfitControl)control).Size.Height * 1.2f;
                groupBox1Height = (int)((visibleGroupCount) * panelHeight);
            }

            groupBox1.Size = new Size(groupBox1.Size.Width, groupBox1Height);
            // set new height
            Size = new Size(Size.Width, MainFormHeight + groupBox1Height);
        }

        /// <summary>
        /// The AddRateInfo
        /// </summary>
        /// <param name="groupName">The <see cref="string"/></param>
        /// <param name="deviceStringInfo">The <see cref="string"/></param>
        /// <param name="iAPIData">The <see cref="ApiData"/></param>
        /// <param name="paying">The <see cref="double"/></param>
        /// <param name="isApiGetException">The <see cref="bool"/></param>
        public void AddRateInfo(string groupName, string deviceStringInfo, ApiData iAPIData, double paying, bool isApiGetException)
        {
            var ApiGetExceptionString = isApiGetException ? "**" : "";

            var speedString = Helpers.FormatDualSpeedOutput(iAPIData.AlgorithmID, iAPIData.Speed, iAPIData.SecondarySpeed) + iAPIData.AlgorithmName + ApiGetExceptionString;
            var rateBTCString = FormatPayingOutput(paying);

            var rateCurrencyString = ExchangeRateAPI.ConvertToActiveCurrency(paying * Globals.BitcoinUSDRate * factorTimeUnit).ToString("F2", CultureInfo.InvariantCulture)
                + string.Format(" {0}/", ExchangeRateAPI.ActiveDisplayCurrency) + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());

            try
            {  // flowLayoutPanelRatesIndex may be OOB, so catch
                ((GroupProfitControl)flowLayoutPanelRates.Controls[flowLayoutPanelRatesIndex++])
                    .UpdateProfitStats(groupName, deviceStringInfo, speedString, rateBTCString, rateCurrencyString);
            }
            catch { }

            UpdateGlobalRate();
        }

        /// <summary>
        /// The ShowNotProfitable
        /// </summary>
        /// <param name="msg">The <see cref="string"/></param>
        public void ShowNotProfitable(string msg)
        {
            if (ConfigManager.GeneralConfig.UseIFTTT)
            {
                if (!IsNotProfitable)
                {
                    IFTTT.PostToIFTTT("nicehash", msg);
                    IsNotProfitable = true;
                }
            }

            label_NotProfitable.Visible = true;
            label_NotProfitable.Text = msg;
            label_NotProfitable.Invalidate();
        }

        /// <summary>
        /// The HideNotProfitable
        /// </summary>
        public void HideNotProfitable()
        {
            if (ConfigManager.GeneralConfig.UseIFTTT)
            {
                if (IsNotProfitable)
                {
                    IFTTT.PostToIFTTT("nicehash", "Mining is once again profitable and has resumed.");
                    IsNotProfitable = false;
                }
            }

            label_NotProfitable.Visible = false;
            label_NotProfitable.Invalidate();
        }

        /// <summary>
        /// The UpdateGlobalRate
        /// </summary>
        private void UpdateGlobalRate()
        {
            var TotalRate = MinersManager.GetTotalRate();

            if (ConfigManager.GeneralConfig.AutoScaleBTCValues && TotalRate < 0.1)
            {
                toolStripStatusLabelBTCDayText.Text = "mBTC/" + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
                toolStripStatusLabelGlobalRateValue.Text = (TotalRate * 1000 * factorTimeUnit).ToString("F5", CultureInfo.InvariantCulture);
            }
            else
            {
                toolStripStatusLabelBTCDayText.Text = "BTC/" + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
                toolStripStatusLabelGlobalRateValue.Text = (TotalRate * factorTimeUnit).ToString("F6", CultureInfo.InvariantCulture);
            }

            toolStripStatusLabelBTCDayValue.Text = ExchangeRateAPI.ConvertToActiveCurrency((TotalRate * factorTimeUnit * Globals.BitcoinUSDRate)).ToString("F2", CultureInfo.InvariantCulture);
            toolStripStatusLabelBalanceText.Text = (ExchangeRateAPI.ActiveDisplayCurrency + "/") + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
        }

        /// <summary>
        /// The BalanceCallback
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void BalanceCallback(object sender, EventArgs e)
        {
            Helpers.ConsolePrint("Hash-Kings", "Balance update");
            var Balance = CryptoStats.Balance;

            if (Balance > 0)
            {
                if (ConfigManager.GeneralConfig.AutoScaleBTCValues && Balance < 0.1)
                {
                    toolStripStatusLabelBalanceBTCCode.Text = "mBTC";
                    toolStripStatusLabelBalanceBTCValue.Text = (Balance * 1000).ToString("F5", CultureInfo.InvariantCulture);
                }
                else
                {
                    toolStripStatusLabelBalanceBTCCode.Text = "BTC";
                    toolStripStatusLabelBalanceBTCValue.Text = Balance.ToString("F6", CultureInfo.InvariantCulture);
                }

                // Helpers.ConsolePrint("CurrencyConverter", "Using CurrencyConverter" + ConfigManager.Instance.GeneralConfig.DisplayCurrency);
                var Amount = (Balance * Globals.BitcoinUSDRate);
                Amount = ExchangeRateAPI.ConvertToActiveCurrency(Amount);
                toolStripStatusLabelBalanceDollarText.Text = Amount.ToString("F2", CultureInfo.InvariantCulture);
                toolStripStatusLabelBalanceDollarValue.Text = $"({ExchangeRateAPI.ActiveDisplayCurrency})";
            }
        }

        /// <summary>
        /// The BitcoinExchangeCheck_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void BitcoinExchangeCheck_Tick(object sender, EventArgs e)
        {
            Helpers.ConsolePrint("Hash-Kings", "Bitcoin rate get");
            ExchangeRateAPI.UpdateAPI(textBoxWorkerName.Text.Trim());
            var BR = ExchangeRateAPI.GetUSDExchangeRate();
            var currencyRate = International.GetText("BenchmarkRatioRateN_A");

            if (BR > 0)
            {
                Globals.BitcoinUSDRate = BR;
                currencyRate = ExchangeRateAPI.ConvertToActiveCurrency(BR).ToString("F2");
            }

            toolTip1.SetToolTip(statusStrip1, $"1 BTC = {currencyRate} {ExchangeRateAPI.ActiveDisplayCurrency}");

            Helpers.ConsolePrint("Hash-Kings", "Current Bitcoin rate: " + Globals.BitcoinUSDRate.ToString("F2", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The SMACallback
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void SMACallback(object sender, EventArgs e)
        {
            Helpers.ConsolePrint("Hash-Kings", "API Update");
            isSMAUpdated = true;

            if (CryptoStats.AlgorithmRates != null)
            {
                Globals.CryptoMiner937Data = CryptoStats.AlgorithmRates; Globals.CryptoMiner937Data = CryptoStats.AlgorithmRates;
                // Save new rates to config
                ConfigManager.ApiCache.CryptoMiner937Data = CryptoStats.AlgorithmRates;
                ConfigManager.ApiCache.CryptoMiner937DataTimeStamp = DateTime.Now;
                ConfigManager.ApiCacheFileCommit();
                // Helpers.ConsolePrint("Hash-Kings", "API Update from Null Value");
                // groupBox1.Text = String.Format(International.GetText("Form_Main_Group_Device_Rates"), ConfigManager.GeneralConfig.CryptoMiner937DataTimeStamp);
                updateText = string.Format(International.GetText("Form_Main_Group_Device_Rates"), ConfigManager.ApiCache.CryptoMiner937DataTimeStamp);
            }
            else
            {
                Globals.CryptoMiner937Data = CryptoStats.AlgorithmRates; Globals.CryptoMiner937Data = CryptoStats.AlgorithmRates;
                // Save new rates to config
                ConfigManager.ApiCache.CryptoMiner937Data = CryptoStats.AlgorithmRates;
                ConfigManager.ApiCache.CryptoMiner937DataTimeStamp = DateTime.Now;
                ConfigManager.ApiCacheFileCommit();
                // Helpers.ConsolePrint("Hash-Kings", "API Update Existing Values");
                // groupBox1.Text = String.Format(International.GetText("Form_Main_Group_Device_Rates"), ConfigManager.GeneralConfig.CryptoMiner937DataTimeStamp);
                updateText = string.Format(International.GetText("Form_Main_Group_Device_Rates"), ConfigManager.ApiCache.CryptoMiner937DataTimeStamp);
            }
        }

        /// <summary>
        /// The VersionBurnCallback
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="SocketEventArgs"/></param>
        private void VersionBurnCallback(object sender, SocketEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                StopMining();

                if (BenchmarkForm != null)
                    BenchmarkForm.StopBenchmark();

                var dialogResult = MessageBox.Show(e.Message, International.GetText("Error_with_Exclamation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }));
        }

        /// <summary>
        /// The ConnectionLostCallback
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ConnectionLostCallback(object sender, EventArgs e)
        {
            if (Globals.CryptoMiner937Data == null && ConfigManager.GeneralConfig.ShowInternetConnectionWarning && ShowWarningNiceHashData)
            {
                ShowWarningNiceHashData = false;

                var dialogResult = MessageBox.Show(International.GetText("Form_Main_msgbox_NoInternetMsg"),
                                                            International.GetText("Form_Main_msgbox_NoInternetTitle"),
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Yes) return;
                else if (dialogResult == DialogResult.No)
                    System.Windows.Forms.Application.Exit();
            }
        }

        /// <summary>
        /// The ConnectionEstablishedCallback
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ConnectionEstablishedCallback(object sender, EventArgs e)
        {
            // send credentials
            CryptoStats.SetCredentials(textBoxBTCAddress.Text.Trim(), textBoxWorkerName.Text.Trim());
        }

        /// <summary>
        /// The VersionUpdateCallback
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void VersionUpdateCallback(object sender, EventArgs e)
        {
            var ver = CryptoStats.Version.Replace(".", ",");
            var ver2 = CryptoStats.Version;
            if (ver == null) return;
            // var programVersion = "Fork_Fix_"+ConfigManager.GeneralConfig.ForkFixVersion.ToString().Replace(",",".");
            var programVersion = ConfigManager.GeneralConfig.ConfigFileVersion.ToString().Replace(".", ",");
            // Helpers.ConsolePrint("Program version: ", programVersion);
            // var ret = programVersion.CompareTo(ver);
            if (ver.Length < 1)
            {
                return;
            }

            ver = ver.Replace("-Beta", "");
            ver2 = ver2.Replace("-Beta", "");
            ver = ver.Replace("-Alpha", "");
            ver2 = ver2.Replace("-Alpha", "");
            // Helpers.ConsolePrint("Github version: ", ver);
            var programVersionn = double.Parse(programVersion, CultureInfo.InvariantCulture);
            // Helpers.ConsolePrint("Program version: ", programVersionn.ToString());
            var vern = double.Parse(ver, CultureInfo.InvariantCulture);
            // Helpers.ConsolePrint("Github version: ", vern.ToString());
            // if (ret < 0 || (ret == 0 && BetaAlphaPostfixString != ""))
            if (programVersionn < vern)
            {
                Helpers.ConsolePrint("Old version detected. Update needed.", "");
                SetVersionLabel(string.Format(International.GetText("Form_Main_new_version_released").Replace("v{0}", "{0}"), "V " + ver2));
                // _visitUrlNew = Links.VisitUrlNew + ver;
                VisitURLNew = Links.VisitURLNew;
            }
        }

        /// <summary>
        /// The SetVersionLabelCallback
        /// </summary>
        /// <param name="text">The <see cref="string"/></param>
        private delegate void SetVersionLabelCallback(string text);

        /// <summary>
        /// The SetVersionLabel
        /// </summary>
        /// <param name="text">The <see cref="string"/></param>
        private void SetVersionLabel(string text)
        {
            if (LinkLabelUpdate.InvokeRequired)
            {
                var d = new SetVersionLabelCallback(SetVersionLabel);
                Invoke(d, new object[] { text });
            }
            else
            {
                LinkLabelUpdate.Text = text;
            }
        }

        /// <summary>
        /// The VerifyMiningAddress
        /// </summary>
        /// <param name="ShowError">The <see cref="bool"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool VerifyMiningAddress(bool ShowError)
        {
            if (!BitcoinAddress.ValidateBitcoinAddress(textBoxBTCAddress.Text.Trim()) && ShowError)
            {
                var result = MessageBox.Show(International.GetText("Form_Main_msgbox_InvalidBTCAddressMsg"),
                                                      International.GetText("Error_with_Exclamation"),
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == System.Windows.Forms.DialogResult.Yes)
                    System.Diagnostics.Process.Start(Links.NHM_BTC_Wallet_Faq);

                textBoxBTCAddress.Focus();
                return false;
            }
            else if (!BitcoinAddress.ValidateWorkerName(textBoxWorkerName.Text.Trim()) && ShowError)
            {
                var result = MessageBox.Show(International.GetText("Form_Main_msgbox_InvalidWorkerNameMsg"),
                                                      International.GetText("Error_with_Exclamation"),
                                                      MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBoxWorkerName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// The LinkLabelCheckStats_LinkClicked
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/></param>
        private void LinkLabelCheckStats_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!VerifyMiningAddress(true)) return;

            System.Diagnostics.Process.Start(Links.CheckStats + textBoxBTCAddress.Text.Trim());
        }

        /// <summary>
        /// The LinkLabelChooseBTCWallet_LinkClicked
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/></param>
        private void LinkLabelChooseBTCWallet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Links.NHM_BTC_Wallet_Faq);
        }

        /// <summary>
        /// The LinkLabelUpdate_LinkClicked
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/></param>
        private void LinkLabelUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(VisitURLNew);
        }

        /// <summary>
        /// The Form1_FormClosing
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MinersManager.StopAllMiners();

            MessageBoxManager.Unregister();
        }

        /// <summary>
        /// The ButtonBenchmark_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonBenchmark_Click(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.ServiceLocation = comboBoxLocation.SelectedIndex;

            BenchmarkForm = new Form_Benchmark();
            SetChildFormCenter(BenchmarkForm);
            BenchmarkForm.ShowDialog();
            var startMining = BenchmarkForm.StartMining;
            BenchmarkForm = null;

            InitMainConfigGUIData();

            if (startMining)
            {
                ButtonStartMining_Click(null, null);
            }
        }

        /// <summary>
        /// The ButtonSettings_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            var Settings = new Form_Settings();
            SetChildFormCenter(Settings);
            Settings.ShowDialog();

            if (Settings.IsChange && Settings.IsChangeSaved && Settings.IsRestartNeeded)
            {
                MessageBox.Show(
                    International.GetText("Form_Main_Restart_Required_Msg"),
                    International.GetText("Form_Main_Restart_Required_Title"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                var PHandle = new Process();
                PHandle.StartInfo.FileName = Application.ExecutablePath;
                PHandle.Start();
                Close();
            }
            else if (Settings.IsChange && Settings.IsChangeSaved)
            {
                InitLocalization();
                InitMainConfigGUIData();
            }
        }

        /// <summary>
        /// The ButtonStartMining_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonStartMining_Click(object sender, EventArgs e)
        {
            IsManuallyStarted = true;

            if (StartMining(true) == StartMiningReturnType.ShowNoMining)
            {
                IsManuallyStarted = false;
                StopMining();

                MessageBox.Show(International.GetText("Form_Main_StartMiningReturnedFalse"),
                                International.GetText("Warning_with_Exclamation"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// The ButtonStopMining_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonStopMining_Click(object sender, EventArgs e)
        {
            IsManuallyStarted = false;
            StopMining();
        }

        /// <summary>
        /// The FormatPayingOutput
        /// </summary>
        /// <param name="paying">The <see cref="double"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string FormatPayingOutput(double paying)
        {
            var ret = "";

            if (ConfigManager.GeneralConfig.AutoScaleBTCValues && paying < 0.1)
                ret = (paying * 1000 * factorTimeUnit).ToString("F5", CultureInfo.InvariantCulture) + " mBTC/" + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
            else
                ret = (paying * factorTimeUnit).ToString("F6", CultureInfo.InvariantCulture) + " BTC/" + International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());

            return ret;
        }

        /// <summary>
        /// The ButtonLogo_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonLogo_Click(object sender, EventArgs e)
        {
            // System.Diagnostics.Process.Start(Links.VisitURL);
        }

        /// <summary>
        /// The ButtonHelp_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Links.CheckStats + ConfigManager.GeneralConfig.zergAddress.Trim());
        }

        /// <summary>
        /// The ToolStripStatusLabel10_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ToolStripStatusLabel10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Links.NHM_Paying_Faq);
        }

        /// <summary>
        /// The ToolStripStatusLabel10_MouseHover
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ToolStripStatusLabel10_MouseHover(object sender, EventArgs e)
        {
            statusStrip1.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// The ToolStripStatusLabel10_MouseLeave
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ToolStripStatusLabel10_MouseLeave(object sender, EventArgs e)
        {
            statusStrip1.Cursor = Cursors.Default;
        }

        /// <summary>
        /// The TextBoxCheckBoxMain_Leave
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void TextBoxCheckBoxMain_Leave(object sender, EventArgs e)
        {
            if (VerifyMiningAddress(false))
            {
                if (ConfigManager.GeneralConfig.BitcoinAddress != textBoxBTCAddress.Text.Trim()
                    || ConfigManager.GeneralConfig.WorkerName != textBoxWorkerName.Text.Trim())
                {
                    // Reset credentials
                    CryptoStats.SetCredentials(textBoxBTCAddress.Text.Trim(), textBoxWorkerName.Text.Trim());
                }

                // Commit to config.json
                ConfigManager.GeneralConfig.BitcoinAddress = textBoxBTCAddress.Text.Trim();
                ConfigManager.GeneralConfig.WorkerName = textBoxWorkerName.Text.Trim();
                ConfigManager.GeneralConfig.ServiceLocation = comboBoxLocation.SelectedIndex;
                ConfigManager.GeneralConfigFileCommit();
            }
        }

        // Minimize to system tray if MinimizeToTray is set to true
        /// <summary>
        /// The Form1_Resize
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.Icon = Properties.Resources.logo;
            notifyIcon1.Text = Application.ProductName + " v" + Application.ProductVersion + "\nDouble-click to restore..";

            if (ConfigManager.GeneralConfig.MinimizeToTray && FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        // Restore zPoolMiner from the system tray
        /// <summary>
        /// The NotifyIcon1_DoubleClick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        // /////////////////////////////////////
        // Miner control functions
        /// <summary>
        /// Defines the StartMiningReturnType
        /// </summary>
        private enum StartMiningReturnType
        {
            /// <summary>
            /// Defines the StartMining
            /// </summary>
            StartMining,

            /// <summary>
            /// Defines the ShowNoMining
            /// </summary>
            ShowNoMining,

            /// <summary>
            /// Defines the IgnoreMsg
            /// </summary>
            IgnoreMsg
        }

        /// <summary>
        /// The StartMining
        /// </summary>
        /// <param name="showWarnings">The <see cref="bool"/></param>
        /// <returns>The <see cref="StartMiningReturnType"/></returns>
        private StartMiningReturnType StartMining(bool showWarnings)
        {
            if (textBoxBTCAddress.Text.Equals("1"))
            {
                if (showWarnings)
                {
                    var result = MessageBox.Show(International.GetText("Form_Main_DemoModeMsg"),
                                                      International.GetText("Form_Main_DemoModeTitle"),
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DemoMode = true;
                        labelDemoMode.Visible = true;
                        labelDemoMode.Text = International.GetText("Form_Main_DemoModeLabel");
                    }
                    else
                    {
                        return StartMiningReturnType.IgnoreMsg;
                    }
                }
                else
                {
                    return StartMiningReturnType.IgnoreMsg; ;
                }
            }
            else if (!VerifyMiningAddress(true)) return StartMiningReturnType.IgnoreMsg;

            if (Globals.CryptoMiner937Data == null)
            {
                if (showWarnings)
                {
                    MessageBox.Show(International.GetText("Form_Main_msgbox_NullNiceHashDataMsg"),
                                International.GetText("Error_with_Exclamation"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return StartMiningReturnType.IgnoreMsg;
            }

            // Check if there are unbenchmakred algorithms
            var isBenchInit = true;
            var hasAnyAlgoEnabled = false;

            foreach (var cdev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                if (cdev.Enabled)
                {
                    foreach (var algo in cdev.GetAlgorithmSettings())
                    {
                        if (algo.Enabled == true)
                        {
                            hasAnyAlgoEnabled = true;

                            if (algo.BenchmarkSpeed == 0)
                            {
                                isBenchInit = false;
                                break;
                            }
                        }
                    }
                }
            }

            // Check if the user has run benchmark first
            if (!isBenchInit)
            {
                var result = DialogResult.No;

                if (showWarnings)
                {
                    result = MessageBox.Show(International.GetText("EnabledUnbenchmarkedAlgorithmsWarning"),
                                                              International.GetText("Warning_with_Exclamation"),
                                                              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                }

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    BenchmarkForm = new Form_Benchmark(
                        BenchmarkPerformanceType.Standard,
                        true);

                    SetChildFormCenter(BenchmarkForm);
                    BenchmarkForm.ShowDialog();
                    BenchmarkForm = null;
                    InitMainConfigGUIData();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    // check devices without benchmarks
                    foreach (var cdev in ComputeDeviceManager.Available.AllAvaliableDevices)
                    {
                        if (cdev.Enabled)
                        {
                            var Enabled = false;

                            foreach (var algo in cdev.GetAlgorithmSettings())
                            {
                                if (algo.BenchmarkSpeed > 0)
                                {
                                    Enabled = true;
                                    break;
                                }
                            }

                            cdev.Enabled = Enabled;
                        }
                    }
                }
                else
                {
                    return StartMiningReturnType.IgnoreMsg;
                }
            }

            textBoxBTCAddress.Enabled = false;
            textBoxWorkerName.Enabled = false;
            comboBoxLocation.Enabled = false;
            buttonBenchmark.Enabled = false;
            buttonStartMining.Enabled = false;
            buttonSettings.Enabled = false;
            devicesListViewEnableControl1.IsMining = true;
            buttonStopMining.Enabled = true;

            // Disable profitable notification on start
            IsNotProfitable = false;

            ConfigManager.GeneralConfig.BitcoinAddress = textBoxBTCAddress.Text.Trim();
            ConfigManager.GeneralConfig.WorkerName = textBoxWorkerName.Text.Trim();
            ConfigManager.GeneralConfig.ServiceLocation = comboBoxLocation.SelectedIndex;

            InitFlowPanelStart();
            ClearRatesALL();

            var btcAddress = DemoMode ? Globals.DemoUser : textBoxBTCAddress.Text.Trim();
            var isMining = MinersManager.StartInitialize(this, Globals.MiningLocation[comboBoxLocation.SelectedIndex], textBoxWorkerName.Text.Trim(), btcAddress);

            if (!DemoMode) ConfigManager.GeneralConfigFileCommit();

            isSMAUpdated = true;  // Always check profits on mining start
            SMAMinerCheck.Interval = 100;
            SMAMinerCheck.Start();
            MinerStatsCheck.Start();

            if (ConfigManager.GeneralConfig.RunScriptOnCUDA_GPU_Lost)
            {
                ComputeDevicesCheckTimer = new SystemTimer();
                ComputeDevicesCheckTimer.Elapsed += ComputeDevicesCheckTimer_Tick;
                ComputeDevicesCheckTimer.Interval = 60000;

                ComputeDevicesCheckTimer.Start();
            }

            return isMining ? StartMiningReturnType.StartMining : StartMiningReturnType.ShowNoMining;
        }

        private void restartProgram()
        {
            var pHandle = new Process
            {
                StartInfo =
                    {
                        FileName = Application.ExecutablePath
                    }
            };
            // pHandle.Start();
            // Close();
        }

        private void DeviceStatusTimer_Tick(object sender, EventArgs e)
        {
            if (needRestart)
            {
                needRestart = false;
                restartProgram();
            }
            // devicesListViewEnableControl1.SetComputeDevicesStatus(ComputeDeviceManager.Available.Devices);
        }

        /// <summary>
        /// The StopMining
        /// </summary>
        private void StopMining()
        {
            MinerStatsCheck.Stop();
            SMAMinerCheck.Stop();

            if (ComputeDevicesCheckTimer != null)
                ComputeDevicesCheckTimer.Stop();

            // Disable IFTTT notification before label call
            IsNotProfitable = false;

            MinersManager.StopAllMiners();

            textBoxBTCAddress.Enabled = true;
            textBoxWorkerName.Enabled = true;
            comboBoxLocation.Enabled = true;
            buttonBenchmark.Enabled = true;
            buttonStartMining.Enabled = true;
            buttonSettings.Enabled = true;
            devicesListViewEnableControl1.IsMining = false;
            buttonStopMining.Enabled = false;

            if (DemoMode)
            {
                DemoMode = false;
                labelDemoMode.Visible = false;
            }

            UpdateGlobalRate();
        }

        private void beepToggle_CheckedChanged(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.beep = beepToggle.Checked;
        }

        private void tempLower_ValueChanged(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.tempLowThreshold = decimal.ToInt32(tempLower.Value);
        }

        private void tempUpper_ValueChanged(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.tempHighThreshold = decimal.ToInt32(tempUpper.Value);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}