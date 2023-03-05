using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cike.DependencyInjection.Loader
{
    public class ModuleLoader : IModuleLoader
    {
        private IList<ModuleDescriptor> moduleDescriptors = new List<ModuleDescriptor>();
        public void Loader(Type startupModuleType)
        {
            if (typeof(IServiceInjectModule).IsAssignableFrom(startupModuleType) == false)
            {
                throw new ArgumentException($"modelType {startupModuleType.FullName} not a IServiceInjectModule.");
            }
            if (moduleDescriptors.Any(e => e.ModuleType == startupModuleType))
            {
                return;
            }
            moduleDescriptors.Add(new ModuleDescriptor(startupModuleType));
            var dependOnAttr = startupModuleType.GetCustomAttribute<DependOnAttribute>();
            if (dependOnAttr == null)
            {
                return;
            }

            foreach (var item in dependOnAttr.GetServiceModuleType())
            {
                Loader(item);
            }

        }
        public IEnumerable<ModuleDescriptor> GetModuleDescriptors()
        {
            return moduleDescriptors.Reverse();
        }
    }
}
