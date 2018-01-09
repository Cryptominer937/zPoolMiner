using System;
using System.Collections.Generic;
using System.Text;
using zPoolMiner.Enums;

namespace zPoolMiner
{
    /// <summary>
    /// AlgorithmNiceHashNames class is just a data container for mapping NiceHash JSON API names to algo type
    /// </summary>
    public static class AlgorithmNiceHashNames
    {
        public static string GetName(AlgorithmType type) {
            if ((AlgorithmType.INVALID <= type && type <= AlgorithmType.Skunk) 
                || (AlgorithmType.DaggerSia <= type && type <= AlgorithmType.DaggerPascal) 
                || (AlgorithmType.X17 <= type && type <= AlgorithmType.X17) 
                || (AlgorithmType.Tribus <= type && type <= AlgorithmType.Tribus)
                || (AlgorithmType.Timetravel <= type && type <= AlgorithmType.Timetravel)
                || (AlgorithmType.Veltor <= type && type <= AlgorithmType.Veltor)

                )
            {

                return Enum.GetName(typeof(AlgorithmType), type);
            }
            return "NameNotFound type not supported";
        }
    }
}
