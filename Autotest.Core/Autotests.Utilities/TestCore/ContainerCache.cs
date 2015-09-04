using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autotests.Utilities.Core.Configuration.Settings;
using Autotests.Utilities.WebTestCore.Utils;

namespace Autotests.Utilities.TestCore
{
    public static class ContainerCache
    {
        public static IContainer GetContainer(string key, string settingsFilename, Action<IContainer> configureContainer = null)
        {
            if (!initialized)
            {
                if (!isUnloadInitilized)
                {
                    AppDomain.CurrentDomain.DomainUnload += CurrentDomainOnDomainUnload;
                    isUnloadInitilized = true;
                }
                cache = new Dictionary<string, IContainer>();
                initialized = true;
            }
//            if (!cache.ContainsKey(key))
//                cache[key] = Configure(settingsFilename, configureContainer);
            return cache[key];
        }
//
//        private static IContainer Configure(string settingsFilename, Action<IContainer> configureContainer)
//        {
//            IApplicationSettings applicationSettings = ApplicationSettings.LoadDefault(settingsFilename);
//            var container = new Container(new ContainerConfiguration(AssembliesLoader.Load()));
//            container.Configurator.ForAbstraction<IApplicationSettings>().UseInstances(applicationSettings);
//            container.Configurator.ForAbstraction<ISerializer>().UseInstances(new Serializer(new AllPropertiesExtractor(), new CatalogueGroBufCustomSerializerCollection(), GroBufOptions.MergeOnRead));
//            container.Get<IStorageConfigurator>().Configure(container);
//            if (configureContainer != null)
//                configureContainer(container);
//            return container;
//        }

        private static void CurrentDomainOnDomainUnload(object sender, EventArgs e)
        {
            if (cache != null)
            {
                var keys = cache.Keys.ToArray();
                foreach (var key in keys)
                {
                    IContainer container;
                    if (cache.TryGetValue(key, out container))
                    {
                        container.Dispose();
                        cache.Remove(key);
                    }
                }
                cache = null;
            }
        }

        private static bool isUnloadInitilized;
        private static bool initialized;
        private static Dictionary<string, IContainer> cache;
    }
}

