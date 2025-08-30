using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;
using ApiDemarco.Infrastructure.Repositories.Read.Clientes;
using ApiDemarco.Infrastructure.Repositories.Write.Clientes;

namespace ApiDemarco.Api.Extensions;

public static class DependencyInjectionSetup
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        //Repositorys
        services.AddScoped<IClienteReadRepository, ClienteReadRepository>();
        services.AddScoped<IClienteWriteRepository, ClienteWriteRepository>();
        
        return services;
    }
}