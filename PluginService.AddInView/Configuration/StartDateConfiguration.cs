using System;

namespace PluginService.AddInView
{
    public class StartDateConfiguration : Configuration
    {
        /// <summary>
        /// Точная дата запуска задачи.
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}