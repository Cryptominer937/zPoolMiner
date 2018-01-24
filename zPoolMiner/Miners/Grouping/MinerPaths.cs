using System;
using System.Collections.Generic;
using System.IO;
using zPoolMiner.Configs.ConfigJsonFile;
using zPoolMiner.Devices;
using zPoolMiner.Enums;

namespace zPoolMiner.Miners.Grouping
{
    internal class MinerPathPackageFile : ConfigFile<MinerPathPackage>
    {
        public MinerPathPackageFile(string name)
            : base(FOLDERS.INTERNALS, String.Format("{0}.json", name), String.Format("{0}_old.json", name))
        {
        }
    }

    public class MinerPathPackage
    {
        public string Name;
        public DeviceGroupType DeviceType;
        public List<MinerTypePath> MinerTypes;

        public MinerPathPackage(DeviceGroupType type, List<MinerTypePath> paths)
        {
            DeviceType = type;
            MinerTypes = paths;
            Name = DeviceType.ToString();
        }
    }

    public class MinerTypePath
    {
        public string Name;
        public MinerBaseType Type;
        public List<MinerPath> Algorithms;

        public MinerTypePath(MinerBaseType type, List<MinerPath> paths)
        {
            Type = type;
            Algorithms = paths;
            Name = type.ToString();
        }
    }

    public class MinerPath
    {
        public string Name;
        public AlgorithmType Algorithm;
        public string Path;

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
        public static class Data
        {
            // root binary folder
            private const string _bin = @"bin";

            /// <summary>
            /// ccminers
            /// </summary>
            public const string ccminer_22 = _bin + @"\ccminer_22\ccminer.exe";

            public const string ccminer_alexis_hsr = _bin + @"\ccminer_alexis_hsr\ccminer.exe";
            public const string ccminer_alexis78 = _bin + @"\ccminer_alexis78\ccminer.exe";
            public const string ccminer_cryptonight = _bin + @"\ccminer_cryptonight\ccminer.exe";
            public const string ccminer_decred = _bin + @"\ccminer_decred\ccminer.exe";
            public const string ccminer_klaust = _bin + @"\ccminer_klaust\ccminer.exe";
            public const string ccminer_klaust818 = _bin + @"\ccminer_klaust818\ccminer.exe";
            public const string ccminer_nanashi = _bin + @"\ccminer_nanashi\ccminer.exe";
            public const string ccminer_neoscrypt = _bin + @"\ccminer_neoscrypt\ccminer.exe";
            public const string ccminer_palgin = _bin + @"\ccminer_palgin\ccminer.exe";
            public const string ccminer_polytimos = _bin + @"\ccminer_polytimos\ccminer.exe";
            public const string ccminer_skunkkrnlx = _bin + @"\ccminer_skunkkrnlx\ccminer.exe";
            public const string ccminer_sp = _bin + @"\ccminer_sp\ccminer.exe";
            public const string ccminer_tpruvot = _bin + @"\ccminer_tpruvot\ccminer.exe";
            public const string ccminer_tpruvot2 = _bin + @"\ccminer_tpruvot2\ccminer.exe";
            public const string ccminer_x11gost = _bin + @"\ccminer_x11gost\ccminer.exe";
            public const string ccminer_xevan = _bin + @"\ccminer_xevan\ccminer.exe";

            /// <summary>
            /// ethminers
            /// </summary>
            public const string ethminer = _bin + @"\ethminer\ethminer.exe";

