using System;
using System.Collections.Generic;
using zPoolMiner.Enums;

namespace zPoolMiner.Configs.Data
{
    [Serializable]
    public class ApiCache
    {   /// <summary>
        /// Defines the ConfigFileVersion
        /// </summary>
        public Version ConfigFileVersionapi;

        // SMA data        /// <summary>
        /// Defines the CryptoMiner937Data
        /// </summary>
        public Dictionary<AlgorithmType, CryptoMiner937API> CryptoMiner937Data;

        /// <summary>
        /// Defines the CryptoMiner937DataTimeStamp
        /// </summary>
        public DateTime CryptoMiner937DataTimeStamp = DateTime.MinValue; // device enabled disabled stuff

        // device enabled disabled stuff        /// <summary>
        /// Defines the LastDevicesSettup
        /// </summary>
        public List<ComputeDeviceConfig> LastDevicesSettup = new List<ComputeDeviceConfig>();

        // methods
        /// <summary>
        /// The SetDefaults
        /// </summary>
        public void SetDefaults()
        {
            ConfigFileVersionapi = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        public void FixSettingBounds()
        {
            ConfigFileVersionapi = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}