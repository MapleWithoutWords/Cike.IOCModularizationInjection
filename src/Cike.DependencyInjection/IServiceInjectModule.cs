using Microsoft.Extensions.DependencyInjection;

namespace Cike.DependencyInjection
{
    public interface IServiceInjectModule
    {
        public Task ConfigurationServices(IServiceCollection services);
    }
}