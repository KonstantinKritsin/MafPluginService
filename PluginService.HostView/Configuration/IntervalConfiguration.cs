using System;

namespace PluginService.HostView
{
    public abstract class IntervalConfiguration : Configuration
    {
        public abstract TimeSpan Interval { get; }
    }
}