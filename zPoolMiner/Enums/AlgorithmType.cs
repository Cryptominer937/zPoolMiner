namespace zPoolMiner.Enums
{
    /// <summary>
    /// AlgorithmType enum should/must mirror the values from https://www.nicehash.com/?p=api
    /// Some algorithms are not used anymore on the client, rename them with _UNUSED postfix so we can catch compile time errors if they are used.
    /// </summary>
    public enum AlgorithmType : int
    {
        // dual algos for grouping
        DaggerBlake2s = -7,
        /// <summary>
        /// Defines the DaggerSia
        /// </summary>
        DaggerSia = -6,

        /// <summary>
        /// Defines the DaggerDecred
        /// </summary>
        DaggerDecred = -5,

        /// <summary>
        /// Defines the DaggerLbry
        /// </summary>
        DaggerLbry = -4,

        /// <summary>
        /// Defines the DaggerPascal
        /// </summary>
        DaggerPascal = -3,

        /// <summary>
        /// Defines the INVALID
        /// </summary>
        INVALID = -2,

        /// <summary>
        /// Defines the NONE
        /// </summary>
        NONE = -1,

        /// <summary>
        /// Defines the Hodl
        /// </summary>
        Hodl = -14,

       

        /// <summary>
        /// Defines the cryptonight
        /// </summary>
        cryptonight = 33,

       

        /// <summary>
        /// Defines the Blake256r14_UNUSED
        /// </summary>
        Blake256r14_UNUSED = -96,

        /// <summary>
        /// Defines the Blake256r8vnl_UNUSED
        /// </summary>
        Blake256r8vnl_UNUSED = -86,

        /// <summary>
        /// Defines the ScryptNf_UNUSED
        /// </summary>
        ScryptNf_UNUSED = -76,

        /// <summary>
        /// Defines the WhirlpoolX_UNUSED
        /// </summary>
        WhirlpoolX_UNUSED = -66,

        /// <summary>
        /// Defines the Axiom_UNUSED
        /// </summary>
        Axiom_UNUSED = -56,

        /// <summary>
        /// Defines the ScryptJaneNf16_UNUSED
        /// </summary>
        ScryptJaneNf16_UNUSED = -46,

        /// <summary>
        /// Defines the Lyra2RE
        /// </summary>
        Lyra2RE = -36,

        /// <summary>
        /// Defines the X15_UNUSED
        /// </summary>
        X15_UNUSED = -26,

        /// <summary>
        /// Defines the DaggerHashimoto
        /// </summary>
        DaggerHashimoto = 35,

        /// <summary>
        /// Defines the Bitcore
        /// </summary>
        //Bitcore = 0,

        /// <summary>
        /// Defines the Blake2s
        /// </summary>
        Blake2s = 1,

        /// <summary>
        /// Defines the Blake256r8
        /// </summary>
        Blake256r8 = 2,

        /// <summary>
        /// Defines the C11
        /// </summary>
        C11 = 3,

        /// <summary>
        /// Defines the equihash
        /// </summary>
        equihash = 4,

        /// <summary>
        /// Defines the Groestl
        /// </summary>
        Groestl = 5,
        //hmq1725 = 37,
        /// <summary>
        /// Defines the Hsr
        /// </summary>
        //Hsr = 6,

        /// <summary>
        /// Defines the Keccak
        /// </summary>
        Keccak = 7,

        /// <summary>
        /// Defines the Lbry
        /// </summary>
        Lbry = 8,

        /// <summary>
        /// Defines the Lyra2REv2
        /// </summary>
        Lyra2REv2 = 9,

        /// <summary>
        /// Defines the Myriad_groestl
        /// </summary>
        Myriad_groestl = 10,

        /// <summary>
        /// Defines the NeoScrypt
        /// </summary>
        NeoScrypt = 11,

        /// <summary>
        /// Defines the Nist5
        /// </summary>
        Nist5 = 12,

        /// <summary>
        /// Defines the Phi
        /// </summary>
        Phi = 13,

        /// <summary>
        /// Defines the Polytimos
        /// </summary>
        Polytimos = 14,

        /// <summary>
        /// Defines the Quark_UNUSED
        /// </summary>
        Quark_UNUSED = 15,

        /// <summary>
        /// Defines the Qubit_UNUSED
        /// </summary>
        Qubit_UNUSED = 16,

        /// <summary>
        /// Defines the Scrypt_UNUSED
        /// </summary>
        Scrypt_UNUSED = 17,

        /// <summary>
        /// Defines the SHA256_UNUSED
        /// </summary>
        SHA256_UNUSED = 18,

        /// <summary>
        /// Defines the X11Gost
        /// </summary>
        X11Gost = 19,

        /// <summary>
        /// Defines the Skein
        /// </summary>
        Skein = 20,

        /// <summary>
        /// Defines the Skunk
        /// </summary>
        Skunk = 21,

        /// <summary>
        /// Defines the Timetravel
        /// </summary>
        Timetravel = 22,

        /// <summary>
        /// Defines the Tribus
        /// </summary>
        //Tribus = 23,

        /// <summary>
        /// Defines the Veltor
        /// </summary>
        Veltor = 24,

        /// <summary>
        /// Defines the X11_UNUSED
        /// </summary>
        X11_UNUSED = 25,

        /// <summary>
        /// Defines the X11evo
        /// </summary>
        X11evo = 26,

        /// <summary>
        /// Defines the X13_UNUSED
        /// </summary>
        X13_UNUSED = 27,

        /// <summary>
        /// Defines the X14
        /// </summary>
        X14 = 28,

        /// <summary>
        /// Defines the X17
        /// </summary>
        //X17 = 29,

        /// <summary>
        /// Defines the Xevan
        /// </summary>
        //Xevan = 30,

        /// <summary>
        /// Defines the yescrypt
        /// </summary>
        yescrypt = 31,
        //M7M = 32,
        lyra2z = 36,
        yescryptr16 = 38,
        Sia = 39,
        Decred = 40,
        Pascal = 41,
        //K/eccakc = 42,
        Sha256t = 43,
        cryptonightv7 = 44,
        x16r = 45,
        randomxmonero = 46,
        //randomarq = 47,
        randomx = 48,
        //randomsfx = 49,
        //cryptonight_heavy = 50,
        cryptonight_heavyx = 51,
        //cryptonight_saber = 52,
        //cryptonight_fast = 53,
        cryptonight_haven = 54,
        cryptonight_upx = 55,
        yespower = 56,
        cpupower = 57,
        power2b = 58,
        //yescryptr8g = 59,
        //yespoweriots = 60,
        //chukwa = 61,
        yescryptr32 = 62,
        //x16s = 63,
        //sonoa = 64,
        bcd = 65,
        //phi2 = 66,
        hex = 67,
        allium = 68,
        //lyra2v3 = 69,
        cryptonight_gpu = 70,
        //cryptonight_xeq = 71,
        //cryptonight_conceal = 72,
        equihash144 = 73,
        equihash125 = 74,
        equihash192 = 75,
        //equihash96 = 76,
        scryptn2 = 77,
        karlsenhash = 78,
    }
}
