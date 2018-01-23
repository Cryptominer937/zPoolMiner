using zPoolMiner.Devices;
using zPoolMiner.Enums;
using zPoolMiner.Miners.Equihash;

namespace zPoolMiner.Miners
{
    public class MinerFactory
    {
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

        private static Miner CreateClaymore(AlgorithmType algorithmType, AlgorithmType secondaryAlgorithmType)
        {
            if (AlgorithmType.Equihash == algorithmType)
            {
                return new ClaymoreZcashMiner();
            }
            else if (AlgorithmType.CryptoNight == algorithmType)
            {
                return new ClaymoreCryptoNightMiner();
            }
            else if (AlgorithmType.DaggerHashimoto == algorithmType)
            {
                return new ClaymoreDual(secondaryAlgorithmType);
            }
            return null;
        }

        private static Miner CreateExperimental(DeviceType deviceType, AlgorithmType algorithmType)
        {
            if (AlgorithmType.NeoScrypt == algorithmType && DeviceType.NVIDIA == deviceType)
            {
                return new Ccminer();
            }
            return null;
        }

        public static Miner CreateMiner(DeviceType deviceType, AlgorithmType algorithmType, MinerBaseType minerBaseType, AlgorithmType secondaryAlgorithmType = AlgorithmType.NONE)
        {
            switch (minerBaseType)
            {
                case MinerBaseType.ccminer:
                    return new Ccminer();

                case MinerBaseType.ccminer_22:
                    return new Ccminer();

                case MinerBaseType.ccminer_alexis_hsr:
                    return new Ccminer();

                case MinerBaseType.ccminer_alexis78:
                    return new Ccminer();

                case MinerBaseType.ccminer_klaust818:
                    return new Ccminer();

                case MinerBaseType.ccminer_polytimos:
                    return new Ccminer();

                case MinerBaseType.ccminer_xevan:
                    return new Ccminer();

                case MinerBaseType.ccminer_palgin:
                    return new Ccminer();

                case MinerBaseType.ccminer_skunkkrnlx:
                    return new Ccminer();

                case MinerBaseType.ccminer_tpruvot2:
                    return new Ccminer();

                case MinerBaseType.sgminer:
                    return new Sgminer();

                case MinerBaseType.GatelessGate:
                    return new Glg();

                case MinerBaseType.nheqminer:
                    return new Nheqminer();

                case MinerBaseType.ethminer:
                    return CreateEthminer(deviceType);

                case MinerBaseType.Claymore:
                    return CreateClaymore(algorithmType, secondaryAlgorithmType);

                case MinerBaseType.OptiminerAMD:
                    return new OptiminerZcashMiner();

                case MinerBaseType.excavator:
                    return new Excavator();

                case MinerBaseType.XmrStackCPU:
                    return new XmrStackCPUMiner();

                case MinerBaseType.ccminer_alexis:
                    return new Ccminer();

                case MinerBaseType.experimental:
                    return CreateExperimental(deviceType, algorithmType);

                case MinerBaseType.EWBF:
                    return new EWBF();

                case MinerBaseType.DSTM:
                    return new DSTM();

                case MinerBaseType.Prospector:
                    return new Prospector();

                case MinerBaseType.Xmrig:
                    return new Xmrig();

                case MinerBaseType.XmrStakAMD:
                    return new XmrStakAMD();

                case MinerBaseType.Claymore_old:
                    return new ClaymoreCryptoNightMiner(true);

                case MinerBaseType.hsrneoscrypt:
                    return new Hsrneoscrypt();
                    
                case MinerBaseType.hsrneoscrypt_hsr:
                    return new Hsrneoscrypt_hsr();
                //case MinerBaseType.mkxminer:
                   // return new Mkxminer();
            }
            return null;
        }

        // create miner creates new miners based on device type and algorithm/miner path
        public static Miner CreateMiner(ComputeDevice device, Algorithm algorithm)
        {
            if (device != null && algorithm != null)
            {
                return CreateMiner(device.DeviceType, algorithm.NiceHashID, algorithm.MinerBaseType, algorithm.SecondaryNiceHashID);
            }
            return null;
        }
    }
}
