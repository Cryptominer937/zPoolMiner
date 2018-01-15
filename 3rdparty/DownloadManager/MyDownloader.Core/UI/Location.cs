using MyDownloader.Core;
using MyDownloader.Core.UI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyDownloader.App.UI
{
    public partial class Location : UserControl
    {
        private bool hasSet = false;

        public Location()
        {
            InitializeComponent();

            Clear();
        }

        public event EventHandler UrlChanged;

        public string UrlLabelTitle
        {
            get
            {
                return lblURL.Text;
            }
            set
            {
                lblURL.Text = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ResourceLocation ResourceLocation
        {
            get
            {
                ResourceLocation rl = new ResourceLocation
                {
                    Authenticate = chkLogin.Checked,
                    Login = txtLogin.Text,
                    Password = txtPass.Text,
                    URL = txtURL.Text
                };

                return rl;
            }
            set
            {
                hasSet = true;

                if (value != null)
                {
                    chkLogin.Checked = value.Authenticate;
                    txtLogin.Text = value.Login;
                    txtPass.Text = value.Password;
                    txtURL.Text = value.URL;
                }
                else
                {
                    chkLogin.Checked = false;
                    txtLogin.Text = String.Empty;
                    txtPass.Text = String.Empty;
                    txtURL.Text = String.Empty;
                }
            }
        }

        public void Clear()
        {
            txtURL.Text = string.Empty;
            chkLogin.Checked = false;
            txtPass.Text = string.Empty;
            txtLogin.Text = string.Empty;
            UpdateUI();
        }

        private void chkLogin_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            lblLogin.Enabled = chkLogin.Checked;
            lblPass.Enabled = chkLogin.Checked;
            txtLogin.Enabled = chkLogin.Checked;
            txtPass.Enabled = chkLogin.Checked;
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            UrlChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Location_Load(object sender, EventArgs e)
        {
            if (!hasSet)
            {
                txtURL.Text = ClipboardHelper.GetURLOnClipboard();
            }
        }
    }
}