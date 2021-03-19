using AutoMapper;

using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.DomainObjects;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
  public class ProdutoAppService : IProdutoAppService
  {
    private readonly IProdutoRepository _repository;
    private readonly IEstoqueService _estoqueService;

    private readonly IMapper _mapper;

    public ProdutoAppService(IProdutoRepository repository, IEstoqueService estoqueService)
    {
      _repository = repository;
      _estoqueService = estoqueService;
    }

    public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
    {
      var produto = _mapper.Map<Produto>(produtoViewModel);
      _repository.Adicionar(produto);

      await _repository.UnitOfWork.Commit();
    }

    public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
    {
      var produto = _mapper.Map<Produto>(produtoViewModel);
      _repository.Atualizar(produto);

      await _repository.UnitOfWork.Commit();
    }

    public async Task<ProdutoViewModel> ObterPorId(Guid id)
    {
      return _mapper.Map<ProdutoViewModel>(await _repository.ObterPorId(id));
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
    {
      return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _repository.ObterPorCategoria(codigo));
    }

    public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
    {
      return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _repository.ObterCategorias());
    }

    public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
    {
      if (!await _estoqueService.DebitarEstoque(id, quantidade))
        throw new DomainException("Falha ao debitar estoque");

      return await ObterPorId(id);
    }

    public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quatidade)
    {
      if(!await _estoqueService.ReporEstoque(id, quatidade))
        throw new DomainException("Falha ao repor estoque");

      return await ObterPorId(id);
    }

    public void Dispose()
    {
      _repository?.Dispose();
      _estoqueService?.Dispose();
    }
  }
}
