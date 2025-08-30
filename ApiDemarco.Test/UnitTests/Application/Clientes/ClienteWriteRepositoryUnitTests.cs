using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Entities;
using ApiDemarco.Infrastructure.Data;
using ApiDemarco.Infrastructure.Repositories.Read.Clientes;
using ApiDemarco.Infrastructure.Repositories.Write.Clientes;
using Microsoft.EntityFrameworkCore;
using Moq;

public class ClienteRepositoryUnitTests
{
    private readonly SqlServerDbContext _context;
    private readonly ClienteReadRepository _readRepo;
    private readonly ClienteWriteRepository _writeRepo;

    public ClienteRepositoryUnitTests()
    {
        var options = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDemarco")
            .Options;

        _context = new SqlServerDbContext(options);
        _readRepo = new ClienteReadRepository(_context);

        var loggerMock = new Mock<IAppLogger<ClienteWriteRepository>>();

        _writeRepo = new ClienteWriteRepository(
            _context,
            _readRepo,
            loggerMock.Object
        );
    }

    [Fact(DisplayName = "Adicionar cliente")]
    public async Task AddCliente()
    {
        var cliente = new Cliente { Nome = "Cliente Teste Unit", Email = "cliente_teste_unit@teste.com" };

        await _writeRepo.AddAsync(cliente, CancellationToken.None);

        var inserted = await _readRepo.GetByEmailAsync(cliente.Email);

        Assert.NotNull(inserted);
        Assert.Equal(cliente.Email, inserted.Email);
    }

    [Fact(DisplayName = "Atualizar cliente")]
    public async Task UpdateCliente()
    {
        var cliente = new Cliente { Nome = "Cliente Teste Unit", Email = "cliente_teste_unit2@teste.com" };
        await _writeRepo.AddAsync(cliente, CancellationToken.None);
        
        cliente.Nome = "Cliente Atualizado Unit";
        await _writeRepo.UpdateAsync(cliente, CancellationToken.None);

        var updated = await _readRepo.GetByEmailAsync(cliente.Email);
        Assert.Equal("Cliente Atualizado Unit", updated!.Nome);
    }

    [Fact(DisplayName = "Deletar cliente")]
    public async Task DeleteCliente()
    {
        var cliente = new Cliente { Nome = "Cliente Teste Unit", Email = "cliente_teste_unit@teste.com" };
        await _writeRepo.AddAsync(cliente, CancellationToken.None);

        await _writeRepo.DeleteAsync(cliente, CancellationToken.None);

        var deleted = await _readRepo.GetByEmailAsync(cliente.Email);
        Assert.Null(deleted);
    }

    [Fact(DisplayName = "Retornar lista de clientes")]
    public async Task GetClientesList()
    {
        foreach (var c in await _readRepo.GetClientesListAsync())
            await _writeRepo.DeleteAsync(c, CancellationToken.None);
        
        await _writeRepo.AddAsync(new Cliente { Nome = "Cliente 1", Email = "cliente1@test.com" }, CancellationToken.None);
        await _writeRepo.AddAsync(new Cliente { Nome = "Cliente 2", Email = "cliente2@test.com" }, CancellationToken.None);

        var clientes = await _readRepo.GetClientesListAsync();
        Assert.Equal(2, clientes.Count);
    }
}
