using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cike.DependencyInjection
{
    public class ModuleDescriptor
    {
        public Type ModuleType { get; set; }
        public Assembly ModuleAssembly { get; set; }

        public ModuleDescriptor(Type moduleType)
        {
            ModuleType = moduleType;
            ModuleAssembly = moduleType.Assembly;
        }
    }
}
