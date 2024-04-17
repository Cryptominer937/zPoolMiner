namespace zPoolMiner.Miners
{
    using NiceHashMiner.Miners;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.equihash;

    /// <summary>
    /// Defines the <see cref="MinerFactory" />
    /// </summary>
    public class MinerFactory
    {
        /// <summary>
        /// The CreateEthminer
        /// </summary>
        /// <param name="deviceType">The <see cref="DeviceType"/></param>
        /// <returns>The <see cref="Miner"/></returns>
        private static Miner CreateEthminer(DeviceType deviceType)
        {
            if (DeviceType.AMD == deviceType)
            {
                return new MinerEtherumOCL();
            }
            else if (DeviceType.NVIDIA == deviceType)
            {
                return new MinerEtherumCUDA();
            }

            return null;
        }

        /// <summary>
        /// The CreateClaymore
        /// </summary>
        /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
        /// <param name="secondaryAlgorithmType">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="Miner"/></returns>
        private static Miner CreateClaymore(AlgorithmType algorithmType, AlgorithmType secondaryAlgorithmType)
        {
            if (AlgorithmType.equihash == algorithmType)
            {
                return new ClaymoreZcashMiner();
            }
            else if (AlgorithmType.NeoScrypt == algorithmType)
            {
                return new ClaymoreNeoscryptMiner();
            }
            else if (AlgorithmType.cryptonight == algorithmType)
            {
                return new ClaymorecryptonightMiner();
            }
            else if (AlgorithmType.DaggerHashimoto == algorithmType)
            {
                return new ClaymoreDual(secondaryAlgorithmType);
            }

            return null;
        }

        /// <summary>
        /// The CreateExperimental
        /// </summary>
        /// <param name="deviceType">The <see cref="DeviceType"/></param>
        /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="Miner"/></returns>
        private static Miner CreateExperimental(DeviceType deviceType, AlgorithmType algorithmType)
        {
            if (AlgorithmType.NeoScrypt == algorithmType && DeviceType.NVIDIA == deviceType)
            {
                return new Ccminer();
            }

            return null;
        }

        /// <summary>
        /// The CreateMiner
        /// </summary>
        /// <param name="deviceType">The <see cref="DeviceType"/></param>
        /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
        /// <param name="minerBaseType">The <see cref="MinerBaseType"/></param>
        /// <param name="secondaryAlgorithmType">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="Miner"/></returns>
        public static Miner CreateMiner(DeviceType deviceType, AlgorithmType algorithmType, MinerBaseType minerBaseType, AlgorithmType secondaryAlgorithmType = AlgorithmType.NONE)
        {
            switch (minerBaseType)
            {
                case MinerBaseType.cpuminer:
                    return new Cpuminer();

                case MinerBaseType.ccminer:
                    return new Ccminer();

                case MinerBaseType.ccminer_tpruvot2:
                    return new Ccminer();

                case MinerBaseType.sgminer:
                    return new Sgminer();

                case MinerBaseType.GatelessGate:
                    return new Glg();

                case MinerBaseType.Claymore:
                    return CreateClaymore(algorithmType, secondaryAlgorithmType);

                case MinerBaseType.lolMinerAmd:
                    return new lolMinerAmd();

                case MinerBaseType.lolMinerNvidia:
                    return new lolMinerNvidia();

                case MinerBaseType.OptiminerAMD:
                    return new OptiminerZcashMiner();

                case MinerBaseType.experimental:
                    return CreateExperimental(deviceType, algorithmType);

                case MinerBaseType.CPU_SRBMiner:
                    return new Xmrig();

                case MinerBaseType.CPU_XMRig:
                    return new CPU_Xmrig();

                case MinerBaseType.CPU_XMRigUPX:
                    return new CPU_XMRigUPX();

                case MinerBaseType.CPU_RKZ:
                    return new CPU_RKZ();

                case MinerBaseType.CPU_rplant:
                    return new CPU_rplant();

                case MinerBaseType.CPU_nosuch:
                    return new CPU_nosuch();

                case MinerBaseType.trex:
                    return new trex();

                case MinerBaseType.CryptoDredge16:
                    return new CryptoDredge16();

                case MinerBaseType.CryptoDredge25:
                    return new CryptoDredge25();

                case MinerBaseType.CryptoDredge26:
                    return new CryptoDredge26();

                case MinerBaseType.ZEnemy:
                    return new ZEnemy();

                case MinerBaseType.MiniZ:
                    return new MiniZ();

                case MinerBaseType.CPU_verium:
                    return new CPU_verium();

                /*case MinerBaseType.Palgin_Neoscrypt:
                    return new Palgin_Neoscrypt();

                case MinerBaseType.Palgin_HSR:
                    return new Palgin_HSR();*/
                case MinerBaseType.mkxminer:
                    return new Mkxminer();
            }

            return null;
        }

        // create miner creates new miners based on device type and algorithm/miner path
        /// <summary>
        /// The CreateMiner
        /// </summary>
        /// <param name="device">The <see cref="ComputeDevice"/></param>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <returns>The <see cref="Miner"/></returns>
        public static Miner CreateMiner(ComputeDevice device, Algorithm algorithm)
        {
            if (device != null && algorithm != null)
            {
                return CreateMiner(device.DeviceType, algorithm.CryptoMiner937ID, algorithm.MinerBaseType, algorithm.SecondaryCryptoMiner937ID);
            }

            return null;
        }
    }
}