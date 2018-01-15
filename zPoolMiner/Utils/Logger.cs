using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using zPoolMiner.Configs;

namespace zPoolMiner
{
    public class Logger
    {
        public static bool IsInit = false;
        public static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public const string _logPath = @"logs\";

        public static void ConfigureWithFile()
        {
            try
            {
                if (!Directory.Exists("logs"))
                {
                    Directory.CreateDirectory("logs");
                }
            }
            catch { }

            IsInit = true;
            try
            {
                Hierarchy h = (Hierarchy)LogManager.GetRepository();

                if (ConfigManager.GeneralConfig.LogToFile)
                    h.Root.Level = Level.Info;
                //else if (ConfigManager.Instance.GeneralConfig.LogLevel == 2)
                //    h.Root.Level = Level.Warn;
                //else if (ConfigManager.Instance.GeneralConfig.LogLevel == 3)
                //    h.Root.Level = Level.Error;

                h.Root.AddAppender(CreateFileAppender());
                h.Configured = true;
            }
            catch (Exception e)
            {
                IsInit = false;
            }
        }

        public static IAppender CreateFileAppender()
        {
            RollingFileAppender appender = new RollingFileAppender
            {
                Name = "RollingFileAppender",
                File = _logPath + "log.txt",
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxSizeRollBackups = 1,
                MaxFileSize = ConfigManager.GeneralConfig.LogMaxFileSize,
                PreserveLogFileNameExtension = true,
                Encoding = System.Text.Encoding.Unicode
            };

            PatternLayout layout = new PatternLayout
            {
                ConversionPattern = "[%date{yyyy-MM-dd HH:mm:ss}] [%level] %message%newline"
            };
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();

            return appender;
        }
    }
}