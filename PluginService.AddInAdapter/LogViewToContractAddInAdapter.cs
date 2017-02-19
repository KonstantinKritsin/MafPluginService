using System.AddIn.Pipeline;
using PluginService.AddInView;
using PluginService.Contract;

namespace PluginService.AddInAdapter
{
    internal class LogViewToContractAddInAdapter : ContractBase, ILog
    {
        internal readonly Log View;

        public LogViewToContractAddInAdapter(Log view)
        {
            View = view;
        }

        public string LogPath { set { } }

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