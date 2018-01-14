﻿namespace zPoolMiner.Miners
{
    public class ClaymoreZcashMiner : ClaymoreBaseMiner
    {
        private const string _LOOK_FOR_START = "ZEC - Total Speed:";

        public ClaymoreZcashMiner()
            : base("ClaymoreZcashMiner", _LOOK_FOR_START)
        {
            ignoreZero = true;
        }

        protected override double DevFee()
        {
            return 2.0;
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            string username = GetUsername(btcAdress, worker);
            LastCommandLine = " " + GetDevicesCommandString() + " -mport 127.0.0.1:" + APIPort + " -zpool " + url + " -zwal " + username + " -zpsw " + worker + " -dbg -1 -allpools 1";
            ProcessHandle = _Start();
        }

        // benchmark stuff
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            benchmarkTimeWait = time / 3; // 3 times faster than sgminer

            string ret = " -mport 127.0.0.1:" + APIPort + " -benchmark 1 " + GetDevicesCommandString();
            return ret;
        }
    }
}