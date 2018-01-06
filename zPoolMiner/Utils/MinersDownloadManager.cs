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
            "https://github.com/Cryptominer937/zPoolMiner/raw/Miner-Additions/Bins/bin_1_8_1_6.zip",
            "bins.zip",
            "bin");

        public static DownloadSetup ThirdPartyDlSetup = new DownloadSetup(
            "https://github.com/Cryptominer937/zPoolMiner/raw/Miner-Additions/Bins/bin_3rdparty_1_8_1_6.zip",
            "bins_3rdparty.zip",
            "bin_3rdparty");
    }
}
