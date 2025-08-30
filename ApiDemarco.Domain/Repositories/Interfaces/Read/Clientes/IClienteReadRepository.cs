using ApiDemarco.Domain.Entities;

namespace ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;

public interface IClienteReadRepository
{
    Task<List<Cliente>> GetClientesListAsync();
    
    Task<Cliente?> GetByIdAsync(int id);
    
    Task<Cliente?> GetByEmailAsync(string email);
}