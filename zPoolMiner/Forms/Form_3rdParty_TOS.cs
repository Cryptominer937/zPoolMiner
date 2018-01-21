using System;
using System.Windows.Forms;
using zPoolMiner.Configs;
using zPoolMiner.Enums;

namespace zPoolMiner.Forms
{
    public partial class Form_3rdParty_TOS : Form
    {
        public Form_3rdParty_TOS()
        {
            InitializeComponent();

            // TODO update 3rd party TOS
            this.Text = International.GetText("Form_Main_3rdParty_Title");
            this.label_Tos.Text = International.GetText("Form_Main_3rdParty_Text");
            this.button_Agree.Text = International.GetText("Form_Main_3rdParty_Button_Agree_Text");
            this.button_Decline.Text = International.GetText("Form_Main_3rdParty_Button_Refuse_Text");
        }

        private void Button_Agree_Click(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.Use3rdPartyMiners = Use3rdPartyMiners.YES;
            this.Close();
        }

        private void Button_Decline_Click(object sender, EventArgs e)
        {
            ConfigManager.GeneralConfig.Use3rdPartyMiners = Use3rdPartyMiners.NO;
            this.Close();
        }

        private void label_Tos_Click(object sender, EventArgs e)
        {

        }
    }
}