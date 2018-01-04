using zPoolMiner.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Configs.Data {
    [Serializable]
    public class DeviceBenchmarkConfig {
        public string DeviceUUID = "";
        public string DeviceName = "";
        //public int TimeLimit { get; set; }
        public List<AlgorithmConfig> AlgorithmSettings = new List<AlgorithmConfig>();
    }
}
