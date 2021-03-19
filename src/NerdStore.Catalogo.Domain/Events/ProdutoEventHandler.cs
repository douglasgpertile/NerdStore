using MediatR;

using NerdStore.Catalogo.Domain.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Events
{
  public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
  {

    private readonly IProdutoRepository _produtoRepository;

    public ProdutoEventHandler(IProdutoRepository produtoRepository)
    {
      _produtoRepository = produtoRepository;
    }

    public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
    {
      var produto = _produtoRepository.ObterPorId(mensagem.AggregateId);

      // Enviar email para aquicisão de mais produtos.
      // Avisar responsáveis
      // blabla
    }
  }
}
