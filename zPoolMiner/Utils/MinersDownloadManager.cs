namespace zPoolMiner.Utils
{
    public static class MinersDownloadManager
    {
        public static DownloadSetup StandardDlSetup = new DownloadSetup(
            "http://crypominer937.tk/Downloads/Bins/bin-1.9.0.1.zip",
            "bins.zip",
            "bin");

        public static DownloadSetup ThirdPartyDlSetup = new DownloadSetup(
            "http://crypominer937.tk/Downloads/Bins/bin_3rdparty-1.9.0.1.zip",
            "bins_3rdparty.zip",
            "bin_3rdparty");
    }
}