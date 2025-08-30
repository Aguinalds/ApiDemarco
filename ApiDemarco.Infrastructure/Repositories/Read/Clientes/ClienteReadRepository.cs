using ApiDemarco.Domain.Entities;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiDemarco.Infrastructure.Repositories.Read.Clientes;

public class ClienteReadRepository(SqlServerDbContext context) : IClienteReadRepository
{
    public async Task<List<Cliente>> GetClientesListAsync()
    {
        return await context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await context.Clientes.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Cliente?> GetByEmailAsync(string email)
    {
        return await context.Clientes.SingleOrDefaultAsync(x => x.Email == email);
    }
}