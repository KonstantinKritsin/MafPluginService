using System;

namespace PluginService.Contract.Configuration
{
    public interface IInterval : IConfiguration
    {
        TimeSpan Interval { get; }
    }
}