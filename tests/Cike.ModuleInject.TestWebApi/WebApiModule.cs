
using Cike.DependencyInjection;
using Cike.ModuleInject.TestService;

namespace Cike.ModuleInject.TestWebApi
{
    [DependOn(typeof(ServiceModule))]
    public class WebApiModule : IServiceInjectModule
    {
        public async Task ConfigurationServices(IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}
