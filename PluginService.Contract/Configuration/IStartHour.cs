namespace PluginService.Contract.Configuration
{
    public interface IStartHour : IConfiguration
    {
        /// <summary>
        /// Час запуска задачи(0-23). Если значение не удовлетворяет интервалу, то устанавливается 0 по умолчанию.
        /// Задача запускается один раз в ближайший час, соответствующий указанному значению.
        /// </summary>
        int StartHour { get; }
    }
}