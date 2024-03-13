namespace zPoolMiner.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using zPoolMiner.Configs;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Interfaces;
    using zPoolMiner.Miners;
    using zPoolMiner.Miners.Grouping;

    /// <summary>
    /// Defines the <see cref="Form_Benchmark" />
    /// </summary>
    public partial class Form_Benchmark : Form, IListItemCheckColorSetter, IBenchmarkComunicator, IBenchmarkCalculation
    {
        /// <summary>
        /// Defines the _inBenchmark
        /// </summary>
        private bool _inBenchmark = false;

        /// <summary>
        /// Defines the _bechmarkCurrentIndex
        /// </summary>
        private int _bechmarkCurrentIndex = 0;

        /// <summary>
        /// Defines the _bechmarkedSuccessCount
        /// </summary>
        private int _bechmarkedSuccessCount = 0;

        /// <summary>
        /// Defines the _benchmarkAlgorithmsCount
        /// </summary>
        private int _benchmarkAlgorithmsCount = 0;

        /// <summary>
        /// Defines the _algorithmOption
        /// </summary>
        private AlgorithmBenchmarkSettingsType _algorithmOption = AlgorithmBenchmarkSettingsType.SelectedUnbenchmarkedAlgorithms;

        /// <summary>
        /// Defines the _benchmarkMiners
        /// </summary>
        private List<Miner> _benchmarkMiners;

        /// <summary>
        /// Defines the _currentMiner
        /// </summary>
        private Miner _currentMiner;

        /// <summary>
        /// Defines the _benchmarkDevicesAlgorithmQueue
        /// </summary>
        private List<Tuple<ComputeDevice, Queue<Algorithm>>> _benchmarkDevicesAlgorithmQueue;

        /// <summary>
        /// Defines the ExitWhenFinished
        /// </summary>
        private bool ExitWhenFinished = false;

        //private AlgorithmType _singleBenchmarkType = AlgorithmType.NONE;
        //private AlgorithmType _singleBenchmarkType = AlgorithmType.NONE;        /// <summary>
        /// Defines the _benchmarkingTimer
        /// </summary>
        private Timer _benchmarkingTimer;

        /// <summary>
        /// Defines the dotCount
        /// </summary>
        private int dotCount = 0;

        /// <summary>
        /// Gets or sets a value indicating whether StartMining
        /// </summary>
        public bool StartMining { get; private set; }

        /// <summary>
        /// Defines the <see cref="DeviceAlgo" />
        /// </summary>
        private struct DeviceAlgo
        {
            /// <summary>
            /// Gets or sets the Device
            /// </summary>
            public string Device { get; set; }

            /// <summary>
            /// Gets or sets the Algorithm
            /// </summary>
            public string Algorithm { get; set; }
        }

        /// <summary>
        /// Defines the _benchmarkFailedAlgoPerDev
        /// </summary>
        private List<DeviceAlgo> _benchmarkFailedAlgoPerDev;

        /// <summary>
        /// Defines the BenchmarkSettingsStatus
        /// </summary>
        private enum BenchmarkSettingsStatus : int
        {
            /// <summary>
            /// Defines the NONE
            /// </summary>
            NONE = 0,

            /// <summary>
            /// Defines the TODO
            /// </summary>
            TODO,

            /// <summary>
            /// Defines the DISABLED_NONE
            /// </summary>
            DISABLED_NONE,

            /// <summary>
            /// Defines the DISABLED_TODO
            /// </summary>
            DISABLED_TODO
        }

        /// <summary>
        /// Defines the _benchmarkDevicesAlgorithmStatus
        /// </summary>
        private Dictionary<string, BenchmarkSettingsStatus> _benchmarkDevicesAlgorithmStatus;

        /// <summary>
        /// Defines the _currentDevice
        /// </summary>
        private ComputeDevice _currentDevice;

        /// <summary>
        /// Defines the _currentAlgorithm
        /// </summary>
        private Algorithm _currentAlgorithm;

        /// <summary>
        /// Defines the CurrentAlgoName
        /// </summary>
        private string CurrentAlgoName;

        // CPU benchmarking helpers
        /// <summary>
        /// Defines the <see cref="CPUBenchmarkStatus" />
        /// </summary>
        private class CPUBenchmarkStatus
        {
            /// <summary>
            /// Defines the <see cref="benchmark" />
            /// </summary>
            private class benchmark
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="benchmark"/> class.
                /// </summary>
                /// <param name="lt">The <see cref="int"/></param>
                /// <param name="bench">The <see cref="double"/></param>
                public benchmark(int lt, double bench)
                {
                    LessTreads = lt;
                    Benchmark = bench;
                }

                /// <summary>
                /// Defines the LessTreads
                /// </summary>
                public readonly int LessTreads;

                /// <summary>
                /// Defines the Benchmark
                /// </summary>
                public readonly double Benchmark;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CPUBenchmarkStatus"/> class.
            /// </summary>
            /// <param name="max_threads">The <see cref="int"/></param>
            public CPUBenchmarkStatus(int max_threads)
            {
                _max_threads = max_threads;
            }

            /// <summary>
            /// The HasTest
            /// </summary>
            /// <returns>The <see cref="bool"/></returns>
            public bool HasTest()
            {
                return _cur_less_threads < _max_threads;
            }

            /// <summary>
            /// The SetNextSpeed
            /// </summary>
            /// <param name="speed">The <see cref="double"/></param>
            public void SetNextSpeed(double speed)
            {
                if (HasTest())
                {
                    _benchmarks.Add(new benchmark(_cur_less_threads, speed));
                    ++_cur_less_threads;
                }
            }

            /// <summary>
            /// The FindFastest
            /// </summary>
            public void FindFastest()
            {
                _benchmarks.Sort((a, b) => -a.Benchmark.CompareTo(b.Benchmark));
            }

            /// <summary>
            /// The GetBestSpeed
            /// </summary>
            /// <returns>The <see cref="double"/></returns>
            public double GetBestSpeed()
            {
                return _benchmarks[0].Benchmark;
            }

            /// <summary>
            /// The GetLessThreads
            /// </summary>
            /// <returns>The <see cref="int"/></returns>
            public int GetLessThreads()
            {
                return _benchmarks[0].LessTreads;
            }

            /// <summary>
            /// Defines the _max_threads
            /// </summary>
            private readonly int _max_threads;

            /// <summary>
            /// Defines the _cur_less_threads
            /// </summary>
            private int _cur_less_threads = 0;

            /// <summary>
            /// Defines the _benchmarks
            /// </summary>
            private List<benchmark> _benchmarks = new List<benchmark>();

            /// <summary>
            /// Gets the LessTreads
            /// </summary>
            public int LessTreads
            {
                get { return _cur_less_threads; }
            }

            /// <summary>
            /// Defines the Time
            /// </summary>
            public int Time;
        }

        /// <summary>
        /// Defines the __CPUBenchmarkStatus
        /// </summary>
        private CPUBenchmarkStatus __CPUBenchmarkStatus = null;

        /// <summary>
        /// Defines the <see cref="ClaymoreZcashStatus" />
        /// </summary>
        private class ClaymoreZcashStatus
        {
            /// <summary>
            /// Defines the MAX_BENCH
            /// </summary>
            private const int MAX_BENCH = 2;

            /// <summary>
            /// Defines the ASM_MODES
            /// </summary>
            private readonly string[] ASM_MODES = new string[] { " -asm 1", " -asm 0" };

            /// <summary>
            /// Defines the speeds
            /// </summary>
            private double[] speeds = new double[] { 0.0d, 0.0d };

            /// <summary>
            /// Defines the CurIndex
            /// </summary>
            private int CurIndex = 0;

            /// <summary>
            /// Defines the originalExtraParams
            /// </summary>
            private readonly string originalExtraParams;

            /// <summary>
            /// Initializes a new instance of the <see cref="ClaymoreZcashStatus"/> class.
            /// </summary>
            /// <param name="oep">The <see cref="string"/></param>
            public ClaymoreZcashStatus(string oep)
            {
                originalExtraParams = oep;
            }

            /// <summary>
            /// The HasTest
            /// </summary>
            /// <returns>The <see cref="bool"/></returns>
            public bool HasTest()
            {
                return CurIndex < MAX_BENCH;
            }

            /// <summary>
            /// The SetSpeed
            /// </summary>
            /// <param name="speed">The <see cref="double"/></param>
            public void SetSpeed(double speed)
            {
                if (HasTest())
                {
                    speeds[CurIndex] = speed;
                }
            }

            /// <summary>
            /// The SetNext
            /// </summary>
            public void SetNext()
            {
                CurIndex += 1;
            }

            /// <summary>
            /// The GetTestExtraParams
            /// </summary>
            /// <returns>The <see cref="string"/></returns>
            public string GetTestExtraParams()
            {
                if (HasTest())
                {
                    return originalExtraParams + ASM_MODES[CurIndex];
                }
                return originalExtraParams;
            }

            /// <summary>
            /// The FastestIndex
            /// </summary>
            /// <returns>The <see cref="int"/></returns>
            private int FastestIndex()
            {
                int maxIndex = 0;
                double maxValue = speeds[maxIndex];
                for (int i = 1; i < speeds.Length; ++i)
                {
                    if (speeds[i] > maxValue)
                    {
                        maxIndex = i;
                        maxValue = speeds[i];
                    }
                }

                return 0;
            }

            /// <summary>
            /// The GetFastestExtraParams
            /// </summary>
            /// <returns>The <see cref="string"/></returns>
            public string GetFastestExtraParams()
            {
                return originalExtraParams + ASM_MODES[FastestIndex()];
            }

            /// <summary>
            /// The GetFastestTime
            /// </summary>
            /// <returns>The <see cref="double"/></returns>
            public double GetFastestTime()
            {
                return speeds[FastestIndex()];
            }

            /// <summary>
            /// Defines the Time
            /// </summary>
            public int Time = 180;
        }

        /// <summary>
        /// Defines the __ClaymoreZcashStatus
        /// </summary>
        private ClaymoreZcashStatus __ClaymoreZcashStatus = null;

        // CPU sweet spots
        // CPU sweet spots        /// <summary>
        /// Defines the CPUAlgos
        /// </summary>
        private List<AlgorithmType> CPUAlgos = new List<AlgorithmType>() {
            AlgorithmType.cryptonight
        };

        /// <summary>
        /// Defines the DISABLED_COLOR
        /// </summary>
        private static Color DISABLED_COLOR = Color.DarkGray;

        /// <summary>
        /// Defines the BENCHMARKED_COLOR
        /// </summary>
        private static Color BENCHMARKED_COLOR = Color.DarkGreen;

        /// <summary>
        /// Defines the UNBENCHMARKED_COLOR
        /// </summary>
        private static Color UNBENCHMARKED_COLOR = Color.DarkBlue;

        /// <summary>
        /// The LviSetColor
        /// </summary>
        /// <param name="lvi">The <see cref="ListViewItem"/></param>
        public void LviSetColor(ListViewItem lvi)
        {
            if (lvi.Tag is ComputeDevice CDevice && _benchmarkDevicesAlgorithmStatus != null)
            {
                var uuid = CDevice.UUID;
                if (!CDevice.Enabled)
                {
                    lvi.BackColor = DISABLED_COLOR;
                }
                else
                {
                    switch (_benchmarkDevicesAlgorithmStatus[uuid])
                    {
                        case BenchmarkSettingsStatus.TODO:
                        case BenchmarkSettingsStatus.DISABLED_TODO:
                            lvi.BackColor = UNBENCHMARKED_COLOR;
                            break;

                        case BenchmarkSettingsStatus.NONE:
                        case BenchmarkSettingsStatus.DISABLED_NONE:
                            lvi.BackColor = BENCHMARKED_COLOR;
                            break;
                    }
                }
                //// enable disable status, NOT needed
                //if (cdvo.IsEnabled && _benchmarkDevicesAlgorithmStatus[uuid] >= BenchmarkSettingsStatus.DISABLED_NONE) {
                //    _benchmarkDevicesAlgorithmStatus[uuid] -= 2;
                //} else if (!cdvo.IsEnabled && _benchmarkDevicesAlgorithmStatus[uuid] <= BenchmarkSettingsStatus.TODO) {
                //    _benchmarkDevicesAlgorithmStatus[uuid] += 2;
                //}
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Form_Benchmark"/> class.
        /// </summary>
        /// <param name="benchmarkPerformanceType">The <see cref="BenchmarkPerformanceType"/></param>
        /// <param name="autostart">The <see cref="bool"/></param>
        public Form_Benchmark(BenchmarkPerformanceType benchmarkPerformanceType = BenchmarkPerformanceType.Standard,
            bool autostart = false)
        {
            InitializeComponent();
            Icon = zPoolMiner.Properties.Resources.logo;

            StartMining = false;

            // clear prev pending statuses
            foreach (var dev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
            {
                foreach (var algo in dev.GetAlgorithmSettings())
                {
                    algo.ClearBenchmarkPendingFirst();
                }
            }

            benchmarkOptions1.SetPerformanceType(benchmarkPerformanceType);

            // benchmark only unique devices
            devicesListViewEnableControl1.SetIListItemCheckColorSetter(this);
            devicesListViewEnableControl1.SetComputeDevices(ComputeDeviceManager.Avaliable.AllAvaliableDevices);

            // use this to track miner benchmark statuses
            _benchmarkMiners = new List<Miner>();

            InitLocale();

            _benchmarkingTimer = new Timer();
            _benchmarkingTimer.Tick += BenchmarkingTimer_Tick;
            _benchmarkingTimer.Interval = 1000; // 1s

            //// name, UUID
            //Dictionary<string, string> benchNamesUUIDs = new Dictionary<string, string>();
            //// initialize benchmark settings for same cards to only copy settings
            //foreach (var cDev in ComputeDeviceManager.Avaliable.AllAvaliableDevices) {
            //    var plainDevName = cDev.Name;
            //    if (benchNamesUUIDs.ContainsKey(plainDevName)) {
            //        cDev.Enabled = false;
            //        cDev.BenchmarkCopyUUID = benchNamesUUIDs[plainDevName];
            //    } else if (cDev.Enabled == true) {
            //        benchNamesUUIDs.Add(plainDevName, cDev.UUID);
            //        //cDev.Enabled = true; // enable benchmark
            //        cDev.BenchmarkCopyUUID = null;
            //    }
            //}

            //groupBoxAlgorithmBenchmarkSettings.Enabled = _singleBenchmarkType == AlgorithmType.NONE;
            devicesListViewEnableControl1.Enabled = true;
            devicesListViewEnableControl1.SetDeviceSelectionChangedCallback(DevicesListView1_ItemSelectionChanged);

            devicesListViewEnableControl1.SetAlgorithmsListView(algorithmsListView1);
            devicesListViewEnableControl1.IsBenchmarkForm = true;
            devicesListViewEnableControl1.IsSettingsCopyEnabled = true;

            ResetBenchmarkProgressStatus();
            CalcBenchmarkDevicesAlgorithmQueue();
            devicesListViewEnableControl1.ResetListItemColors();

            // to update laclulation status
            devicesListViewEnableControl1.BenchmarkCalculation = this;
            algorithmsListView1.BenchmarkCalculation = this;

            // set first device selected {
            if (ComputeDeviceManager.Avaliable.AllAvaliableDevices.Count > 0)
            {
                var firstComputedevice = ComputeDeviceManager.Avaliable.AllAvaliableDevices[0];
                algorithmsListView1.SetAlgorithms(firstComputedevice, firstComputedevice.Enabled);
            }

            if (autostart)
            {
                ExitWhenFinished = true;
                StartStopBtn_Click(null, null);
            }
        }

        /// <summary>
        /// The CopyBenchmarks
        /// </summary>
        private void CopyBenchmarks()
        {
            Helpers.ConsolePrint("CopyBenchmarks", "Checking for benchmarks to copy");
            foreach (var cDev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
            {
                // check if copy
                if (!cDev.Enabled && cDev.BenchmarkCopyUUID != null)
                {
                    var copyCdevSettings = ComputeDeviceManager.Avaliable.GetDeviceWithUUID(cDev.BenchmarkCopyUUID);
                    if (copyCdevSettings != null)
                    {
                        Helpers.ConsolePrint("CopyBenchmarks", String.Format("Copy from {0} to {1}", cDev.UUID, cDev.BenchmarkCopyUUID));
                        cDev.CopyBenchmarkSettingsFrom(copyCdevSettings);
                    }
                }
            }
        }

        /// <summary>
        /// The BenchmarkingTimer_Tick
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void BenchmarkingTimer_Tick(object sender, EventArgs e)
        {
            if (_inBenchmark)
            {
                algorithmsListView1.SetSpeedStatus(_currentDevice, _currentAlgorithm, GetDotsWaitString());
            }
        }

        /// <summary>
        /// The GetDotsWaitString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        private string GetDotsWaitString()
        {
            ++dotCount;
            if (dotCount > 3) dotCount = 1;
            return new String('.', dotCount);
        }

        /// <summary>
        /// The InitLocale
        /// </summary>
        private void InitLocale()
        {
            Text = International.GetText("Form_Benchmark_title"); //International.GetText("SubmitResultDialog_title");
            //labelInstruction.Text = International.GetText("SubmitResultDialog_labelInstruction");
            StartStopBtn.Text = International.GetText("SubmitResultDialog_StartBtn");
            CloseBtn.Text = International.GetText("SubmitResultDialog_CloseBtn");

            // TODO fix locale for benchmark enabled label
            devicesListViewEnableControl1.InitLocale();
            benchmarkOptions1.InitLocale();
            algorithmsListView1.InitLocale();
            groupBoxBenchmarkProgress.Text = International.GetText("FormBenchmark_Benchmark_GroupBoxStatus");
            radioButton_SelectedUnbenchmarked.Text = International.GetText("FormBenchmark_Benchmark_All_Selected_Unbenchmarked");
            radioButton_RE_SelectedUnbenchmarked.Text = International.GetText("FormBenchmark_Benchmark_All_Selected_ReUnbenchmarked");
            checkBox_StartMiningAfterBenchmark.Text = International.GetText("Form_Benchmark_checkbox_StartMiningAfterBenchmark");
        }

        /// <summary>
        /// The StartStopBtn_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void StartStopBtn_Click(object sender, EventArgs e)
        {
            if (_inBenchmark)
            {
                StopButonClick();
                BenchmarkStoppedGUISettings();
            }
            else if (StartButonClick())
            {
                StartStopBtn.Text = International.GetText("Form_Benchmark_buttonStopBenchmark");
            }
        }

        /// <summary>
        /// The StopBenchmark
        /// </summary>
        public void StopBenchmark()
        {
            if (_inBenchmark)
            {
                StopButonClick();
                BenchmarkStoppedGUISettings();
            }
        }

        /// <summary>
        /// The BenchmarkStoppedGUISettings
        /// </summary>
        private void BenchmarkStoppedGUISettings()
        {
            StartStopBtn.Text = International.GetText("Form_Benchmark_buttonStartBenchmark");
            // clear benchmark pending status
            if (_currentAlgorithm != null) _currentAlgorithm.ClearBenchmarkPending();
            foreach (var deviceAlgosTuple in _benchmarkDevicesAlgorithmQueue)
            {
                foreach (var algo in deviceAlgosTuple.Item2)
                {
                    algo.ClearBenchmarkPending();
                }
            }
            ResetBenchmarkProgressStatus();
            CalcBenchmarkDevicesAlgorithmQueue();
            benchmarkOptions1.Enabled = true;

            algorithmsListView1.IsInBenchmark = false;
            devicesListViewEnableControl1.IsInBenchmark = false;
            if (_currentDevice != null)
            {
                algorithmsListView1.RepaintStatus(_currentDevice.Enabled, _currentDevice.UUID);
            }

            CloseBtn.Enabled = true;
        }

        // TODO add list for safety and kill all miners
        /// <summary>
        /// The StopButonClick
        /// </summary>
        private void StopButonClick()
        {
            _benchmarkingTimer.Stop();
            _inBenchmark = false;
            Helpers.ConsolePrint("FormBenchmark", "StopButonClick() benchmark routine stopped");
            //// copy benchmarked
            //CopyBenchmarks();
            if (_currentMiner != null)
            {
                _currentMiner.BenchmarkSignalQuit = true;
                _currentMiner.InvokeBenchmarkSignalQuit();
            }
            if (ExitWhenFinished)
            {
                Close();
            }
        }

        /// <summary>
        /// The StartButonClick
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool StartButonClick()
        {
            CalcBenchmarkDevicesAlgorithmQueue();
            // device selection check scope
            {
                bool noneSelected = true;
                foreach (var cDev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
                {
                    if (cDev.Enabled)
                    {
                        noneSelected = false;
                        break;
                    }
                }
                if (noneSelected)
                {
                    MessageBox.Show(International.GetText("FormBenchmark_No_Devices_Selected_Msg"),
                        International.GetText("FormBenchmark_No_Devices_Selected_Title"),
                        MessageBoxButtons.OK);
                    return false;
                }
            }
            // device todo benchmark check scope
            {
                bool nothingToBench = true;
                foreach (var statusKpv in _benchmarkDevicesAlgorithmStatus)
                {
                    if (statusKpv.Value == BenchmarkSettingsStatus.TODO)
                    {
                        nothingToBench = false;
                        break;
                    }
                }
                if (nothingToBench)
                {
                    MessageBox.Show(International.GetText("FormBenchmark_Nothing_to_Benchmark_Msg"),
                        International.GetText("FormBenchmark_Nothing_to_Benchmark_Title"),
                        MessageBoxButtons.OK);
                    return false;
                }
            }

            // current failed new list
            _benchmarkFailedAlgoPerDev = new List<DeviceAlgo>();
            // disable gui controls
            benchmarkOptions1.Enabled = false;
            CloseBtn.Enabled = false;
            algorithmsListView1.IsInBenchmark = true;
            devicesListViewEnableControl1.IsInBenchmark = true;
            // set benchmark pending status
            foreach (var deviceAlgosTuple in _benchmarkDevicesAlgorithmQueue)
            {
                foreach (var algo in deviceAlgosTuple.Item2)
                {
                    algo.SetBenchmarkPending();
                }
            }
            if (_currentDevice != null)
            {
                algorithmsListView1.RepaintStatus(_currentDevice.Enabled, _currentDevice.UUID);
            }

            StartBenchmark();

            return true;
        }

        /// <summary>
        /// The CalcBenchmarkDevicesAlgorithmQueue
        /// </summary>
        public void CalcBenchmarkDevicesAlgorithmQueue()
        {
            _benchmarkAlgorithmsCount = 0;
            _benchmarkDevicesAlgorithmStatus = new Dictionary<string, BenchmarkSettingsStatus>();
            _benchmarkDevicesAlgorithmQueue = new List<Tuple<ComputeDevice, Queue<Algorithm>>>();
            foreach (var cDev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
            {
                var algorithmQueue = new Queue<Algorithm>();
                foreach (var algo in cDev.GetAlgorithmSettings())
                {
                    if (ShoulBenchmark(algo))
                    {
                        algorithmQueue.Enqueue(algo);
                        algo.SetBenchmarkPendingNoMsg();
                    }
                    else
                    {
                        algo.ClearBenchmarkPending();
                    }
                }

                BenchmarkSettingsStatus status;
                if (cDev.Enabled)
                {
                    _benchmarkAlgorithmsCount += algorithmQueue.Count;
                    status = algorithmQueue.Count == 0 ? BenchmarkSettingsStatus.NONE : BenchmarkSettingsStatus.TODO;
                    _benchmarkDevicesAlgorithmQueue.Add(
                    new Tuple<ComputeDevice, Queue<Algorithm>>(cDev, algorithmQueue)
                    );
                }
                else
                {
                    status = algorithmQueue.Count == 0 ? BenchmarkSettingsStatus.DISABLED_NONE : BenchmarkSettingsStatus.DISABLED_TODO;
                }
                _benchmarkDevicesAlgorithmStatus[cDev.UUID] = status;
            }
            // GUI stuff
            progressBarBenchmarkSteps.Maximum = _benchmarkAlgorithmsCount;
            progressBarBenchmarkSteps.Value = 0;
            SetLabelBenchmarkSteps(0, _benchmarkAlgorithmsCount);
        }

        /// <summary>
        /// The ShoulBenchmark
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ShoulBenchmark(Algorithm algorithm)
        {
            bool isBenchmarked = algorithm.BenchmarkSpeed > 0 ? true : false;
            if (_algorithmOption == AlgorithmBenchmarkSettingsType.SelectedUnbenchmarkedAlgorithms
                && !isBenchmarked && algorithm.Enabled)
            {
                return true;
            }
            if (_algorithmOption == AlgorithmBenchmarkSettingsType.UnbenchmarkedAlgorithms && !isBenchmarked)
            {
                return true;
            }
            if (_algorithmOption == AlgorithmBenchmarkSettingsType.ReBecnhSelectedAlgorithms && algorithm.Enabled)
            {
                return true;
            }
            if (_algorithmOption == AlgorithmBenchmarkSettingsType.AllAlgorithms)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The StartBenchmark
        /// </summary>
        private void StartBenchmark()
        {
            _inBenchmark = true;
            _bechmarkCurrentIndex = -1;
            NextBenchmark();
        }

        /// <summary>
        /// The NextBenchmark
        /// </summary>
        private void NextBenchmark()
        {
            ConfigManager.CommitBenchmarks();
            if (_bechmarkCurrentIndex > -1)
            {
                StepUpBenchmarkStepProgress();
            }
            ++_bechmarkCurrentIndex;
            if (_bechmarkCurrentIndex >= _benchmarkAlgorithmsCount)
            {
                EndBenchmark();
                return;
            }

            Tuple<ComputeDevice, Queue<Algorithm>> currentDeviceAlgosTuple;
            Queue<Algorithm> algorithmBenchmarkQueue;
            while (_benchmarkDevicesAlgorithmQueue.Count > 0)
            {
                currentDeviceAlgosTuple = _benchmarkDevicesAlgorithmQueue[0];
                _currentDevice = currentDeviceAlgosTuple.Item1;
                algorithmBenchmarkQueue = currentDeviceAlgosTuple.Item2;
                if (algorithmBenchmarkQueue.Count != 0)
                {
                    _currentAlgorithm = algorithmBenchmarkQueue.Dequeue();
                    break;
                }
                else
                {
                    _benchmarkDevicesAlgorithmQueue.RemoveAt(0);
                }
            }

            if (_currentDevice != null && _currentAlgorithm != null)
            {
                _currentMiner = MinerFactory.CreateMiner(_currentDevice, _currentAlgorithm);
                if (_currentAlgorithm.MinerBaseType == MinerBaseType.cpuminer && _currentAlgorithm.CryptoMiner937ID == AlgorithmType.cryptonight && string.IsNullOrEmpty(_currentAlgorithm.ExtraLaunchParameters) && _currentAlgorithm.ExtraLaunchParameters.Contains("enable_ht=true") == false)
                {
                    __CPUBenchmarkStatus = new CPUBenchmarkStatus(Globals.ThreadsPerCPU);
                    _currentAlgorithm.LessThreads = __CPUBenchmarkStatus.LessTreads;
                }
                else
                {
                    __CPUBenchmarkStatus = null;
                }
                if (_currentAlgorithm.MinerBaseType == MinerBaseType.Claymore && _currentAlgorithm.CryptoMiner937ID == AlgorithmType.equihash && _currentAlgorithm.ExtraLaunchParameters != null && !_currentAlgorithm.ExtraLaunchParameters.Contains("-asm"))
                {
                    __ClaymoreZcashStatus = new ClaymoreZcashStatus(_currentAlgorithm.ExtraLaunchParameters);
                    _currentAlgorithm.ExtraLaunchParameters = __ClaymoreZcashStatus.GetTestExtraParams();
                }
                else
                {
                    __ClaymoreZcashStatus = null;
                }
            }

            if (_currentMiner != null && _currentAlgorithm != null)
            {
                _benchmarkMiners.Add(_currentMiner);
                CurrentAlgoName = AlgorithmCryptoMiner937Names.GetName(_currentAlgorithm.CryptoMiner937ID);
                _currentMiner.InitBenchmarkSetup(new MiningPair(_currentDevice, _currentAlgorithm));

                var time = ConfigManager.GeneralConfig.BenchmarkTimeLimits
                    .GetBenchamrktime(benchmarkOptions1.PerformanceType, _currentDevice.DeviceGroupType);
                //currentConfig.TimeLimit = time;
                if (__CPUBenchmarkStatus != null)
                {
                    __CPUBenchmarkStatus.Time = time;
                }
                if (__ClaymoreZcashStatus != null)
                {
                    __ClaymoreZcashStatus.Time = time;
                }

                // dagger about 4 minutes
                var showWaitTime = _currentAlgorithm.CryptoMiner937ID == AlgorithmType.DaggerHashimoto ? 4 * 60 : time;

                dotCount = 0;
                _benchmarkingTimer.Start();

                _currentMiner.BenchmarkStart(time, this);
                algorithmsListView1.SetSpeedStatus(_currentDevice, _currentAlgorithm,
                    GetDotsWaitString());
            }
            else
            {
                NextBenchmark();
            }
        }

        /// <summary>
        /// The EndBenchmark
        /// </summary>
        private void EndBenchmark()
        {
            _benchmarkingTimer.Stop();
            _inBenchmark = false;
            Helpers.ConsolePrint("FormBenchmark", "EndBenchmark() benchmark routine finished");

            //CopyBenchmarks();

            BenchmarkStoppedGUISettings();
            // check if all ok
            if (_benchmarkFailedAlgoPerDev.Count == 0 && StartMining == false)
            {
                MessageBox.Show(
                    International.GetText("FormBenchmark_Benchmark_Finish_Succes_MsgBox_Msg"),
                    International.GetText("FormBenchmark_Benchmark_Finish_MsgBox_Title"),
                    MessageBoxButtons.OK);
            }
            else if (StartMining == false)
            {
                var result = MessageBox.Show(
                    International.GetText("FormBenchmark_Benchmark_Finish_Fail_MsgBox_Msg"),
                    International.GetText("FormBenchmark_Benchmark_Finish_MsgBox_Title"),
                    MessageBoxButtons.RetryCancel);

                if (result == System.Windows.Forms.DialogResult.Retry)
                {
                    StartButonClick();
                    return;
                }
                else /*Cancel*/
                {
                    // get unbenchmarked from criteria and disable
                    CalcBenchmarkDevicesAlgorithmQueue();
                    foreach (var deviceAlgoQueue in _benchmarkDevicesAlgorithmQueue)
                    {
                        foreach (var algorithm in deviceAlgoQueue.Item2)
                        {
                            algorithm.Enabled = false;
                        }
                    }
                }
            }
            if (ExitWhenFinished || StartMining)
            {
                Close();
            }
        }

        /// <summary>
        /// The SetCurrentStatus
        /// </summary>
        /// <param name="status">The <see cref="string"/></param>
        public void SetCurrentStatus(string status)
        {
            Invoke((MethodInvoker)delegate
            {
                algorithmsListView1.SetSpeedStatus(_currentDevice, _currentAlgorithm, GetDotsWaitString());
            });
        }

        /// <summary>
        /// The OnBenchmarkComplete
        /// </summary>
        /// <param name="success">The <see cref="bool"/></param>
        /// <param name="status">The <see cref="string"/></param>
        public void OnBenchmarkComplete(bool success, string status)
        {
            if (!_inBenchmark) return;
            Invoke((MethodInvoker)delegate
            {
                _bechmarkedSuccessCount += success ? 1 : 0;
                bool rebenchSame = false;
                if (success && __CPUBenchmarkStatus != null && CPUAlgos.Contains(_currentAlgorithm.CryptoMiner937ID))
                {
                    __CPUBenchmarkStatus.SetNextSpeed(_currentAlgorithm.BenchmarkSpeed);
                    rebenchSame = __CPUBenchmarkStatus.HasTest();
                    _currentAlgorithm.LessThreads = __CPUBenchmarkStatus.LessTreads;
                    if (rebenchSame == false)
                    {
                        __CPUBenchmarkStatus.FindFastest();
                        _currentAlgorithm.BenchmarkSpeed = __CPUBenchmarkStatus.GetBestSpeed();
                        _currentAlgorithm.LessThreads = __CPUBenchmarkStatus.GetLessThreads();
                    }
                }

                if (__ClaymoreZcashStatus != null && _currentAlgorithm.MinerBaseType == MinerBaseType.Claymore && _currentAlgorithm.CryptoMiner937ID == AlgorithmType.equihash)
                {
                    if (__ClaymoreZcashStatus.HasTest())
                    {
                        _currentMiner = MinerFactory.CreateMiner(_currentDevice, _currentAlgorithm);
                        rebenchSame = true;
                        //System.Threading.Thread.Sleep(1000*60*5);
                        __ClaymoreZcashStatus.SetSpeed(_currentAlgorithm.BenchmarkSpeed);
                        __ClaymoreZcashStatus.SetNext();
                        _currentAlgorithm.ExtraLaunchParameters = __ClaymoreZcashStatus.GetTestExtraParams();
                        Helpers.ConsolePrint("ClaymoreAMD_equihash", _currentAlgorithm.ExtraLaunchParameters);
                        _currentMiner.InitBenchmarkSetup(new MiningPair(_currentDevice, _currentAlgorithm));
                    }

                    if (__ClaymoreZcashStatus.HasTest() == false)
                    {
                        rebenchSame = false;
                        // set fastest mode
                        _currentAlgorithm.BenchmarkSpeed = __ClaymoreZcashStatus.GetFastestTime();
                        _currentAlgorithm.ExtraLaunchParameters = __ClaymoreZcashStatus.GetFastestExtraParams();
                    }
                }

                if (!rebenchSame)
                {
                    _benchmarkingTimer.Stop();
                }

                if (!success && !rebenchSame)
                {
                    // add new failed list
                    _benchmarkFailedAlgoPerDev.Add(
                        new DeviceAlgo()
                        {
                            Device = _currentDevice.Name,
                            Algorithm = _currentAlgorithm.AlgorithmName
                        });
                    algorithmsListView1.SetSpeedStatus(_currentDevice, _currentAlgorithm, status);
                }
                else if (!rebenchSame)
                {
                    // set status to empty string it will return speed
                    _currentAlgorithm.ClearBenchmarkPending();
                    algorithmsListView1.SetSpeedStatus(_currentDevice, _currentAlgorithm, "");
                }
                if (rebenchSame)
                {
                    if (__CPUBenchmarkStatus != null)
                    {
                        _currentMiner.BenchmarkStart(__CPUBenchmarkStatus.Time, this);
                    }
                    else if (__ClaymoreZcashStatus != null)
                    {
                        _currentMiner.BenchmarkStart(__ClaymoreZcashStatus.Time, this);
                    }
                }
                else
                {
                    NextBenchmark();
                }
            });
        }

        /// <summary>
        /// The SetLabelBenchmarkSteps
        /// </summary>
        /// <param name="current">The <see cref="int"/></param>
        /// <param name="max">The <see cref="int"/></param>
        private void SetLabelBenchmarkSteps(int current, int max)
        {
            labelBenchmarkSteps.Text = String.Format(International.GetText("FormBenchmark_Benchmark_Step"), current, max);
        }

        /// <summary>
        /// The StepUpBenchmarkStepProgress
        /// </summary>
        private void StepUpBenchmarkStepProgress()
        {
            SetLabelBenchmarkSteps(_bechmarkCurrentIndex + 1, _benchmarkAlgorithmsCount);
            progressBarBenchmarkSteps.Value = _bechmarkCurrentIndex + 1;
        }

        /// <summary>
        /// The ResetBenchmarkProgressStatus
        /// </summary>
        private void ResetBenchmarkProgressStatus()
        {
            progressBarBenchmarkSteps.Value = 0;
        }

        /// <summary>
        /// The CloseBtn_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// The FormBenchmark_New_FormClosing
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/></param>
        private void FormBenchmark_New_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_inBenchmark)
            {
                e.Cancel = true;
                return;
            }

            // disable all pending benchmark
            foreach (var cDev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
            {
                foreach (var algorithm in cDev.GetAlgorithmSettings())
                {
                    algorithm.ClearBenchmarkPending();
                }
            }

            // save already benchmarked algorithms
            ConfigManager.CommitBenchmarks();
            // check devices without benchmarks
            foreach (var cdev in ComputeDeviceManager.Avaliable.AllAvaliableDevices)
            {
                if (cdev.Enabled)
                {
                    bool Enabled = false;
                    foreach (var algo in cdev.GetAlgorithmSettings())
                    {
                        if (algo.BenchmarkSpeed > 0)
                        {
                            Enabled = true;
                            break;
                        }
                    }
                    cdev.Enabled = Enabled;
                }
            }
        }

        /// <summary>
        /// The DevicesListView1_ItemSelectionChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="ListViewItemSelectionChangedEventArgs"/></param>
        private void DevicesListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //algorithmSettingsControl1.Deselect();
            // show algorithms
            var _selectedComputeDevice = ComputeDeviceManager.Avaliable.GetCurrentlySelectedComputeDevice(e.ItemIndex, true);
            algorithmsListView1.SetAlgorithms(_selectedComputeDevice, _selectedComputeDevice.Enabled);
        }

        /// <summary>
        /// The RadioButton_SelectedUnbenchmarked_CheckedChanged_1
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void RadioButton_SelectedUnbenchmarked_CheckedChanged_1(object sender, EventArgs e)
        {
            _algorithmOption = AlgorithmBenchmarkSettingsType.SelectedUnbenchmarkedAlgorithms;
            CalcBenchmarkDevicesAlgorithmQueue();
            devicesListViewEnableControl1.ResetListItemColors();
            algorithmsListView1.ResetListItemColors();
        }

        /// <summary>
        /// The RadioButton_RE_SelectedUnbenchmarked_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void RadioButton_RE_SelectedUnbenchmarked_CheckedChanged(object sender, EventArgs e)
        {
            _algorithmOption = AlgorithmBenchmarkSettingsType.ReBecnhSelectedAlgorithms;
            CalcBenchmarkDevicesAlgorithmQueue();
            devicesListViewEnableControl1.ResetListItemColors();
            algorithmsListView1.ResetListItemColors();
        }

        /// <summary>
        /// The CheckBox_StartMiningAfterBenchmark_CheckedChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void CheckBox_StartMiningAfterBenchmark_CheckedChanged(object sender, EventArgs e)
        {
            StartMining = checkBox_StartMiningAfterBenchmark.Checked;
        }

        /// <summary>
        /// The AlgorithmsListView1_Load
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void AlgorithmsListView1_Load(object sender, EventArgs e)
        {
        }
    }
}
