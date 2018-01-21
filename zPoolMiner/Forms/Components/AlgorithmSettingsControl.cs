using System;
using System.Windows.Forms;
using zPoolMiner.Devices;
using zPoolMiner.Enums;

namespace zPoolMiner.Forms.Components
{
    public partial class AlgorithmSettingsControl : UserControl, AlgorithmsListView.IAlgorithmsListView
    {
        private ComputeDevice _computeDevice = null;
        private Algorithm _currentlySelectedAlgorithm = null;
        private ListViewItem _currentlySelectedLvi = null;

        // winform crappy event workarond
        private bool _selected = false;

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

        private string ParseStringDefault(string value)
        {
            return value ?? "";
        }

        private string ParseDoubleDefault(double value)
        {
            return value <= 0 ? "" : value.ToString();
        }

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
                this.Enabled = lvi.Checked;

                groupBoxSelectedAlgorithmSettings.Text = String.Format(International.GetText("AlgorithmsListView_GroupBox"),
                String.Format("{0} ({1})", algorithm.AlgorithmName, algorithm.MinerBaseTypeName)); ;

                field_LessThreads.Enabled = _computeDevice.DeviceGroupType == DeviceGroupType.CPU && algorithm.MinerBaseType == MinerBaseType.XmrStackCPU;
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
                secondaryFieldBoxBenchmarkSpeed.Enabled = _currentlySelectedAlgorithm.SecondaryNiceHashID != AlgorithmType.NONE;
                this.Update();
            }
            else
            {
                // TODO this should not be null
            }
        }

        public void HandleCheck(ListViewItem lvi)
        {
            if (Object.ReferenceEquals(_currentlySelectedLvi, lvi))
            {
                this.Enabled = lvi.Checked;
            }
        }

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

        private bool CanEdit()
        {
            return _currentlySelectedAlgorithm != null && _selected;
        }

        #region Callbacks Events

        private void TextChangedBenchmarkSpeed(object sender, EventArgs e)
        {
            if (!CanEdit()) return;
            if (Double.TryParse(fieldBoxBenchmarkSpeed.EntryText, out double value))
            {
                _currentlySelectedAlgorithm.BenchmarkSpeed = value;
            }
            UpdateSpeedText();
        }

        private void SecondaryTextChangedBenchmarkSpeed(object sender, EventArgs e)
        {
            if (Double.TryParse(secondaryFieldBoxBenchmarkSpeed.EntryText, out double secondaryValue))
            {
                _currentlySelectedAlgorithm.SecondaryBenchmarkSpeed = secondaryValue;
            }
            UpdateSpeedText();
        }

        private void UpdateSpeedText()
        {
            var speedString = Helpers.FormatDualSpeedOutput(_currentlySelectedAlgorithm.NiceHashID, _currentlySelectedAlgorithm.BenchmarkSpeed, _currentlySelectedAlgorithm.SecondaryBenchmarkSpeed);
            // update lvi speed
            if (_currentlySelectedLvi != null)
            {
                _currentlySelectedLvi.SubItems[2].Text = speedString;
            }
        }

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

        private void TextChangedExtraLaunchParameters(object sender, EventArgs e)
        {
            if (!CanEdit()) return;
            var ExtraLaunchParams = richTextBoxExtraLaunchParameters.Text.Replace("\r\n", " ");
            ExtraLaunchParams = ExtraLaunchParams.Replace("\n", " ");
            _currentlySelectedAlgorithm.ExtraLaunchParameters = ExtraLaunchParams;
        }

        private void fieldBoxBenchmarkSpeed_Load(object sender, EventArgs e)
        {

        }

        #endregion Callbacks Events

        //private void buttonBenchmark_Click(object sender, EventArgs e) {
        //    var device = new List<ComputeDevice>();
        //    device.Add(_computeDevice);
        //    var BenchmarkForm = new Form_Benchmark(
        //                BenchmarkPerformanceType.Standard,
        //                false, _currentlySelectedAlgorithm.NiceHashID);
        //    BenchmarkForm.ShowDialog();
        //    fieldBoxBenchmarkSpeed.EntryText = _currentlySelectedAlgorithm.BenchmarkSpeed.ToString();
        //    // update lvi speed
        //    if (_currentlySelectedLvi != null) {
        //        _currentlySelectedLvi.SubItems[2].Text = Helpers.FormatSpeedOutput(_currentlySelectedAlgorithm.BenchmarkSpeed);
        //    }
        //}
    }
}