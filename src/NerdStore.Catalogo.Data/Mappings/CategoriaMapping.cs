using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NerdStore.Catalogo.Domain;
using NerdStore.Catalogo.Domain.Entities;

namespace NerdStore.Catalogo.Data.Mappings
{
  public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
  {
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
      builder.HasKey(c => c.Id);
      
      builder.Property(x => x.Nome)
        .IsRequired()
        .HasMaxLength(250);

      builder.ToTable("Categorias");
    }
  }
}
