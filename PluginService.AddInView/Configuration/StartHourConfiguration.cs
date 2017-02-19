namespace PluginService.AddInView
{
    public class StartHourConfiguration : Configuration
    {
        /// <summary>
        /// Час запуска задачи(0-23). Если значение не удовлетворяет интервалу, то устанавливается 0 по умолчанию.
        /// Задача запускается один раз в ближайший час, соответствующий указанному значению.
        /// </summary>
        public int StartHour { get; set; }
    }
}