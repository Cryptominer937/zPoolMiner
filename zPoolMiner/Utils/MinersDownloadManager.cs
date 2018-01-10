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
            "http://github.com/Cryptominer937/zPoolMiner/releases/download/1.8.1.7-BINS/bin-1.8.1.7.zip",
            "bins.zip",
            "bin");

        public static DownloadSetup ThirdPartyDlSetup = new DownloadSetup(
            "http://github.com/Cryptominer937/zPoolMiner/releases/download/1.8.1.7-BINS/bin_3rdparty-1.8.1.7.zip",
            "bins_3rdparty.zip",
            "bin_3rdparty");
    }
}
