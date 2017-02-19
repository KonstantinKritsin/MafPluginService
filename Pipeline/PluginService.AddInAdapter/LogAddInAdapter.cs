using PluginService.AddInView;
using PluginService.Contract;

namespace PluginService.AddInAdapter
{
    public class LogAddInAdapter
    {
        internal static Log ContractToViewAdapter(ILog contract)
        {
            if (!System.Runtime.Remoting.RemotingServices.IsObjectOutOfAppDomain(contract) && (contract.GetType() == typeof(LogViewToContractAddInAdapter)))
                return ((LogViewToContractAddInAdapter)contract).View;

            return new LogContractToViewAddInAdapter(contract);
        }

        public static ILog ViewToContractAdapter(Log view)
        {
            if (!System.Runtime.Remoting.RemotingServices.IsObjectOutOfAppDomain(view) && (view.GetType() == typeof(LogContractToViewAddInAdapter)))
                return ((LogContractToViewAddInAdapter)view).Contract;

            return new LogViewToContractAddInAdapter(view);
        }
    }
}