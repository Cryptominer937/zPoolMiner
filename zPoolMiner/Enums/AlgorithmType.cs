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
        //Scrypt_UNUSED = 0,
        //SHA256_UNUSED = 1,
        //ScryptNf_UNUSED = 2,
        //X11_UNUSED = 3,
        //X13 = 4,
        Keccak = 7,
        //X15 = 6,
        Nist5 = 12,
        //NeoScrypt = 8,
        //Lyra2RE = 9,
        //WhirlpoolX = 10,
        //Qubit = 11,
        //Quark = 12,
        //Axiom_UNUSED = 13,
        //Lyra2REv2 = 14,
        //ScryptJaneNf16_UNUSED = 15,
        //Blake256r8 = 16,
        //Blake256r14 = 17, // NOT USED ANYMORE?
        //Blake256r8vnl = 18,
        //Hodl = 19,
        //DaggerHashimoto = 20,
        //Decred = 21,
        //CryptoNight = 22,
        //Lbry = 23,
        //Equihash = 24,
        //Pascal = 25
        // UNUSED START
        Scrypt_UNUSED = 17,
        SHA256_UNUSED = 18,
        ScryptNf_UNUSED = -2,
        X11_UNUSED = 25,
        X13_UNUSED = 27,
        //Keccak_UNUSED = 5,
        X15_UNUSED = -6,
        //Nist5_UNUSED = 7,
        WhirlpoolX_UNUSED = -10,
        Qubit_UNUSED = 16,
        Quark_UNUSED = 15,
        Axiom_UNUSED = -13,
        ScryptJaneNf16_UNUSED = -15,
        Blake256r8_UNUSED = -2,
        Blake256r14_UNUSED = -17,
        Blake256r8vnl_UNUSED = -18,
        // UNUSED END
        NeoScrypt = 11,
        Lyra2RE = -5,
        Lyra2REv2 = 9,
        Tribus = 23,
        X17 = 29,
        Hodl = -9,
        DaggerHashimoto = 50,
        Decred = -6,
        CryptoNight = -3,
        Lbry = 8,
        Equihash = 4,
        Pascal = -2,
        X11Gost = 19,
        Sia = -1,
        Blake2s = 2,
        Skunk = 21
        #endregion // NiceHashAPI
    }
}
