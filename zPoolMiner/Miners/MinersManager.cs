namespace zPoolMiner.Miners
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Interfaces;

    /// <summary>
    /// Defines the <see cref="MinersManager" />
    /// </summary>
    public static class MinersManager
    {
        /// <summary>
        /// Defines the CurMiningSession
        /// </summary>
        private static MiningSession CurMiningSession;

        /// <summary>
        /// The StopAllMiners
        /// </summary>
        public static void StopAllMiners()
        {
            if (CurMiningSession != null) CurMiningSession.StopAllMiners();
            CurMiningSession = null;
        }

        /// <summary>
        /// The StopAllMinersNonProfitable
        /// </summary>
        public static void StopAllMinersNonProfitable()
        {
            if (CurMiningSession != null) CurMiningSession.StopAllMinersNonProfitable();
        }

        /// <summary>
        /// The GetActiveMinersGroup
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public static string GetActiveMinersGroup()
        {
            if (CurMiningSession != null)
            {
                return CurMiningSession.GetActiveMinersGroup();
            }
            // if no session it is idle
            return "IDLE";
        }

        /// <summary>
        /// The GetActiveMinersIndexes
        /// </summary>
        /// <returns>The <see cref="List{int}"/></returns>
        public static List<int> GetActiveMinersIndexes()
        {
            if (CurMiningSession != null)
            {
                return CurMiningSession.ActiveDeviceIndexes;
            }
            return new List<int>();
        }

        /// <summary>
        /// The GetTotalRate
        /// </summary>
        /// <returns>The <see cref="double"/></returns>
        public static double GetTotalRate()
        {
            if (CurMiningSession != null) return CurMiningSession.GetTotalRate();
            return 0;
        }

        /// <summary>
        /// The StartInitialize
        /// </summary>
        /// <param name="mainFormRatesComunication">The <see cref="IMainFormRatesComunication"/></param>
        /// <param name="miningLocation">The <see cref="string"/></param>
        /// <param name="worker">The <see cref="string"/></param>
        /// <param name="btcAddress">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool StartInitialize(IMainFormRatesComunication mainFormRatesComunication,
            string miningLocation, string worker, string btcAddress)
        {
            CurMiningSession = new MiningSession(ComputeDeviceManager.Available.AllAvaliableDevices,
                mainFormRatesComunication, miningLocation, worker, btcAddress);

            return CurMiningSession.IsMiningEnabled;
        }

        /// <summary>
        /// The IsMiningEnabled
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsMiningEnabled()
        {
            if (CurMiningSession != null) return CurMiningSession.IsMiningEnabled;
            return false;
        }

        /// <summary>
        /// SwichMostProfitable should check the best combination for most profit.
        /// Calculate profit for each supported algorithm per device and group.
        /// </summary>
        /// <param name="CryptoMiner937Data"></param>
        public static async Task SwichMostProfitableGroupUpMethod(Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data)
        {
            if (CurMiningSession != null) await CurMiningSession.SwitchMostProfitableGroupUpMethod(CryptoMiner937Data);
        }

        /// <summary>
        /// The MinerStatsCheck
        /// </summary>
        /// <param name="CryptoMiner937Data">The <see cref="Dictionary{AlgorithmType, CryptoMiner937API}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        async public static Task MinerStatsCheck(Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data)
        {
            if (CurMiningSession != null) await CurMiningSession.MinerStatsCheck(CryptoMiner937Data);
        }
    }
}
