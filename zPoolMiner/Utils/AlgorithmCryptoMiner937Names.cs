﻿namespace zPoolMiner
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
                //|| (AlgorithmType.X17 <= type && type <= AlgorithmType.X17)
                || (AlgorithmType.Sha256t <= type && type <= AlgorithmType.Sha256t)
                //|| (AlgorithmType.Tribus <= type && type <= AlgorithmType.Tribus)
                || (AlgorithmType.Timetravel <= type && type <= AlgorithmType.Timetravel)
                || (AlgorithmType.Veltor <= type && type <= AlgorithmType.Veltor)
                //|| (AlgorithmType.Xevan <= type && type <= AlgorithmType.Xevan)
                || (AlgorithmType.yescrypt <= type && type <= AlgorithmType.yescrypt)
                || (AlgorithmType.yescryptr16 <= type && type <= AlgorithmType.yescryptr16)
                //|| (AlgorithmType.M7M <= type && type <= AlgorithmType.M7M)
                || (AlgorithmType.cryptonight <= type && type <= AlgorithmType.cryptonight)
                || (AlgorithmType.cryptonightv7 <= type && type <= AlgorithmType.cryptonightv7)
                || (AlgorithmType.x16r <= type && type <= AlgorithmType.x16r)
                || (AlgorithmType.DaggerHashimoto <= type && type <= AlgorithmType.DaggerHashimoto)
                || (AlgorithmType.DaggerBlake2s <= type && type <= AlgorithmType.DaggerPascal)
                || (AlgorithmType.lyra2z <= type && type <= AlgorithmType.lyra2z)
                //|| (AlgorithmType.hmq1725 <= type && type <= AlgorithmType.hmq1725)
                //|| (AlgorithmType.Keccakc <= type && type <= AlgorithmType.Keccakc)
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
                //|| (AlgorithmType.x16s <= type && type <= AlgorithmType.x16s)
                //|| (AlgorithmType.phi2 <= type && type <= AlgorithmType.phi2)
                || (AlgorithmType.hex <= type && type <= AlgorithmType.hex)
                //|| (AlgorithmType.sonoa <= type && type <= AlgorithmType.sonoa)
                || (AlgorithmType.bcd <= type && type <= AlgorithmType.bcd)
                //|| (AlgorithmType.argon2d250 <= type && type <= AlgorithmType.argon2d250)
                //|| (AlgorithmType.argon2d4096 <= type && type <= AlgorithmType.argon2d4096)
                || (AlgorithmType.cryptonight_gpu <= type && type <= AlgorithmType.cryptonight_gpu)
                //|| (AlgorithmType.cryptonight_xeq <= type && type <= AlgorithmType.cryptonight_xeq)
                //|| (AlgorithmType.cryptonight_saber <= type && type <= AlgorithmType.cryptonight_saber)
                //|| (AlgorithmType.cryptonight_conceal <= type && type <= AlgorithmType.cryptonight_conceal)
                //|| (AlgorithmType.lyra2v3 <= type && type <= AlgorithmType.lyra2v3)
                //|| (AlgorithmType.equihash96 <= type && type <= AlgorithmType.equihash96)
                || (AlgorithmType.equihash125 <= type && type <= AlgorithmType.equihash125)
                || (AlgorithmType.equihash144 <= type && type <= AlgorithmType.equihash144)
                || (AlgorithmType.equihash192 <= type && type <= AlgorithmType.equihash192)
                || (AlgorithmType.scryptn2 <= type && type <= AlgorithmType.scryptn2)
                || (AlgorithmType.karlsenhash <= type && type <= AlgorithmType.karlsenhash)
                || (AlgorithmType.pyrinhash <= type && type <= AlgorithmType.pyrinhash)
                || (AlgorithmType.ethash <= type && type <= AlgorithmType.ethash)
                || (AlgorithmType.ethashb3 <= type && type <= AlgorithmType.ethashb3)
                || (AlgorithmType.kawpow <= type && type <= AlgorithmType.kawpow)
                || (AlgorithmType.aurum <= type && type <= AlgorithmType.aurum)
                || (AlgorithmType.bmw512 <= type && type <= AlgorithmType.bmw512)
                || (AlgorithmType.curvehash <= type && type <= AlgorithmType.curvehash)
                || (AlgorithmType.evrprogpow <= type && type <= AlgorithmType.evrprogpow)
                || (AlgorithmType.firopow <= type && type <= AlgorithmType.firopow)
                || (AlgorithmType.frkhash <= type && type <= AlgorithmType.frkhash)
                || (AlgorithmType.ghostrider <= type && type <= AlgorithmType.ghostrider)
                || (AlgorithmType.heavyhash <= type && type <= AlgorithmType.heavyhash)
                || (AlgorithmType.karlsenhashnxl <= type && type <= AlgorithmType.karlsenhashnxl)
                || (AlgorithmType.kheavyhash <= type && type <= AlgorithmType.kheavyhash)
                || (AlgorithmType.megabtx <= type && type <= AlgorithmType.megabtx)
                || (AlgorithmType.memehash <= type && type <= AlgorithmType.memehash)
                || (AlgorithmType.meowpow <= type && type <= AlgorithmType.meowpow)
                || (AlgorithmType.mike <= type && type <= AlgorithmType.mike)
                || (AlgorithmType.minotaurx <= type && type <= AlgorithmType.minotaurx)
                || (AlgorithmType.neoscryptxaya <= type && type <= AlgorithmType.neoscryptxaya)
                || (AlgorithmType.nexapow <= type && type <= AlgorithmType.nexapow)
                || (AlgorithmType.odocrypt <= type && type <= AlgorithmType.odocrypt)
                || (AlgorithmType.panthera <= type && type <= AlgorithmType.panthera)
                || (AlgorithmType.randomarq <= type && type <= AlgorithmType.randomarq)
                || (AlgorithmType.sha256csm <= type && type <= AlgorithmType.sha256csm)
                || (AlgorithmType.sha256dt <= type && type <= AlgorithmType.sha256dt)
                || (AlgorithmType.sha3d <= type && type <= AlgorithmType.sha3d)
                || (AlgorithmType.sha512256d <= type && type <= AlgorithmType.sha512256d)
                || (AlgorithmType.skydoge <= type && type <= AlgorithmType.skydoge)
                || (AlgorithmType.verthash <= type && type <= AlgorithmType.verthash)
                || (AlgorithmType.verushash <= type && type <= AlgorithmType.verushash)
                || (AlgorithmType.x16rt <= type && type <= AlgorithmType.x16rt)
                || (AlgorithmType.x16rv2 <= type && type <= AlgorithmType.x16rv2)
                || (AlgorithmType.x21s <= type && type <= AlgorithmType.x21s)
                || (AlgorithmType.x25x <= type && type <= AlgorithmType.x25x)
                || (AlgorithmType.yespowerltncg <= type && type <= AlgorithmType.yespowerltncg)
                || (AlgorithmType.yespowermgpc <= type && type <= AlgorithmType.yespowermgpc)
                || (AlgorithmType.yespowerr16 <= type && type <= AlgorithmType.yespowerr16)
                || (AlgorithmType.yespowersugar <= type && type <= AlgorithmType.yespowersugar)
                || (AlgorithmType.yespowertide <= type && type <= AlgorithmType.yespowertide)
                || (AlgorithmType.yespowerurx <= type && type <= AlgorithmType.yespowerurx)
                || (AlgorithmType.kawpow <= type && type <= AlgorithmType.kawpow))
            {
                return Enum.GetName(typeof(AlgorithmType), type);
            }

            return "NameNotFound type not supported";
        }
    }
}