using System;
using System.Windows.Forms;
using zPoolMiner.Enums;

namespace zPoolMiner.Forms.Components
{
    public partial class BenchmarkLimitControl : UserControl
    {
        public string GroupName
        {
            get
            {
                return groupBox1.Text;
            }
            set
            {
                if (value != null) groupBox1.Text = value;
            }
        }

        // int array reference property
        private int[] _timeLimits;

        public int[] TimeLimits
        {
            get { return _timeLimits; }
            set
            {
                if (value != null)
                {
                    _timeLimits = value;

                    for (int indexKey = 0; indexKey < _timeLimits.Length; ++indexKey)
                        _textBoxes[(int)indexKey].Text = _timeLimits[indexKey].ToString();
                }
            }
        }

        // texbox references
        private TextBox[] _textBoxes;

        public BenchmarkLimitControl()
        {
            InitializeComponent();
            textBoxQuick.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
            textBoxStandard.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
            textBoxPrecise.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
            _textBoxes = new TextBox[] { textBoxQuick, textBoxStandard, textBoxPrecise };
            // InitLocale();
        }

        public void SetToolTip(ref ToolTip toolTip, string groupTypeName)
        {
            // TODO old implementation has textBox tooltips that don't work
            toolTip.SetToolTip(labelQuick, string.Format(International.GetText("Form_Settings_ToolTip_BenchmarkTimeLimits"), International.GetText("Quick"), groupTypeName) + ".");
            toolTip.SetToolTip(labelStandard, string.Format(International.GetText("Form_Settings_ToolTip_BenchmarkTimeLimits"), International.GetText("Standard"), groupTypeName) + ".");
            toolTip.SetToolTip(labelPrecise, string.Format(International.GetText("Form_Settings_ToolTip_BenchmarkTimeLimits"), International.GetText("Standard"), groupTypeName) + ".");
        }

        public void InitLocale()
        {
            labelQuick.Text = International.GetText("Quick") + ":";
            labelStandard.Text = International.GetText("Standard") + ":";
            labelPrecise.Text = International.GetText("Precise") + ":";
        }

        // TODO replace  TextChanged Events with TextBox exit events

        #region Events

        private void TextBoxQuick_TextChanged(object sender, EventArgs e)
        {
            SetTimeLimit(BenchmarkPerformanceType.Quick, textBoxQuick.Text);
        }

        private void TextBoxStandard_TextChanged(object sender, EventArgs e)
        {
            SetTimeLimit(BenchmarkPerformanceType.Standard, textBoxStandard.Text);
        }

        private void TextBoxPrecise_TextChanged(object sender, EventArgs e)
        {
            SetTimeLimit(BenchmarkPerformanceType.Precise, textBoxPrecise.Text);
        }

        #endregion Events

        private void SetTimeLimit(BenchmarkPerformanceType type, string numString)
        {
            if (_timeLimits == null) return;

            if (int.TryParse(numString, out int value))
            {
                _timeLimits[(int)type] = value;
            }
        }
    }
}