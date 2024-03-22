namespace zPoolMiner.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using zPoolMiner.Configs;
    using zPoolMiner.Configs.Data;
    using zPoolMiner.Enums;
    using zPoolMiner.Miners.Grouping;

    /// <summary>
    /// Defines the <see cref="ComputeDevice" />
    /// </summary>
    public class ComputeDevice
    {
        /// <summary>
        /// Defines the ID
        /// </summary>
        readonly public int ID;

        /// <summary>
        /// Gets or sets the Index
        /// </summary>
        public int Index { get; protected set; }

        // to identify equality;
        // to identify equality;        /// <summary>
        /// Defines the Name
        /// </summary>
        readonly public string Name;// { get; set; }

        // name count is the short name for displaying in moning groups
        // name count is the short name for displaying in moning groups        /// <summary>
        /// Defines the NameCount
        /// </summary>
        readonly public string NameCount;

        /// <summary>
        /// Defines the Enabled
        /// </summary>
        public bool Enabled;

        /// <summary>
        /// Defines the DeviceGroupType
        /// </summary>
        readonly public DeviceGroupType DeviceGroupType;

        // CPU, NVIDIA, AMD
        // CPU, NVIDIA, AMD        /// <summary>
        /// Defines the DeviceType
        /// </summary>
        readonly public DeviceType DeviceType;

        // UUID now used for saving

        // UUID now used for saving
        /// <summary>
        /// Gets or sets the UUID
        /// </summary>
        public string UUID { get; protected set; }

        // used for Claymore indexing

        // used for Claymore indexing
        /// <summary>
        /// Gets or sets the BusID
        /// </summary>
        public int BusID { get; protected set; } = -1;
        // used for lolMiner indexing
        public double lolMinerBusID { get; set; } = -1;

        /// <summary>
        /// Defines the IDByBus
        /// </summary>
        public int IDByBus = -1;

        // CPU extras

        // CPU extras
        /// <summary>
        /// Gets or sets the Threads
        /// </summary>
        public int Threads { get; protected set; }

        /// <summary>
        /// Gets or sets the AffinityMask
        /// </summary>
        public ulong AffinityMask { get; protected set; }

        // GPU extras
        // GPU extras        /// <summary>
        /// Defines the GpuRam
        /// </summary>
        public readonly ulong GpuRam;

        /// <summary>
        /// Defines the IsEtherumCapale
        /// </summary>
        public readonly bool IsEtherumCapale;

        /// <summary>
        /// Defines the MEMORY_3GB
        /// </summary>
        public static readonly ulong MEMORY_3GB = 3221225472;

        // sgminer extra quickfix
        //public readonly bool IsOptimizedVersion;

        // sgminer extra quickfix
        //public readonly bool IsOptimizedVersion;
        /// <summary>
        /// Gets or sets the Codename
        /// </summary>
        public string Codename { get; protected set; }

        /// <summary>
        /// Gets or sets the InfSection
        /// </summary>
        public string InfSection { get; protected set; }

        // amd has some algos not working with new drivers

        // amd has some algos not working with new drivers
        /// <summary>
        /// Gets or sets a value indicating whether DriverDisableAlgos
        /// </summary>
        public bool DriverDisableAlgos { get; protected set; }

        /// <summary>
        /// Defines the AlgorithmSettings
        /// </summary>
        protected List<Algorithm> AlgorithmSettings;

        /// <summary>
        /// Gets or sets the BenchmarkCopyUUID
        /// </summary>
        public string BenchmarkCopyUUID { get; set; }

        /// <summary>
        /// Gets the Load
        /// </summary>

        public virtual float Load => -1;
        public virtual float Temp => -1;
        public virtual int FanSpeed => -1;
        public virtual double PowerUsage => -1;

        // Ambiguous constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeDevice"/> class.
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <param name="name">The <see cref="string"/></param>
        /// <param name="enabled">The <see cref="bool"/></param>
        /// <param name="group">The <see cref="DeviceGroupType"/></param>
        /// <param name="ethereumCapable">The <see cref="bool"/></param>
        /// <param name="type">The <see cref="DeviceType"/></param>
        /// <param name="nameCount">The <see cref="string"/></param>
        /// <param name="gpuRAM">The <see cref="ulong"/></param>
        protected ComputeDevice(int id, string name, bool enabled, DeviceGroupType group, bool ethereumCapable, DeviceType type, string nameCount, ulong gpuRAM)
        {
            ID = id;
            Name = name;
            Enabled = enabled;
            DeviceGroupType = group;
            IsEtherumCapale = ethereumCapable;
            DeviceType = type;
            NameCount = nameCount;
            GpuRam = gpuRAM;
        }

        // Fake dev
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeDevice"/> class.
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        public ComputeDevice(int id)
        {
            ID = id;
            Name = "fake_" + id;
            NameCount = Name;
            Enabled = true;
            DeviceType = DeviceType.CPU;
            DeviceGroupType = DeviceGroupType.NONE;
            IsEtherumCapale = false;
            //IsOptimizedVersion = false;
            Codename = "fake";
            UUID = GetUUID(ID, GroupNames.GetGroupName(DeviceGroupType, ID), Name, DeviceGroupType);
            GpuRam = 0;
        }

        // CPU
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeDevice"/> class.
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <param name="group">The <see cref="string"/></param>
        /// <param name="name">The <see cref="string"/></param>
        /// <param name="threads">The <see cref="int"/></param>
        /// <param name="affinityMask">The <see cref="ulong"/></param>
        /// <param name="CPUCount">The <see cref="int"/></param>
        public ComputeDevice(int id, string group, string name, int threads, ulong affinityMask, int CPUCount)
        {
            ID = id;
            Name = name;
            Threads = threads;
            AffinityMask = affinityMask;
            Enabled = true;
            DeviceGroupType = DeviceGroupType.CPU;
            DeviceType = DeviceType.CPU;
            NameCount = String.Format(International.GetText("ComputeDevice_Short_Name_CPU"), CPUCount);
            UUID = GetUUID(ID, GroupNames.GetGroupName(DeviceGroupType, ID), Name, DeviceGroupType);
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            IsEtherumCapale = false;
            GpuRam = 0;
        }

        // GPU NVIDIA
        // GPU NVIDIA        /// <summary>
        /// Defines the _SM_major
        /// </summary>
        protected int _SM_major = -1;

        /// <summary>
        /// Defines the _SM_minor
        /// </summary>
        protected int _SM_minor = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeDevice"/> class.
        /// </summary>
        /// <param name="cudaDevice">The <see cref="CudaDevice"/></param>
        /// <param name="group">The <see cref="DeviceGroupType"/></param>
        /// <param name="GPUCount">The <see cref="int"/></param>
        public ComputeDevice(CudaDevice cudaDevice, DeviceGroupType group, int GPUCount)
        {
            _SM_major = cudaDevice.SM_major;
            _SM_minor = cudaDevice.SM_minor;
            ID = (int)cudaDevice.DeviceID;
            Name = cudaDevice.GetName();
            Enabled = true;
            DeviceGroupType = group;
            IsEtherumCapale = cudaDevice.IsEtherumCapable();
            DeviceType = DeviceType.NVIDIA;
            NameCount = String.Format(International.GetText("ComputeDevice_Short_Name_NVIDIA_GPU"), GPUCount);
            UUID = cudaDevice.UUID;
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            GpuRam = cudaDevice.DeviceGlobalMemory;
        }

        /// <summary>
        /// The IsSM50
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsSM50()
        {
            return _SM_major == 5 && _SM_minor == 0;
        }

        // GPU AMD
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeDevice"/> class.
        /// </summary>
        /// <param name="amdDevice">The <see cref="AmdGpuDevice"/></param>
        /// <param name="GPUCount">The <see cref="int"/></param>
        /// <param name="isDetectionFallback">The <see cref="bool"/></param>
        public ComputeDevice(AmdGpuDevice amdDevice, int GPUCount, bool isDetectionFallback)
        {
            ID = amdDevice.DeviceID;
            BusID = amdDevice.BusID;
            DeviceGroupType = DeviceGroupType.AMD_OpenCL;
            Name = amdDevice.DeviceName;
            Enabled = true;
            IsEtherumCapale = amdDevice.IsEtherumCapable();
            DeviceType = DeviceType.AMD;
            NameCount = String.Format(International.GetText("ComputeDevice_Short_Name_AMD_GPU"), GPUCount);
            if (isDetectionFallback)
            {
                UUID = GetUUID(ID, GroupNames.GetGroupName(DeviceGroupType, ID), Name, DeviceGroupType);
            }
            else
            {
                UUID = amdDevice.UUID;
            }
            // sgminer extra
            //IsOptimizedVersion = amdDevice.UseOptimizedVersion;
            Codename = amdDevice.Codename;
            InfSection = amdDevice.InfSection;
            AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
            DriverDisableAlgos = amdDevice.DriverDisableAlgos;
            GpuRam = amdDevice.DeviceGlobalMemory;
        }

        // combines long and short name
        /// <summary>
        /// The GetFullName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetFullName()
        {
            return String.Format(International.GetText("ComputeDevice_Full_Device_Name"), NameCount, Name);
        }

        /// <summary>
        /// The GetAlgorithm
        /// </summary>
        /// <param name="MinerBaseType">The <see cref="MinerBaseType"/></param>
        /// <param name="AlgorithmType">The <see cref="AlgorithmType"/></param>
        /// <param name="SecondaryAlgorithmType">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="Algorithm"/></returns>
        public Algorithm GetAlgorithm(MinerBaseType MinerBaseType, AlgorithmType AlgorithmType, AlgorithmType SecondaryAlgorithmType)
        {
            int toSetIndex = AlgorithmSettings.FindIndex((a) => a.CryptoMiner937ID == AlgorithmType && a.MinerBaseType == MinerBaseType && a.SecondaryCryptoMiner937ID == SecondaryAlgorithmType);
            if (toSetIndex > -1)
            {
                return AlgorithmSettings[toSetIndex];
            }
            return null;
        }

        //public Algorithm GetAlgorithm(string algoID) {
        //    int toSetIndex = this.AlgorithmSettings.FindIndex((a) => a.AlgorithmStringID == algoID);
        //    if (toSetIndex > -1) {
        //        return this.AlgorithmSettings[toSetIndex];
        //    }
        //    return null;
        //}
        /// <summary>
        /// The CopyBenchmarkSettingsFrom
        /// </summary>
        /// <param name="copyBenchCDev">The <see cref="ComputeDevice"/></param>
        public void CopyBenchmarkSettingsFrom(ComputeDevice copyBenchCDev)
        {
            foreach (var copyFromAlgo in copyBenchCDev.AlgorithmSettings)
            {
                var setAlgo = GetAlgorithm(copyFromAlgo.MinerBaseType, copyFromAlgo.CryptoMiner937ID, copyFromAlgo.SecondaryCryptoMiner937ID);
                if (setAlgo != null)
                {
                    setAlgo.BenchmarkSpeed = copyFromAlgo.BenchmarkSpeed;
                    setAlgo.SecondaryBenchmarkSpeed = copyFromAlgo.SecondaryBenchmarkSpeed;
                    setAlgo.ExtraLaunchParameters = copyFromAlgo.ExtraLaunchParameters;
                    setAlgo.LessThreads = copyFromAlgo.LessThreads;
                }
            }
        }

        // settings
        // setters
        /// <summary>
        /// The SetFromComputeDeviceConfig
        /// </summary>
        /// <param name="config">The <see cref="ComputeDeviceConfig"/></param>
        public void SetFromComputeDeviceConfig(ComputeDeviceConfig config)
        {
            if (config != null && config.UUID == UUID)
            {
                Enabled = config.Enabled;
            }
        }

        /// <summary>
        /// The SetAlgorithmDeviceConfig
        /// </summary>
        /// <param name="config">The <see cref="DeviceBenchmarkConfig"/></param>
        public void SetAlgorithmDeviceConfig(DeviceBenchmarkConfig config)
        {
            if (config != null && config.DeviceUUID == UUID && config.AlgorithmSettings != null)
            {
                AlgorithmSettings = GroupAlgorithms.CreateForDeviceList(this);
                foreach (var conf in config.AlgorithmSettings)
                {
                    var setAlgo = GetAlgorithm(conf.MinerBaseType, conf.CryptoMiner937ID, conf.SecondaryCryptoMiner937ID);
                    if (setAlgo != null)
                    {
                        setAlgo.BenchmarkSpeed = conf.BenchmarkSpeed;
                        setAlgo.SecondaryBenchmarkSpeed = conf.SecondaryBenchmarkSpeed;
                        setAlgo.ExtraLaunchParameters = conf.ExtraLaunchParameters;
                        setAlgo.Enabled = conf.Enabled;
                        setAlgo.LessThreads = conf.LessThreads;
                    }
                }
            }
        }

        // getters
        /// <summary>
        /// The GetComputeDeviceConfig
        /// </summary>
        /// <returns>The <see cref="ComputeDeviceConfig"/></returns>
        public ComputeDeviceConfig GetComputeDeviceConfig()
        {
            ComputeDeviceConfig ret = new ComputeDeviceConfig
            {
                Enabled = Enabled,
                Name = Name,
                UUID = UUID
            };
            return ret;
        }

        /// <summary>
        /// The GetAlgorithmDeviceConfig
        /// </summary>
        /// <returns>The <see cref="DeviceBenchmarkConfig"/></returns>
        public DeviceBenchmarkConfig GetAlgorithmDeviceConfig()
        {
            DeviceBenchmarkConfig ret = new DeviceBenchmarkConfig
            {
                DeviceName = Name,
                DeviceUUID = UUID
            };
            // init algo settings
            foreach (var algo in AlgorithmSettings)
            {
                // create/setup
                AlgorithmConfig conf = new AlgorithmConfig
                {
                    Name = algo.AlgorithmStringID,
                    CryptoMiner937ID = algo.CryptoMiner937ID,
                    SecondaryCryptoMiner937ID = algo.SecondaryCryptoMiner937ID,
                    MinerBaseType = algo.MinerBaseType,
                    MinerName = algo.MinerName, // TODO probably not needed
                    BenchmarkSpeed = algo.BenchmarkSpeed,
                    SecondaryBenchmarkSpeed = algo.SecondaryBenchmarkSpeed,
                    ExtraLaunchParameters = algo.ExtraLaunchParameters,
                    Enabled = algo.Enabled,
                    LessThreads = algo.LessThreads
                };
                // insert
                ret.AlgorithmSettings.Add(conf);
            }
            return ret;
        }

        /// <summary>
        /// The GetAlgorithmSettings
        /// </summary>
        /// <returns>The <see cref="List{Algorithm}"/></returns>
        public List<Algorithm> GetAlgorithmSettings()
        {
            // hello state
            var algos = GetAlgorithmSettingsThirdParty(ConfigManager.GeneralConfig.Use3rdPartyMiners);

            var retAlgos = MinerPaths.GetAndInitAlgorithmsMinerPaths(algos, this); ;

            // NVIDIA
            if (DeviceGroupType == DeviceGroupType.NVIDIA_5_x || DeviceGroupType == DeviceGroupType.NVIDIA_6_x)
            {

            }
            else if (DeviceType == DeviceType.NVIDIA)
            {

            }

            // sort by algo
            retAlgos.Sort((a_1, a_2) => (a_1.CryptoMiner937ID - a_2.CryptoMiner937ID) != 0 ? (a_1.CryptoMiner937ID - a_2.CryptoMiner937ID) : (a_1.MinerBaseType - a_2.MinerBaseType));

            return retAlgos;
        }

        /// <summary>
        /// The GetAlgorithmSettingsFastest
        /// </summary>
        /// <returns>The <see cref="List{Algorithm}"/></returns>
        public List<Algorithm> GetAlgorithmSettingsFastest()
        {
            // hello state
            var algosTmp = GetAlgorithmSettings();
            Dictionary<AlgorithmType, Algorithm> sortDict = new Dictionary<AlgorithmType, Algorithm>();
            foreach (var algo in algosTmp)
            {
                var algoKey = algo.CryptoMiner937ID;
                if (sortDict.ContainsKey(algoKey))
                {
                    if (sortDict[algoKey].BenchmarkSpeed < algo.BenchmarkSpeed)
                    {
                        sortDict[algoKey] = algo;
                    }
                }
                else
                {
                    sortDict[algoKey] = algo;
                }
            }
            List<Algorithm> retAlgos = new List<Algorithm>();
            foreach (var fastestAlgo in sortDict.Values)
            {
                retAlgos.Add(fastestAlgo);
            }

            return retAlgos;
        }

        /// <summary>
        /// The GetAlgorithmSettingsThirdParty
        /// </summary>
        /// <param name="use3rdParty">The <see cref="Use3rdPartyMiners"/></param>
        /// <returns>The <see cref="List{Algorithm}"/></returns>
        private List<Algorithm> GetAlgorithmSettingsThirdParty(Use3rdPartyMiners use3rdParty)
        {
            if (use3rdParty == Use3rdPartyMiners.YES)
            {
                return AlgorithmSettings;
            }
            var third_party_miners = new List<MinerBaseType>() { };

            return AlgorithmSettings.FindAll((a) => third_party_miners.IndexOf(a.MinerBaseType) == -1);
        }

        // static methods
        /// <summary>
        /// The GetUUID
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <param name="group">The <see cref="string"/></param>
        /// <param name="name">The <see cref="string"/></param>
        /// <param name="deviceGroupType">The <see cref="DeviceGroupType"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected static string GetUUID(int id, string group, string name, DeviceGroupType deviceGroupType)
        {
            var SHA256 = new SHA256Managed();
            var hash = new StringBuilder();
            string mixedAttr = id.ToString() + group + name + ((int)deviceGroupType).ToString();
            byte[] hashedBytes = SHA256.ComputeHash(Encoding.UTF8.GetBytes(mixedAttr), 0, Encoding.UTF8.GetByteCount(mixedAttr));
            foreach (var b in hashedBytes)
            {
                hash.Append(b.ToString("x2"));
            }
            // GEN indicates the UUID has been generated and cannot be presumed to be immutable
            return "GEN-" + hash.ToString();
        }

        /// <summary>
        /// The IsAlgorithmSettingsInitialized
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        internal bool IsAlgorithmSettingsInitialized()
        {
            return AlgorithmSettings != null;
        }
    }
}
