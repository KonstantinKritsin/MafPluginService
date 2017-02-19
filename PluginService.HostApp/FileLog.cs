using System;
using System.Globalization;
using System.IO;
using System.Threading;
using PluginService.HostView;

namespace PluginService.HostApp
{
    internal class FileLog : Log
    {
        private readonly LogLevelEnum _level;
        private string _initialPath;
        private string _path;

        public FileLog(LogLevelEnum level)
        {
            _level = level;
        }

        private static string CheckCreateDirectory(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Log";
            }
            else
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, "Log");
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                var date = DateTime.Now.Subtract(TimeSpan.FromDays(30));
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var dateStr = Path.GetFileName(file)?.Split('_')[0];
                    DateTime fileDate;
                    if (dateStr != null &&
                        DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out fileDate) &&
                        fileDate < date)
                        File.Delete(file);
                }
            }
            return path;
        }

        public override string LogPath
        {
            set
            {
                _initialPath = value;
            }
        }

        public override void Fatal(object msg)
        {
            if (_level >= LogLevelEnum.Fatal)
                WriteFile("FATAL", msg?.ToString());
        }

        public override void Error(object msg)
        {
            if (_level >= LogLevelEnum.Error)
                WriteFile("ERROR", msg?.ToString());
        }

        public override void Warn(object msg)
        {
            if (_level >= LogLevelEnum.Warn)
                WriteFile("WARN", msg?.ToString());
        }

        public override void Info(object msg)
        {
            if (_level >= LogLevelEnum.Info)
                WriteFile("INFO", msg?.ToString());
        }

        public override void Debug(object msg)
        {
            if (_level >= LogLevelEnum.Debug)
                WriteFile("DEBUG", msg?.ToString());
        }

        private void WriteFile(string msgType, string str)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            _path = CheckCreateDirectory(_initialPath);
            var now = DateTime.UtcNow;
            using (var stream = new FileStream(Path.Combine(_path, $"{now:yyyy-MM-dd}_Utc.log"), FileMode.Append, FileAccess.Write,
                FileShare.Delete | FileShare.ReadWrite, 4096))
            {
                var bytes = ToBytes($"{now:T} {msgType} {threadId}: {str}{Environment.NewLine}");
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static byte[] ToBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}