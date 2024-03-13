namespace zPoolMiner.Miners.Grouping
{
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="GroupingLogic" />
    /// </summary>
    public static class GroupingLogic
    {
        /// <summary>
        /// The ShouldGroup
        /// </summary>
        /// <param name="a">The <see cref="MiningPair"/></param>
        /// <param name="b">The <see cref="MiningPair"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool ShouldGroup(MiningPair a, MiningPair b)
        {
            bool canGroup = IsGroupableMinerBaseType(a) && IsGroupableMinerBaseType(b);
            // group if same bin path and same algo type
            if (canGroup && IsSameBinPath(a, b) && IsSameAlgorithmType(a, b))
            {
                // Allow group if prospector
                //if ((IsNotCpuGroups(a, b) && IsSameDeviceType(a, b))
                    //|| (a.Algorithm.MinerBaseType == MinerBaseType.Prospector && b.Algorithm.MinerBaseType == MinerBaseType.Prospector))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The IsNotCpuGroups
        /// </summary>
        /// <param name="a">The <see cref="MiningPair"/></param>
        /// <param name="b">The <see cref="MiningPair"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsNotCpuGroups(MiningPair a, MiningPair b)
        {
            return a.Device.DeviceType != DeviceType.CPU && b.Device.DeviceType != DeviceType.CPU;
        }

        /// <summary>
        /// The IsSameBinPath
        /// </summary>
        /// <param name="a">The <see cref="MiningPair"/></param>
        /// <param name="b">The <see cref="MiningPair"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsSameBinPath(MiningPair a, MiningPair b)
        {
            return a.Algorithm.MinerBinaryPath == b.Algorithm.MinerBinaryPath;
        }

        /// <summary>
        /// The IsSameAlgorithmType
        /// </summary>
        /// <param name="a">The <see cref="MiningPair"/></param>
        /// <param name="b">The <see cref="MiningPair"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsSameAlgorithmType(MiningPair a, MiningPair b)
        {
            return a.Algorithm.DualCryptoMiner937ID() == b.Algorithm.DualCryptoMiner937ID();
        }

        /// <summary>
        /// The IsSameDeviceType
        /// </summary>
        /// <param name="a">The <see cref="MiningPair"/></param>
        /// <param name="b">The <see cref="MiningPair"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsSameDeviceType(MiningPair a, MiningPair b)
        {
            return a.Device.DeviceType == b.Device.DeviceType;
        }

        /// <summary>
        /// The IsGroupableMinerBaseType
        /// </summary>
        /// <param name="a">The <see cref="MiningPair"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsGroupableMinerBaseType(MiningPair a)
        {
            return a.Algorithm.MinerBaseType != MinerBaseType.cpuminer;
        }
    }
}
