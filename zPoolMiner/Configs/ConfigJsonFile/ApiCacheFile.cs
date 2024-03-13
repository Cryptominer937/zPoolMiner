using zPoolMiner.Configs.Data;

namespace zPoolMiner.Configs.ConfigJsonFile
{
    public class ApiCacheFile : ConfigFile<ApiCache>
    {
        public ApiCacheFile()
            : base(FOLDERS.CONFIG, "ApiCache.json", "ApiCache_old.json")
        {
        }        
    }
}
