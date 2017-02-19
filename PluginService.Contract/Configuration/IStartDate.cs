using System;

namespace PluginService.Contract.Configuration
{
    public interface IStartDate : IConfiguration
    {
        /// <summary>
        /// Точная дата запуска задачи.
        /// </summary>
        DateTime StartDate { get; }
    }
}