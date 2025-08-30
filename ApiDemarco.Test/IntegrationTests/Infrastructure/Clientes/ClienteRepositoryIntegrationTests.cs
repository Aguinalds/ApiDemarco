using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Entities;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;
using ApiDemarco.Infrastructure.Data;
using ApiDemarco.Infrastructure.Repositories.Read.Clientes;
using ApiDemarco.Infrastructure.Repositories.Write.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ApiDemarco.Test.IntegrationTests.Infrastructure;

public class ClienteRepositoryIntegrationTests : IAsyncLifetime
{
    private readonly SqlServerDbContext _context;
    private readonly IClienteReadRepository _clienteReadRepository;
    private readonly IClienteWriteRepository _clienteWriteRepository;
    private Cliente _clienteTest = null!;

    public ClienteRepositoryIntegrationTests()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Test.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _context = new SqlServerDbContext(options);
        
        _clienteReadRepository = new ClienteReadRepository(_context);
        
        var loggerMock = new Mock<IAppLogger<ClienteWriteRepository>>();
        
        _clienteWriteRepository = new ClienteWriteRepository(
            _context,
            _clienteReadRepository,
            loggerMock.Object
        );
    }


    [Fact(DisplayName = "Deve criar um cliente")]
    public async Task AddCliente()
    {
        _clienteTest = new Cliente
        {
            Nome = "Cliente Teste",
            Email = $"cliente_teste@teste.com"
        };

        await _clienteWriteRepository.AddAsync(_clienteTest, CancellationToken.None);
        await _context.SaveChangesAsync();

        var inserted = await _clienteReadRepository.GetByEmailAsync(_clienteTest.Email);

        Assert.NotNull(inserted);
        Assert.Equal(_clienteTest.Email, inserted!.Email);
        
        _clienteTest = inserted;
    }

    [Fact(DisplayName = "Deve atualizar um cliente")]
    public async Task UpdateCliente()
    {
        _clienteTest = new Cliente
        {
            Nome = "Cliente Teste",
            Email = $"cliente_teste@teste.com"
        };

        await _clienteWriteRepository.AddAsync(_clienteTest, CancellationToken.None);
        await _context.SaveChangesAsync();
        
        var cliente = await _clienteReadRepository.GetByEmailAsync("cliente_teste@teste.com");

        cliente.Nome = "Cliente Atualizado";
        await _clienteWriteRepository.UpdateAsync(cliente, CancellationToken.None);
        await _context.SaveChangesAsync();

        var updated = await _clienteReadRepository.GetByIdAsync(cliente.Id);

        Assert.NotNull(updated);
        Assert.Equal("Cliente Atualizado", updated!.Nome);
        
        await _clienteWriteRepository.DeleteAsync(cliente, CancellationToken.None);
    }

    [Fact(DisplayName = "Deve buscar os clientes")]
    public async Task GetListClientes()
    {
        var fetched = await _clienteReadRepository.GetClientesListAsync();

        Assert.NotNull(fetched);
    }

    [Fact(DisplayName = "Deve deletar um cliente")]
    public async Task DeleteCliente()
    {
        var cliente = await _clienteReadRepository.GetByEmailAsync("cliente_teste@teste.com");

        await _clienteWriteRepository.DeleteAsync(cliente, CancellationToken.None);
        await _context.SaveChangesAsync();

        var deleted = await _clienteReadRepository.GetByIdAsync(cliente.Id);

        Assert.Null(deleted);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => _context.DisposeAsync().AsTask();
}