using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using vs.OpenApiValidator.Services;
using vs.OpenApiValidator.Services.Contracts;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IClient, Client>();
        services.AddSingleton<IOpenApiValidator, OpenApiValidator>();
    })
    .Build();

var app = host.Services.GetRequiredService<IClient>();
return app.Run(args);