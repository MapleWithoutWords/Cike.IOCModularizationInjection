using Cike.Modularization.ApplicationInitialization;
using Cike.Modularization.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Cike.ModuleInject.TestService
{
    public class ServiceModule : IServiceInjectModule,IApplicationInitialization
    {
        public async Task ConfigurationServicesAsync(IServiceCollection services)
        {

        }


        public async Task StartingAsync(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}