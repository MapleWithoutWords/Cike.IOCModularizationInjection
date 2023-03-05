# Cike.Modularization

## Introduction

this is used modularization method register service and start application's a package,it can avoid Program class's too much content explosion. And it not dependency ```AspNet.Core```,it just start the program in a different way.

## Usage

1. first step: install package ```Cike.Modularization.DependencyInjection``` and ```Cike.Modularization.ApplicationInitialization``` package for your application.
   * ```Cike.Modularization.DependencyInjection``` ：the package be used for service register.
   * ```Cike.Modularization.ApplicationInitialization``` ：the package be used for application starting do some thing, example: start a job.

```shell
dotnet add package Cike.Modularization.DependencyInjection
dotnet add package Cike.Modularization.ApplicationInitialization
```

2. second step: create a ''{ClassLibraryName}Module' class in the your class library below, And let it inherit from ```IServiceInjectModule``` and ```IApplicationInitialization``` interface. For example:

```c#
public class ServiceModule : IServiceInjectModule,IApplicationInitialization
{
    public async Task ConfigurationServicesAsync(IServiceCollection services)
    {
		// Add services to the container.
    }


    public async Task StartingAsync(IServiceProvider serviceProvider)
    {
        // Do some things.For example:start a job.
    }
}
```

3. thirty step: add code ```await builder.Services.AddModuleInjectionAsync<StartUpModule>();``` and ```await app.Services.AddModuleStartingAsync<StartUpModule>();``` to your Program.cs. 
   * StartUpModule: This class is the program entry module class.

```c#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

await builder.Services.AddModuleInjectionAsync<StartUpModule>();

var app = builder.Build();

await app.Services.AddModuleStartingAsync<StartUpModule>();
// Configure the HTTP request pipeline.


app.MapControllers();

app.Run();
```

Notice：Modularity automatically scans all classes under this module that inherit ```ITransientInjection```,``` IScopeInjection```, ```ISingletonInjection``` or ```InjectionAttribute``` and adds them to the IOC container. For example:

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230305232846124-780887070.png)

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230305233313098-1346936421.png)
