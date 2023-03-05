namespace Cike.ApplicationStarting.Abstracts
{
    public interface IApplicationStarting
    {
        public Task OnStartingAsync(IServiceProvider serviceProvider);

        public Task ShutDownAsync();
    }
}