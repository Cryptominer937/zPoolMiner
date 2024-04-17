using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using zPoolMiner.Enums;

namespace zPoolMiner
{
    internal class International
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("LANGUAGE");

        private class Language
        {
#pragma warning disable 649
            public string Name;
            public LanguageType ID;
            public Dictionary<string, string> Entries;
#pragma warning restore 649
        }

        private static Language SelectedLanguage;

        private static List<Language> GetLanguages()
        {
            var langs = new List<Language>();

            try
            {
                var di = new DirectoryInfo("langs");
                var files = di.GetFiles("*.lang");

                foreach (FileInfo fi in files)
                {
                    try
                    {
                        var l = JsonConvert.DeserializeObject<Language>(File.ReadAllText(fi.FullName)); // TODO , Globals.JsonSettings not sure since data must be localized
                        langs.Add(l);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return langs;
        }

        public static void Initialize(LanguageType lid)
        {
            var langs = GetLanguages();

            foreach (Language lang in langs)
            {
                if (lang.ID == lid)
                {
                    log.Info("Selected language: " + lang.Name);
                    SelectedLanguage = lang;
                    return;
                }
            }

            log.Error("Critical error: missing language");
        }

        /// <summary>
        /// Call this method to obtain available languages. Used by Settings GUI.
        /// </summary>
        /// <returns>Each dictionary entry contains id of the language (int) and name of the language (string).</returns>
        public static Dictionary<LanguageType, string> GetAvailableLanguages()
        {
            var langs = GetLanguages();
            var retdict = new Dictionary<LanguageType, string>();

            foreach (Language lang in langs)
            {
                log.Debug("Found language: " + lang.Name);
                retdict.Add(lang.ID, lang.Name);
            }

            return retdict;
        }

        public static string GetText(string token)
        {
            if (SelectedLanguage == null) return "";

            if (SelectedLanguage.Entries.ContainsKey(token))
                return SelectedLanguage.Entries[token];
            else
                return "";
        }
    }
}