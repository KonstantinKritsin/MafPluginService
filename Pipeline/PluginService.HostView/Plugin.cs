namespace PluginService.HostView
{
    public abstract class Plugin
    {
        /// <summary>
        /// Массив для задания конфигурации запуска задачи.
        /// </summary>
        /// <returns>
        /// Если значение равно null, либо массив пуст, то данная задача по умолчанию стартует немедленно.
        /// </returns>
        public abstract Configuration[] Configuration { get; }
        /// <summary>
        /// Метод вызывается в момент старта задачи. Задача считается завершённой, когда метод возвращает управление.
        /// </summary>
        public abstract void Execute();
        /// <summary>
        /// Метод для принудительной остановки задачи. Вызывается только в том случае, если метод <see cref="Execute"/> к моменту вызова не вернул управления.
        /// Желательно избегать внутри метода длительных операций.
        /// </summary>
        public abstract void Interrupt();
        public abstract void SetDefaultLog(Log log);
    }
}