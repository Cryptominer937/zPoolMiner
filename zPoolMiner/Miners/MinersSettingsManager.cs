namespace zPoolMiner.Miners
{
    using System.Collections.Generic;
    using zPoolMiner.Configs.ConfigJsonFile;
    using zPoolMiner.Devices;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Grouping;
    using zPoolMiner.Miners.Parsing;

    /// <summary>
    /// Defines the <see cref="MinersSettingsManager" />
    /// </summary>
    public static class MinersSettingsManager
    {
        /// <summary>
        /// Defines the <see cref="MinerReservedPortsFile" />
        /// </summary>
        private class MinerReservedPortsFile : ConfigFile<Dictionary<MinerBaseType, Dictionary<string, Dictionary<AlgorithmType, List<int>>>>>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MinerReservedPortsFile"/> class.
            /// </summary>
            public MinerReservedPortsFile()
                : base(FOLDERS.CONFIG, "MinerReservedPorts.json", "MinerReservedPorts_old.json")
            {
            }
        }

        // {miner path : {envName : envValue} }
        /// <summary>
        /// Defines the <see cref="MinerSystemVariablesFile" />
        /// </summary>
        private class MinerSystemVariablesFile : ConfigFile<Dictionary<string, Dictionary<string, string>>>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MinerSystemVariablesFile"/> class.
            /// </summary>
            public MinerSystemVariablesFile() : base(FOLDERS.CONFIG, "MinerSystemVariables.json", "MinerSystemVariables_old.json")
            {
            }
        }

        /// <summary>
        /// Defines the MinerReservedPorts
        /// </summary>
        private static Dictionary<MinerBaseType,
            Dictionary<string,
                Dictionary<AlgorithmType,
                    List<int>>>> MinerReservedPorts = new Dictionary<MinerBaseType, Dictionary<string, Dictionary<AlgorithmType, List<int>>>>();

        /// <summary>
        /// Defines the AllReservedPorts
        /// </summary>
        public static List<int> AllReservedPorts = new List<int>();

        /// <summary>
        /// Defines the MinerSystemVariables
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> MinerSystemVariables = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// The Init
        /// </summary>
        public static void Init()
        {
            ExtraLaunchParameters.InitializePackages();
            MinerPaths.InitializePackages();
            InitMinerReservedPortsFile();
            InitMinerSystemVariablesFile();
        }

        /// <summary>
        /// The GetPortsListFor
        /// </summary>
        /// <param name="minerBaseType">The <see cref="MinerBaseType"/></param>
        /// <param name="path">The <see cref="string"/></param>
        /// <param name="algorithmType">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="List{int}"/></returns>
        public static List<int> GetPortsListFor(MinerBaseType minerBaseType, string path, AlgorithmType algorithmType)
        {
            if (MinerReservedPorts != null && MinerReservedPorts.ContainsKey(minerBaseType))
            {
                if (MinerReservedPorts[minerBaseType] != null && MinerReservedPorts[minerBaseType].ContainsKey(path))
                {
                    if (MinerReservedPorts[minerBaseType][path] != null && MinerReservedPorts[minerBaseType][path].ContainsKey(algorithmType))
                    {
                        if (MinerReservedPorts[minerBaseType][path][algorithmType] != null)
                        {
                            return MinerReservedPorts[minerBaseType][path][algorithmType];
                        }
                    }
                }
            }

            return new List<int>();
        }

        /// <summary>
        /// The InitMinerReservedPortsFile
        /// </summary>
        public static void InitMinerReservedPortsFile()
        {
            var file = new MinerReservedPortsFile();
            MinerReservedPorts = new Dictionary<MinerBaseType, Dictionary<string, Dictionary<AlgorithmType, List<int>>>>();

            if (file.IsFileExists())
            {
                var read = file.ReadFile();
                if (read != null) MinerReservedPorts = read;
            }

            try
            {
                for (MinerBaseType type = (MinerBaseType.NONE + 1); type < MinerBaseType.END; ++type)
                {
                    if (MinerReservedPorts.ContainsKey(type) == false)
                    {
                        MinerReservedPorts[type] = new Dictionary<string, Dictionary<AlgorithmType, List<int>>>();
                    }
                }

                for (DeviceGroupType devGroupType = (DeviceGroupType.NONE + 1); devGroupType < DeviceGroupType.LAST; ++devGroupType)
                {
                    var minerAlgosForGroup = GroupAlgorithms.CreateDefaultsForGroup(devGroupType);

                    if (minerAlgosForGroup != null)
                    {
                        foreach (var mbaseKvp in minerAlgosForGroup)
                        {
                            var minerBaseType = mbaseKvp.Key;

                            if (MinerReservedPorts.ContainsKey(minerBaseType))
                            {
                                var algos = mbaseKvp.Value;

                                foreach (var algo in algos)
                                {
                                    var algoType = algo.CryptoMiner937ID;
                                    var path = MinerPaths.GetPathFor(minerBaseType, algoType, devGroupType, 0);
                                    var isPathValid = path != MinerPaths.Data.NONE;

                                    if (isPathValid && MinerReservedPorts[minerBaseType].ContainsKey(path) == false)
                                    {
                                        MinerReservedPorts[minerBaseType][path] = new Dictionary<AlgorithmType, List<int>>();
                                    }

                                    if (isPathValid && MinerReservedPorts[minerBaseType][path] != null && MinerReservedPorts[minerBaseType][path].ContainsKey(algoType) == false)
                                    {
                                        MinerReservedPorts[minerBaseType][path][algoType] = new List<int>();
                                    }
                                }
                            }
                        }
                    }
                }

                file.Commit(MinerReservedPorts);
                // set all reserved
                foreach (var paths in MinerReservedPorts.Values)
                {
                    foreach (var algos in paths.Values)
                    {
                        foreach (var ports in algos.Values)
                        {
                            foreach (int port in ports)
                                AllReservedPorts.Add(port);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// The InitMinerSystemVariablesFile
        /// </summary>
        public static void InitMinerSystemVariablesFile()
        {
            var file = new MinerSystemVariablesFile();
            MinerSystemVariables = new Dictionary<string, Dictionary<string, string>>();
            var isFileInit = false;

            if (file.IsFileExists())
            {
                var read = file.ReadFile();

                if (read != null)
                {
                    isFileInit = true;
                    MinerSystemVariables = read;
                }
            }

            if (!isFileInit)
            {
                // general AMD defaults scope
                {
                    var minerPaths = new List<string>()
                    {
                        //MinerPaths.Data.sgminer_5_6_0_general,
                        //MinerPaths.Data.sgminer_gm,
                        //MinerPaths.Data.sgminer_HSR,
                        //MinerPaths.Data.sgminer_Bitcore,
                        ////MinerPaths.Data.sgminer_Phi,
                        //MinerPaths.Data.sgminer_Skein,
                        //MinerPaths.Data.sgminer_Tribus,
                        //MinerPaths.Data.sgminer_Xevan,
                        //MinerPaths.Data.sgminer_aceneun,
                        //MinerPaths.Data.glg,
                        //MinerPaths.Data.ClaymorecryptonightMiner,
                        //MinerPaths.Data.ClaymoreZcashMiner,
                        //MinerPaths.Data.ClaymoreNeoscryptMiner,
                        //MinerPaths.Data.OptiminerZcashMiner
                    };

                    foreach (var minerPath in minerPaths)
                    {
                        MinerSystemVariables[minerPath] = new Dictionary<string, string>() {
                            { "GPU_MAX_ALLOC_PERCENT",      "100" },
                            { "GPU_USE_SYNC_OBJECTS",       "1" },
                            { "GPU_SINGLE_ALLOC_PERCENT",   "100" },
                            { "GPU_MAX_HEAP_SIZE",          "100" },
                            { "GPU_FORCE_64BIT_PTR",        "0" }
                        };
                    }
                }
                // ClaymoreDual scope
                /*{
                    MinerSystemVariables[MinerPaths.Data.ClaymoreDual] = new Dictionary<string, string>() {
                        { "GPU_MAX_ALLOC_PERCENT",      "100" },
                        { "GPU_USE_SYNC_OBJECTS",       "1" },
                        { "GPU_SINGLE_ALLOC_PERCENT",   "100" },
                        { "GPU_MAX_HEAP_SIZE",          "100" },
                        { "GPU_FORCE_64BIT_PTR",        "0" }
                    };
                }*/
                // save defaults
                file.Commit(MinerSystemVariables);
            }
        }
    }
}