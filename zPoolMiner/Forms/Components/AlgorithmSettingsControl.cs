namespace zPoolMiner.Forms.Components
{
    using System;
    using System.Windows.Forms;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="AlgorithmSettingsControl" />
    /// </summary>
    public partial class AlgorithmSettingsControl : UserControl, AlgorithmsListView.IAlgorithmsListView
    {
        /// <summary>
        /// Defines the _computeDevice
        /// </summary>
        private ComputeDevice _computeDevice = null;

        /// <summary>
        /// Defines the _currentlySelectedAlgorithm
        /// </summary>
        private Algorithm _currentlySelectedAlgorithm = null;

        /// <summary>
        /// Defines the _currentlySelectedLvi
        /// </summary>
        private ListViewItem _currentlySelectedLvi = null;

        // winform crappy event workarond
        // winform crappy event workarond        /// <summary>
        /// Defines the _selected
        /// </summary>
        private bool _selected = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmSettingsControl"/> class.
        /// </summary>
        public AlgorithmSettingsControl()
        {
            InitializeComponent();
            fieldBoxBenchmarkSpeed.SetInputModeDoubleOnly();
            secondaryFieldBoxBenchmarkSpeed.SetInputModeDoubleOnly();
            field_LessThreads.SetInputModeIntOnly();

            field_LessThreads.SetOnTextLeave(LessThreads_Leave);
            fieldBoxBenchmarkSpeed.SetOnTextChanged(TextChangedBenchmarkSpeed);
            secondaryFieldBoxBenchmarkSpeed.SetOnTextChanged(SecondaryTextChangedBenchmarkSpeed);
            richTextBoxExtraLaunchParameters.TextChanged += TextChangedExtraLaunchParameters;
        }

        /// <summary>
        /// The Deselect
        /// </summary>
        public void Deselect()
        {
            _selected = false;
            groupBoxSelectedAlgorithmSettings.Text = String.Format(International.GetText("AlgorithmsListView_GroupBox"),
                International.GetText("AlgorithmsListView_GroupBox_NONE"));
            Enabled = false;
            fieldBoxBenchmarkSpeed.EntryText = "";
            secondaryFieldBoxBenchmarkSpeed.EntryText = "";
            field_LessThreads.EntryText = "";
            richTextBoxExtraLaunchParameters.Text = "";
        }

        /// <summary>
        /// The InitLocale
        /// </summary>
        /// <param name="toolTip1">The <see cref="ToolTip"/></param>
        public void InitLocale(ToolTip toolTip1)
        {
            field_LessThreads.InitLocale(toolTip1,
                International.GetText("Form_Settings_General_CPU_LessThreads") + ":",
                International.GetText("Form_Settings_ToolTip_CPU_LessThreads"));
            fieldBoxBenchmarkSpeed.InitLocale(toolTip1,
                International.GetText("Form_Settings_Algo_BenchmarkSpeed") + ":",
                International.GetText("Form_Settings_ToolTip_AlgoBenchmarkSpeed"));
            secondaryFieldBoxBenchmarkSpeed.InitLocale(toolTip1,
                International.GetText("Form_Settings_Algo_SecondaryBenchmarkSpeed") + ":",
                International.GetText("Form_Settings_ToolTip_AlgoSecondaryBenchmarkSpeed"));
            groupBoxExtraLaunchParameters.Text = International.GetText("Form_Settings_General_ExtraLaunchParameters");
            toolTip1.SetToolTip(groupBoxExtraLaunchParameters, International.GetText("Form_Settings_ToolTip_AlgoExtraLaunchParameters"));
            toolTip1.SetToolTip(pictureBox1, International.GetText("Form_Settings_ToolTip_AlgoExtraLaunchParameters"));
        }

        /// <summary>
        /// The ParseStringDefault
        /// </summary>
        /// <param name="value">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string ParseStringDefault(string value)
        {
            return value ?? "";
        }

        /// <summary>
        /// The ParseDoubleDefault
        /// </summary>
        /// <param name="value">The <see cref="double"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string ParseDoubleDefault(double value)
        {
            return value <= 0 ? "" : value.ToString();
        }

        /// <summary>
        /// The SetCurrentlySelected
        /// </summary>
        /// <param name="lvi">The <see cref="ListViewItem"/></param>
        /// <param name="computeDevice">The <see cref="ComputeDevice"/></param>
        public void SetCurrentlySelected(ListViewItem lvi, ComputeDevice computeDevice)
        {
            // should not happen ever
            if (lvi == null) return;

            _computeDevice = computeDevice;
            if (lvi.Tag is Algorithm algorithm)
            {
                _selected = true;
                _currentlySelectedAlgorithm = algorithm;
                _currentlySelectedLvi = lvi;
                Enabled = lvi.Checked;

                groupBoxSelectedAlgorithmSettings.Text = String.Format(International.GetText("AlgorithmsListView_GroupBox"),
                String.Format("{0} ({1})", algorithm.AlgorithmName, algorithm.MinerBaseTypeName)); ;

                field_LessThreads.Enabled = _computeDevice.DeviceGroupType == DeviceGroupType.CPU;
                if (field_LessThreads.Enabled)
                {
                    field_LessThreads.EntryText = algorithm.LessThreads.ToString();
                }
                else
                {
                    field_LessThreads.EntryText = "";
                }
                fieldBoxBenchmarkSpeed.EntryText = ParseDoubleDefault(algorithm.BenchmarkSpeed);
                richTextBoxExtraLaunchParameters.Text = ParseStringDefault(algorithm.ExtraLaunchParameters);
                secondaryFieldBoxBenchmarkSpeed.EntryText = ParseDoubleDefault(algorithm.SecondaryBenchmarkSpeed);
                secondaryFieldBoxBenchmarkSpeed.Enabled = _currentlySelectedAlgorithm.SecondaryCryptoMiner937ID != AlgorithmType.NONE;
                Update();
            }
            else
            {
                // TODO this should not be null
            }
        }

        /// <summary>
        /// The HandleCheck
        /// </summary>
        /// <param name="lvi">The <see cref="ListViewItem"/></param>
        public void HandleCheck(ListViewItem lvi)
        {
            if (Object.ReferenceEquals(_currentlySelectedLvi, lvi))
            {
                Enabled = lvi.Checked;
            }
        }

        /// <summary>
        /// The ChangeSpeed
        /// </summary>
        /// <param name="lvi">The <see cref="ListViewItem"/></param>
        public void ChangeSpeed(ListViewItem lvi)
        {
            if (Object.ReferenceEquals(_currentlySelectedLvi, lvi))
            {
                if (lvi.Tag is Algorithm algorithm)
                {
                    fieldBoxBenchmarkSpeed.EntryText = ParseDoubleDefault(algorithm.BenchmarkSpeed);
                    secondaryFieldBoxBenchmarkSpeed.EntryText = ParseDoubleDefault(algorithm.SecondaryBenchmarkSpeed);
                }
            }
        }

        /// <summary>
        /// The CanEdit
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool CanEdit()
        {
            return _currentlySelectedAlgorithm != null && _selected;
        }

        /// <summary>
        /// The TextChangedBenchmarkSpeed
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void TextChangedBenchmarkSpeed(object sender, EventArgs e)
        {
            if (!CanEdit()) return;
            if (Double.TryParse(fieldBoxBenchmarkSpeed.EntryText, out double value))
            {
                _currentlySelectedAlgorithm.BenchmarkSpeed = value;
            }
            UpdateSpeedText();
        }

        /// <summary>
        /// The SecondaryTextChangedBenchmarkSpeed
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void SecondaryTextChangedBenchmarkSpeed(object sender, EventArgs e)
        {
            if (Double.TryParse(secondaryFieldBoxBenchmarkSpeed.EntryText, out double secondaryValue))
            {
                _currentlySelectedAlgorithm.SecondaryBenchmarkSpeed = secondaryValue;
            }
            UpdateSpeedText();
        }

        /// <summary>
        /// The UpdateSpeedText
        /// </summary>
        private void UpdateSpeedText()
        {
            var speedString = Helpers.FormatDualSpeedOutput(_currentlySelectedAlgorithm.CryptoMiner937ID, _currentlySelectedAlgorithm.BenchmarkSpeed, _currentlySelectedAlgorithm.SecondaryBenchmarkSpeed);
            // update lvi speed
            if (_currentlySelectedLvi != null)
            {
                _currentlySelectedLvi.SubItems[2].Text = speedString;
            }
        }

        /// <summary>
        /// The LessThreads_Leave
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void LessThreads_Leave(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            if (Int32.TryParse(txtbox.Text, out int val))
            {
                if (Globals.ThreadsPerCPU - val < 1)
                {
                    MessageBox.Show(International.GetText("Form_Main_msgbox_CPUMiningLessThreadMsg"),
                                    International.GetText("Warning_with_Exclamation"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _currentlySelectedAlgorithm.LessThreads = val;
                }
                txtbox.Text = _currentlySelectedAlgorithm.LessThreads.ToString();
            }
            else
            {
                MessageBox.Show(International.GetText("Form_Settings_LessThreadWarningMsg"),
                                International.GetText("Form_Settings_LessThreadWarningTitle"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbox.Text = _currentlySelectedAlgorithm.LessThreads.ToString();
                txtbox.Focus();
            }
        }

        /// <summary>
        /// The TextChangedExtraLaunchParameters
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void TextChangedExtraLaunchParameters(object sender, EventArgs e)
        {
            if (!CanEdit()) return;
            var ExtraLaunchParams = richTextBoxExtraLaunchParameters.Text.Replace("\r\n", " ");
            ExtraLaunchParams = ExtraLaunchParams.Replace("\n", " ");
            _currentlySelectedAlgorithm.ExtraLaunchParameters = ExtraLaunchParams;
        }

        /// <summary>
        /// The fieldBoxBenchmarkSpeed_Load
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void fieldBoxBenchmarkSpeed_Load(object sender, EventArgs e)
        {
        }
    }
}