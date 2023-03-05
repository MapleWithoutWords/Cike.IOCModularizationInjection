using Cike.DependencyInjection.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cike.Modularization.Loader
{
    public class ModuleLoaderFactory
    {
        private static IModuleLoader _moduleLoader;

        public static IModuleLoader GetOrCreate()
        {
            if (_moduleLoader == null)
            {
                _moduleLoader = (IModuleLoader)Activator.CreateInstance(typeof(ModuleLoader))!;
            }
            return _moduleLoader;
        }
    }
}
