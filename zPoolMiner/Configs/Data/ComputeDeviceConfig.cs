using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Configs.Data {
    [Serializable]
    public class ComputeDeviceConfig {
        public string Name = "";
        public bool Enabled = true;
        public string UUID = "";
    }
}
