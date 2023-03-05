using Microsoft.Extensions.DependencyInjection;

namespace Cike.Modularization.DependencyInjection
{
    public interface IServiceInjectModule : ICikeModule
    {
        public Task ConfigurationServicesAsync(IServiceCollection services);
    }
}