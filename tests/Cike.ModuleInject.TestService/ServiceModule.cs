using Cike.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Cike.ModuleInject.TestService
{
    public class ServiceModule : IServiceInjectModule
    {
        public async Task ConfigurationServices(IServiceCollection services)
        {

        }
    }
}