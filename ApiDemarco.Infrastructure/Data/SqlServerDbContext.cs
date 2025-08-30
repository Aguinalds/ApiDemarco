using ApiDemarco.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDemarco.Infrastructure.Data;

public class SqlServerDbContext : DbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>().ToTable("Clientes", "demarco");
    }
}