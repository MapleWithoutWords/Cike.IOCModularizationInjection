using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cike.DependencyInjection.Loader
{
    public interface IModuleLoader
    {
        public void Loader(Type startupType);
        public IEnumerable<ModuleDescriptor> GetModuleDescriptors();
    }
}
