using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Caching;
using Newtonsoft.Json;
using StackExchange.Opserver.Data.PerformanceCounter;

namespace StackExchange.Opserver
{
    public class PerformanceCounterSettings
    {
        public static readonly PerformanceCounterSettings Instance = new PerformanceCounterSettings();
        private static readonly string Key = "PerformanceCounterSettings";
        private readonly ObjectCache objectCache = MemoryCache.Default;

        private PerformanceCounterSettings()
        {
        }

        public List<Node> Nodes
        {
            get
            {
                if (this.objectCache[Key] == null)
                {
                    this.LoadPerformanceCounterSettings();
                }

                return (List<Node>)this.objectCache[Key];
            }
        }

        private void LoadPerformanceCounterSettings()
        {
            var settings = (SettingsSection)ConfigurationManager.GetSection("Settings");

            var file = Path.Combine(
                settings.Path.Replace("~\\", AppDomain.CurrentDomain.BaseDirectory),
                "PerformanceCounterSettings.json");

            var definition = new
                             {
                                 Nodes = new List<Node>()
                             };

            var deserialized = JsonConvert.DeserializeAnonymousType(File.ReadAllText(file), definition);

            var policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(
                new HostFileChangeMonitor(
                    new List<string>
                    {
                        file
                    }));

            this.objectCache.Set(Key, deserialized.Nodes, policy);
        }
    }
}
