namespace zPoolMiner
{
    using System.Collections.Generic;
    using zPoolMiner.Enums;

    // this class mirrors the web profitability, chech what https://www.nicehash.com/?p=calc is using for each algo
    /// <summary>
    /// Defines the <see cref="ProfitabilityCalculator" />
    /// </summary>
    internal static class ProfitabilityCalculator
    {
        /// <summary>
        /// Defines the kHs
        /// </summary>
        private const double kHs = 1000;

        /// <summary>
        /// Defines the MHs
        /// </summary>
        private const double MHs = 1000000;

        /// <summary>
        /// Defines the GHs
        /// </summary>
        private const double GHs = 1000000000;

        /// <summary>
        /// Defines the THs
        /// </summary>
        private const double THs = 1000000000000;

        // divide factor to mirror web values
        // divide factor to mirror web values        /// <summary>
        /// Defines the _div
        /// </summary>
        private static readonly Dictionary<AlgorithmType, double> _div = new Dictionary<AlgorithmType, double>()
        {
            { AlgorithmType.INVALID , 1 },
            { AlgorithmType.NONE , 1 },
            { AlgorithmType.Scrypt_UNUSED,                  MHs }, // NOT used
            { AlgorithmType.SHA256_UNUSED ,                 THs }, // NOT used
            { AlgorithmType.ScryptNf_UNUSED ,               MHs }, // NOT used
            { AlgorithmType.X11_UNUSED ,                    MHs }, // NOT used
            { AlgorithmType.X13_UNUSED ,                    MHs },
            { AlgorithmType.Skein ,                         MHs },
            //{ AlgorithmType.Tribus ,                        MHs },
            //{ AlgorithmType.X17 ,                           MHs },
            { AlgorithmType.Keccak ,                        GHs },
            { AlgorithmType.X15_UNUSED ,                    MHs },
            { AlgorithmType.Nist5 ,                         MHs },
            { AlgorithmType.NeoScrypt ,                     MHs },
            { AlgorithmType.Lyra2RE ,                       MHs },
            { AlgorithmType.WhirlpoolX_UNUSED ,             MHs },
            { AlgorithmType.Qubit_UNUSED ,                  MHs },
            { AlgorithmType.Quark_UNUSED ,                  MHs },
            { AlgorithmType.Axiom_UNUSED ,                  kHs }, // NOT used
            { AlgorithmType.Lyra2REv2 ,                     GHs },
            { AlgorithmType.ScryptJaneNf16_UNUSED ,         kHs }, // NOT used
            { AlgorithmType.Blake256r8 ,                    GHs },
            { AlgorithmType.Blake256r14_UNUSED ,            GHs },
            { AlgorithmType.Blake256r8vnl_UNUSED ,          GHs },
            { AlgorithmType.Hodl ,                          kHs },
            { AlgorithmType.DaggerHashimoto ,               MHs },
            { AlgorithmType.Decred ,                        GHs },
            { AlgorithmType.cryptonight ,                   kHs },
            { AlgorithmType.Lbry ,                          GHs },
            { AlgorithmType.equihash ,                      1 }, // Sols /s
            { AlgorithmType.Pascal ,                        GHs },
            { AlgorithmType.X11Gost ,                       MHs },
            { AlgorithmType.Sia ,                           GHs },
            { AlgorithmType.Blake2s ,                       GHs },
            { AlgorithmType.Skunk ,                         MHs },
            { AlgorithmType.karlsenhash,                        kHs },
            { AlgorithmType.pyrinhash,                        kHs },
            { AlgorithmType.ethash,                        MHs },
            { AlgorithmType.ethashb3,                        MHs }
        };

        /// <summary>
        /// The GetFormatedSpeed
        /// </summary>
        /// <param name="speed">The <see cref="double"/></param>
        /// <param name="type">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="double"/></returns>
        public static double GetFormatedSpeed(double speed, AlgorithmType type)
        {
            if (_div.ContainsKey(type))
            {
                return speed / _div[type];
            }

            return speed; // should never happen
        }
    }
}