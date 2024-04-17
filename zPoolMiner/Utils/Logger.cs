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

                //if (ConfigManager.GeneralConfig.LogToFile)
                //    h.Root.Level = Level.Info;
                //else if (ConfigManager.Instance.GeneralConfig.LogLevel == 2)
                //    h.Root.Level = Level.Warn;
                //else if (ConfigManager.Instance.GeneralConfig.LogLevel == 3)
                //    h.Root.Level = Level.Error;

                h.Root.AddAppender(CreateFileAppender());
                if (ConfigManager.GeneralConfig.LogToFile)
                    h.Root.AddAppender(CreateFileAppender());

#if DEBUG
                h.Root.AddAppender(CreateDebugAppender());
#else
                h.Root.AddAppender(CreateColoredConsoleAppender());
#endif
                h.Configured = true;
            }
            catch (Exception)
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

        public static IAppender CreateDebugAppender()
        {
            PatternLayout debugLayout = new PatternLayout();
            debugLayout.ConversionPattern = "[%date{MM.dd HH:mm:ss,fff}] [%thread] [%-5level] - %message%newline";
            debugLayout.ActivateOptions();
            DebugAppender debugAppender = new DebugAppender();
            debugAppender.Layout = debugLayout;
            debugAppender.ActivateOptions();

            return debugAppender;
        }

        public static IAppender CreateColoredConsoleAppender()
        {
            PatternLayout layout = new PatternLayout();
            layout.ConversionPattern = "[%date{MM.dd HH:mm:ss,fff}] [%thread] [%-5level] - %message%newline";
            layout.ActivateOptions();
            var appender = new ColoredConsoleAppender { Layout = layout };
            appender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Debug,
                ForeColor = ColoredConsoleAppender.Colors.Green
            });
            appender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Info,
                ForeColor = ColoredConsoleAppender.Colors.White
            });
            appender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Warn,
                ForeColor = ColoredConsoleAppender.Colors.Yellow
            });
            appender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Error,
                ForeColor = ColoredConsoleAppender.Colors.Red
            });
            appender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Fatal,
                ForeColor = ColoredConsoleAppender.Colors.HighIntensity | ColoredConsoleAppender.Colors.Red
            });
            appender.ActivateOptions();
            return appender;
        }
    }
}