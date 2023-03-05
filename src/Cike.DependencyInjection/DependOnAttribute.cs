using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cike.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependOnAttribute : Attribute
    {
        private List<Type> ServiceModules { get; set; } = new List<Type>();
        public DependOnAttribute(params Type[] types)
        {
            if (types != null)
            {
                foreach (var item in types)
                {
                    if (typeof(IServiceInjectModule).IsAssignableFrom(item)==false)
                    {
                        throw new TypeLoadException($"type '{item.FullName}' not a IServiceInjectModule");
                    }
                    ServiceModules.Add(item);
                }
            }
        }

        public List<Type> GetServiceModuleType()
        {
            return ServiceModules;
        }
    }
}
