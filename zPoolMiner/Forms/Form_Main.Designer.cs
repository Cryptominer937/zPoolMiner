namespace zPoolMiner
{
    partial class Form_Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonStartMining = new System.Windows.Forms.Button();
            this.textBoxBTCAddress = new System.Windows.Forms.TextBox();
            this.labelServiceLocation = new System.Windows.Forms.Label();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.labelBitcoinAddress = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelGlobalRateText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelGlobalRateValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBTCDayText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBTCDayValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceBTCCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceBTCValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceDollarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceDollarValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.linkLabelCheckStats = new System.Windows.Forms.LinkLabel();
            this.labelWorkerName = new System.Windows.Forms.Label();
            this.textBoxWorkerName = new System.Windows.Forms.TextBox();
            this.buttonStopMining = new System.Windows.Forms.Button();
            this.buttonBenchmark = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonLogo = new System.Windows.Forms.Button();
            this.linkLabelChooseBTCWallet = new System.Windows.Forms.LinkLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.labelDemoMode = new System.Windows.Forms.Label();
            this.flowLayoutPanelRates = new System.Windows.Forms.FlowLayoutPanel();
            this.label_NotProfitable = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.linkLabelNewVersion = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.devicesListViewEnableControl1 = new zPoolMiner.Forms.Components.DevicesListViewEnableControl();
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanelRates.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartMining
            // 
            this.buttonStartMining.Location = new System.Drawing.Point(452, 162);
            this.buttonStartMining.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStartMining.Name = "buttonStartMining";
            this.buttonStartMining.Size = new System.Drawing.Size(89, 23);
            this.buttonStartMining.TabIndex = 6;
            this.buttonStartMining.Text = "&Start";
            this.buttonStartMining.UseVisualStyleBackColor = true;
            this.buttonStartMining.Click += new System.EventHandler(this.ButtonStartMining_Click);
            // 
            // textBoxBTCAddress
            // 
            this.textBoxBTCAddress.Location = new System.Drawing.Point(113, 39);
            this.textBoxBTCAddress.Name = "textBoxBTCAddress";
            this.textBoxBTCAddress.Size = new System.Drawing.Size(237, 20);
            this.textBoxBTCAddress.TabIndex = 1;
            this.textBoxBTCAddress.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // labelServiceLocation
            // 
            this.labelServiceLocation.AutoSize = true;
            this.labelServiceLocation.Location = new System.Drawing.Point(8, 15);
            this.labelServiceLocation.Name = "labelServiceLocation";
            this.labelServiceLocation.Size = new System.Drawing.Size(86, 13);
            this.labelServiceLocation.TabIndex = 99;
            this.labelServiceLocation.Text = "Service location:";
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Items.AddRange(new object[] {
            "Europe - Amsterdam",
            "USA - San Jose",
            "China - Hong Kong",
            "Japan - Tokyo",
            "India - Chennai",
            "Brazil - Sao Paulo"});
            this.comboBoxLocation.Location = new System.Drawing.Point(113, 12);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLocation.TabIndex = 0;
            this.comboBoxLocation.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // labelBitcoinAddress
            // 
            this.labelBitcoinAddress.AutoSize = true;
            this.labelBitcoinAddress.Location = new System.Drawing.Point(8, 42);
            this.labelBitcoinAddress.Name = "labelBitcoinAddress";
            this.labelBitcoinAddress.Size = new System.Drawing.Size(83, 13);
            this.labelBitcoinAddress.TabIndex = 99;
            this.labelBitcoinAddress.Text = "Payout address:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelGlobalRateText,
            this.toolStripStatusLabelGlobalRateValue,
            this.toolStripStatusLabelBTCDayText,
            this.toolStripStatusLabelBTCDayValue,
            this.toolStripStatusLabelBalanceBTCCode,
            this.toolStripStatusLabelBalanceText,
            this.toolStripStatusLabelBalanceBTCValue,
            this.toolStripStatusLabelBalanceDollarText,
            this.toolStripStatusLabelBalanceDollarValue,
            this.toolStripStatusLabel10,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 331);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(560, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelGlobalRateText
            // 
            this.toolStripStatusLabelGlobalRateText.Name = "toolStripStatusLabelGlobalRateText";
            this.toolStripStatusLabelGlobalRateText.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabelGlobalRateText.Text = "Global rate:";
            // 
            // toolStripStatusLabelGlobalRateValue
            // 
            this.toolStripStatusLabelGlobalRateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelGlobalRateValue.Name = "toolStripStatusLabelGlobalRateValue";
            this.toolStripStatusLabelGlobalRateValue.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabelGlobalRateValue.Text = "0.00000000";
            // 
            // toolStripStatusLabelBTCDayText
            // 
            this.toolStripStatusLabelBTCDayText.Name = "toolStripStatusLabelBTCDayText";
            this.toolStripStatusLabelBTCDayText.Size = new System.Drawing.Size(52, 17);
            this.toolStripStatusLabelBTCDayText.Text = "BTC/Day";
            // 
            // toolStripStatusLabelBTCDayValue
            // 
            this.toolStripStatusLabelBTCDayValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBTCDayValue.Name = "toolStripStatusLabelBTCDayValue";
            this.toolStripStatusLabelBTCDayValue.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabelBTCDayValue.Text = "0.00";
            // 
            // toolStripStatusLabelBalanceBTCCode
            // 
            this.toolStripStatusLabelBalanceBTCCode.Name = "toolStripStatusLabelBalanceBTCCode";
            this.toolStripStatusLabelBalanceBTCCode.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabelBalanceBTCCode.Text = "Fiat/Day";
            // 
            // toolStripStatusLabelBalanceText
            // 
            this.toolStripStatusLabelBalanceText.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripStatusLabelBalanceText.Name = "toolStripStatusLabelBalanceText";
            this.toolStripStatusLabelBalanceText.Size = new System.Drawing.Size(38, 20);
            this.toolStripStatusLabelBalanceText.Text = "$/Day";
            this.toolStripStatusLabelBalanceText.Visible = false;
            this.toolStripStatusLabelBalanceText.Click += new System.EventHandler(this.ToolStripStatusLabelBalanceText_Click);
            // 
            // toolStripStatusLabelBalanceBTCValue
            // 
            this.toolStripStatusLabelBalanceBTCValue.Enabled = false;
            this.toolStripStatusLabelBalanceBTCValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBalanceBTCValue.Name = "toolStripStatusLabelBalanceBTCValue";
            this.toolStripStatusLabelBalanceBTCValue.Size = new System.Drawing.Size(73, 20);
            this.toolStripStatusLabelBalanceBTCValue.Text = "0.00000000";
            this.toolStripStatusLabelBalanceBTCValue.Visible = false;
            // 
            // toolStripStatusLabelBalanceDollarText
            // 
            this.toolStripStatusLabelBalanceDollarText.Enabled = false;
            this.toolStripStatusLabelBalanceDollarText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBalanceDollarText.Name = "toolStripStatusLabelBalanceDollarText";
            this.toolStripStatusLabelBalanceDollarText.Size = new System.Drawing.Size(31, 20);
            this.toolStripStatusLabelBalanceDollarText.Text = "0.00";
            this.toolStripStatusLabelBalanceDollarText.Visible = false;
            // 
            // toolStripStatusLabelBalanceDollarValue
            // 
            this.toolStripStatusLabelBalanceDollarValue.Enabled = false;
            this.toolStripStatusLabelBalanceDollarValue.Name = "toolStripStatusLabelBalanceDollarValue";
            this.toolStripStatusLabelBalanceDollarValue.Size = new System.Drawing.Size(16, 20);
            this.toolStripStatusLabelBalanceDollarValue.Text = "$ ";
            this.toolStripStatusLabelBalanceDollarValue.Visible = false;
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Image = global::zPoolMiner.Properties.Resources.NHM_Cash_Register_Bitcoin_transparent;
            this.toolStripStatusLabel10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(35, 20);
            this.toolStripStatusLabel10.Visible = false;
            this.toolStripStatusLabel10.Click += new System.EventHandler(this.ToolStripStatusLabel10_Click);
            this.toolStripStatusLabel10.MouseLeave += new System.EventHandler(this.ToolStripStatusLabel10_MouseLeave);
            this.toolStripStatusLabel10.MouseHover += new System.EventHandler(this.ToolStripStatusLabel10_MouseHover);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // linkLabelCheckStats
            // 
            this.linkLabelCheckStats.AutoSize = true;
            this.linkLabelCheckStats.Location = new System.Drawing.Point(16, 91);
            this.linkLabelCheckStats.Name = "linkLabelCheckStats";
            this.linkLabelCheckStats.Size = new System.Drawing.Size(113, 13);
            this.linkLabelCheckStats.TabIndex = 9;
            this.linkLabelCheckStats.TabStop = true;
            this.linkLabelCheckStats.Text = "Check my stats online!";
            this.linkLabelCheckStats.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelCheckStats_LinkClicked);
            // 
            // labelWorkerName
            // 
            this.labelWorkerName.AutoSize = true;
            this.labelWorkerName.Location = new System.Drawing.Point(6, 68);
            this.labelWorkerName.Name = "labelWorkerName";
            this.labelWorkerName.Size = new System.Drawing.Size(106, 13);
            this.labelWorkerName.TabIndex = 99;
            this.labelWorkerName.Text = "Coin / Worker name:";
            // 
            // textBoxWorkerName
            // 
            this.textBoxWorkerName.Location = new System.Drawing.Point(113, 65);
            this.textBoxWorkerName.Name = "textBoxWorkerName";
            this.textBoxWorkerName.Size = new System.Drawing.Size(187, 20);
            this.textBoxWorkerName.TabIndex = 2;
            this.textBoxWorkerName.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // buttonStopMining
            // 
            this.buttonStopMining.Enabled = false;
            this.buttonStopMining.Location = new System.Drawing.Point(452, 188);
            this.buttonStopMining.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStopMining.Name = "buttonStopMining";
            this.buttonStopMining.Size = new System.Drawing.Size(89, 23);
            this.buttonStopMining.TabIndex = 7;
            this.buttonStopMining.Text = "St&op";
            this.buttonStopMining.UseVisualStyleBackColor = true;
            this.buttonStopMining.Click += new System.EventHandler(this.ButtonStopMining_Click);
            // 
            // buttonBenchmark
            // 
            this.buttonBenchmark.Location = new System.Drawing.Point(452, 109);
            this.buttonBenchmark.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.buttonBenchmark.Name = "buttonBenchmark";
            this.buttonBenchmark.Size = new System.Drawing.Size(89, 23);
            this.buttonBenchmark.TabIndex = 4;
            this.buttonBenchmark.Text = "&Benchmark";
            this.buttonBenchmark.UseVisualStyleBackColor = true;
            this.buttonBenchmark.Click += new System.EventHandler(this.ButtonBenchmark_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(452, 135);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(89, 23);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "S&ettings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // buttonLogo
            // 
            this.buttonLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogo.FlatAppearance.BorderSize = 0;
            this.buttonLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogo.Image = global::zPoolMiner.Properties.Resources.NHM_logo_xsmall_light;
            this.buttonLogo.Location = new System.Drawing.Point(395, 7);
            this.buttonLogo.Name = "buttonLogo";
            this.buttonLogo.Size = new System.Drawing.Size(162, 52);
            this.buttonLogo.TabIndex = 11;
            this.buttonLogo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.buttonLogo.UseMnemonic = false;
            this.buttonLogo.UseVisualStyleBackColor = true;
            this.buttonLogo.Click += new System.EventHandler(this.ButtonLogo_Click);
            // 
            // linkLabelChooseBTCWallet
            // 
            this.linkLabelChooseBTCWallet.AutoSize = true;
            this.linkLabelChooseBTCWallet.Location = new System.Drawing.Point(135, 91);
            this.linkLabelChooseBTCWallet.Name = "linkLabelChooseBTCWallet";
            this.linkLabelChooseBTCWallet.Size = new System.Drawing.Size(165, 13);
            this.linkLabelChooseBTCWallet.TabIndex = 10;
            this.linkLabelChooseBTCWallet.TabStop = true;
            this.linkLabelChooseBTCWallet.Text = "Help me choose my Bitcoin wallet";
            this.linkLabelChooseBTCWallet.Visible = false;
            this.linkLabelChooseBTCWallet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelChooseBTCWallet_LinkClicked);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // labelDemoMode
            // 
            this.labelDemoMode.AutoSize = true;
            this.labelDemoMode.BackColor = System.Drawing.Color.Transparent;
            this.labelDemoMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDemoMode.ForeColor = System.Drawing.Color.Red;
            this.labelDemoMode.Location = new System.Drawing.Point(306, 62);
            this.labelDemoMode.Name = "labelDemoMode";
            this.labelDemoMode.Size = new System.Drawing.Size(260, 25);
            this.labelDemoMode.TabIndex = 100;
            this.labelDemoMode.Text = "zPool Miner DEMO mode!";
            this.labelDemoMode.Visible = false;
            // 
            // flowLayoutPanelRates
            // 
            this.flowLayoutPanelRates.AutoScroll = true;
            this.flowLayoutPanelRates.Controls.Add(this.label_NotProfitable);
            this.flowLayoutPanelRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelRates.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelRates.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelRates.Name = "flowLayoutPanelRates";
            this.flowLayoutPanelRates.Size = new System.Drawing.Size(545, 40);
            this.flowLayoutPanelRates.TabIndex = 107;
            this.flowLayoutPanelRates.WrapContents = false;
            // 
            // label_NotProfitable
            // 
            this.label_NotProfitable.AutoSize = true;
            this.label_NotProfitable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_NotProfitable.ForeColor = System.Drawing.Color.Red;
            this.label_NotProfitable.Location = new System.Drawing.Point(3, 0);
            this.label_NotProfitable.Name = "label_NotProfitable";
            this.label_NotProfitable.Size = new System.Drawing.Size(366, 24);
            this.label_NotProfitable.TabIndex = 110;
            this.label_NotProfitable.Text = "CURRENTLY MINING NOT PROFITABLE.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanelRates);
            this.groupBox1.Location = new System.Drawing.Point(9, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 59);
            this.groupBox1.TabIndex = 108;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group/Device Rates:";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(452, 215);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(89, 23);
            this.buttonHelp.TabIndex = 8;
            this.buttonHelp.Text = "&Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
            // 
            // linkLabelNewVersion
            // 
            this.linkLabelNewVersion.AutoSize = true;
            this.linkLabelNewVersion.Location = new System.Drawing.Point(292, 77);
            this.linkLabelNewVersion.Name = "linkLabelNewVersion";
            this.linkLabelNewVersion.Size = new System.Drawing.Size(0, 13);
            this.linkLabelNewVersion.TabIndex = 110;
            this.linkLabelNewVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelNewVersion_LinkClicked);
            // 
            // devicesListViewEnableControl1
            // 
            this.devicesListViewEnableControl1.BenchmarkCalculation = null;
            this.devicesListViewEnableControl1.FirstColumnText = "Enabled";
            this.devicesListViewEnableControl1.IsInBenchmark = false;
            this.devicesListViewEnableControl1.IsMining = false;
            this.devicesListViewEnableControl1.Location = new System.Drawing.Point(11, 109);
            this.devicesListViewEnableControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.devicesListViewEnableControl1.Name = "devicesListViewEnableControl1";
            this.devicesListViewEnableControl1.SaveToGeneralConfig = false;
            this.devicesListViewEnableControl1.Size = new System.Drawing.Size(435, 129);
            this.devicesListViewEnableControl1.TabIndex = 109;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 353);
            this.Controls.Add(this.linkLabelNewVersion);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.devicesListViewEnableControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelDemoMode);
            this.Controls.Add(this.linkLabelChooseBTCWallet);
            this.Controls.Add(this.buttonLogo);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonBenchmark);
            this.Controls.Add(this.buttonStopMining);
            this.Controls.Add(this.labelWorkerName);
            this.Controls.Add(this.textBoxWorkerName);
            this.Controls.Add(this.linkLabelCheckStats);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.labelBitcoinAddress);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.labelServiceLocation);
            this.Controls.Add(this.textBoxBTCAddress);
            this.Controls.Add(this.buttonStartMining);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(576, 316);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "zPool Miner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanelRates.ResumeLayout(false);
            this.flowLayoutPanelRates.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartMining;
        private System.Windows.Forms.Label labelServiceLocation;
        private System.Windows.Forms.Label labelBitcoinAddress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.LinkLabel linkLabelCheckStats;
        private System.Windows.Forms.Label labelWorkerName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateText;
        private System.Windows.Forms.Button buttonStopMining;
        private System.Windows.Forms.Button buttonBenchmark;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBTCDayText;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonLogo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox textBoxBTCAddress;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.TextBox textBoxWorkerName;
        private System.Windows.Forms.LinkLabel linkLabelChooseBTCWallet;
        private System.Windows.Forms.Label labelDemoMode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_NotProfitable;
        private Forms.Components.DevicesListViewEnableControl devicesListViewEnableControl1;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.LinkLabel linkLabelNewVersion;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBTCDayValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCCode;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceDollarText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceDollarValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceText;
    }
}



