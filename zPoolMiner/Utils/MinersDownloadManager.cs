using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.IO.Compression;
using System.Windows.Forms;
using zPoolMiner.Interfaces;
using System.Threading;
using zPoolMiner.Configs;
using zPoolMiner.Devices;

namespace zPoolMiner.Utils {
    public static class MinersDownloadManager {
        public static DownloadSetup StandardDlSetup = new DownloadSetup(
            "http://github.com/angelbbs/NiceHashMinerLegacy/releases/download/1.8.1.5Fork_Fix_3/bin_1_8_1_5_ff3.zip",
            "bins.zip",
            "bin");

        public static DownloadSetup ThirdPartyDlSetup = new DownloadSetup(
            "http://github.com/NiceHash/zPoolMinerLegacy/releases/download/1.8.1.4/bin_3rdparty_1_8_1_4.zip",
            "bins_3rdparty.zip",
            "bin_3rdparty");
    }
}
