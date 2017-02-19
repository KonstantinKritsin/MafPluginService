using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using PluginService.Contract;
using PluginService.Contract.Configuration;
using PluginService.HostView;

namespace PluginService.HostAdapter
{
    internal static class ConfigurationContractToViewHostAdapter
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
            view => (view as IntervalWrapper)?.Contract,
            view => (view as DurationInHourWrapper)?.Contract,
            view => (view as StartDateWrapper)?.Contract,
            view => (view as StartHourWrapper)?.Contract,
            view => (view as StartNowWrapper)?.Contract
        };

        private static readonly List<Func<IConfiguration, Configuration>> ContractToViewChecks = new List<Func<IConfiguration, Configuration>>
        {
            view =>
            {
                var obj = view as IInterval;
                return obj == null ? null : new IntervalWrapper(obj);
            },
            view =>
            {
                var obj = view as IDurationInHour;
                return obj == null ? null : new DurationInHourWrapper(obj);
            },
            view =>
            {
                var obj = view as IStartDate;
                return obj == null ? null : new StartDateWrapper(obj);
            },
            view =>
            {
                var obj = view as IStartHour;
                return obj == null ? null : new StartHourWrapper(obj);
            },
            view =>
            {
                var obj = view as IStartNow;
                return obj == null ? null : new StartNowWrapper(obj);
            },

        };

        private class IntervalWrapper : IntervalConfiguration
        {
            private readonly ContractHandle _handle;
            public IInterval Contract { get; }

            public IntervalWrapper(IInterval contract)
            {
                Contract = contract;
                _handle = new ContractHandle(contract);
            }

            public override TimeSpan Interval => Contract.Interval;
        }
        private class DurationInHourWrapper : DurationInHourConfiguration
        {
            private readonly ContractHandle _handle;
            public IDurationInHour Contract { get; }
            public DurationInHourWrapper(IDurationInHour contract)
            {
                Contract = contract;
                _handle = new ContractHandle(contract);
            }

            public override int DurationInHour => Contract.DurationInHour;
        }
        private class StartDateWrapper : StartDateConfiguration
        {
            private readonly ContractHandle _handle;
            public IStartDate Contract { get; }
            public StartDateWrapper(IStartDate contract)
            {
                Contract = contract;
                _handle = new ContractHandle(contract);
            }

            public override DateTime StartDate => Contract.StartDate;
        }
        private class StartHourWrapper : StartHourConfiguration
        {
            private readonly ContractHandle _handle;
            public IStartHour Contract { get; }
            public StartHourWrapper(IStartHour contract)
            {
                Contract = contract;
                _handle = new ContractHandle(contract);
            }

            public override int StartHour => Contract.StartHour;
        }

        private class StartNowWrapper : StartNowConfiguration
        {
            public IStartNow Contract { get; }

            public StartNowWrapper(IStartNow contract)
            {
                Contract = contract;
            }
        }

        #endregion
    }
}
