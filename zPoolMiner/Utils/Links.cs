using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner {
    public static class Links {
        public static string VisitURL = "https://www.nicehash.com?utm_source=NHM";
        // add version
        public static string VisitURLNew = "https://github.com/NiceHash/zPoolMinerLegacy/releases/tag/";
        // add btc adress as parameter
        //public static string CheckStats = "https://www.nicehash.com/index.jsp?utm_source=NHM&p=miners&addr=";
        public static string CheckStats = "http://zpool.ca/?address=";
        // help and faq
        public static string NHM_Help = "https://github.com/Cryptominer937/zPoolMiner/issues";
        public static string NHM_NoDev_Help = "https://github.com/Cryptominer937/zPoolMiner/issues";
        // faq
        public static string NHM_BTC_Wallet_Faq = "https://bitcointalk.org/index.php?topic=2691188";
        public static string NHM_Paying_Faq = "http://zpool.ca";
        // API
        // btc adress as parameter
        public static string NHM_API_stats = "https://api.nicehash.com/api?method=stats.provider&addr=";
        public static string NHM_API_info = "https://api.nicehash.com/api?method=simplemultialgo.info";
        public static string NHM_API_version = "https://api.nicehash.com/zPoolMiner?method=version&legacy";
        //public static string NHM_API_stats_provider_workers = "https://api.nicehash.com/api?method=stats.provider.workers&addr=";

        // device profits
        public static string NHM_Profit_Check = "https://api.nicehash.com/?utm_source=NHM&p=calc&name=";

        // SMA Socket
        public static string NHM_Socket_Address = "wss://api.nicehash.com/v2/nhm";
    }
}
