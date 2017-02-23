using System.ComponentModel.Composition;
using log4net;
using PluginService.AddInView;

namespace TestSimplePluginWithCustomLog
{
    [Export(typeof(Log))]
    public class Log4NetLog : Log
    {

        private readonly ILog _log;

        public Log4NetLog()
        {
            log4net.Config.XmlConfigurator.Configure();
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public override void Fatal(object msg)
        {
            _log.Fatal(msg);
        }

        public override void Error(object msg)
        {
            _log.Error(msg);
        }

        public override void Warn(object msg)
        {
            _log.Warn(msg);
        }

        public override void Info(object msg)
        {
            _log.Info(msg);
        }

        public override void Debug(object msg)
        {
            _log.Debug(msg);
        }
    }


}