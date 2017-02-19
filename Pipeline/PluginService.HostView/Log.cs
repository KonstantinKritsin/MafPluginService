namespace PluginService.HostView
{
    public abstract class Log
    {
        public abstract string LogPath { set; }
        public abstract void Fatal(object msg);
        public abstract void Error(object msg);
        public abstract void Warn(object msg);
        public abstract void Info(object msg);
        public abstract void Debug(object msg);
    }
}