using ApiDemarco.Domain.Entities;

namespace ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;

public interface IClienteWriteRepository
{
    Task<Cliente> AddAsync(Cliente cliente, CancellationToken cancellationToken);
    Task<Cliente> UpdateAsync (Cliente cliente, CancellationToken cancellationToken);
    Task<Cliente> DeleteAsync (Cliente cliente, CancellationToken cancellationToken);
}