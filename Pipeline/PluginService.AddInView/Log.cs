namespace PluginService.AddInView
{
    public abstract class Log
    {
        public abstract void Fatal(object msg);
        public abstract void Error(object msg);
        public abstract void Warn(object msg);
        public abstract void Info(object msg);
        public abstract void Debug(object msg);
    }
}