using System;
using System.AddIn.Hosting;
using System.Linq;
using PluginService.HostView;

namespace PluginService.HostApp
{
    class Program
    {
        private const string PipelinePath = "..\\..\\Pipeline";
        private const string PluginPath = "..\\..\\Plugins";

        static void Main(string[] args)
        {

            var errors = AddInStore.Rebuild(PipelinePath);
            if (errors.Length > 0)
            {
                foreach (var error in errors) Console.WriteLine(error);
                Console.ReadKey();
                return;
            }

            errors = AddInStore.RebuildAddIns(PluginPath);
            if (errors.Length > 0)
            {
                foreach (var error in errors) Console.WriteLine(error);
                Console.ReadKey();
                return;
            }

            var pluginTokens = AddInStore.FindAddIns(typeof (Plugin), PipelinePath, PluginPath);

            foreach (var token in pluginTokens)
            {
                Console.WriteLine(token.AddInFullName);
                foreach (var item in token)
                {
                    Console.WriteLine($"\t{item.Name}:{item.Value}");
                }
            }


            foreach (var pluginToken in pluginTokens)
            {
                var process = new AddInProcess
                {
                    KeepAlive = false
                };
                //var set = new PermissionSet(PermissionState.Unrestricted);
                //set.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.GetDirectoryName(t.AssemblyName.CodeBase)));
                var plugin = pluginToken.Activate<Plugin>(process, AddInSecurityLevel.Host);
                if (plugin != null)
                {
                    plugin.SetDefaultLog(new FileLog(LogLevelEnum.All));
                    if (plugin.Configuration != null)
                        foreach (var configuration in plugin.Configuration)
                            Console.WriteLine(configuration.GetType().FullName);

                    plugin.Execute();
                    plugin.Interrupt();

                    var controller = AddInController.GetAddInController(plugin);
                    controller.Shutdown();
                }
            }

            Console.ReadKey();
        }
    }
}
