using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Interfaces;
using zPoolMiner.Miners.Grouping;
using Timer = System.Timers.Timer;

namespace zPoolMiner.Miners
{
    using GroupedDevices = SortedSet<string>;

    public class MiningSession
    {
        // Donation stats
#if DEBUG
        protected static TimeSpan DonationTime = TimeSpan.FromMinutes(1); //debug only
        public static TimeSpan DonateEvery = TimeSpan.FromMinutes(1); // debug only

#else
        protected static TimeSpan DonationTime = TimeSpan.FromMinutes(30);
        public static TimeSpan DonateEvery = TimeSpan.FromHours(24);
#endif
        public static DateTime DonationStart = DateTime.UtcNow.Add(DonateEvery);
        public static bool SHOULD_START_DONATING => DateTime.UtcNow > DonationStart;
        public static bool SHOULD_STOP_DONATING => DateTime.UtcNow > DonationStart.Add(DonationTime);
        public static bool IS_DONATING { get; set; } = false;
        public static bool DONATION_SESSION { get; set; } = false;

        private const string TAG = "MiningSession";
        private const string DOUBLE_FORMAT = "F12";

        // session variables fixed
        private string _miningLocation = "";

        private string _btcAddress = "";
        private string _worker = "";
        private List<MiningDevice> _miningDevices = new List<MiningDevice>();
        private IMainFormRatesComunication _mainFormRatesComunication;

        // session variables changing
        // GroupDevices hash code doesn't work correctly use string instead
        //Dictionary<GroupedDevices, GroupMiners> _groupedDevicesMiners;
        private Dictionary<string, GroupMiner> _runningGroupMiners = new Dictionary<string, GroupMiner>();

        private GroupMiner _ethminerNVIDIAPaused = null;
        private GroupMiner _ethminerAMDPaused = null;

        private bool IsProfitable = true;

        private bool IsConnectedToInternet = true;
        private bool IsMiningRegardlesOfProfit = true;

        // timers
        private Timer _preventSleepTimer;

        // check Internet connection
        private Timer _internetCheckTimer;

        public bool IsMiningEnabled
        {
            get
            {
                // if this is not empty it means we can mine
                return _miningDevices.Count > 0;
            }
        }

        private bool IsCurrentlyIdle
        {
            get
            {
                return !IsMiningEnabled || !IsConnectedToInternet || !IsProfitable;
            }
        }

        public List<int> ActiveDeviceIndexes
        {
            get
            {
                var minerIDs = new List<int>();
                if (!IsCurrentlyIdle)
                {
                    foreach (var miner in _runningGroupMiners.Values)
                    {
                        minerIDs.AddRange(miner.DevIndexes);
                    }
                }
                return minerIDs;
            }
        }

        public MiningSession(List<ComputeDevice> devices,
            IMainFormRatesComunication mainFormRatesComunication,
            string miningLocation, string worker, string btcAddress)
        {
            // init fixed
            _mainFormRatesComunication = mainFormRatesComunication;
            _miningLocation = miningLocation;

            _btcAddress = btcAddress;
            _worker = worker;

            // initial setup
            _miningDevices = GroupSetupUtils.GetMiningDevices(devices, true);
            if (_miningDevices.Count > 0)
            {
                GroupSetupUtils.AvarageSpeeds(_miningDevices);
            }

            // init timer stuff
            _preventSleepTimer = new Timer();
            _preventSleepTimer.Elapsed += PreventSleepTimer_Tick;
            // sleep time is minimal 1 minute
            _preventSleepTimer.Interval = 20 * 1000; // leave this interval, it works

            // set Internet checking
            _internetCheckTimer = new Timer();
            _internetCheckTimer.Elapsed += InternetCheckTimer_Tick;
            _internetCheckTimer.Interval = 1 * 1000 * 60; // every minute

            // assume profitable
            IsProfitable = true;
            // assume we have INTERNET
            IsConnectedToInternet = true;

            if (IsMiningEnabled)
            {
                _preventSleepTimer.Start();
                _internetCheckTimer.Start();
            }

            IsMiningRegardlesOfProfit = ConfigManager.GeneralConfig.MinimumProfit == 0;
        }