            /// <summary>
            /// sgminers
            /// </summary>
            public const string sgminer_5_6_0_general = _bin + @"\sgminer-5-6-0-general\sgminer.exe";
            public const string sgminer_gm = _bin + @"\sgminer-gm\sgminer.exe";
            public const string sgminer_HSR = _bin + @"\sgminer-HSR\sgminer.exe";
            public const string sgminer_Phi = _bin + @"\sgminer-Phi\sgminer.exe";
            public const string sgminer_Bitcore = _bin + @"\sgminer-Bitcore\sgminer.exe";
            public const string sgminer_Skein = _bin + @"\sgminer-Skein\sgminer.exe";
            public const string sgminer_Tribus = _bin + @"\sgminer-Tribus\sgminer.exe";
            public const string sgminer_Xevan = _bin + @"\sgminer-Xevan\sgminer.exe";
            public const string glg = _bin + @"\glg\gatelessgate.exe";
            public const string nheqminer = _bin + @"\nheqminer_v0.4b\nheqminer.exe";
            public const string excavator = _bin + @"\excavator\excavator.exe";
            public const string XmrStackCPUMiner = _bin + @"\xmr-stak-cpu\xmr-stak-cpu.exe";
            public const string XmrStakAMD = _bin + @"\xmr-stak-amd\xmr-stak-amd.exe";
            public const string Xmrig = _bin + @"\xmrig\xmrig.exe";
            public const string NONE = "";

            // root binary folder
            private const string _bin_3rdparty = @"bin_3rdparty";

            public const string ClaymoreZcashMiner = _bin_3rdparty + @"\claymore_zcash\ZecMiner64.exe";
            public const string ClaymoreCryptoNightMiner = _bin_3rdparty + @"\claymore_cryptonight\NsGpuCNMiner.exe";
            public const string ClaymoreCryptoNightMiner_old = _bin_3rdparty + @"\claymore_cryptonight_old\NsGpuCNMiner.exe";
            public const string OptiminerZcashMiner = _bin_3rdparty + @"\optiminer_zcash_win\Optiminer.exe";
            public const string ClaymoreDual = _bin_3rdparty + @"\claymore_dual\EthDcrMiner64.exe";
            public const string EWBF = _bin_3rdparty + @"\ewbf\miner.exe";
            public const string DSTM = _bin_3rdparty + @"\dstm\zm.exe";
            public const string hsrneoscrypt = _bin_3rdparty + @"\hsrminer_neoscrypt\hsrminer_neoscrypt.exe";
            public const string hsrneoscrypt_hsr = _bin_3rdparty + @"\hsrminer_neoscrypt\hsrminer_hsr.exe";
            public const string prospector = _bin_3rdparty + @"\prospector\prospector.exe";
            public const string mkxminer = _bin_3rdparty + @"\mkxminer\mkxminer.exe";
        }

        // NEW START
        ////////////////////////////////////////////
        // Pure functions
        //public static bool IsMinerAlgorithmAvaliable(List<Algorithm> algos, MinerBaseType minerBaseType, AlgorithmType algorithmType) {
        //    return algos.FindIndex((a) => a.MinerBaseType == minerBaseType && a.NiceHashID == algorithmType) > -1;
        //}

        public static string GetPathFor(MinerBaseType minerBaseType, AlgorithmType algoType, DeviceGroupType devGroupType, bool def = false)
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
                case MinerBaseType.ccminer:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_22:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_alexis_hsr:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_alexis78:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_klaust818:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_palgin:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_polytimos:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_skunkkrnlx:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_xevan:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.ccminer_tpruvot2:
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);

                case MinerBaseType.sgminer:
                    return AMD_GROUP.Sgminer_path(algoType);

                case MinerBaseType.GatelessGate:
                    return AMD_GROUP.Glg_path(algoType);

                case MinerBaseType.nheqminer:
                    return Data.nheqminer;

                case MinerBaseType.ethminer:
                    return Data.ethminer;

                case MinerBaseType.Claymore:
                    return AMD_GROUP.ClaymorePath(algoType);

                case MinerBaseType.OptiminerAMD:
                    return Data.OptiminerZcashMiner;

                case MinerBaseType.excavator:
                    return Data.excavator;

                case MinerBaseType.XmrStackCPU:
                    return Data.XmrStackCPUMiner;

                case MinerBaseType.ccminer_alexis:
                    return NVIDIA_GROUPS.Ccminer_unstable_path(algoType, devGroupType);

                case MinerBaseType.experimental:
                    return EXPERIMENTAL.GetPath(algoType, devGroupType);

