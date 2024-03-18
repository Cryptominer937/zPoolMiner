namespace zPoolMiner.Miners.Grouping
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using zPoolMiner.Configs.ConfigJsonFile;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Configs;

    /// <summary>
    /// Defines the <see cref="MinerPathPackageFile" />
    /// </summary>
    internal class MinerPathPackageFile : ConfigFile<MinerPathPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinerPathPackageFile"/> class.
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        public MinerPathPackageFile(string name)
            : base(FOLDERS.INTERNALS, String.Format("{0}.json", name), String.Format("{0}_old.json", name))
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="MinerPathPackage" />
    /// </summary>
    public class MinerPathPackage
    {
        /// <summary>
        /// Defines the Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Defines the DeviceType
        /// </summary>
        public DeviceGroupType DeviceType;

        /// <summary>
        /// Defines the MinerTypes
        /// </summary>
        public List<MinerTypePath> MinerTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinerPathPackage"/> class.
        /// </summary>
        /// <param name="type">The <see cref="DeviceGroupType"/></param>
        /// <param name="paths">The <see cref="List{MinerTypePath}"/></param>
        public MinerPathPackage(DeviceGroupType type, List<MinerTypePath> paths)
        {
            DeviceType = type;
            MinerTypes = paths;
            Name = DeviceType.ToString();
        }
    }

    /// <summary>
    /// Defines the <see cref="MinerTypePath" />
    /// </summary>
    public class MinerTypePath
    {
        /// <summary>
        /// Defines the Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Defines the Type
        /// </summary>
        public MinerBaseType Type;

        /// <summary>
        /// Defines the Algorithms
        /// </summary>
        public List<MinerPath> Algorithms;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinerTypePath"/> class.
        /// </summary>
        /// <param name="type">The <see cref="MinerBaseType"/></param>
        /// <param name="paths">The <see cref="List{MinerPath}"/></param>
        public MinerTypePath(MinerBaseType type, List<MinerPath> paths)
        {
            Type = type;
            Algorithms = paths;
            Name = type.ToString();
        }
    }

    /// <summary>
    /// Defines the <see cref="MinerPath" />
    /// </summary>
    public class MinerPath
    {
        /// <summary>
        /// Defines the Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Defines the Algorithm
        /// </summary>
        public AlgorithmType Algorithm;

        /// <summary>
        /// Defines the Path
        /// </summary>
        public string Path;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinerPath"/> class.
        /// </summary>
        /// <param name="algo">The <see cref="AlgorithmType"/></param>
        /// <param name="path">The <see cref="string"/></param>
        public MinerPath(AlgorithmType algo, string path)
        {
            Algorithm = algo;
            Path = path;
            Name = Algorithm.ToString();
        }
    }

    /// <summary>
    /// MinerPaths, used just to store miners paths strings. Only one instance needed
    /// </summary>
    public static class MinerPaths
    {
        /// <summary>
        /// Defines the <see cref="Data" />
        /// </summary>
        public static class Data
        {
            // root binary folder
            // root binary folder            /// <summary>
            /// Defines the _bin
            /// </summary>
            private const string _bin = @"bin";

            /// <summary>
            /// ccminers
            /// </summary>
            //public const string ccminer_22 = _bin + @"\ccminer_22\ccminer.exe";
            //public const string ccminer_ocminer = _bin + @"\ccminer_ocminer\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_alexis_hsr
            /// </summary>
            //public const string ccminer_alexis_hsr = _bin + @"\ccminer_alexis_hsr\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_alexis78
            /// </summary>
            //public const string ccminer_alexis78 = _bin + @"\ccminer_alexis78\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_cryptonight
            /// </summary>
            //public const string ccminer_cryptonight = _bin + @"\ccminer_cryptonight\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_decred
            /// </summary>
            //public const string ccminer_decred = _bin + @"\ccminer_decred\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_klaust
            /// </summary>
            //public const string ccminer_klaust = _bin + @"\ccminer_klaust\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_klaust818
            /// </summary>
            //public const string ccminer_klaust818 = _bin + @"\ccminer_klaust818\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_nanashi
            /// </summary>
            //public const string ccminer_nanashi = _bin + @"\ccminer_nanashi\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_neoscrypt
            /// </summary>
            //public const string ccminer_neoscrypt = _bin + @"\ccminer_neoscrypt\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_palgin
            /// </summary>
            //public const string ccminer_palgin = _bin + @"\ccminer_palgin\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_polytimos
            /// </summary>
            //public const string ccminer_polytimos = _bin + @"\ccminer_polytimos\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_skunkkrnlx
            /// </summary>
            //public const string ccminer_skunkkrnlx = _bin + @"\ccminer_skunkkrnlx\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_sp
            /// </summary>
            //public const string ccminer_sp = _bin + @"\ccminer_sp\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_tpruvot
            /// </summary>
            //public const string ccminer_tpruvot = _bin + @"\ccminer_tpruvot\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_tpruvot2
            /// </summary>
            public const string ccminer_tpruvot2 = _bin + @"\ccminer_tpruvot2\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_x11gost
            /// </summary>
            //public const string ccminer_x11gost = _bin + @"\ccminer_x11gost\ccminer.exe";

            /// <summary>
            /// Defines the ccminer_xevan
            /// </summary>
            //public const string ccminer_xevan = _bin + @"\ccminer_xevan\ccminer.exe";

            public const string lolMiner = _bin + @"\lolMiner\lolMiner.exe";
            /// <summary>
            /// CPUminers
            /// </summary>
            public const string cpuminer_opt_AES = _bin + @"\CPU\CPU-JayDDee\cpuminer-aes-sse42.exe";

            /// <summary>
            /// Defines the cpuminer_opt_AVX2
            /// </summary>
            public const string cpuminer_opt_AVX2 = _bin + @"\CPU\CPU-JayDDee\cpuminer-avx2.exe";

            /// <summary>
            /// ethminers
            /// </summary>
            //public const string ethminer = _bin + @"\ethminer\ethminer.exe";

            /// <summary>
            /// sgminers
            /// </summary>
            public const string sgminer_5_6_0_general = _bin + @"\AMD\sgminer-5-6-0-general\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_gm
            /// </summary>
            public const string sgminer_gm = _bin + @"\AMD\sgminer-gm\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_HSR
            /// </summary>
            //public const string sgminer_HSR = _bin + @"\AMD\sgminer-HSR\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_Phi
            /// </summary>
            public const string sgminer_Phi = _bin + @"\AMD\sgminer-Phi\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_Bitcore
            /// </summary
            //public const string sgminer_Bitcore = _bin + @"\AMD\sgminer-Bitcore\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_Skein
            /// </summary>
            public const string sgminer_Skein = _bin + @"\AMD\sgminer-Skein\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_Tribus
            /// </summary>
            //public const string sgminer_Tribus = _bin + @"\AMD\sgminer-Tribus\sgminer.exe";

            /// <summary>
            /// Defines the sgminer_Xevan
            /// </summary>
            //public const string sgminer_Xevan = _bin + @"\AMD\sgminer-Xevan\sgminer.exe";
            public const string sgminer_aceneun = _bin + @"\AMD\sgminer-aceneun\sgminer.exe";

            /// <summary>
            /// Defines the glg
            /// </summary>
            public const string glg = _bin + @"\AMD\GLG\gatelessgate.exe";

            /// <summary>
            /// Defines the nheqminer
            /// </summary>
            //public const string nheqminer = _bin + @"\nheqminer_v0.4b\nheqminer.exe";

            /// <summary>
            /// Defines the excavator
            /// </summary>
            //public const string excavator = _bin + @"\excavator\excavator.exe";

            /// <summary>
            /// Defines the XmrStackCPUMiner
            /// </summary>
            //public const string XmrStackCPUMiner = _bin + @"\xmr-stak-cpu\xmr-stak-cpu.exe";

            /// <summary>
            /// Defines the XmrStakAMD
            /// </summary>
            //public const string XmrStakAMD = _bin + @"\xmr-stak-amd\xmr-stak-amd.exe";

            /// <summary>
            /// Defines the Xmrig
            /// </summary>
            //public const string Xmrig = _bin + @"\xmrig\xmrig.exe";

            public const string CPU_cpuminerOptBF = _bin + @"\CPU\CPU-cpuminerOptBF\cpuminer-aes-sse42.exe";
            public const string CPU_Cpupower = _bin + @"\CPU\CPU-Cpupower\cpuminer-aes-sse42.exe";
            public const string CPU_JayDDee = _bin + @"\CPU\CPU-JayDDee\cpuminer-aes-sse42.exe";
            public const string CPU_JayDDeeYespower = _bin + @"\CPU\CPU-JayDDeeYespower\cpuminer-sse42.exe";
            public const string CPU_nosuch = _bin + @"\CPU\CPU-nosuch\cpuminer-aes-sse2.exe";
            public const string CPU_RKZ = _bin + @"\CPU\CPU-RKZ\cpuminer.exe";
            public const string CPU_rplant = _bin + @"\CPU\CPU-rplant\cpuminer-aes.exe";
            public const string CPU_verium = _bin + @"\CPU\CPU-Verium\cpuminer.exe";
            public const string CPU_SRBMiner = _bin + @"\CPU\CPU-SRBMiner\SRBMiner-MULTI.exe";
            public const string CPU_XMRig = _bin + @"\CPU\CPU-XMRig\xmrig.exe";
            public const string CPU_XMRigUPX = _bin + @"\CPU\CPU-XMRigUPX\xmrig.exe";
            public const string trex = _bin + @"\NVIDIA\NVIDIA-trex\t-rex.exe";
            public const string MiniZ = _bin + @"\NVIDIA\NVIDIA-miniZ\miniZ.exe";
            public const string CryptoDredge = _bin + @"\NVIDIA\NVIDIA-CryptoDredge\CryptoDredge.exe";
            public const string ZEnemy = _bin + @"\NVIDIA\NVIDIA-zealotenemy\z-enemy.exe";
            //public const string ClaymoreZcashMiner = _bin + @"\AMD-NVIDIA\claymore_zcash\ZecMiner64.exe";

            //public const string ClaymoreNeoscryptMiner = _bin + @"\AMD-NVIDIA\claymore_neoscrypt\NeoScryptMiner.exe";

            /// <summary>
            /// Defines the ClaymorecryptonightMiner
            /// </summary>
            //public const string ClaymorecryptonightMiner = _bin + @"\AMD-NVIDIA\claymore_cryptonight\NsGpuCNMiner.exe";

            /// <summary>
            /// Defines the ClaymorecryptonightMiner_old
            /// </summary>
            //public const string ClaymorecryptonightMiner_old = _bin + @"\AMD-NVIDIA\claymore_cryptonight_old\NsGpuCNMiner.exe";

            /// <summary>
            /// Defines the OptiminerZcashMiner
            /// </summary>
            //public const string OptiminerZcashMiner = _bin + @"\AMD\optiminer_zcash_win\Optiminer.exe";

            /// <summary>
            /// Defines the ClaymoreDual
            /// </summary>
            //public const string ClaymoreDual = _bin + @"\AMD-NVIDIA\claymore_dual\EthDcrMiner64.exe";

            /// <summary>
            /// Defines the mkxminer
            /// </summary>
            public const string mkxminer = _bin + @"\AMD\mkxminer\mkxminer.exe";
            /// <summary>
                                                                                     /// Defines the NONE
                                                                                     /// </summary>
            public const string NONE = "";

            // root binary folder
            // root binary folder            /// <summary>
            /// Defines the _bin
            /// </summary>
            private const string _bin_3rdparty = @"bin_3rdparty";

            /// <summary>
            /// Defines the ClaymoreZcashMiner
            /// </summary>
            
        }

        // NEW START
        ////////////////////////////////////////////
        // Pure functions
        //public static bool IsMinerAlgorithmAvaliable(List<Algorithm> algos, MinerBaseType minerBaseType, AlgorithmType algorithmType) {
        //    return algos.FindIndex((a) => a.MinerBaseType == minerBaseType && a.CryptoMiner937ID == algorithmType) > -1;
        //}
        /// <summary>
        /// The GetPathFor
        /// </summary>
        /// <param name="minerBaseType">The <see cref="MinerBaseType"/></param>
        /// <param name="algoType">The <see cref="AlgorithmType"/></param>
        /// <param name="devGroupType">The <see cref="DeviceGroupType"/></param>
        /// <param name="def">The <see cref="bool"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetPathFor(MinerBaseType minerBaseType, AlgorithmType algoType, DeviceGroupType devGroupType, CPUExtensionType MostOptimizedCPUExtensionType, bool def = false)
        {
            if (!def & configurableMiners.Contains(minerBaseType))
            {
                // Override with internals
                var path = minerPathPackages.Find(p => p.DeviceType == devGroupType)
                    .MinerTypes.Find(p => p.Type == minerBaseType)
                    .Algorithms.Find(p => p.Algorithm == algoType);
                if (path != null)
                {
                    if (File.Exists(path.Path))
                    {
                        return path.Path;
                    }
                    else
                    {
                        Helpers.ConsolePrint("PATHS", String.Format("Path {0} not found, using defaults", path.Path));
                    }
                }
            }
            switch (minerBaseType)
            {
                case MinerBaseType.cpuminer:
                    return CPU_GROUP.cpu_miner_opt(MostOptimizedCPUExtensionType);

                case MinerBaseType.ccminer:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_tpruvot2:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.sgminer:
                    return AMD_GROUP.Sgminer_path(algoType);

                case MinerBaseType.GatelessGate:
                    return AMD_GROUP.Glg_path(algoType);

                /*case MinerBaseType.Claymore:
                    return AMD_GROUP.ClaymorePath(algoType);*/

                /*case MinerBaseType.OptiminerAMD:
                    return Data.OptiminerZcashMiner;*/

                case MinerBaseType.experimental:
                    return EXPERIMENTAL.GetPath(algoType, devGroupType);

                case MinerBaseType.CPU_XMRig:
                    return Data.CPU_XMRig;

                case MinerBaseType.CPU_XMRigUPX:
                    return Data.CPU_XMRigUPX;
                case MinerBaseType.CPU_RKZ:
                    return Data.CPU_RKZ;
                case MinerBaseType.CPU_rplant:
                    return Data.CPU_rplant;
                case MinerBaseType.CPU_nosuch:
                    return Data.CPU_nosuch;
                case MinerBaseType.trex:
                    return NVIDIA_GROUPS.trex(algoType, devGroupType);
                case MinerBaseType.MiniZ:
                    return NVIDIA_GROUPS.MiniZ(algoType, devGroupType);

                case MinerBaseType.ZEnemy:
                    return NVIDIA_GROUPS.ZEnemy(algoType, devGroupType);

                case MinerBaseType.CryptoDredge:
                    return NVIDIA_GROUPS.CryptoDredge(algoType, devGroupType);
                case MinerBaseType.CPU_verium:
                    return Data.CPU_verium;
                case MinerBaseType.lolMiner:
                    return AMD_GROUP.lolMiner(algoType);
            }
            return Data.NONE;
        }

        /// <summary>
        /// The GetPathFor
        /// </summary>
        /// <param name="computeDevice">The <see cref="ComputeDevice"/></param>
        /// <param name="algorithm">The <see cref="Algorithm"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetPathFor(ComputeDevice computeDevice, Algorithm algorithm /*, Options: MinerPathsConfig*/)
        {
            if (computeDevice == null || algorithm == null)
            {
                return Data.NONE;
            }

            return GetPathFor(
                algorithm.MinerBaseType,
                algorithm.CryptoMiner937ID,
                computeDevice.DeviceGroupType,
                CPUUtils.GetMostOptimized()
                );
        }

        /// <summary>
        /// The IsValidMinerPath
        /// </summary>
        /// <param name="minerPath">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsValidMinerPath(string minerPath)
        {
            // TODO make a list of valid miner paths and check that instead
            return minerPath != null && Data.NONE != minerPath && minerPath != "";
        }

        /**
         * InitAlgorithmsMinerPaths gets and sets miner paths
         */
        /// <summary>
        /// The GetAndInitAlgorithmsMinerPaths
        /// </summary>
        /// <param name="algos">The <see cref="List{Algorithm}"/></param>
        /// <param name="computeDevice">The <see cref="ComputeDevice"/></param>
        /// <returns>The <see cref="List{Algorithm}"/></returns>
        public static List<Algorithm> GetAndInitAlgorithmsMinerPaths(List<Algorithm> algos, ComputeDevice computeDevice/*, Options: MinerPathsConfig*/)
        {
            var retAlgos = algos.FindAll((a) => a != null).ConvertAll((a) =>
            {
                a.MinerBinaryPath = GetPathFor(computeDevice, a/*, Options*/);
                return a;
            });

            return retAlgos;
        }

        // NEW END

        ////// private stuff from here on
        /// <summary>
        /// Defines the <see cref="NVIDIA_GROUPS" />
        /// </summary>
        private static class NVIDIA_GROUPS
        {
            /// <summary>
            /// The Ccminer_sm21_or_sm3x
            /// </summary>
            /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Ccminer_sm21_or_sm3x(AlgorithmType algorithmType)
            {
                return Data.ccminer_tpruvot2;
            }

                /// <summary>
                /// The Ccminer_sm5x
                /// </summary>
                /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
                /// <returns>The <see cref="string"/></returns>
                public static string Ccminer_sm5x(AlgorithmType algorithmType)
            {

                return Data.ccminer_tpruvot2;
            }

            /// <summary>
            /// The Ccminer_sm6x
            /// </summary>
            /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Ccminer_sm6x(AlgorithmType algorithmType)
            {
                

                return Data.ccminer_tpruvot2;
            }
            public static string trex(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return Data.trex;
                }
                // sm5x and sm6x have same settings otherwise
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x || nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    return Data.trex; ;
                }
                // TODO wrong case?
                return Data.NONE; // should not happen
            }
            public static string MiniZ(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return Data.MiniZ;
                }
                // sm5x and sm6x have same settings otherwise
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x || nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    return Data.MiniZ; ;
                }
                // TODO wrong case?
                return Data.NONE; // should not happen
            }

            public static string ZEnemy(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return Data.ZEnemy;
                }
                // sm5x and sm6x have same settings otherwise
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x || nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    return Data.ZEnemy; ;
                }
                // TODO wrong case?
                return Data.NONE; // should not happen
            }

            public static string CryptoDredge(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return Data.CryptoDredge;
                }
                // sm5x and sm6x have same settings otherwise
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x || nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    return Data.CryptoDredge; ;
                }
                // TODO wrong case?
                return Data.NONE; // should not happen
            }
            /// <summary>
            /// The Palgin_Neoscrypt_path
            /// </summary>
            /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
            /// <param name="nvidiaGroup">The <see cref="DeviceGroupType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Palgin_Neoscrypt_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings                
                // CN exception
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return Data.NONE;
                }
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x)
                {
                    return Data.NONE;
                }
                if (nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    
                }

                // TODO wrong case?
                return Data.NONE; // should not happen
            }

            /// <summary>
            /// The Palgin_HSR_path
            /// </summary>
            /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
            /// <param name="nvidiaGroup">The <see cref="DeviceGroupType"/></param>
            /// <returns>The <see cref="string"/></returns>
            /*public static string Palgin_HSR_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings                
                // CN exception
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return Data.NONE;
                }
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x)
                {
                    return Data.NONE;
                }
                if (nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    
                }

                // TODO wrong case?
                return Data.NONE; // should not happen
            }*/

            /// <summary>
            /// The Ccminer_path
            /// </summary>
            /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
            /// <param name="nvidiaGroup">The <see cref="DeviceGroupType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Ccminer_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return NVIDIA_GROUPS.Ccminer_sm21_or_sm3x(algorithmType);
                }
                // CN exception
                if (nvidiaGroup == DeviceGroupType.NVIDIA_6_x && algorithmType == AlgorithmType.cryptonight)
                {
                    return Data.ccminer_tpruvot2;
                }
                // sm5x and sm6x have same settings otherwise
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x)
                {
                    return NVIDIA_GROUPS.Ccminer_sm5x(algorithmType);
                }
                if (nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    return NVIDIA_GROUPS.Ccminer_sm6x(algorithmType);
                }
                // TODO wrong case?
                return Data.NONE; // should not happen
            }

            // untested might not need anymore
            /// <summary>
            /// The Ccminer_unstable_path
            /// </summary>
            /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
            /// <param name="nvidiaGroup">The <see cref="DeviceGroupType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Ccminer_unstable_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm5x and sm6x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x || nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {
                    
                }
                // TODO wrong case?
                return Data.ccminer_tpruvot2; // should not happen
            }
        }

        /// <summary>
        /// Defines the <see cref="AMD_GROUP" />
        /// </summary>
        private static class AMD_GROUP
        {
            /// <summary>
            /// The Sgminer_path
            /// </summary>
            /// <param name="type">The <see cref="AlgorithmType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Sgminer_path(AlgorithmType type)
            {
                if (AlgorithmType.cryptonight == type || AlgorithmType.DaggerHashimoto == type)
                {
                    return Data.sgminer_gm;
                }
                if (AlgorithmType.Skein == type)
                {
                    return Data.sgminer_Skein;
                }
                /*if (AlgorithmType.Bitcore == type)
                {
                    return Data.sgminer_Bitcore;
                }*/
                /*if (AlgorithmType.Hsr == type)
                {
                    return Data.sgminer_HSR;
                }*/
                if (AlgorithmType.Phi == type)
                {
                    return Data.sgminer_Phi;
                }
                if (AlgorithmType.karlsenhash == type)
                {
                    return Data.lolMiner;
                }
                /*if (AlgorithmType.x16r == type)
                {
                    return Data.sgminer_aceneun;
                }*/

                return Data.sgminer_5_6_0_general;
            }
            public static string lolMiner_path(AlgorithmType type)
            {
             
                if (AlgorithmType.karlsenhash == type)
                {
                    return Data.lolMiner;
                }

                return Data.lolMiner;
            }

            /// <summary>
            /// The Glg_path
            /// </summary>
            /// <param name="type">The <see cref="AlgorithmType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string Glg_path(AlgorithmType type)
            {
                // AlgorithmType.Pascal == type || AlgorithmType.DaggerHashimoto == type || AlgorithmType.Decred == type || AlgorithmType.Lbry == type || AlgorithmType.X11Gost == type || AlgorithmType.DaggerHashimoto == type
                if (AlgorithmType.cryptonight == type || AlgorithmType.DaggerHashimoto == type || AlgorithmType.equihash == type || AlgorithmType.NeoScrypt == type || AlgorithmType.Lyra2REv2 == type || AlgorithmType.Myriad_groestl == type || AlgorithmType.Keccak == type)
                {
                    return Data.glg;
                }
                return Data.NONE;
            }

            public static string lolMiner(AlgorithmType type)
            {
                if (AlgorithmType.karlsenhash == type)
                {
                    return Data.lolMiner;
                }
                return Data.NONE;
            }

            /// <summary>
            /// The ClaymorePath
            /// </summary>
            /// <param name="type">The <see cref="AlgorithmType"/></param>
            /// <returns>The <see cref="string"/></returns>
            /*public static string ClaymorePath(AlgorithmType type)
            {
                if (AlgorithmType.equihash == type)
                {
                    return Data.ClaymoreZcashMiner;
                }
                else if (AlgorithmType.NeoScrypt == type) {
                    return Data.ClaymoreNeoscryptMiner;
                }
                else if (AlgorithmType.cryptonight == type)
                {
                    return Data.ClaymorecryptonightMiner;
                }
                else if (AlgorithmType.DaggerHashimoto == type)
                {
                    return Data.ClaymoreDual;
                }
                return Data.NONE; // should not happen
            }*/
        }

        /// <summary>
        /// Defines the <see cref="CPU_GROUP" />
        /// </summary>
        internal static class CPU_GROUP
        {
            /// <summary>
            /// The cpu_miner_opt
            /// </summary>
            /// <param name="type">The <see cref="CPUExtensionType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string cpu_miner_opt(CPUExtensionType type)
            {
                // algorithmType is not needed ATM
                // algorithmType: AlgorithmType
                if (CPUExtensionType.AVX2 == type) { return Data.cpuminer_opt_AVX2; }
                if (CPUExtensionType.AES == type) { return Data.cpuminer_opt_AES; }

                return Data.NONE; // should not happen
            }
        }

        // unstable miners, NVIDIA for now
        /// <summary>
        /// Defines the <see cref="EXPERIMENTAL" />
        /// </summary>
        private static class EXPERIMENTAL
        {
            /// <summary>
            /// The GetPath
            /// </summary>
            /// <param name="algoType">The <see cref="AlgorithmType"/></param>
            /// <param name="devGroupType">The <see cref="DeviceGroupType"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string GetPath(AlgorithmType algoType, DeviceGroupType devGroupType)
            {
                if (devGroupType == DeviceGroupType.NVIDIA_6_x)
                {
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);
                }
                return Data.NONE; // should not happen
            }
        }

        /// <summary>
        /// Defines the minerPathPackages
        /// </summary>
        private static List<MinerPathPackage> minerPathPackages = new List<MinerPathPackage>();

        /// <summary>
        /// Defines the configurableMiners
        /// </summary>
        private static readonly List<MinerBaseType> configurableMiners = new List<MinerBaseType> {
            MinerBaseType.ccminer,
            MinerBaseType.sgminer
        };

        /// <summary>
        /// The InitializePackages
        /// </summary>
        public static void InitializePackages()
        {
            var defaults = new List<MinerPathPackage>();
            for (var i = DeviceGroupType.NONE + 1; i < DeviceGroupType.LAST; i++)
            {
                var minerTypePaths = new List<MinerTypePath>();
                var package = GroupAlgorithms.CreateDefaultsForGroup(i);
                foreach (var type in configurableMiners)
                {
                    if (package.ContainsKey(type))
                    {
                        var minerPaths = new List<MinerPath>();
                        foreach (var algo in package[type])
                        {
                            minerPaths.Add(new MinerPath(algo.CryptoMiner937ID, GetPathFor(type, algo.CryptoMiner937ID, i,0, true)));
                        }
                        minerTypePaths.Add(new MinerTypePath(type, minerPaths));
                    }
                }
                if (minerTypePaths.Count > 0)
                {
                    defaults.Add(new MinerPathPackage(i, minerTypePaths));
                }
            }

            foreach (var pack in defaults)
            {
                var packageName = String.Format("MinerPathPackage_{0}", pack.Name);
                var packageFile = new MinerPathPackageFile(packageName);
                var readPack = packageFile.ReadFile();
                if (readPack == null)
                {   // read has failed
                    Helpers.ConsolePrint("MinerPaths", "Creating internal paths config " + packageName);
                    minerPathPackages.Add(pack);
                    packageFile.Commit(pack);
                }
                else
                {
                    Helpers.ConsolePrint("MinerPaths", "Loading internal paths config " + packageName);
                    var isChange = false;
                    foreach (var miner in pack.MinerTypes)
                    {
                        var readMiner = readPack.MinerTypes.Find(x => x.Type == miner.Type);
                        if (readMiner != null)
                        {  // file contains miner type
                            foreach (var algo in miner.Algorithms)
                            {
                                if (!readMiner.Algorithms.Exists(x => x.Algorithm == algo.Algorithm))
                                {  // file does not contain algo on this miner
                                    Helpers.ConsolePrint("PATHS", String.Format("Algorithm {0} not found in miner {1} on device {2}. Adding default", algo.Name, miner.Name, pack.Name));
                                    readMiner.Algorithms.Add(algo);
                                    isChange = true;
                                }
                            }
                        }
                        else
                        {  // file does not contain miner type
                            Helpers.ConsolePrint("PATHS", String.Format("Miner {0} not found on device {1}", miner.Name, pack.Name));
                            readPack.MinerTypes.Add(miner);
                            isChange = true;
                        }
                    }
                    minerPathPackages.Add(readPack);
                    if (isChange) packageFile.Commit(readPack);
                }
            }
        }
    }
}
