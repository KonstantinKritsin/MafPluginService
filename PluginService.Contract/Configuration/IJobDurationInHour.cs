namespace PluginService.Contract.Configuration
{
    public interface IJobDurationInHour : IConfiguration
    {
        int JobDurationInHour { get; }
    }
}