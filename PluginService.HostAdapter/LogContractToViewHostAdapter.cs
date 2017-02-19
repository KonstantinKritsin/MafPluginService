using System.AddIn.Pipeline;
using PluginService.Contract;
using PluginService.HostView;

namespace PluginService.HostAdapter
{
    internal class LogContractToViewHostAdapter : Log
    {
        internal readonly ILog Contract;
        private readonly ContractHandle _handle;
        public LogContractToViewHostAdapter(ILog contract)
        {
            Contract = contract;
            _handle = new ContractHandle(contract);
        }

        public override string LogPath
        {
            set { Contract.LogPath = value; }
        }

        public override void Fatal(object msg)
        {
            Contract.Fatal(msg.ToString());
        }

        public override void Error(object msg)
        {
            Contract.Error(msg.ToString());
        }

        public override void Warn(object msg)
        {
            Contract.Warn(msg.ToString());
        }

        public override void Info(object msg)
        {
            Contract.Info(msg.ToString());
        }

        public override void Debug(object msg)
        {
            Contract.Debug(msg.ToString());
        }
    }
}