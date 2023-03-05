namespace Cike.Modularization.ApplicationInitialization
{
    public interface IApplicationInitialization
    {
        public Task StartingAsync(IServiceProvider serviceProvider);
    }
}