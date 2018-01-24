using System;
using zPoolMiner.Configs;

namespace zPoolMiner
{
    public static class Ethereum
    {
        //public static string EtherMinerPath;
        public static string CurrentBlockNum;

        static Ethereum()
        {
            CurrentBlockNum = "";
        }

        public static void GetCurrentBlock(string worker)
        {
            string ret = CryptoStats.GetCryptominerAPIData("https://etherchain.org/api/blocks/count", worker);

            if (ret == null)
            {
                Helpers.ConsolePrint(worker, String.Format("Failed to obtain current block, using default {0}.", ConfigManager.GeneralConfig.ethminerDefaultBlockHeight));
                CurrentBlockNum = ConfigManager.GeneralConfig.ethminerDefaultBlockHeight.ToString();
            }
            else
            {
                ret = ret.Substring(ret.LastIndexOf("count") + 7);
                CurrentBlockNum = ret.Substring(0, ret.Length - 3);
            }
        }
    }
}