using Cike.ModuleInject.TestWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddModularizationInjection<WebApiModule>();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapControllers();

app.Run();
