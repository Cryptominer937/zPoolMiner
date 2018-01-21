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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
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
            this.toolStripStatusLabelBalanceText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceBTCValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBalanceBTCCode = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_NotProfitable = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.linkLabelNewVersion = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.devicesListViewEnableControl1 = new zPoolMiner.Forms.Components.DevicesListViewEnableControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanelRates.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartMining
            // 
            this.buttonStartMining.Location = new System.Drawing.Point(3, 132);
            this.buttonStartMining.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStartMining.Name = "buttonStartMining";
            this.buttonStartMining.Size = new System.Drawing.Size(153, 59);
            this.buttonStartMining.TabIndex = 6;
            this.buttonStartMining.Text = "&Start";
            this.buttonStartMining.UseVisualStyleBackColor = true;
            this.buttonStartMining.Click += new System.EventHandler(this.ButtonStartMining_Click);
            // 
            // textBoxBTCAddress
            // 
            this.textBoxBTCAddress.Location = new System.Drawing.Point(108, 4);
            this.textBoxBTCAddress.Name = "textBoxBTCAddress";
            this.textBoxBTCAddress.Size = new System.Drawing.Size(447, 21);
            this.textBoxBTCAddress.TabIndex = 1;
            this.textBoxBTCAddress.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // labelServiceLocation
            // 
            this.labelServiceLocation.AutoSize = true;
            this.labelServiceLocation.ForeColor = System.Drawing.SystemColors.Control;
            this.labelServiceLocation.Location = new System.Drawing.Point(337, 36);
            this.labelServiceLocation.Name = "labelServiceLocation";
            this.labelServiceLocation.Size = new System.Drawing.Size(91, 16);
            this.labelServiceLocation.TabIndex = 99;
            this.labelServiceLocation.Text = "Service location:";
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.Enabled = false;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Items.AddRange(new object[] {
            "Zpool",
            "Future Pool",
            "Future Pool",
            "The Blocks Factory - unused",
            "Future Pool",
            "Future Pool"});
            this.comboBoxLocation.Location = new System.Drawing.Point(434, 31);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(121, 24);
            this.comboBoxLocation.TabIndex = 0;
            this.comboBoxLocation.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // labelBitcoinAddress
            // 
            this.labelBitcoinAddress.AutoSize = true;
            this.labelBitcoinAddress.ForeColor = System.Drawing.SystemColors.Control;
            this.labelBitcoinAddress.Location = new System.Drawing.Point(16, 7);
            this.labelBitcoinAddress.Name = "labelBitcoinAddress";
            this.labelBitcoinAddress.Size = new System.Drawing.Size(86, 16);
            this.labelBitcoinAddress.TabIndex = 99;
            this.labelBitcoinAddress.Text = "Payout address:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelGlobalRateText,
            this.toolStripStatusLabelGlobalRateValue,
            this.toolStripStatusLabelBTCDayText,
            this.toolStripStatusLabelBTCDayValue,
            this.toolStripStatusLabelBalanceText,
            this.toolStripStatusLabelBalanceBTCValue,
            this.toolStripStatusLabelBalanceBTCCode,
            this.toolStripStatusLabelBalanceDollarText,
            this.toolStripStatusLabelBalanceDollarValue,
            this.toolStripStatusLabel10,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(743, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelGlobalRateText
            // 
            this.toolStripStatusLabelGlobalRateText.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelGlobalRateText.Name = "toolStripStatusLabelGlobalRateText";
            this.toolStripStatusLabelGlobalRateText.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabelGlobalRateText.Text = "Global rate:";
            // 
            // toolStripStatusLabelGlobalRateValue
            // 
            this.toolStripStatusLabelGlobalRateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelGlobalRateValue.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelGlobalRateValue.Name = "toolStripStatusLabelGlobalRateValue";
            this.toolStripStatusLabelGlobalRateValue.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabelGlobalRateValue.Text = "0.00000000";
            // 
            // toolStripStatusLabelBTCDayText
            // 
            this.toolStripStatusLabelBTCDayText.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelBTCDayText.Name = "toolStripStatusLabelBTCDayText";
            this.toolStripStatusLabelBTCDayText.Size = new System.Drawing.Size(52, 17);
            this.toolStripStatusLabelBTCDayText.Text = "BTC/Day";
            // 
            // toolStripStatusLabelBTCDayValue
            // 
            this.toolStripStatusLabelBTCDayValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBTCDayValue.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelBTCDayValue.Name = "toolStripStatusLabelBTCDayValue";
            this.toolStripStatusLabelBTCDayValue.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabelBTCDayValue.Text = "0.00";
            // 
            // toolStripStatusLabelBalanceText
            // 
            this.toolStripStatusLabelBalanceText.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelBalanceText.Name = "toolStripStatusLabelBalanceText";
            this.toolStripStatusLabelBalanceText.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabelBalanceText.Text = "$/Day";
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
            // toolStripStatusLabelBalanceBTCCode
            // 
            this.toolStripStatusLabelBalanceBTCCode.Enabled = false;
            this.toolStripStatusLabelBalanceBTCCode.Name = "toolStripStatusLabelBalanceBTCCode";
            this.toolStripStatusLabelBalanceBTCCode.Size = new System.Drawing.Size(27, 20);
            this.toolStripStatusLabelBalanceBTCCode.Text = "BTC";
            this.toolStripStatusLabelBalanceBTCCode.Visible = false;
            // 
            // toolStripStatusLabelBalanceDollarText
            // 
            this.toolStripStatusLabelBalanceDollarText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBalanceDollarText.Name = "toolStripStatusLabelBalanceDollarText";
            this.toolStripStatusLabelBalanceDollarText.Size = new System.Drawing.Size(31, 20);
            this.toolStripStatusLabelBalanceDollarText.Text = "0.00";
            this.toolStripStatusLabelBalanceDollarText.Visible = false;
            // 
            // toolStripStatusLabelBalanceDollarValue
            // 
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
            this.linkLabelCheckStats.Location = new System.Drawing.Point(15, 24);
            this.linkLabelCheckStats.Name = "linkLabelCheckStats";
            this.linkLabelCheckStats.Size = new System.Drawing.Size(119, 16);
            this.linkLabelCheckStats.TabIndex = 9;
            this.linkLabelCheckStats.TabStop = true;
            this.linkLabelCheckStats.Text = "Check my stats online!";
            this.linkLabelCheckStats.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelCheckStats_LinkClicked);
            // 
            // labelWorkerName
            // 
            this.labelWorkerName.AutoSize = true;
            this.labelWorkerName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelWorkerName.Location = new System.Drawing.Point(16, 36);
            this.labelWorkerName.Name = "labelWorkerName";
            this.labelWorkerName.Size = new System.Drawing.Size(78, 16);
            this.labelWorkerName.TabIndex = 99;
            this.labelWorkerName.Text = "Worker name:";
            // 
            // textBoxWorkerName
            // 
            this.textBoxWorkerName.Location = new System.Drawing.Point(108, 33);
            this.textBoxWorkerName.Name = "textBoxWorkerName";
            this.textBoxWorkerName.Size = new System.Drawing.Size(223, 21);
            this.textBoxWorkerName.TabIndex = 2;
            this.textBoxWorkerName.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // buttonStopMining
            // 
            this.buttonStopMining.Enabled = false;
            this.buttonStopMining.Location = new System.Drawing.Point(3, 195);
            this.buttonStopMining.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStopMining.Name = "buttonStopMining";
            this.buttonStopMining.Size = new System.Drawing.Size(153, 59);
            this.buttonStopMining.TabIndex = 7;
            this.buttonStopMining.Text = "St&op";
            this.buttonStopMining.UseVisualStyleBackColor = true;
            this.buttonStopMining.Click += new System.EventHandler(this.ButtonStopMining_Click);
            // 
            // buttonBenchmark
            // 
            this.buttonBenchmark.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBenchmark.Location = new System.Drawing.Point(2, 71);
            this.buttonBenchmark.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.buttonBenchmark.Name = "buttonBenchmark";
            this.buttonBenchmark.Size = new System.Drawing.Size(153, 57);
            this.buttonBenchmark.TabIndex = 4;
            this.buttonBenchmark.Text = "&Benchmark";
            this.buttonBenchmark.UseVisualStyleBackColor = true;
            this.buttonBenchmark.Click += new System.EventHandler(this.ButtonBenchmark_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(3, 258);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(153, 56);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "S&ettings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // buttonLogo
            // 
            this.buttonLogo.BackColor = System.Drawing.Color.White;
            this.buttonLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogo.FlatAppearance.BorderSize = 0;
            this.buttonLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogo.Image = global::zPoolMiner.Properties.Resources.NHM_logo_xsmall_light;
            this.buttonLogo.Location = new System.Drawing.Point(6, 9);
            this.buttonLogo.Name = "buttonLogo";
            this.buttonLogo.Size = new System.Drawing.Size(149, 52);
            this.buttonLogo.TabIndex = 11;
            this.buttonLogo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.buttonLogo.UseMnemonic = false;
            this.buttonLogo.UseVisualStyleBackColor = false;
            this.buttonLogo.Click += new System.EventHandler(this.ButtonLogo_Click);
            // 
            // linkLabelChooseBTCWallet
            // 
            this.linkLabelChooseBTCWallet.AutoSize = true;
            this.linkLabelChooseBTCWallet.Location = new System.Drawing.Point(336, 348);
            this.linkLabelChooseBTCWallet.Name = "linkLabelChooseBTCWallet";
            this.linkLabelChooseBTCWallet.Size = new System.Drawing.Size(175, 16);
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
            this.labelDemoMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDemoMode.ForeColor = System.Drawing.Color.Red;
            this.labelDemoMode.Location = new System.Drawing.Point(19, 124);
            this.labelDemoMode.Name = "labelDemoMode";
            this.labelDemoMode.Size = new System.Drawing.Size(294, 20);
            this.labelDemoMode.TabIndex = 100;
            this.labelDemoMode.Text = "ZpoolMiner is running in DEMO mode!";
            this.labelDemoMode.Visible = false;
            // 
            // flowLayoutPanelRates
            // 
            this.flowLayoutPanelRates.AutoScroll = true;
            this.flowLayoutPanelRates.Controls.Add(this.panel2);
            this.flowLayoutPanelRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelRates.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelRates.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanelRates.Name = "flowLayoutPanelRates";
            this.flowLayoutPanelRates.Size = new System.Drawing.Size(545, 79);
            this.flowLayoutPanelRates.TabIndex = 107;
            this.flowLayoutPanelRates.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanelRates);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(12, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 99);
            this.groupBox1.TabIndex = 108;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group/Device Rates:";
            // 
            // label_NotProfitable
            // 
            this.label_NotProfitable.AutoSize = true;
            this.label_NotProfitable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_NotProfitable.ForeColor = System.Drawing.Color.Red;
            this.label_NotProfitable.Location = new System.Drawing.Point(9, 37);
            this.label_NotProfitable.Name = "label_NotProfitable";
            this.label_NotProfitable.Size = new System.Drawing.Size(313, 20);
            this.label_NotProfitable.TabIndex = 110;
            this.label_NotProfitable.Text = "CURRENTLY MINING NOT PROFITABLE.";
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
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(115, 387);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(41, 35);
            this.buttonHelp.TabIndex = 8;
            this.buttonHelp.Text = "&Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Visible = false;
            this.buttonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
            // 
            // linkLabelNewVersion
            // 
            this.linkLabelNewVersion.AutoSize = true;
            this.linkLabelNewVersion.Location = new System.Drawing.Point(292, 77);
            this.linkLabelNewVersion.Name = "linkLabelNewVersion";
            this.linkLabelNewVersion.Size = new System.Drawing.Size(0, 16);
            this.linkLabelNewVersion.TabIndex = 110;
            this.linkLabelNewVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelNewVersion_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.buttonBenchmark);
            this.panel1.Controls.Add(this.buttonHelp);
            this.panel1.Controls.Add(this.buttonStopMining);
            this.panel1.Controls.Add(this.buttonSettings);
            this.panel1.Controls.Add(this.buttonStartMining);
            this.panel1.Location = new System.Drawing.Point(581, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 435);
            this.panel1.TabIndex = 111;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.panel6.Controls.Add(this.buttonLogo);
            this.panel6.Location = new System.Drawing.Point(-2, -2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(162, 67);
            this.panel6.TabIndex = 114;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Controls.Add(this.linkLabelCheckStats);
            this.panel4.Location = new System.Drawing.Point(4, 319);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(152, 60);
            this.panel4.TabIndex = 113;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelBitcoinAddress);
            this.panel3.Controls.Add(this.textBoxBTCAddress);
            this.panel3.Controls.Add(this.labelWorkerName);
            this.panel3.Controls.Add(this.comboBoxLocation);
            this.panel3.Controls.Add(this.textBoxWorkerName);
            this.panel3.Controls.Add(this.labelServiceLocation);
            this.panel3.Location = new System.Drawing.Point(0, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(578, 62);
            this.panel3.TabIndex = 112;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Controls.Add(this.label_NotProfitable);
            this.panel5.Controls.Add(this.labelDemoMode);
            this.panel5.Controls.Add(this.devicesListViewEnableControl1);
            this.panel5.Location = new System.Drawing.Point(223, 72);
            this.panel5.Margin = new System.Windows.Forms.Padding(5);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(4);
            this.panel5.Size = new System.Drawing.Size(340, 230);
            this.panel5.TabIndex = 113;
            // 
            // devicesListViewEnableControl1
            // 
            this.devicesListViewEnableControl1.BenchmarkCalculation = null;
            this.devicesListViewEnableControl1.FirstColumnText = "Enabled";
            this.devicesListViewEnableControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.devicesListViewEnableControl1.IsInBenchmark = false;
            this.devicesListViewEnableControl1.IsMining = false;
            this.devicesListViewEnableControl1.Location = new System.Drawing.Point(4, 5);
            this.devicesListViewEnableControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.devicesListViewEnableControl1.Name = "devicesListViewEnableControl1";
            this.devicesListViewEnableControl1.SaveToGeneralConfig = false;
            this.devicesListViewEnableControl1.Size = new System.Drawing.Size(332, 220);
            this.devicesListViewEnableControl1.TabIndex = 109;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.richTextBox1.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.richTextBox1.Location = new System.Drawing.Point(12, 91);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(203, 206);
            this.richTextBox1.TabIndex = 114;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.richTextBox2.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.richTextBox2.Location = new System.Drawing.Point(12, 70);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(203, 23);
            this.richTextBox2.TabIndex = 115;
            this.richTextBox2.Text = "Getting Started:";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(743, 434);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linkLabelNewVersion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabelChooseBTCWallet);
            this.Controls.Add(this.statusStrip1);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(576, 316);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZpoolMiner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanelRates.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCCode;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateText;
        private System.Windows.Forms.Button buttonStopMining;
        private System.Windows.Forms.Button buttonBenchmark;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBTCDayText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBTCDayValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceDollarText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceDollarValue;
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}



