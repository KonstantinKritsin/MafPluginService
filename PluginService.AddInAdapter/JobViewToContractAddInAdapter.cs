using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using PluginService.AddInView;
using PluginService.Contract;

namespace PluginService.AddInAdapter
{
    [AddInAdapter]
    internal class JobViewToContractAddInAdapter : ContractBase, IJob
    {
        private readonly Job _view;

        public IListContract<IConfiguration> Configuration
            => CollectionAdapters.ToIListContract(_view.Configuration,
                ConfigurationViewToContractAddInAdapter.ViewToContractAdapter,
                ConfigurationViewToContractAddInAdapter.ContractToViewAdapter);

        public JobViewToContractAddInAdapter(Job view)
        {
            var catalogs = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && a.ExportedTypes.Any(t => t.IsSubclassOf(typeof(Job))))
                .Select(a => Path.GetDirectoryName(a.Location))
                .Where(path => !string.IsNullOrWhiteSpace(path))
                .Select(path => new DirectoryCatalog(path))
                .ToList();

            var catalog = new AggregateCatalog(catalogs);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(view);

            _view = view;
        }

        public void Execute()
        {
            _view.Execute();
        }

        public void Interrupt()
        {
            _view.Interrupt();
        }

        public void SetDefaultLog(ILog log)
        {
            log.LogPath = AppDomain.CurrentDomain.BaseDirectory;
            ((Job.LogWrap)_view.Log).SetLog(LogAddInAdapter.ContractToViewAdapter(log));
        }
    }
}