        #region Timers stuff

        private void InternetCheckTimer_Tick(object sender, EventArgs e)
        {
            if (ConfigManager.GeneralConfig.IdleWhenNoInternetAccess)
            {
                IsConnectedToInternet = Helpers.IsConnectedToInternet();
            }
        }

        private void PreventSleepTimer_Tick(object sender, ElapsedEventArgs e)
        {
            // when mining keep system awake, prevent sleep
            Helpers.PreventSleep();
        }

        #endregion Timers stuff

        #region Start/Stop

        public void StopAllMiners()
        {
            if (_runningGroupMiners != null)
            {
                foreach (var groupMiner in _runningGroupMiners.Values)
                {
                    groupMiner.End();
                }
                _runningGroupMiners = new Dictionary<string, GroupMiner>();
            }
            if (_ethminerNVIDIAPaused != null)
            {
                _ethminerNVIDIAPaused.End();
                _ethminerNVIDIAPaused = null;
            }
            if (_ethminerAMDPaused != null)
            {
                _ethminerAMDPaused.End();
                _ethminerAMDPaused = null;
            }
            if (_mainFormRatesComunication != null)
            {
                _mainFormRatesComunication.ClearRatesALL();
            }

            // restore/enable sleep
            _preventSleepTimer.Stop();
            _internetCheckTimer.Stop();
            Helpers.AllowMonitorPowerdownAndSleep();
        }

        public void StopAllMinersNonProfitable()
        {
            if (_runningGroupMiners != null)
            {
                foreach (var groupMiner in _runningGroupMiners.Values)
                {
                    groupMiner.End();
                }
                _runningGroupMiners = new Dictionary<string, GroupMiner>();
            }
            if (_ethminerNVIDIAPaused != null)
            {
                _ethminerNVIDIAPaused.End();
                _ethminerNVIDIAPaused = null;
            }
            if (_ethminerAMDPaused != null)
            {
                _ethminerAMDPaused.End();
                _ethminerAMDPaused = null;
            }
            if (_mainFormRatesComunication != null)
            {
                _mainFormRatesComunication.ClearRates(-1);
            }
        }

        #endregion Start/Stop

        private string CalcGroupedDevicesKey(GroupedDevices group)
        {
            return String.Join(", ", group);
        }

        public string GetActiveMinersGroup()
        {
            if (IsCurrentlyIdle)
            {
                return "IDLE";
            }

            string ActiveMinersGroup = "";

            //get unique miner groups like CPU, NVIDIA, AMD,...
            HashSet<string> UniqueMinerGroups = new HashSet<string>();
            foreach (var miningDevice in _miningDevices)
            {
                //if (miningDevice.MostProfitableKey != AlgorithmType.NONE) {
                UniqueMinerGroups.Add(GroupNames.GetNameGeneral(miningDevice.Device.DeviceType));
                //}
            }
            if (UniqueMinerGroups.Count > 0 && IsProfitable)
            {
                ActiveMinersGroup = String.Join("/", UniqueMinerGroups);
            }

            return ActiveMinersGroup;
        }

        public double GetTotalRate()
        {
            double TotalRate = 0;

            if (_runningGroupMiners != null)
            {
                foreach (var groupMiner in _runningGroupMiners.Values)
                {
                    TotalRate += groupMiner.CurrentRate;
                }
            }

            return TotalRate;
        }

        // full of state
        private bool CheckIfProfitable(double CurrentProfit, bool log = true)
        {
            // TODO FOR NOW USD ONLY
            var currentProfitUSD = (CurrentProfit * Globals.BitcoinUSDRate);
            IsProfitable =
                IsMiningRegardlesOfProfit
                || !IsMiningRegardlesOfProfit && currentProfitUSD >= ConfigManager.GeneralConfig.MinimumProfit;
            if (log)
            {
                Helpers.ConsolePrint(TAG, "Current Global profit: " + currentProfitUSD.ToString("F8") + " USD/Day");
                if (!IsProfitable)
                {
                    Helpers.ConsolePrint(TAG, "Current Global profit: NOT PROFITABLE MinProfit " + ConfigManager.GeneralConfig.MinimumProfit.ToString("F8") + " USD/Day");
                }
                else
                {
                    string profitabilityInfo = IsMiningRegardlesOfProfit ? "mine always regardless of profit" : ConfigManager.GeneralConfig.MinimumProfit.ToString("F8") + " USD/Day";
                    Helpers.ConsolePrint(TAG, "Current Global profit: IS PROFITABLE MinProfit " + profitabilityInfo);
                }
            }
            return IsProfitable;
        }

