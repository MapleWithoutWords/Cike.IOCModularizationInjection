using Microsoft.Extensions.DependencyInjection;

namespace Cike.IOCModularizationInjection
{
    public interface IServiceInjectModule
    {
        public int Sort { get; set; }
        public Task ConfigurationServices(IServiceCollection services);
    }
}