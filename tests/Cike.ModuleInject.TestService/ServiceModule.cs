using Cike.Modularization.ApplicationInitialization;
using Cike.Modularization.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Cike.ModuleInject.TestService
{
    public class ServiceModule : IServiceInjectModule,IApplicationInitialization
    {
        public async Task ConfigurationServicesAsync(IServiceCollection services)
        {
            // Add services to the container.
        }


        public async Task StartingAsync(IServiceProvider serviceProvider)
        {
            // Do some things.For example:start a job.
        }
    }
}