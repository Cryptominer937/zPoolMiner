namespace zPoolMiner
{
    using System;
    using System.Windows.Forms;
    using zPoolMiner.Configs;

    /// <summary>
    /// Defines the <see cref="DriverVersionConfirmationDialog" />
    /// </summary>
    public partial class DriverVersionConfirmationDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DriverVersionConfirmationDialog"/> class.
        /// </summary>
        public DriverVersionConfirmationDialog()
        {
            InitializeComponent();

            Text = International.GetText("DriverVersionConfirmationDialog_title");
            labelWarning.Text = International.GetText("DriverVersionConfirmationDialog_labelWarning");
            linkToDriverDownloadPage.Text = International.GetText("DriverVersionConfirmationDialog_linkToDriverDownloadPage");
            chkBoxDontShowAgain.Text = International.GetText("DriverVersionConfirmationDialog_chkBoxDontShowAgain");
            buttonOK.Text = International.GetText("Global_OK");
        }

        /// <summary>
        /// The ButtonOK_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (chkBoxDontShowAgain.Checked)
            {
                Helpers.ConsolePrint("CryptoMiner937", "Setting ShowDriverVersionWarning to false");
                ConfigManager.GeneralConfig.ShowDriverVersionWarning = false;
            }

            Close();
        }

        /// <summary>
        /// The LinkToDriverDownloadPage_LinkClicked
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/></param>
        private void LinkToDriverDownloadPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://support.amd.com/en-us/kb-articles/Pages/Radeon-Software-Crimson-ReLive-Edition-Beta-for-Blockchain-Compute-Release-Notes.aspx");
        }
    }
}