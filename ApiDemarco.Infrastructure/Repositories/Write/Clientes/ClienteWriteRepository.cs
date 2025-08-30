using ApiDemarco.Application.Exceptions;
using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Entities;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;
using ApiDemarco.Infrastructure.Data;

namespace ApiDemarco.Infrastructure.Repositories.Write.Clientes;

public class ClienteWriteRepository(SqlServerDbContext context, IClienteReadRepository clienteReadRepository, IAppLogger<ClienteWriteRepository> logger) : IClienteWriteRepository
{
    public async Task<Cliente> AddAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        try
        {
            await context.Clientes.AddAsync(cliente, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return cliente;
        }
        catch (Exception ex)
        {
            logger.LogError(ex);
            throw new DatabaseOperationException(ex);
        }
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        try
        {
            var userUpdate = await clienteReadRepository.GetByIdAsync(cliente.Id);
            
            userUpdate.Id = cliente.Id;
            userUpdate.Nome = cliente.Nome;
            userUpdate.Email = cliente.Email;
            
            context.Clientes.Update(userUpdate);
            await context.SaveChangesAsync(cancellationToken);
            return userUpdate;
        }
        catch (Exception ex)
        {
            logger.LogError(ex);
            throw new DatabaseOperationException(ex);
        }
    }

    public async Task<Cliente> DeleteAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        try
        {
            var clienteDelete = await clienteReadRepository.GetByIdAsync(cliente.Id);
            
            context.Clientes.Remove(clienteDelete);
            await context.SaveChangesAsync(cancellationToken);
            return clienteDelete;
        }
        catch (Exception ex)
        {
            logger.LogError(ex);
            throw new DatabaseOperationException(ex);
        }
    }
}