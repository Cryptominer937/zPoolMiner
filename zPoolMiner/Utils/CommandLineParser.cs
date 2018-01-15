using System;
using zPoolMiner.Enums;

namespace zPoolMiner.Utils
{
    internal class CommandLineParser
    {
        // keep it simple only two parameters for now
        readonly public bool IsLang = false;

        readonly public LanguageType LangValue = 0;

        public CommandLineParser(string[] argv)
        {
            if (ParseCommandLine(argv, "-config", out string tmpString))
            {
                Helpers.ConsolePrint("CommandLineParser", "-config parameter has been depreciated, run setting from GUI");
            }
            if (ParseCommandLine(argv, "-lang", out tmpString))
            {
                IsLang = true;
                // if parsing fails set to default
                if (Int32.TryParse(tmpString, out int tmp))
                {
                    LangValue = (LanguageType)tmp;
                }
                else
                {
                    LangValue = LanguageType.En;
                }
            }

            if (ParseCommandLine(argv, "-donations", out tmpString))
                if (tmpString == "false") Miner.DonationStart = DateTime.MaxValue;
        }

        private bool ParseCommandLine(string[] argv, string find, out string value)
        {
            value = "";

            for (int i = 0; i < argv.Length; i++)
            {
                if (argv[i].Equals(find))
                {
                    if ((i + 1) < argv.Length && argv[i + 1].Trim()[0] != '-')
                    {
                        value = argv[i + 1];
                    }

                    return true;
                }
            }

            return false;
        }
    }
}