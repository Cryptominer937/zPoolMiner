using Newtonsoft.Json;
using System;
using System.IO;

namespace zPoolMiner.Configs.ConfigJsonFile
{
    public abstract class ConfigFile<T> where T : class
    {
        // statics/consts
        private const string TAG_FORMAT = "ConfigFile<{0}>";

        private readonly string CONF_FOLDER; // = @"configs\";
        private readonly string TAG;

        public ConfigFile(string iCONF_FOLDER, string fileName, string fileNameOld)
        {
            CONF_FOLDER = iCONF_FOLDER;

            if (fileName.Contains(CONF_FOLDER))
            {
                _filePath = fileName;
            }
            else
            {
                _filePath = CONF_FOLDER + fileName;
            }

            if (fileNameOld.Contains(CONF_FOLDER))
            {
                _filePathOld = fileNameOld;
            }
            else
            {
                _filePathOld = CONF_FOLDER + fileNameOld;
            }

            TAG = string.Format(TAG_FORMAT, typeof(T).Name);
        }

        private void CheckAndCreateConfigsFolder()
        {
            try
            {
                if (Directory.Exists(CONF_FOLDER) == false)
                {
                    Directory.CreateDirectory(CONF_FOLDER);
                }
            }
            catch { }
        }

        // member stuff
        protected string _filePath = "";

        protected string _filePathOld = "";

        public bool IsFileExists() => File.Exists(_filePath);

        public T ReadFile()
        {
            CheckAndCreateConfigsFolder();
            T file = null;

            try
            {
                if (File.Exists(_filePath))
                {
                    file = JsonConvert.DeserializeObject<T>(File.ReadAllText(_filePath), Globals.JsonSettings);
                }
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(TAG, string.Format("ReadFile {0}: exception {1}", _filePath, ex.ToString()));
                file = null;
            }

            return file;
        }

        public void Commit(T file)
        {
            CheckAndCreateConfigsFolder();

            if (file == null)
            {
                Helpers.ConsolePrint(TAG, string.Format("Commit for FILE {0} IGNORED. Passed null object", _filePath));
                return;
            }

            try
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(file, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Helpers.ConsolePrint(TAG, string.Format("Commit {0}: exception {1}", _filePath, ex.ToString()));
            }
        }

        public void CreateBackup()
        {
            Helpers.ConsolePrint(TAG, string.Format("Backing up {0} to {1}..", _filePath, _filePathOld));

            try
            {
                if (File.Exists(_filePathOld))
                    File.Delete(_filePathOld);

                File.Copy(_filePath, _filePathOld, true);
            }
            catch { }
        }
    }
}