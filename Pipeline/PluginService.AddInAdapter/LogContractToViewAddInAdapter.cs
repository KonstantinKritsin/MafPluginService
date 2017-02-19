using System.AddIn.Pipeline;
using PluginService.AddInView;
using PluginService.Contract;

namespace PluginService.AddInAdapter
{
    internal class LogContractToViewAddInAdapter : Log
    {
        internal readonly ILog Contract;
        private ContractHandle _handle;

        public LogContractToViewAddInAdapter(ILog contract)
        {
            Contract = contract;
            _handle = new ContractHandle(contract);
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