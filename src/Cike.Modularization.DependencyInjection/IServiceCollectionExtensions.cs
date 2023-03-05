using Cike.DependencyInjection.Loader;
using Cike.Modularization.DependencyInjection;
using Cike.Modularization.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddModuleInjection<TStartModule>(this IServiceCollection services) where TStartModule : class, IServiceInjectModule
        {
            services.AddSingleton(typeof(IModuleLoader), ModuleLoaderFactory.GetOrCreate());

            var moduleLoader = services.GetSingleInstanceOrDefault<IModuleLoader>();
            moduleLoader.Loader(typeof(TStartModule));
            var allModuleTypes = moduleLoader.GetModuleDescriptors();

            foreach (var moduleDescriptor in allModuleTypes)
            {
                var moduleAssemblyTypes = moduleDescriptor.ModuleAssembly.GetTypes().Where(e => e.IsClass && e.IsAbstract == false);
                foreach (var type in moduleAssemblyTypes)
                {
                    var serviceInfo = GetServiceLiftTime(type);

                    if (serviceInfo.LifeTime == null)
                    {
                        continue;
                    }
                    if (serviceInfo.ReplaceService)
                    {
                        services.ReplaceService(type);
                        continue;
                    }

                    foreach (var typeInterface in type.GetInterfaces())
                    {
                        switch (serviceInfo.LifeTime.Value)
                        {
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(typeInterface, type);
                                break;
                            case ServiceLifetime.Scoped:
                                services.AddScoped(typeInterface, type);
                                break;
                            case ServiceLifetime.Transient:
                                services.AddTransient(typeInterface, type);
                                break;
                        }
                    }

                }

                var module = ((IServiceInjectModule)Activator.CreateInstance(moduleDescriptor.ModuleType)!);
                module.ConfigurationServices(services);
                module.ConfigurationServicesAsync(services).Wait();
            }
        }

        public static (ServiceLifetime? LifeTime, bool ReplaceService) GetServiceLiftTime(Type type)
        {
            ServiceLifetime? lifeTime = null;
            bool isReplace = false;
            if (typeof(ISingletonInjection).IsAssignableFrom(type))
            {
                lifeTime = ServiceLifetime.Singleton;
            }
            else if (typeof(IScopeInjection).IsAssignableFrom(type))
            {
                lifeTime = ServiceLifetime.Scoped;
            }
            else if (typeof(ITransientInjection).IsAssignableFrom(type))
            {
                lifeTime = ServiceLifetime.Transient;
            }
            var injectAttr = type.GetCustomAttribute<InjectionAttribute>();
            if (injectAttr != null)
            {
                lifeTime = injectAttr.LifeTime;
                isReplace = injectAttr.ReplaceService;
            }
            return (lifeTime, isReplace);
        }


        /// <summary>
        /// replace implServiceType all interface service type.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="implServiceType"></param>
        public static void ReplaceService(this IServiceCollection services, Type implServiceType)
        {
            foreach (var item in implServiceType.GetInterfaces())
            {
                var itemInterface = services.FirstOrDefault(e => e.ServiceType == item);
                if (itemInterface != null)
                {
                    services.Replace(new ServiceDescriptor(item, implServiceType, itemInterface.Lifetime));
                }
            }
        }

        public static TService GetSingleInstanceOrDefault<TService>(this IServiceCollection services)
        {
            return (TService)services.FirstOrDefault(e => e.Lifetime == ServiceLifetime.Singleton && e.ServiceType == typeof(TService)).ImplementationInstance;
        }
    }
}
