using Microsoft.EntityFrameworkCore;

using NerdStore.Catalogo.Domain;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Data.Repository
{
  public class ProdutoRepository : IProdutoRepository
  {
    public IUnitOfWork UnitOfWork => _context;

    private readonly CatalogoContext _context;

    public ProdutoRepository(CatalogoContext context)
    {
      _context = context;
    }

    public void Adicionar(Produto produto)
    {
      _context.Produtos.Add(produto);
    }

    public void Adicionar(Categoria categoria)
    {
      _context.Categorias.Add(categoria);
    }

    public void Atualizar(Produto produto)
    {
      _context.Produtos.Update(produto);
    }

    public void Atualizar(Categoria categoria)
    {
      _context.Categorias.Update(categoria);
    }

    public async Task<IEnumerable<Categoria>> ObterCategorias()
    {
      return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
    {
      return await _context.Produtos
        .AsNoTracking()
        .Include(p => p.Categoria)
        .Where(p => p.Categoria.Codigo == codigo)
        .ToListAsync();
    }

    public async Task<Produto> ObterPorId(Guid id)
    {
      return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<IEnumerable<Produto>> ObterTodos()
    {
      return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
