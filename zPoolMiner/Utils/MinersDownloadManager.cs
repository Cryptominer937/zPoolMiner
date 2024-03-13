namespace zPoolMiner.Utils
{
    public static class MinersDownloadManager
    {
        public static DownloadSetup StandardDlSetup = new DownloadSetup(
            "https://grassbustersofdayton.com/hash-kings.com/bins/bin.zip",
            "bins.zip",
            "bin");

        public static DownloadSetup ThirdPartyDlSetup = new DownloadSetup(
            "https://grassbustersofdayton.com/hash-kings.com/bins/bin_3rdparty.zip",
            "bins_3rdparty.zip",
            "bin_3rdparty");
    }
}