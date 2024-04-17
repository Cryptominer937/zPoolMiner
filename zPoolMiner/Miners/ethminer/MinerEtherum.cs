namespace zPoolMiner.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Grouping;

    /// <summary>
    /// For now used only for daggerhashimoto
    /// </summary>
    public abstract class MinerEtherum : Miner
    {
        private const double DevFee = 6.0;

        //ComputeDevice
        //ComputeDevice        /// <summary>
        /// Defines the DaggerHashimotoGenerateDevice
        /// </summary>
        protected ComputeDevice DaggerHashimotoGenerateDevice;

        /// <summary>
        /// Defines the CurrentBlockString
        /// </summary>
        protected readonly string CurrentBlockString;

        /// <summary>
        /// Defines the DagGenerationType
        /// </summary>
        private readonly DagGenerationType DagGenerationType;

        /// <summary>
        /// Defines the IsPaused
        /// </summary>
        protected bool IsPaused = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinerEtherum"/> class.
        /// </summary>
        /// <param name="minerDeviceName">The <see cref="string"/></param>
        /// <param name="blockString">The <see cref="string"/></param>
        public MinerEtherum(string minerDeviceName, string blockString)
            : base(minerDeviceName)
        {
            CurrentBlockString = blockString;
            DagGenerationType = ConfigManager.GeneralConfig.EthminerDagGenerationType;
        }

        /// <summary>
        /// The GET_MAX_CooldownTimeInMilliseconds
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 90 * 1000; // 1.5 minute max, whole waiting time 75seconds
        }

        /// <summary>
        /// The GetStartCommandStringPart
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="username">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected abstract string GetStartCommandStringPart(string url, string username);

        /// <summary>
        /// The GetBenchmarkCommandStringPart
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected abstract string GetBenchmarkCommandStringPart(Algorithm algorithm);

        /// <summary>
        /// The GetDevicesCommandString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        protected override string GetDevicesCommandString()
        {
            string deviceStringCommand = " ";

            List<string> ids = new List<string>();
            foreach (var mPair in MiningSetup.MiningPairs)
            {
                ids.Add(mPair.Device.ID.ToString());
            }
            deviceStringCommand += String.Join(" ", ids);
            // set dag load mode
            deviceStringCommand += String.Format(" --dag-load-mode {0} ", GetDagGenerationString(DagGenerationType));
            if (DagGenerationType == DagGenerationType.Single
                || DagGenerationType == DagGenerationType.SingleKeep)
            {
                // set dag generation device
                deviceStringCommand += DaggerHashimotoGenerateDevice.ID.ToString();
            }
            return deviceStringCommand;
        }

        /// <summary>
        /// The GetDagGenerationString
        /// </summary>
        /// <param name="type">The <see cref="DagGenerationType"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetDagGenerationString(DagGenerationType type)
        {
            switch (type)
            {
                case DagGenerationType.Parallel:
                    return "parallel";

                case DagGenerationType.Sequential:
                    return "sequential";

                case DagGenerationType.Single:
                    return "single";

                case DagGenerationType.SingleKeep:
                    return "singlekeep";
            }
            return "singlekeep";
        }

        /// <summary>
        /// The Start
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        /// <param name="usedMiners">The <see cref="List{MinerEtherum}"/></param>
        public void Start(string url, string btcAddress, string worker, List<MinerEtherum> usedMiners)
        {
            if (MiningSession.DONATION_SESSION)
            {
                if (url.Contains("zpool.ca"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("nicehash.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("zergpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("blazepool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }
                if (url.Contains("miningpoolhub.com"))
                {
                    btcAddress = "cryptominer.Devfee";
                    worker = "x";
                }
                else
                {
                    btcAddress = Globals.DemoUser;
                }
            }
            else
            {
                if (url.Contains("zpool.ca"))
                {
                    btcAddress = zPoolMiner.Globals.GetzpoolUser();
                    worker = zPoolMiner.Globals.GetzpoolWorker();
                }
                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetahashUser();
                    worker = zPoolMiner.Globals.GetahashWorker();
                }
                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = zPoolMiner.Globals.GethashrefineryUser();
                    worker = zPoolMiner.Globals.GethashrefineryWorker();
                }
                if (url.Contains("nicehash.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetnicehashUser();
                    worker = zPoolMiner.Globals.GetnicehashWorker();
                }
                if (url.Contains("zergpool.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetzergUser();
                    worker = zPoolMiner.Globals.GetzergWorker() + "";
                }
                if (url.Contains("minemoney.co"))
                {
                    btcAddress = zPoolMiner.Globals.GetminemoneyUser();
                    worker = zPoolMiner.Globals.GetminemoneyWorker();
                }
                if (url.Contains("blazepool.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetblazepoolUser();
                    worker = zPoolMiner.Globals.GetblazepoolWorker();
                }
                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = zPoolMiner.Globals.GetblockmunchUser();
                    worker = zPoolMiner.Globals.GetblockmunchWorker();
                }
                if (url.Contains("miningpoolhub.com"))
                {
                    btcAddress = zPoolMiner.Globals.GetMPHUser();
                    worker = zPoolMiner.Globals.GetMPHWorker();
                }
            }
            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }
            foreach (var ethminer in usedMiners)
            {
                if (ethminer.MINER_ID != MINER_ID && (ethminer.IsRunning || ethminer.IsPaused))
                {
                    Helpers.ConsolePrint(MinerTag(), String.Format("Will end {0} {1}", ethminer.MinerTag(), ethminer.ProcessTag()));
                    ethminer.End();
                    System.Threading.Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);
                }
            }

            IsPaused = false;
            if (ProcessHandle == null)
            {
                string username = GetUsername(btcAddress, worker);
                LastCommandLine = GetStartCommandStringPart(url, username) + GetDevicesCommandString();
                ProcessHandle = _Start();
            }
            else
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Resuming ethminer..");
                StartCoolDownTimerChecker();
                StartMining();
            }
        }

        /// <summary>
        /// The BenchmarkCreateCommandLine
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <param name="time">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            string CommandLine = GetBenchmarkCommandStringPart(algorithm) + GetDevicesCommandString();
            Ethereum.GetCurrentBlock(CurrentBlockString);
            CommandLine += " --benchmark " + Ethereum.CurrentBlockNum;

            return CommandLine;
        }

        /// <summary>
        /// The InitMiningSetup
        /// </summary>
        /// <param name="miningSetup">The <see cref="MiningSetup"/></param>
        public override void InitMiningSetup(MiningSetup miningSetup)
        {
            base.InitMiningSetup(miningSetup);
            // now find the fastest for DAG generation
            double fastestSpeed = double.MinValue;
            foreach (var mPair in MiningSetup.MiningPairs)
            {
                double compareSpeed = mPair.Algorithm.AvaragedSpeed;
                if (fastestSpeed < compareSpeed)
                {
                    DaggerHashimotoGenerateDevice = mPair.Device;
                    fastestSpeed = compareSpeed;
                }
            }
        }

        /// <summary>
        /// The GetSummaryAsync
        /// </summary>
        /// <returns>The <see cref="Task{APIData}"/></returns>
        public override Task<ApiData> GetSummaryAsync()
        {
            ApiData ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            var getSpeedStatus = GetSpeed(out bool ismining, out ad.Speed);
            if (GetSpeedStatus.GOT == getSpeedStatus)
            {
                // fix MH/s
                ad.Speed *= 1000 * 1000;
                CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                // check if speed zero
                if (ad.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                return Task.FromResult(ad);
            }
            else if (GetSpeedStatus.NONE == getSpeedStatus)
            {
                ad.Speed = 0;
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return Task.FromResult(ad);
            }
            // else if (GetSpeedStatus.EXCEPTION == getSpeedStatus) {
            // we don't restart unles not responding for long time check cooldown logic in Miner
            //Helpers.ConsolePrint(MinerTAG(), "ethminer is not running.. restarting..");
            //IsRunning = false;
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            return Task.FromResult<ApiData>(null);
        }

        /// <summary>
        /// The _Start
        /// </summary>
        /// <returns>The <see cref="HashKingsProcess"/></returns>
        protected override HashKingsProcess _Start()
        {
            SetEthminerAPI(ApiPort);
            return base._Start();
        }

        /// <summary>
        /// The _Stop
        /// </summary>
        /// <param name="willswitch">The <see cref="MinerStopType"/></param>
        protected override void _Stop(MinerStopType willswitch)
        {
            // prevent logging non runing miner
            if (IsRunning && !IsPaused && willswitch == MinerStopType.SWITCH)
            {
                // daggerhashimoto - we only "pause" mining
                IsPaused = true;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Pausing ethminer..");
                StopMining();
                return;
            }
            else if ((IsRunning || IsPaused) && willswitch != MinerStopType.SWITCH)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Shutting down miner");
            }
            if ((willswitch == MinerStopType.FORCE_END || willswitch == MinerStopType.END) && ProcessHandle != null)
            {
                IsPaused = false; // shutting down means it is not paused
                try
                {
                    ProcessHandle.Kill();
                }
                catch
                {
                }
                finally
                {
                    ProcessHandle = null;
                }
            }
        }

        // benchmark stuff
        /// <summary>
        /// The BenchmarkParseLine
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool BenchmarkParseLine(string outdata)
        {
            if (outdata.Contains("min/mean/max:"))
            {
                string[] splt = outdata.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int index = Array.IndexOf(splt, "mean");
                double avg_spd = Convert.ToDouble(splt[index + 2]);
                Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + avg_spd + "H/s");

                BenchmarkAlgorithm.BenchmarkSpeed = (avg_spd) * (1.0 - DevFee * 0.01);
                return true;
            }

            return false;
        }

        /// <summary>
        /// The BenchmarkOutputErrorDataReceivedImpl
        /// </summary>
        /// <param name="outdata">The <see cref="string"/></param>
        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        /// <summary>
        /// Defines the GetSpeedStatus
        /// </summary>
        private enum GetSpeedStatus
        {
            /// <summary>
            /// Defines the NONE
            /// </summary>
            NONE,

            /// <summary>
            /// Defines the GOT
            /// </summary>
            GOT,

            /// <summary>
            /// Defines the EXCEPTION
            /// </summary>
            EXCEPTION
        }

        /// <summary>
        /// Initialize ethminer API instance.
        /// </summary>
        /// <param name="port">ethminer's API port.</param>
        private void SetEthminerAPI(int port)
        {
            m_port = port;
            m_client = new UdpClient("127.0.0.1", port);
        }

        /// <summary>
        /// Call this to start ethminer. If ethminer is already running, nothing happens.
        /// </summary>
        private void StartMining()
        {
            Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Sending START UDP");
            SendUDP(2);
            IsRunning = true;
        }

        /// <summary>
        /// Call this to stop ethminer. If ethminer is already stopped, nothing happens.
        /// </summary>
        private void StopMining()
        {
            Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Sending STOP UDP");
            SendUDP(1);
            IsRunning = false;
        }

        /// <summary>
        /// Call this to get current ethminer speed. This method may block up to 2 seconds.
        /// </summary>
        /// <param name="ismining">Set to true if ethminer is not mining (has been stopped).</param>
        /// <param name="speed">Current ethminer speed in MH/s.</param>
        /// <returns>False if ethminer is unreachable (crashed or unresponsive and needs restarting).</returns>
        private GetSpeedStatus GetSpeed(out bool ismining, out double speed)
        {
            speed = 0;
            ismining = false;

            SendUDP(3);

            DateTime start = DateTime.Now;

            while ((DateTime.Now - start) < TimeSpan.FromMilliseconds(2000))
            {
                if (m_client.Available > 0)
                {
                    // read
                    try
                    {
                        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), m_port);
                        byte[] data = m_client.Receive(ref ipep);
                        if (data.Length != 8) return GetSpeedStatus.NONE;
                        speed = BitConverter.ToDouble(data, 0);
                        if (speed >= 0) ismining = true;
                        else speed = 0;
                        return GetSpeedStatus.GOT;
                    }
                    catch
                    {
                        Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from API bind port");
                        return GetSpeedStatus.EXCEPTION;
                    }
                }
                else
                    System.Threading.Thread.Sleep(2);
            }

            return GetSpeedStatus.NONE;
        }

        /// <summary>
        /// Defines the m_port
        /// </summary>
        private int m_port;

        /// <summary>
        /// Defines the m_client
        /// </summary>
        private UdpClient m_client;

        /// <summary>
        /// The SendUDP
        /// </summary>
        /// <param name="code">The <see cref="int"/></param>
        private void SendUDP(int code)
        {
            byte[] data = new byte[1];
            data[0] = (byte)code;
            m_client.Send(data, data.Length);
        }
    }
}