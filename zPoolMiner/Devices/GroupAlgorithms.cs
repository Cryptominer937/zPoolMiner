namespace zPoolMiner.Devices
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
                            int Lyra2REv2_Index = sgminerAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.Lyra2REv2);
                            //int NeoScrypt_Index = sgminerAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.NeoScrypt);
                            int cryptonight_Index = sgminerAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.cryptonight);

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
                            if (cryptonight_Index > -1)
                            {
                                if (device.Codename.Contains("Hawaii"))
                                {
                                    sgminerAlgos[cryptonight_Index].ExtraLaunchParameters = "--rawintensity 640 -w 8 -g 2";
                                }
                                else if (device.Name.Contains("Vega"))
                                {
                                    sgminerAlgos[cryptonight_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + " --rawintensity 1850 -w 8 -g 2";
                                }
                            }
                        }
                        if (algoSettings.ContainsKey(MinerBaseType.GatelessGate))
                        {
                            var glgAlgos = algoSettings[MinerBaseType.GatelessGate];
                            int Lyra2REv2_Index = glgAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.Lyra2REv2);
                            int NeoScrypt_Index = glgAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.NeoScrypt);
                            int cryptonight_Index = glgAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.cryptonight);


                            // Check for optimized version
                            if (!device.Codename.Contains("Tahiti") && NeoScrypt_Index > -1)
                            {
                                glgAlgos[NeoScrypt_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + "--intensity 13 --worksize  256 --gpu-threads 1";
                                Helpers.ConsolePrint("ComputeDevice", "The GPU detected (" + device.Codename + ") is not Tahiti. Changing default gpu-threads to 2.");
                            }
                            if (cryptonight_Index > -1)
                            {
                                if (device.Codename.Contains("gfx804")) //rx550
                                {
                                    glgAlgos[cryptonight_Index].ExtraLaunchParameters = "--rawintensity 448 -w 8 -g 2";
                                }
                                if (device.Codename.Contains("Pitcairn")) //r7-370
                                {
                                    glgAlgos[cryptonight_Index].ExtraLaunchParameters = "--rawintensity 416 -w 8 -g 2";
                                }
                                if (device.Codename.Contains("Baffin")) //rx460/560
                                {
                                    glgAlgos[cryptonight_Index].ExtraLaunchParameters = "--rawintensity 448 -w 8 -g 2";
                                }

                                if (device.Codename.Contains("Ellesmere")) //rx570/580
                                {
                                    glgAlgos[cryptonight_Index].ExtraLaunchParameters = "--intensity 13 --worksize  256 --gpu-threads 1";
                                }

                                if (device.Codename.Contains("Hawaii"))
                                {
                                    glgAlgos[cryptonight_Index].ExtraLaunchParameters = "--rawintensity 640 -w 8 -g 2";
                                }
                                else if (device.Name.Contains("Vega"))
                                {
                                    glgAlgos[cryptonight_Index].ExtraLaunchParameters = AmdGpuDevice.DefaultParam + " --rawintensity 1850 -w 8 -g 2";
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
                                    if (algo.CryptoMiner937ID == AlgorithmType.Decred || algo.CryptoMiner937ID == AlgorithmType.Lbry)
                                    {
                                        algo.Enabled = false;
                                    }
                                }
                            }
                        }
                        // non sgminer optimizations
                        if (algoSettings.ContainsKey(MinerBaseType.Claymore))
                        {


                            var claymoreNewAlgos = algoSettings[MinerBaseType.Claymore];
                            var cryptonightNewIndex =
                                claymoreNewAlgos.FindIndex(el => el.CryptoMiner937ID == AlgorithmType.cryptonight);

                            if (cryptonightNewIndex > -1)
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
                                    claymoreNewAlgos[cryptonightNewIndex].Enabled = !isOld;
                                }
                            }
                        }

                        // drivers algos issue
                        if (device.DriverDisableAlgos)
                        {
                            //algoSettings = FilterMinerAlgos(algoSettings, new List<AlgorithmType> { AlgorithmType.NeoScrypt, AlgorithmType.Lyra2REv2 });
                            //algoSettings = FilterMinerAlgos(algoSettings, new List<AlgorithmType> { AlgorithmType.Lyra2REv2 });
                        }

                        if (algoSettings.ContainsKey(MinerBaseType.mkxminer))
                        {
                            var mkxminerAlgos = algoSettings[MinerBaseType.mkxminer];
                            int Lyra2REv2_Index = mkxminerAlgos.FindIndex((el) => el.CryptoMiner937ID == AlgorithmType.Lyra2REv2);

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
                        }

                        // disable by default
                        {
                            var minerBases = new List<MinerBaseType>() { MinerBaseType.OptiminerAMD };
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
                                    if (algo.CryptoMiner937ID == AlgorithmType.DaggerHashimoto)
                                    {
                                        algo.Enabled = false;
                                    }
                                }
                            }
                            if (algoSettings.ContainsKey(MinerBaseType.Claymore))
                            {
                                foreach (var algo in algoSettings[MinerBaseType.Claymore])
                                {
                                    if (algo.CryptoMiner937ID == AlgorithmType.cryptonight)
                                    {
                                        algo.Enabled = false;
                                    }
                                    if (device.Codename.Contains("gfx804"))
                                    {
                                        if (algo.CryptoMiner937ID == AlgorithmType.NeoScrypt)
                                        {
                                            algo.Enabled = false;
                                        }
                                    }
                                    if (algoSettings.ContainsKey(MinerBaseType.lolMinerAmd))
                                    {

                                    }
                                }
                            }
                        }
                    } // END AMD case


                    // check if it is Etherum capable
                    if (device.IsEtherumCapale == false)
                    {
                        algoSettings = FilterMinerAlgos(algoSettings, new List<AlgorithmType> { AlgorithmType.DaggerHashimoto });
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
                    //    int equihash_index = algoSettings[MinerBaseType.excavator].FindIndex((algo) => algo.CryptoMiner937ID == AlgorithmType.equihash);
                    //    if (equihash_index > -1) {
                    //        // -c1 1 needed for SM50 to work ATM
                    //        algoSettings[MinerBaseType.excavator][equihash_index].ExtraLaunchParameters = "-c1 1";
                    //    }
                    //}
                    // nheqminer exceptions scope
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
                    { MinerBaseType.cpuminer,
                        new List<Algorithm>()
                        {
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Lyra2REv2, "lyra2rev2"),
                            new Algorithm(MinerBaseType.cpuminer, AlgorithmType.yescrypt, "yescrypt"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.yescryptr16, "yescryptr16"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Blake2s, "blake2s"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Blake256r8, "blakecoin"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Hsr, "x13sm3"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.X17, "x17"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Lbry, "lbry"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Myriad_groestl, "myr-gr"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Skein, "skein"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Timetravel, "timetravel"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Polytimos, "polytimos"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Nist5, "nist5"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Groestl, "groestl"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.lyra2z, "lyra2z"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.hmq1725, "hmq1725"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Skunk, "skunk"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.X11Gost, "x11gost"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Phi, "phi1612"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Tribus, "tribus"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Bitcore, "timetravel10"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.M7M, "m7m"),
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Xevan, "xevan")
                            //new Algorithm(MinerBaseType.cpuminer, AlgorithmType.Keccak, "keccak")

                        }
                    },
                { MinerBaseType.CPU_SRBMiner,
                    new List<Algorithm>() {
                        //new Algorithm(MinerBaseType.Xmrig, AlgorithmType.cryptonight, ""),
                        //new Algorithm(MinerBaseType.Xmrig, AlgorithmType.cryptonightv7, "")
                    }
                },
                { MinerBaseType.CPU_XMRig,
                    new List<Algorithm>() {
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.randomxmonero, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.randomarq, ""),
                        new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.randomx, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.randomsfx, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.cryptonightv7, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.cryptonight_heavy, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.cryptonight_heavyx, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.cryptonight_saber, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.cryptonight_fast, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.cryptonight_haven, ""),
                        //new Algorithm(MinerBaseType.CPU_XMRig, AlgorithmType.chukwa, "")
                    }
                },
                { MinerBaseType.CPU_XMRigUPX,
                    new List<Algorithm>() {
                        new Algorithm(MinerBaseType.CPU_XMRigUPX, AlgorithmType.cryptonight_upx, "")
                    }
                },
                { MinerBaseType.CPU_RKZ,
                        new List<Algorithm>()
                        {
                            new Algorithm(MinerBaseType.CPU_RKZ, AlgorithmType.yescrypt, "yescrypt"),
                            new Algorithm(MinerBaseType.CPU_RKZ, AlgorithmType.yespower, "yespower"),
                            new Algorithm(MinerBaseType.CPU_RKZ, AlgorithmType.cpupower, "cpupower"),
                            //new Algorithm(MinerBaseType.CPU_RKZ, AlgorithmType.power2b, "power2b")

                        }
                    },
                { MinerBaseType.CPU_rplant,
                        new List<Algorithm>()
                        {
                            //new Algorithm(MinerBaseType.CPU_rplant, AlgorithmType.yescryptr8g, "yescryptr8g"),
                            //new Algorithm(MinerBaseType.CPU_rplant, AlgorithmType.yespoweriots, "yespoweriots")

                        }
                    },
                { MinerBaseType.CPU_nosuch,
                        new List<Algorithm>()
                        {
                            new Algorithm(MinerBaseType.CPU_nosuch, AlgorithmType.yescryptr32, "yescryptr32")

                        }
                    },
                { MinerBaseType.CPU_verium,
                        new List<Algorithm>()
                        {
                            new Algorithm(MinerBaseType.CPU_verium, AlgorithmType.scryptn2, "scryptn2")

                        }
                    },
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
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Lbry, "lbry") { ExtraLaunchParameters = DefaultParam + "--xintensity 512 --worksize 128 --gpu-threads 2" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.cryptonight, "cryptonight") { ExtraLaunchParameters = DefaultParam + "--rawintensity 512 -w 4 -g 2" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Pascal, "pascal") { ExtraLaunchParameters = DefaultParam + "--intensity 21 -w 64 -g 2" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.X11Gost, "sibcoin-mod") { ExtraLaunchParameters = DefaultParam + "--intensity 19 -w 128 -g 2" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Keccak, "keccak") { ExtraLaunchParameters = DefaultParam + "--intensity 15" },
                            //Cryptominer937 Additions                            
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Skein, "skeincoin") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 2 --worksize 256 --intensity d" },
                           // new Algorithm(MinerBaseType.sgminer, AlgorithmType.Myriad_groestl, "myriadcoin-groestl") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 2 --worksize 64 --intensity d" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Bitcore, "timetravel10") { ExtraLaunchParameters = DefaultParam + "--intensity 21" },
                           // new Algorithm(MinerBaseType.sgminer, AlgorithmType.Blake2s, "sia") { ExtraLaunchParameters = DefaultParam + "--intensity d" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Hsr, "hsr") { ExtraLaunchParameters = DefaultParam + "--intensity 20" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Phi, "phi") { ExtraLaunchParameters = DefaultParam + "--intensity 21" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Veltor, "veltor") { ExtraLaunchParameters = DefaultParam + "--intensity 19" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Tribus, "tribus") { ExtraLaunchParameters = DefaultParam + "--shaders 1792 --lookup-gap 4 --intensity 19" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.Xevan, "xevan-mod") { ExtraLaunchParameters = DefaultParam + "--intensity 19" },
                            //new Algorithm(MinerBaseType.sgminer, AlgorithmType.x16r, "x16r") { ExtraLaunchParameters = DefaultParam + "--intensity 19" }

                        }
                    },

                    { MinerBaseType.GatelessGate,
                         new List<Algorithm>() {
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.NeoScrypt, "neoscrypt") { ExtraLaunchParameters = DefaultParam + "--intensity 13 --worksize  256 --gpu-threads 1" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Lyra2REv2,  "Lyra2REv2") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 6 --worksize 128 --intensity d" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.equihash, "equihash"){ ExtraLaunchParameters = RemDis + "--xintensity 512 --worksize 256 --gpu-threads 2" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.ethash, "ethash") { ExtraLaunchParameters = DefaultParam + "--xintensity 512 -w 192 -g 1" },
                             //new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Decred, "decred") { ExtraLaunchParameters = RemDis + "--gpu-threads 1 --xintensity 256 --lookup-gap 2 --worksize 64" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Lbry, "lbry") { ExtraLaunchParameters = DefaultParam + "--xintensity 512 --worksize 128 --gpu-threads 2" },
                             //new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.cryptonight, "cryptonight") { ExtraLaunchParameters = DefaultParam + "--rawintensity 512 -w 8 -g 2" },
                             //new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Pascal, "pascal") { ExtraLaunchParameters = DefaultParam + "--intensity 21 -w 64 -g 2" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.X11Gost, "sibcoin-mod") { ExtraLaunchParameters = DefaultParam + "--intensity 16 -w 64 -g 2" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Skein, "skein") { ExtraLaunchParameters = DefaultParam + "--intensity d --gpu-threads 2" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Myriad_groestl, "myriadcoin-groestl") { ExtraLaunchParameters = DefaultParam + "--gpu-threads 2 --worksize 64 --intensity d" },
                             new Algorithm(MinerBaseType.GatelessGate, AlgorithmType.Keccak, "keccak") { ExtraLaunchParameters = DefaultParam + "--intensity d --gpu-threads 2" }
                         }
                     },
                    { MinerBaseType.mkxminer,
                         new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.mkxminer, AlgorithmType.Lyra2REv2,  "Lyra2REv2") { ExtraLaunchParameters = "--exitsick -I 23" }
                         }
                     },
                    { MinerBaseType.Claymore,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.cryptonight, "cryptonight"),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.equihash, "equihash"),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.NeoScrypt, "neoscrypt"){ ExtraLaunchParameters = AmdGpuDevice.DefaultParam + "-powlim 10" },
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, ""),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Decred),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Lbry),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Pascal),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Sia),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Blake2s)
                        }
                    },
                    /*{ MinerBaseType.Claymore_old,
                        new List<Algorithm> {
                            new Algorithm(MinerBaseType.Claymore_old, AlgorithmType.cryptonight, "old")
                        }
                    },*/
                    { MinerBaseType.OptiminerAMD,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.OptiminerAMD, AlgorithmType.equihash, "equihash")
                        }
                    },/*,
                    { MinerBaseType.XmrStakAMD,
                        new List<Algorithm> {
                            new Algorithm(MinerBaseType.XmrStakAMD, AlgorithmType.cryptonight, "")
                        }
                    }*/{MinerBaseType.lolMinerAmd,
                new List<Algorithm>() {
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.karlsenhash,"karlsenhash"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.pyrinhash,"pyrinhash"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.ethash,"ethash"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.ethashb3,"ethashb3"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.nexapow,"nexapow"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.sha512256d,"sha512256d"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.equihash144,"equihash144"),
                    new Algorithm(MinerBaseType.lolMinerAmd,AlgorithmType.equihash192,"equihash192")
                }
                }
            };
            }
            // NVIDIA

            if (DeviceGroupType.NVIDIA_2_1 == deviceGroupType || DeviceGroupType.NVIDIA_3_x == deviceGroupType || DeviceGroupType.NVIDIA_5_x == deviceGroupType || DeviceGroupType.NVIDIA_6_x == deviceGroupType)
            {
                var ToRemoveAlgoTypes = new List<AlgorithmType>();
                var ToRemoveMinerTypes = new List<MinerBaseType>();
                var ret = new Dictionary<MinerBaseType, List<Algorithm>>() {
                    { MinerBaseType.lolMinerNvidia,
                        new List<Algorithm>() {
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.karlsenhash,"karlsenhash"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.pyrinhash,"pyrinhash"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.ethash,"ethash"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.ethashb3,"ethashb3"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.nexapow,"nexapow"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.sha512256d,"sha512256d"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.equihash144,"equihash144"),
                    new Algorithm(MinerBaseType.lolMinerNvidia,AlgorithmType.equihash192,"equihash192")
                        }
                    },
                    { MinerBaseType.ccminer_tpruvot2,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.ccminer_tpruvot2, AlgorithmType.NeoScrypt, "neoscrypt"),
                        }
                    },
                    { MinerBaseType.Claymore,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, ""),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Decred),
                           // new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Lbry),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Pascal),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Sia),
                            //new Algorithm(MinerBaseType.Claymore, AlgorithmType.DaggerHashimoto, "", AlgorithmType.Blake2s)
                        }
                    },
                    { MinerBaseType.trex,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.trex, AlgorithmType.lyra2z, "Lyra2z"),
                            new Algorithm(MinerBaseType.trex, AlgorithmType.x16r, "x16r"),
                        }
                     },
                    { MinerBaseType.ZEnemy,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.ZEnemy, AlgorithmType.x16r, "x16r"),
                        }
                    },
                    { MinerBaseType.CryptoDredge16,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.CryptoDredge16, AlgorithmType.allium, "allium"),
                            new Algorithm(MinerBaseType.CryptoDredge16, AlgorithmType.NeoScrypt, "neoscrypt"),
                            new Algorithm(MinerBaseType.CryptoDredge16, AlgorithmType.lyra2z, "lyra2z"),
                            new Algorithm(MinerBaseType.CryptoDredge16, AlgorithmType.x16r, "x16r"),
                            //new Algorithm(MinerBaseType.CryptoDredge16, AlgorithmType.x16rt, "x16rt"),
                            new Algorithm(MinerBaseType.CryptoDredge16, AlgorithmType.x21s, "x21s"),
                        }
                    },{ MinerBaseType.CryptoDredge25,
                        new List<Algorithm>() {
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.allium, "allium"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.NeoScrypt, "neoscrypt"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.cryptonight_upx, "cryptonight_upx"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.cryptonight_gpu, "cryptonight_gpu"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.lyra2z, "lyra2z"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.x16r, "x16r"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.x16rt, "x16rt"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.x16rv2, "x16rv2"),
                            new Algorithm(MinerBaseType.CryptoDredge25, AlgorithmType.x21s, "x21s"),
                        }
                    },{ MinerBaseType.CryptoDredge26,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.CryptoDredge26, AlgorithmType.kawpow, "kawpow"),
                            //new Algorithm(MinerBaseType.CryptoDredge26, AlgorithmType.cryptonight_upx, "cryptonight_upx"),
                        }
                    },
                    { MinerBaseType.MiniZ,
                        new List<Algorithm>() {
                            //new Algorithm(MinerBaseType.MiniZ, AlgorithmType.equihash96, "equihash96"),
                            new Algorithm(MinerBaseType.MiniZ, AlgorithmType.equihash125, "equihash125"),
                            new Algorithm(MinerBaseType.MiniZ, AlgorithmType.equihash144, "equihash144"),
                            new Algorithm(MinerBaseType.MiniZ, AlgorithmType.equihash192, "equihash192"),
                        }
                    },
                };



                if (DeviceGroupType.NVIDIA_6_x == deviceGroupType)
                {
                    ToRemoveMinerTypes.AddRange(new MinerBaseType[] {

                    });
                }
                if (DeviceGroupType.NVIDIA_5_x == deviceGroupType)
                {
                    ToRemoveMinerTypes.AddRange(new MinerBaseType[] {
                        MinerBaseType.lolMinerNvidia

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
                        
                        //MinerBaseType.Palgin_HSR,
                        //MinerBaseType.Palgin_Neoscrypt
                        MinerBaseType.lolMinerNvidia
                    });
                }
                if (DeviceGroupType.NVIDIA_2_1 == deviceGroupType)
                {
                    ToRemoveAlgoTypes.AddRange(new AlgorithmType[] {
                        AlgorithmType.DaggerHashimoto,
                        AlgorithmType.cryptonight,
                        AlgorithmType.Pascal,
                        AlgorithmType.X11Gost
                    });
                    ToRemoveMinerTypes.AddRange(new MinerBaseType[] {
                        MinerBaseType.Claymore,
                        MinerBaseType.lolMinerNvidia
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
                    var algoList = kvp.Value.FindAll((a) => toRemove.IndexOf(a.CryptoMiner937ID) == -1);
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
                        var algoList = kvp.Value.FindAll((a) => toRemove.IndexOf(a.CryptoMiner937ID) == -1);
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
