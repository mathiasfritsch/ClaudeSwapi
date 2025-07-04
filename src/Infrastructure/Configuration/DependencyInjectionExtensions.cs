using Application.Interfaces;
using Infrastructure.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Infrastructure.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRefitClient<ISwapiApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://www.swapi.tech/api");
                c.Timeout = TimeSpan.FromSeconds(30);
            });

        services.AddScoped<ISwapiClient, SwapiClient>();

        return services;
    }
}