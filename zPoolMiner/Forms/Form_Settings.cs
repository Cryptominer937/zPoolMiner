namespace zPoolMiner.Forms
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners;
    using zPoolMiner.Miners.Grouping;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="Form_Settings" />
    /// </summary>
    public partial class Form_Settings : Form
    {
        /// <summary>
        /// Defines the _isInitFinished
        /// </summary>
        private bool _isInitFinished;

        /// <summary>
        /// Defines the _isChange
        /// </summary>
        private bool _isChange;

        /// <summary>
        /// Gets or sets a value indicating whether IsChange
        /// </summary>
        public bool IsChange
        {
            get { return _isChange; }
            private set
            {
                if (_isInitFinished) _isChange = value;
                else _isChange = false;
            }
        }

        /// <summary>
        /// Defines the isCredChange
        /// </summary>
        private bool isCredChange;

        /// <summary>
        /// Gets or sets a value indicating whether IsChangeSaved
        /// </summary>
        public bool IsChangeSaved { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsRestartNeeded
        /// </summary>
        public bool IsRestartNeeded { get; private set; }

        // most likely we wil have settings only per unique devices
        // most likely we wil have settings only per unique devices        /// <summary>
        /// Defines the ShowUniqueDeviceList
        /// </summary>
        private bool ShowUniqueDeviceList = true;

        /// <summary>
        /// Defines the _selectedComputeDevice
        /// </summary>
        private ComputeDevice _selectedComputeDevice;

        /// <summary>
        /// Defines the rkStartup
        /// </summary>
        private RegistryKey rkStartup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        /// <summary>
        /// Defines the isStartupChanged
        /// </summary>
        private bool isStartupChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form_Settings"/> class.
        /// </summary>
        public Form_Settings()
        {
            InitializeComponent();
            Icon = zPoolMiner.Properties.Resources.logo;

            // ret = 1; // default
            IsChange = false;
            IsChangeSaved = false;

            // backup settings
            ConfigManager.CreateBackup();

            // initialize form
            InitializeFormTranslations();

            // Initialize toolTip
            InitializeToolTip();

            // Initialize tabs
            InitializeGeneralTab();

            // initialization calls
            InitializeDevicesTab();
            // link algorithm list with algorithm settings control
            algorithmSettingsControl1.Enabled = false;
            algorithmsListView1.ComunicationInterface = algorithmSettingsControl1;
            // algorithmsListView1.RemoveRatioRates();

            // set first device selected {
            if (ComputeDeviceManager.Available.AllAvaliableDevices.Count > 0)
            {
                _selectedComputeDevice = ComputeDeviceManager.Available.AllAvaliableDevices[0];
                algorithmsListView1.SetAlgorithms(_selectedComputeDevice, _selectedComputeDevice.Enabled);
                groupBoxAlgorithmSettings.Text = string.Format(International.GetText("FormSettings_AlgorithmsSettings"), _selectedComputeDevice.Name);
            }

            // At the very end set to true
            _isInitFinished = true;
        }

        /// <summary>
        /// The InitializeToolTip
        /// </summary>
        private void InitializeToolTip()
        {
            // Setup Tooltips
            toolTip1.SetToolTip(comboBox_Language, International.GetText("Form_Settings_ToolTip_Language"));
            toolTip1.SetToolTip(label_Language, International.GetText("Form_Settings_ToolTip_Language"));
            toolTip1.SetToolTip(pictureBox_Language, International.GetText("Form_Settings_ToolTip_Language"));

            toolTip1.SetToolTip(checkBox_DebugConsole, International.GetText("Form_Settings_ToolTip_checkBox_DebugConsole"));
            toolTip1.SetToolTip(pictureBox_DebugConsole, International.GetText("Form_Settings_ToolTip_checkBox_DebugConsole"));

            toolTip1.SetToolTip(comboBox_TimeUnit, International.GetText("Form_Settings_ToolTip_TimeUnit"));
            toolTip1.SetToolTip(label_TimeUnit, International.GetText("Form_Settings_ToolTip_TimeUnit"));
            toolTip1.SetToolTip(pictureBox_TimeUnit, International.GetText("Form_Settings_ToolTip_TimeUnit"));

            toolTip1.SetToolTip(checkBox_HideMiningWindows, International.GetText("Form_Settings_ToolTip_checkBox_HideMiningWindows"));
            toolTip1.SetToolTip(pictureBox_HideMiningWindows, International.GetText("Form_Settings_ToolTip_checkBox_HideMiningWindows"));

            toolTip1.SetToolTip(checkBox_MinimizeToTray, International.GetText("Form_Settings_ToolTip_checkBox_MinimizeToTray"));
            toolTip1.SetToolTip(pictureBox_MinimizeToTray, International.GetText("Form_Settings_ToolTip_checkBox_MinimizeToTray"));

            toolTip1.SetToolTip(checkBox_Use3rdPartyMiners, International.GetText("Form_Settings_General_3rdparty_ToolTip"));
            toolTip1.SetToolTip(pictureBox_Use3rdPartyMiners, International.GetText("Form_Settings_General_3rdparty_ToolTip"));

            toolTip1.SetToolTip(checkBox_AllowMultipleInstances, International.GetText("Form_Settings_General_AllowMultipleInstances_ToolTip"));
            toolTip1.SetToolTip(pictureBox_AllowMultipleInstances, International.GetText("Form_Settings_General_AllowMultipleInstances_ToolTip"));

            toolTip1.SetToolTip(textBox_SwitchMinSecondsFixed, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsFixed"));
            toolTip1.SetToolTip(label_SwitchMinSecondsFixed, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsFixed"));
            toolTip1.SetToolTip(pictureBox_SwitchMinSecondsFixed, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsFixed"));

            toolTip1.SetToolTip(label_MinProfit, International.GetText("Form_Settings_ToolTip_MinimumProfit"));
            toolTip1.SetToolTip(pictureBox_MinProfit, International.GetText("Form_Settings_ToolTip_MinimumProfit"));
            toolTip1.SetToolTip(textBox_MinProfit, International.GetText("Form_Settings_ToolTip_MinimumProfit"));

            toolTip1.SetToolTip(textBox_SwitchMinSecondsDynamic, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsDynamic"));
            toolTip1.SetToolTip(label_SwitchMinSecondsDynamic, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsDynamic"));
            toolTip1.SetToolTip(pictureBox_SwitchMinSecondsDynamic, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsDynamic"));

            toolTip1.SetToolTip(textBox_SwitchMinSecondsAMD, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsAMD"));
            toolTip1.SetToolTip(label_SwitchMinSecondsAMD, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsAMD"));
            toolTip1.SetToolTip(pictureBox_SwitchMinSecondsAMD, International.GetText("Form_Settings_ToolTip_SwitchMinSecondsAMD"));

            toolTip1.SetToolTip(textBox_MinerAPIQueryInterval, International.GetText("Form_Settings_ToolTip_MinerAPIQueryInterval"));
            toolTip1.SetToolTip(label_MinerAPIQueryInterval, International.GetText("Form_Settings_ToolTip_MinerAPIQueryInterval"));
            toolTip1.SetToolTip(pictureBox_MinerAPIQueryInterval, International.GetText("Form_Settings_ToolTip_MinerAPIQueryInterval"));

            toolTip1.SetToolTip(textBox_MinerRestartDelayMS, International.GetText("Form_Settings_ToolTip_MinerRestartDelayMS"));
            toolTip1.SetToolTip(label_MinerRestartDelayMS, International.GetText("Form_Settings_ToolTip_MinerRestartDelayMS"));
            toolTip1.SetToolTip(pictureBox_MinerRestartDelayMS, International.GetText("Form_Settings_ToolTip_MinerRestartDelayMS"));

            toolTip1.SetToolTip(textBox_APIBindPortStart, International.GetText("Form_Settings_ToolTip_APIBindPortStart"));
            toolTip1.SetToolTip(label_APIBindPortStart, International.GetText("Form_Settings_ToolTip_APIBindPortStart"));
            toolTip1.SetToolTip(pictureBox_APIBindPortStart, International.GetText("Form_Settings_ToolTip_APIBindPortStart"));

            toolTip1.SetToolTip(comboBox_DagLoadMode, International.GetText("Form_Settings_ToolTip_DagGeneration"));
            toolTip1.SetToolTip(label_DagGeneration, International.GetText("Form_Settings_ToolTip_DagGeneration"));
            toolTip1.SetToolTip(pictureBox_DagGeneration, International.GetText("Form_Settings_ToolTip_DagGeneration"));

            toolTip1.SetToolTip(checkBox_DisableDetectionNVIDIA, string.Format(International.GetText("Form_Settings_ToolTip_checkBox_DisableDetection"), "NVIDIA"));
            toolTip1.SetToolTip(checkBox_DisableDetectionAMD, string.Format(International.GetText("Form_Settings_ToolTip_checkBox_DisableDetection"), "AMD"));
            toolTip1.SetToolTip(pictureBox_DisableDetectionNVIDIA, string.Format(International.GetText("Form_Settings_ToolTip_checkBox_DisableDetection"), "NVIDIA"));
            toolTip1.SetToolTip(pictureBox_DisableDetectionAMD, string.Format(International.GetText("Form_Settings_ToolTip_checkBox_DisableDetection"), "AMD"));

            toolTip1.SetToolTip(checkBox_AutoScaleBTCValues, International.GetText("Form_Settings_ToolTip_checkBox_AutoScaleBTCValues"));
            toolTip1.SetToolTip(pictureBox_AutoScaleBTCValues, International.GetText("Form_Settings_ToolTip_checkBox_AutoScaleBTCValues"));

            toolTip1.SetToolTip(checkBox_StartMiningWhenIdle, International.GetText("Form_Settings_ToolTip_checkBox_StartMiningWhenIdle"));
            toolTip1.SetToolTip(pictureBox_StartMiningWhenIdle, International.GetText("Form_Settings_ToolTip_checkBox_StartMiningWhenIdle"));

            toolTip1.SetToolTip(textBox_MinIdleSeconds, International.GetText("Form_Settings_ToolTip_MinIdleSeconds"));
            toolTip1.SetToolTip(label_MinIdleSeconds, International.GetText("Form_Settings_ToolTip_MinIdleSeconds"));
            toolTip1.SetToolTip(pictureBox_MinIdleSeconds, International.GetText("Form_Settings_ToolTip_MinIdleSeconds"));

            toolTip1.SetToolTip(checkBox_LogToFile, International.GetText("Form_Settings_ToolTip_checkBox_LogToFile"));
            toolTip1.SetToolTip(pictureBox_LogToFile, International.GetText("Form_Settings_ToolTip_checkBox_LogToFile"));

            toolTip1.SetToolTip(textBox_LogMaxFileSize, International.GetText("Form_Settings_ToolTip_LogMaxFileSize"));
            toolTip1.SetToolTip(label_LogMaxFileSize, International.GetText("Form_Settings_ToolTip_LogMaxFileSize"));
            toolTip1.SetToolTip(pictureBox_LogMaxFileSize, International.GetText("Form_Settings_ToolTip_LogMaxFileSize"));

            toolTip1.SetToolTip(checkBox_ShowDriverVersionWarning, International.GetText("Form_Settings_ToolTip_checkBox_ShowDriverVersionWarning"));
            toolTip1.SetToolTip(pictureBox_ShowDriverVersionWarning, International.GetText("Form_Settings_ToolTip_checkBox_ShowDriverVersionWarning"));

            toolTip1.SetToolTip(checkBox_DisableWindowsErrorReporting, International.GetText("Form_Settings_ToolTip_checkBox_DisableWindowsErrorReporting"));
            toolTip1.SetToolTip(pictureBox_DisableWindowsErrorReporting, International.GetText("Form_Settings_ToolTip_checkBox_DisableWindowsErrorReporting"));

            toolTip1.SetToolTip(checkBox_ShowInternetConnectionWarning, International.GetText("Form_Settings_ToolTip_checkBox_ShowInternetConnectionWarning"));
            toolTip1.SetToolTip(pictureBox_ShowInternetConnectionWarning, International.GetText("Form_Settings_ToolTip_checkBox_ShowInternetConnectionWarning"));

            toolTip1.SetToolTip(checkBox_NVIDIAP0State, International.GetText("Form_Settings_ToolTip_checkBox_NVIDIAP0State"));
            toolTip1.SetToolTip(pictureBox_NVIDIAP0State, International.GetText("Form_Settings_ToolTip_checkBox_NVIDIAP0State"));

            toolTip1.SetToolTip(checkBox_RunScriptOnCUDA_GPU_Lost, International.GetText("Form_Settings_ToolTip_checkBox_RunScriptOnCUDA_GPU_Lost"));
            toolTip1.SetToolTip(pictureBox_RunScriptOnCUDA_GPU_Lost, International.GetText("Form_Settings_ToolTip_checkBox_RunScriptOnCUDA_GPU_Lost"));

            toolTip1.SetToolTip(checkBox_RunAtStartup, International.GetText("Form_Settings_ToolTip_checkBox_RunAtStartup"));
            toolTip1.SetToolTip(pictureBox_RunAtStartup, International.GetText("Form_Settings_ToolTip_checkBox_RunAtStartup"));

            toolTip1.SetToolTip(checkBox_AutoStartMining, International.GetText("Form_Settings_ToolTip_checkBox_AutoStartMining"));
            toolTip1.SetToolTip(pictureBox_AutoStartMining, International.GetText("Form_Settings_ToolTip_checkBox_AutoStartMining"));

            toolTip1.SetToolTip(textBox_ethminerDefaultBlockHeight, International.GetText("Form_Settings_ToolTip_ethminerDefaultBlockHeight"));
            toolTip1.SetToolTip(label_ethminerDefaultBlockHeight, International.GetText("Form_Settings_ToolTip_ethminerDefaultBlockHeight"));
            toolTip1.SetToolTip(pictureBox_ethminerDefaultBlockHeight, International.GetText("Form_Settings_ToolTip_ethminerDefaultBlockHeight"));

            toolTip1.SetToolTip(label_displayCurrency, International.GetText("Form_Settings_ToolTip_DisplayCurrency"));
            toolTip1.SetToolTip(pictureBox_displayCurrency, International.GetText("Form_Settings_ToolTip_DisplayCurrency"));
            toolTip1.SetToolTip(currencyConverterCombobox, International.GetText("Form_Settings_ToolTip_DisplayCurrency"));

            // Setup Tooltips CPU
            toolTip1.SetToolTip(comboBox_CPU0_ForceCPUExtension, International.GetText("Form_Settings_ToolTip_CPU_ForceCPUExtension"));
            toolTip1.SetToolTip(label_CPU0_ForceCPUExtension, International.GetText("Form_Settings_ToolTip_CPU_ForceCPUExtension"));
            toolTip1.SetToolTip(pictureBox_CPU0_ForceCPUExtension, International.GetText("Form_Settings_ToolTip_CPU_ForceCPUExtension"));

            // amd disable temp control
            toolTip1.SetToolTip(checkBox_AMD_DisableAMDTempControl, International.GetText("Form_Settings_ToolTip_DisableAMDTempControl"));
            toolTip1.SetToolTip(pictureBox_AMD_DisableAMDTempControl, International.GetText("Form_Settings_ToolTip_DisableAMDTempControl"));

            // disable default optimizations
            toolTip1.SetToolTip(checkBox_DisableDefaultOptimizations, International.GetText("Form_Settings_ToolTip_DisableDefaultOptimizations"));
            toolTip1.SetToolTip(pictureBox_DisableDefaultOptimizations, International.GetText("Form_Settings_ToolTip_DisableDefaultOptimizations"));

            // internet connection mining check
            toolTip1.SetToolTip(checkBox_IdleWhenNoInternetAccess, International.GetText("Form_Settings_ToolTip_ContinueMiningIfNoInternetAccess"));
            toolTip1.SetToolTip(pictureBox_IdleWhenNoInternetAccess, International.GetText("Form_Settings_ToolTip_ContinueMiningIfNoInternetAccess"));

            toolTip1.SetToolTip(pictureBox_SwitchProfitabilityThreshold, International.GetText("Form_Settings_ToolTip_SwitchProfitabilityThreshold"));
            toolTip1.SetToolTip(label_SwitchProfitabilityThreshold, International.GetText("Form_Settings_ToolTip_SwitchProfitabilityThreshold"));

            toolTip1.SetToolTip(pictureBox_MinimizeMiningWindows, International.GetText("Form_Settings_ToolTip_MinimizeMiningWindows"));
            toolTip1.SetToolTip(checkBox_MinimizeMiningWindows, International.GetText("Form_Settings_ToolTip_MinimizeMiningWindows"));

            Text = International.GetText("Form_Settings_Title");

            algorithmSettingsControl1.InitLocale(toolTip1);
        }

        /// <summary>
        /// The InitializeFormTranslations
        /// </summary>
        private void InitializeFormTranslations()
        {
            buttonDefaults.Text = International.GetText("Form_Settings_buttonDefaultsText");
            buttonSaveClose.Text = International.GetText("Form_Settings_buttonSaveText");
            buttonCloseNoSave.Text = International.GetText("Form_Settings_buttonCloseNoSaveText");
        }

        /// <summary>
        /// The InitializeGeneralTabTranslations
        /// </summary>
        private void InitializeGeneralTabTranslations()
        {
            checkBox_DebugConsole.Text = International.GetText("Form_Settings_General_DebugConsole");
            checkBox_AutoStartMining.Text = International.GetText("Form_Settings_General_AutoStartMining");
            checkBox_HideMiningWindows.Text = International.GetText("Form_Settings_General_HideMiningWindows");
            checkBox_MinimizeToTray.Text = International.GetText("Form_Settings_General_MinimizeToTray");
            checkBox_DisableDetectionNVIDIA.Text = string.Format(International.GetText("Form_Settings_General_DisableDetection"), "NVIDIA");
            checkBox_DisableDetectionAMD.Text = string.Format(International.GetText("Form_Settings_General_DisableDetection"), "AMD");
            checkBox_AutoScaleBTCValues.Text = International.GetText("Form_Settings_General_AutoScaleBTCValues");
            checkBox_StartMiningWhenIdle.Text = International.GetText("Form_Settings_General_StartMiningWhenIdle");
            checkBox_ShowDriverVersionWarning.Text = International.GetText("Form_Settings_General_ShowDriverVersionWarning");
            checkBox_DisableWindowsErrorReporting.Text = International.GetText("Form_Settings_General_DisableWindowsErrorReporting");
            checkBox_ShowInternetConnectionWarning.Text = International.GetText("Form_Settings_General_ShowInternetConnectionWarning");
            checkBox_Use3rdPartyMiners.Text = International.GetText("Form_Settings_General_3rdparty_Text");
            checkBox_NVIDIAP0State.Text = International.GetText("Form_Settings_General_NVIDIAP0State");
            checkBox_LogToFile.Text = International.GetText("Form_Settings_General_LogToFile");
            checkBox_AMD_DisableAMDTempControl.Text = International.GetText("Form_Settings_General_DisableAMDTempControl");
            checkBox_AllowMultipleInstances.Text = International.GetText("Form_Settings_General_AllowMultipleInstances_Text");
            checkBox_RunAtStartup.Text = International.GetText("Form_Settings_General_RunAtStartup");
            checkBox_MinimizeMiningWindows.Text = International.GetText("Form_Settings_General_MinimizeMiningWindows");
            checkBox_RunScriptOnCUDA_GPU_Lost.Text = International.GetText("Form_Settings_General_RunScriptOnCUDA_GPU_Lost");

            checkbox_Group_same_devices.Text = "Group Like Cards";
            label_Language.Text = International.GetText("Form_Settings_General_Language") + ":";
            label_MinIdleSeconds.Text = International.GetText("Form_Settings_General_MinIdleSeconds") + ":";
            label_MinerRestartDelayMS.Text = International.GetText("Form_Settings_General_MinerRestartDelayMS") + ":";
            label_MinerAPIQueryInterval.Text = International.GetText("Form_Settings_General_MinerAPIQueryInterval") + ":";
            label_LogMaxFileSize.Text = International.GetText("Form_Settings_General_LogMaxFileSize") + ":";

            label_SwitchMinSecondsFixed.Text = International.GetText("Form_Settings_General_SwitchMinSecondsFixed") + ":";
            label_SwitchMinSecondsDynamic.Text = International.GetText("Form_Settings_General_SwitchMinSecondsDynamic") + ":";
            label_SwitchMinSecondsAMD.Text = International.GetText("Form_Settings_General_SwitchMinSecondsAMD") + ":";

            label_ethminerDefaultBlockHeight.Text = International.GetText("Form_Settings_General_ethminerDefaultBlockHeight") + ":";
            label_DagGeneration.Text = International.GetText("Form_Settings_DagGeneration") + ":";
            label_APIBindPortStart.Text = International.GetText("Form_Settings_APIBindPortStart") + ":";

            label_MinProfit.Text = International.GetText("Form_Settings_General_MinimumProfit") + ":";

            label_displayCurrency.Text = International.GetText("Form_Settings_DisplayCurrency");

            // device enabled listview translation
            devicesListViewEnableControl1.InitLocale();
            algorithmsListView1.InitLocale();

            // Setup Tooltips CPU
            label_CPU0_ForceCPUExtension.Text = International.GetText("Form_Settings_General_CPU_ForceCPUExtension") + ":";
            // new translations
            tabControlGeneral.TabPages[0].Text = International.GetText("FormSettings_Tab_General");
            tabControlGeneral.TabPages[1].Text = International.GetText("FormSettings_Tab_Advanced");
            tabControlGeneral.TabPages[2].Text = International.GetText("FormSettings_Tab_Devices_Algorithms");
            groupBox_Main.Text = International.GetText("FormSettings_Tab_General_Group_Main");
            groupBox_Localization.Text = International.GetText("FormSettings_Tab_General_Group_Localization");
            groupBox_Logging.Text = International.GetText("FormSettings_Tab_General_Group_Logging");
            groupBox_Misc.Text = International.GetText("FormSettings_Tab_General_Group_Misc");
            // advanced
            groupBox_Miners.Text = International.GetText("FormSettings_Tab_Advanced_Group_Miners");

            // buttonAllProfit.Text = International.GetText("FormSettings_Tab_Devices_Algorithms_Check_ALLProfitability");
            // buttonSelectedProfit.Text = International.GetText("FormSettings_Tab_Devices_Algorithms_Check_SingleProfitability");

            checkBox_DisableDefaultOptimizations.Text = International.GetText("Form_Settings_Text_DisableDefaultOptimizations");
            checkBox_IdleWhenNoInternetAccess.Text = International.GetText("Form_Settings_Text_ContinueMiningIfNoInternetAccess");

            label_SwitchProfitabilityThreshold.Text = International.GetText("Form_Settings_General_SwitchProfitabilityThreshold");
        }

        /// <summary>
        /// The InitializeGeneralTabCallbacks
        /// </summary>
        private void InitializeGeneralTabCallbacks()
        {
            // Add EventHandler for all the general tab's checkboxes
            {
                checkBox_AutoScaleBTCValues.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_DisableDetectionAMD.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_DisableDetectionNVIDIA.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_MinimizeToTray.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_HideMiningWindows.CheckedChanged += new EventHandler(CheckBox_HideMiningWindows_CheckChanged);
                checkBox_DebugConsole.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_ShowDriverVersionWarning.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_DisableWindowsErrorReporting.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_ShowInternetConnectionWarning.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_StartMiningWhenIdle.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_NVIDIAP0State.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_LogToFile.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                /*checkBox_HashRefinery.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_NiceHash.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_AhashPool.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_zpool.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);*/
                checkBox_zerg.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);/*
                checkBox_minemoney.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_MPH.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_devapi.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_starpool.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_blockmunch.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_blazepool.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);*/
                checkBox_AutoStartMining.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_AllowMultipleInstances.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_MinimizeMiningWindows.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkBox_RunScriptOnCUDA_GPU_Lost.CheckedChanged += new EventHandler(GeneralCheckBoxes_CheckedChanged);
                checkbox_Group_same_devices.CheckedChanged += GeneralCheckBoxes_CheckedChanged;
            }
            // Add EventHandler for all the general tab's textboxes
            {
                /*textBox_Zpool_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_Zpool_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_AhashPool_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_AhashPool_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_HashRefinery_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_hashrefinery_worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_NiceHash_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_NiceHash_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);*/
                textBox_zerg_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_zerg_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);/*
                textBox_minemoney_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_minemoney_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_starpool_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_starpool_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_blockmunch_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_blockmunch_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_blazepool_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);*/
                textBox_averaging.Leave += new EventHandler(GeneralTextBoxes_Leave);
                // textBox_blazepool_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                // textBox_MPH_Wallet.Leave += new EventHandler(GeneralTextBoxes_Leave);
                // textBox_MPH_Worker.Leave += new EventHandler(GeneralTextBoxes_Leave);
                // these are ints only
                textBox_SwitchMinSecondsFixed.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_SwitchMinSecondsDynamic.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_SwitchMinSecondsAMD.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_MinerAPIQueryInterval.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_MinerRestartDelayMS.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_MinIdleSeconds.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_LogMaxFileSize.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_ethminerDefaultBlockHeight.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_APIBindPortStart.Leave += new EventHandler(GeneralTextBoxes_Leave);
                textBox_MinProfit.Leave += new EventHandler(GeneralTextBoxes_Leave);
                // set int only keypress
                textBox_SwitchMinSecondsFixed.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_SwitchMinSecondsDynamic.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_SwitchMinSecondsAMD.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_MinerAPIQueryInterval.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_MinerRestartDelayMS.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_MinIdleSeconds.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_LogMaxFileSize.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_ethminerDefaultBlockHeight.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                textBox_APIBindPortStart.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
                // set double only keypress
                textBox_MinProfit.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxDoubleOnly_KeyPress);
            }
            // Add EventHandler for all the general tab's textboxes
            {
                comboBox_Language.Leave += new EventHandler(GeneralComboBoxes_Leave);
                comboBox_TimeUnit.Leave += new EventHandler(GeneralComboBoxes_Leave);
                comboBox_DagLoadMode.Leave += new EventHandler(GeneralComboBoxes_Leave);
            }

            // CPU exceptions
            comboBox_CPU0_ForceCPUExtension.SelectedIndex = (int)ConfigManager.GeneralConfig.ForceCPUExtension;
            comboBox_CPU0_ForceCPUExtension.SelectedIndexChanged += ComboBox_CPU0_ForceCPUExtension_SelectedIndexChanged;
            // fill dag dropdown
            comboBox_DagLoadMode.Items.Clear();

            for (int i = 0; i < (int)DagGenerationType.END; ++i)
            {
                comboBox_DagLoadMode.Items.Add(MinerEtherum.GetDagGenerationString((DagGenerationType)i));
            }

            // set selected
            comboBox_DagLoadMode.SelectedIndex = (int)ConfigManager.GeneralConfig.EthminerDagGenerationType;
        }

        /// <summary>
        /// The InitializeGeneralTabFieldValuesReferences
        /// </summary>
        private void InitializeGeneralTabFieldValuesReferences()
        {
            // Checkboxes set checked value
            {
                checkBox_DebugConsole.Checked = ConfigManager.GeneralConfig.DebugConsole;
                checkBox_AutoStartMining.Checked = ConfigManager.GeneralConfig.AutoStartMining;
                /*checkBox_zpool.Checked = ConfigManager.GeneralConfig.zpoolenabled;
                checkBox_AhashPool.Checked = ConfigManager.GeneralConfig.ahashenabled;*/
                checkBox_zerg.Checked = ConfigManager.GeneralConfig.zergenabled;/*
                checkBox_minemoney.Checked = ConfigManager.GeneralConfig.minemoneyenabled;
                checkBox_starpool.Checked = ConfigManager.GeneralConfig.starpoolenabled;
                checkBox_blockmunch.Checked = ConfigManager.GeneralConfig.blockmunchenabled;
                checkBox_blazepool.Checked = ConfigManager.GeneralConfig.blazepoolenabled;
                checkBox_MPH.Checked = ConfigManager.GeneralConfig.MPHenabled;
                checkBox_NiceHash.Checked = ConfigManager.GeneralConfig.nicehashenabled;
                checkBox_HashRefinery.Checked = ConfigManager.GeneralConfig.hashrefineryenabled;
                checkBox_devapi.Checked = ConfigManager.GeneralConfig.devapi;*/
                checkBox_HideMiningWindows.Checked = ConfigManager.GeneralConfig.HideMiningWindows;
                checkBox_MinimizeToTray.Checked = ConfigManager.GeneralConfig.MinimizeToTray;
                checkBox_DisableDetectionNVIDIA.Checked = ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionNVIDIA;
                checkBox_DisableDetectionAMD.Checked = ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionAMD;
                checkBox_AutoScaleBTCValues.Checked = ConfigManager.GeneralConfig.AutoScaleBTCValues;
                checkBox_StartMiningWhenIdle.Checked = ConfigManager.GeneralConfig.StartMiningWhenIdle;
                checkBox_ShowDriverVersionWarning.Checked = ConfigManager.GeneralConfig.ShowDriverVersionWarning;
                checkBox_DisableWindowsErrorReporting.Checked = ConfigManager.GeneralConfig.DisableWindowsErrorReporting;
                checkBox_ShowInternetConnectionWarning.Checked = ConfigManager.GeneralConfig.ShowInternetConnectionWarning;
                checkBox_NVIDIAP0State.Checked = ConfigManager.GeneralConfig.NVIDIAP0State;
                checkBox_LogToFile.Checked = ConfigManager.GeneralConfig.LogToFile;
                checkBox_AMD_DisableAMDTempControl.Checked = ConfigManager.GeneralConfig.DisableAMDTempControl;
                checkBox_DisableDefaultOptimizations.Checked = ConfigManager.GeneralConfig.DisableDefaultOptimizations;
                checkBox_IdleWhenNoInternetAccess.Checked = ConfigManager.GeneralConfig.IdleWhenNoInternetAccess;
                checkBox_Use3rdPartyMiners.Checked = ConfigManager.GeneralConfig.Use3rdPartyMiners == Use3rdPartyMiners.YES;
                checkBox_AllowMultipleInstances.Checked = ConfigManager.GeneralConfig.AllowMultipleInstances;
                checkBox_RunAtStartup.Checked = IsInStartupRegistry();
                checkBox_MinimizeMiningWindows.Checked = ConfigManager.GeneralConfig.MinimizeMiningWindows;
                checkBox_MinimizeMiningWindows.Enabled = !ConfigManager.GeneralConfig.HideMiningWindows;
                checkBox_RunScriptOnCUDA_GPU_Lost.Checked = ConfigManager.GeneralConfig.RunScriptOnCUDA_GPU_Lost;
                checkbox_Group_same_devices.Checked = ConfigManager.GeneralConfig.Group_same_devices;
            }
            // Textboxes
            {
                /*textBox_Zpool_Wallet.Text = ConfigManager.GeneralConfig.zpoolAddress;
                textBox_Zpool_Worker.Text = ConfigManager.GeneralConfig.zpoolWorkerName;
                textBox_AhashPool_Wallet.Text = ConfigManager.GeneralConfig.ahashAddress;
                textBox_AhashPool_Worker.Text = ConfigManager.GeneralConfig.ahashWorkerName;
                textBox_HashRefinery_Wallet.Text = ConfigManager.GeneralConfig.hashrefineryAddress;
                textBox_hashrefinery_worker.Text = ConfigManager.GeneralConfig.hashrefineryWorkerName;
                textBox_NiceHash_Wallet.Text = ConfigManager.GeneralConfig.nicehashAddress;
                textBox_NiceHash_Worker.Text = ConfigManager.GeneralConfig.nicehashWorkerName;*/
                textBox_zerg_Wallet.Text = ConfigManager.GeneralConfig.zergAddress;
                textBox_zerg_Worker.Text = ConfigManager.GeneralConfig.zergWorkerName;
                /*textBox_MPH_Wallet.Text = ConfigManager.GeneralConfig.MPHAddress;
                textBox_MPH_Worker.Text = ConfigManager.GeneralConfig.MPHWorkerName;
                textBox_minemoney_Wallet.Text = ConfigManager.GeneralConfig.minemoneyAddress;
                textBox_minemoney_Worker.Text = ConfigManager.GeneralConfig.minemoneyWorkerName;
                textBox_starpool_Wallet.Text = ConfigManager.GeneralConfig.starpoolAddress;
                textBox_starpool_Worker.Text = ConfigManager.GeneralConfig.starpoolWorkerName;
                textBox_blockmunch_Wallet.Text = ConfigManager.GeneralConfig.blockmunchAddress;
                textBox_blockmunch_Worker.Text = ConfigManager.GeneralConfig.blockmunchWorkerName;
                textBox_blazepool_Wallet.Text = ConfigManager.GeneralConfig.blazepoolAddress;

                textBox_blazepool_Worker.Text = ConfigManager.GeneralConfig.blazepoolWorkerName;
                textBox_minemoney_Wallet.Text = ConfigManager.GeneralConfig.minemoneyAddress;
                textBox_minemoney_Worker.Text = ConfigManager.GeneralConfig.minemoneyWorkerName;*/
                textBox_averaging.Text = ConfigManager.GeneralConfig.Averaging;
                textBox_SwitchMinSecondsFixed.Text = ConfigManager.GeneralConfig.SwitchMinSecondsFixed.ToString();
                textBox_SwitchMinSecondsDynamic.Text = ConfigManager.GeneralConfig.SwitchMinSecondsDynamic.ToString();
                textBox_SwitchMinSecondsAMD.Text = ConfigManager.GeneralConfig.SwitchMinSecondsAMD.ToString();
                textBox_MinerAPIQueryInterval.Text = ConfigManager.GeneralConfig.MinerAPIQueryInterval.ToString();
                textBox_MinerRestartDelayMS.Text = ConfigManager.GeneralConfig.MinerRestartDelayMS.ToString();
                textBox_MinIdleSeconds.Text = ConfigManager.GeneralConfig.MinIdleSeconds.ToString();
                textBox_LogMaxFileSize.Text = ConfigManager.GeneralConfig.LogMaxFileSize.ToString();
                textBox_ethminerDefaultBlockHeight.Text = ConfigManager.GeneralConfig.ethminerDefaultBlockHeight.ToString();
                textBox_APIBindPortStart.Text = ConfigManager.GeneralConfig.ApiBindPortPoolStart.ToString();
                textBox_MinProfit.Text = ConfigManager.GeneralConfig.MinimumProfit.ToString("F2").Replace(',', '.'); // force comma;
                textBox_SwitchProfitabilityThreshold.Text = ConfigManager.GeneralConfig.SwitchProfitabilityThreshold.ToString("F2").Replace(',', '.'); // force comma;
            }
            // set custom control referances
            {
                // here we want all devices
                devicesListViewEnableControl1.SetComputeDevices(ComputeDeviceManager.Available.AllAvaliableDevices);
                devicesListViewEnableControl1.SetAlgorithmsListView(algorithmsListView1);
                devicesListViewEnableControl1.IsSettingsCopyEnabled = true;
            }
            // Add language selections list
            {
                var lang = International.GetAvailableLanguages();

                comboBox_Language.Items.Clear();

                for (int i = 0; i < lang.Count; i++)
                    comboBox_Language.Items.Add(lang[(LanguageType)i]);
            }
            // Add time unit selection list
            {
                var timeunits = new Dictionary<TimeUnitType, string>();

                foreach (TimeUnitType timeunit in Enum.GetValues(typeof(TimeUnitType)))
                {
                    timeunits.Add(timeunit, International.GetText(timeunit.ToString()));
                    comboBox_TimeUnit.Items.Add(timeunits[timeunit]);
                }
            }
            // ComboBox
            {
                comboBox_Language.SelectedIndex = (int)ConfigManager.GeneralConfig.Language;
                comboBox_TimeUnit.SelectedItem = International.GetText(ConfigManager.GeneralConfig.TimeUnit.ToString());
                currencyConverterCombobox.SelectedItem = ConfigManager.GeneralConfig.DisplayCurrency;
            }
        }

        /// <summary>
        /// The InitializeGeneralTab
        /// </summary>
        private void InitializeGeneralTab()
        {
            InitializeGeneralTabTranslations();
            InitializeGeneralTabCallbacks();
            InitializeGeneralTabFieldValuesReferences();
        }

        /// <summary>
        /// The InitializeDevicesTab
        /// </summary>
        private void InitializeDevicesTab() => InitializeDevicesCallbacks();

        /// <summary>
        /// The InitializeDevicesCallbacks
        /// </summary>
        private void InitializeDevicesCallbacks()
        {
            devicesListViewEnableControl1.SetDeviceSelectionChangedCallback(DevicesListView1_ItemSelectionChanged);
        }

        /// <summary>
        /// The GeneralCheckBoxes_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void GeneralCheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;
            // indicate there has been a change
            IsChange = true;
            ConfigManager.GeneralConfig.DebugConsole = checkBox_DebugConsole.Checked;
            ConfigManager.GeneralConfig.AutoStartMining = checkBox_AutoStartMining.Checked;
            /*ConfigManager.GeneralConfig.zpoolenabled = checkBox_zpool.Checked;
            ConfigManager.GeneralConfig.ahashenabled = checkBox_AhashPool.Checked;*/
            ConfigManager.GeneralConfig.zergenabled = checkBox_zerg.Checked;/*
            ConfigManager.GeneralConfig.MPHenabled = checkBox_MPH.Checked;
            ConfigManager.GeneralConfig.minemoneyenabled = checkBox_minemoney.Checked;
            ConfigManager.GeneralConfig.starpoolenabled = checkBox_starpool.Checked;
            ConfigManager.GeneralConfig.blockmunchenabled = checkBox_blockmunch.Checked;
            ConfigManager.GeneralConfig.blazepoolenabled = checkBox_blazepool.Checked;
            ConfigManager.GeneralConfig.nicehashenabled = checkBox_NiceHash.Checked;
            ConfigManager.GeneralConfig.hashrefineryenabled = checkBox_HashRefinery.Checked;
            ConfigManager.GeneralConfig.devapi = checkBox_devapi.Checked;*/
            ConfigManager.GeneralConfig.HideMiningWindows = checkBox_HideMiningWindows.Checked;
            ConfigManager.GeneralConfig.MinimizeToTray = checkBox_MinimizeToTray.Checked;
            ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionNVIDIA = checkBox_DisableDetectionNVIDIA.Checked;
            ConfigManager.GeneralConfig.DeviceDetection.DisableDetectionAMD = checkBox_DisableDetectionAMD.Checked;
            ConfigManager.GeneralConfig.AutoScaleBTCValues = checkBox_AutoScaleBTCValues.Checked;
            ConfigManager.GeneralConfig.StartMiningWhenIdle = checkBox_StartMiningWhenIdle.Checked;
            ConfigManager.GeneralConfig.ShowDriverVersionWarning = checkBox_ShowDriverVersionWarning.Checked;
            ConfigManager.GeneralConfig.DisableWindowsErrorReporting = checkBox_DisableWindowsErrorReporting.Checked;
            ConfigManager.GeneralConfig.ShowInternetConnectionWarning = checkBox_ShowInternetConnectionWarning.Checked;
            ConfigManager.GeneralConfig.NVIDIAP0State = checkBox_NVIDIAP0State.Checked;
            ConfigManager.GeneralConfig.LogToFile = checkBox_LogToFile.Checked;
            ConfigManager.GeneralConfig.IdleWhenNoInternetAccess = checkBox_IdleWhenNoInternetAccess.Checked;
            ConfigManager.GeneralConfig.AllowMultipleInstances = checkBox_AllowMultipleInstances.Checked;
            ConfigManager.GeneralConfig.MinimizeMiningWindows = checkBox_MinimizeMiningWindows.Checked;
            ConfigManager.GeneralConfig.RunScriptOnCUDA_GPU_Lost = checkBox_RunScriptOnCUDA_GPU_Lost.Checked;
            ConfigManager.GeneralConfig.Group_same_devices = checkbox_Group_same_devices.Checked;
        }

        /// <summary>
        /// The CheckBox_AMD_DisableAMDTempControl_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CheckBox_AMD_DisableAMDTempControl_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;

            // indicate there has been a change
            IsChange = true;
            ConfigManager.GeneralConfig.DisableAMDTempControl = checkBox_AMD_DisableAMDTempControl.Checked;

            foreach (var cDev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                if (cDev.DeviceType == DeviceType.AMD)
                {
                    foreach (var algorithm in cDev.GetAlgorithmSettings())
                    {
                        if (algorithm.CryptoMiner937ID != AlgorithmType.DaggerHashimoto)
                        {
                            algorithm.ExtraLaunchParameters += AmdGpuDevice.TemperatureParam;

                            algorithm.ExtraLaunchParameters = ExtraLaunchParametersParser.ParseForMiningPair(
                                new MiningPair(cDev, algorithm), algorithm.CryptoMiner937ID, DeviceType.AMD, false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The CheckBox_DisableDefaultOptimizations_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CheckBox_DisableDefaultOptimizations_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;

            // indicate there has been a change
            IsChange = true;
            ConfigManager.GeneralConfig.DisableDefaultOptimizations = checkBox_DisableDefaultOptimizations.Checked;

            if (ConfigManager.GeneralConfig.DisableDefaultOptimizations)
            {
                foreach (var cDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                {
                    foreach (var algorithm in cDev.GetAlgorithmSettings())
                    {
                        algorithm.ExtraLaunchParameters = "";

                        if (cDev.DeviceType == DeviceType.AMD && algorithm.CryptoMiner937ID != AlgorithmType.DaggerHashimoto)
                        {
                            algorithm.ExtraLaunchParameters += AmdGpuDevice.TemperatureParam;

                            algorithm.ExtraLaunchParameters = ExtraLaunchParametersParser.ParseForMiningPair(
                                new MiningPair(cDev, algorithm), algorithm.CryptoMiner937ID, cDev.DeviceType, false);
                        }
                    }
                }
            }
            else
            {
                foreach (var cDev in ComputeDeviceManager.Available.AllAvaliableDevices)
                {
                    if (cDev.DeviceType == DeviceType.CPU) continue; // cpu has no defaults
                    var deviceDefaultsAlgoSettings = GroupAlgorithms.CreateForDeviceList(cDev);

                    foreach (var defaultAlgoSettings in deviceDefaultsAlgoSettings)
                    {
                        var toSetAlgo = cDev.GetAlgorithm(defaultAlgoSettings.MinerBaseType, defaultAlgoSettings.CryptoMiner937ID, defaultAlgoSettings.SecondaryCryptoMiner937ID);

                        if (toSetAlgo != null)
                        {
                            toSetAlgo.ExtraLaunchParameters = defaultAlgoSettings.ExtraLaunchParameters;

                            toSetAlgo.ExtraLaunchParameters = ExtraLaunchParametersParser.ParseForMiningPair(
                                new MiningPair(cDev, toSetAlgo), toSetAlgo.CryptoMiner937ID, cDev.DeviceType, false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The CheckBox_RunAtStartup_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CheckBox_RunAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            isStartupChanged = true;
        }

        /// <summary>
        /// The IsInStartupRegistry
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsInStartupRegistry()
        {
            // Value is stored in registry
            var startVal = "";

            try
            {
                startVal = (string)rkStartup.GetValue(Application.ProductName, "");
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint("REGISTRY", e.ToString());
            }

            return startVal == Application.ExecutablePath;
        }

        /// <summary>
        /// The GeneralTextBoxes_Leave
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void GeneralTextBoxes_Leave(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;
            IsChange = true;
            /*if (ConfigManager.GeneralConfig.zpoolAddress != textBox_Zpool_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.zpoolAddress = textBox_Zpool_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.ahashAddress != textBox_AhashPool_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.ahashAddress = textBox_AhashPool_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.hashrefineryAddress != textBox_HashRefinery_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.hashrefineryAddress = textBox_HashRefinery_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.nicehashAddress != textBox_NiceHash_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.nicehashAddress = textBox_NiceHash_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.zpoolWorkerName != textBox_Zpool_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.zpoolWorkerName = textBox_Zpool_Worker.Text.Trim();
            if (ConfigManager.GeneralConfig.ahashWorkerName != textBox_AhashPool_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.ahashWorkerName = textBox_AhashPool_Worker.Text.Trim();
            if (ConfigManager.GeneralConfig.hashrefineryWorkerName != textBox_hashrefinery_worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.hashrefineryWorkerName = textBox_hashrefinery_worker.Text.Trim();
            if (ConfigManager.GeneralConfig.nicehashWorkerName != textBox_NiceHash_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.nicehashWorkerName = textBox_NiceHash_Worker.Text.Trim();
            if (ConfigManager.GeneralConfig.minemoneyAddress != textBox_minemoney_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.minemoneyAddress = textBox_minemoney_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.minemoneyWorkerName != textBox_minemoney_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.minemoneyWorkerName = textBox_minemoney_Worker.Text.Trim();
            if (ConfigManager.GeneralConfig.starpoolAddress != textBox_starpool_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.starpoolAddress = textBox_starpool_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.starpoolWorkerName != textBox_starpool_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.starpoolWorkerName = textBox_starpool_Worker.Text.Trim();
            if (ConfigManager.GeneralConfig.blockmunchAddress != textBox_blockmunch_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.blockmunchAddress = textBox_blockmunch_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.blockmunchWorkerName != textBox_blockmunch_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.blockmunchWorkerName = textBox_blockmunch_Worker.Text.Trim();
            if (ConfigManager.GeneralConfig.blazepoolAddress != textBox_blazepool_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.blazepoolAddress = textBox_blazepool_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.blazepoolWorkerName != textBox_blazepool_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.blazepoolWorkerName = textBox_blazepool_Worker.Text.Trim();*/
            if (ConfigManager.GeneralConfig.zergAddress != textBox_zerg_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.zergAddress = textBox_zerg_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.zergWorkerName != textBox_zerg_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.zergWorkerName = textBox_zerg_Worker.Text.Trim();/*
            if (ConfigManager.GeneralConfig.MPHAddress != textBox_MPH_Wallet.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.MPHAddress = textBox_MPH_Wallet.Text.Trim();
            if (ConfigManager.GeneralConfig.MPHWorkerName != textBox_MPH_Worker.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.MPHWorkerName = textBox_MPH_Worker.Text.Trim();*/
            if (ConfigManager.GeneralConfig.Averaging != textBox_averaging.Text.Trim()) isCredChange = true;
            ConfigManager.GeneralConfig.Averaging = textBox_averaging.Text.Trim();

            ConfigManager.GeneralConfig.SwitchMinSecondsFixed = Helpers.ParseInt(textBox_SwitchMinSecondsFixed.Text);
            ConfigManager.GeneralConfig.SwitchMinSecondsDynamic = Helpers.ParseInt(textBox_SwitchMinSecondsDynamic.Text);
            ConfigManager.GeneralConfig.SwitchMinSecondsAMD = Helpers.ParseInt(textBox_SwitchMinSecondsAMD.Text);
            ConfigManager.GeneralConfig.MinerAPIQueryInterval = Helpers.ParseInt(textBox_MinerAPIQueryInterval.Text);
            ConfigManager.GeneralConfig.MinerRestartDelayMS = Helpers.ParseInt(textBox_MinerRestartDelayMS.Text);
            ConfigManager.GeneralConfig.MinIdleSeconds = Helpers.ParseInt(textBox_MinIdleSeconds.Text);
            ConfigManager.GeneralConfig.LogMaxFileSize = Helpers.ParseLong(textBox_LogMaxFileSize.Text);
            ConfigManager.GeneralConfig.ethminerDefaultBlockHeight = Helpers.ParseInt(textBox_ethminerDefaultBlockHeight.Text);
            ConfigManager.GeneralConfig.ApiBindPortPoolStart = Helpers.ParseInt(textBox_APIBindPortStart.Text);
            // min profit
            ConfigManager.GeneralConfig.MinimumProfit = Helpers.ParseDouble(textBox_MinProfit.Text);
            ConfigManager.GeneralConfig.SwitchProfitabilityThreshold = Helpers.ParseDouble(textBox_SwitchProfitabilityThreshold.Text);

            // Fix bounds
            ConfigManager.GeneralConfig.FixSettingBounds();
            // update strings
            textBox_MinProfit.Text = ConfigManager.GeneralConfig.MinimumProfit.ToString("F2").Replace(',', '.'); // force comma
            textBox_SwitchProfitabilityThreshold.Text = ConfigManager.GeneralConfig.SwitchProfitabilityThreshold.ToString("F2").Replace(',', '.'); // force comma
            textBox_SwitchMinSecondsFixed.Text = ConfigManager.GeneralConfig.SwitchMinSecondsFixed.ToString();
            textBox_SwitchMinSecondsDynamic.Text = ConfigManager.GeneralConfig.SwitchMinSecondsDynamic.ToString();
            textBox_SwitchMinSecondsAMD.Text = ConfigManager.GeneralConfig.SwitchMinSecondsAMD.ToString();
            textBox_MinerAPIQueryInterval.Text = ConfigManager.GeneralConfig.MinerAPIQueryInterval.ToString();
            textBox_MinerRestartDelayMS.Text = ConfigManager.GeneralConfig.MinerRestartDelayMS.ToString();
            textBox_MinIdleSeconds.Text = ConfigManager.GeneralConfig.MinIdleSeconds.ToString();
            textBox_LogMaxFileSize.Text = ConfigManager.GeneralConfig.LogMaxFileSize.ToString();
            textBox_ethminerDefaultBlockHeight.Text = ConfigManager.GeneralConfig.ethminerDefaultBlockHeight.ToString();
            textBox_APIBindPortStart.Text = ConfigManager.GeneralConfig.ApiBindPortPoolStart.ToString();
        }

        /// <summary>
        /// The GeneralComboBoxes_Leave
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void GeneralComboBoxes_Leave(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;
            IsChange = true;
            ConfigManager.GeneralConfig.Language = (LanguageType)comboBox_Language.SelectedIndex;
            ConfigManager.GeneralConfig.TimeUnit = (TimeUnitType)comboBox_TimeUnit.SelectedIndex;
            ConfigManager.GeneralConfig.EthminerDagGenerationType = (DagGenerationType)comboBox_DagLoadMode.SelectedIndex;
        }

        /// <summary>
        /// The ComboBox_CPU0_ForceCPUExtension_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ComboBox_CPU0_ForceCPUExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmbbox = (ComboBox)sender;
            ConfigManager.GeneralConfig.ForceCPUExtension = (CPUExtensionType)cmbbox.SelectedIndex;
        }

        /// <summary>
        /// The DevicesListView1_ItemSelectionChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="ListViewItemSelectionChangedEventArgs"/></param>
        private void DevicesListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            algorithmSettingsControl1.Deselect();
            // show algorithms
            _selectedComputeDevice = ComputeDeviceManager.Available.GetCurrentlySelectedComputeDevice(e.ItemIndex, ShowUniqueDeviceList);
            algorithmsListView1.SetAlgorithms(_selectedComputeDevice, _selectedComputeDevice.Enabled);
            groupBoxAlgorithmSettings.Text = string.Format(International.GetText("FormSettings_AlgorithmsSettings"), _selectedComputeDevice.Name);
        }

        /// <summary>
        /// The ButtonSelectedProfit_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonSelectedProfit_Click(object sender, EventArgs e)
        {
            if (_selectedComputeDevice == null)
            {
                MessageBox.Show(International.GetText("FormSettings_ButtonProfitSingle"),
                                International.GetText("Warning_with_Exclamation"),
                                MessageBoxButtons.OK);

                return;
            }

            var url = Links.NHM_Profit_Check + _selectedComputeDevice.Name;

            foreach (var algorithm in _selectedComputeDevice.GetAlgorithmSettingsFastest())
            {
                var id = (int)algorithm.CryptoMiner937ID;
            }
            // url += "&speed" + id + "=" + ProfitabilityCalculator.GetFormatedSpeed(algorithm.BenchmarkSpeed, algorithm.CryptoMiner937ID).ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

            url += "&nhmver=" + Application.ProductVersion.ToString();  // Add version info
            url += "&cost=1&power=1"; // Set default power and cost to 1
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// The ButtonAllProfit_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonAllProfit_Click(object sender, EventArgs e)
        {
            var url = Links.NHM_Profit_Check + "CUSTOM";
            var total = new Dictionary<AlgorithmType, double>();

            foreach (var curCDev in ComputeDeviceManager.Available.AllAvaliableDevices)
            {
                foreach (var algorithm in curCDev.GetAlgorithmSettingsFastest())
                {
                    if (total.ContainsKey(algorithm.CryptoMiner937ID))
                    {
                        total[algorithm.CryptoMiner937ID] += algorithm.BenchmarkSpeed;
                    }
                    else
                    {
                        total[algorithm.CryptoMiner937ID] = algorithm.BenchmarkSpeed;
                    }
                }
            }

            foreach (var algorithm in total)
            {
                var id = (int)algorithm.Key;
            }
            // url += "&speed" + id + "=" + ProfitabilityCalculator.GetFormatedSpeed(algorithm.Value, algorithm.Key).ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

            url += "&nhmver=" + Application.ProductVersion.ToString();  // Add version info
            url += "&cost=1&power=1"; // Set default power and cost to 1
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// The ToolTip1_Popup
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="PopupEventArgs"/></param>
        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.ToolTipTitle = International.GetText("Form_Settings_ToolTip_Explaination");
        }

        /// <summary>
        /// The ButtonDefaults_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonDefaults_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(International.GetText("Form_Settings_buttonDefaultsMsg"),
                                                  International.GetText("Form_Settings_buttonDefaultsTitle"),
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                IsChange = true;
                IsChangeSaved = true;
                ConfigManager.GeneralConfig.SetDefaults();

                International.Initialize(ConfigManager.GeneralConfig.Language);
                InitializeGeneralTabFieldValuesReferences();
                InitializeGeneralTabTranslations();
            }
        }

        /// <summary>
        /// The ButtonSaveClose_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonSaveClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show(International.GetText("Form_Settings_buttonSaveMsg"),
                            International.GetText("Form_Settings_buttonSaveTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            IsChange = true;
            IsChangeSaved = true;

            if (isCredChange)
            {
                /*CryptoStats.SetCredentials(ConfigManager.GeneralConfig.BitcoinAddress.Trim(), ConfigManager.GeneralConfig.WorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.zpoolAddress.Trim(), ConfigManager.GeneralConfig.zpoolWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.ahashAddress.Trim(), ConfigManager.GeneralConfig.ahashWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.hashrefineryAddress.Trim(), ConfigManager.GeneralConfig.hashrefineryWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.nicehashAddress.Trim(), ConfigManager.GeneralConfig.nicehashWorkerName.Trim());*/
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.zergAddress.Trim(), ConfigManager.GeneralConfig.zergWorkerName.Trim());/*
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.minemoneyAddress.Trim(), ConfigManager.GeneralConfig.minemoneyWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.starpoolAddress.Trim(), ConfigManager.GeneralConfig.starpoolWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.blockmunchAddress.Trim(), ConfigManager.GeneralConfig.blockmunchWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.blazepoolAddress.Trim(), ConfigManager.GeneralConfig.blazepoolWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.MPHAddress.Trim(), ConfigManager.GeneralConfig.MPHWorkerName.Trim());
                CryptoStats.SetCredentials(ConfigManager.GeneralConfig.Averaging.Trim(), ConfigManager.GeneralConfig.Averaging.Trim());*/
            }

            Close();
        }

        /// <summary>
        /// The ButtonCloseNoSave_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonCloseNoSave_Click(object sender, EventArgs e)
        {
            IsChangeSaved = false;
            Close();
        }

        /// <summary>
        /// The FormSettings_FormClosing
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/></param>
        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChange && !IsChangeSaved)
            {
                var result = MessageBox.Show(International.GetText("Form_Settings_buttonCloseNoSaveMsg"),
                                                      International.GetText("Form_Settings_buttonCloseNoSaveTitle"),
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            // check restart parameters change
            IsRestartNeeded = ConfigManager.IsRestartNeeded();

            if (IsChangeSaved)
            {
                ConfigManager.GeneralConfigFileCommit();
                ConfigManager.CommitBenchmarks();
                International.Initialize(ConfigManager.GeneralConfig.Language);

                if (isStartupChanged)
                {
                    // Commit to registry
                    try
                    {
                        if (checkBox_RunAtStartup.Checked)
                        {
                            // Add NHML to startup registry
                            rkStartup.SetValue(Application.ProductName, Application.ExecutablePath);
                        }
                        else
                        {
                            rkStartup.DeleteValue(Application.ProductName, false);
                        }
                    }
                    catch (Exception er)
                    {
                        Helpers.ConsolePrint("REGISTRY", er.ToString());
                    }
                }
            }
            else
            {
                ConfigManager.RestoreBackup();
            }
        }

        /// <summary>
        /// The CurrencyConverterCombobox_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CurrencyConverterCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Selected = currencyConverterCombobox.SelectedItem.ToString();
            ConfigManager.GeneralConfig.DisplayCurrency = Selected;
        }

        /// <summary>
        /// The TabControlGeneral_Selected
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="TabControlEventArgs"/></param>
        private void TabControlGeneral_Selected(object sender, TabControlEventArgs e)
        {
            // set first device selected {
            if (ComputeDeviceManager.Available.AllAvaliableDevices.Count > 0)
            {
                algorithmSettingsControl1.Deselect();
            }
        }

        /// <summary>
        /// The CheckBox_Use3rdPartyMiners_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CheckBox_Use3rdPartyMiners_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;

            if (checkBox_Use3rdPartyMiners.Checked)
            {
                // Show TOS
                Form tos = new Form_3rdParty_TOS();
                tos.ShowDialog(this);
                checkBox_Use3rdPartyMiners.Checked = ConfigManager.GeneralConfig.Use3rdPartyMiners == Use3rdPartyMiners.YES;
            }
            else
            {
                ConfigManager.GeneralConfig.Use3rdPartyMiners = Use3rdPartyMiners.NO;
            }
        }

        /// <summary>
        /// The CheckBox_HideMiningWindows_CheckChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CheckBox_HideMiningWindows_CheckChanged(object sender, EventArgs e)
        {
            if (!_isInitFinished) return;
            IsChange = true;
            ConfigManager.GeneralConfig.HideMiningWindows = checkBox_HideMiningWindows.Checked;
            checkBox_MinimizeMiningWindows.Enabled = !checkBox_HideMiningWindows.Checked;
        }

        private void textBox_averaging_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox_averaging.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox_averaging.Text = textBox_averaging.Text.Remove(textBox_averaging.Text.Length - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.ShowDialog();
        }
    }
}