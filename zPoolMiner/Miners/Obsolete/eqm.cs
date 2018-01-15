using zPoolMiner.Enums;
using zPoolMiner.Miners.Parsing;

namespace zPoolMiner.Miners
{
    public class Eqm : NheqBase
    {
        public Eqm()
            : base("eqm")
        {
            ConectionType = NHMConectionType.LOCKED;
            IsNeverHideMiningWindow = true;
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            LastCommandLine = GetDevicesCommandString() + " -a " + APIPort + " -l " + url + " -u " + btcAdress + " -w " + worker;
            ProcessHandle = _Start();
        }

        protected override string GetDevicesCommandString()
        {
            string deviceStringCommand = " ";

            if (CPU_Setup.IsInit)
            {
                deviceStringCommand += "-p " + CPU_Setup.MiningPairs.Count;
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(CPU_Setup, DeviceType.CPU);
            }
            else
            {
                // disable CPU
                deviceStringCommand += " -t 0 ";
            }

            if (NVIDIA_Setup.IsInit)
            {
                deviceStringCommand += " -cd ";
                foreach (var nvidia_pair in NVIDIA_Setup.MiningPairs)
                {
                    if (nvidia_pair.CurrentExtraLaunchParameters.Contains("-ct"))
                    {
                        for (int i = 0; i < ExtraLaunchParametersParser.GetEqmCudaThreadCount(nvidia_pair); ++i)
                        {
                            deviceStringCommand += nvidia_pair.Device.ID + " ";
                        }
                    }
                    else
                    { // use default 2 best performance
                        for (int i = 0; i < 2; ++i)
                        {
                            deviceStringCommand += nvidia_pair.Device.ID + " ";
                        }
                    }
                }
                // no extra launch params
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(NVIDIA_Setup, DeviceType.NVIDIA);
            }

            return deviceStringCommand;
        }

        // benchmark stuff
        private const string TOTAL_MES = "Total measured:";

        protected override bool BenchmarkParseLine(string outdata)
        {
            if (outdata.Contains(TOTAL_MES) && outdata.Contains(Iter_PER_SEC))
            {
                curSpeed = GetNumber(outdata, TOTAL_MES, Iter_PER_SEC) * SolMultFactor;
            }
            if (outdata.Contains(TOTAL_MES) && outdata.Contains(Sols_PER_SEC))
            {
                var sols = GetNumber(outdata, TOTAL_MES, Sols_PER_SEC);
                if (sols > 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = curSpeed;
                    return true;
                }
            }
            return false;
        }
    }
}