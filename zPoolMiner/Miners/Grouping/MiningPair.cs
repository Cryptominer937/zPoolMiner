using zPoolMiner.Devices;

namespace zPoolMiner.Miners.Grouping
{
    public class MiningPair
    {
        public readonly ComputeDevice Device;
        public readonly Algorithm Algorithm;
        public string CurrentExtraLaunchParameters;

        public MiningPair(ComputeDevice d, Algorithm a)
        {
            this.Device = d;
            this.Algorithm = a;
            this.CurrentExtraLaunchParameters = Algorithm.ExtraLaunchParameters;
        }
    }
}