                case MinerBaseType.EWBF:
                    return Data.EWBF;

                case MinerBaseType.DSTM:
                    return Data.DSTM;

                case MinerBaseType.Prospector:
                    return Data.prospector;

                case MinerBaseType.Xmrig:
                    return Data.Xmrig;

                case MinerBaseType.XmrStakAMD:
                    return Data.XmrStakAMD;

                case MinerBaseType.Claymore_old:
                    return Data.ClaymoreCryptoNightMiner_old;

                case MinerBaseType.hsrneoscrypt:
                    return NVIDIA_GROUPS.Hsrneoscrypt_path(algoType, devGroupType);
                    
                case MinerBaseType.hsrneoscrypt_hsr:
                    return NVIDIA_GROUPS.hsrneoscrypt_hsr_path(algoType, devGroupType);
                //case MinerBaseType.mkxminer:
                    //return Data.mkxminer;
            }
            return Data.NONE;
        }

        public static string GetPathFor(ComputeDevice computeDevice, Algorithm algorithm /*, Options: MinerPathsConfig*/)
        {
            if (computeDevice == null || algorithm == null)
            {
                return Data.NONE;
            }

            return GetPathFor(
                algorithm.MinerBaseType,
                algorithm.NiceHashID,
                computeDevice.DeviceGroupType
                );
        }

        public static bool IsValidMinerPath(string minerPath)
        {
            // TODO make a list of valid miner paths and check that instead
            return minerPath != null && Data.NONE != minerPath && minerPath != "";
        }

        /**
         * InitAlgorithmsMinerPaths gets and sets miner paths
         */

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
        private static class NVIDIA_GROUPS
        {
            public static string Ccminer_sm21_or_sm3x(AlgorithmType algorithmType)
            {
                if (AlgorithmType.Decred == algorithmType)
                {
                    return Data.ccminer_decred;
                }
                if (AlgorithmType.CryptoNight == algorithmType)
                {
                    return Data.ccminer_cryptonight;
                }
                return Data.ccminer_tpruvot;
            }
            public static string Ccminer_sm5x(AlgorithmType algorithmType)
            {
                if (AlgorithmType.Decred == algorithmType)
                {
                    return Data.ccminer_decred;
                }
                if (AlgorithmType.Lyra2RE == algorithmType)
                {
                    return Data.ccminer_nanashi;
                }
                if (AlgorithmType.CryptoNight == algorithmType)
                {
                    return Data.ccminer_cryptonight;
                }
                if (AlgorithmType.NeoScrypt == algorithmType)
                {
                    return Data.ccminer_tpruvot;
                }
                if (AlgorithmType.Sia == algorithmType)
                {
                    return Data.ccminer_klaust;
                }
                if (AlgorithmType.Xevan == algorithmType)
                {
                    return Data.ccminer_xevan;
                }
                if (AlgorithmType.X17 == algorithmType
                     || AlgorithmType.Blake256r8 == algorithmType
                     || AlgorithmType.X11evo == algorithmType
                     || AlgorithmType.X11Gost == algorithmType)
                {
                    return Data.ccminer_palgin;
                }
                if (AlgorithmType.Hsr == algorithmType)
                {
                    return Data.ccminer_alexis_hsr;
                }
                if (AlgorithmType.Phi == algorithmType
                    || AlgorithmType.NeoScrypt == algorithmType
                    || AlgorithmType.Tribus == algorithmType
                    || AlgorithmType.Skunk == algorithmType)
                {
                    return Data.ccminer_tpruvot2;
                }
                if (AlgorithmType.X17 == algorithmType
                    //zpool removed veltor
                    //|| AlgorithmType.Veltor == algorithmType
                    || AlgorithmType.Lbry == algorithmType
                    || AlgorithmType.Lyra2REv2 == algorithmType
                    || AlgorithmType.Blake2s == algorithmType
                    || AlgorithmType.Nist5 == algorithmType
                    || AlgorithmType.Skein == algorithmType
                    || AlgorithmType.Keccak == algorithmType
                    || AlgorithmType.Polytimos == algorithmType)
                {
                    return Data.ccminer_polytimos;
                }
                if (AlgorithmType.Bitcore == algorithmType)
                {
                    return Data.ccminer_22;
                }
                if (AlgorithmType.Hsr == algorithmType
                    || AlgorithmType.Blake256r8 == algorithmType
                    || AlgorithmType.X11Gost == algorithmType
                    || AlgorithmType.C11 == algorithmType)
                {
                    return Data.ccminer_alexis78;
                }
                if (AlgorithmType.Timetravel == algorithmType)
                {
                    return Data.ccminer_skunkkrnlx;
                }

                return Data.ccminer_sp;
            }
            public static string Ccminer_sm6x(AlgorithmType algorithmType)
            {
                if (AlgorithmType.Decred == algorithmType)
                {
                    return Data.ccminer_decred;
                }
                if (AlgorithmType.Lyra2RE == algorithmType)
                {
                    return Data.ccminer_nanashi;
                }
                if (AlgorithmType.CryptoNight == algorithmType)
                {
                    return Data.ccminer_cryptonight;
                }
                if (AlgorithmType.NeoScrypt == algorithmType)
                {
                    return Data.ccminer_tpruvot;
                }
                if (AlgorithmType.Sia == algorithmType)
                {
                    return Data.ccminer_klaust;
                }
                if (AlgorithmType.Xevan == algorithmType)
                {
                    return Data.ccminer_xevan;
                }
                if (AlgorithmType.X17 == algorithmType
                     || AlgorithmType.Blake256r8 == algorithmType
                     || AlgorithmType.X11evo == algorithmType
                    || AlgorithmType.X11Gost == algorithmType)
                {
                    return Data.ccminer_palgin;
                }
                if (AlgorithmType.Hsr == algorithmType)
                {
                    return Data.ccminer_alexis_hsr;
                }
                if (AlgorithmType.Phi == algorithmType
                    || AlgorithmType.NeoScrypt == algorithmType
                    || AlgorithmType.Tribus == algorithmType
                    || AlgorithmType.Skunk == algorithmType)
                {
                    return Data.ccminer_tpruvot2;
                }
                if (AlgorithmType.X17 == algorithmType
                    //zpool removed veltor
                    //|| AlgorithmType.Veltor == algorithmType
                    || AlgorithmType.Lbry == algorithmType
                    || AlgorithmType.Lyra2REv2 == algorithmType
                    || AlgorithmType.Blake2s == algorithmType
                    || AlgorithmType.Nist5 == algorithmType
                    || AlgorithmType.Skein == algorithmType
                    || AlgorithmType.Keccak == algorithmType
                    || AlgorithmType.Polytimos == algorithmType)
                {
                    return Data.ccminer_polytimos;
                }
                if (AlgorithmType.Bitcore == algorithmType)
                {
                    return Data.ccminer_22;
                }
                if (AlgorithmType.Hsr == algorithmType
                    || AlgorithmType.Blake256r8 == algorithmType
                    || AlgorithmType.X11Gost == algorithmType
                    || AlgorithmType.C11 == algorithmType)
                {
                    return Data.ccminer_alexis78;
                }
                if (AlgorithmType.Timetravel == algorithmType)
                {
                    return Data.ccminer_skunkkrnlx;
                }

                return Data.ccminer_sp;
            }

            public static string Hsrneoscrypt_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
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
                    return Data.hsrneoscrypt;
                }
                
                // TODO wrong case?
                return Data.NONE; // should not happen
            }
            
            public static string hsrneoscrypt_hsr_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
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
                    return Data.hsrneoscrypt_hsr;
                }

                // TODO wrong case?
                return Data.NONE; // should not happen
            }
            public static string Ccminer_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm21 and sm3x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_2_1 || nvidiaGroup == DeviceGroupType.NVIDIA_3_x)
                {
                    return NVIDIA_GROUPS.Ccminer_sm21_or_sm3x(algorithmType);
                }
                // CN exception
                if (nvidiaGroup == DeviceGroupType.NVIDIA_6_x && algorithmType == AlgorithmType.CryptoNight  || AlgorithmType.Tribus == algorithmType)
                {
                    return Data.ccminer_tpruvot;
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
            public static string Ccminer_unstable_path(AlgorithmType algorithmType, DeviceGroupType nvidiaGroup)
            {
                // sm5x and sm6x have same settings
                if (nvidiaGroup == DeviceGroupType.NVIDIA_5_x || nvidiaGroup == DeviceGroupType.NVIDIA_6_x)
                {

                   if (AlgorithmType.X11Gost == algorithmType || AlgorithmType.Nist5 == algorithmType || AlgorithmType.X17 == algorithmType || AlgorithmType.Nist5 == algorithmType)
 
                   {
                     return Data.ccminer_x11gost;
                    }
                }
                // TODO wrong case?
                return Data.NONE; // should not happen
            }
        }

        private static class AMD_GROUP
        {
            public static string Sgminer_path(AlgorithmType type)
            {
                if (AlgorithmType.CryptoNight == type || AlgorithmType.DaggerHashimoto == type)
                {
                    return Data.sgminer_gm;
                }
                if (AlgorithmType.Skein == type)
                {
                    return Data.sgminer_Skein;
                }
                if (AlgorithmType.Bitcore == type)
                {
                    return Data.sgminer_Bitcore;
                }
                if (AlgorithmType.Hsr == type)
                {
                    return Data.sgminer_HSR;
                }
                if (AlgorithmType.Phi == type)
                {
                    return Data.sgminer_Phi;
                }
                if (AlgorithmType.Tribus == type || AlgorithmType.Veltor == type)
                {
                    return Data.sgminer_Tribus;
                }
                if (AlgorithmType.Xevan == type)
                {
                    return Data.sgminer_Xevan;
                }

                return Data.sgminer_5_6_0_general;
            }

            public static string Glg_path(AlgorithmType type)
            {
                // AlgorithmType.Pascal == type || AlgorithmType.DaggerHashimoto == type || AlgorithmType.Decred == type || AlgorithmType.Lbry == type || AlgorithmType.X11Gost == type || AlgorithmType.DaggerHashimoto == type
                if (AlgorithmType.CryptoNight == type || AlgorithmType.Equihash == type || AlgorithmType.NeoScrypt == type || AlgorithmType.Lyra2REv2 == type  || AlgorithmType.Myriad_groestl == type  || AlgorithmType.Keccak == type)
                {
                    return Data.glg;
                }
                return Data.NONE;
            }

            public static string ClaymorePath(AlgorithmType type)
            {
                if (AlgorithmType.Equihash == type)
                {
                    return Data.ClaymoreZcashMiner;
                }
                else if (AlgorithmType.CryptoNight == type)
                {
                    return Data.ClaymoreCryptoNightMiner;
                }
                else if (AlgorithmType.DaggerHashimoto == type)
                {
                    return Data.ClaymoreDual;
                }
                return Data.NONE; // should not happen
            }
        }

        // unstable miners, NVIDIA for now
        private static class EXPERIMENTAL
        {
            public static string GetPath(AlgorithmType algoType, DeviceGroupType devGroupType)
            {
                if (devGroupType == DeviceGroupType.NVIDIA_6_x)
                {
                    return NVIDIA_GROUPS.Ccminer_path(algoType, devGroupType);
                }
                return Data.NONE; // should not happen
            }
        }

        private static List<MinerPathPackage> minerPathPackages = new List<MinerPathPackage>();

        private static readonly List<MinerBaseType> configurableMiners = new List<MinerBaseType> {
            MinerBaseType.ccminer,
            MinerBaseType.sgminer
        };

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
                            minerPaths.Add(new MinerPath(algo.NiceHashID, GetPathFor(type, algo.NiceHashID, i, true)));
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
