using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace PluginService.Contract
{
    [AddInContract]
    public interface IJob : IContract
    {
        /// <summary>
        /// Массив для задания конфигурации запуска задачи.
        /// </summary>
        /// <returns>
        /// Если значение равно null, либо массив пуст, то данная задача по умолчанию стартует немедленно.
        /// </returns>
        IListContract<IConfiguration> Configuration { get; }
        /// <summary>
        /// Метод вызывается в момент старта задачи. Задача считается завершённой, когда метод возвращает управление.
        /// </summary>
        void Execute();
        /// <summary>
        /// Метод для принудительной остановки задачи. Вызывается только в том случае, если метод <see cref="Execute"/> к моменту вызова не вернул управления.
        /// Желательно избегать внутри метода длительных операций.
        /// </summary>
        void Interrupt();

        void SetDefaultLog(ILog log);
    }
}