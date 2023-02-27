using Cike.IOCModularizationInjection.InjectionLifetimeAbstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cike.IOCModularizationInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddModularizationInjection<TStartModule>(this IServiceCollection services) where TStartModule : class, IServiceInjectModule
        {
            List<Type> allModuleTypes = new List<Type>();
            typeof(TStartModule).FindModuleDependency(allModuleTypes);
            allModuleTypes.Reverse();

            foreach (var moduleType in allModuleTypes)
            {
                var moduleAssemblyTypes = moduleType.Assembly.GetTypes().Where(e => e.IsClass && e.IsAbstract == false);
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
            }
        }

        private static (ServiceLifetime? LifeTime, bool ReplaceService) GetServiceLiftTime(Type type)
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

        public static void FindModuleDependency(this Type moduleType, List<Type> allModuleTypes)
        {
            if (typeof(IServiceInjectModule).IsAssignableFrom(moduleType) == false)
            {
                throw new ArgumentException($"modelType {moduleType.FullName} not a IServiceInjectModule.");
            }
            if (allModuleTypes.Contains(moduleType) == false)
            {
                return;
            }
            allModuleTypes.Add(moduleType);
            var dependOnAttr = moduleType.GetCustomAttribute<DependOnAttribute>();
            if (dependOnAttr == null)
            {
                return;
            }

            foreach (var item in dependOnAttr.GetServiceModuleType())
            {
                FindModuleDependency(item, allModuleTypes);
            }
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
    }
}
