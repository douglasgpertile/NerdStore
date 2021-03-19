using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Bus;

using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Services
{
  public class EstoqueService : IEstoqueService
  {

    private readonly IProdutoRepository _repository;
    private readonly IMediatRHandler _bus;

    public EstoqueService(IProdutoRepository repository, IMediatRHandler bus)
    {
      _repository = repository;
      _bus = bus;
    }

    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
      var produto = await _repository.ObterPorId(produtoId);

      if (produto is null) return false;

      if (!produto.PossuiEstoque(quantidade)) return false;

      produto.DebitarEstoque(quantidade);
      
      // TODO: Parametrizar a quantidade minima para lançamento do evento
      if (produto.QuantidadeEstoque < 10)
        await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
  
      _repository.Atualizar(produto);
      return await _repository.UnitOfWork.Commit();
    }

    public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
    {
      var produto = await _repository.ObterPorId(produtoId);

      if (produto is null) return false;

      produto.ReporEstoque(quantidade);
      
      _repository.Atualizar(produto);
      return await _repository.UnitOfWork.Commit();
    }

    public void Dispose()
    {
      _repository.Dispose();
    }

  }
}
