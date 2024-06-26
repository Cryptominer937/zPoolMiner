﻿namespace zPoolMiner
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows.Forms;
    using zPoolMiner.Configs;
    using zPoolMiner.Enums;
    using zPoolMiner.Interfaces;
    using zPoolMiner.Miners;
    using zPoolMiner.Miners.Grouping;
    using Timer = System.Timers.Timer;

    /// <summary>
    /// Defines the <see cref="ApiData" />
    /// </summary>
    public class ApiData
    {
        /// <summary>
        /// Defines the AlgorithmID
        /// </summary>
        public AlgorithmType AlgorithmID;

        /// <summary>
        /// Defines the SecondaryAlgorithmID
        /// </summary>
        public AlgorithmType SecondaryAlgorithmID;

        /// <summary>
        /// Defines the AlgorithmName
        /// </summary>
        public string AlgorithmName;

        /// <summary>
        /// Defines the Speed
        /// </summary>
        public double Speed;

        /// <summary>
        /// Defines the SecondarySpeed
        /// </summary>
        public double SecondarySpeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiData"/> class.
        /// </summary>
        /// <param name="algorithmID">The <see cref="AlgorithmType"/></param>
        /// <param name="secondaryAlgorithmID">The <see cref="AlgorithmType"/></param>
        public ApiData(AlgorithmType algorithmID, AlgorithmType secondaryAlgorithmID = AlgorithmType.NONE, MiningPair miningPair = null)
        {
            AlgorithmID = algorithmID;
            SecondaryAlgorithmID = secondaryAlgorithmID;
            AlgorithmName = AlgorithmCryptoMiner937Names.GetName(DualAlgorithmID());
            Speed = 0.0;
            SecondarySpeed = 0.0;
        }

        /// <summary>
        /// The DualAlgorithmID
        /// </summary>
        /// <returns>The <see cref="AlgorithmType"/></returns>
        public AlgorithmType DualAlgorithmID()
        {
            if (AlgorithmID == AlgorithmType.DaggerHashimoto)
            {
                switch (SecondaryAlgorithmID)
                {
                    case AlgorithmType.Decred:
                        return AlgorithmType.DaggerDecred;

                    case AlgorithmType.Lbry:
                        return AlgorithmType.DaggerLbry;

                    case AlgorithmType.Pascal:
                        return AlgorithmType.DaggerPascal;

                    case AlgorithmType.Sia:
                        return AlgorithmType.DaggerSia;

                    case AlgorithmType.Blake2s:
                        return AlgorithmType.DaggerBlake2s;
                }
            }

            return AlgorithmID;
        }
    }

    //
    /// <summary>
    /// Defines the <see cref="MinerPID_Data" />
    /// </summary>
    public class MinerPID_Data
    {
        /// <summary>
        /// Defines the minerBinPath
        /// </summary>
        public string minerBinPath;

        /// <summary>
        /// Defines the PID
        /// </summary>
        public int PID = -1;
    }

    public abstract class Miner
    {
        // MINER_ID_COUNT used to identify miners creation
        protected static long MINER_ID_COUNT { get; private set; }

        // Donation stats
        public static bool SHOULD_START_DONATING => DonationStart < DateTime.UtcNow;

        public static bool SHOULD_STOP_DONATING => DonationStart.Add(DonationTime) < DateTime.UtcNow;
        public static bool IS_DONATING { get; set; } = false;
        public static DateTime DonationStart = DateTime.UtcNow.AddHours(3);
        protected static TimeSpan DonationTime = TimeSpan.FromMinutes(24);
        public static TimeSpan DonateEvery = TimeSpan.FromHours(24);

        public NHMConectionType ConectionType { get; protected set; }

        // used to identify miner instance
        protected readonly long MINER_ID;

        private string _minetTag;
        public string MinerDeviceName { get; set; }
        protected int ApiPort { get; private set; }

        // if miner has no API bind port for reading curentlly only cryptonight on ccminer
        public bool IsApiReadException { get; protected set; }

        public bool IsNeverHideMiningWindow { get; protected set; }

        // mining algorithm stuff
        protected bool IsInit { get; private set; }

        protected MiningSetup MiningSetup { get; set; }

        // sgminer/zcash claymore workaround
        protected bool IsKillAllUsedMinerProcs { get; set; }

        public bool IsRunning { get; protected set; }
        protected string Path { get; private set; }
        protected string LastCommandLine { get; set; }

        // TODO check this
        protected double PreviousTotalMH;

        // the defaults will be
        protected string WorkingDirectory { get; private set; }

        protected string MinerExeName { get; private set; }
        protected HashKingsProcess ProcessHandle;
        private MinerPID_Data _currentPidData;
        private List<MinerPID_Data> _allPidData = new List<MinerPID_Data>();

        // Benchmark stuff
        public bool BenchmarkSignalQuit;

        public bool BenchmarkSignalHanged;
        private Stopwatch BenchmarkTimeOutStopWatch;
        public bool BenchmarkSignalTimedout;
        protected bool BenchmarkSignalFinnished;
        protected IBenchmarkComunicator BenchmarkComunicator;
        protected bool OnBenchmarkCompleteCalled;
        protected Algorithm BenchmarkAlgorithm { get; set; }
        public BenchmarkProcessStatus BenchmarkProcessStatus { get; protected set; }
        protected string BenchmarkProcessPath { get; set; }
        protected Process BenchmarkHandle { get; set; }
        protected Exception BenchmarkException;
        protected int BenchmarkTimeInSeconds;

        private string benchmarkLogPath = "";
        protected List<string> BenchLines;

        // TODO maybe set for individual miner cooldown/retries logic variables
        // this replaces MinerAPIGraceSeconds(AMD)
        private const int _MIN_CooldownTimeInMilliseconds = 5 * 1000; // 5 seconds

        // private const int _MIN_CooldownTimeInMilliseconds = 1000; // TESTING

        // private const int _MAX_CooldownTimeInMilliseconds = 60 * 1000; // 1 minute max, whole waiting time 75seconds
        private readonly int _MAX_CooldownTimeInMilliseconds; // = GET_MAX_CooldownTimeInMilliseconds();

        protected abstract int GetMaxCooldownTimeInMilliseconds();

        private Timer _cooldownCheckTimer;
        protected MinerApiReadStatus CurrentMinerReadStatus { get; set; }
        private int _currentCooldownTimeInSeconds = _MIN_CooldownTimeInMilliseconds;
        private int _currentCooldownTimeInSecondsLeft = _MIN_CooldownTimeInMilliseconds;
        private const int IS_COOLDOWN_CHECK_TIMER_ALIVE_CAP = 15;
        private bool NeedsRestart;

        private bool isEnded;

        public bool IsUpdatingAPI;

        protected const string HTTPHeaderDelimiter = "\r\n\r\n";

        public Miner(string minerDeviceName)
        {
            ConectionType = NHMConectionType.STRATUM_TCP;
            MiningSetup = new MiningSetup(null);
            IsInit = false;
            MINER_ID = MINER_ID_COUNT++;

            MinerDeviceName = minerDeviceName;

            WorkingDirectory = "";

            IsRunning = false;
            PreviousTotalMH = 0.0;

            LastCommandLine = "";

            IsApiReadException = false;
            // Only set minimize if hide is false (specific miners will override true after)
            IsNeverHideMiningWindow = ConfigManager.GeneralConfig.MinimizeMiningWindows && !ConfigManager.GeneralConfig.HideMiningWindows;
            IsKillAllUsedMinerProcs = false;
            _MAX_CooldownTimeInMilliseconds = GetMaxCooldownTimeInMilliseconds();
            //
            Helpers.ConsolePrint(MinerTag(), "NEW MINER CREATED");
        }

        ~Miner()
        {
            // free the port
            MinersApiPortsManager.RemovePort(ApiPort);
            Helpers.ConsolePrint(MinerTag(), "MINER DESTROYED");
        }

        protected void SetWorkingDirAndProgName(string fullPath)
        {
            WorkingDirectory = "";
            Path = fullPath;
            var lastIndex = fullPath.LastIndexOf("\\") + 1;

            if (lastIndex > 0)
            {
                WorkingDirectory = fullPath.Substring(0, lastIndex);
                MinerExeName = fullPath.Substring(lastIndex);
            }
        }

        private void SetAPIPort()
        {
            if (IsInit)
            {
                var minerBase = MiningSetup.MiningPairs[0].Algorithm.MinerBaseType;
                var algoType = MiningSetup.MiningPairs[0].Algorithm.CryptoMiner937ID;
                var path = MiningSetup.MinerPath;
                var reservedPorts = MinersSettingsManager.GetPortsListFor(minerBase, path, algoType);
                ApiPort = -1; // not set

                foreach (var reservedPort in reservedPorts)
                {
                    Thread.Sleep(1000);

                    if (MinersApiPortsManager.IsPortAvaliable(reservedPort))
                    {
                        ApiPort = reservedPort;
                        break;
                    }
                }

                if (ApiPort == -1)
                {
                    ApiPort = MinersApiPortsManager.GetAvaliablePort();
                }
            }
        }

        public virtual void InitMiningSetup(MiningSetup miningSetup)
        {
            MiningSetup = miningSetup;
            IsInit = MiningSetup.IsInit;
            SetAPIPort();
            SetWorkingDirAndProgName(MiningSetup.MinerPath);
        }

        public void InitBenchmarkSetup(MiningPair benchmarkPair)
        {
            InitMiningSetup(new MiningSetup(new List<MiningPair>() { benchmarkPair }));
            BenchmarkAlgorithm = benchmarkPair.Algorithm;
        }

        // TAG for identifying miner
        public string MinerTag()
        {
            if (_minetTag == null)
            {
                const string MASK = "{0}-MINER_ID({1})-DEVICE_IDs({2})";
                // no devices set
                if (!IsInit)
                {
                    return string.Format(MASK, MinerDeviceName, MINER_ID, "NOT_SET");
                }

                // contains ids
                var ids = new List<string>();
                foreach (var cdevs in MiningSetup.MiningPairs) ids.Add(cdevs.Device.ID.ToString());
                _minetTag = string.Format(MASK, MinerDeviceName, MINER_ID, string.Join(",", ids));
            }

            return _minetTag;
        }

        private string ProcessTag(MinerPID_Data pidData)
        {
            return string.Format("[pid({0})|bin({1})]", pidData.PID, pidData.minerBinPath);
        }

        public string ProcessTag()
        {
            if (_currentPidData == null)
            {
                return "PidData is NULL";
            }

            return ProcessTag(_currentPidData);
        }

        public void KillAllUsedMinerProcesses()
        {
            var toRemovePidData = new List<MinerPID_Data>();
            Helpers.ConsolePrint(MinerTag(), "Trying to kill all miner processes for this instance:");

            foreach (var PidData in _allPidData)
            {
                try
                {
                    var process = Process.GetProcessById(PidData.PID);

                    if (process != null && PidData.minerBinPath.Contains(process.ProcessName))
                    {
                        Helpers.ConsolePrint(MinerTag(), string.Format("Trying to kill {0}", ProcessTag(PidData)));

                        try
                        {
                            process.Kill();
                            process.Close();
                            process.WaitForExit(1000 * 60 * 1);
                        }
                        catch (Exception e)
                        {
                            Helpers.ConsolePrint(MinerTag(), string.Format("Exception killing {0}, exMsg {1}", ProcessTag(PidData), e.Message));
                        }
                    }
                }
                catch (Exception e)
                {
                    toRemovePidData.Add(PidData);
                    Helpers.ConsolePrint(MinerTag(), string.Format("Nothing to kill {0}, exMsg {1}", ProcessTag(PidData), e.Message));
                }
            }

            _allPidData.RemoveAll(x => toRemovePidData.Contains(x));
        }

        public abstract void Start(string url, string btcAddress, string worker);

        protected string GetUsername(string btcAddress, string worker)
        {
            Helpers.ConsolePrint("Miners MiningSession.DONATION_SESSION ", "" + MiningSession.DONATION_SESSION);

            if (MiningSession.DONATION_SESSION)
            {
                Helpers.ConsolePrint("Miner", Globals.DemoUser);
                return Globals.DemoUser;
            }

            if (worker.Length > 0)
            {
                Helpers.ConsolePrint("Miner worker length check", btcAddress);
                return btcAddress;
            }

            Helpers.ConsolePrint("Miner default", Globals.DemoUser);
            return btcAddress;
        }

        protected abstract void _Stop(MinerStopType willswitch);

        public virtual void Stop(MinerStopType willswitch = MinerStopType.SWITCH)
        {
            if (_cooldownCheckTimer != null) _cooldownCheckTimer.Stop();
            _Stop(willswitch);
            PreviousTotalMH = 0.0;
            IsRunning = false;
        }

        public void End()
        {
            isEnded = true;
            Stop(MinerStopType.FORCE_END);
        }

        protected void Stop_cpu_ccminer_sgminer_nheqminer(MinerStopType willswitch)
        {
            if (IsRunning)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Shutting down miner");
            }

            if (ProcessHandle != null)
            {
                try { ProcessHandle.Kill(); } catch { }
                // try { ProcessHandle.SendCtrlC((uint)Process.GetCurrentProcess().Id); } catch { }
                ProcessHandle.Close();
                ProcessHandle = null;

                // sgminer needs to be removed and kill by PID
                if (IsKillAllUsedMinerProcs) KillAllUsedMinerProcesses();
            }
        }

        protected void KillProspectorClaymoreMinerBase(string exeName)
        {
            foreach (Process process in Process.GetProcessesByName(exeName))
            {
                try { process.Kill(); } catch (Exception e) { Helpers.ConsolePrint(MinerDeviceName, e.ToString()); }
            }
        }

        protected virtual string GetDevicesCommandString()
        {
            var deviceStringCommand = " ";

            var ids = new List<string>();

            foreach (var mPair in MiningSetup.MiningPairs)
                ids.Add(mPair.Device.ID.ToString());

            deviceStringCommand += string.Join(",", ids);

            return deviceStringCommand;
        }

        public int BenchmarkTimeoutInSeconds(int timeInSeconds)
        {
            if (BenchmarkAlgorithm.CryptoMiner937ID == AlgorithmType.DaggerHashimoto)
            {
                return 5 * 60 + 120; // 5 minutes plus two minutes
            }

            if (BenchmarkAlgorithm.CryptoMiner937ID == AlgorithmType.cryptonight)
            {
                return 5 * 60 + 120; // 5 minutes plus two minutes
            }

            return timeInSeconds + 120; // wait time plus two minutes
        }

        // TODO remove algorithm
        protected abstract string BenchmarkCreateCommandLine(Algorithm algorithm, int time);

        // The benchmark config and algorithm must guarantee that they are compatible with miner
        // we guarantee algorithm is supported
        // we will not have empty benchmark configs, all benchmark configs will have device list
        public virtual void BenchmarkStart(int time, IBenchmarkComunicator benchmarkComunicator)
        {
            BenchmarkComunicator = benchmarkComunicator;
            BenchmarkTimeInSeconds = time;
            BenchmarkSignalFinnished = true;
            // check and kill
            BenchmarkHandle = null;
            OnBenchmarkCompleteCalled = false;
            BenchmarkTimeOutStopWatch = null;

            try
            {
                if (!Directory.Exists("logs"))
                {
                    Directory.CreateDirectory("logs");
                }
            }
            catch { }

            BenchLines = new List<string>();
            benchmarkLogPath = string.Format("{0}Log_{1}_{2}", Logger._logPath, MiningSetup.MiningPairs[0].Device.UUID, MiningSetup.MiningPairs[0].Algorithm.AlgorithmStringID);

            var CommandLine = BenchmarkCreateCommandLine(BenchmarkAlgorithm, time);

            var BenchmarkThread = new Thread(BenchmarkThreadRoutine);
            BenchmarkThread.Start(CommandLine);
        }

        protected virtual Process BenchmarkStartProcess(string CommandLine)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Helpers.ConsolePrint(MinerTag(), "Starting benchmark: " + CommandLine);

            var BenchmarkHandle = new Process();

            BenchmarkHandle.StartInfo.FileName = MiningSetup.MinerPath;

            // sgminer quickfix
            if (this is Sgminer | this is Glg)
            {
                BenchmarkProcessPath = "cmd / " + BenchmarkHandle.StartInfo.FileName;
                BenchmarkHandle.StartInfo.FileName = "cmd";
            }
            else
            {
                BenchmarkProcessPath = BenchmarkHandle.StartInfo.FileName;
                Helpers.ConsolePrint(MinerTag(), "Using miner: " + BenchmarkHandle.StartInfo.FileName);
                BenchmarkHandle.StartInfo.WorkingDirectory = WorkingDirectory;
            }

            // set sys variables
            if (MinersSettingsManager.MinerSystemVariables.ContainsKey(Path))
            {
                foreach (var kvp in MinersSettingsManager.MinerSystemVariables[Path])
                {
                    var envName = kvp.Key;
                    var envValue = kvp.Value;
                    BenchmarkHandle.StartInfo.EnvironmentVariables[envName] = envValue;
                }
            }

            BenchmarkHandle.StartInfo.Arguments = (string)CommandLine;
            BenchmarkHandle.StartInfo.UseShellExecute = false;
            BenchmarkHandle.StartInfo.RedirectStandardError = true;
            BenchmarkHandle.StartInfo.RedirectStandardOutput = true;
            BenchmarkHandle.StartInfo.CreateNoWindow = true;
            BenchmarkHandle.OutputDataReceived += BenchmarkOutputErrorDataReceived;
            BenchmarkHandle.ErrorDataReceived += BenchmarkOutputErrorDataReceived;
            BenchmarkHandle.Exited += BenchmarkHandle_Exited;

            if (!BenchmarkHandle.Start()) return null;

            _currentPidData = new MinerPID_Data
            {
                minerBinPath = BenchmarkHandle.StartInfo.FileName,
                PID = BenchmarkHandle.Id
            };

            _allPidData.Add(_currentPidData);

            return BenchmarkHandle;
        }

        private void BenchmarkHandle_Exited(object sender, EventArgs e) => BenchmarkSignalFinnished = true;

        private void BenchmarkOutputErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (BenchmarkTimeOutStopWatch == null)
            {
                BenchmarkTimeOutStopWatch = new Stopwatch();
                BenchmarkTimeOutStopWatch.Start();
            }
            else if (BenchmarkTimeOutStopWatch.Elapsed.TotalSeconds > BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds))
            {
                BenchmarkTimeOutStopWatch.Stop();
                BenchmarkSignalTimedout = true;
            }

            var outdata = e.Data;

            if (e.Data != null)
            {
                BenchmarkOutputErrorDataReceivedImpl(outdata);
            }

            // terminate process situations
            if (BenchmarkSignalQuit
                || BenchmarkSignalFinnished
                || BenchmarkSignalHanged
                || BenchmarkSignalTimedout
                || BenchmarkException != null)
            {
                EndBenchmarkProcces();
            }
        }

        protected abstract void BenchmarkOutputErrorDataReceivedImpl(string outdata);

        protected void CheckOutdata(string outdata)
        {
            // Helpers.ConsolePrint("BENCHMARK" + benchmarkLogPath, outdata);
            BenchLines.Add(outdata);
            // ccminer, cpuminer
            if (outdata.Contains("Cuda error"))
                BenchmarkException = new Exception("CUDA error");

            if (outdata.Contains("is not supported"))
                BenchmarkException = new Exception("N/A");

            if (outdata.Contains("illegal memory access"))
                BenchmarkException = new Exception("CUDA error");

            if (outdata.Contains("unknown error"))
                BenchmarkException = new Exception("Unknown error");

            if (outdata.Contains("No servers could be used! Exiting."))
                BenchmarkException = new Exception("No pools or work can be used for benchmarking");
            // if (outdata.Contains("error") || outdata.Contains("Error"))
            //    BenchmarkException = new Exception("Unknown error #2");
            // Ethminer
            if (outdata.Contains("No GPU device with sufficient memory was found"))
                BenchmarkException = new Exception("[daggerhashimoto] No GPU device with sufficient memory was found.");

            // lastly parse data
            if (BenchmarkParseLine(outdata))
            {
                BenchmarkSignalFinnished = true;
            }
        }

        public void InvokeBenchmarkSignalQuit() => KillAllUsedMinerProcesses();

        protected double BenchmarkParseLine_cpu_ccminer_extra(string outdata)
        {
            // parse line
            if (outdata.Contains("Benchmark: ") && outdata.Contains("/s"))
            {
                var i = outdata.IndexOf("Benchmark:");
                var k = outdata.IndexOf("/s");
                var hashspeed = outdata.Substring(i + 11, k - i - 9);
                Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + hashspeed);

                // save speed
                var b = hashspeed.IndexOf(" ");

                if (b < 0)
                {
                    for (int _i = hashspeed.Length - 1; _i >= 0; --_i)
                    {
                        if (int.TryParse(hashspeed[_i].ToString(), out int stub))
                        {
                            b = _i;
                            break;
                        }
                    }
                }

                if (b >= 0)
                {
                    var speedStr = hashspeed.Substring(0, b);
                    var spd = Helpers.ParseDouble(speedStr);

                    if (hashspeed.Contains("kH/s")) spd *= 1000;
                    else if (hashspeed.Contains("MH/s")) spd *= 1000000;
                    else if (hashspeed.Contains("GH/s")) spd *= 1000000000;

                    return spd;
                }
            }
            else if (outdata.Contains("Benchmark: ") && outdata.Contains("/m"))
            {
                var i = outdata.IndexOf("Benchmark:");
                var k = outdata.IndexOf("/m");
                var hashspeed = outdata.Substring(i + 11, k - i - 9);
                Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + (hashspeed));

                // save speed
                var b = hashspeed.IndexOf(" ");

                if (b < 0)
                {
                    for (int _i = hashspeed.Length - 1; _i >= 0; --_i)
                    {
                        if (int.TryParse(hashspeed[_i].ToString(), out int stub))
                        {
                            b = _i;
                            break;
                        }
                    }
                }

                if (b >= 0)
                {
                    var speedStr = hashspeed.Substring(0, b);
                    var spd = Helpers.ParseDouble(speedStr);

                    if (hashspeed.Contains("kH/s")) spd *= 1000;
                    else if (hashspeed.Contains("MH/s")) spd *= 1000000;
                    else if (hashspeed.Contains("GH/s")) spd *= 1000000000;
                    else if (hashspeed.Contains("H/m")) spd /= 60;

                    return spd;
                }
            }

            return 0.0d;
        }

        // add hsrminer palgin
        protected double BenchmarkParseLine_cpu_Palgin_Neoscrypt_extra(string outdata)
        {
            // parse line
            if (outdata.Contains("Benchmark: ") && outdata.Contains("/s"))
            {
                var i = outdata.IndexOf("Benchmark:");
                var k = outdata.IndexOf("/s");
                var hashspeed = outdata.Substring(i + 11, k - i - 9);
                Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + hashspeed);

                // save speed
                var b = hashspeed.IndexOf(" ");

                if (b < 0)
                {
                    for (int _i = hashspeed.Length - 1; _i >= 0; --_i)
                    {
                        if (int.TryParse(hashspeed[_i].ToString(), out int stub))
                        {
                            b = _i;
                            break;
                        }
                    }
                }

                if (b >= 0)
                {
                    var speedStr = hashspeed.Substring(0, b);
                    var spd = Helpers.ParseDouble(speedStr);

                    if (hashspeed.Contains("kH/s")) spd *= 1000;
                    else if (hashspeed.Contains("MH/s")) spd *= 1000000;
                    else if (hashspeed.Contains("GH/s")) spd *= 1000000000;

                    return spd;
                }
            }

            return 0.0d;
        }

        /*// add mkxminer palgin
        protected double BenchmarkParseLine_cpu_mkxminer_extra(string outdata)
        {
            // parse line
            if (outdata.Contains(">") && outdata.Contains("/s"))
            {
                int i = outdata.IndexOf("> ");
                int k = outdata.IndexOf("/s");
                string hashspeed = outdata.Substring(i + 8, k - i - 6);
                Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + hashspeed);

                // save speed
                int b = hashspeed.IndexOf(" ");
                if (b < 0)
                {
                    for (int _i = hashspeed.Length - 1; _i >= 0; --_i)
                    {
                        if (Int32.TryParse(hashspeed[_i].ToString(), out int stub))
                        {
                            b = _i;
                            break;
                        }
                    }
                }
                if (b >= 0)
                {
                    string speedStr = hashspeed.Substring(0, b);
                    double spd = Helpers.ParseDouble(speedStr);
                    if (hashspeed.Contains("kH/s"))
                        spd *= 1000;
                    else if (hashspeed.Contains("MH/s"))
                        spd *= 1000000;
                    else if (hashspeed.Contains("GH/s"))
                        spd *= 1000000000;

                    return spd;
                }
            }
            return 0.0d;
        }*/

        // killing proccesses can take time
        public virtual void EndBenchmarkProcces()
        {
            if (BenchmarkHandle != null && BenchmarkProcessStatus != BenchmarkProcessStatus.Killing && BenchmarkProcessStatus != BenchmarkProcessStatus.DoneKilling)
            {
                BenchmarkProcessStatus = BenchmarkProcessStatus.Killing;

                try
                {
                    Helpers.ConsolePrint("BENCHMARK", string.Format("Trying to kill benchmark process {0} algorithm {1}", BenchmarkProcessPath, BenchmarkAlgorithm.AlgorithmName));
                    BenchmarkHandle.Kill();
                    BenchmarkHandle.Close();
                    KillAllUsedMinerProcesses();
                }
                catch { }
                finally
                {
                    BenchmarkProcessStatus = BenchmarkProcessStatus.DoneKilling;
                    Helpers.ConsolePrint("BENCHMARK", string.Format("Benchmark process {0} algorithm {1} KILLED", BenchmarkProcessPath, BenchmarkAlgorithm.AlgorithmName));
                    // BenchmarkHandle = null;
                }
            }
        }

        protected virtual void BenchmarkThreadRoutineStartSettup()
        {
            BenchmarkHandle.BeginErrorReadLine();
            BenchmarkHandle.BeginOutputReadLine();
        }

        protected void BenchmarkThreadRoutineCatch(Exception ex)
        {
            BenchmarkAlgorithm.BenchmarkSpeed = 0;

            Helpers.ConsolePrint(MinerTag(), "Benchmark Exception: " + ex.Message);

            if (BenchmarkComunicator != null && !OnBenchmarkCompleteCalled)
            {
                OnBenchmarkCompleteCalled = true;
                BenchmarkComunicator.OnBenchmarkComplete(false, GetFinalBenchmarkString());
            }
        }

        protected virtual string GetFinalBenchmarkString()
        {
            return BenchmarkSignalTimedout ? International.GetText("Benchmark_Timedout") : International.GetText("Benchmark_Terminated");
        }

        protected void BenchmarkThreadRoutineFinish()
        {
            var status = BenchmarkProcessStatus.Finished;

            if (BenchmarkAlgorithm.BenchmarkSpeed > 0)
            {
                status = BenchmarkProcessStatus.Success;
            }

            using (StreamWriter sw = File.AppendText(benchmarkLogPath))
            {
                foreach (var line in BenchLines)
                    sw.WriteLine(line);
            }

            BenchmarkProcessStatus = status;
            Helpers.ConsolePrint("BENCHMARK", "Final Speed: " + Helpers.FormatDualSpeedOutput(BenchmarkAlgorithm.CryptoMiner937ID, BenchmarkAlgorithm.BenchmarkSpeed, BenchmarkAlgorithm.SecondaryBenchmarkSpeed));
            Helpers.ConsolePrint("BENCHMARK", "Benchmark ends");

            if (BenchmarkComunicator != null && !OnBenchmarkCompleteCalled)
            {
                OnBenchmarkCompleteCalled = true;
                var isOK = BenchmarkProcessStatus.Success == status;
                var msg = GetFinalBenchmarkString();
                BenchmarkComunicator.OnBenchmarkComplete(isOK, isOK ? "" : msg);
            }
        }

        protected virtual void BenchmarkThreadRoutine(object CommandLine)
        {
            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;

            try
            {
                Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
                BenchmarkHandle = BenchmarkStartProcess((string)CommandLine);

                BenchmarkThreadRoutineStartSettup();
                // wait a little longer then the benchmark routine if exit false throw
                // var timeoutTime = BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds);
                // var exitSucces = BenchmarkHandle.WaitForExit(timeoutTime * 1000);
                // don't use wait for it breaks everything
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
                BenchmarkHandle.WaitForExit();

                if (BenchmarkSignalTimedout)
                {
                    throw new Exception("Benchmark timedout");
                }

                if (BenchmarkException != null)
                {
                    throw BenchmarkException;
                }

                if (BenchmarkSignalQuit)
                {
                    throw new Exception("Termined by user request");
                }

                if (BenchmarkSignalHanged)
                {
                    throw new Exception("SGMiner is not responding");
                }

                if (BenchmarkSignalFinnished)
                {
                    // break;
                }
            }
            catch (Exception ex)
            {
                BenchmarkThreadRoutineCatch(ex);
            }
            finally
            {
                BenchmarkThreadRoutineFinish();
            }
        }

        /// <summary>
        /// Thread routine for miners that cannot be scheduled to stop and need speed data read from command line
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="benchmarkTimeWait"></param>
        protected void BenchmarkThreadRoutineAlternate(object commandLine, int benchmarkTimeWait)
        {
            CleanAllOldLogs();

            Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);

            BenchmarkSignalQuit = false;
            BenchmarkSignalHanged = false;
            BenchmarkSignalFinnished = false;
            BenchmarkException = null;

            try
            {
                Helpers.ConsolePrint("BENCHMARK", "Benchmark starts");
                Helpers.ConsolePrint(MinerTag(), "Benchmark should end in : " + benchmarkTimeWait + " seconds");
                BenchmarkHandle = BenchmarkStartProcess((string)commandLine);
                BenchmarkHandle.WaitForExit(benchmarkTimeWait + 2);
                var _benchmarkTimer = new Stopwatch();
                _benchmarkTimer.Reset();
                _benchmarkTimer.Start();
                // BenchmarkThreadRoutineStartSettup();
                // wait a little longer then the benchmark routine if exit false throw
                // var timeoutTime = BenchmarkTimeoutInSeconds(BenchmarkTimeInSeconds);
                // var exitSucces = BenchmarkHandle.WaitForExit(timeoutTime * 1000);
                // don't use wait for it breaks everything
                BenchmarkProcessStatus = BenchmarkProcessStatus.Running;
                var keepRunning = true;

                while (keepRunning && IsActiveProcess(BenchmarkHandle.Id))
                {
                    // string outdata = BenchmarkHandle.StandardOutput.ReadLine();
                    // BenchmarkOutputErrorDataReceivedImpl(outdata);
                    // terminate process situations
                    if (_benchmarkTimer.Elapsed.TotalSeconds >= (benchmarkTimeWait + 2)
                        || BenchmarkSignalQuit
                        || BenchmarkSignalFinnished
                        || BenchmarkSignalHanged
                        || BenchmarkSignalTimedout
                        || BenchmarkException != null)
                    {
                        var imageName = MinerExeName.Replace(".exe", "");
                        // maybe will have to KILL process
                        KillProspectorClaymoreMinerBase(imageName);

                        if (BenchmarkSignalTimedout)
                        {
                            throw new Exception("Benchmark timedout");
                        }

                        if (BenchmarkException != null)
                        {
                            throw BenchmarkException;
                        }

                        if (BenchmarkSignalQuit)
                        {
                            throw new Exception("Termined by user request");
                        }

                        if (BenchmarkSignalFinnished) break;
                        keepRunning = false;
                        break;
                    }
                    else
                    {
                        // wait a second reduce CPU load
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                BenchmarkThreadRoutineCatch(ex);
            }
            finally
            {
                BenchmarkAlgorithm.BenchmarkSpeed = 0;
                // find latest log file
                var latestLogFile = "";
                var dirInfo = new DirectoryInfo(WorkingDirectory);

                foreach (var file in dirInfo.GetFiles("*_log.txt"))
                {
                    latestLogFile = file.Name;
                    break;
                }

                BenchmarkHandle.WaitForExit(10000);
                // read file log
                if (File.Exists(WorkingDirectory + latestLogFile))
                {
                    var lines = File.ReadAllLines(WorkingDirectory + latestLogFile);
                    var addBenchLines = BenchLines.Count == 0;
                    ProcessBenchLinesAlternate(lines);
                }

                BenchmarkThreadRoutineFinish();
            }
        }

        protected void CleanAllOldLogs()
        {
            // clean old logs
            try
            {
                var dirInfo = new DirectoryInfo(WorkingDirectory);
                var deleteContains = "_log.txt";

                if (dirInfo != null && dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        if (file.Name.Contains(deleteContains))
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch { }
        }

        protected virtual void ProcessBenchLinesAlternate(string[] lines)
        {
        }

        protected abstract bool BenchmarkParseLine(string outdata);

        protected bool IsActiveProcess(int pid)
        {
            try
            {
                return Process.GetProcessById(pid) != null;
            }
            catch
            {
                return false;
            }
        }

        protected virtual HashKingsProcess _Start()
        {
            // never start when ended
            if (isEnded)
            {
                return null;
            }

            PreviousTotalMH = 0.0;
            if (LastCommandLine.Length == 0) return null;

            var P = new HashKingsProcess();

            if (WorkingDirectory.Length > 1)
            {
                P.StartInfo.WorkingDirectory = WorkingDirectory;
            }

            if (MinersSettingsManager.MinerSystemVariables.ContainsKey(Path))
            {
                foreach (var kvp in MinersSettingsManager.MinerSystemVariables[Path])
                {
                    var envName = kvp.Key;
                    var envValue = kvp.Value;
                    P.StartInfo.EnvironmentVariables[envName] = envValue;
                }
            }

            P.StartInfo.FileName = Path;
            P.ExitEvent = Miner_Exited;

            P.StartInfo.Arguments = LastCommandLine;

            if (IsNeverHideMiningWindow)
            {
                P.StartInfo.CreateNoWindow = false;

                if (ConfigManager.GeneralConfig.HideMiningWindows || ConfigManager.GeneralConfig.MinimizeMiningWindows)
                {
                    P.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    P.StartInfo.UseShellExecute = true;
                }
            }
            else
            {
                P.StartInfo.CreateNoWindow = ConfigManager.GeneralConfig.HideMiningWindows;
            }

            P.StartInfo.UseShellExecute = false;

            try
            {
                if (P.Start())
                {
                    IsRunning = true;

                    _currentPidData = new MinerPID_Data
                    {
                        minerBinPath = P.StartInfo.FileName,
                        PID = P.Id
                    };

                    _allPidData.Add(_currentPidData);

                    Helpers.ConsolePrint(MinerTag(), "Starting miner " + ProcessTag() + " " + LastCommandLine);
                    // add hsrminer palgin
                    // StartCoolDownTimerChecker();
                    /*if (!ProcessTag().Contains("hsrminer_neoscrypt")) //temporary disable hsrminer checker
                    {
                        StartCoolDownTimerChecker();
                    }*/
                    /* if (!ProcessTag().Contains("mkxminer_lyra2rev2")) //temporary disable mkxminer checker
                     {
                         StartCoolDownTimerChecker();
                     }*/

                    return P;
                }
                else
                {
                    Helpers.ConsolePrint(MinerTag(), "NOT STARTED " + ProcessTag() + " " + LastCommandLine);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " _Start: " + ex.Message);
                return null;
            }
        }

        protected void StartCoolDownTimerChecker()
        {
            if (ConfigManager.GeneralConfig.CoolDownCheckEnabled)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Starting cooldown checker");
                if (_cooldownCheckTimer != null && _cooldownCheckTimer.Enabled) _cooldownCheckTimer.Stop();
                // cool down init
                _cooldownCheckTimer = new Timer()
                {
                    Interval = _MIN_CooldownTimeInMilliseconds
                };

                _cooldownCheckTimer.Elapsed += MinerCoolingCheck_Tick;
                _cooldownCheckTimer.Start();
                _currentCooldownTimeInSeconds = _MIN_CooldownTimeInMilliseconds;
                _currentCooldownTimeInSecondsLeft = _currentCooldownTimeInSeconds;
            }
            else
            {
                Helpers.ConsolePrint(MinerTag(), "Cooldown checker disabled");
            }

            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
        }

        protected virtual void Miner_Exited() => ScheduleRestart(5000);

        protected void ScheduleRestart(int ms)
        {
            var RestartInMS = ConfigManager.GeneralConfig.MinerRestartDelayMS > ms ?
                ConfigManager.GeneralConfig.MinerRestartDelayMS : ms;

            Helpers.ConsolePrint(MinerTag(), ProcessTag() + string.Format(" Miner_Exited Will restart in {0} ms", RestartInMS));

            if (ConfigManager.GeneralConfig.CoolDownCheckEnabled)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                NeedsRestart = true;
                _currentCooldownTimeInSecondsLeft = RestartInMS;
            }
            else
            {  // directly restart since cooldown checker not running
                Thread.Sleep(RestartInMS);
                Restart();
            }
        }

        protected void Restart()
        {
            if (!isEnded)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Restarting miner..");
                Stop(MinerStopType.END); // stop miner first
                System.Threading.Thread.Sleep(ConfigManager.GeneralConfig.MinerRestartDelayMS);
                ProcessHandle = _Start(); // start with old command line
            }
        }

        protected virtual bool IsApiEof(byte third, byte second, byte last) => false;

        protected async Task<string> GetAPIDataAsync(int port, string DataToSend, bool exitHack = false, bool overrideLoop = false)
        {
            string ResponseFromServer = null;

            try
            {
                var tcpc = new TcpClient("127.0.0.1", port);
                var nwStream = tcpc.GetStream();

                var BytesToSend = ASCIIEncoding.ASCII.GetBytes(DataToSend);
                await nwStream.WriteAsync(BytesToSend, 0, BytesToSend.Length);

                var IncomingBuffer = new byte[tcpc.ReceiveBufferSize];
                var prevOffset = -1;
                var offset = 0;
                var fin = false;

                while (!fin && tcpc.Client.Connected)
                {
                    var r = await nwStream.ReadAsync(IncomingBuffer, offset, tcpc.ReceiveBufferSize - offset);

                    for (int i = offset; i < offset + r; i++)
                    {
                        if (IncomingBuffer[i] == 0x7C || IncomingBuffer[i] == 0x00
                            || (i > 2 && IsApiEof(IncomingBuffer[i - 2], IncomingBuffer[i - 1], IncomingBuffer[i]))
                            || overrideLoop)
                        {
                            fin = true;
                            break;
                        }
                        // Not working
                        // if (IncomingBuffer[i] == 0x5d || IncomingBuffer[i] == 0x5e) {
                        //    fin = true;
                        //    break;
                        // }
                    }

                    offset += r;

                    if (exitHack)
                    {
                        if (prevOffset == offset)
                        {
                            fin = true;
                            break;
                        }

                        prevOffset = offset;
                    }
                }

                tcpc.Close();

                if (offset > 0)
                    ResponseFromServer = ASCIIEncoding.ASCII.GetString(IncomingBuffer);
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " GetAPIData reason: " + ex.Message);
                return null;
            }

            return ResponseFromServer;
        }

        public abstract Task<ApiData> GetSummaryAsync();

        protected async Task<ApiData> GetSummaryCPUAsync(string method = "", bool overrideLoop = false)
        {
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            try
            {
                CurrentMinerReadStatus = MinerApiReadStatus.WAIT;
                var dataToSend = GetHttpRequestNHMAgentStrin(method);
                var respStr = await GetAPIDataAsync(ApiPort, dataToSend);

                if (string.IsNullOrEmpty(respStr))
                {
                    CurrentMinerReadStatus = MinerApiReadStatus.NETWORK_EXCEPTION;
                    throw new Exception("Response is empty!");
                }

                if (respStr.IndexOf("HTTP/1.1 200 OK") > -1)
                {
                    respStr = respStr.Substring(respStr.IndexOf(HTTPHeaderDelimiter) + HTTPHeaderDelimiter.Length);
                }
                else
                {
                    throw new Exception("Response not HTTP formed! " + respStr);
                }

                dynamic resp = JsonConvert.DeserializeObject(respStr);

                if (resp != null)
                {
                    JArray totals = resp.hashrate.total;

                    foreach (var total in totals)
                    {
                        if (total.Value<string>() == null) continue;
                        ad.Speed = total.Value<double>();
                        break;
                    }

                    if (ad.Speed == 0)
                    {
                        CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                    }
                    else
                    {
                        CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                    }
                }
                else
                {
                    throw new Exception($"Response does not contain speed data: {respStr.Trim()}");
                }
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(MinerTag(), ex.Message);
            }

            return ad;
        }

        // add hsrminer palgin
        protected async Task<ApiData> GetSummaryCPU_Palgin_NeoscryptAsync()
        {
            string resp;
            // TODO aname
            string aname = null;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            var DataToSend = GetHttpRequestNHMAgentStrin("summary");

            resp = await GetAPIDataAsync(ApiPort, DataToSend);

            if (resp == null)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " summary is null");
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            try
            {
                var resps = resp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < resps.Length; i++)
                {
                    var optval = resps[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (optval.Length != 2) continue;

                    if (optval[0] == "ALGO")
                        aname = optval[1];
                    else if (optval[0] == "KHS")
                        ad.Speed = double.Parse(optval[1], CultureInfo.InvariantCulture) * 1000; // HPS
                }
            }
            catch
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from API bind port");
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            // check if speed zero
            if (ad.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;

            return ad;
        }

        // add mkxminer
        protected async Task<ApiData> GetSummaryCPU_mkxminerAsync()
        {
            string resp;
            // TODO aname
            string aname = null;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            var DataToSend = GetHttpRequestNHMAgentStrin("summary");

            resp = await GetAPIDataAsync(ApiPort, DataToSend);

            if (resp == null)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " summary is null");
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            try
            {
                var resps = resp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < resps.Length; i++)
                {
                    var optval = resps[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (optval.Length != 2) continue;

                    if (optval[0] == "ALGO")
                        aname = optval[1];
                    else if (optval[0] == "KHS")
                        ad.Speed = double.Parse(optval[1], CultureInfo.InvariantCulture) * 1000; // HPS
                }
            }
            catch
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from API bind port");
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            // check if speed zero
            if (ad.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;

            return ad;
        }

        protected string GetHttpRequestNHMAgentStrin(string cmd)
        {
            return "GET /" + cmd + " HTTP/1.1\r\n" +
                    "Host: 127.0.0.1\r\n" +
                    "User-Agent: zPoolMiner/" + Application.ProductVersion + "\r\n" +
                    "\r\n";
        }

        protected async Task<ApiData> GetSummaryCPU_CCMINERAsync()
        {
            string resp;
            // TODO aname
            string aname = null;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            var DataToSend = GetHttpRequestNHMAgentStrin("summary");

            resp = await GetAPIDataAsync(ApiPort, DataToSend);

            if (resp == null)
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " summary is null");
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            try
            {
                var resps = resp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < resps.Length; i++)
                {
                    var optval = resps[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (optval.Length != 2) continue;

                    if (optval[0] == "ALGO")
                        aname = optval[1];
                    else if (optval[0] == "KHS")
                        ad.Speed = double.Parse(optval[1], CultureInfo.InvariantCulture) * 1000; // HPS
                }
            }
            catch
            {
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " Could not read data from API bind port");
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
            // check if speed zero
            if (ad.Speed == 0) CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;

            return ad;
        }

        /// <summary>
        /// decrement time for half current half time, if less then min ammend
        /// </summary>
        private void CoolDown()
        {
            if (_currentCooldownTimeInSeconds > _MIN_CooldownTimeInMilliseconds)
            {
                _currentCooldownTimeInSeconds = _MIN_CooldownTimeInMilliseconds;
                // Helpers.ConsolePrint(MinerTAG(), String.Format("{0} Reseting cool time = {1} ms", ProcessTag(), _MIN_CooldownTimeInMilliseconds.ToString()));
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            }
        }

        /// <summary>
        /// increment time for half current half time, if more then max set restart
        /// </summary>
        private void CoolUp()
        {
            _currentCooldownTimeInSeconds *= 2;
            // Helpers.ConsolePrint(MinerTAG(), String.Format("{0} Cooling UP, cool time is {1} ms", ProcessTag(), _currentCooldownTimeInSeconds.ToString()));
            if (_currentCooldownTimeInSeconds > _MAX_CooldownTimeInMilliseconds)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.RESTART;
                Helpers.ConsolePrint(MinerTag(), ProcessTag() + " MAX cool time exceeded. RESTARTING");
                Restart();
            }
        }

        private void MinerCoolingCheck_Tick(object sender, ElapsedEventArgs e)
        {
            if (isEnded)
            {
                End();
                return;
            }

            _currentCooldownTimeInSecondsLeft -= (int)_cooldownCheckTimer.Interval;
            // if times up
            if (_currentCooldownTimeInSecondsLeft <= 0)
            {
                if (NeedsRestart)
                {
                    NeedsRestart = false;
                    Restart();
                }
                else if (CurrentMinerReadStatus == MinerApiReadStatus.GOT_READ)
                {
                    CoolDown();
                }
                else if (CurrentMinerReadStatus == MinerApiReadStatus.READ_SPEED_ZERO)
                {
                    // Helpers.ConsolePrint(MinerTAG(), ProcessTag() + " READ SPEED ZERO, will cool up");
                    CoolUp();
                }
                else if (CurrentMinerReadStatus == MinerApiReadStatus.RESTART)
                {
                    Restart();
                }
                else
                {
                    CoolUp();
                }

                // set new times left from the CoolUp/Down change
                _currentCooldownTimeInSecondsLeft = _currentCooldownTimeInSeconds;
            }
        }
    }
}