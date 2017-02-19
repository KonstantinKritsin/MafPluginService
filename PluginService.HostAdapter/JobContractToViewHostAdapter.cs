using System.AddIn.Pipeline;
using System.Linq;
using PluginService.Contract;
using PluginService.HostView;

namespace PluginService.HostAdapter
{
    [HostAdapter]
    internal class JobContractToViewHostAdapter : Job
    {
        private readonly IJob _contract;
        private readonly ContractHandle _handle;

        public JobContractToViewHostAdapter(IJob contract)
        {
            _contract = contract;
            _handle = new ContractHandle(contract);
        }

        public override Configuration[] Configuration
            => CollectionAdapters.ToIList(_contract.Configuration,
                ConfigurationContractToViewHostAdapter.ContractToViewAdapter,
                ConfigurationContractToViewHostAdapter.ViewToContractAdapter).ToArray();
        public override void Execute()
        {
            _contract.Execute();
        }

        public override void Interrupt()
        {
            _contract.Interrupt();
        }

        public override void SetDefaultLog(Log log)
        {
            var contract = LogAddInAdapter.ViewToContractAdapter(log);
            _contract.SetDefaultLog(contract);
        }
    }
}