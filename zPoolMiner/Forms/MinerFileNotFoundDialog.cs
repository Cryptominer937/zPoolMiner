using System;
using System.Windows.Forms;

namespace zPoolMiner
{
    // TODO probably remove
    public partial class MinerFileNotFoundDialog : Form
    {
        public bool DisableDetection;

        public MinerFileNotFoundDialog(string MinerDeviceName, string Path)
        {
            InitializeComponent();

            DisableDetection = false;
            this.Text = International.GetText("MinerFileNotFoundDialog_title");
            linkLabelError.Text = String.Format(International.GetText("MinerFileNotFoundDialog_linkLabelError"), MinerDeviceName, Path, International.GetText("MinerFileNotFoundDialog_link"));
            linkLabelError.LinkArea = new LinkArea(this.linkLabelError.Text.IndexOf(International.GetText("MinerFileNotFoundDialog_link")), International.GetText("MinerFileNotFoundDialog_link").Length);
            chkBoxDisableDetection.Text = International.GetText("MinerFileNotFoundDialog_chkBoxDisableDetection");
            buttonOK.Text = International.GetText("Global_OK");
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (chkBoxDisableDetection.Checked)
                DisableDetection = true;

            this.Close();
        }

        private void LinkLabelError_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Cryptominer937/zPoolMiner/issues");
        }
    }
}