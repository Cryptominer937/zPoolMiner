namespace zPoolMiner.Miners.Parsing
{
    using System;
    using System.Collections.Generic;
    using zPoolMiner.Configs.ConfigJsonFile;
    using zPoolMiner.Enums;

    /// <summary>
    /// Defines the <see cref="MinerOptionPackageFile" />
    /// </summary>
    internal class MinerOptionPackageFile : ConfigFile<MinerOptionPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinerOptionPackageFile"/> class.
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        public MinerOptionPackageFile(string name)
            : base(FOLDERS.INTERNALS, String.Format("{0}.json", name), String.Format("{0}.json", name))
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="ExtraLaunchParameters" />
    /// </summary>
    public static class ExtraLaunchParameters
    {
        /// <summary>
        /// Defines the DEFAULTS
        /// </summary>
        private static readonly List<MinerOptionPackage> DEFAULTS = new List<MinerOptionPackage>() {
            new MinerOptionPackage(
                MinerType.ccminer,
                new List<MinerOption>() {
                    new MinerOption("Intensity", "-i", "--intensity=", "0", MinerOptionFlagType.MultiParam, ",")
                },
                new List<MinerOption>()
            ),
               /*new MinerOptionPackage(
                 MinerType.Palgin_Neoscrypt,
                 new List<MinerOption>() {
                     new MinerOption("Intensity", "-i", "--intensity=", "0", MinerOptionFlagType.MultiParam, ",")
                 },
                 new List<MinerOption>()
            ),*/
            new MinerOptionPackage(
                MinerType.ccminer_cryptonight,
                new List<MinerOption>() {
                    new MinerOption("Launch", "-l", "--launch=", "8x20", MinerOptionFlagType.MultiParam, ","), // default is 8x20
                    new MinerOption("Bfactor", "", "--bfactor=", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Bsleep", "", "--bsleep=", "0", MinerOptionFlagType.MultiParam, ",") // TODO check default
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.trex,
                 new List<MinerOption>() {
                     new MinerOption("Intensity", "-i", "--intensity ", "0", MinerOptionFlagType.MultiParam, ",")
                 },
                 new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.MiniZ,
                 new List<MinerOption>() {
                     new MinerOption("Intensity", "-i", "--intensity ", "0", MinerOptionFlagType.MultiParam, ",")
                 },
                 new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.CryptoDredge,
                new List<MinerOption>() {
                    new MinerOption("Intensity", "-i", "--intensity=", "0", MinerOptionFlagType.MultiParam, ",")
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.ZEnemy,
                new List<MinerOption>() {
                    new MinerOption("Intensity", "-i", "--intensity=", "0", MinerOptionFlagType.MultiParam, ",")
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.ethminer_OCL,
                new List<MinerOption>() {
                    new MinerOption("LocalWork", "", "--cl-local-work", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("GlobalWork", "", "--cl-global-work", "0", MinerOptionFlagType.MultiParam, ","),
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.ethminer_CUDA,
                new List<MinerOption>() {
                    new MinerOption("CudaBlockSize", "", "--cuda-block-size", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("CudaGridSize", "", "--cuda-grid-size", "0", MinerOptionFlagType.MultiParam, ","),
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.sgminer,
                new List<MinerOption>() {
                    // SingleParam
                    new MinerOption("KeccakUnroll", "", "--keccak-unroll", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("HamsiExpandBig", "", "--hamsi-expand-big", "4", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("Nfactor", "", "--nfactor", "10", MinerOptionFlagType.SingleParam, ""),
                    // MultiParam TODO IMPORTANT check defaults
                    new MinerOption("Intensity", "-I", "--intensity", "d", MinerOptionFlagType.MultiParam, ","), // default is "d" check if -1 works
                    new MinerOption("Xintensity", "-X", "--xintensity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("Rawintensity", "", "--rawintensity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("ThreadConcurrency", "", "--thread-concurrency", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("Worksize", "-w", "--worksize", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("GpuThreads", "-g", "--gpu-threads", "1", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("LookupGap", "", "--lookup-gap", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    // Uni
                    new MinerOption("RemoveDisabled", "--remove-disabled", "--remove-disabled", "", MinerOptionFlagType.Uni, ""), // default none
                },
                // TemperatureOptions
                new List<MinerOption>() {
                    new MinerOption("GpuFan", "", "--gpu-fan", "30-60", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("TempCutoff", "", "--temp-cutoff", "95", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("TempOverheat", "", "--temp-overheat", "85", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("TempTarget", "", "--temp-target", "75", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("AutoFan", "", "--auto-fan", null, MinerOptionFlagType.Uni, ""),
                    new MinerOption("AutoGpu", "", "--auto-gpu", null, MinerOptionFlagType.Uni, "")
                }
            ),
            new MinerOptionPackage(
                MinerType.lolMinerAmd,
                new List<MinerOption>() {
                    // SingleParam
                    new MinerOption("lolMiner_log", "", "--logs", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_log", "", "--logs", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_enablezilcache", "--enablezilcache=", "--enablezilcache=", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_win4galloc", "", "--win4galloc", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_4gallocsize", "", "--4g-alloc-size", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_keepfree", "", "--keepfree", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_zombie-tune", "", "--zombie-tune", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_lhrtune", "", "--lhrtune", "auto", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_cclk", "", "--cclk", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_mclk", "", "--mclk", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_coff", "", "--coff", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_moff", "", "--moff", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_pl", "", "--pl", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_fan", "", "--fan", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_ergo-prebuild", "--ergo-prebuild", "--ergo-prebuild", "0", MinerOptionFlagType.MultiParam, ","),
                    //new MinerOption("lolMinerasm", "", "--asm", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMinerbasecolor", "", "--basecolor", "", MinerOptionFlagType.Uni, ""),
                    new MinerOption("lolMinernocolor", "", "--nocolor", "", MinerOptionFlagType.Uni, ""),
                    new MinerOption("lolMiner_lhrwait", "--lhrwait", "--lhrwait", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_screen ", "--screen ", "--screen", "0", MinerOptionFlagType.SingleParam, ""),
                },
                // TemperatureOptions
                new List<MinerOption>() {
                }
            ),
            new MinerOptionPackage(
                MinerType.lolMinerNvidia,
                new List<MinerOption>() {
                    // SingleParam
                    new MinerOption("lolMiner_log", "", "--logs", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_log", "", "--logs", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_enablezilcache", "--enablezilcache=", "--enablezilcache=", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_win4galloc", "", "--win4galloc", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_4gallocsize", "", "--4g-alloc-size", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_keepfree", "", "--keepfree", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_zombie-tune", "", "--zombie-tune", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_lhrtune", "", "--lhrtune", "auto", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_cclk", "", "--cclk", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_mclk", "", "--mclk", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_coff", "", "--coff", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_moff", "", "--moff", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_pl", "", "--pl", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_fan", "", "--fan", "*", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("lolMiner_ergo-prebuild", "--ergo-prebuild", "--ergo-prebuild", "0", MinerOptionFlagType.MultiParam, ","),
                    //new MinerOption("lolMinerasm", "", "--asm", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMinerbasecolor", "", "--basecolor", "", MinerOptionFlagType.Uni, ""),
                    new MinerOption("lolMinernocolor", "", "--nocolor", "", MinerOptionFlagType.Uni, ""),
                    new MinerOption("lolMiner_lhrwait", "--lhrwait", "--lhrwait", "0", MinerOptionFlagType.SingleParam, ""),
                    new MinerOption("lolMiner_screen ", "--screen ", "--screen", "0", MinerOptionFlagType.SingleParam, ""),
                },
                // TemperatureOptions
                new List<MinerOption>() {
                }
            ),
           /* new MinerOptionPackage(
                 MinerType.mkxminer,
                 new List<MinerOption>() {
                     // SingleParam
                     new MinerOption("ExitSick", "", "--exitsick", null, MinerOptionFlagType.SingleParam, ""),
                     new MinerOption("Asm", "", "--asm", null, MinerOptionFlagType.SingleParam, ""),
                     // MultiParam TODO IMPORTANT check defaults
                     new MinerOption("GPUclock" , "", "--engine", "-1", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("Memclock" , "", "--memclock", "-1", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("Intensity", "-I", "--intensity", "d", MinerOptionFlagType.MultiParam, ","), // default is "d" check if -1 works
                     new MinerOption("Powertune", "", "--powertune", "-1", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("GPUvoltage", "", "--vddc", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                 },
                 // TemperatureOptions
                 new List<MinerOption>() {
                     new MinerOption("GpuFan", "", "--fan", "30-95", MinerOptionFlagType.MultiParam, ","), // default none
                     new MinerOption("TempCutoff", "", "--temp-cutoff", "95", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("TempOverheat", "", "--temp-overheat", "85", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("TempTarget", "", "--temp-target", "75", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("AutoFan", "", "--auto-fan", null, MinerOptionFlagType.Uni, ""),
                     new MinerOption("AutoGpu", "", "--auto-gpu", null, MinerOptionFlagType.Uni, "")
                 }
             ),*/
            new MinerOptionPackage(
                 MinerType.glg,
                 new List<MinerOption>() {
                     // SingleParam
                     new MinerOption("KeccakUnroll", "", "--keccak-unroll", "0", MinerOptionFlagType.SingleParam, ""),
                     new MinerOption("HamsiExpandBig", "", "--hamsi-expand-big", "4", MinerOptionFlagType.SingleParam, ""),
                     new MinerOption("Nfactor", "", "--nfactor", "10", MinerOptionFlagType.SingleParam, ""),
                     // MultiParam TODO IMPORTANT check defaults
                     new MinerOption("Intensity", "-I", "--intensity", "d", MinerOptionFlagType.MultiParam, ","), // default is "d" check if -1 works
                     new MinerOption("Xintensity", "-X", "--xintensity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                     new MinerOption("Rawintensity", "", "--rawintensity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                     new MinerOption("ThreadConcurrency", "", "--thread-concurrency", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                     new MinerOption("Worksize", "-w", "--worksize", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                     new MinerOption("GpuThreads", "-g", "--gpu-threads", "1", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("LookupGap", "", "--lookup-gap", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                     // Uni
                     new MinerOption("RemoveDisabled", "--remove-disabled", "--remove-disabled", "", MinerOptionFlagType.Uni, ""), // default none
                 },
                 // TemperatureOptions
                 new List<MinerOption>() {
                     new MinerOption("GpuFan", "", "--gpu-fan", "30-60", MinerOptionFlagType.MultiParam, ","), // default none
                     new MinerOption("TempCutoff", "", "--temp-cutoff", "95", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("TempOverheat", "", "--temp-overheat", "85", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("TempTarget", "", "--temp-target", "75", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("AutoFan", "", "--auto-fan", null, MinerOptionFlagType.Uni, ""),
                     new MinerOption("AutoGpu", "", "--auto-gpu", null, MinerOptionFlagType.Uni, "")
                 }
             ),
            new MinerOptionPackage(
                MinerType.cpuminer_opt,
                new List<MinerOption>() {
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuAffinity", "", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuPriority", "", "--cpu-priority", "-1", MinerOptionFlagType.MultiParam, ",") // default
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.nheqminer_CPU,
                new List<MinerOption>() {
                    new MinerOption("Threads", "-t", "-t", "-1", MinerOptionFlagType.SingleParam, " "), // default none
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.nheqminer_CUDA,
                new List<MinerOption>() {
                    new MinerOption("Solver_Version", "-cv", "-cv", "0", MinerOptionFlagType.SingleParam, " "), // default 0
                    new MinerOption("Solver_Block", "-cb", "-cb", "0", MinerOptionFlagType.MultiParam, " "), // default 0
                    new MinerOption("Solver_Thread", "-ct", "-ct", "0", MinerOptionFlagType.MultiParam, " "), // default 0
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.eqm_CUDA,
                new List<MinerOption>() {
                    new MinerOption("Solver_Mode", "-cm", "-cm", "0", MinerOptionFlagType.MultiParam, " "), // default 0
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.ClaymoreZcash,
                new List<MinerOption>() {
                    new MinerOption("ClaymoreZcash_a"      , "-a", "-a", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_asm"      , "-asm", "-asm", "1", MinerOptionFlagType.MultiParam, ","),

                    new MinerOption("ClaymoreZcash_i"      , "-i", "-i", "6", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_wd"     , "-wd", "-wd", "1", MinerOptionFlagType.SingleParam, ","),
                    //new MinerOption(ClaymoreZcash_r      , , , , MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_nofee"  , "-nofee", "-nofee", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreZcash_li"     , "-li", "-li", "0", MinerOptionFlagType.MultiParam, ","),

                    //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                    new MinerOption("ClaymoreZcash_cclock" , "-cclock", "-cclock", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_mclock" , "-mclock", "-mclock", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_powlim" , "-powlim", "-powlim", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_cvddc"  , "-cvddc", "-cvddc", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_mvddc"  , "-mvddc", "-mvddc", "0", MinerOptionFlagType.MultiParam, ","),
                },
                new List<MinerOption>() {
                    // temperature stuff
                    //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                    new MinerOption("ClaymoreZcash_tt"     , "-tt", "-tt", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreZcash_ttli"   , "-ttli", "-ttli", "70", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreZcash_tstop"  , "-tstop", "-tstop", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreZcash_fanmax" , "-fanmax", "-fanmax", "100", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreZcash_fanmin" , "-fanmin", "-fanmin", "0", MinerOptionFlagType.MultiParam, ","),
                }
            ),
            new MinerOptionPackage(
                 MinerType.ClaymoreNeoScrypt,
                 new List<MinerOption>() {
                     new MinerOption("ClaymoreNeoscrypt_a"      , "-a", "-a", "1", MinerOptionFlagType.MultiParam, ","),

                     new MinerOption("ClaymoreNeoscrypt_wd"     , "-wd", "-wd", "1", MinerOptionFlagType.SingleParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_nofee"  , "-nofee", "-nofee", "0", MinerOptionFlagType.SingleParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_li"     , "-li", "-li", "0", MinerOptionFlagType.MultiParam, ","),
 
                     //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                     new MinerOption("ClaymoreNeoscrypt_cclock" , "-cclock", "-cclock", "0", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_mclock" , "-mclock", "-mclock", "0", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_powlim" , "-powlim", "-powlim", "0", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_cvddc"  , "-cvddc", "-cvddc", "0", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_mvddc"  , "-mvddc", "-mvddc", "0", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_colors"  , "-colors", "-colors", "1", MinerOptionFlagType.MultiParam, ","),
                 },
                 new List<MinerOption>() {
                     // temperature stuff
                     //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                     new MinerOption("ClaymoreNeoscrypt_tt"     , "-tt", "-tt", "1", MinerOptionFlagType.SingleParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_ttli"   , "-ttli", "-ttli", "70", MinerOptionFlagType.SingleParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_tstop"  , "-tstop", "-tstop", "0", MinerOptionFlagType.SingleParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_fanmax" , "-fanmax", "-fanmax", "100", MinerOptionFlagType.MultiParam, ","),
                     new MinerOption("ClaymoreNeoscrypt_fanmin" , "-fanmin", "-fanmin", "0", MinerOptionFlagType.MultiParam, ","),
                 }
             ),
            new MinerOptionPackage(
                MinerType.Claymorecryptonight,
                new List<MinerOption>() {
                    new MinerOption("Claymorecryptonight_a"      , "-a", "-a", "0", MinerOptionFlagType.MultiParam, ""),
                    new MinerOption("Claymorecryptonight_wd"     , "-wd", "-wd", "1", MinerOptionFlagType.SingleParam, ","),
                    //new MinerOption(Claymorecryptonight_r      , , , , MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_nofee"  , "-nofee", "-nofee", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("Claymorecryptonight_li"     , "-li", "-li", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_h"     , "-h", "-h", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_allpools", "-allpools", "0", MinerOptionFlagType.SingleParam),
                    new MinerOption("Claymorecryptonight_cclock" , "-cclock", "-cclock", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_mclock" , "-mclock", "-mclock", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_powlim" , "-powlim", "-powlim", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_cvddc"  , "-cvddc", "-cvddc", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_mvddc"  , "-mvddc", "-mvddc", "0", MinerOptionFlagType.MultiParam, ","),
                },
                new List<MinerOption>() {
                    // temperature stuff
                    //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                    new MinerOption("Claymorecryptonight_tt"     , "-tt", "-tt", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("Claymorecryptonight_tstop"  , "-tstop", "-tstop", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("Claymorecryptonight_fanmax" , "-fanmax", "-fanmax", "100", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("Claymorecryptonight_fanmin" , "-fanmin", "-fanmin", "0", MinerOptionFlagType.MultiParam, ","),
                }
            ),
            new MinerOptionPackage(
                MinerType.OptiminerZcash,
                new List<MinerOption>() {
                    new MinerOption("ForceGenericKernel"      , "--force-generic-kernel", "--force-generic-kernel", null, MinerOptionFlagType.Uni, ""),
                    new MinerOption("ExperimentalKernel"      , "--experimental-kernel", "--experimental-kernel", null, MinerOptionFlagType.Uni, ""),
                    new MinerOption("nodevfee"                , "--nodevfee", "--nodevfee", null, MinerOptionFlagType.Uni, ""),
                    new MinerOption("Intensity"               , "-i", "--intensity", "0", MinerOptionFlagType.DuplicateMultiParam, ""),
                    new MinerOption("PciMode"               , "--pci-mode", "--pci-mode", "", MinerOptionFlagType.SingleParam, ""),
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.excavator,
                new List<MinerOption>() {
                    // parameters differ according to algorithm
                    new MinerOption("parameter1", "-c1", "-c1", "0", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("parameter2", "-c2", "-c2", "0", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("debug_param", "-f", "-f", "", MinerOptionFlagType.SingleParam, " "),
                    // Overclocking not supported from NHM
                    //new MinerOption("Overclocking_os", "-os", "-os", "", MinerOptionFlagType.MultiParam, " "),
                    //new MinerOption("Overclocking_od", "-od", "-od", "2", MinerOptionFlagType.MultiParam, " "),
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.ClaymoreDual,
                new List<MinerOption>() {
                    new MinerOption("ClaymoreDual_etha"      , "-etha", "-etha", "-1", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_ethi"      , "-ethi", "-ethi", "8", MinerOptionFlagType.MultiParam, ","),

                    new MinerOption("ClaymoreDual_eres"      , "-eres", "-eres", "2", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_erate"     , "-erate", "-erate", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_estale"    , "-estale", "-estale", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_gser"      , "-gser", "-gser", "0", MinerOptionFlagType.SingleParam, ","),

                    new MinerOption("ClaymoreDual_wd"        , "-wd", "-wd", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_retrydelay"  , "-retrydelay", "-retrydelay", "20", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_nofee"  , "-nofee", "-nofee", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_li"     , "-li", "-li", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_lidag"     , "-lidag", "-lidag", "0", MinerOptionFlagType.MultiParam, ","),

                    //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                    new MinerOption("ClaymoreDual_cclock" , "-cclock", "-cclock", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_mclock" , "-mclock", "-mclock", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_powlim" , "-powlim", "-powlim", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_cvddc"  , "-cvddc", "-cvddc", "0", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_mvddc"  , "-mvddc", "-mvddc", "0", MinerOptionFlagType.MultiParam, ","),

                    // other and dual mining features
                    new MinerOption("ClaymoreDual_etht"  , "-etht", "-etht", "200", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_allcoins"  , "-allcoins", "-allcoins", "", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_r"  , "-r", "-r", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_mode"  , "-mode", "-mode", "0", MinerOptionFlagType.SingleParam, ","), // this is not well supported in MultiParam

                    new MinerOption("ClaymoreDual_ftime"  , "-ftime", "-ftime", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_eres"  , "-eres", "-eres", "2", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_dpool"  , "-dpool", "-dpool", "", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_dwal"  , "-dwal", "-dwal", "", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_dpsw"  , "-dpsw", "-dpsw", "", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_dcoin"  , "-dcoin", "-dcoin", "", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_dcri"  , "-dcri", "-dcri", "30", MinerOptionFlagType.MultiParam, ","),

                    new MinerOption("ClaymoreDual_dcrt"  , "-dcrt", "-dcrt", "5", MinerOptionFlagType.SingleParam, ","),
                    // v9.0
                    new MinerOption("ClaymoreDual_asm"  , "-asm", "-asm", "1", MinerOptionFlagType.MultiParam, ","),
                },
                new List<MinerOption>() {
                    // temperature stuff
                    //MinerOptionFlagType.MultiParam might not work corectly due to ADL indexing so use single param to apply to all
                    new MinerOption("ClaymoreDual_tt"     , "-tt", "-tt", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_ttdcr"     , "-ttdcr", "-ttdcr", "1", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_ttli"   , "-ttli", "-ttli", "70", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_tstop"  , "-tstop", "-tstop", "0", MinerOptionFlagType.SingleParam, ","),
                    new MinerOption("ClaymoreDual_fanmax" , "-fanmax", "-fanmax", "100", MinerOptionFlagType.MultiParam, ","),
                    new MinerOption("ClaymoreDual_fanmin" , "-fanmin", "-fanmin", "0", MinerOptionFlagType.MultiParam, ","),
                }
            ),
            new MinerOptionPackage(
                MinerType.EWBF,
                new List<MinerOption>() {
                    // parameters differ according to algorithm
                    new MinerOption("EWBF_fee", "--fee", "--fee", "0", MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_templimit", "--templimit", "--templimit", "90", MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_tempunits", "--tempunits", "--tempunits", "C", MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_eexit", "--eexit", "--eexit", null, MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_solver", "--solver", "--solver", "0", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("EWBF_intensity", "--intensity", "--intensity", "64", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("EWBF_powercalc", "--pec", "--pec", null, MinerOptionFlagType.Uni, " "),
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.DSTM,
                new List<MinerOption>() {
                    // parameters differ according to algorithm
                    new MinerOption("EWBF_fee", "--fee", "--fee", "0", MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_templimit", "--templimit", "--templimit", "90", MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_tempunits", "--tempunits", "--tempunits", "C", MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_eexit", "--eexit", "--eexit", null, MinerOptionFlagType.SingleParam, " "),
                    new MinerOption("EWBF_solver", "--solver", "--solver", "0", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("EWBF_intensity", "--intensity", "--intensity", "64", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("EWBF_powercalc", "--pec", "--pec", null, MinerOptionFlagType.Uni, " "),
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.Xmrig,
                new List<MinerOption> {
                    new MinerOption("Xmrig_fee", "--donate-level=", "1", MinerOptionFlagType.SingleParam),
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_av", "-v", "--av=", "0", MinerOptionFlagType.SingleParam),
                    new MinerOption("CpuAffinity", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_priority", "--cpu-priority", null, MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_nohugepages", "--no-huge-pages", null, MinerOptionFlagType.Uni),
                    new MinerOption("Xmrig_nocolor", "--no-color", null, MinerOptionFlagType.Uni),
                    new MinerOption("Xmrig_maxusage", "--max-cpu-usage=", "75", MinerOptionFlagType.SingleParam),
                    new MinerOption("Xmrig_safe", "--safe", null, MinerOptionFlagType.Uni)
                },
                new List<MinerOption>()
                ),
            new MinerOptionPackage(
                MinerType.CPU_XMRig,
                new List<MinerOption> {
                    new MinerOption("Xmrig_fee", "--donate-level=", "1", MinerOptionFlagType.SingleParam),
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_av", "-v", "--av=", "0", MinerOptionFlagType.SingleParam),
                    new MinerOption("CpuAffinity", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_priority", "--cpu-priority", null, MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_nohugepages", "--no-huge-pages", null, MinerOptionFlagType.Uni),
                    new MinerOption("Xmrig_nocolor", "--no-color", null, MinerOptionFlagType.Uni),
                    new MinerOption("Xmrig_maxusage", "--max-cpu-usage=", "75", MinerOptionFlagType.SingleParam),
                    new MinerOption("Xmrig_safe", "--safe", null, MinerOptionFlagType.Uni)
                },
                new List<MinerOption>()),
            new MinerOptionPackage(
                MinerType.CPU_XMRigUPX,
                new List<MinerOption> {
                    new MinerOption("Xmrig_fee", "--donate-level=", "1", MinerOptionFlagType.SingleParam),
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_av", "-v", "--av=", "0", MinerOptionFlagType.SingleParam),
                    new MinerOption("CpuAffinity", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_priority", "--cpu-priority", null, MinerOptionFlagType.MultiParam, " "),
                    new MinerOption("Xmrig_nohugepages", "--no-huge-pages", null, MinerOptionFlagType.Uni),
                    new MinerOption("Xmrig_nocolor", "--no-color", null, MinerOptionFlagType.Uni),
                    new MinerOption("Xmrig_maxusage", "--max-cpu-usage=", "75", MinerOptionFlagType.SingleParam),
                    new MinerOption("Xmrig_safe", "--safe", null, MinerOptionFlagType.Uni)
                },
                new List<MinerOption>()),

            new MinerOptionPackage(
                MinerType.CPU_RKZ,
                new List<MinerOption>() {
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuAffinity", "", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuPriority", "", "--cpu-priority", "-1", MinerOptionFlagType.MultiParam, ",") // default
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.CPU_rplant,
                new List<MinerOption>() {
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuAffinity", "", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuPriority", "", "--cpu-priority", "-1", MinerOptionFlagType.MultiParam, ",") // default
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.CPU_nosuch,
                new List<MinerOption>() {
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuAffinity", "", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuPriority", "", "--cpu-priority", "-1", MinerOptionFlagType.MultiParam, ",") // default
                },
                new List<MinerOption>()
            ),
            new MinerOptionPackage(
                MinerType.CPU_verium,
                new List<MinerOption>() {
                    new MinerOption("Threads", "-t", "--threads=", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuAffinity", "", "--cpu-affinity", "-1", MinerOptionFlagType.MultiParam, ","), // default none
                    new MinerOption("CpuPriority", "", "--cpu-priority", "-1", MinerOptionFlagType.MultiParam, ",") // default
                },
                new List<MinerOption>()
            ),
        };

        /// <summary>
        /// Defines the MinerOptionPackages
        /// </summary>
        private static List<MinerOptionPackage> MinerOptionPackages = new List<MinerOptionPackage>();

        /// <summary>
        /// The InitializePackages
        /// </summary>
        public static void InitializePackages()
        {
            foreach (var pack in DEFAULTS)
            {
                var packageName = String.Format("MinerOptionPackage_{0}", pack.Name);
                var packageFile = new MinerOptionPackageFile(packageName);
                var readPack = packageFile.ReadFile();
                if (readPack == null)
                { // read has failed
                    Helpers.ConsolePrint("ExtraLaunchParameters", "Creating internal params config " + packageName);
                    MinerOptionPackages.Add(pack);
                    // create defaults
                    packageFile.Commit(pack);
                }
                else
                {
                    Helpers.ConsolePrint("ExtraLaunchParameters", "Loading internal params config " + packageName);
                    MinerOptionPackages.Add(readPack);
                }
            }
            var defaultKeys = DEFAULTS.ConvertAll((p) => p.Type);
            // extra check if DEFAULTS is missing a key
            for (var type = (MinerType.NONE + 1); type < MinerType.END; ++type)
            {
                if (defaultKeys.Contains(type) == false)
                {
                    var packageName = String.Format("MinerOptionPackage_{0}", Enum.GetName(typeof(MinerType), type));
                    var packageFile = new MinerOptionPackageFile(packageName);
                    var readPack = packageFile.ReadFile();
                    if (readPack != null)
                    { // read has failed
                        Helpers.ConsolePrint("ExtraLaunchParameters", "Creating internal params config " + packageName);
                        MinerOptionPackages.Add(readPack);
                    }
                }
            }
        }

        /// <summary>
        /// The GetMinerOptionPackageForMinerType
        /// </summary>
        /// <param name="type">The <see cref="MinerType"/></param>
        /// <returns>The <see cref="MinerOptionPackage"/></returns>
        public static MinerOptionPackage GetMinerOptionPackageForMinerType(MinerType type)
        {
            int index = MinerOptionPackages.FindIndex((p) => p.Type == type);
            if (index > -1)
            {
                return MinerOptionPackages[index];
            }
            // if none found
            return new MinerOptionPackage(MinerType.NONE, new List<MinerOption>(), new List<MinerOption>());
        }
    }
}
