using Cike.ModuleInject.TestWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddModuleInjection<WebApiModule>();

var app = builder.Build();

app.Services.AddModuleStarting<WebApiModule>();
// Configure the HTTP request pipeline.


app.MapControllers();

app.Run();
