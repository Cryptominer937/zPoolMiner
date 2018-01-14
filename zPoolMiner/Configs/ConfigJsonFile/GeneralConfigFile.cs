using zPoolMiner.Configs.Data;

namespace zPoolMiner.Configs.ConfigJsonFile
{
    public class GeneralConfigFile : ConfigFile<GeneralConfig>
    {
        public GeneralConfigFile()
            : base(FOLDERS.CONFIG, "General.json", "General_old.json")
        {
        }
    }
}