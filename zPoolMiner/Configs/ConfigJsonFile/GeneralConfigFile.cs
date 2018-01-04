using zPoolMiner.Configs.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Configs.ConfigJsonFile {
    public class GeneralConfigFile : ConfigFile<GeneralConfig> {
        public GeneralConfigFile()
            : base(FOLDERS.CONFIG, "General.json", "General_old.json") {
        }
    }
}
