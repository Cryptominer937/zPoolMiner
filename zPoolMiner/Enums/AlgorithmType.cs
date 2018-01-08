using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Enums
{
    /// <summary>
    /// AlgorithmType enum should/must mirror the values from https://www.nicehash.com/?p=api
    /// Some algorithms are not used anymore on the client, rename them with _UNUSED postfix so we can catch compile time errors if they are used.
    /// </summary>
    public enum AlgorithmType : int
    {
        // dual algos for grouping
        DaggerSia = -6,
        DaggerDecred = -5,
        DaggerLbry = -4,
        DaggerPascal = -3,
        INVALID = -2,
        NONE = -1,
        #region NiceHashAPI

        Hodl = -14,
        Decred = -13,
        CryptoNight = -12,
        Pascal = -11,
        Sia = -10,
        Blake256r14_UNUSED = -9,
        Blake256r8vnl_UNUSED = -8,
        ScryptNf_UNUSED = -7,
        WhirlpoolX_UNUSED = -6,
        Axiom_UNUSED = -5,
        ScryptJaneNf16_UNUSED = -4,
        Lyra2RE = -3,
        X15_UNUSED = -2,
        DaggerHashimoto = -1,
        Bitcore = 0,
        Blake2s = 1,
        Blake256r8_UNUSED = 2,
        Equihash = 4,
        Groestl = 5,
        Hsr = 6,
        Keccak = 7,
        Lbry = 8,
        Lyra2REv2 = 9,
        Myriad_groestl = 10,
        NeoScrypt = 11,
        Nist5 = 12,
        Phi = 13,
        Polytimos = 14,
        Quark_UNUSED = 15,
        Qubit_UNUSED = 16,
        Scrypt_UNUSED = 17,
        SHA256_UNUSED = 18,
        X11Gost = 19,
        Skein = 20,
        Skunk = 21,
        Timetravel = 22,
        Tribus = 23,
        Veltor = 24,
        X11_UNUSED = 25,
        X11evo = 26,
        X13_UNUSED = 27,
        X14 = 28,
        X17 = 29,
        Xevan = 30,
        Yescrypt = 31
        #endregion // NiceHashAPI
    }
}
