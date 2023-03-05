using Cike.Modularization.ApplicationInitialization;
using Cike.Modularization.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class IServiceProviderExtensions
    {
        public static void AddModuleStarting<TStartType>(this IServiceProvider serviceProvider)
        {
            if ((typeof(TStartType) is IApplicationInitialization)==false)
            {
                throw new ArgumentException($"The 'TStartType' not inherit 'IApplicationInitialization'.");
            }
            var moduleLoader = ModuleLoaderFactory.GetOrCreate();
            if (moduleLoader.GetModuleDescriptors().Count()<1)
            {
                moduleLoader.Loader(typeof(TStartType));
            }

            var moduleDescriptors = moduleLoader.GetModuleDescriptors();

            foreach (var moduleDescriptor in moduleDescriptors)
            {
                if (moduleDescriptor.ModuleType is IApplicationInitialization applicationInitialization)
                {
                    applicationInitialization.StartingAsync(serviceProvider).Wait();
                }
            }

        }
    }
}