        private bool CheckIfShouldMine(double CurrentProfit, bool log = false)
        {
            // if profitable and connected to Internet mine
            bool shouldMine = CheckIfProfitable(CurrentProfit, log) && IsConnectedToInternet;
            if (shouldMine)
            {
                _mainFormRatesComunication.HideNotProfitable();
            }
            else
            {
                if (!IsConnectedToInternet)
                {
                    // change msg
                    if (log) Helpers.ConsolePrint(TAG, "NO INTERNET!!! Stopping mining.");
                    _mainFormRatesComunication.ShowNotProfitable(International.GetText("Form_Main_MINING_NO_INTERNET_CONNECTION"));
                }
                else
                {
                    _mainFormRatesComunication.ShowNotProfitable(International.GetText("Form_Main_MINING_NOT_PROFITABLE"));
                }
                // return don't group
                StopAllMinersNonProfitable();
            }
            return shouldMine;
        }

        public async Task SwitchMostProfitableGroupUpMethod(Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data, bool log = false)
        {
#if (SWITCH_TESTING)
            MiningDevice.SetNextTest();
#endif
            List<MiningPair> profitableDevices = new List<MiningPair>();
            double CurrentProfit = 0.0d;
            double PrevStateProfit = 0.0d;
            foreach (var device in _miningDevices)
            {
                // calculate profits
                device.CalculateProfits(CryptoMiner937Data);
                // check if device has profitable algo
                if (device.HasProfitableAlgo())
                {
                    profitableDevices.Add(device.GetMostProfitablePair());
                    CurrentProfit += device.GetCurrentMostProfitValue;
                    PrevStateProfit += device.GetPrevMostProfitValue;
                }
            }

            // print profit statuses
            if (log)
            {
                StringBuilder stringBuilderFull = new StringBuilder();
                stringBuilderFull.AppendLine("Current device profits:");
                foreach (var device in _miningDevices)
                {
                    StringBuilder stringBuilderDevice = new StringBuilder();
                    stringBuilderDevice.AppendLine(String.Format("\tProfits for {0} ({1}):", device.Device.UUID, device.Device.GetFullName()));
                    foreach (var algo in device.Algorithms)
                    {
                        stringBuilderDevice.AppendLine(String.Format("\t\tPROFIT = {0}\t(SPEED = {1}\t\t| NHSMA = {2})\t[{3}]",
                             algo.CurrentProfit.ToString(DOUBLE_FORMAT), // Profit
                             algo.AvaragedSpeed + (algo.IsDual() ? "/" + algo.SecondaryAveragedSpeed : ""), // Speed
                             algo.CurNhmSMADataVal + (algo.IsDual() ? "/" + algo.SecondaryCurNhmSMADataVal : ""), // CryptoMiner937Data
                             algo.AlgorithmStringID // Name
                         ));
                    }
                    // most profitable
                    stringBuilderDevice.AppendLine(String.Format("\t\tMOST PROFITABLE ALGO: {0}, PROFIT: {1}",
                        device.GetMostProfitableString(),
                        device.GetCurrentMostProfitValue.ToString(DOUBLE_FORMAT)));
                    stringBuilderFull.AppendLine(stringBuilderDevice.ToString());
                }
                Helpers.ConsolePrint(TAG, stringBuilderFull.ToString());
            }

            // check if should mine
            // Only check if profitable inside this method when getting SMA data, cheching during mining is not reliable
            if (CheckIfShouldMine(CurrentProfit, log) == false)
            {
                foreach (var device in _miningDevices)
                {
                    device.SetNotMining();
                }
                return;
            }

            // check profit threshold
            Helpers.ConsolePrint(TAG, String.Format("PrevStateProfit {0}, CurrentProfit {1}", PrevStateProfit, CurrentProfit));
            if (PrevStateProfit > 0 && CurrentProfit > 0)
            {
                double a = Math.Max(PrevStateProfit, CurrentProfit);
                double b = Math.Min(PrevStateProfit, CurrentProfit);
                //double percDiff = Math.Abs((PrevStateProfit / CurrentProfit) - 1);
                double percDiff = ((a - b)) / b;
                if (percDiff < ConfigManager.GeneralConfig.SwitchProfitabilityThreshold)
                {
                    // don't switch
                    //Helpers.ConsolePrint(TAG, String.Format("Will NOT switch profit diff is {0}, current threshold {1}", percDiff, ConfigManager.GeneralConfig.SwitchProfitabilityThreshold));
                    // RESTORE OLD PROFITS STATE
                    foreach (var device in _miningDevices)
                    {
                        device.RestoreOldProfitsState();
                    }
                    if (!SHOULD_START_DONATING)
                        return;
                }
                else
                {
                    Helpers.ConsolePrint(TAG, String.Format("Will SWITCH profit diff is {0}, current threshold {1}", percDiff, ConfigManager.GeneralConfig.SwitchProfitabilityThreshold));
                }
            }

            Helpers.ConsolePrint("Monitoring", "If enabled here I would submit Monitoring data to the server");

            if (ConfigManager.GeneralConfig.monitoring == true)
            { //Run monitoring command here
                var monversion = "Hash-Kings Miner V" + Application.ProductVersion;
                var monstatus = "Running";
                var monrunningminers = ""/*Runningminers go here*/;
                var monserver = ConfigManager.GeneralConfig.MonServerurl + "/api/report.php";
                var request = (HttpWebRequest)WebRequest.Create(monserver);
                var postData = "";
                /* fetch last command line for each miner
                Do this for each mining group*/
                foreach (var cdev in ComputeDeviceManager.Available.AllAvaliableDevices)
                {
                    if (cdev.Enabled)
                    {
                        postData += "Name" + Uri.EscapeDataString("Miner Name");
                        postData += "Path" + Uri.EscapeDataString("miner Path goes here");
                        postData += "Type" + Uri.EscapeDataString("Type of card EX. AMD");
                        postData += "Algorithm" + Uri.EscapeDataString("Current mining Algorithm");
                        postData += "Pool" + Uri.EscapeDataString("Pool Goes Here");
                        postData += "CurrentSpeed" + Uri.EscapeDataString("Actual hashrate goes here");
                        postData += "EstimatedSpeed" + Uri.EscapeDataString("Benchmark hashrate Goes Here");
                        postData += "Profit" + Uri.EscapeDataString("group profitability goes here");
                    };
                }
                /*

            Convert above data to Json
            Fetch Profit to Variable
            $Profit = [string]([Math]::Round(($data | Measure-Object Profit -Sum).Sum, 8))
            Send the request
            $Body = @{user = $Config.MonitoringUser; worker = $Config.WorkerName; version = $Version; status = $Status; profit = $Profit; data = $DataJSON}
        Try {
            $Response = Invoke-RestMethod -Uri "$($Config.MonitoringServer)/api/report.php" -Method Post -Body $Body -UseBasicParsing -TimeoutSec 10 -ErrorAction Stop
            Helpers.ConsolePrint("Monitoring", "Reporting status to server..." + $Server responce here"
        }
        Catch {
            Helpers.ConsolePrint("Monitoring", "Unable to send status to " monserver}*/
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }

            // group new miners
            Dictionary<string, List<MiningPair>> newGroupedMiningPairs = new Dictionary<string, List<MiningPair>>();
            // group devices with same supported algorithms
            {
                var currentGroupedDevices = new List<GroupedDevices>();
                for (int first = 0; first < profitableDevices.Count; ++first)
                {
                    var firstDev = profitableDevices[first].Device;
                    // check if is in group
                    bool isInGroup = false;
                    foreach (var groupedDevices in currentGroupedDevices)
                    {
                        if (groupedDevices.Contains(firstDev.UUID))
                        {
                            isInGroup = true;
                            break;
                        }
                    }
                    // if device is not in any group create new group and check if other device should group
                    if (isInGroup == false)
                    {
                        var newGroup = new GroupedDevices();
                        var miningPairs = new List<MiningPair>() { profitableDevices[first] };
                        newGroup.Add(firstDev.UUID);
                        for (int second = first + 1; second < profitableDevices.Count; ++second)
                        {
                            // check if we should group
                            var firstPair = profitableDevices[first];
                            var secondPair = profitableDevices[second];
                            if (GroupingLogic.ShouldGroup(firstPair, secondPair))
                            {
                                var secondDev = profitableDevices[second].Device;
                                newGroup.Add(secondDev.UUID);
                                miningPairs.Add(profitableDevices[second]);
                            }
                        }
                        currentGroupedDevices.Add(newGroup);
                        newGroupedMiningPairs[CalcGroupedDevicesKey(newGroup)] = miningPairs;
                    }
                }
            }
            //bool IsMinerStatsCheckUpdate = false;
            {
                // check which groupMiners should be stopped and which ones should be started and which to keep running
                Dictionary<string, GroupMiner> toStopGroupMiners = new Dictionary<string, GroupMiner>();
                Dictionary<string, GroupMiner> toRunNewGroupMiners = new Dictionary<string, GroupMiner>();
                Dictionary<string, GroupMiner> noChangeGroupMiners = new Dictionary<string, GroupMiner>();
                if (MiningSession.SHOULD_START_DONATING)
                {
                    MiningSession.IS_DONATING = true;
                }

                // check what to stop/update
                int count = _runningGroupMiners.Keys.Count;
                int it = 0;
                //  + " Mining For : " +(MiningSession.DONATION_SESSION ? "Developer" : "User")
                foreach (string runningGroupKey in _runningGroupMiners.Keys)
                {
                    it++;
                    if (newGroupedMiningPairs.ContainsKey(runningGroupKey) == false)
                    {
                        // runningGroupKey not in new group definately needs to be stopped and removed from curently running
                        toStopGroupMiners[runningGroupKey] = _runningGroupMiners[runningGroupKey];
                    }
                    // If we need to start donating, stop everything
                    else if (MiningSession.IS_DONATING && !MiningSession.DONATION_SESSION)
                    {
                        if (it == count)
                        {
                            MiningSession.DONATION_SESSION = true;
                        }
                        toStopGroupMiners[runningGroupKey] = _runningGroupMiners[runningGroupKey];

                        Helpers.ConsolePrint(TAG, "STARTING - DEV_FEE - Mining Dev-Fee for 24 Minutes.");
                        var miningPairs = newGroupedMiningPairs[runningGroupKey];
                        var newAlgoType = GetMinerPairAlgorithmType(miningPairs);
                        GroupMiner newGroupMiner = null;
                        if (newAlgoType == AlgorithmType.DaggerHashimoto)
                        {
                            if (_ethminerNVIDIAPaused != null && _ethminerNVIDIAPaused.Key == runningGroupKey)
                            {
                                newGroupMiner = _ethminerNVIDIAPaused;
                            }
                            if (_ethminerAMDPaused != null && _ethminerAMDPaused.Key == runningGroupKey)
                            {
                                newGroupMiner = _ethminerAMDPaused;
                            }
                        }
                        if (newGroupMiner == null)
                        {
                            newGroupMiner = new GroupMiner(miningPairs, runningGroupKey);
                        }
                        toRunNewGroupMiners[runningGroupKey] = newGroupMiner;
                    }
                    else if (MiningSession.IS_DONATING && MiningSession.DONATION_SESSION && MiningSession.SHOULD_STOP_DONATING)
                    {
                        if (it == count)
                        {
                            MiningSession.DONATION_SESSION = false;
                            MiningSession.IS_DONATING = false;
                            MiningSession.DonationStart = MiningSession.DonationStart.Add(MiningSession.DonateEvery);
                        }
                        toStopGroupMiners[runningGroupKey] = _runningGroupMiners[runningGroupKey];
                        Helpers.ConsolePrint(TAG, "STOPPING - DEV_FEE - Next Dev-Fee Mining will start in 24 Hours.");
                        var miningPairs = newGroupedMiningPairs[runningGroupKey];
                        var newAlgoType = GetMinerPairAlgorithmType(miningPairs);
                        GroupMiner newGroupMiner = null;
                        if (newAlgoType == AlgorithmType.DaggerHashimoto)
                        {
                            if (_ethminerNVIDIAPaused != null && _ethminerNVIDIAPaused.Key == runningGroupKey)
                            {
                                newGroupMiner = _ethminerNVIDIAPaused;
                            }
                            if (_ethminerAMDPaused != null && _ethminerAMDPaused.Key == runningGroupKey)
                            {
                                newGroupMiner = _ethminerAMDPaused;
                            }
                        }
                        if (newGroupMiner == null)
                        {
                            newGroupMiner = new GroupMiner(miningPairs, runningGroupKey);
                        }
                        toRunNewGroupMiners[runningGroupKey] = newGroupMiner;
                    }
                    else
                    {
                        MiningSession.DONATION_SESSION = false;
                        MiningSession.IS_DONATING = false;
                        // runningGroupKey is contained but needs to check if mining algorithm is changed
                        var miningPairs = newGroupedMiningPairs[runningGroupKey];
                        var newAlgoType = GetMinerPairAlgorithmType(miningPairs);
                        if (newAlgoType != AlgorithmType.NONE && newAlgoType != AlgorithmType.INVALID)
                        {
                            // if algoType valid and different from currently running update
                            if (newAlgoType != _runningGroupMiners[runningGroupKey].DualAlgorithmType)
                            {
                                // remove current one and schedule to stop mining
                                toStopGroupMiners[runningGroupKey] = _runningGroupMiners[runningGroupKey];
                                // create new one TODO check if DaggerHashimoto
                                GroupMiner newGroupMiner = null;
                                if (newAlgoType == AlgorithmType.DaggerHashimoto)
                                {
                                    if (_ethminerNVIDIAPaused != null && _ethminerNVIDIAPaused.Key == runningGroupKey)
                                    {
                                        newGroupMiner = _ethminerNVIDIAPaused;
                                    }
                                    if (_ethminerAMDPaused != null && _ethminerAMDPaused.Key == runningGroupKey)
                                    {
                                        newGroupMiner = _ethminerAMDPaused;
                                    }
                                }
                                if (newGroupMiner == null)
                                {
                                    newGroupMiner = new GroupMiner(miningPairs, runningGroupKey);
                                }
                                toRunNewGroupMiners[runningGroupKey] = newGroupMiner;
                            }
                            else
                                noChangeGroupMiners[runningGroupKey] = _runningGroupMiners[runningGroupKey];
                        }
                    }
                }
                // check brand new
                foreach (var kvp in newGroupedMiningPairs)
                {
                    var key = kvp.Key;
                    var miningPairs = kvp.Value;
                    if (_runningGroupMiners.ContainsKey(key) == false)
                    {
                        GroupMiner newGroupMiner = new GroupMiner(miningPairs, key);
                        toRunNewGroupMiners[key] = newGroupMiner;
                    }
                }

                if ((toStopGroupMiners.Values.Count > 0) || (toRunNewGroupMiners.Values.Count > 0))
                {
                    StringBuilder stringBuilderPreviousAlgo = new StringBuilder();
                    StringBuilder stringBuilderCurrentAlgo = new StringBuilder();
                    StringBuilder stringBuilderNoChangeAlgo = new StringBuilder();
                    // stop old miners
                    foreach (var toStop in toStopGroupMiners.Values)
                    {
                        stringBuilderPreviousAlgo.Append(String.Format("{0}: {1}, ", toStop.DevicesInfoString, toStop.AlgorithmType));

                        toStop.Stop();
                        _runningGroupMiners.Remove(toStop.Key);
                        // TODO check if daggerHashimoto and save
                        if (toStop.AlgorithmType == AlgorithmType.DaggerHashimoto)
                        {
                            if (toStop.DeviceType == DeviceType.NVIDIA)
                            {
                                _ethminerNVIDIAPaused = toStop;
                            }
                            else if (toStop.DeviceType == DeviceType.AMD)
                            {
                                _ethminerAMDPaused = toStop;
                            }
                        }
                    }

                    // start new miners
                    foreach (var toStart in toRunNewGroupMiners.Values)
                    {
                        stringBuilderCurrentAlgo.Append(String.Format("{0}: {1}, ", toStart.DevicesInfoString, toStart.AlgorithmType));

                        toStart.Start(_miningLocation, _btcAddress, _worker);
                        _runningGroupMiners[toStart.Key] = toStart;
                    }

                    // which miners dosen't change
                    foreach (var noChange in noChangeGroupMiners.Values)
                        stringBuilderNoChangeAlgo.Append(String.Format("{0}: {1}, ", noChange.DevicesInfoString, noChange.AlgorithmType));

                    if (stringBuilderPreviousAlgo.Length > 0)
                        Helpers.ConsolePrint(TAG, String.Format("Stop Mining: {0}", stringBuilderPreviousAlgo.ToString()));

                    if (stringBuilderCurrentAlgo.Length > 0)
                        Helpers.ConsolePrint(TAG, String.Format("Now Mining : {0}", stringBuilderCurrentAlgo.ToString()));

                    if (stringBuilderNoChangeAlgo.Length > 0)
                        Helpers.ConsolePrint(TAG, String.Format("No change  : {0}", stringBuilderNoChangeAlgo.ToString()));
                }
            }

            // stats quick fix code
            //if (_currentAllGroupedDevices.Count != _previousAllGroupedDevices.Count) {
            await MinerStatsCheck(CryptoMiner937Data);
            //}
        }

