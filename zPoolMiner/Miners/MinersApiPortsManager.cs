using System.Collections.Generic;
using System.Net.NetworkInformation;
using zPoolMiner.Configs;

namespace zPoolMiner.Miners
{
    public static class MinersApiPortsManager
    {
        private static HashSet<int> _usedPorts = new HashSet<int>();

        public static bool IsPortAvaliable(int port)
        {
            var isAvailable = true;

            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            // check TCP
            {
                var tcpIpEndpoints = ipGlobalProperties.GetActiveTcpListeners();

                foreach (var tcp in tcpIpEndpoints)
                {
                    if (tcp.Port == port)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }

            // check UDP
            if (isAvailable)
            {
                var udpIpEndpoints = ipGlobalProperties.GetActiveUdpListeners();

                foreach (var udp in udpIpEndpoints)
                {
                    if (udp.Port == port)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }

            return isAvailable;
        }

        public static int GetAvaliablePort()
        {
            var port = ConfigManager.GeneralConfig.ApiBindPortPoolStart;
            var newPortEnd = port + 3000;

            for (; port < newPortEnd; ++port)
            {
                if (MinersSettingsManager.AllReservedPorts.Contains(port) == false && IsPortAvaliable(port) && _usedPorts.Add(port))
                {
                    break;
                }
            }

            return port;
        }

        public static void RemovePort(int port) => _usedPorts.Remove(port);
    }
}