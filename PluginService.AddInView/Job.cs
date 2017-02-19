using System.AddIn.Pipeline;
using System.ComponentModel.Composition;

namespace PluginService.AddInView
{
    [AddInBase]
    public abstract class Job
    {
        [Import(AllowDefault = true)]
        private Log InjectedLog
        {
            set
            {
                if (value != null)
                    ((LogWrap)Log).SetLog(value);
            }
        }

        public Log Log { get; } = new LogWrap();


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

        public class LogWrap : Log
        {
            private Log _log;

            public void SetLog(Log log)
            {
                if (_log == null) _log = log;
            }

            public override void Fatal(object msg)
            {
                _log?.Fatal(msg);
            }

            public override void Error(object msg)
            {
                _log?.Error(msg);
            }

            public override void Warn(object msg)
            {
                _log?.Warn(msg);
            }

            public override void Info(object msg)
            {
                _log?.Info(msg);
            }

            public override void Debug(object msg)
            {
                _log?.Debug(msg);
            }
        }
    }
}