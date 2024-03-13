namespace zPoolMiner.Forms
{
    /// <summary>
    /// Defines the <see cref="Form_Settings" />
    /// </summary>
    partial class Form_Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Settings));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlGeneral = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBox_Localization = new System.Windows.Forms.GroupBox();
            this.checkBox_devapi = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_averaging = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label_Language = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Language = new System.Windows.Forms.PictureBox();
            this.comboBox_Language = new System.Windows.Forms.ComboBox();
            this.groupBox_Miners = new System.Windows.Forms.GroupBox();
            this.pictureBox_SwitchMinSecondsFixed = new System.Windows.Forms.PictureBox();
            this.pictureBox_MinerRestartDelayMS = new System.Windows.Forms.PictureBox();
            this.pictureBox_APIBindPortStart = new System.Windows.Forms.PictureBox();
            this.pictureBox_SwitchMinSecondsDynamic = new System.Windows.Forms.PictureBox();
            this.pictureBox_SwitchProfitabilityThreshold = new System.Windows.Forms.PictureBox();
            this.pictureBox_ethminerDefaultBlockHeight = new System.Windows.Forms.PictureBox();
            this.pictureBox_DagGeneration = new System.Windows.Forms.PictureBox();
            this.pictureBox_CPU0_ForceCPUExtension = new System.Windows.Forms.PictureBox();
            this.pictureBox_MinerAPIQueryInterval = new System.Windows.Forms.PictureBox();
            this.pictureBox_SwitchMinSecondsAMD = new System.Windows.Forms.PictureBox();
            this.pictureBox_MinIdleSeconds = new System.Windows.Forms.PictureBox();
            this.comboBox_DagLoadMode = new System.Windows.Forms.ComboBox();
            this.label_DagGeneration = new System.Windows.Forms.Label();
            this.comboBox_CPU0_ForceCPUExtension = new System.Windows.Forms.ComboBox();
            this.label_CPU0_ForceCPUExtension = new System.Windows.Forms.Label();
            this.label_MinIdleSeconds = new System.Windows.Forms.Label();
            this.label_SwitchMinSecondsFixed = new System.Windows.Forms.Label();
            this.label_SwitchMinSecondsDynamic = new System.Windows.Forms.Label();
            this.label_MinerAPIQueryInterval = new System.Windows.Forms.Label();
            this.label_MinerRestartDelayMS = new System.Windows.Forms.Label();
            this.textBox_SwitchMinSecondsAMD = new System.Windows.Forms.TextBox();
            this.label_APIBindPortStart = new System.Windows.Forms.Label();
            this.textBox_SwitchProfitabilityThreshold = new System.Windows.Forms.TextBox();
            this.textBox_ethminerDefaultBlockHeight = new System.Windows.Forms.TextBox();
            this.label_SwitchProfitabilityThreshold = new System.Windows.Forms.Label();
            this.label_ethminerDefaultBlockHeight = new System.Windows.Forms.Label();
            this.textBox_APIBindPortStart = new System.Windows.Forms.TextBox();
            this.label_SwitchMinSecondsAMD = new System.Windows.Forms.Label();
            this.textBox_MinIdleSeconds = new System.Windows.Forms.TextBox();
            this.textBox_SwitchMinSecondsFixed = new System.Windows.Forms.TextBox();
            this.textBox_SwitchMinSecondsDynamic = new System.Windows.Forms.TextBox();
            this.textBox_MinerRestartDelayMS = new System.Windows.Forms.TextBox();
            this.textBox_MinerAPIQueryInterval = new System.Windows.Forms.TextBox();
            this.groupBox_Misc = new System.Windows.Forms.GroupBox();
            this.checkbox_Group_same_devices = new System.Windows.Forms.CheckBox();
            this.pictureBox_RunScriptOnCUDA_GPU_Lost = new System.Windows.Forms.PictureBox();
            this.checkBox_RunScriptOnCUDA_GPU_Lost = new System.Windows.Forms.CheckBox();
            this.pictureBox_ShowInternetConnectionWarning = new System.Windows.Forms.PictureBox();
            this.checkBox_ShowInternetConnectionWarning = new System.Windows.Forms.CheckBox();
            this.checkBox_MinimizeMiningWindows = new System.Windows.Forms.CheckBox();
            this.pictureBox_MinimizeMiningWindows = new System.Windows.Forms.PictureBox();
            this.pictureBox_RunAtStartup = new System.Windows.Forms.PictureBox();
            this.checkBox_RunAtStartup = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowMultipleInstances = new System.Windows.Forms.CheckBox();
            this.checkBox_DisableDefaultOptimizations = new System.Windows.Forms.CheckBox();
            this.checkBox_AMD_DisableAMDTempControl = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoStartMining = new System.Windows.Forms.CheckBox();
            this.checkBox_HideMiningWindows = new System.Windows.Forms.CheckBox();
            this.pictureBox_AllowMultipleInstances = new System.Windows.Forms.PictureBox();
            this.checkBox_MinimizeToTray = new System.Windows.Forms.CheckBox();
            this.pictureBox_DisableDefaultOptimizations = new System.Windows.Forms.PictureBox();
            this.pictureBox_AMD_DisableAMDTempControl = new System.Windows.Forms.PictureBox();
            this.pictureBox_NVIDIAP0State = new System.Windows.Forms.PictureBox();
            this.pictureBox_DisableWindowsErrorReporting = new System.Windows.Forms.PictureBox();
            this.pictureBox_ShowDriverVersionWarning = new System.Windows.Forms.PictureBox();
            this.pictureBox_StartMiningWhenIdle = new System.Windows.Forms.PictureBox();
            this.pictureBox_AutoScaleBTCValues = new System.Windows.Forms.PictureBox();
            this.pictureBox_DisableDetectionAMD = new System.Windows.Forms.PictureBox();
            this.pictureBox_Use3rdPartyMiners = new System.Windows.Forms.PictureBox();
            this.pictureBox_DisableDetectionNVIDIA = new System.Windows.Forms.PictureBox();
            this.pictureBox_AutoStartMining = new System.Windows.Forms.PictureBox();
            this.pictureBox_MinimizeToTray = new System.Windows.Forms.PictureBox();
            this.pictureBox_HideMiningWindows = new System.Windows.Forms.PictureBox();
            this.checkBox_Use3rdPartyMiners = new System.Windows.Forms.CheckBox();
            this.checkBox_DisableDetectionNVIDIA = new System.Windows.Forms.CheckBox();
            this.checkBox_DisableDetectionAMD = new System.Windows.Forms.CheckBox();
            this.checkBox_NVIDIAP0State = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoScaleBTCValues = new System.Windows.Forms.CheckBox();
            this.checkBox_DisableWindowsErrorReporting = new System.Windows.Forms.CheckBox();
            this.checkBox_StartMiningWhenIdle = new System.Windows.Forms.CheckBox();
            this.checkBox_ShowDriverVersionWarning = new System.Windows.Forms.CheckBox();
            this.groupBox_Logging = new System.Windows.Forms.GroupBox();
            this.label_LogMaxFileSize = new System.Windows.Forms.Label();
            this.textBox_LogMaxFileSize = new System.Windows.Forms.TextBox();
            this.checkBox_LogToFile = new System.Windows.Forms.CheckBox();
            this.pictureBox_DebugConsole = new System.Windows.Forms.PictureBox();
            this.pictureBox_LogMaxFileSize = new System.Windows.Forms.PictureBox();
            this.pictureBox_LogToFile = new System.Windows.Forms.PictureBox();
            this.checkBox_DebugConsole = new System.Windows.Forms.CheckBox();
            this.groupBox_Main = new System.Windows.Forms.GroupBox();
            this.pictureBox_TimeUnit = new System.Windows.Forms.PictureBox();
            this.label_TimeUnit = new System.Windows.Forms.Label();
            this.pictureBox_displayCurrency = new System.Windows.Forms.PictureBox();
            this.comboBox_TimeUnit = new System.Windows.Forms.ComboBox();
            this.checkBox_IdleWhenNoInternetAccess = new System.Windows.Forms.CheckBox();
            this.currencyConverterCombobox = new System.Windows.Forms.ComboBox();
            this.pictureBox_MinProfit = new System.Windows.Forms.PictureBox();
            this.label_displayCurrency = new System.Windows.Forms.Label();
            this.textBox_MinProfit = new System.Windows.Forms.TextBox();
            this.pictureBox_IdleWhenNoInternetAccess = new System.Windows.Forms.PictureBox();
            this.label_MinProfit = new System.Windows.Forms.Label();
            this.tabPageDevicesAlgos = new System.Windows.Forms.TabPage();
            this.groupBoxAlgorithmSettings = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_blazepool_Worker = new System.Windows.Forms.TextBox();
            this.textBox_blockmunch_Worker = new System.Windows.Forms.TextBox();
            this.textBox_starpool_Worker = new System.Windows.Forms.TextBox();
            this.textBox_minemoney_Worker = new System.Windows.Forms.TextBox();
            this.textBox_zerg_Worker = new System.Windows.Forms.TextBox();
            this.textBox_hashrefinery_worker = new System.Windows.Forms.TextBox();
            this.textBox_AhashPool_Worker = new System.Windows.Forms.TextBox();
            this.textBox_Zpool_Worker = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_MPH_Worker = new System.Windows.Forms.TextBox();
            this.textBox_NiceHash_Worker = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_blazepool_Wallet = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_blockmunch_Wallet = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_starpool_Wallet = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_minemoney_Wallet = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_zerg_Wallet = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_MPH_Wallet = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Zpool_Wallet = new System.Windows.Forms.TextBox();
            this.textBox_NiceHash_Wallet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_AhashPool_Wallet = new System.Windows.Forms.TextBox();
            this.textBox_HashRefinery_Wallet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_blazepool = new System.Windows.Forms.CheckBox();
            this.checkBox_blockmunch = new System.Windows.Forms.CheckBox();
            this.checkBox_starpool = new System.Windows.Forms.CheckBox();
            this.checkBox_zerg = new System.Windows.Forms.CheckBox();
            this.checkBox_minemoney = new System.Windows.Forms.CheckBox();
            this.checkBox_MPH = new System.Windows.Forms.CheckBox();
            this.checkBox_NiceHash = new System.Windows.Forms.CheckBox();
            this.checkBox_HashRefinery = new System.Windows.Forms.CheckBox();
            this.checkBox_AhashPool = new System.Windows.Forms.CheckBox();
            this.checkBox_zpool = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSaveClose = new System.Windows.Forms.Button();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.buttonCloseNoSave = new System.Windows.Forms.Button();
            this.algorithmsListView1 = new zPoolMiner.Forms.Components.AlgorithmsListView();
            this.devicesListViewEnableControl1 = new zPoolMiner.Forms.Components.DevicesListViewEnableControl();
            this.algorithmSettingsControl1 = new zPoolMiner.Forms.Components.AlgorithmSettingsControl();
            this.tabControlGeneral.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBox_Localization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Language)).BeginInit();
            this.groupBox_Miners.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchMinSecondsFixed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinerRestartDelayMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_APIBindPortStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchMinSecondsDynamic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchProfitabilityThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ethminerDefaultBlockHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DagGeneration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CPU0_ForceCPUExtension)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinerAPIQueryInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchMinSecondsAMD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinIdleSeconds)).BeginInit();
            this.groupBox_Misc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunScriptOnCUDA_GPU_Lost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShowInternetConnectionWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinimizeMiningWindows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunAtStartup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AllowMultipleInstances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableDefaultOptimizations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AMD_DisableAMDTempControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NVIDIAP0State)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableWindowsErrorReporting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShowDriverVersionWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_StartMiningWhenIdle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AutoScaleBTCValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableDetectionAMD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Use3rdPartyMiners)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableDetectionNVIDIA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AutoStartMining)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinimizeToTray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_HideMiningWindows)).BeginInit();
            this.groupBox_Logging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DebugConsole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LogMaxFileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LogToFile)).BeginInit();
            this.groupBox_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TimeUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_displayCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IdleWhenNoInternetAccess)).BeginInit();
            this.tabPageDevicesAlgos.SuspendLayout();
            this.groupBoxAlgorithmSettings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.ToolTip1_Popup);
            // 
            // tabControlGeneral
            // 
            this.tabControlGeneral.Controls.Add(this.tabPageGeneral);
            this.tabControlGeneral.Controls.Add(this.tabPageDevicesAlgos);
            this.tabControlGeneral.Controls.Add(this.tabPage1);
            this.tabControlGeneral.Location = new System.Drawing.Point(12, 11);
            this.tabControlGeneral.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControlGeneral.Name = "tabControlGeneral";
            this.tabControlGeneral.SelectedIndex = 0;
            this.tabControlGeneral.Size = new System.Drawing.Size(626, 440);
            this.tabControlGeneral.TabIndex = 0;
            this.tabControlGeneral.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControlGeneral_Selected);
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.BackColor = System.Drawing.Color.White;
            this.tabPageGeneral.Controls.Add(this.groupBox_Localization);
            this.tabPageGeneral.Controls.Add(this.groupBox_Miners);
            this.tabPageGeneral.Controls.Add(this.groupBox_Misc);
            this.tabPageGeneral.Controls.Add(this.groupBox_Logging);
            this.tabPageGeneral.Controls.Add(this.groupBox_Main);
            this.tabPageGeneral.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageGeneral.Size = new System.Drawing.Size(618, 414);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            // 
            // groupBox_Localization
            // 
            this.groupBox_Localization.Controls.Add(this.checkBox_devapi);
            this.groupBox_Localization.Controls.Add(this.label15);
            this.groupBox_Localization.Controls.Add(this.textBox_averaging);
            this.groupBox_Localization.Controls.Add(this.label14);
            this.groupBox_Localization.Controls.Add(this.label_Language);
            this.groupBox_Localization.Controls.Add(this.pictureBox5);
            this.groupBox_Localization.Controls.Add(this.pictureBox_Language);
            this.groupBox_Localization.Controls.Add(this.comboBox_Language);
            this.groupBox_Localization.Location = new System.Drawing.Point(6, 197);
            this.groupBox_Localization.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Localization.Name = "groupBox_Localization";
            this.groupBox_Localization.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Localization.Size = new System.Drawing.Size(206, 101);
            this.groupBox_Localization.TabIndex = 385;
            this.groupBox_Localization.TabStop = false;
            this.groupBox_Localization.Text = "Other:";
            // 
            // checkBox_devapi
            // 
            this.checkBox_devapi.AutoSize = true;
            this.checkBox_devapi.Enabled = false;
            this.checkBox_devapi.Location = new System.Drawing.Point(5, 75);
            this.checkBox_devapi.Name = "checkBox_devapi";
            this.checkBox_devapi.Size = new System.Drawing.Size(88, 17);
            this.checkBox_devapi.TabIndex = 368;
            this.checkBox_devapi.Text = "Use Dev API";
            this.checkBox_devapi.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 33);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(171, 13);
            this.label15.TabIndex = 367;
            this.label15.Text = "1 = 3 Minutes, 2 = 6 Minutes, ETC ";
            // 
            // textBox_averaging
            // 
            this.textBox_averaging.Location = new System.Drawing.Point(4, 49);
            this.textBox_averaging.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_averaging.Name = "textBox_averaging";
            this.textBox_averaging.Size = new System.Drawing.Size(169, 20);
            this.textBox_averaging.TabIndex = 366;
            this.textBox_averaging.TextChanged += new System.EventHandler(this.textBox_averaging_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 14);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 13);
            this.label14.TabIndex = 365;
            this.label14.Text = "Profitability Normalization:";
            // 
            // label_Language
            // 
            this.label_Language.AutoSize = true;
            this.label_Language.Location = new System.Drawing.Point(224, 29);
            this.label_Language.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Language.Name = "label_Language";
            this.label_Language.Size = new System.Drawing.Size(58, 13);
            this.label_Language.TabIndex = 358;
            this.label_Language.Text = "Language:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox5.Location = new System.Drawing.Point(-58, 59);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(18, 18);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 364;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox_Language
            // 
            this.pictureBox_Language.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_Language.Location = new System.Drawing.Point(396, 29);
            this.pictureBox_Language.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_Language.Name = "pictureBox_Language";
            this.pictureBox_Language.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_Language.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Language.TabIndex = 364;
            this.pictureBox_Language.TabStop = false;
            // 
            // comboBox_Language
            // 
            this.comboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Language.FormattingEnabled = true;
            this.comboBox_Language.Location = new System.Drawing.Point(224, 49);
            this.comboBox_Language.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox_Language.Name = "comboBox_Language";
            this.comboBox_Language.Size = new System.Drawing.Size(190, 21);
            this.comboBox_Language.TabIndex = 328;
            // 
            // groupBox_Miners
            // 
            this.groupBox_Miners.Controls.Add(this.pictureBox_SwitchMinSecondsFixed);
            this.groupBox_Miners.Controls.Add(this.pictureBox_MinerRestartDelayMS);
            this.groupBox_Miners.Controls.Add(this.pictureBox_APIBindPortStart);
            this.groupBox_Miners.Controls.Add(this.pictureBox_SwitchMinSecondsDynamic);
            this.groupBox_Miners.Controls.Add(this.pictureBox_SwitchProfitabilityThreshold);
            this.groupBox_Miners.Controls.Add(this.pictureBox_ethminerDefaultBlockHeight);
            this.groupBox_Miners.Controls.Add(this.pictureBox_DagGeneration);
            this.groupBox_Miners.Controls.Add(this.pictureBox_CPU0_ForceCPUExtension);
            this.groupBox_Miners.Controls.Add(this.pictureBox_MinerAPIQueryInterval);
            this.groupBox_Miners.Controls.Add(this.pictureBox_SwitchMinSecondsAMD);
            this.groupBox_Miners.Controls.Add(this.pictureBox_MinIdleSeconds);
            this.groupBox_Miners.Controls.Add(this.comboBox_DagLoadMode);
            this.groupBox_Miners.Controls.Add(this.label_DagGeneration);
            this.groupBox_Miners.Controls.Add(this.comboBox_CPU0_ForceCPUExtension);
            this.groupBox_Miners.Controls.Add(this.label_CPU0_ForceCPUExtension);
            this.groupBox_Miners.Controls.Add(this.label_MinIdleSeconds);
            this.groupBox_Miners.Controls.Add(this.label_SwitchMinSecondsFixed);
            this.groupBox_Miners.Controls.Add(this.label_SwitchMinSecondsDynamic);
            this.groupBox_Miners.Controls.Add(this.label_MinerAPIQueryInterval);
            this.groupBox_Miners.Controls.Add(this.label_MinerRestartDelayMS);
            this.groupBox_Miners.Controls.Add(this.textBox_SwitchMinSecondsAMD);
            this.groupBox_Miners.Controls.Add(this.label_APIBindPortStart);
            this.groupBox_Miners.Controls.Add(this.textBox_SwitchProfitabilityThreshold);
            this.groupBox_Miners.Controls.Add(this.textBox_ethminerDefaultBlockHeight);
            this.groupBox_Miners.Controls.Add(this.label_SwitchProfitabilityThreshold);
            this.groupBox_Miners.Controls.Add(this.label_ethminerDefaultBlockHeight);
            this.groupBox_Miners.Controls.Add(this.textBox_APIBindPortStart);
            this.groupBox_Miners.Controls.Add(this.label_SwitchMinSecondsAMD);
            this.groupBox_Miners.Controls.Add(this.textBox_MinIdleSeconds);
            this.groupBox_Miners.Controls.Add(this.textBox_SwitchMinSecondsFixed);
            this.groupBox_Miners.Controls.Add(this.textBox_SwitchMinSecondsDynamic);
            this.groupBox_Miners.Controls.Add(this.textBox_MinerRestartDelayMS);
            this.groupBox_Miners.Controls.Add(this.textBox_MinerAPIQueryInterval);
            this.groupBox_Miners.Location = new System.Drawing.Point(448, 6);
            this.groupBox_Miners.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Miners.Name = "groupBox_Miners";
            this.groupBox_Miners.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Miners.Size = new System.Drawing.Size(166, 399);
            this.groupBox_Miners.TabIndex = 392;
            this.groupBox_Miners.TabStop = false;
            this.groupBox_Miners.Text = "Miners:";
            // 
            // pictureBox_SwitchMinSecondsFixed
            // 
            this.pictureBox_SwitchMinSecondsFixed.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_SwitchMinSecondsFixed.Location = new System.Drawing.Point(138, 165);
            this.pictureBox_SwitchMinSecondsFixed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_SwitchMinSecondsFixed.Name = "pictureBox_SwitchMinSecondsFixed";
            this.pictureBox_SwitchMinSecondsFixed.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_SwitchMinSecondsFixed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_SwitchMinSecondsFixed.TabIndex = 385;
            this.pictureBox_SwitchMinSecondsFixed.TabStop = false;
            // 
            // pictureBox_MinerRestartDelayMS
            // 
            this.pictureBox_MinerRestartDelayMS.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_MinerRestartDelayMS.Location = new System.Drawing.Point(138, 205);
            this.pictureBox_MinerRestartDelayMS.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_MinerRestartDelayMS.Name = "pictureBox_MinerRestartDelayMS";
            this.pictureBox_MinerRestartDelayMS.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_MinerRestartDelayMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_MinerRestartDelayMS.TabIndex = 385;
            this.pictureBox_MinerRestartDelayMS.TabStop = false;
            // 
            // pictureBox_APIBindPortStart
            // 
            this.pictureBox_APIBindPortStart.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_APIBindPortStart.Location = new System.Drawing.Point(138, 247);
            this.pictureBox_APIBindPortStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_APIBindPortStart.Name = "pictureBox_APIBindPortStart";
            this.pictureBox_APIBindPortStart.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_APIBindPortStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_APIBindPortStart.TabIndex = 385;
            this.pictureBox_APIBindPortStart.TabStop = false;
            // 
            // pictureBox_SwitchMinSecondsDynamic
            // 
            this.pictureBox_SwitchMinSecondsDynamic.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_SwitchMinSecondsDynamic.Location = new System.Drawing.Point(138, 79);
            this.pictureBox_SwitchMinSecondsDynamic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_SwitchMinSecondsDynamic.Name = "pictureBox_SwitchMinSecondsDynamic";
            this.pictureBox_SwitchMinSecondsDynamic.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_SwitchMinSecondsDynamic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_SwitchMinSecondsDynamic.TabIndex = 385;
            this.pictureBox_SwitchMinSecondsDynamic.TabStop = false;
            // 
            // pictureBox_SwitchProfitabilityThreshold
            // 
            this.pictureBox_SwitchProfitabilityThreshold.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_SwitchProfitabilityThreshold.Location = new System.Drawing.Point(364, 61);
            this.pictureBox_SwitchProfitabilityThreshold.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_SwitchProfitabilityThreshold.Name = "pictureBox_SwitchProfitabilityThreshold";
            this.pictureBox_SwitchProfitabilityThreshold.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_SwitchProfitabilityThreshold.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_SwitchProfitabilityThreshold.TabIndex = 385;
            this.pictureBox_SwitchProfitabilityThreshold.TabStop = false;
            this.pictureBox_SwitchProfitabilityThreshold.Visible = false;
            // 
            // pictureBox_ethminerDefaultBlockHeight
            // 
            this.pictureBox_ethminerDefaultBlockHeight.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_ethminerDefaultBlockHeight.Location = new System.Drawing.Point(367, 19);
            this.pictureBox_ethminerDefaultBlockHeight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_ethminerDefaultBlockHeight.Name = "pictureBox_ethminerDefaultBlockHeight";
            this.pictureBox_ethminerDefaultBlockHeight.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_ethminerDefaultBlockHeight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_ethminerDefaultBlockHeight.TabIndex = 385;
            this.pictureBox_ethminerDefaultBlockHeight.TabStop = false;
            this.pictureBox_ethminerDefaultBlockHeight.Visible = false;
            // 
            // pictureBox_DagGeneration
            // 
            this.pictureBox_DagGeneration.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_DagGeneration.Location = new System.Drawing.Point(362, 146);
            this.pictureBox_DagGeneration.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_DagGeneration.Name = "pictureBox_DagGeneration";
            this.pictureBox_DagGeneration.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_DagGeneration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_DagGeneration.TabIndex = 385;
            this.pictureBox_DagGeneration.TabStop = false;
            this.pictureBox_DagGeneration.Visible = false;
            // 
            // pictureBox_CPU0_ForceCPUExtension
            // 
            this.pictureBox_CPU0_ForceCPUExtension.Enabled = false;
            this.pictureBox_CPU0_ForceCPUExtension.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_CPU0_ForceCPUExtension.Location = new System.Drawing.Point(138, 334);
            this.pictureBox_CPU0_ForceCPUExtension.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_CPU0_ForceCPUExtension.Name = "pictureBox_CPU0_ForceCPUExtension";
            this.pictureBox_CPU0_ForceCPUExtension.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_CPU0_ForceCPUExtension.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_CPU0_ForceCPUExtension.TabIndex = 385;
            this.pictureBox_CPU0_ForceCPUExtension.TabStop = false;
            this.pictureBox_CPU0_ForceCPUExtension.Visible = false;
            // 
            // pictureBox_MinerAPIQueryInterval
            // 
            this.pictureBox_MinerAPIQueryInterval.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_MinerAPIQueryInterval.Location = new System.Drawing.Point(138, 289);
            this.pictureBox_MinerAPIQueryInterval.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_MinerAPIQueryInterval.Name = "pictureBox_MinerAPIQueryInterval";
            this.pictureBox_MinerAPIQueryInterval.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_MinerAPIQueryInterval.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_MinerAPIQueryInterval.TabIndex = 385;
            this.pictureBox_MinerAPIQueryInterval.TabStop = false;
            // 
            // pictureBox_SwitchMinSecondsAMD
            // 
            this.pictureBox_SwitchMinSecondsAMD.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_SwitchMinSecondsAMD.Location = new System.Drawing.Point(138, 123);
            this.pictureBox_SwitchMinSecondsAMD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_SwitchMinSecondsAMD.Name = "pictureBox_SwitchMinSecondsAMD";
            this.pictureBox_SwitchMinSecondsAMD.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_SwitchMinSecondsAMD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_SwitchMinSecondsAMD.TabIndex = 385;
            this.pictureBox_SwitchMinSecondsAMD.TabStop = false;
            // 
            // pictureBox_MinIdleSeconds
            // 
            this.pictureBox_MinIdleSeconds.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_MinIdleSeconds.Location = new System.Drawing.Point(138, 33);
            this.pictureBox_MinIdleSeconds.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_MinIdleSeconds.Name = "pictureBox_MinIdleSeconds";
            this.pictureBox_MinIdleSeconds.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_MinIdleSeconds.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_MinIdleSeconds.TabIndex = 385;
            this.pictureBox_MinIdleSeconds.TabStop = false;
            // 
            // comboBox_DagLoadMode
            // 
            this.comboBox_DagLoadMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DagLoadMode.FormattingEnabled = true;
            this.comboBox_DagLoadMode.Items.AddRange(new object[] {
            "Automatic",
            "SSE2",
            "AVX",
            "AVX2"});
            this.comboBox_DagLoadMode.Location = new System.Drawing.Point(210, 165);
            this.comboBox_DagLoadMode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox_DagLoadMode.Name = "comboBox_DagLoadMode";
            this.comboBox_DagLoadMode.Size = new System.Drawing.Size(172, 21);
            this.comboBox_DagLoadMode.TabIndex = 383;
            this.comboBox_DagLoadMode.Visible = false;
            // 
            // label_DagGeneration
            // 
            this.label_DagGeneration.AutoSize = true;
            this.label_DagGeneration.Location = new System.Drawing.Point(210, 149);
            this.label_DagGeneration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_DagGeneration.Name = "label_DagGeneration";
            this.label_DagGeneration.Size = new System.Drawing.Size(87, 13);
            this.label_DagGeneration.TabIndex = 384;
            this.label_DagGeneration.Text = "Dag Load Mode:";
            this.label_DagGeneration.Visible = false;
            // 
            // comboBox_CPU0_ForceCPUExtension
            // 
            this.comboBox_CPU0_ForceCPUExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CPU0_ForceCPUExtension.Enabled = false;
            this.comboBox_CPU0_ForceCPUExtension.FormattingEnabled = true;
            this.comboBox_CPU0_ForceCPUExtension.Items.AddRange(new object[] {
            "Automatic",
            "AVX2",
            "AVX",
            "AES"});
            this.comboBox_CPU0_ForceCPUExtension.Location = new System.Drawing.Point(10, 334);
            this.comboBox_CPU0_ForceCPUExtension.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox_CPU0_ForceCPUExtension.Name = "comboBox_CPU0_ForceCPUExtension";
            this.comboBox_CPU0_ForceCPUExtension.Size = new System.Drawing.Size(126, 21);
            this.comboBox_CPU0_ForceCPUExtension.TabIndex = 379;
            // 
            // label_CPU0_ForceCPUExtension
            // 
            this.label_CPU0_ForceCPUExtension.AutoSize = true;
            this.label_CPU0_ForceCPUExtension.Enabled = false;
            this.label_CPU0_ForceCPUExtension.Location = new System.Drawing.Point(10, 318);
            this.label_CPU0_ForceCPUExtension.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_CPU0_ForceCPUExtension.Name = "label_CPU0_ForceCPUExtension";
            this.label_CPU0_ForceCPUExtension.Size = new System.Drawing.Size(105, 13);
            this.label_CPU0_ForceCPUExtension.TabIndex = 382;
            this.label_CPU0_ForceCPUExtension.Text = "ForceCPUExtension:";
            this.label_CPU0_ForceCPUExtension.Visible = false;
            // 
            // label_MinIdleSeconds
            // 
            this.label_MinIdleSeconds.AutoSize = true;
            this.label_MinIdleSeconds.Location = new System.Drawing.Point(10, 15);
            this.label_MinIdleSeconds.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MinIdleSeconds.Name = "label_MinIdleSeconds";
            this.label_MinIdleSeconds.Size = new System.Drawing.Size(86, 13);
            this.label_MinIdleSeconds.TabIndex = 356;
            this.label_MinIdleSeconds.Text = "MinIdleSeconds:";
            // 
            // label_SwitchMinSecondsFixed
            // 
            this.label_SwitchMinSecondsFixed.AutoSize = true;
            this.label_SwitchMinSecondsFixed.Location = new System.Drawing.Point(10, 149);
            this.label_SwitchMinSecondsFixed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SwitchMinSecondsFixed.Name = "label_SwitchMinSecondsFixed";
            this.label_SwitchMinSecondsFixed.Size = new System.Drawing.Size(126, 13);
            this.label_SwitchMinSecondsFixed.TabIndex = 366;
            this.label_SwitchMinSecondsFixed.Text = "SwitchMinSecondsFixed:";
            // 
            // label_SwitchMinSecondsDynamic
            // 
            this.label_SwitchMinSecondsDynamic.AutoSize = true;
            this.label_SwitchMinSecondsDynamic.Location = new System.Drawing.Point(10, 63);
            this.label_SwitchMinSecondsDynamic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SwitchMinSecondsDynamic.Name = "label_SwitchMinSecondsDynamic";
            this.label_SwitchMinSecondsDynamic.Size = new System.Drawing.Size(142, 13);
            this.label_SwitchMinSecondsDynamic.TabIndex = 378;
            this.label_SwitchMinSecondsDynamic.Text = "SwitchMinSecondsDynamic:";
            // 
            // label_MinerAPIQueryInterval
            // 
            this.label_MinerAPIQueryInterval.AutoSize = true;
            this.label_MinerAPIQueryInterval.Location = new System.Drawing.Point(10, 273);
            this.label_MinerAPIQueryInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MinerAPIQueryInterval.Name = "label_MinerAPIQueryInterval";
            this.label_MinerAPIQueryInterval.Size = new System.Drawing.Size(116, 13);
            this.label_MinerAPIQueryInterval.TabIndex = 376;
            this.label_MinerAPIQueryInterval.Text = "MinerAPIQueryInterval:";
            // 
            // label_MinerRestartDelayMS
            // 
            this.label_MinerRestartDelayMS.AutoSize = true;
            this.label_MinerRestartDelayMS.Location = new System.Drawing.Point(10, 189);
            this.label_MinerRestartDelayMS.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MinerRestartDelayMS.Name = "label_MinerRestartDelayMS";
            this.label_MinerRestartDelayMS.Size = new System.Drawing.Size(113, 13);
            this.label_MinerRestartDelayMS.TabIndex = 375;
            this.label_MinerRestartDelayMS.Text = "MinerRestartDelayMS:";
            // 
            // textBox_SwitchMinSecondsAMD
            // 
            this.textBox_SwitchMinSecondsAMD.Location = new System.Drawing.Point(10, 123);
            this.textBox_SwitchMinSecondsAMD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_SwitchMinSecondsAMD.Name = "textBox_SwitchMinSecondsAMD";
            this.textBox_SwitchMinSecondsAMD.Size = new System.Drawing.Size(124, 20);
            this.textBox_SwitchMinSecondsAMD.TabIndex = 342;
            // 
            // label_APIBindPortStart
            // 
            this.label_APIBindPortStart.AutoSize = true;
            this.label_APIBindPortStart.Location = new System.Drawing.Point(10, 231);
            this.label_APIBindPortStart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_APIBindPortStart.Name = "label_APIBindPortStart";
            this.label_APIBindPortStart.Size = new System.Drawing.Size(118, 13);
            this.label_APIBindPortStart.TabIndex = 357;
            this.label_APIBindPortStart.Text = "API Bind port pool start:";
            // 
            // textBox_SwitchProfitabilityThreshold
            // 
            this.textBox_SwitchProfitabilityThreshold.Location = new System.Drawing.Point(210, 80);
            this.textBox_SwitchProfitabilityThreshold.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_SwitchProfitabilityThreshold.Name = "textBox_SwitchProfitabilityThreshold";
            this.textBox_SwitchProfitabilityThreshold.Size = new System.Drawing.Size(172, 20);
            this.textBox_SwitchProfitabilityThreshold.TabIndex = 333;
            this.textBox_SwitchProfitabilityThreshold.Visible = false;
            // 
            // textBox_ethminerDefaultBlockHeight
            // 
            this.textBox_ethminerDefaultBlockHeight.Location = new System.Drawing.Point(213, 36);
            this.textBox_ethminerDefaultBlockHeight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_ethminerDefaultBlockHeight.Name = "textBox_ethminerDefaultBlockHeight";
            this.textBox_ethminerDefaultBlockHeight.Size = new System.Drawing.Size(172, 20);
            this.textBox_ethminerDefaultBlockHeight.TabIndex = 333;
            this.textBox_ethminerDefaultBlockHeight.Visible = false;
            // 
            // label_SwitchProfitabilityThreshold
            // 
            this.label_SwitchProfitabilityThreshold.AutoSize = true;
            this.label_SwitchProfitabilityThreshold.Location = new System.Drawing.Point(210, 59);
            this.label_SwitchProfitabilityThreshold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SwitchProfitabilityThreshold.Name = "label_SwitchProfitabilityThreshold";
            this.label_SwitchProfitabilityThreshold.Size = new System.Drawing.Size(142, 13);
            this.label_SwitchProfitabilityThreshold.TabIndex = 361;
            this.label_SwitchProfitabilityThreshold.Text = "ethminerDefaultBlockHeight:";
            this.label_SwitchProfitabilityThreshold.Visible = false;
            // 
            // label_ethminerDefaultBlockHeight
            // 
            this.label_ethminerDefaultBlockHeight.AutoSize = true;
            this.label_ethminerDefaultBlockHeight.Location = new System.Drawing.Point(210, 20);
            this.label_ethminerDefaultBlockHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ethminerDefaultBlockHeight.Name = "label_ethminerDefaultBlockHeight";
            this.label_ethminerDefaultBlockHeight.Size = new System.Drawing.Size(142, 13);
            this.label_ethminerDefaultBlockHeight.TabIndex = 361;
            this.label_ethminerDefaultBlockHeight.Text = "ethminerDefaultBlockHeight:";
            this.label_ethminerDefaultBlockHeight.Visible = false;
            // 
            // textBox_APIBindPortStart
            // 
            this.textBox_APIBindPortStart.Location = new System.Drawing.Point(10, 247);
            this.textBox_APIBindPortStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_APIBindPortStart.Name = "textBox_APIBindPortStart";
            this.textBox_APIBindPortStart.Size = new System.Drawing.Size(124, 20);
            this.textBox_APIBindPortStart.TabIndex = 334;
            // 
            // label_SwitchMinSecondsAMD
            // 
            this.label_SwitchMinSecondsAMD.AutoSize = true;
            this.label_SwitchMinSecondsAMD.Location = new System.Drawing.Point(10, 107);
            this.label_SwitchMinSecondsAMD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SwitchMinSecondsAMD.Name = "label_SwitchMinSecondsAMD";
            this.label_SwitchMinSecondsAMD.Size = new System.Drawing.Size(125, 13);
            this.label_SwitchMinSecondsAMD.TabIndex = 362;
            this.label_SwitchMinSecondsAMD.Text = "SwitchMinSecondsAMD:";
            // 
            // textBox_MinIdleSeconds
            // 
            this.textBox_MinIdleSeconds.Location = new System.Drawing.Point(10, 33);
            this.textBox_MinIdleSeconds.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_MinIdleSeconds.Name = "textBox_MinIdleSeconds";
            this.textBox_MinIdleSeconds.Size = new System.Drawing.Size(124, 20);
            this.textBox_MinIdleSeconds.TabIndex = 335;
            // 
            // textBox_SwitchMinSecondsFixed
            // 
            this.textBox_SwitchMinSecondsFixed.Location = new System.Drawing.Point(10, 165);
            this.textBox_SwitchMinSecondsFixed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_SwitchMinSecondsFixed.Name = "textBox_SwitchMinSecondsFixed";
            this.textBox_SwitchMinSecondsFixed.Size = new System.Drawing.Size(124, 20);
            this.textBox_SwitchMinSecondsFixed.TabIndex = 332;
            // 
            // textBox_SwitchMinSecondsDynamic
            // 
            this.textBox_SwitchMinSecondsDynamic.Location = new System.Drawing.Point(10, 79);
            this.textBox_SwitchMinSecondsDynamic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_SwitchMinSecondsDynamic.Name = "textBox_SwitchMinSecondsDynamic";
            this.textBox_SwitchMinSecondsDynamic.Size = new System.Drawing.Size(124, 20);
            this.textBox_SwitchMinSecondsDynamic.TabIndex = 337;
            // 
            // textBox_MinerRestartDelayMS
            // 
            this.textBox_MinerRestartDelayMS.Location = new System.Drawing.Point(10, 205);
            this.textBox_MinerRestartDelayMS.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_MinerRestartDelayMS.Name = "textBox_MinerRestartDelayMS";
            this.textBox_MinerRestartDelayMS.Size = new System.Drawing.Size(124, 20);
            this.textBox_MinerRestartDelayMS.TabIndex = 340;
            // 
            // textBox_MinerAPIQueryInterval
            // 
            this.textBox_MinerAPIQueryInterval.Location = new System.Drawing.Point(10, 289);
            this.textBox_MinerAPIQueryInterval.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_MinerAPIQueryInterval.Name = "textBox_MinerAPIQueryInterval";
            this.textBox_MinerAPIQueryInterval.Size = new System.Drawing.Size(124, 20);
            this.textBox_MinerAPIQueryInterval.TabIndex = 341;
            // 
            // groupBox_Misc
            // 
            this.groupBox_Misc.Controls.Add(this.checkbox_Group_same_devices);
            this.groupBox_Misc.Controls.Add(this.pictureBox_RunScriptOnCUDA_GPU_Lost);
            this.groupBox_Misc.Controls.Add(this.checkBox_RunScriptOnCUDA_GPU_Lost);
            this.groupBox_Misc.Controls.Add(this.pictureBox_ShowInternetConnectionWarning);
            this.groupBox_Misc.Controls.Add(this.checkBox_ShowInternetConnectionWarning);
            this.groupBox_Misc.Controls.Add(this.checkBox_MinimizeMiningWindows);
            this.groupBox_Misc.Controls.Add(this.pictureBox_MinimizeMiningWindows);
            this.groupBox_Misc.Controls.Add(this.pictureBox_RunAtStartup);
            this.groupBox_Misc.Controls.Add(this.checkBox_RunAtStartup);
            this.groupBox_Misc.Controls.Add(this.checkBox_AllowMultipleInstances);
            this.groupBox_Misc.Controls.Add(this.checkBox_DisableDefaultOptimizations);
            this.groupBox_Misc.Controls.Add(this.checkBox_AMD_DisableAMDTempControl);
            this.groupBox_Misc.Controls.Add(this.checkBox_AutoStartMining);
            this.groupBox_Misc.Controls.Add(this.checkBox_HideMiningWindows);
            this.groupBox_Misc.Controls.Add(this.pictureBox_AllowMultipleInstances);
            this.groupBox_Misc.Controls.Add(this.checkBox_MinimizeToTray);
            this.groupBox_Misc.Controls.Add(this.pictureBox_DisableDefaultOptimizations);
            this.groupBox_Misc.Controls.Add(this.pictureBox_AMD_DisableAMDTempControl);
            this.groupBox_Misc.Controls.Add(this.pictureBox_NVIDIAP0State);
            this.groupBox_Misc.Controls.Add(this.pictureBox_DisableWindowsErrorReporting);
            this.groupBox_Misc.Controls.Add(this.pictureBox_ShowDriverVersionWarning);
            this.groupBox_Misc.Controls.Add(this.pictureBox_StartMiningWhenIdle);
            this.groupBox_Misc.Controls.Add(this.pictureBox_AutoScaleBTCValues);
            this.groupBox_Misc.Controls.Add(this.pictureBox_DisableDetectionAMD);
            this.groupBox_Misc.Controls.Add(this.pictureBox_Use3rdPartyMiners);
            this.groupBox_Misc.Controls.Add(this.pictureBox_DisableDetectionNVIDIA);
            this.groupBox_Misc.Controls.Add(this.pictureBox_AutoStartMining);
            this.groupBox_Misc.Controls.Add(this.pictureBox_MinimizeToTray);
            this.groupBox_Misc.Controls.Add(this.pictureBox_HideMiningWindows);
            this.groupBox_Misc.Controls.Add(this.checkBox_Use3rdPartyMiners);
            this.groupBox_Misc.Controls.Add(this.checkBox_DisableDetectionNVIDIA);
            this.groupBox_Misc.Controls.Add(this.checkBox_DisableDetectionAMD);
            this.groupBox_Misc.Controls.Add(this.checkBox_NVIDIAP0State);
            this.groupBox_Misc.Controls.Add(this.checkBox_AutoScaleBTCValues);
            this.groupBox_Misc.Controls.Add(this.checkBox_DisableWindowsErrorReporting);
            this.groupBox_Misc.Controls.Add(this.checkBox_StartMiningWhenIdle);
            this.groupBox_Misc.Controls.Add(this.checkBox_ShowDriverVersionWarning);
            this.groupBox_Misc.Location = new System.Drawing.Point(216, 6);
            this.groupBox_Misc.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Misc.Name = "groupBox_Misc";
            this.groupBox_Misc.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Misc.Size = new System.Drawing.Size(228, 402);
            this.groupBox_Misc.TabIndex = 391;
            this.groupBox_Misc.TabStop = false;
            this.groupBox_Misc.Text = "Misc:";
            // 
            // checkbox_Group_same_devices
            // 
            this.checkbox_Group_same_devices.AutoSize = true;
            this.checkbox_Group_same_devices.Location = new System.Drawing.Point(6, 382);
            this.checkbox_Group_same_devices.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkbox_Group_same_devices.Name = "checkbox_Group_same_devices";
            this.checkbox_Group_same_devices.Size = new System.Drawing.Size(97, 17);
            this.checkbox_Group_same_devices.TabIndex = 374;
            this.checkbox_Group_same_devices.Text = "Group Devices";
            this.checkbox_Group_same_devices.UseVisualStyleBackColor = true;
            // 
            // pictureBox_RunScriptOnCUDA_GPU_Lost
            // 
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.Location = new System.Drawing.Point(201, 366);
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.Name = "pictureBox_RunScriptOnCUDA_GPU_Lost";
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.TabIndex = 373;
            this.pictureBox_RunScriptOnCUDA_GPU_Lost.TabStop = false;
            // 
            // checkBox_RunScriptOnCUDA_GPU_Lost
            // 
            this.checkBox_RunScriptOnCUDA_GPU_Lost.AutoSize = true;
            this.checkBox_RunScriptOnCUDA_GPU_Lost.Location = new System.Drawing.Point(6, 366);
            this.checkBox_RunScriptOnCUDA_GPU_Lost.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_RunScriptOnCUDA_GPU_Lost.Name = "checkBox_RunScriptOnCUDA_GPU_Lost";
            this.checkBox_RunScriptOnCUDA_GPU_Lost.Size = new System.Drawing.Size(191, 17);
            this.checkBox_RunScriptOnCUDA_GPU_Lost.TabIndex = 372;
            this.checkBox_RunScriptOnCUDA_GPU_Lost.Text = "Run script when CUDA GPU is lost";
            this.checkBox_RunScriptOnCUDA_GPU_Lost.UseVisualStyleBackColor = true;
            // 
            // pictureBox_ShowInternetConnectionWarning
            // 
            this.pictureBox_ShowInternetConnectionWarning.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_ShowInternetConnectionWarning.Location = new System.Drawing.Point(201, 344);
            this.pictureBox_ShowInternetConnectionWarning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_ShowInternetConnectionWarning.Name = "pictureBox_ShowInternetConnectionWarning";
            this.pictureBox_ShowInternetConnectionWarning.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_ShowInternetConnectionWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_ShowInternetConnectionWarning.TabIndex = 371;
            this.pictureBox_ShowInternetConnectionWarning.TabStop = false;
            // 
            // checkBox_ShowInternetConnectionWarning
            // 
            this.checkBox_ShowInternetConnectionWarning.AutoSize = true;
            this.checkBox_ShowInternetConnectionWarning.Location = new System.Drawing.Point(6, 344);
            this.checkBox_ShowInternetConnectionWarning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_ShowInternetConnectionWarning.Name = "checkBox_ShowInternetConnectionWarning";
            this.checkBox_ShowInternetConnectionWarning.Size = new System.Drawing.Size(192, 17);
            this.checkBox_ShowInternetConnectionWarning.TabIndex = 370;
            this.checkBox_ShowInternetConnectionWarning.Text = "Show Internet Connection Warning";
            this.checkBox_ShowInternetConnectionWarning.UseVisualStyleBackColor = true;
            // 
            // checkBox_MinimizeMiningWindows
            // 
            this.checkBox_MinimizeMiningWindows.AutoSize = true;
            this.checkBox_MinimizeMiningWindows.Location = new System.Drawing.Point(6, 61);
            this.checkBox_MinimizeMiningWindows.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_MinimizeMiningWindows.Name = "checkBox_MinimizeMiningWindows";
            this.checkBox_MinimizeMiningWindows.Size = new System.Drawing.Size(141, 17);
            this.checkBox_MinimizeMiningWindows.TabIndex = 368;
            this.checkBox_MinimizeMiningWindows.Text = "MinimizeMiningWindows";
            this.checkBox_MinimizeMiningWindows.UseVisualStyleBackColor = true;
            // 
            // pictureBox_MinimizeMiningWindows
            // 
            this.pictureBox_MinimizeMiningWindows.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_MinimizeMiningWindows.Location = new System.Drawing.Point(201, 61);
            this.pictureBox_MinimizeMiningWindows.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_MinimizeMiningWindows.Name = "pictureBox_MinimizeMiningWindows";
            this.pictureBox_MinimizeMiningWindows.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_MinimizeMiningWindows.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_MinimizeMiningWindows.TabIndex = 369;
            this.pictureBox_MinimizeMiningWindows.TabStop = false;
            // 
            // pictureBox_RunAtStartup
            // 
            this.pictureBox_RunAtStartup.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_RunAtStartup.Location = new System.Drawing.Point(201, 322);
            this.pictureBox_RunAtStartup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_RunAtStartup.Name = "pictureBox_RunAtStartup";
            this.pictureBox_RunAtStartup.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_RunAtStartup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_RunAtStartup.TabIndex = 367;
            this.pictureBox_RunAtStartup.TabStop = false;
            // 
            // checkBox_RunAtStartup
            // 
            this.checkBox_RunAtStartup.AutoSize = true;
            this.checkBox_RunAtStartup.Location = new System.Drawing.Point(6, 322);
            this.checkBox_RunAtStartup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_RunAtStartup.Name = "checkBox_RunAtStartup";
            this.checkBox_RunAtStartup.Size = new System.Drawing.Size(120, 17);
            this.checkBox_RunAtStartup.TabIndex = 366;
            this.checkBox_RunAtStartup.Text = "Start With Windows";
            this.checkBox_RunAtStartup.UseVisualStyleBackColor = true;
            this.checkBox_RunAtStartup.CheckedChanged += new System.EventHandler(this.CheckBox_RunAtStartup_CheckedChanged);
            // 
            // checkBox_AllowMultipleInstances
            // 
            this.checkBox_AllowMultipleInstances.AutoSize = true;
            this.checkBox_AllowMultipleInstances.Location = new System.Drawing.Point(6, 300);
            this.checkBox_AllowMultipleInstances.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_AllowMultipleInstances.Name = "checkBox_AllowMultipleInstances";
            this.checkBox_AllowMultipleInstances.Size = new System.Drawing.Size(139, 17);
            this.checkBox_AllowMultipleInstances.TabIndex = 365;
            this.checkBox_AllowMultipleInstances.Text = "Allow Multiple Instances";
            this.checkBox_AllowMultipleInstances.UseVisualStyleBackColor = true;
            this.checkBox_AllowMultipleInstances.CheckedChanged += new System.EventHandler(this.CheckBox_DisableDefaultOptimizations_CheckedChanged);
            // 
            // checkBox_DisableDefaultOptimizations
            // 
            this.checkBox_DisableDefaultOptimizations.AutoSize = true;
            this.checkBox_DisableDefaultOptimizations.Location = new System.Drawing.Point(6, 279);
            this.checkBox_DisableDefaultOptimizations.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_DisableDefaultOptimizations.Name = "checkBox_DisableDefaultOptimizations";
            this.checkBox_DisableDefaultOptimizations.Size = new System.Drawing.Size(163, 17);
            this.checkBox_DisableDefaultOptimizations.TabIndex = 365;
            this.checkBox_DisableDefaultOptimizations.Text = "Disable Default Optimizations";
            this.checkBox_DisableDefaultOptimizations.UseVisualStyleBackColor = true;
            this.checkBox_DisableDefaultOptimizations.CheckedChanged += new System.EventHandler(this.CheckBox_DisableDefaultOptimizations_CheckedChanged);
            // 
            // checkBox_AMD_DisableAMDTempControl
            // 
            this.checkBox_AMD_DisableAMDTempControl.AutoSize = true;
            this.checkBox_AMD_DisableAMDTempControl.Location = new System.Drawing.Point(6, 258);
            this.checkBox_AMD_DisableAMDTempControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_AMD_DisableAMDTempControl.Name = "checkBox_AMD_DisableAMDTempControl";
            this.checkBox_AMD_DisableAMDTempControl.Size = new System.Drawing.Size(145, 17);
            this.checkBox_AMD_DisableAMDTempControl.TabIndex = 365;
            this.checkBox_AMD_DisableAMDTempControl.Text = "DisableAMDTempControl";
            this.checkBox_AMD_DisableAMDTempControl.UseVisualStyleBackColor = true;
            this.checkBox_AMD_DisableAMDTempControl.CheckedChanged += new System.EventHandler(this.CheckBox_AMD_DisableAMDTempControl_CheckedChanged);
            // 
            // checkBox_AutoStartMining
            // 
            this.checkBox_AutoStartMining.AutoSize = true;
            this.checkBox_AutoStartMining.Location = new System.Drawing.Point(6, 19);
            this.checkBox_AutoStartMining.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_AutoStartMining.Name = "checkBox_AutoStartMining";
            this.checkBox_AutoStartMining.Size = new System.Drawing.Size(102, 17);
            this.checkBox_AutoStartMining.TabIndex = 315;
            this.checkBox_AutoStartMining.Text = "Autostart Mining";
            this.checkBox_AutoStartMining.UseVisualStyleBackColor = true;
            // 
            // checkBox_HideMiningWindows
            // 
            this.checkBox_HideMiningWindows.AutoSize = true;
            this.checkBox_HideMiningWindows.Location = new System.Drawing.Point(6, 40);
            this.checkBox_HideMiningWindows.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_HideMiningWindows.Name = "checkBox_HideMiningWindows";
            this.checkBox_HideMiningWindows.Size = new System.Drawing.Size(123, 17);
            this.checkBox_HideMiningWindows.TabIndex = 315;
            this.checkBox_HideMiningWindows.Text = "HideMiningWindows";
            this.checkBox_HideMiningWindows.UseVisualStyleBackColor = true;
            // 
            // pictureBox_AllowMultipleInstances
            // 
            this.pictureBox_AllowMultipleInstances.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_AllowMultipleInstances.Location = new System.Drawing.Point(201, 300);
            this.pictureBox_AllowMultipleInstances.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_AllowMultipleInstances.Name = "pictureBox_AllowMultipleInstances";
            this.pictureBox_AllowMultipleInstances.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_AllowMultipleInstances.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_AllowMultipleInstances.TabIndex = 364;
            this.pictureBox_AllowMultipleInstances.TabStop = false;
            // 
            // checkBox_MinimizeToTray
            // 
            this.checkBox_MinimizeToTray.AutoSize = true;
            this.checkBox_MinimizeToTray.Location = new System.Drawing.Point(6, 83);
            this.checkBox_MinimizeToTray.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_MinimizeToTray.Name = "checkBox_MinimizeToTray";
            this.checkBox_MinimizeToTray.Size = new System.Drawing.Size(100, 17);
            this.checkBox_MinimizeToTray.TabIndex = 316;
            this.checkBox_MinimizeToTray.Text = "MinimizeToTray";
            this.checkBox_MinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DisableDefaultOptimizations
            // 
            this.pictureBox_DisableDefaultOptimizations.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_DisableDefaultOptimizations.Location = new System.Drawing.Point(201, 279);
            this.pictureBox_DisableDefaultOptimizations.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_DisableDefaultOptimizations.Name = "pictureBox_DisableDefaultOptimizations";
            this.pictureBox_DisableDefaultOptimizations.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_DisableDefaultOptimizations.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_DisableDefaultOptimizations.TabIndex = 364;
            this.pictureBox_DisableDefaultOptimizations.TabStop = false;
            // 
            // pictureBox_AMD_DisableAMDTempControl
            // 
            this.pictureBox_AMD_DisableAMDTempControl.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_AMD_DisableAMDTempControl.Location = new System.Drawing.Point(201, 258);
            this.pictureBox_AMD_DisableAMDTempControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_AMD_DisableAMDTempControl.Name = "pictureBox_AMD_DisableAMDTempControl";
            this.pictureBox_AMD_DisableAMDTempControl.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_AMD_DisableAMDTempControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_AMD_DisableAMDTempControl.TabIndex = 364;
            this.pictureBox_AMD_DisableAMDTempControl.TabStop = false;
            // 
            // pictureBox_NVIDIAP0State
            // 
            this.pictureBox_NVIDIAP0State.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_NVIDIAP0State.Location = new System.Drawing.Point(201, 236);
            this.pictureBox_NVIDIAP0State.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_NVIDIAP0State.Name = "pictureBox_NVIDIAP0State";
            this.pictureBox_NVIDIAP0State.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_NVIDIAP0State.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_NVIDIAP0State.TabIndex = 364;
            this.pictureBox_NVIDIAP0State.TabStop = false;
            // 
            // pictureBox_DisableWindowsErrorReporting
            // 
            this.pictureBox_DisableWindowsErrorReporting.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_DisableWindowsErrorReporting.Location = new System.Drawing.Point(201, 215);
            this.pictureBox_DisableWindowsErrorReporting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_DisableWindowsErrorReporting.Name = "pictureBox_DisableWindowsErrorReporting";
            this.pictureBox_DisableWindowsErrorReporting.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_DisableWindowsErrorReporting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_DisableWindowsErrorReporting.TabIndex = 364;
            this.pictureBox_DisableWindowsErrorReporting.TabStop = false;
            // 
            // pictureBox_ShowDriverVersionWarning
            // 
            this.pictureBox_ShowDriverVersionWarning.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_ShowDriverVersionWarning.Location = new System.Drawing.Point(201, 191);
            this.pictureBox_ShowDriverVersionWarning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_ShowDriverVersionWarning.Name = "pictureBox_ShowDriverVersionWarning";
            this.pictureBox_ShowDriverVersionWarning.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_ShowDriverVersionWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_ShowDriverVersionWarning.TabIndex = 364;
            this.pictureBox_ShowDriverVersionWarning.TabStop = false;
            // 
            // pictureBox_StartMiningWhenIdle
            // 
            this.pictureBox_StartMiningWhenIdle.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_StartMiningWhenIdle.Location = new System.Drawing.Point(201, 169);
            this.pictureBox_StartMiningWhenIdle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_StartMiningWhenIdle.Name = "pictureBox_StartMiningWhenIdle";
            this.pictureBox_StartMiningWhenIdle.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_StartMiningWhenIdle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_StartMiningWhenIdle.TabIndex = 364;
            this.pictureBox_StartMiningWhenIdle.TabStop = false;
            // 
            // pictureBox_AutoScaleBTCValues
            // 
            this.pictureBox_AutoScaleBTCValues.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_AutoScaleBTCValues.Location = new System.Drawing.Point(754, 19);
            this.pictureBox_AutoScaleBTCValues.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_AutoScaleBTCValues.Name = "pictureBox_AutoScaleBTCValues";
            this.pictureBox_AutoScaleBTCValues.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_AutoScaleBTCValues.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_AutoScaleBTCValues.TabIndex = 364;
            this.pictureBox_AutoScaleBTCValues.TabStop = false;
            this.pictureBox_AutoScaleBTCValues.Visible = false;
            // 
            // pictureBox_DisableDetectionAMD
            // 
            this.pictureBox_DisableDetectionAMD.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_DisableDetectionAMD.Location = new System.Drawing.Point(201, 147);
            this.pictureBox_DisableDetectionAMD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_DisableDetectionAMD.Name = "pictureBox_DisableDetectionAMD";
            this.pictureBox_DisableDetectionAMD.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_DisableDetectionAMD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_DisableDetectionAMD.TabIndex = 364;
            this.pictureBox_DisableDetectionAMD.TabStop = false;
            // 
            // pictureBox_Use3rdPartyMiners
            // 
            this.pictureBox_Use3rdPartyMiners.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_Use3rdPartyMiners.Location = new System.Drawing.Point(201, 103);
            this.pictureBox_Use3rdPartyMiners.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_Use3rdPartyMiners.Name = "pictureBox_Use3rdPartyMiners";
            this.pictureBox_Use3rdPartyMiners.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_Use3rdPartyMiners.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Use3rdPartyMiners.TabIndex = 364;
            this.pictureBox_Use3rdPartyMiners.TabStop = false;
            // 
            // pictureBox_DisableDetectionNVIDIA
            // 
            this.pictureBox_DisableDetectionNVIDIA.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_DisableDetectionNVIDIA.Location = new System.Drawing.Point(201, 125);
            this.pictureBox_DisableDetectionNVIDIA.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_DisableDetectionNVIDIA.Name = "pictureBox_DisableDetectionNVIDIA";
            this.pictureBox_DisableDetectionNVIDIA.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_DisableDetectionNVIDIA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_DisableDetectionNVIDIA.TabIndex = 364;
            this.pictureBox_DisableDetectionNVIDIA.TabStop = false;
            // 
            // pictureBox_AutoStartMining
            // 
            this.pictureBox_AutoStartMining.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_AutoStartMining.Location = new System.Drawing.Point(201, 19);
            this.pictureBox_AutoStartMining.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_AutoStartMining.Name = "pictureBox_AutoStartMining";
            this.pictureBox_AutoStartMining.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_AutoStartMining.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_AutoStartMining.TabIndex = 364;
            this.pictureBox_AutoStartMining.TabStop = false;
            // 
            // pictureBox_MinimizeToTray
            // 
            this.pictureBox_MinimizeToTray.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_MinimizeToTray.Location = new System.Drawing.Point(201, 83);
            this.pictureBox_MinimizeToTray.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_MinimizeToTray.Name = "pictureBox_MinimizeToTray";
            this.pictureBox_MinimizeToTray.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_MinimizeToTray.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_MinimizeToTray.TabIndex = 364;
            this.pictureBox_MinimizeToTray.TabStop = false;
            // 
            // pictureBox_HideMiningWindows
            // 
            this.pictureBox_HideMiningWindows.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_HideMiningWindows.Location = new System.Drawing.Point(201, 40);
            this.pictureBox_HideMiningWindows.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_HideMiningWindows.Name = "pictureBox_HideMiningWindows";
            this.pictureBox_HideMiningWindows.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_HideMiningWindows.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_HideMiningWindows.TabIndex = 364;
            this.pictureBox_HideMiningWindows.TabStop = false;
            // 
            // checkBox_Use3rdPartyMiners
            // 
            this.checkBox_Use3rdPartyMiners.AutoSize = true;
            this.checkBox_Use3rdPartyMiners.Location = new System.Drawing.Point(6, 104);
            this.checkBox_Use3rdPartyMiners.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_Use3rdPartyMiners.Name = "checkBox_Use3rdPartyMiners";
            this.checkBox_Use3rdPartyMiners.Size = new System.Drawing.Size(129, 17);
            this.checkBox_Use3rdPartyMiners.TabIndex = 319;
            this.checkBox_Use3rdPartyMiners.Text = "Enable3rdPartyMiners";
            this.checkBox_Use3rdPartyMiners.UseVisualStyleBackColor = true;
            this.checkBox_Use3rdPartyMiners.CheckedChanged += new System.EventHandler(this.CheckBox_Use3rdPartyMiners_CheckedChanged);
            // 
            // checkBox_DisableDetectionNVIDIA
            // 
            this.checkBox_DisableDetectionNVIDIA.AutoSize = true;
            this.checkBox_DisableDetectionNVIDIA.Location = new System.Drawing.Point(6, 125);
            this.checkBox_DisableDetectionNVIDIA.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_DisableDetectionNVIDIA.Name = "checkBox_DisableDetectionNVIDIA";
            this.checkBox_DisableDetectionNVIDIA.Size = new System.Drawing.Size(143, 17);
            this.checkBox_DisableDetectionNVIDIA.TabIndex = 319;
            this.checkBox_DisableDetectionNVIDIA.Text = "DisableDetectionNVIDIA";
            this.checkBox_DisableDetectionNVIDIA.UseVisualStyleBackColor = true;
            // 
            // checkBox_DisableDetectionAMD
            // 
            this.checkBox_DisableDetectionAMD.AutoSize = true;
            this.checkBox_DisableDetectionAMD.Location = new System.Drawing.Point(6, 147);
            this.checkBox_DisableDetectionAMD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_DisableDetectionAMD.Name = "checkBox_DisableDetectionAMD";
            this.checkBox_DisableDetectionAMD.Size = new System.Drawing.Size(131, 17);
            this.checkBox_DisableDetectionAMD.TabIndex = 320;
            this.checkBox_DisableDetectionAMD.Text = "DisableDetectionAMD";
            this.checkBox_DisableDetectionAMD.UseVisualStyleBackColor = true;
            // 
            // checkBox_NVIDIAP0State
            // 
            this.checkBox_NVIDIAP0State.AutoSize = true;
            this.checkBox_NVIDIAP0State.Location = new System.Drawing.Point(6, 236);
            this.checkBox_NVIDIAP0State.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_NVIDIAP0State.Name = "checkBox_NVIDIAP0State";
            this.checkBox_NVIDIAP0State.Size = new System.Drawing.Size(100, 17);
            this.checkBox_NVIDIAP0State.TabIndex = 326;
            this.checkBox_NVIDIAP0State.Text = "NVIDIAP0State";
            this.checkBox_NVIDIAP0State.UseVisualStyleBackColor = true;
            // 
            // checkBox_AutoScaleBTCValues
            // 
            this.checkBox_AutoScaleBTCValues.AutoSize = true;
            this.checkBox_AutoScaleBTCValues.Location = new System.Drawing.Point(538, 19);
            this.checkBox_AutoScaleBTCValues.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_AutoScaleBTCValues.Name = "checkBox_AutoScaleBTCValues";
            this.checkBox_AutoScaleBTCValues.Size = new System.Drawing.Size(128, 17);
            this.checkBox_AutoScaleBTCValues.TabIndex = 321;
            this.checkBox_AutoScaleBTCValues.Text = "AutoScaleBTCValues";
            this.checkBox_AutoScaleBTCValues.UseVisualStyleBackColor = true;
            this.checkBox_AutoScaleBTCValues.Visible = false;
            // 
            // checkBox_DisableWindowsErrorReporting
            // 
            this.checkBox_DisableWindowsErrorReporting.AutoSize = true;
            this.checkBox_DisableWindowsErrorReporting.Location = new System.Drawing.Point(6, 215);
            this.checkBox_DisableWindowsErrorReporting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_DisableWindowsErrorReporting.Name = "checkBox_DisableWindowsErrorReporting";
            this.checkBox_DisableWindowsErrorReporting.Size = new System.Drawing.Size(173, 17);
            this.checkBox_DisableWindowsErrorReporting.TabIndex = 324;
            this.checkBox_DisableWindowsErrorReporting.Text = "DisableWindowsErrorReporting";
            this.checkBox_DisableWindowsErrorReporting.UseVisualStyleBackColor = true;
            // 
            // checkBox_StartMiningWhenIdle
            // 
            this.checkBox_StartMiningWhenIdle.AutoSize = true;
            this.checkBox_StartMiningWhenIdle.Location = new System.Drawing.Point(6, 169);
            this.checkBox_StartMiningWhenIdle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_StartMiningWhenIdle.Name = "checkBox_StartMiningWhenIdle";
            this.checkBox_StartMiningWhenIdle.Size = new System.Drawing.Size(125, 17);
            this.checkBox_StartMiningWhenIdle.TabIndex = 322;
            this.checkBox_StartMiningWhenIdle.Text = "StartMiningWhenIdle";
            this.checkBox_StartMiningWhenIdle.UseVisualStyleBackColor = true;
            // 
            // checkBox_ShowDriverVersionWarning
            // 
            this.checkBox_ShowDriverVersionWarning.AutoSize = true;
            this.checkBox_ShowDriverVersionWarning.Location = new System.Drawing.Point(6, 191);
            this.checkBox_ShowDriverVersionWarning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_ShowDriverVersionWarning.Name = "checkBox_ShowDriverVersionWarning";
            this.checkBox_ShowDriverVersionWarning.Size = new System.Drawing.Size(156, 17);
            this.checkBox_ShowDriverVersionWarning.TabIndex = 323;
            this.checkBox_ShowDriverVersionWarning.Text = "ShowDriverVersionWarning";
            this.checkBox_ShowDriverVersionWarning.UseVisualStyleBackColor = true;
            // 
            // groupBox_Logging
            // 
            this.groupBox_Logging.Controls.Add(this.label_LogMaxFileSize);
            this.groupBox_Logging.Controls.Add(this.textBox_LogMaxFileSize);
            this.groupBox_Logging.Controls.Add(this.checkBox_LogToFile);
            this.groupBox_Logging.Controls.Add(this.pictureBox_DebugConsole);
            this.groupBox_Logging.Controls.Add(this.pictureBox_LogMaxFileSize);
            this.groupBox_Logging.Controls.Add(this.pictureBox_LogToFile);
            this.groupBox_Logging.Controls.Add(this.checkBox_DebugConsole);
            this.groupBox_Logging.Location = new System.Drawing.Point(6, 304);
            this.groupBox_Logging.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Logging.Name = "groupBox_Logging";
            this.groupBox_Logging.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Logging.Size = new System.Drawing.Size(206, 110);
            this.groupBox_Logging.TabIndex = 388;
            this.groupBox_Logging.TabStop = false;
            this.groupBox_Logging.Text = "Logging:";
            // 
            // label_LogMaxFileSize
            // 
            this.label_LogMaxFileSize.AutoSize = true;
            this.label_LogMaxFileSize.Location = new System.Drawing.Point(4, 62);
            this.label_LogMaxFileSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_LogMaxFileSize.Name = "label_LogMaxFileSize";
            this.label_LogMaxFileSize.Size = new System.Drawing.Size(84, 13);
            this.label_LogMaxFileSize.TabIndex = 357;
            this.label_LogMaxFileSize.Text = "LogMaxFileSize:";
            // 
            // textBox_LogMaxFileSize
            // 
            this.textBox_LogMaxFileSize.Location = new System.Drawing.Point(4, 84);
            this.textBox_LogMaxFileSize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_LogMaxFileSize.Name = "textBox_LogMaxFileSize";
            this.textBox_LogMaxFileSize.Size = new System.Drawing.Size(160, 20);
            this.textBox_LogMaxFileSize.TabIndex = 334;
            // 
            // checkBox_LogToFile
            // 
            this.checkBox_LogToFile.AutoSize = true;
            this.checkBox_LogToFile.Location = new System.Drawing.Point(6, 19);
            this.checkBox_LogToFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_LogToFile.Name = "checkBox_LogToFile";
            this.checkBox_LogToFile.Size = new System.Drawing.Size(72, 17);
            this.checkBox_LogToFile.TabIndex = 327;
            this.checkBox_LogToFile.Text = "Log to file";
            this.checkBox_LogToFile.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DebugConsole
            // 
            this.pictureBox_DebugConsole.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_DebugConsole.Location = new System.Drawing.Point(146, 41);
            this.pictureBox_DebugConsole.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_DebugConsole.Name = "pictureBox_DebugConsole";
            this.pictureBox_DebugConsole.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_DebugConsole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_DebugConsole.TabIndex = 364;
            this.pictureBox_DebugConsole.TabStop = false;
            // 
            // pictureBox_LogMaxFileSize
            // 
            this.pictureBox_LogMaxFileSize.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_LogMaxFileSize.Location = new System.Drawing.Point(146, 62);
            this.pictureBox_LogMaxFileSize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_LogMaxFileSize.Name = "pictureBox_LogMaxFileSize";
            this.pictureBox_LogMaxFileSize.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_LogMaxFileSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_LogMaxFileSize.TabIndex = 364;
            this.pictureBox_LogMaxFileSize.TabStop = false;
            // 
            // pictureBox_LogToFile
            // 
            this.pictureBox_LogToFile.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_LogToFile.Location = new System.Drawing.Point(146, 18);
            this.pictureBox_LogToFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_LogToFile.Name = "pictureBox_LogToFile";
            this.pictureBox_LogToFile.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_LogToFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_LogToFile.TabIndex = 364;
            this.pictureBox_LogToFile.TabStop = false;
            // 
            // checkBox_DebugConsole
            // 
            this.checkBox_DebugConsole.AutoSize = true;
            this.checkBox_DebugConsole.Location = new System.Drawing.Point(6, 42);
            this.checkBox_DebugConsole.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_DebugConsole.Name = "checkBox_DebugConsole";
            this.checkBox_DebugConsole.Size = new System.Drawing.Size(96, 17);
            this.checkBox_DebugConsole.TabIndex = 313;
            this.checkBox_DebugConsole.Text = "DebugConsole";
            this.checkBox_DebugConsole.UseVisualStyleBackColor = true;
            // 
            // groupBox_Main
            // 
            this.groupBox_Main.Controls.Add(this.pictureBox_TimeUnit);
            this.groupBox_Main.Controls.Add(this.label_TimeUnit);
            this.groupBox_Main.Controls.Add(this.pictureBox_displayCurrency);
            this.groupBox_Main.Controls.Add(this.comboBox_TimeUnit);
            this.groupBox_Main.Controls.Add(this.checkBox_IdleWhenNoInternetAccess);
            this.groupBox_Main.Controls.Add(this.currencyConverterCombobox);
            this.groupBox_Main.Controls.Add(this.pictureBox_MinProfit);
            this.groupBox_Main.Controls.Add(this.label_displayCurrency);
            this.groupBox_Main.Controls.Add(this.textBox_MinProfit);
            this.groupBox_Main.Controls.Add(this.pictureBox_IdleWhenNoInternetAccess);
            this.groupBox_Main.Controls.Add(this.label_MinProfit);
            this.groupBox_Main.Location = new System.Drawing.Point(6, 6);
            this.groupBox_Main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Main.Name = "groupBox_Main";
            this.groupBox_Main.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox_Main.Size = new System.Drawing.Size(206, 186);
            this.groupBox_Main.TabIndex = 386;
            this.groupBox_Main.TabStop = false;
            this.groupBox_Main.Text = "Main:";
            // 
            // pictureBox_TimeUnit
            // 
            this.pictureBox_TimeUnit.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_TimeUnit.Location = new System.Drawing.Point(146, 16);
            this.pictureBox_TimeUnit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_TimeUnit.Name = "pictureBox_TimeUnit";
            this.pictureBox_TimeUnit.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_TimeUnit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_TimeUnit.TabIndex = 372;
            this.pictureBox_TimeUnit.TabStop = false;
            // 
            // label_TimeUnit
            // 
            this.label_TimeUnit.AutoSize = true;
            this.label_TimeUnit.Location = new System.Drawing.Point(4, 16);
            this.label_TimeUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_TimeUnit.Name = "label_TimeUnit";
            this.label_TimeUnit.Size = new System.Drawing.Size(52, 13);
            this.label_TimeUnit.TabIndex = 371;
            this.label_TimeUnit.Text = "TimeUnit:";
            // 
            // pictureBox_displayCurrency
            // 
            this.pictureBox_displayCurrency.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_displayCurrency.Location = new System.Drawing.Point(106, 132);
            this.pictureBox_displayCurrency.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_displayCurrency.Name = "pictureBox_displayCurrency";
            this.pictureBox_displayCurrency.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_displayCurrency.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_displayCurrency.TabIndex = 364;
            this.pictureBox_displayCurrency.TabStop = false;
            // 
            // comboBox_TimeUnit
            // 
            this.comboBox_TimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TimeUnit.FormattingEnabled = true;
            this.comboBox_TimeUnit.Location = new System.Drawing.Point(4, 37);
            this.comboBox_TimeUnit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox_TimeUnit.Name = "comboBox_TimeUnit";
            this.comboBox_TimeUnit.Size = new System.Drawing.Size(160, 21);
            this.comboBox_TimeUnit.TabIndex = 370;
            // 
            // checkBox_IdleWhenNoInternetAccess
            // 
            this.checkBox_IdleWhenNoInternetAccess.AutoSize = true;
            this.checkBox_IdleWhenNoInternetAccess.Location = new System.Drawing.Point(4, 111);
            this.checkBox_IdleWhenNoInternetAccess.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox_IdleWhenNoInternetAccess.Name = "checkBox_IdleWhenNoInternetAccess";
            this.checkBox_IdleWhenNoInternetAccess.Size = new System.Drawing.Size(169, 17);
            this.checkBox_IdleWhenNoInternetAccess.TabIndex = 365;
            this.checkBox_IdleWhenNoInternetAccess.Text = "Idle When No Internet Access";
            this.checkBox_IdleWhenNoInternetAccess.UseVisualStyleBackColor = true;
            this.checkBox_IdleWhenNoInternetAccess.CheckedChanged += new System.EventHandler(this.GeneralCheckBoxes_CheckedChanged);
            // 
            // currencyConverterCombobox
            // 
            this.currencyConverterCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currencyConverterCombobox.FormattingEnabled = true;
            this.currencyConverterCombobox.Items.AddRange(new object[] {
            "AUD",
            "BRL",
            "CAD",
            "CHF",
            "CLP",
            "CNY",
            "DKK",
            "EUR",
            "GBP",
            "HKD",
            "INR",
            "ISK",
            "JPY",
            "KRW",
            "NZD",
            "PLN",
            "RUB",
            "SEK",
            "SGD",
            "THB",
            "TWD",
            "USD"});
            this.currencyConverterCombobox.Location = new System.Drawing.Point(4, 152);
            this.currencyConverterCombobox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.currencyConverterCombobox.Name = "currencyConverterCombobox";
            this.currencyConverterCombobox.Size = new System.Drawing.Size(160, 21);
            this.currencyConverterCombobox.Sorted = true;
            this.currencyConverterCombobox.TabIndex = 381;
            this.currencyConverterCombobox.SelectedIndexChanged += new System.EventHandler(this.CurrencyConverterCombobox_SelectedIndexChanged);
            // 
            // pictureBox_MinProfit
            // 
            this.pictureBox_MinProfit.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_MinProfit.Location = new System.Drawing.Point(146, 65);
            this.pictureBox_MinProfit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_MinProfit.Name = "pictureBox_MinProfit";
            this.pictureBox_MinProfit.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_MinProfit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_MinProfit.TabIndex = 364;
            this.pictureBox_MinProfit.TabStop = false;
            // 
            // label_displayCurrency
            // 
            this.label_displayCurrency.AutoSize = true;
            this.label_displayCurrency.Location = new System.Drawing.Point(4, 132);
            this.label_displayCurrency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_displayCurrency.Name = "label_displayCurrency";
            this.label_displayCurrency.Size = new System.Drawing.Size(89, 13);
            this.label_displayCurrency.TabIndex = 382;
            this.label_displayCurrency.Text = "Display Currency:";
            // 
            // textBox_MinProfit
            // 
            this.textBox_MinProfit.Location = new System.Drawing.Point(4, 85);
            this.textBox_MinProfit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox_MinProfit.Name = "textBox_MinProfit";
            this.textBox_MinProfit.Size = new System.Drawing.Size(160, 20);
            this.textBox_MinProfit.TabIndex = 334;
            // 
            // pictureBox_IdleWhenNoInternetAccess
            // 
            this.pictureBox_IdleWhenNoInternetAccess.Image = global::zPoolMiner.Properties.Resources.info_black_18;
            this.pictureBox_IdleWhenNoInternetAccess.Location = new System.Drawing.Point(177, 110);
            this.pictureBox_IdleWhenNoInternetAccess.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_IdleWhenNoInternetAccess.Name = "pictureBox_IdleWhenNoInternetAccess";
            this.pictureBox_IdleWhenNoInternetAccess.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_IdleWhenNoInternetAccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_IdleWhenNoInternetAccess.TabIndex = 364;
            this.pictureBox_IdleWhenNoInternetAccess.TabStop = false;
            // 
            // label_MinProfit
            // 
            this.label_MinProfit.AutoSize = true;
            this.label_MinProfit.Location = new System.Drawing.Point(4, 65);
            this.label_MinProfit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MinProfit.Name = "label_MinProfit";
            this.label_MinProfit.Size = new System.Drawing.Size(115, 13);
            this.label_MinProfit.TabIndex = 357;
            this.label_MinProfit.Text = "Minimum Profit ($/day):";
            // 
            // tabPageDevicesAlgos
            // 
            this.tabPageDevicesAlgos.BackColor = System.Drawing.Color.White;
            this.tabPageDevicesAlgos.Controls.Add(this.groupBoxAlgorithmSettings);
            this.tabPageDevicesAlgos.Controls.Add(this.devicesListViewEnableControl1);
            this.tabPageDevicesAlgos.Controls.Add(this.algorithmSettingsControl1);
            this.tabPageDevicesAlgos.Location = new System.Drawing.Point(4, 22);
            this.tabPageDevicesAlgos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageDevicesAlgos.Name = "tabPageDevicesAlgos";
            this.tabPageDevicesAlgos.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageDevicesAlgos.Size = new System.Drawing.Size(618, 414);
            this.tabPageDevicesAlgos.TabIndex = 1;
            this.tabPageDevicesAlgos.Text = "Devices/Algorithms";
            // 
            // groupBoxAlgorithmSettings
            // 
            this.groupBoxAlgorithmSettings.Controls.Add(this.algorithmsListView1);
            this.groupBoxAlgorithmSettings.Location = new System.Drawing.Point(6, 201);
            this.groupBoxAlgorithmSettings.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBoxAlgorithmSettings.Name = "groupBoxAlgorithmSettings";
            this.groupBoxAlgorithmSettings.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBoxAlgorithmSettings.Size = new System.Drawing.Size(598, 207);
            this.groupBoxAlgorithmSettings.TabIndex = 395;
            this.groupBoxAlgorithmSettings.TabStop = false;
            this.groupBoxAlgorithmSettings.Text = "Algorithm settings for selected device:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(618, 414);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Pool Selection";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.textBox_blazepool_Worker);
            this.panel3.Controls.Add(this.textBox_blockmunch_Worker);
            this.panel3.Controls.Add(this.textBox_starpool_Worker);
            this.panel3.Controls.Add(this.textBox_minemoney_Worker);
            this.panel3.Controls.Add(this.textBox_zerg_Worker);
            this.panel3.Controls.Add(this.textBox_hashrefinery_worker);
            this.panel3.Controls.Add(this.textBox_AhashPool_Worker);
            this.panel3.Controls.Add(this.textBox_Zpool_Worker);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.textBox_MPH_Worker);
            this.panel3.Controls.Add(this.textBox_NiceHash_Worker);
            this.panel3.Location = new System.Drawing.Point(497, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 407);
            this.panel3.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.Control;
            this.label17.Location = new System.Drawing.Point(3, 230);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Passwords";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_blazepool_Worker
            // 
            this.textBox_blazepool_Worker.Enabled = false;
            this.textBox_blazepool_Worker.Location = new System.Drawing.Point(3, 205);
            this.textBox_blazepool_Worker.Name = "textBox_blazepool_Worker";
            this.textBox_blazepool_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_blazepool_Worker.TabIndex = 21;
            this.textBox_blazepool_Worker.Text = "c=BTC,Worker1";
            // 
            // textBox_blockmunch_Worker
            // 
            this.textBox_blockmunch_Worker.Enabled = false;
            this.textBox_blockmunch_Worker.Location = new System.Drawing.Point(3, 182);
            this.textBox_blockmunch_Worker.Name = "textBox_blockmunch_Worker";
            this.textBox_blockmunch_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_blockmunch_Worker.TabIndex = 20;
            this.textBox_blockmunch_Worker.Text = "c=BTC,Worker1";
            // 
            // textBox_starpool_Worker
            // 
            this.textBox_starpool_Worker.Enabled = false;
            this.textBox_starpool_Worker.Location = new System.Drawing.Point(3, 159);
            this.textBox_starpool_Worker.Name = "textBox_starpool_Worker";
            this.textBox_starpool_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_starpool_Worker.TabIndex = 19;
            this.textBox_starpool_Worker.Text = "c=BTC,Worker1";
            // 
            // textBox_minemoney_Worker
            // 
            this.textBox_minemoney_Worker.Enabled = false;
            this.textBox_minemoney_Worker.Location = new System.Drawing.Point(3, 136);
            this.textBox_minemoney_Worker.Name = "textBox_minemoney_Worker";
            this.textBox_minemoney_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_minemoney_Worker.TabIndex = 18;
            this.textBox_minemoney_Worker.Text = "c=BTC,Worker1";
            // 
            // textBox_zerg_Worker
            // 
            this.textBox_zerg_Worker.Location = new System.Drawing.Point(3, 113);
            this.textBox_zerg_Worker.Name = "textBox_zerg_Worker";
            this.textBox_zerg_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_zerg_Worker.TabIndex = 15;
            this.textBox_zerg_Worker.Text = "c=BTC,Worker1";
            // 
            // textBox_hashrefinery_worker
            // 
            this.textBox_hashrefinery_worker.Enabled = false;
            this.textBox_hashrefinery_worker.Location = new System.Drawing.Point(3, 66);
            this.textBox_hashrefinery_worker.Name = "textBox_hashrefinery_worker";
            this.textBox_hashrefinery_worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_hashrefinery_worker.TabIndex = 17;
            this.textBox_hashrefinery_worker.Text = "c=BTC,Worker1";
            // 
            // textBox_AhashPool_Worker
            // 
            this.textBox_AhashPool_Worker.Enabled = false;
            this.textBox_AhashPool_Worker.Location = new System.Drawing.Point(3, 43);
            this.textBox_AhashPool_Worker.Name = "textBox_AhashPool_Worker";
            this.textBox_AhashPool_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_AhashPool_Worker.TabIndex = 16;
            this.textBox_AhashPool_Worker.Text = "c=BTC,Worker1";
            // 
            // textBox_Zpool_Worker
            // 
            this.textBox_Zpool_Worker.Enabled = false;
            this.textBox_Zpool_Worker.Location = new System.Drawing.Point(3, 16);
            this.textBox_Zpool_Worker.Name = "textBox_Zpool_Worker";
            this.textBox_Zpool_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_Zpool_Worker.TabIndex = 15;
            this.textBox_Zpool_Worker.Text = "c=BTC,Worker1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(-1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Workers";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_MPH_Worker
            // 
            this.textBox_MPH_Worker.Enabled = false;
            this.textBox_MPH_Worker.Location = new System.Drawing.Point(2, 245);
            this.textBox_MPH_Worker.Name = "textBox_MPH_Worker";
            this.textBox_MPH_Worker.Size = new System.Drawing.Size(110, 20);
            this.textBox_MPH_Worker.TabIndex = 14;
            this.textBox_MPH_Worker.Text = "Worker1";
            // 
            // textBox_NiceHash_Worker
            // 
            this.textBox_NiceHash_Worker.Enabled = false;
            this.textBox_NiceHash_Worker.Location = new System.Drawing.Point(3, 89);
            this.textBox_NiceHash_Worker.Name = "textBox_NiceHash_Worker";
            this.textBox_NiceHash_Worker.Size = new System.Drawing.Size(111, 20);
            this.textBox_NiceHash_Worker.TabIndex = 12;
            this.textBox_NiceHash_Worker.Text = "Worker1";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.textBox_blazepool_Wallet);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.textBox_blockmunch_Wallet);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.textBox_starpool_Wallet);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox_minemoney_Wallet);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBox_zerg_Wallet);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox_MPH_Wallet);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox_Zpool_Wallet);
            this.panel2.Controls.Add(this.textBox_NiceHash_Wallet);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBox_AhashPool_Wallet);
            this.panel2.Controls.Add(this.textBox_HashRefinery_Wallet);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(141, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 408);
            this.panel2.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.Control;
            this.label18.Location = new System.Drawing.Point(-1, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(350, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "Pools Using Wallets For Usernames";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.Control;
            this.label16.Location = new System.Drawing.Point(-1, 230);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(350, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "The Following Pools Require Usernames";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(6, 209);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "BlazePool";
            // 
            // textBox_blazepool_Wallet
            // 
            this.textBox_blazepool_Wallet.Enabled = false;
            this.textBox_blazepool_Wallet.Location = new System.Drawing.Point(103, 207);
            this.textBox_blazepool_Wallet.Name = "textBox_blazepool_Wallet";
            this.textBox_blazepool_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_blazepool_Wallet.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(6, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "BlockMunch";
            // 
            // textBox_blockmunch_Wallet
            // 
            this.textBox_blockmunch_Wallet.Enabled = false;
            this.textBox_blockmunch_Wallet.Location = new System.Drawing.Point(103, 184);
            this.textBox_blockmunch_Wallet.Name = "textBox_blockmunch_Wallet";
            this.textBox_blockmunch_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_blockmunch_Wallet.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(6, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "StarPool";
            // 
            // textBox_starpool_Wallet
            // 
            this.textBox_starpool_Wallet.Enabled = false;
            this.textBox_starpool_Wallet.Location = new System.Drawing.Point(103, 161);
            this.textBox_starpool_Wallet.Name = "textBox_starpool_Wallet";
            this.textBox_starpool_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_starpool_Wallet.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(6, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "MineMoney";
            // 
            // textBox_minemoney_Wallet
            // 
            this.textBox_minemoney_Wallet.Enabled = false;
            this.textBox_minemoney_Wallet.Location = new System.Drawing.Point(103, 137);
            this.textBox_minemoney_Wallet.Name = "textBox_minemoney_Wallet";
            this.textBox_minemoney_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_minemoney_Wallet.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "ZergPool";
            // 
            // textBox_zerg_Wallet
            // 
            this.textBox_zerg_Wallet.Location = new System.Drawing.Point(103, 114);
            this.textBox_zerg_Wallet.Name = "textBox_zerg_Wallet";
            this.textBox_zerg_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_zerg_Wallet.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(6, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "MiningPoolHub";
            // 
            // textBox_MPH_Wallet
            // 
            this.textBox_MPH_Wallet.Enabled = false;
            this.textBox_MPH_Wallet.Location = new System.Drawing.Point(103, 246);
            this.textBox_MPH_Wallet.Name = "textBox_MPH_Wallet";
            this.textBox_MPH_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_MPH_Wallet.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(6, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "NiceHash";
            // 
            // textBox_Zpool_Wallet
            // 
            this.textBox_Zpool_Wallet.Enabled = false;
            this.textBox_Zpool_Wallet.Location = new System.Drawing.Point(103, 17);
            this.textBox_Zpool_Wallet.Name = "textBox_Zpool_Wallet";
            this.textBox_Zpool_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_Zpool_Wallet.TabIndex = 2;
            // 
            // textBox_NiceHash_Wallet
            // 
            this.textBox_NiceHash_Wallet.Enabled = false;
            this.textBox_NiceHash_Wallet.Location = new System.Drawing.Point(103, 91);
            this.textBox_NiceHash_Wallet.Name = "textBox_NiceHash_Wallet";
            this.textBox_NiceHash_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_NiceHash_Wallet.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Zpool.ca";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "HashRefinery";
            // 
            // textBox_AhashPool_Wallet
            // 
            this.textBox_AhashPool_Wallet.Enabled = false;
            this.textBox_AhashPool_Wallet.Location = new System.Drawing.Point(103, 43);
            this.textBox_AhashPool_Wallet.Name = "textBox_AhashPool_Wallet";
            this.textBox_AhashPool_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_AhashPool_Wallet.TabIndex = 4;
            // 
            // textBox_HashRefinery_Wallet
            // 
            this.textBox_HashRefinery_Wallet.Enabled = false;
            this.textBox_HashRefinery_Wallet.Location = new System.Drawing.Point(103, 67);
            this.textBox_HashRefinery_Wallet.Name = "textBox_HashRefinery_Wallet";
            this.textBox_HashRefinery_Wallet.Size = new System.Drawing.Size(239, 20);
            this.textBox_HashRefinery_Wallet.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "AhashPool";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBox_blazepool);
            this.panel1.Controls.Add(this.checkBox_blockmunch);
            this.panel1.Controls.Add(this.checkBox_starpool);
            this.panel1.Controls.Add(this.checkBox_zerg);
            this.panel1.Controls.Add(this.checkBox_minemoney);
            this.panel1.Controls.Add(this.checkBox_MPH);
            this.panel1.Controls.Add(this.checkBox_NiceHash);
            this.panel1.Controls.Add(this.checkBox_HashRefinery);
            this.panel1.Controls.Add(this.checkBox_AhashPool);
            this.panel1.Controls.Add(this.checkBox_zpool);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(132, 408);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 69);
            this.button1.TabIndex = 17;
            this.button1.Text = "Test Button For integrated Browser";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(-1, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 14);
            this.label2.TabIndex = 371;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_blazepool
            // 
            this.checkBox_blazepool.AutoSize = true;
            this.checkBox_blazepool.Enabled = false;
            this.checkBox_blazepool.Location = new System.Drawing.Point(11, 208);
            this.checkBox_blazepool.Name = "checkBox_blazepool";
            this.checkBox_blazepool.Size = new System.Drawing.Size(73, 17);
            this.checkBox_blazepool.TabIndex = 370;
            this.checkBox_blazepool.Text = "BlazePool";
            this.checkBox_blazepool.UseVisualStyleBackColor = true;
            // 
            // checkBox_blockmunch
            // 
            this.checkBox_blockmunch.AutoSize = true;
            this.checkBox_blockmunch.Enabled = false;
            this.checkBox_blockmunch.Location = new System.Drawing.Point(11, 185);
            this.checkBox_blockmunch.Name = "checkBox_blockmunch";
            this.checkBox_blockmunch.Size = new System.Drawing.Size(86, 17);
            this.checkBox_blockmunch.TabIndex = 369;
            this.checkBox_blockmunch.Text = "BlockMunch";
            this.checkBox_blockmunch.UseVisualStyleBackColor = true;
            // 
            // checkBox_starpool
            // 
            this.checkBox_starpool.AutoSize = true;
            this.checkBox_starpool.Enabled = false;
            this.checkBox_starpool.Location = new System.Drawing.Point(11, 162);
            this.checkBox_starpool.Name = "checkBox_starpool";
            this.checkBox_starpool.Size = new System.Drawing.Size(66, 17);
            this.checkBox_starpool.TabIndex = 368;
            this.checkBox_starpool.Text = "StarPool";
            this.checkBox_starpool.UseVisualStyleBackColor = true;
            // 
            // checkBox_zerg
            // 
            this.checkBox_zerg.AutoSize = true;
            this.checkBox_zerg.Checked = true;
            this.checkBox_zerg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_zerg.Location = new System.Drawing.Point(11, 116);
            this.checkBox_zerg.Name = "checkBox_zerg";
            this.checkBox_zerg.Size = new System.Drawing.Size(69, 17);
            this.checkBox_zerg.TabIndex = 367;
            this.checkBox_zerg.Text = "ZergPool";
            this.checkBox_zerg.UseVisualStyleBackColor = true;
            // 
            // checkBox_minemoney
            // 
            this.checkBox_minemoney.AutoSize = true;
            this.checkBox_minemoney.Enabled = false;
            this.checkBox_minemoney.Location = new System.Drawing.Point(11, 139);
            this.checkBox_minemoney.Name = "checkBox_minemoney";
            this.checkBox_minemoney.Size = new System.Drawing.Size(81, 17);
            this.checkBox_minemoney.TabIndex = 366;
            this.checkBox_minemoney.Text = "MineMoney";
            this.checkBox_minemoney.UseVisualStyleBackColor = true;
            // 
            // checkBox_MPH
            // 
            this.checkBox_MPH.AutoSize = true;
            this.checkBox_MPH.Enabled = false;
            this.checkBox_MPH.Location = new System.Drawing.Point(11, 247);
            this.checkBox_MPH.Name = "checkBox_MPH";
            this.checkBox_MPH.Size = new System.Drawing.Size(98, 17);
            this.checkBox_MPH.TabIndex = 5;
            this.checkBox_MPH.Text = "MiningPoolHub";
            this.checkBox_MPH.UseVisualStyleBackColor = true;
            // 
            // checkBox_NiceHash
            // 
            this.checkBox_NiceHash.AutoSize = true;
            this.checkBox_NiceHash.Enabled = false;
            this.checkBox_NiceHash.Location = new System.Drawing.Point(11, 93);
            this.checkBox_NiceHash.Name = "checkBox_NiceHash";
            this.checkBox_NiceHash.Size = new System.Drawing.Size(73, 17);
            this.checkBox_NiceHash.TabIndex = 4;
            this.checkBox_NiceHash.Text = "NiceHash";
            this.checkBox_NiceHash.UseVisualStyleBackColor = true;
            // 
            // checkBox_HashRefinery
            // 
            this.checkBox_HashRefinery.AutoSize = true;
            this.checkBox_HashRefinery.Enabled = false;
            this.checkBox_HashRefinery.Location = new System.Drawing.Point(11, 69);
            this.checkBox_HashRefinery.Name = "checkBox_HashRefinery";
            this.checkBox_HashRefinery.Size = new System.Drawing.Size(90, 17);
            this.checkBox_HashRefinery.TabIndex = 3;
            this.checkBox_HashRefinery.Text = "HashRefinery";
            this.checkBox_HashRefinery.UseVisualStyleBackColor = true;
            // 
            // checkBox_AhashPool
            // 
            this.checkBox_AhashPool.AutoSize = true;
            this.checkBox_AhashPool.Enabled = false;
            this.checkBox_AhashPool.Location = new System.Drawing.Point(11, 45);
            this.checkBox_AhashPool.Name = "checkBox_AhashPool";
            this.checkBox_AhashPool.Size = new System.Drawing.Size(77, 17);
            this.checkBox_AhashPool.TabIndex = 2;
            this.checkBox_AhashPool.Text = "AhashPool";
            this.checkBox_AhashPool.UseVisualStyleBackColor = true;
            // 
            // checkBox_zpool
            // 
            this.checkBox_zpool.AutoSize = true;
            this.checkBox_zpool.Enabled = false;
            this.checkBox_zpool.Location = new System.Drawing.Point(11, 19);
            this.checkBox_zpool.Name = "checkBox_zpool";
            this.checkBox_zpool.Size = new System.Drawing.Size(68, 17);
            this.checkBox_zpool.TabIndex = 1;
            this.checkBox_zpool.Text = "Zpool.ca";
            this.checkBox_zpool.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(-1, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pools";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSaveClose
            // 
            this.buttonSaveClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveClose.Location = new System.Drawing.Point(368, 459);
            this.buttonSaveClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonSaveClose.Name = "buttonSaveClose";
            this.buttonSaveClose.Size = new System.Drawing.Size(134, 23);
            this.buttonSaveClose.TabIndex = 44;
            this.buttonSaveClose.Text = "&Save and Close";
            this.buttonSaveClose.UseVisualStyleBackColor = true;
            this.buttonSaveClose.Click += new System.EventHandler(this.ButtonSaveClose_Click);
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefaults.Location = new System.Drawing.Point(290, 459);
            this.buttonDefaults.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(74, 23);
            this.buttonDefaults.TabIndex = 43;
            this.buttonDefaults.Text = "&Defaults";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.ButtonDefaults_Click);
            // 
            // buttonCloseNoSave
            // 
            this.buttonCloseNoSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseNoSave.Location = new System.Drawing.Point(506, 459);
            this.buttonCloseNoSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonCloseNoSave.Name = "buttonCloseNoSave";
            this.buttonCloseNoSave.Size = new System.Drawing.Size(134, 23);
            this.buttonCloseNoSave.TabIndex = 45;
            this.buttonCloseNoSave.Text = "&Close without Saving";
            this.buttonCloseNoSave.UseVisualStyleBackColor = true;
            this.buttonCloseNoSave.Click += new System.EventHandler(this.ButtonCloseNoSave_Click);
            // 
            // algorithmsListView1
            // 
            this.algorithmsListView1.BenchmarkCalculation = null;
            this.algorithmsListView1.ComunicationInterface = null;
            this.algorithmsListView1.IsInBenchmark = false;
            this.algorithmsListView1.Location = new System.Drawing.Point(6, 15);
            this.algorithmsListView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.algorithmsListView1.Name = "algorithmsListView1";
            this.algorithmsListView1.Size = new System.Drawing.Size(648, 184);
            this.algorithmsListView1.TabIndex = 2;
            // 
            // devicesListViewEnableControl1
            // 
            this.devicesListViewEnableControl1.AutoSize = true;
            this.devicesListViewEnableControl1.BackColor = System.Drawing.SystemColors.Window;
            this.devicesListViewEnableControl1.BenchmarkCalculation = null;
            this.devicesListViewEnableControl1.FirstColumnText = "Enabled";
            this.devicesListViewEnableControl1.IsInBenchmark = false;
            this.devicesListViewEnableControl1.IsMining = false;
            this.devicesListViewEnableControl1.Location = new System.Drawing.Point(6, 6);
            this.devicesListViewEnableControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.devicesListViewEnableControl1.Name = "devicesListViewEnableControl1";
            this.devicesListViewEnableControl1.SaveToGeneralConfig = false;
            this.devicesListViewEnableControl1.Size = new System.Drawing.Size(352, 172);
            this.devicesListViewEnableControl1.TabIndex = 397;
            // 
            // algorithmSettingsControl1
            // 
            this.algorithmSettingsControl1.Location = new System.Drawing.Point(366, 0);
            this.algorithmSettingsControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.algorithmSettingsControl1.Name = "algorithmSettingsControl1";
            this.algorithmSettingsControl1.Size = new System.Drawing.Size(238, 193);
            this.algorithmSettingsControl1.TabIndex = 396;
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(654, 495);
            this.Controls.Add(this.buttonDefaults);
            this.Controls.Add(this.buttonSaveClose);
            this.Controls.Add(this.tabControlGeneral);
            this.Controls.Add(this.buttonCloseNoSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.tabControlGeneral.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBox_Localization.ResumeLayout(false);
            this.groupBox_Localization.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Language)).EndInit();
            this.groupBox_Miners.ResumeLayout(false);
            this.groupBox_Miners.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchMinSecondsFixed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinerRestartDelayMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_APIBindPortStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchMinSecondsDynamic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchProfitabilityThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ethminerDefaultBlockHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DagGeneration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CPU0_ForceCPUExtension)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinerAPIQueryInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SwitchMinSecondsAMD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinIdleSeconds)).EndInit();
            this.groupBox_Misc.ResumeLayout(false);
            this.groupBox_Misc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunScriptOnCUDA_GPU_Lost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShowInternetConnectionWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinimizeMiningWindows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunAtStartup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AllowMultipleInstances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableDefaultOptimizations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AMD_DisableAMDTempControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NVIDIAP0State)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableWindowsErrorReporting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShowDriverVersionWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_StartMiningWhenIdle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AutoScaleBTCValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableDetectionAMD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Use3rdPartyMiners)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DisableDetectionNVIDIA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AutoStartMining)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinimizeToTray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_HideMiningWindows)).EndInit();
            this.groupBox_Logging.ResumeLayout(false);
            this.groupBox_Logging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DebugConsole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LogMaxFileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LogToFile)).EndInit();
            this.groupBox_Main.ResumeLayout(false);
            this.groupBox_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TimeUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_displayCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MinProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IdleWhenNoInternetAccess)).EndInit();
            this.tabPageDevicesAlgos.ResumeLayout(false);
            this.tabPageDevicesAlgos.PerformLayout();
            this.groupBoxAlgorithmSettings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Defines the buttonSaveClose
        /// </summary>
        private System.Windows.Forms.Button buttonSaveClose;

        /// <summary>
        /// Defines the buttonDefaults
        /// </summary>
        private System.Windows.Forms.Button buttonDefaults;

        /// <summary>
        /// Defines the buttonCloseNoSave
        /// </summary>
        private System.Windows.Forms.Button buttonCloseNoSave;

        /// <summary>
        /// Defines the tabControlGeneral
        /// </summary>
        private System.Windows.Forms.TabControl tabControlGeneral;

        /// <summary>
        /// Defines the tabPageGeneral
        /// </summary>
        private System.Windows.Forms.TabPage tabPageGeneral;

        /// <summary>
        /// Defines the label_displayCurrency
        /// </summary>
        private System.Windows.Forms.Label label_displayCurrency;

        /// <summary>
        /// Defines the currencyConverterCombobox
        /// </summary>
        private System.Windows.Forms.ComboBox currencyConverterCombobox;

        /// <summary>
        /// Defines the textBox_LogMaxFileSize
        /// </summary>
        private System.Windows.Forms.TextBox textBox_LogMaxFileSize;

        /// <summary>
        /// Defines the checkBox_LogToFile
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_LogToFile;

        /// <summary>
        /// Defines the label_Language
        /// </summary>
        private System.Windows.Forms.Label label_Language;

        /// <summary>
        /// Defines the comboBox_Language
        /// </summary>
        private System.Windows.Forms.ComboBox comboBox_Language;

        /// <summary>
        /// Defines the label_LogMaxFileSize
        /// </summary>
        private System.Windows.Forms.Label label_LogMaxFileSize;

        /// <summary>
        /// Defines the checkBox_DebugConsole
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_DebugConsole;

        /// <summary>
        /// Defines the tabPageDevicesAlgos
        /// </summary>
        private System.Windows.Forms.TabPage tabPageDevicesAlgos;

        /// <summary>
        /// Defines the toolTip1
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;

        /// <summary>
        /// Defines the groupBox_Main
        /// </summary>
        private System.Windows.Forms.GroupBox groupBox_Main;

        /// <summary>
        /// Defines the groupBox_Localization
        /// </summary>
        private System.Windows.Forms.GroupBox groupBox_Localization;

        /// <summary>
        /// Defines the groupBox_Logging
        /// </summary>
        private System.Windows.Forms.GroupBox groupBox_Logging;

        /// <summary>
        /// Defines the textBox_MinProfit
        /// </summary>
        private System.Windows.Forms.TextBox textBox_MinProfit;

        /// <summary>
        /// Defines the label_MinProfit
        /// </summary>
        private System.Windows.Forms.Label label_MinProfit;

        /// <summary>
        /// Defines the groupBox_Misc
        /// </summary>
        private System.Windows.Forms.GroupBox groupBox_Misc;

        /// <summary>
        /// Defines the checkBox_HideMiningWindows
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_HideMiningWindows;

        /// <summary>
        /// Defines the checkBox_MinimizeToTray
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_MinimizeToTray;

        /// <summary>
        /// Defines the checkBox_DisableDetectionNVIDIA
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_DisableDetectionNVIDIA;

        /// <summary>
        /// Defines the checkBox_DisableDetectionAMD
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_DisableDetectionAMD;

        /// <summary>
        /// Defines the checkBox_NVIDIAP0State
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_NVIDIAP0State;

        /// <summary>
        /// Defines the checkBox_AutoScaleBTCValues
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_AutoScaleBTCValues;

        /// <summary>
        /// Defines the checkBox_DisableWindowsErrorReporting
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_DisableWindowsErrorReporting;

        /// <summary>
        /// Defines the checkBox_StartMiningWhenIdle
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_StartMiningWhenIdle;

        /// <summary>
        /// Defines the checkBox_ShowDriverVersionWarning
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_ShowDriverVersionWarning;

        /// <summary>
        /// Defines the algorithmSettingsControl1
        /// </summary>
        private Components.AlgorithmSettingsControl algorithmSettingsControl1;

        /// <summary>
        /// Defines the groupBoxAlgorithmSettings
        /// </summary>
        private System.Windows.Forms.GroupBox groupBoxAlgorithmSettings;

        /// <summary>
        /// Defines the algorithmsListView1
        /// </summary>
        private Components.AlgorithmsListView algorithmsListView1;

        /// <summary>
        /// Defines the pictureBox_NVIDIAP0State
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_NVIDIAP0State;

        /// <summary>
        /// Defines the pictureBox_DisableWindowsErrorReporting
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_DisableWindowsErrorReporting;

        /// <summary>
        /// Defines the pictureBox_ShowDriverVersionWarning
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_ShowDriverVersionWarning;

        /// <summary>
        /// Defines the pictureBox_StartMiningWhenIdle
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_StartMiningWhenIdle;

        /// <summary>
        /// Defines the pictureBox_AutoScaleBTCValues
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_AutoScaleBTCValues;

        /// <summary>
        /// Defines the pictureBox_DisableDetectionAMD
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_DisableDetectionAMD;

        /// <summary>
        /// Defines the pictureBox_DisableDetectionNVIDIA
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_DisableDetectionNVIDIA;

        /// <summary>
        /// Defines the pictureBox_MinimizeToTray
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_MinimizeToTray;

        /// <summary>
        /// Defines the pictureBox_HideMiningWindows
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_HideMiningWindows;

        /// <summary>
        /// Defines the pictureBox_DebugConsole
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_DebugConsole;

        /// <summary>
        /// Defines the pictureBox_LogMaxFileSize
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_LogMaxFileSize;

        /// <summary>
        /// Defines the pictureBox_LogToFile
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_LogToFile;

        /// <summary>
        /// Defines the pictureBox_MinProfit
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_MinProfit;

        /// <summary>
        /// Defines the pictureBox5
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox5;

        /// <summary>
        /// Defines the pictureBox_displayCurrency
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_displayCurrency;

        /// <summary>
        /// Defines the pictureBox_Language
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_Language;

        /// <summary>
        /// Defines the devicesListViewEnableControl1
        /// </summary>
        private Components.DevicesListViewEnableControl devicesListViewEnableControl1;

        /// <summary>
        /// Defines the checkBox_AutoStartMining
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_AutoStartMining;

        /// <summary>
        /// Defines the pictureBox_AutoStartMining
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_AutoStartMining;

        /// <summary>
        /// Defines the checkBox_AMD_DisableAMDTempControl
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_AMD_DisableAMDTempControl;

        /// <summary>
        /// Defines the pictureBox_AMD_DisableAMDTempControl
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_AMD_DisableAMDTempControl;

        /// <summary>
        /// Defines the checkBox_DisableDefaultOptimizations
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_DisableDefaultOptimizations;

        /// <summary>
        /// Defines the pictureBox_DisableDefaultOptimizations
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_DisableDefaultOptimizations;

        /// <summary>
        /// Defines the checkBox_IdleWhenNoInternetAccess
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_IdleWhenNoInternetAccess;

        /// <summary>
        /// Defines the pictureBox_IdleWhenNoInternetAccess
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_IdleWhenNoInternetAccess;

        /// <summary>
        /// Defines the pictureBox_Use3rdPartyMiners
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_Use3rdPartyMiners;

        /// <summary>
        /// Defines the checkBox_Use3rdPartyMiners
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_Use3rdPartyMiners;

        /// <summary>
        /// Defines the checkBox_AllowMultipleInstances
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_AllowMultipleInstances;

        /// <summary>
        /// Defines the pictureBox_AllowMultipleInstances
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_AllowMultipleInstances;

        /// <summary>
        /// Defines the checkBox_RunAtStartup
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_RunAtStartup;

        /// <summary>
        /// Defines the pictureBox_RunAtStartup
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_RunAtStartup;

        /// <summary>
        /// Defines the checkBox_MinimizeMiningWindows
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_MinimizeMiningWindows;

        /// <summary>
        /// Defines the pictureBox_MinimizeMiningWindows
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_MinimizeMiningWindows;

        /// <summary>
        /// Defines the pictureBox_ShowInternetConnectionWarning
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_ShowInternetConnectionWarning;

        /// <summary>
        /// Defines the checkBox_ShowInternetConnectionWarning
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_ShowInternetConnectionWarning;

        /// <summary>
        /// Defines the pictureBox_RunScriptOnCUDA_GPU_Lost
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_RunScriptOnCUDA_GPU_Lost;

        /// <summary>
        /// Defines the checkBox_RunScriptOnCUDA_GPU_Lost
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox_RunScriptOnCUDA_GPU_Lost;

        /// <summary>
        /// Defines the pictureBox_TimeUnit
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox_TimeUnit;

        /// <summary>
        /// Defines the label_TimeUnit
        /// </summary>
        private System.Windows.Forms.Label label_TimeUnit;

        /// <summary>
        /// Defines the comboBox_TimeUnit
        /// </summary>
        private System.Windows.Forms.ComboBox comboBox_TimeUnit;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_AhashPool_Wallet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Zpool_Wallet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_NiceHash_Wallet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_HashRefinery_Wallet;
        private System.Windows.Forms.CheckBox checkBox_NiceHash;
        private System.Windows.Forms.CheckBox checkBox_HashRefinery;
        private System.Windows.Forms.CheckBox checkBox_AhashPool;
        private System.Windows.Forms.CheckBox checkBox_zpool;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_MPH_Wallet;
        private System.Windows.Forms.CheckBox checkBox_MPH;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_hashrefinery_worker;
        private System.Windows.Forms.TextBox textBox_AhashPool_Worker;
        private System.Windows.Forms.TextBox textBox_Zpool_Worker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_MPH_Worker;
        private System.Windows.Forms.TextBox textBox_NiceHash_Worker;
        private System.Windows.Forms.CheckBox checkBox_zerg;
        private System.Windows.Forms.CheckBox checkBox_minemoney;
        private System.Windows.Forms.TextBox textBox_minemoney_Worker;
        private System.Windows.Forms.TextBox textBox_zerg_Worker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_minemoney_Wallet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_zerg_Wallet;
        private System.Windows.Forms.TextBox textBox_blazepool_Worker;
        private System.Windows.Forms.TextBox textBox_blockmunch_Worker;
        private System.Windows.Forms.TextBox textBox_starpool_Worker;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_blazepool_Wallet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_blockmunch_Wallet;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_starpool_Wallet;
        private System.Windows.Forms.CheckBox checkBox_blazepool;
        private System.Windows.Forms.CheckBox checkBox_blockmunch;
        private System.Windows.Forms.CheckBox checkBox_starpool;
        private System.Windows.Forms.GroupBox groupBox_Miners;
        private System.Windows.Forms.PictureBox pictureBox_SwitchMinSecondsFixed;
        private System.Windows.Forms.PictureBox pictureBox_MinerRestartDelayMS;
        private System.Windows.Forms.PictureBox pictureBox_APIBindPortStart;
        private System.Windows.Forms.PictureBox pictureBox_SwitchMinSecondsDynamic;
        private System.Windows.Forms.PictureBox pictureBox_SwitchProfitabilityThreshold;
        private System.Windows.Forms.PictureBox pictureBox_ethminerDefaultBlockHeight;
        private System.Windows.Forms.PictureBox pictureBox_DagGeneration;
        private System.Windows.Forms.PictureBox pictureBox_CPU0_ForceCPUExtension;
        private System.Windows.Forms.PictureBox pictureBox_MinerAPIQueryInterval;
        private System.Windows.Forms.PictureBox pictureBox_SwitchMinSecondsAMD;
        private System.Windows.Forms.PictureBox pictureBox_MinIdleSeconds;
        private System.Windows.Forms.ComboBox comboBox_DagLoadMode;
        private System.Windows.Forms.Label label_DagGeneration;
        private System.Windows.Forms.ComboBox comboBox_CPU0_ForceCPUExtension;
        private System.Windows.Forms.Label label_CPU0_ForceCPUExtension;
        private System.Windows.Forms.Label label_MinIdleSeconds;
        private System.Windows.Forms.Label label_SwitchMinSecondsFixed;
        private System.Windows.Forms.Label label_SwitchMinSecondsDynamic;
        private System.Windows.Forms.Label label_MinerAPIQueryInterval;
        private System.Windows.Forms.Label label_MinerRestartDelayMS;
        private System.Windows.Forms.TextBox textBox_SwitchMinSecondsAMD;
        private System.Windows.Forms.Label label_APIBindPortStart;
        private System.Windows.Forms.TextBox textBox_SwitchProfitabilityThreshold;
        private System.Windows.Forms.TextBox textBox_ethminerDefaultBlockHeight;
        private System.Windows.Forms.Label label_SwitchProfitabilityThreshold;
        private System.Windows.Forms.Label label_ethminerDefaultBlockHeight;
        private System.Windows.Forms.TextBox textBox_APIBindPortStart;
        private System.Windows.Forms.Label label_SwitchMinSecondsAMD;
        private System.Windows.Forms.TextBox textBox_MinIdleSeconds;
        private System.Windows.Forms.TextBox textBox_SwitchMinSecondsFixed;
        private System.Windows.Forms.TextBox textBox_SwitchMinSecondsDynamic;
        private System.Windows.Forms.TextBox textBox_MinerRestartDelayMS;
        private System.Windows.Forms.TextBox textBox_MinerAPIQueryInterval;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_averaging;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_devapi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkbox_Group_same_devices;
    }
}
