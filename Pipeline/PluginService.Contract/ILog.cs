using System.AddIn.Contract;

namespace PluginService.Contract
{
    public interface ILog : IContract
    {
        string LogPath { set; }
        void Fatal(string msg);
        void Error(string msg);
        void Warn(string msg);
        void Info(string msg);
        void Debug(string msg);
    }
}