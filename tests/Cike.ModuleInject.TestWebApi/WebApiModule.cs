using Cike.Modularization;
using Cike.Modularization.ApplicationInitialization;
using Cike.Modularization.DependencyInjection;
using Cike.ModuleInject.TestService;
using Cike.ModuleInject.TestService.Services;

namespace Cike.ModuleInject.TestWebApi
{
    [DependOn(typeof(ServiceModule))]
    public class WebApiModule : IServiceInjectModule, IApplicationInitialization
    {
        public async Task ConfigurationServicesAsync(IServiceCollection services)
        {
            services.AddControllers();
        }

        public async Task StartingAsync(IServiceProvider serviceProvider)
        {
            var userService = serviceProvider.GetRequiredService<IUserService>();

            var res = await userService.GetUserNameAsync("Hello,word!");
        }
    }
}
