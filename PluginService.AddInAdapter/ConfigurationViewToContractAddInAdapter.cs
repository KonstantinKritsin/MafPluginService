using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using PluginService.AddInView;
using PluginService.Contract;
using PluginService.Contract.Configuration;

namespace PluginService.AddInAdapter
{
    internal static class ConfigurationViewToContractAddInAdapter
    {
        public static IConfiguration ViewToContractAdapter(Configuration view)
        {
            return ViewToContractChecks.Select(f => f(view)).FirstOrDefault(c => c != null);
        }

        public static Configuration ContractToViewAdapter(IConfiguration contract)
        {
            return ContractToViewChecks.Select(f => f(contract)).FirstOrDefault(c => c != null);
        }

        #region wrappers

        private static readonly List<Func<Configuration, IConfiguration>> ViewToContractChecks = new List<Func<Configuration, IConfiguration>>
        {
            view =>
            {
                var obj = view as IntervalConfiguration;
                return obj == null ? null : new IntervalWrapper {Interval = obj.Interval};
            },
            view =>
            {
                var obj = view as JobDurationInHourConfiguration;
                return obj == null ? null : new JobDurationInHourWrapper {JobDurationInHour = obj.JobDurationInHour};
            },
            view =>
            {
                var obj = view as StartDateConfiguration;
                return obj == null ? null : new StartDateWrapper {StartDate = obj.StartDate};
            },
            view =>
            {
                var obj = view as StartHourConfiguration;
                return obj == null ? null : new StartHourWrapper {StartHour = obj.StartHour};
            }
        };

        private static readonly List<Func<IConfiguration, Configuration>> ContractToViewChecks = new List<Func<IConfiguration, Configuration>>
        {
            view =>
            {
                var obj = view as IInterval;
                return obj == null ? null : new IntervalConfiguration {Interval = obj.Interval};
            },
            view =>
            {
                var obj = view as IJobDurationInHour;
                return obj == null ? null : new JobDurationInHourConfiguration {JobDurationInHour = obj.JobDurationInHour};
            },
            view =>
            {
                var obj = view as IStartDate;
                return obj == null ? null : new StartDateConfiguration {StartDate = obj.StartDate};
            },
            view =>
            {
                var obj = view as IStartHour;
                return obj == null ? null : new StartHourConfiguration {StartHour = obj.StartHour};
            }
        };

        private class IntervalWrapper : ContractBase, IInterval
        {
            public TimeSpan Interval { get; internal set; }
        }
        private class JobDurationInHourWrapper : ContractBase, IJobDurationInHour
        {
            public int JobDurationInHour { get; internal set; }
        }
        private class StartDateWrapper : ContractBase, IStartDate
        {
            public DateTime StartDate { get; internal set; }
        }
        private class StartHourWrapper : ContractBase, IStartHour
        {
            public int StartHour { get; internal set; }
        }

        #endregion
    }
}