        private AlgorithmType GetMinerPairAlgorithmType(List<MiningPair> miningPairs)
        {
            if (miningPairs.Count > 0)
            {
                return miningPairs[0].Algorithm.DualCryptoMiner937ID();
            }
            return AlgorithmType.NONE;
        }

        public async Task MinerStatsCheck(Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data)
        {
            double CurrentProfit = 0.0d;
            _mainFormRatesComunication.ClearRates(_runningGroupMiners.Count);
            var checks = new List<GroupMiner>(_runningGroupMiners.Values);
            try
            {
                foreach (var groupMiners in checks)
                {
                    Miner m = groupMiners.Miner;

                    // skip if not running or if await already in progress
                    if (!m.IsRunning || m.IsUpdatingAPI) continue;

                    m.IsUpdatingAPI = true;
                    ApiData AD = await m.GetSummaryAsync();
                    m.IsUpdatingAPI = false;
                    if (AD == null)
                    {
                        Helpers.ConsolePrint(m.MinerTag(), "GetSummary returned null..");
                    }
                    // set rates
                    if (CryptoMiner937Data != null && AD != null)
                    {
                        groupMiners.CurrentRate = CryptoMiner937Data[AD.AlgorithmID].paying * AD.Speed * 0.000000001;
                        if (CryptoMiner937Data.ContainsKey(AD.SecondaryAlgorithmID))
                        {
                            groupMiners.CurrentRate += CryptoMiner937Data[AD.SecondaryAlgorithmID].paying * AD.SecondarySpeed * 0.000000001;
                        }
                    }
                    else
                    {
                        groupMiners.CurrentRate = 0;
                        // set empty
                        AD = new ApiData(groupMiners.AlgorithmType);
                    }
                    CurrentProfit += groupMiners.CurrentRate;
                    // Update GUI
                    _mainFormRatesComunication.AddRateInfo(m.MinerTag(), groupMiners.DevicesInfoString, AD, groupMiners.CurrentRate, m.IsApiReadException);
                }
            }
            catch (Exception e) { Helpers.ConsolePrint(TAG, e.Message); }
        }
    }
}