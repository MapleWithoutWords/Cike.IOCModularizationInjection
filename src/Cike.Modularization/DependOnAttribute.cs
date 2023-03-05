using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cike.Modularization
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependOnAttribute : Attribute
    {
        private List<Type> ApplicationModules { get; set; } = new List<Type>();
        public DependOnAttribute(params Type[] types)
        {
            if (types != null)
            {
                foreach (var item in types)
                {
                    if (typeof(ICikeModule).IsAssignableFrom(item) == false)
                    {
                        throw new TypeLoadException($"type '{item.FullName}' not a IServiceInjectModule");
                    }
                    ApplicationModules.Add(item);
                }
            }
        }

        public List<Type> GetServiceModuleType()
        {
            return ApplicationModules;
        }
    }
}
