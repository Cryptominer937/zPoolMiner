using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using zPoolMiner.Configs;
using zPoolMiner.Forms;
using zPoolMiner.Miners;
using zPoolMiner.Utils;

namespace zPoolMiner
{
    internal static class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("zPoolMiner");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        private static void Main(string[] argv)
        {
            // Set working directory to exe
            var pathSet = false;
            var path = Path.GetDirectoryName(Application.ExecutablePath);
            if (path != null)
            {
                Environment.CurrentDirectory = path;
                pathSet = true;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            // #0 set this first so data parsing will work correctly
            Globals.JsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Culture = CultureInfo.InvariantCulture
            };

            // #1 first initialize config
            ConfigManager.InitializeConfig();

            // #2 check if multiple instances are allowed
            bool startProgram = true;
            if (ConfigManager.GeneralConfig.AllowMultipleInstances == false)
            {
                try
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            startProgram = false;
                        }
                    }
                }
                catch { }
            }

            if (startProgram)
            {
                if (ConfigManager.GeneralConfig.LogToFile)
                    if (ConfigManager.GeneralConfig.DebugConsole)
                    {
                        Helpers.AllocConsole();
                    }
                {
                    Logger.ConfigureWithFile();
                }

                if (ConfigManager.GeneralConfig.DebugConsole)
                {
                    Helpers.AllocConsole();
                }

                // init active display currency after config load
                ExchangeRateAPI.ActiveDisplayCurrency = ConfigManager.GeneralConfig.DisplayCurrency;

                // #2 then parse args
                var commandLineArgs = new CommandLineParser(argv);

                log.Info("Starting up zPoolMiner v" + Application.ProductVersion);
                if (!pathSet)
                {
                    Helpers.ConsolePrint("HashKings", "Path not set to executable");
                }
                var tosChecked = ConfigManager.GeneralConfig.agreedWithTOS == Globals.CURRENT_TOS_VER;
                if (!tosChecked || !ConfigManager.GeneralConfigIsFileExist() && !commandLineArgs.IsLang)
                {
                    log.Warn("No config file found. Running zPool Miner for the first time. Choosing a default language.");
                    Application.Run(new Form_ChooseLanguage());
                }

                // Init languages
                International.Initialize(ConfigManager.GeneralConfig.Language);

                if (commandLineArgs.IsLang)
                {
                    log.Warn("Language is overwritten by command line parameter (-lang).");
                    International.Initialize(commandLineArgs.LangValue);
                    ConfigManager.GeneralConfig.Language = commandLineArgs.LangValue;
                }
                if (argv.Any(a => a == "--disable-donation"))
                    MiningSession.DonationStart = DateTime.MaxValue;
                // check WMI
                if (Helpers.IsWMIEnabled())
                {
                    if (ConfigManager.GeneralConfig.agreedWithTOS == Globals.CURRENT_TOS_VER)
                    {
                        Application.Run(new Form_Main());
                        Console.WriteLine("Press to exit");
                        Console.ReadLine();
                    }
                }
                else
                {
                    MessageBox.Show(International.GetText("Program_WMI_Error_Text"),
                                                            International.GetText("Program_WMI_Error_Title"),
                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}