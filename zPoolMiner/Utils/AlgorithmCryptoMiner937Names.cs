namespace zPoolMiner
{
    using System;
    using zPoolMiner.Enums;

    /// <summary>
    /// AlgorithmCryptoMiner937Names class is just a data container for mapping NiceHash JSON API names to algo type
    /// </summary>
    public static class AlgorithmCryptoMiner937Names
    {
        /// <summary>
        /// The GetName
        /// </summary>
        /// <param name="type">The <see cref="AlgorithmType"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetName(AlgorithmType type)
        {
            if ((AlgorithmType.INVALID <= type && type <= AlgorithmType.Skunk)
                || (AlgorithmType.DaggerSia <= type && type <= AlgorithmType.DaggerPascal)
                || (AlgorithmType.X17 <= type && type <= AlgorithmType.X17)
                || (AlgorithmType.Sha256t <= type && type <= AlgorithmType.Sha256t)
                || (AlgorithmType.Tribus <= type && type <= AlgorithmType.Tribus)
                || (AlgorithmType.Timetravel <= type && type <= AlgorithmType.Timetravel)
                || (AlgorithmType.Veltor <= type && type <= AlgorithmType.Veltor)
                || (AlgorithmType.Xevan <= type && type <= AlgorithmType.Xevan)
                || (AlgorithmType.yescrypt <= type && type <= AlgorithmType.yescrypt)
                || (AlgorithmType.yescryptr16 <= type && type <= AlgorithmType.yescryptr16)
                //|| (AlgorithmType.M7M <= type && type <= AlgorithmType.M7M)
                || (AlgorithmType.cryptonight <= type && type <= AlgorithmType.cryptonight)
                || (AlgorithmType.cryptonightv7 <= type && type <= AlgorithmType.cryptonightv7)
                || (AlgorithmType.x16r <= type && type <= AlgorithmType.x16r)
                || (AlgorithmType.DaggerHashimoto <= type && type <= AlgorithmType.DaggerHashimoto)
                || (AlgorithmType.DaggerBlake2s <= type && type <= AlgorithmType.DaggerPascal)
                || (AlgorithmType.lyra2z <= type && type <= AlgorithmType.lyra2z)
                || (AlgorithmType.hmq1725 <= type && type <= AlgorithmType.hmq1725)
                || (AlgorithmType.Keccakc <= type && type <= AlgorithmType.Keccakc)
                || (AlgorithmType.randomxmonero <= type && type <= AlgorithmType.randomxmonero)
                //|| (AlgorithmType.randomarq <= type && type <= AlgorithmType.randomarq)
                || (AlgorithmType.randomx <= type && type <= AlgorithmType.randomx)
                //|| (AlgorithmType.randomsfx <= type && type <= AlgorithmType.randomsfx)
                //|| (AlgorithmType.cryptonight_heavy <= type && type <= AlgorithmType.cryptonight_heavy)
                || (AlgorithmType.cryptonight_heavyx <= type && type <= AlgorithmType.cryptonight_heavyx)
                //|| (AlgorithmType.cryptonight_saber <= type && type <= AlgorithmType.cryptonight_saber)
                //|| (AlgorithmType.cryptonight_fast <= type && type <= AlgorithmType.cryptonight_fast)
                || (AlgorithmType.cryptonight_haven <= type && type <= AlgorithmType.cryptonight_haven)
                || (AlgorithmType.cryptonight_upx <= type && type <= AlgorithmType.cryptonight_upx)
                || (AlgorithmType.yespower <= type && type <= AlgorithmType.yespower)
                || (AlgorithmType.cpupower <= type && type <= AlgorithmType.cpupower)
                || (AlgorithmType.power2b <= type && type <= AlgorithmType.power2b)
                //|| (AlgorithmType.yescryptr8g <= type && type <= AlgorithmType.yescryptr8g)
                //|| (AlgorithmType.yespoweriots <= type && type <= AlgorithmType.yespoweriots)
                //|| (AlgorithmType.chukwa <= type && type <= AlgorithmType.chukwa)
                || (AlgorithmType.yescryptr32 <= type && type <= AlgorithmType.yescryptr32)
                || (AlgorithmType.allium <= type && type <= AlgorithmType.allium)
                || (AlgorithmType.x16s <= type && type <= AlgorithmType.x16s)
                || (AlgorithmType.phi2 <= type && type <= AlgorithmType.phi2)
                || (AlgorithmType.hex <= type && type <= AlgorithmType.hex)
                || (AlgorithmType.sonoa <= type && type <= AlgorithmType.sonoa)
                || (AlgorithmType.bcd <= type && type <= AlgorithmType.bcd)
                //|| (AlgorithmType.argon2d250 <= type && type <= AlgorithmType.argon2d250)
                //|| (AlgorithmType.argon2d4096 <= type && type <= AlgorithmType.argon2d4096)
                || (AlgorithmType.cryptonight_gpu <= type && type <= AlgorithmType.cryptonight_gpu)
                || (AlgorithmType.cryptonight_xeq <= type && type <= AlgorithmType.cryptonight_xeq)
                //|| (AlgorithmType.cryptonight_saber <= type && type <= AlgorithmType.cryptonight_saber)
                || (AlgorithmType.cryptonight_conceal <= type && type <= AlgorithmType.cryptonight_conceal)
                || (AlgorithmType.lyra2v3 <= type && type <= AlgorithmType.lyra2v3)
                || (AlgorithmType.equihash96 <= type && type <= AlgorithmType.equihash96)
                || (AlgorithmType.equihash125 <= type && type <= AlgorithmType.equihash125)
                || (AlgorithmType.equihash144 <= type && type <= AlgorithmType.equihash144)
                || (AlgorithmType.equihash192 <= type && type <= AlgorithmType.equihash192)
                || (AlgorithmType.scryptn2 <= type && type <= AlgorithmType.scryptn2)

                )
            {
                return Enum.GetName(typeof(AlgorithmType), type);
            }
            return "NameNotFound type not supported";
        }
    }
}
