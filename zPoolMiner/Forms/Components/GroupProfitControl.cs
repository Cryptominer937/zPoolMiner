﻿using System;
using System.Windows.Forms;

namespace zPoolMiner.Forms.Components
{
    public partial class GroupProfitControl : UserControl
    {
        public GroupProfitControl()
        {
            InitializeComponent();

            labelSpeedIndicator.Text = International.GetText("ListView_Speed");
            labelBTCRateIndicator.Text = International.GetText("Rate");
        }

        public void UpdateProfitStats(string groupName, string deviceStringInfo,
            string speedString, string btcRateString, string currencyRateString)
        {
            groupBoxMinerGroup.Text = String.Format(International.GetText("Form_Main_MiningDevices"), deviceStringInfo);
            labelSpeedValue.Text = speedString;
            labelBTCRateValue.Text = btcRateString;
            labelCurentcyPerDayVaue.Text = currencyRateString;
        }
    }
}