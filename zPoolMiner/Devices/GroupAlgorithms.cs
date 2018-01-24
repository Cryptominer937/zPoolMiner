﻿namespace zPoolMiner.Devices
{
    using System.Collections.Generic;
    using zPoolMiner.Enums;

    /// <summary>
    /// GroupAlgorithms creates defaults supported algorithms. Currently based in Miner implementation
    /// </summary>
    public static class GroupAlgorithms
    {
        /// <summary>
        /// The CreateForDevice
        /// </summary>
        /// <param name="device">The <see cref="ComputeDevice"/></param>
        /// <returns>The <see cref="Dictionary{MinerBaseType, List{Algorithm}}"/></returns>
        private static Dictionary<MinerBaseType, List<Algorithm>> CreateForDevice(ComputeDevice device)
        {
            if (device != null)
            {
                var algoSettings = CreateDefaultsForGroup(device.DeviceGroupType);
                if (algoSettings != null)
                {
                    if (device.DeviceType == DeviceType.AMD)
                    {
                        // sgminer stuff
                        if (algoSettings.ContainsKey(MinerBaseType.sgminer))
                        {
                            var sgminerAlgos = algoSettings[MinerBaseType.sgminer];
                            int Lyra2REv2_Index = sgminerAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.Lyra2REv2);
                            //int NeoScrypt_Index = sgminerAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.NeoScrypt);
                            int CryptoNight_Index = sgminerAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.CryptoNight);

                            // Check for optimized version
                            if (Lyra2REv2_Index > -1)
                            {
                                sgminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + "--nfactor 10 --xintensity 64 --thread-concurrency 0 --worksize 64 --gpu-threads 2";
                            }
                            //if (!device.Codename.Contains("Tahiti") && NeoScrypt_Index > -1)
                            //{
                            //    sgminerAlgos[NeoScrypt_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + "--intensity 13 --worksize  256 --gpu-threads 1";
                            //    Helpers.ConsolePrint("ComputeDevice", "The GPU detected (" + device.Codename + ") is not Tahiti. Changing default gpu-threads to 2.");
                            //}
                            if (CryptoNight_Index > -1)
                            {
                                if (device.Codename.Contains("Hawaii"))
                                {
                                    sgminerAlgos[CryptoNight_Index].ExtraLaunchParameters = "--rawintensity 640 -w 8 -g 2";
                                }
                                else if (device.Name.Contains("Vega"))
                                {
                                    sgminerAlgos[CryptoNight_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + " --rawintensity 1850 -w 8 -g 2";
                                }
                            }
                        }
                        if (algoSettings.ContainsKey(MinerBaseType.GatelessGate))
                        {
                            var glgAlgos = algoSettings[MinerBaseType.GatelessGate];
                            int Lyra2REv2_Index = glgAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.Lyra2REv2);
                            int NeoScrypt_Index = glgAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.NeoScrypt);
                            int CryptoNight_Index = glgAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.CryptoNight);


                            // Check for optimized version
                            if (!device.Codename.Contains("Tahiti") && NeoScrypt_Index > -1)
                            {
                                glgAlgos[NeoScrypt_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + "--intensity 13 --worksize  256 --gpu-threads 1";
                                Helpers.ConsolePrint("ComputeDevice", "The GPU detected (" + device.Codename + ") is not Tahiti. Changing default gpu-threads to 2.");
                            }
                            if (CryptoNight_Index > -1)
                            {
                                if (device.Codename.Contains("gfx804")) //rx550
                                {
                                    glgAlgos[CryptoNight_Index].ExtraLaunchParameters = "--rawintensity 448 -w 8 -g 2";
                                }
                                if (device.Codename.Contains("Pitcairn")) //r7-370
                                {
                                    glgAlgos[CryptoNight_Index].ExtraLaunchParameters = "--rawintensity 416 -w 8 -g 2";
                                }
                                if (device.Codename.Contains("Baffin")) //rx460/560
                                {
                                    glgAlgos[CryptoNight_Index].ExtraLaunchParameters = "--rawintensity 448 -w 8 -g 2";
                                }

                                if (device.Codename.Contains("Ellesmere")) //rx570/580
                                {
                                    glgAlgos[CryptoNight_Index].ExtraLaunchParameters = "--intensity 13 --worksize  256 --gpu-threads 1";
                                }

                                if (device.Codename.Contains("Hawaii"))
                                {
                                    glgAlgos[CryptoNight_Index].ExtraLaunchParameters = "--rawintensity 640 -w 8 -g 2";
                                }
                                else if (device.Name.Contains("Vega"))
                                {
                                    glgAlgos[CryptoNight_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + " --rawintensity 1850 -w 8 -g 2";
                                }
                            }
                        }
                        // Ellesmere, Polaris
                        // Ellesmere sgminer workaround, keep this until sgminer is fixed to work with Ellesmere
                        if ((device.Codename.Contains("Ellesmere") || device.InfSection.ToLower().Contains("polaris")))
                        {
                            foreach (var algosInMiner in algoSettings)
                            {
                                foreach (var algo in algosInMiner.Value)
                                {
                                    // disable all algos in list
                                    if (algo.NiceHashID == AlgorithmType.Decred || algo.NiceHashID == AlgorithmType.Lbry)
                                    {
                                        algo.Enabled = false;
                                    }
                                }
                            }
                        }
                        // non sgminer optimizations
                        if (algoSettings.ContainsKey(MinerBaseType.Claymore_old) && algoSettings.ContainsKey(MinerBaseType.Claymore))
                        {
                            var claymoreOldAlgos = algoSettings[MinerBaseType.Claymore_old];
                            var cryptoNightOldIndex =
                                claymoreOldAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.CryptoNight);

                            var claymoreNewAlgos = algoSettings[MinerBaseType.Claymore];
                            var cryptoNightNewIndex =
                                claymoreNewAlgos.FindIndex(el => el.NiceHashID == AlgorithmType.CryptoNight);

                            if (cryptoNightOldIndex > -1 && cryptoNightNewIndex > -1)
                            {
                                //string regex_a_3 = "[5|6][0-9][0-9][0-9]";
                                List<string> a_4 = new List<string>() {
                                    "270",
                                    "270x",
                                    "280",
                                    "280x",
                                    "290",
                                    "290x",
                                    "370",
                                    "380",
                                    "390",
                                    "470",
                                    "480"};
                                foreach (var namePart in a_4)
                                {
                                    if (device.Name.Contains(namePart))
                                    {
                                        claymoreOldAlgos[cryptoNightOldIndex].ExtraLaunchParameters = "-a 4";
                                        break;
                                    }
                                }

                                List<string> old = new List<string> {
                                    "Verde",
                                    "Oland",
                                    "Bonaire"
                                };
                                foreach (var codeName in old)
                                {
                                    var isOld = device.Codename.Contains(codeName);
                                    claymoreOldAlgos[cryptoNightOldIndex].Enabled = isOld;
                                    claymoreNewAlgos[cryptoNightNewIndex].Enabled = !isOld;
                                }
                            }
                        }

                        // drivers algos issue
                        if (device.DriverDisableAlgos)
                        {
                            //algoSettings = FilterMinerAlgos(algoSettings, new List<AlgorithmType> { AlgorithmType.NeoScrypt, AlgorithmType.Lyra2REv2 });
                            //algoSettings = FilterMinerAlgos(algoSettings, new List<AlgorithmType> { AlgorithmType.Lyra2REv2 });
                        }

                        /*if (algoSettings.ContainsKey(MinerBaseType.mkxminer))
                        {
                            var mkxminerAlgos = algoSettings[MinerBaseType.mkxminer];
                            int Lyra2REv2_Index = mkxminerAlgos.FindIndex((el) => el.NiceHashID == AlgorithmType.Lyra2REv2);

                            if (Lyra2REv2_Index > -1)
                            {
                                if (device.Codename.Contains("gfx804")) //rx550
                                {
                                    mkxminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = "-I 23";
                                }
                                if (device.Codename.Contains("Pitcairn")) //r7-370
                                {
                                    mkxminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = "-I 23";
                                }
                                if (device.Codename.Contains("Baffin")) //rx460/560
                                {
                                    mkxminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = "-I 23";
                                }

                                if (device.Codename.Contains("Ellesmere")) //rx570/580
                                {
                                    mkxminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = "-I 23";
                                }

                                if (device.Codename.Contains("Hawaii"))
                                {
                                    mkxminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = "-I 23";
                                }
                                else if (device.Name.Contains("Vega"))
                                {
                                    mkxminerAlgos[Lyra2REv2_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + "-I 23";
                                }
                            }
                        } */

                        // disable by default
                        {
                            var minerBases = new List<MinerBaseType>() { MinerBaseType.ethminer, MinerBaseType.OptiminerAMD };
                            foreach (var minerKey in minerBases)
                            {
                                if (algoSettings.ContainsKey(minerKey))
                                {
                                    foreach (var algo in algoSettings[minerKey])
                                    {
                                        algo.Enabled = false;
                                    }
                                }
                            }
                            if (algoSettings.ContainsKey(MinerBaseType.sgminer))
                            {
                                foreach (var algo in algoSettings[MinerBaseType.sgminer])
                                {
                                    if (algo.NiceHashID == AlgorithmType.DaggerHashimoto)
                                    {
                                        algo.Enabled = false;
                                    }
                                }
                            }
                            //if (algoSettings.ContainsKey(MinerBaseType.Claymore)) {
                            //    foreach (var algo in algoSettings[MinerBaseType.Claymore]) {
                            //        if (algo.NiceHashID == AlgorithmType.CryptoNight) {
                            //            algo.Enabled = false;
                            //        }
                            //    }
                            //}
                        }
                    } // END AMD case

                    // check if it is Etherum capable
                    if (device.IsEtherumCapale == false)
                    {
                        algoSettings = FilterMinerAlgos(algoSettings, new List<AlgorithmType> { AlgorithmType.DaggerHashimoto });
                    }

                    if (algoSettings.ContainsKey(MinerBaseType.ccminer_alexis))
                    {
                        foreach (var unstable_algo in algoSettings[MinerBaseType.ccminer_alexis])
                        {
                            unstable_algo.Enabled = false;
                        }
                    }
                    if (algoSettings.ContainsKey(MinerBaseType.experimental))
                    {
                        foreach (var unstable_algo in algoSettings[MinerBaseType.experimental])
                        {
                            unstable_algo.Enabled = false;
                        }
                    }

                    // This is not needed anymore after excavator v1.1.4a
                    //if (device.IsSM50() && algoSettings.ContainsKey(MinerBaseType.excavator)) {
                    //    int Equihash_index = algoSettings[MinerBaseType.excavator].FindIndex((algo) => algo.NiceHashID == AlgorithmType.Equihash);
                    //    if (Equihash_index > -1) {
                    //        // -c1 1 needed for SM50 to work ATM
                    //        algoSettings[MinerBaseType.excavator][Equihash_index].ExtraLaunchParameters = "-c1 1";
                    //    }
                    //}
                    // nheqminer exceptions scope
                    {
                        const MinerBaseType minerBaseKey = MinerBaseType.nheqminer;
                        if (algoSettings.ContainsKey(minerBaseKey) && device.Name.Contains("GTX")
                            && (device.Name.Contains("560") || device.Name.Contains("650") || device.Name.Contains("680") || device.Name.Contains("770"))
                            )
                        {
                            algoSettings = FilterMinerBaseTypes(algoSettings, new List<MinerBaseType>() { minerBaseKey });
                        }
                    }
                } // END algoSettings != null
                return algoSettings;
            }
            return null;
        }

        /// <summary>
        /// The CreateForDeviceList
        /// </summary>
        /// <param name="device">The <see cref="ComputeDevice"/></param>
        /// <returns>The <see cref="List{Algorithm}"/></returns>
        public static List<Algorithm> CreateForDeviceList(ComputeDevice device)
        {
            List<Algorithm> ret = new List<Algorithm>();
            var retDict = CreateForDevice(device);
            if (retDict != null)
            {
                foreach (var kvp in retDict)
                {
                    ret.AddRange(kvp.Value);
                }
            }
            return ret;
        }

        /// <summary>
        /// The CreateDefaultsForGroup
        /// </summary>
        /// <param name="deviceGroupType">The <see cref="DeviceGroupType"/></param>
        /// <returns>The <see cref="Dictionary{MinerBaseType, List{Algorithm}}"/></returns>
        public static Dictionary<MinerBaseType, List<Algorithm>> CreateDefaultsForGroup(DeviceGroupType deviceGroupType)
        {
            if (DeviceGroupType.CPU == deviceGroupType)
            {
                return new Dictionary<MinerBaseType, List<Algorithm>>() {
                    { MinerBaseType.XmrStackCPU,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.XmrStackCPU, AlgorithmType.CryptoNight, "cryptonight")
                        }
                    },
                    { MinerBaseType.Xmrig,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.Xmrig, AlgorithmType.CryptoNight, "")
                        }
                    }
                };
            }
            if (DeviceGroupType.AMD_OpenCL == deviceGroupType)
            {
                // DisableAMDTempControl = false; TemperatureParam must be appended lastly
                string RemDis = " --remove-disabled ";
                string DefaultParam = RemDis + AmdGpuDevice.DefaultParam;
                return new Dictionary<MinerBaseType, List<Algorithm>>() {
                    { MinerBaseType.sgminer,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.NeoScrypt, "neoscrypt") { ExtraLaunchParameters = DefaultParam + "--intensity 13 --worksize  256 --gpu-threads 1" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Lyra2REv2,  "Lyra2REv2") { ExtraLaunchParameters = DefaultParam + "--nfactor 10 --xintensity  160 --thread-concurrency    0 --worksize  64 --gpu-threads 1" },
                            // not on zPool
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.DaggerHashimoto, "ethash") { ExtraLaunchParameters = RemDis + "--xintensity 512 -w 192 -g 1" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Decred, "decred") { ExtraLaunchParameters = RemDis + "--gpu-threads 1 --xintensity 256 --lookup-gap 2 --worksize 64" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Lbry, "lbry") { ExtraLaunchParameters = DefaultParam + "--xintensity 512 --worksize 128 --gpu-threads 2" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.CryptoNight, "cryptonight") { ExtraLaunchParameters = DefaultParam + "--rawintensity 512 -w 4 -g 2" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Pascal, "pascal") { ExtraLaunchParameters = DefaultParam + "--intensity 21 -w 64 -g 2" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.X11Gost, "sibcoin-mod") { ExtraLaunchParameters = DefaultParam + "--intensity 19 -w 128 -g 2" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Keccak, "keccak") { ExtraLaunchParameters = DefaultParam + "--intensity 15" },
                            //Cryptominer937 Additions                            
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Skein, "skeincoin") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 2 --worksize 256 --intensity d" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Myriad_groestl, "myriadcoin-groestl") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 2 --worksize 64 --intensity d" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Bitcore, "timetravel10") { ExtraLaunchParameters = DefaultParam + "--intensity 21" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Blake2s, "sia") { ExtraLaunchParameters = DefaultParam + "--intensity d" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Hsr, "hsr") { ExtraLaunchParameters = DefaultParam + "--intensity 20" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Phi, "phi") { ExtraLaunchParameters = DefaultParam + "--intensity 21" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Veltor, "veltor") { ExtraLaunchParameters = DefaultParam + "--intensity 19" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Tribus, "tribus") { ExtraLaunchParameters = DefaultParam + "--shaders 1792 --lookup-gap 4 --intensity 19" },
                            new Algorithm(MinerBaseType.sgminer, AlgorithmType.Xevan, "xevan") { ExtraLaunchParameters = DefaultParam + "--intensity 19" }

                        }
                    },

                    { MinerBaseType.GatelessGate,
                         new List<Algorithm>() {
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.NeoScrypt, "neoscrypt") { ExtraLaunchParameters = DefaultParam + "--intensity 13 --worksize  256 --gpu-threads 1" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Lyra2REv2,  "Lyra2REv2") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 6 --worksize 128 --intensity d" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Equihash, "equihash"){ ExtraLaunchParameters = RemDis + "--xintensity 512 --worksize 256 --gpu-threads 2" },
                           //  new Algorithm(MinerBaseType.glg, AlgorithmType.DaggerHashimoto, "ethash") { ExtraLaunchParameters = DefaultParam + "--xintensity 512 -w 192 -g 1" },
                           //  new Algorithm(MinerBaseType.glg, AlgorithmType.Decred, "decred") { ExtraLaunchParameters = RemDis + "--gpu-threads 1 --xintensity 256 --lookup-gap 2 --worksize 64" },
                           //  new Algorithm(MinerBaseType.glg, AlgorithmType.Lbry, "lbry") { ExtraLaunchParameters = DefaultParam + "--xintensity 512 --worksize 128 --gpu-threads 2" },
                           //  new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.CryptoNight, "cryptonight") { ExtraLaunchParameters = DefaultParam + "--rawintensity 512 -w 8 -g 2" },
                           //  new Algorithm(MinerBaseType.glg, AlgorithmType.Pascal, "pascal") { ExtraLaunchParameters = DefaultParam + "--intensity 21 -w 64 -g 2" },
                             //new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.X11Gost, "sibcoin-mod") { ExtraLaunchParameters = DefaultParam + "--intensity 16 -w 64 -g 2" },
                             //new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Skein, "skein") { ExtraLaunchParameters = DefaultParam + "--intensity d --gpu-threads 2" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Myriad_groestl, "myriadcoin-groestl") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 2 --worksize 64 --intensity d" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Keccak, "keccak") { ExtraLaunchParameters = DefaultParam + "--intensity d --gpu-threads 2" }
                         }
                     },
                    /*{ MinerBaseType.mkxminer,
                         new List<Algorithm>() {
                            new Algorithm(MinerBaseType.mkxminer, AlgorithmType.Lyra2REv2,  "Lyra2REv2") { ExtraLaunchParameters = "--exitsick -I 23" }
                         }
                     },*/
                    { MinerBaseType.Claymore,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.CryptoNight, "cryptonight"),
                            new Algorithm(MinerBaseType.Claymore, AlgorithmType.Equihash, "equihash"),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, ""),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Decred),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Lbry),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Pascal),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Sia)
                        }
                    },
                    //{ MinerBaseType.Claymore_old,
                    //    new List<Algorithm> {
                    //        new Algorithm(MinerBaseType.Claymore_old, AlgorithmType.CryptoNight, "old")
                    //    }
                    //},
                    { MinerBaseType.OptiminerAMD,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.OptiminerAMD, AlgorithmType.Equihash, "equihash")
                        }
                    },
                    { MinerBaseType.Prospector,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.Prospector, AlgorithmType.Skunk, "sigt"),
                            //new Algorithm(MinerBaseType.Prospector, AlgorithmType.Sia, "sia")
                        }
                    },
                    //{ MinerBaseType.XmrStakAMD,
                    //    new List<Algorithm> {
                    //        new Algorithm(MinerBaseType.XmrStakAMD, AlgorithmType.CryptoNight, "")
                    //    }
                    //}
                };
            }
            // NVIDIA

            if (DeviceGroupType.NVIDIA_2_1 == deviceGroupType || DeviceGroupType.NVIDIA_3_x == deviceGroupType || DeviceGroupType.NVIDIA_5_x == deviceGroupType || DeviceGroupType.NVIDIA_6_x == deviceGroupType)
            {
                var ToRemoveAlgoTypes = new List<AlgorithmType>();
                var ToRemoveMinerTypes = new List<MinerBaseType>();
                var ret = new Dictionary<MinerBaseType, List<Algorithm>>() {
                    { MinerBaseType.ccminer,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer, AlgorithmType.Skunk, "skunk"),
                            new Algorithm(MinerBaseType.ccminer, AlgorithmType.Groestl, "groestl"),
                            new Algorithm(MinerBaseType.ccminer, AlgorithmType.Myriad_groestl, "myr-gr")
                        }
                    },
                    { MinerBaseType.ccminer_22,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_22, AlgorithmType.Bitcore, "bitcore")
                        }
                    },
                    { MinerBaseType.ccminer_alexis_hsr,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_alexis_hsr, AlgorithmType.Hsr, "hsr")
                        }
                    },
                    { MinerBaseType.ccminer_alexis78,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_alexis78, AlgorithmType.C11, "c11")
                        }
                    },
                    { MinerBaseType.ccminer_polytimos,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Veltor, "veltor"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Lbry, "lbry"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Keccak, "keccak"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Skein, "skein"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Polytimos, "poly"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Nist5, "nist5"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Lyra2REv2, "lyra2v2"),
                            new Algorithm(MinerBaseType.ccminer_polytimos, AlgorithmType.Blake2s, "blake2s")
                        }
                    },
                    { MinerBaseType.ccminer_xevan,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_xevan, AlgorithmType.Xevan, "xevan")
                        }
                    },
                    { MinerBaseType.ccminer_palgin,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_palgin, AlgorithmType.X11Gost, "sib"),
                            new Algorithm(MinerBaseType.ccminer_palgin, AlgorithmType.X17, "x17"),
                            new Algorithm(MinerBaseType.ccminer_palgin, AlgorithmType.Blake256r8, "blakecoin")
                        }
                    },
                    { MinerBaseType.ccminer_skunkkrnlx,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_skunkkrnlx, AlgorithmType.Timetravel, "timetravel")
                        }
                    },
                    { MinerBaseType.ccminer_tpruvot2,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_tpruvot2, AlgorithmType.NeoScrypt, "neoscrypt"),
                            new Algorithm(MinerBaseType.ccminer_tpruvot2, AlgorithmType.Skunk, "skunk"),
                            new Algorithm(MinerBaseType.ccminer_tpruvot2, AlgorithmType.Tribus, "tribus"),
                            new Algorithm(MinerBaseType.ccminer_tpruvot2, AlgorithmType.Phi, "phi")
                        }
                    },
                    { MinerBaseType.ccminer_alexis,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ccminer_alexis, AlgorithmType.X11Gost, "sib"),
                            new Algorithm(MinerBaseType.ccminer_alexis, AlgorithmType.Nist5, "nist5"),
                            new Algorithm(MinerBaseType.ccminer_alexis, AlgorithmType.X17, "x17")
                        }
                    },
                    //Add HSRNeoscrypt by Palgin
                    { MinerBaseType.hsrneoscrypt,
                         new List<Algorithm>() {
                             new Algorithm(MinerBaseType.hsrneoscrypt, AlgorithmType.NeoScrypt, "Neoscrypt"),
                         }
                     },
                    { MinerBaseType.hsrneoscrypt_hsr,
                         new List<Algorithm>() {
                             new Algorithm(MinerBaseType.hsrneoscrypt_hsr, AlgorithmType.Hsr, "hsr")
                         }
                     },
                    //{ MinerBaseType.ethminer,
                    //    new List<Algorithm>() {
                    //        new Algorithm(MinerBaseType.ethminer, AlgorithmType.DaggerHashimoto, "daggerhashimoto")
                    //    }
                    //},
                    { MinerBaseType.nheqminer,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.nheqminer, AlgorithmType.Equihash, "equihash")
                        }
                    },
                    { MinerBaseType.excavator,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.excavator, AlgorithmType.Equihash, "equihash"),
                            //new Algorithm(MinerBaseType.excavator, AlgorithmType.Pascal, "pascal")
                        }
                    },
                    { MinerBaseType.EWBF,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.EWBF, AlgorithmType.Equihash, "")
                        }
                    },
                    { MinerBaseType.DSTM,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.DSTM, AlgorithmType.Equihash, "")
                        }
                    },
                    //{ MinerBaseType.Claymore,
                    //    new List<Algorithm>() {
                    //        new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, ""),
                    //        new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Decred),
                    //        new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Lbry),
                    //        new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Pascal),
                    //        new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Sia)
                    //    }
                    //}
                };

                if (DeviceGroupType.NVIDIA_6_x == deviceGroupType || DeviceGroupType.NVIDIA_5_x == deviceGroupType)
                {
                    ToRemoveMinerTypes.AddRange(new MinerBaseType[] {
                        MinerBaseType.nheqminer
                    });
                }

                if (DeviceGroupType.NVIDIA_2_1 == deviceGroupType || DeviceGroupType.NVIDIA_3_x == deviceGroupType)
                {
                    ToRemoveAlgoTypes.AddRange(new AlgorithmType[] {
                        AlgorithmType.NeoScrypt,
                        AlgorithmType.Lyra2RE,
                        AlgorithmType.Lyra2REv2
                    });
                    ToRemoveMinerTypes.AddRange(new MinerBaseType[] {
                        MinerBaseType.eqm,
                        MinerBaseType.excavator,
                        MinerBaseType.EWBF,
                        MinerBaseType.DSTM
                    });
                }
                if (DeviceGroupType.NVIDIA_2_1 == deviceGroupType)
                {
                    ToRemoveAlgoTypes.AddRange(new AlgorithmType[] {
                        AlgorithmType.DaggerHashimoto,
                        AlgorithmType.CryptoNight,
                        AlgorithmType.Pascal,
                        AlgorithmType.X11Gost
                    });
                    ToRemoveMinerTypes.AddRange(new MinerBaseType[] {
                        MinerBaseType.Claymore
                    });
                }

                // filter unused
                var finalRet = FilterMinerAlgos(ret, ToRemoveAlgoTypes, new List<MinerBaseType>() { MinerBaseType.ccminer });
                finalRet = FilterMinerBaseTypes(finalRet, ToRemoveMinerTypes);

                return finalRet;
            }

            return null;
        }

        /// <summary>
        /// The FilterMinerBaseTypes
        /// </summary>
        /// <param name="minerAlgos">The <see cref="Dictionary{MinerBaseType, List{Algorithm}}"/></param>
        /// <param name="toRemove">The <see cref="List{MinerBaseType}"/></param>
        /// <returns>The <see cref="Dictionary{MinerBaseType, List{Algorithm}}"/></returns>
        private static Dictionary<MinerBaseType, List<Algorithm>> FilterMinerBaseTypes(Dictionary<MinerBaseType, List<Algorithm>> minerAlgos, List<MinerBaseType> toRemove)
        {
            var finalRet = new Dictionary<MinerBaseType, List<Algorithm>>();
            foreach (var kvp in minerAlgos)
            {
                if (toRemove.IndexOf(kvp.Key) == -1)
                {
                    finalRet[kvp.Key] = kvp.Value;
                }
            }
            return finalRet;
        }

        /// <summary>
        /// The FilterMinerAlgos
        /// </summary>
        /// <param name="minerAlgos">The <see cref="Dictionary{MinerBaseType, List{Algorithm}}"/></param>
        /// <param name="toRemove">The <see cref="List{AlgorithmType}"/></param>
        /// <param name="toRemoveBase">The <see cref="List{MinerBaseType}"/></param>
        /// <returns>The <see cref="Dictionary{MinerBaseType, List{Algorithm}}"/></returns>
        private static Dictionary<MinerBaseType, List<Algorithm>> FilterMinerAlgos(Dictionary<MinerBaseType, List<Algorithm>> minerAlgos, List<AlgorithmType> toRemove, List<MinerBaseType> toRemoveBase = null)
        {
            var finalRet = new Dictionary<MinerBaseType, List<Algorithm>>();
            if (toRemoveBase == null)
            { // all minerbasekeys
                foreach (var kvp in minerAlgos)
                {
                    var algoList = kvp.Value.FindAll((a) => toRemove.IndexOf(a.NiceHashID) == -1);
                    if (algoList.Count > 0)
                    {
                        finalRet[kvp.Key] = algoList;
                    }
                }
            }
            else
            {
                foreach (var kvp in minerAlgos)
                {
                    // filter only if base key is defined
                    if (toRemoveBase.IndexOf(kvp.Key) > -1)
                    {
                        var algoList = kvp.Value.FindAll((a) => toRemove.IndexOf(a.NiceHashID) == -1);
                        if (algoList.Count > 0)
                        {
                            finalRet[kvp.Key] = algoList;
                        }
                    }
                    else
                    { // keep all
                        finalRet[kvp.Key] = kvp.Value;
                    }
                }
            }
            return finalRet;
        }
    }
}
