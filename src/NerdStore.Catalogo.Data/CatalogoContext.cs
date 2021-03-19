using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.Data;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Data
{
  public class CatalogoContext : DbContext, IUnitOfWork
  {

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var prop in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        prop.SetColumnType("varchar(100)");

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
      foreach (var entry in ChangeTracker.Entries()
        .Where(e => e.Entity.GetType().GetProperty("DataCadastro") != null))
      {
        if (entry.State == EntityState.Added)
          entry.Property("DataCadastro").CurrentValue = DateTime.Now;

        if (entry.State == EntityState.Modified)
          entry.Property("DataCadastro").IsModified = false;
      }

      return await base.SaveChangesAsync() > 0;
    }

  }
}
