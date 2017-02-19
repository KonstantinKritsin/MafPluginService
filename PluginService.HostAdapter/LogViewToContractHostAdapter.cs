using System.AddIn.Pipeline;
using PluginService.Contract;
using PluginService.HostView;

namespace PluginService.HostAdapter
{
    internal class LogViewToContractHostAdapter : ContractBase, ILog
    {
        internal readonly Log View;

        public LogViewToContractHostAdapter(Log view)
        {
            View = view;
        }

        public string LogPath { set { View.LogPath = value; } }

        public void Fatal(string msg)
        {
            View.Fatal(msg);
        }

        public void Error(string msg)
        {
            View.Error(msg);
        }

        public void Warn(string msg)
        {
            View.Warn(msg);
        }

        public void Info(string msg)
        {
            View.Info(msg);
        }

        public void Debug(string msg)
        {
            View.Debug(msg);
        }
    }
}