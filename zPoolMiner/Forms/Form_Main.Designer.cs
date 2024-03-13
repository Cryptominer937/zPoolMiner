namespace zPoolMiner
{
    /// <summary>
    /// Defines the <see cref="Form_Main" />
    /// </summary>
    public partial class Form_Main
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
            this.labelWorkerName = new System.Windows.Forms.Label();
            this.textBoxWorkerName = new System.Windows.Forms.TextBox();
            this.buttonStopMining = new System.Windows.Forms.Button();
            this.buttonBenchmark = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.linkLabelChooseBTCWallet = new System.Windows.Forms.LinkLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.labelDemoMode = new System.Windows.Forms.Label();
            this.flowLayoutPanelRates = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_NotProfitable = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.LinkLabelUpdate = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonLogo = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelDevfeeStatus = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.deviceStats1 = new zPoolMiner.Forms.Components.DeviceStats();
            this.devicesListViewEnableControl1 = new zPoolMiner.Forms.Components.DevicesListViewEnableControl();
            this.tempLower = new System.Windows.Forms.NumericUpDown();
            this.tempUpper = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.beepToggle = new zPoolMiner.Forms.Components.Toggle();
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanelRates.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempUpper)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartMining
            // 
            this.buttonStartMining.BackColor = System.Drawing.Color.Transparent;
            this.buttonStartMining.BackgroundImage = global::zPoolMiner.Properties.Resources.button_start;
            this.buttonStartMining.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonStartMining.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStartMining.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.buttonStartMining.FlatAppearance.BorderSize = 0;
            this.buttonStartMining.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonStartMining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStartMining.Font = new System.Drawing.Font("Franklin Gothic Medium", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartMining.ForeColor = System.Drawing.Color.Transparent;
            this.buttonStartMining.Location = new System.Drawing.Point(0, 0);
            this.buttonStartMining.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStartMining.Name = "buttonStartMining";
            this.buttonStartMining.Size = new System.Drawing.Size(92, 67);
            this.buttonStartMining.TabIndex = 6;
            this.buttonStartMining.UseVisualStyleBackColor = false;
            this.buttonStartMining.Click += new System.EventHandler(this.ButtonStartMining_Click);
            // 
            // textBoxBTCAddress
            // 
            this.textBoxBTCAddress.Location = new System.Drawing.Point(337, 13);
            this.textBoxBTCAddress.Name = "textBoxBTCAddress";
            this.textBoxBTCAddress.Size = new System.Drawing.Size(131, 21);
            this.textBoxBTCAddress.TabIndex = 1;
            this.textBoxBTCAddress.Visible = false;
            this.textBoxBTCAddress.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // labelServiceLocation
            // 
            this.labelServiceLocation.AutoSize = true;
            this.labelServiceLocation.ForeColor = System.Drawing.SystemColors.Control;
            this.labelServiceLocation.Location = new System.Drawing.Point(256, 47);
            this.labelServiceLocation.Name = "labelServiceLocation";
            this.labelServiceLocation.Size = new System.Drawing.Size(90, 16);
            this.labelServiceLocation.TabIndex = 99;
            this.labelServiceLocation.Text = "Service location:";
            this.labelServiceLocation.Visible = false;
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
            this.comboBoxLocation.Location = new System.Drawing.Point(353, 42);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(121, 24);
            this.comboBoxLocation.TabIndex = 0;
            this.comboBoxLocation.Visible = false;
            this.comboBoxLocation.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // labelBitcoinAddress
            // 
            this.labelBitcoinAddress.AutoSize = true;
            this.labelBitcoinAddress.ForeColor = System.Drawing.SystemColors.Control;
            this.labelBitcoinAddress.Location = new System.Drawing.Point(245, 16);
            this.labelBitcoinAddress.Name = "labelBitcoinAddress";
            this.labelBitcoinAddress.Size = new System.Drawing.Size(85, 16);
            this.labelBitcoinAddress.TabIndex = 99;
            this.labelBitcoinAddress.Text = "Payout address:";
            this.labelBitcoinAddress.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(230)))));
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(707, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelGlobalRateText
            // 
            this.toolStripStatusLabelGlobalRateText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelGlobalRateText.Name = "toolStripStatusLabelGlobalRateText";
            this.toolStripStatusLabelGlobalRateText.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabelGlobalRateText.Text = "Global rate:";
            // 
            // toolStripStatusLabelGlobalRateValue
            // 
            this.toolStripStatusLabelGlobalRateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelGlobalRateValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelGlobalRateValue.Name = "toolStripStatusLabelGlobalRateValue";
            this.toolStripStatusLabelGlobalRateValue.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabelGlobalRateValue.Text = "0.00000000";
            // 
            // toolStripStatusLabelBTCDayText
            // 
            this.toolStripStatusLabelBTCDayText.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabelBTCDayText.Name = "toolStripStatusLabelBTCDayText";
            this.toolStripStatusLabelBTCDayText.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabelBTCDayText.Text = "BTC/Day";
            // 
            // toolStripStatusLabelBTCDayValue
            // 
            this.toolStripStatusLabelBTCDayValue.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabelBTCDayValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBTCDayValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelBTCDayValue.Name = "toolStripStatusLabelBTCDayValue";
            this.toolStripStatusLabelBTCDayValue.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabelBTCDayValue.Text = "0.00";
            // 
            // toolStripStatusLabelBalanceText
            // 
            this.toolStripStatusLabelBalanceText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelBalanceText.Name = "toolStripStatusLabelBalanceText";
            this.toolStripStatusLabelBalanceText.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabelBalanceText.Text = "$/Day";
            // 
            // toolStripStatusLabelBalanceBTCValue
            // 
            this.toolStripStatusLabelBalanceBTCValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBalanceBTCValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelBalanceBTCValue.Name = "toolStripStatusLabelBalanceBTCValue";
            this.toolStripStatusLabelBalanceBTCValue.Size = new System.Drawing.Size(73, 20);
            this.toolStripStatusLabelBalanceBTCValue.Text = "0.00000000";
            this.toolStripStatusLabelBalanceBTCValue.Visible = false;
            // 
            // toolStripStatusLabelBalanceBTCCode
            // 
            this.toolStripStatusLabelBalanceBTCCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelBalanceBTCCode.Name = "toolStripStatusLabelBalanceBTCCode";
            this.toolStripStatusLabelBalanceBTCCode.Size = new System.Drawing.Size(26, 20);
            this.toolStripStatusLabelBalanceBTCCode.Text = "BTC";
            this.toolStripStatusLabelBalanceBTCCode.Visible = false;
            // 
            // toolStripStatusLabelBalanceDollarText
            // 
            this.toolStripStatusLabelBalanceDollarText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelBalanceDollarText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatusLabelBalanceDollarText.Name = "toolStripStatusLabelBalanceDollarText";
            this.toolStripStatusLabelBalanceDollarText.Size = new System.Drawing.Size(31, 20);
            this.toolStripStatusLabelBalanceDollarText.Text = "0.00";
            this.toolStripStatusLabelBalanceDollarText.Visible = false;
            // 
            // toolStripStatusLabelBalanceDollarValue
            // 
            this.toolStripStatusLabelBalanceDollarValue.ForeColor = System.Drawing.SystemColors.WindowText;
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
            // labelWorkerName
            // 
            this.labelWorkerName.AutoSize = true;
            this.labelWorkerName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelWorkerName.Location = new System.Drawing.Point(120, 54);
            this.labelWorkerName.Name = "labelWorkerName";
            this.labelWorkerName.Size = new System.Drawing.Size(77, 16);
            this.labelWorkerName.TabIndex = 99;
            this.labelWorkerName.Text = "Worker name:";
            this.labelWorkerName.Visible = false;
            // 
            // textBoxWorkerName
            // 
            this.textBoxWorkerName.Location = new System.Drawing.Point(202, 52);
            this.textBoxWorkerName.Name = "textBoxWorkerName";
            this.textBoxWorkerName.Size = new System.Drawing.Size(28, 21);
            this.textBoxWorkerName.TabIndex = 2;
            this.textBoxWorkerName.Visible = false;
            this.textBoxWorkerName.Leave += new System.EventHandler(this.TextBoxCheckBoxMain_Leave);
            // 
            // buttonStopMining
            // 
            this.buttonStopMining.BackColor = System.Drawing.Color.Transparent;
            this.buttonStopMining.BackgroundImage = global::zPoolMiner.Properties.Resources.button_stop;
            this.buttonStopMining.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonStopMining.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStopMining.Enabled = false;
            this.buttonStopMining.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.buttonStopMining.FlatAppearance.BorderSize = 0;
            this.buttonStopMining.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonStopMining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStopMining.ForeColor = System.Drawing.Color.Transparent;
            this.buttonStopMining.Location = new System.Drawing.Point(92, 0);
            this.buttonStopMining.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStopMining.Name = "buttonStopMining";
            this.buttonStopMining.Size = new System.Drawing.Size(90, 67);
            this.buttonStopMining.TabIndex = 7;
            this.buttonStopMining.UseVisualStyleBackColor = false;
            this.buttonStopMining.Click += new System.EventHandler(this.ButtonStopMining_Click);
            // 
            // buttonBenchmark
            // 
            this.buttonBenchmark.BackColor = System.Drawing.Color.Transparent;
            this.buttonBenchmark.BackgroundImage = global::zPoolMiner.Properties.Resources.button_benchmark;
            this.buttonBenchmark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonBenchmark.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonBenchmark.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.buttonBenchmark.FlatAppearance.BorderSize = 0;
            this.buttonBenchmark.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonBenchmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBenchmark.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBenchmark.ForeColor = System.Drawing.Color.Transparent;
            this.buttonBenchmark.Location = new System.Drawing.Point(392, 0);
            this.buttonBenchmark.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.buttonBenchmark.Name = "buttonBenchmark";
            this.buttonBenchmark.Size = new System.Drawing.Size(135, 67);
            this.buttonBenchmark.TabIndex = 4;
            this.buttonBenchmark.UseVisualStyleBackColor = false;
            this.buttonBenchmark.Click += new System.EventHandler(this.ButtonBenchmark_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.Transparent;
            this.buttonSettings.BackgroundImage = global::zPoolMiner.Properties.Resources.button_settings;
            this.buttonSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.ForeColor = System.Drawing.Color.Transparent;
            this.buttonSettings.Location = new System.Drawing.Point(605, 0);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(101, 67);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // linkLabelChooseBTCWallet
            // 
            this.linkLabelChooseBTCWallet.AutoSize = true;
            this.linkLabelChooseBTCWallet.Location = new System.Drawing.Point(325, 439);
            this.linkLabelChooseBTCWallet.Name = "linkLabelChooseBTCWallet";
            this.linkLabelChooseBTCWallet.Size = new System.Drawing.Size(174, 16);
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
            this.labelDemoMode.Location = new System.Drawing.Point(-181, -15);
            this.labelDemoMode.Name = "labelDemoMode";
            this.labelDemoMode.Size = new System.Drawing.Size(498, 25);
            this.labelDemoMode.TabIndex = 100;
            this.labelDemoMode.Text = "NiceHash Miner Legacy is running in DEMO mode!";
            this.labelDemoMode.Visible = false;
            // 
            // flowLayoutPanelRates
            // 
            this.flowLayoutPanelRates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelRates.AutoScroll = true;
            this.flowLayoutPanelRates.Controls.Add(this.panel2);
            this.flowLayoutPanelRates.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelRates.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanelRates.Name = "flowLayoutPanelRates";
            this.flowLayoutPanelRates.Size = new System.Drawing.Size(564, 32);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label_NotProfitable);
            this.groupBox1.Controls.Add(this.flowLayoutPanelRates);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(1, 395);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 69);
            this.groupBox1.TabIndex = 108;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group/Device Rates:";
            // 
            // label_NotProfitable
            // 
            this.label_NotProfitable.AutoSize = true;
            this.label_NotProfitable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_NotProfitable.ForeColor = System.Drawing.Color.Red;
            this.label_NotProfitable.Location = new System.Drawing.Point(6, 0);
            this.label_NotProfitable.Name = "label_NotProfitable";
            this.label_NotProfitable.Size = new System.Drawing.Size(366, 24);
            this.label_NotProfitable.TabIndex = 110;
            this.label_NotProfitable.Text = "CURRENTLY MINING NOT PROFITABLE.";
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackColor = System.Drawing.Color.Transparent;
            this.buttonHelp.BackgroundImage = global::zPoolMiner.Properties.Resources.button_stats;
            this.buttonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelp.ForeColor = System.Drawing.Color.Transparent;
            this.buttonHelp.Location = new System.Drawing.Point(527, 0);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(78, 67);
            this.buttonHelp.TabIndex = 8;
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
            // 
            // LinkLabelUpdate
            // 
            this.LinkLabelUpdate.AutoSize = true;
            this.LinkLabelUpdate.Dock = System.Windows.Forms.DockStyle.Right;
            this.LinkLabelUpdate.Font = new System.Drawing.Font("Franklin Gothic Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabelUpdate.LinkArea = new System.Windows.Forms.LinkArea(25, 25);
            this.LinkLabelUpdate.Location = new System.Drawing.Point(704, 0);
            this.LinkLabelUpdate.Name = "LinkLabelUpdate";
            this.LinkLabelUpdate.Size = new System.Drawing.Size(0, 37);
            this.LinkLabelUpdate.TabIndex = 110;
            this.LinkLabelUpdate.UseCompatibleTextRendering = true;
            this.LinkLabelUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelUpdate_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.buttonBenchmark);
            this.panel1.Controls.Add(this.buttonHelp);
            this.panel1.Controls.Add(this.buttonStopMining);
            this.panel1.Controls.Add(this.buttonSettings);
            this.panel1.Controls.Add(this.buttonStartMining);
            this.panel1.Location = new System.Drawing.Point(1, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(706, 67);
            this.panel1.TabIndex = 111;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.buttonLogo);
            this.panel6.Controls.Add(this.labelDemoMode);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(182, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(210, 67);
            this.panel6.TabIndex = 114;
            // 
            // buttonLogo
            // 
            this.buttonLogo.BackColor = System.Drawing.Color.Transparent;
            this.buttonLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLogo.FlatAppearance.BorderSize = 0;
            this.buttonLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogo.Image = global::zPoolMiner.Properties.Resources.HK_Main_Small;
            this.buttonLogo.Location = new System.Drawing.Point(0, 0);
            this.buttonLogo.MaximumSize = new System.Drawing.Size(210, 67);
            this.buttonLogo.Name = "buttonLogo";
            this.buttonLogo.Size = new System.Drawing.Size(210, 61);
            this.buttonLogo.TabIndex = 11;
            this.buttonLogo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.buttonLogo.UseMnemonic = false;
            this.buttonLogo.UseVisualStyleBackColor = false;
            this.buttonLogo.Click += new System.EventHandler(this.ButtonLogo_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.labelDevfeeStatus);
            this.panel3.Controls.Add(this.LinkLabelUpdate);
            this.panel3.Controls.Add(this.labelBitcoinAddress);
            this.panel3.Controls.Add(this.textBoxBTCAddress);
            this.panel3.Controls.Add(this.labelWorkerName);
            this.panel3.Controls.Add(this.comboBoxLocation);
            this.panel3.Controls.Add(this.textBoxWorkerName);
            this.panel3.Controls.Add(this.labelServiceLocation);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(706, 76);
            this.panel3.TabIndex = 112;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(-1, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 103;
            this.label5.Text = "3. Start Mining ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 102;
            this.label4.Text = "2. Benchmark your cards ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(293, 16);
            this.label3.TabIndex = 101;
            this.label3.Text = "1. Set options in Settings > Pools/Usernames/Passwords ";
            // 
            // labelDevfeeStatus
            // 
            this.labelDevfeeStatus.AutoSize = true;
            this.labelDevfeeStatus.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDevfeeStatus.ForeColor = System.Drawing.Color.White;
            this.labelDevfeeStatus.Location = new System.Drawing.Point(-2, 47);
            this.labelDevfeeStatus.Name = "labelDevfeeStatus";
            this.labelDevfeeStatus.Size = new System.Drawing.Size(116, 26);
            this.labelDevfeeStatus.TabIndex = 100;
            this.labelDevfeeStatus.Text = "Mining For: ";
            // 
            // panel5
            // 
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.panel5.Controls.Add(this.deviceStats1);
            this.panel5.Controls.Add(this.devicesListViewEnableControl1);
            this.panel5.Location = new System.Drawing.Point(1, 137);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(703, 255);
            this.panel5.TabIndex = 114;
            // 
            // deviceStats1
            // 
            this.deviceStats1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceStats1.AutoSize = true;
            this.deviceStats1.BackColor = System.Drawing.SystemColors.Window;
            this.deviceStats1.BenchmarkCalculation = null;
            this.deviceStats1.FirstColumnText = "Device Stats";
            this.deviceStats1.IsInBenchmark = false;
            this.deviceStats1.IsMining = false;
            this.deviceStats1.Location = new System.Drawing.Point(327, 168);
            this.deviceStats1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deviceStats1.Name = "deviceStats1";
            this.deviceStats1.SaveToGeneralConfig = false;
            this.deviceStats1.Size = new System.Drawing.Size(101, 0);
            this.deviceStats1.TabIndex = 110;
            this.deviceStats1.Visible = false;
            // 
            // devicesListViewEnableControl1
            // 
            this.devicesListViewEnableControl1.AutoSize = true;
            this.devicesListViewEnableControl1.BackColor = System.Drawing.SystemColors.Window;
            this.devicesListViewEnableControl1.BenchmarkCalculation = null;
            this.devicesListViewEnableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devicesListViewEnableControl1.FirstColumnText = "Enabled";
            this.devicesListViewEnableControl1.ForeColor = System.Drawing.Color.Black;
            this.devicesListViewEnableControl1.IsInBenchmark = false;
            this.devicesListViewEnableControl1.IsMining = false;
            this.devicesListViewEnableControl1.Location = new System.Drawing.Point(0, 0);
            this.devicesListViewEnableControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.devicesListViewEnableControl1.Name = "devicesListViewEnableControl1";
            this.devicesListViewEnableControl1.SaveToGeneralConfig = false;
            this.devicesListViewEnableControl1.Size = new System.Drawing.Size(703, 255);
            this.devicesListViewEnableControl1.TabIndex = 109;
            // 
            // tempLower
            // 
            this.tempLower.Location = new System.Drawing.Point(78, 51);
            this.tempLower.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.tempLower.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tempLower.Name = "tempLower";
            this.tempLower.Size = new System.Drawing.Size(47, 21);
            this.tempLower.TabIndex = 115;
            this.tempLower.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tempLower.ValueChanged += new System.EventHandler(this.tempLower_ValueChanged);
            // 
            // tempUpper
            // 
            this.tempUpper.Location = new System.Drawing.Point(78, 74);
            this.tempUpper.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.tempUpper.Name = "tempUpper";
            this.tempUpper.Size = new System.Drawing.Size(47, 21);
            this.tempUpper.TabIndex = 116;
            this.tempUpper.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.tempUpper.ValueChanged += new System.EventHandler(this.tempUpper_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 118;
            this.label1.Text = "Lower Temp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 119;
            this.label2.Text = "Upper Temp";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.beepToggle);
            this.groupBox2.Controls.Add(this.tempUpper);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tempLower);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(577, 395);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 100);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Temp Monitoring";
            // 
            // beepToggle
            // 
            this.beepToggle.BackColor = System.Drawing.Color.Transparent;
            this.beepToggle.Location = new System.Drawing.Point(72, 20);
            this.beepToggle.MaximumSize = new System.Drawing.Size(150, 50);
            this.beepToggle.MinimumSize = new System.Drawing.Size(30, 30);
            this.beepToggle.Name = "beepToggle";
            this.beepToggle.Padding = new System.Windows.Forms.Padding(6);
            this.beepToggle.Size = new System.Drawing.Size(53, 30);
            this.beepToggle.TabIndex = 117;
            this.beepToggle.Text = "----";
            this.beepToggle.UseVisualStyleBackColor = false;
            this.beepToggle.CheckedChanged += new System.EventHandler(this.beepToggle_CheckedChanged);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(707, 525);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabelChooseBTCWallet);
            this.Controls.Add(this.statusStrip1);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(723, 564);
            this.Name = "Form_Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hash-Kings - Miner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanelRates.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tempLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempUpper)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Defines the labelServiceLocation
        /// </summary>
        private System.Windows.Forms.Label labelServiceLocation;

        /// <summary>
        /// Defines the labelBitcoinAddress
        /// </summary>
        private System.Windows.Forms.Label labelBitcoinAddress;

        /// <summary>
        /// Defines the statusStrip1
        /// </summary>
        private System.Windows.Forms.StatusStrip statusStrip1;

        /// <summary>
        /// Defines the labelWorkerName
        /// </summary>
        private System.Windows.Forms.Label labelWorkerName;

        /// <summary>
        /// Defines the toolStripStatusLabelGlobalRateValue
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateValue;

        /// <summary>
        /// Defines the toolStripStatusLabelBalanceText
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceText;

        /// <summary>
        /// Defines the toolStripStatusLabelBalanceBTCValue
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCValue;

        /// <summary>
        /// Defines the toolStripStatusLabelBalanceBTCCode
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceBTCCode;

        /// <summary>
        /// Defines the toolStripStatusLabelGlobalRateText
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGlobalRateText;

        /// <summary>
        /// Defines the toolStripStatusLabelBTCDayText
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBTCDayText;

        /// <summary>
        /// Defines the toolStripStatusLabelBTCDayValue
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBTCDayValue;

        /// <summary>
        /// Defines the toolStripStatusLabelBalanceDollarText
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceDollarText;

        /// <summary>
        /// Defines the toolStripStatusLabelBalanceDollarValue
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBalanceDollarValue;

        /// <summary>
        /// Defines the toolStripStatusLabel10
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;

        /// <summary>
        /// Defines the notifyIcon1
        /// </summary>
        private System.Windows.Forms.NotifyIcon notifyIcon1;

        /// <summary>
        /// Defines the textBoxBTCAddress
        /// </summary>
        private System.Windows.Forms.TextBox textBoxBTCAddress;

        /// <summary>
        /// Defines the comboBoxLocation
        /// </summary>
        private System.Windows.Forms.ComboBox comboBoxLocation;

        /// <summary>
        /// Defines the textBoxWorkerName
        /// </summary>
        private System.Windows.Forms.TextBox textBoxWorkerName;

        /// <summary>
        /// Defines the linkLabelChooseBTCWallet
        /// </summary>
        private System.Windows.Forms.LinkLabel linkLabelChooseBTCWallet;

        /// <summary>
        /// Defines the labelDemoMode
        /// </summary>
        private System.Windows.Forms.Label labelDemoMode;

        /// <summary>
        /// Defines the flowLayoutPanelRates
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRates;

        /// <summary>
        /// Defines the groupBox1
        /// </summary>
        private System.Windows.Forms.GroupBox groupBox1;

        /// <summary>
        /// Defines the label_NotProfitable
        /// </summary>
        private System.Windows.Forms.Label label_NotProfitable;

        /// <summary>
        /// Defines the devicesListViewEnableControl1
        /// </summary>
        private Forms.Components.DevicesListViewEnableControl devicesListViewEnableControl1;

        /// <summary>
        /// Defines the LinkLabelUpdate
        /// </summary>
        private System.Windows.Forms.LinkLabel LinkLabelUpdate;

        /// <summary>
        /// Defines the toolTip1
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;

        /// <summary>
        /// Defines the toolStripStatusLabel1
        /// </summary>
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

        /// <summary>
        /// Defines the panel1
        /// </summary>
        private System.Windows.Forms.Panel panel1;

        /// <summary>
        /// Defines the panel2
        /// </summary>
        private System.Windows.Forms.Panel panel2;

        /// <summary>
        /// Defines the panel3
        /// </summary>
        private System.Windows.Forms.Panel panel3;

        /// <summary>
        /// Defines the panel6
        /// </summary>
        private System.Windows.Forms.Panel panel6;
        private Forms.Components.DeviceStats deviceStats;
        private System.Windows.Forms.Panel panel5;
        private Forms.Components.DeviceStats deviceStats1;
        private System.Windows.Forms.NumericUpDown tempLower;
        private System.Windows.Forms.NumericUpDown tempUpper;
        private Forms.Components.Toggle beepToggle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label labelDevfeeStatus;
        private System.Windows.Forms.Button buttonStartMining;
        private System.Windows.Forms.Button buttonStopMining;
        private System.Windows.Forms.Button buttonBenchmark;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonLogo;
    }
}
