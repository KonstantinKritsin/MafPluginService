using PluginService.Contract;
using PluginService.HostView;

namespace PluginService.HostAdapter
{
    public class LogAddInAdapter
    {
        internal static ILog ViewToContractAdapter(Log view)
        {
            if (!System.Runtime.Remoting.RemotingServices.IsObjectOutOfAppDomain(view) && (view.GetType() == typeof(LogContractToViewHostAdapter)))
                return ((LogContractToViewHostAdapter)view).Contract;
            return new LogViewToContractHostAdapter(view);
        }

        internal static Log ContractToViewAdapter(ILog contract)
        {
            if (!System.Runtime.Remoting.RemotingServices.IsObjectOutOfAppDomain(contract) && (contract.GetType() == typeof(LogViewToContractHostAdapter)))
                return ((LogViewToContractHostAdapter)contract).View;
            return new LogContractToViewHostAdapter(contract);
        }
    }
}