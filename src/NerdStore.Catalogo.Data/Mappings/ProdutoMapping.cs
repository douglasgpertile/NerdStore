using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NerdStore.Catalogo.Domain;
using NerdStore.Catalogo.Domain.Entities;

namespace NerdStore.Catalogo.Data.Mappings
{
  public class ProdutoMapping : IEntityTypeConfiguration<Produto>
  {
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
      builder.HasKey(x => x.Id);

      // 1 : N => Categorias : Produtos
      builder.HasOne(p => p.Categoria)
        .WithMany()
        .HasForeignKey(p => p.CategoriaId);

      builder.Property(x => x.Nome)
        .IsRequired()
        .HasMaxLength(250);

      builder.Property(x => x.Descricao)
        .IsRequired()
        .HasMaxLength(500);
      
      builder.Property(x => x.Imagem)
        .IsRequired()
        .HasMaxLength(250);

      builder.OwnsOne(p => p.Dimensoes, dimb =>
      {
        dimb.Property(d => d.Altura)
          .HasColumnName("Altura")
          .HasColumnType("int");        
        
        dimb.Property(d => d.Largura)
          .HasColumnName("Largura")
          .HasColumnType("int");
        
        dimb.Property(d => d.Profundidade)
          .HasColumnName("Profundidade")
          .HasColumnType("int");
      });

      builder.ToTable("Produtos");
    }
  }
}
