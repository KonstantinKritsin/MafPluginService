using System;

namespace PluginService.HostView
{
    public abstract class StartDateConfiguration : Configuration
    {
        /// <summary>
        /// Точная дата запуска задачи.
        /// </summary>
        public abstract DateTime StartDate { get; }
    }
}