using System;
using System.Globalization;
using System.IO;
using Backups.Tools;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class FileLogger : ILogger
    {
        public FileLogger(string path)
        {
            Path = path ?? throw new BackupsException("path for file logger can not be the null");
            Path += "log.txt";
            File.AppendAllText(Path, "BackupJob logger");
        }

        public string Path { get; }
        public void Log(string text, bool timeCodePrefix = false)
        {
            if (timeCodePrefix)
                text += ':' + DateTime.Now.ToString(CultureInfo.InvariantCulture);
            File.AppendAllText(Path, text);
        }
    }
}