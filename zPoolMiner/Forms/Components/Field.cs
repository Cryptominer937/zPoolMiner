using System;
using System.Windows.Forms;

namespace zPoolMiner.Forms.Components
{
    public partial class Field : UserControl
    {
        public string LabelText
        {
            get
            {
                return labelFieldIndicator.Text;
            }
            set
            {
                if (value != null)
                {
                    labelFieldIndicator.Text = value;
                }
            }
        }

        public string EntryText
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                if (value != null) textBox.Text = value;
            }
        }

        public Field() => InitializeComponent();

        public void InitLocale(ToolTip toolTip1, string infoLabel, string infoMsg)
        {
            labelFieldIndicator.Text = infoLabel;
            toolTip1.SetToolTip(labelFieldIndicator, infoMsg);
            toolTip1.SetToolTip(textBox, infoMsg);
            toolTip1.SetToolTip(pictureBox1, infoMsg);
        }

        public void SetInputModeDoubleOnly()
        {
            textBox.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxDoubleOnly_KeyPress);
        }

        public void SetInputModeIntOnly()
        {
            textBox.KeyPress += new KeyPressEventHandler(TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress);
        }

        public void SetOnTextChanged(EventHandler textChanged) => textBox.TextChanged += textChanged;

        public void SetOnTextLeave(EventHandler textLeave) => textBox.Leave += textLeave;
    }
}