using System;
using System.AddIn;
using PluginService.AddInView;

namespace TestSimplePlugin
{
    [AddIn("SimplePlugin", Description = "Simple AddIn")]
    public class SimplePlugin : Plugin
    {
        public override Configuration[] Configuration { get; } = {
            new StartHourConfiguration {StartHour = 3},
            new IntervalConfiguration {Interval = TimeSpan.FromHours(24)}
        };

        public override void Execute()
        {
            Log.Info("SimplePlugin Execute");
        }

        public override void Interrupt()
        {
            Log.Info("SimplePlugin Interrupt");
        }
    }
}