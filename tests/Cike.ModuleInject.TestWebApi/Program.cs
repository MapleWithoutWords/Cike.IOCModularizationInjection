using Cike.ModuleInject.TestWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

await builder.Services.AddModuleInjectionAsync<WebApiModule>();

var app = builder.Build();

await app.Services.AddModuleStartingAsync<WebApiModule>();
// Configure the HTTP request pipeline.


app.MapControllers();

app.Run();
