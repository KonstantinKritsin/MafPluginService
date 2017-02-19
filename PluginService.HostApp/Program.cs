using System;
using System.AddIn.Hosting;
using System.Linq;
using PluginService.HostView;

namespace PluginService.HostApp
{
    class Program
    {
        private const string PipelinePath = "F:\\Proj\\ConsoleApplication4_5\\Mef\\ETR.PluginService.HostApp\\Pipeline";
        private const string PluginPath = "F:\\Proj\\ConsoleApplication4_5\\Mef\\ETR.PluginService.HostApp\\Plugins";

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

            var jobTokens = AddInStore.FindAddIns(typeof (Job), PipelinePath, PluginPath);

            foreach (var token in jobTokens)
            {
                Console.WriteLine(token.AddInFullName);
                foreach (var item in token)
                {
                    Console.WriteLine($"\t{item.Name}:{item.Value}");
                }
            }


            var t = jobTokens.FirstOrDefault();
            if (t != null)
            {
                var process = new AddInProcess
                {
                    KeepAlive = false
                };
                //var set = new PermissionSet(PermissionState.Unrestricted);
                //set.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.GetDirectoryName(t.AssemblyName.CodeBase)));
                var job = t.Activate<Job>(process, AddInSecurityLevel.Host);
                if (job != null)
                {
                    job.SetDefaultLog(new FileLog(LogLevelEnum.All));
                    if (job.Configuration != null)
                        foreach (var configuration in job.Configuration)
                            Console.WriteLine(configuration.GetType().FullName);

                    job.Execute();
                    job.Interrupt();

                    var controller = AddInController.GetAddInController(job);
                    controller.Shutdown();
                }
            }
            Console.ReadKey();
        }
    }
}
