namespace zPoolMiner.Utils
{
    public static class MinersDownloadManager
    {
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