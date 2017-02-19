using System;
using System.AddIn;
using PluginService.AddInView;

namespace TestSimplePluginWithCustomLog
{
    [AddIn("SimplePluginWithLog4Net", Description = "Simple AddIn with custom log4net log")]
    public class SimplePluginWithLog4Net : Plugin
    {
        public override Configuration[] Configuration { get; } = {
            new StartHourConfiguration {StartHour = 3},
            new IntervalConfiguration {Interval = TimeSpan.FromHours(24)}
        };

        public override void Execute()
        {
            Log.Info("SimplePluginWithLog4Net Execute");
        }

        public override void Interrupt()
        {
            Log.Info("SimplePluginWithLog4Net Interrupt");
        }
    }
}
