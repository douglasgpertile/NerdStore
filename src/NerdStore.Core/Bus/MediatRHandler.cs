
using MediatR;

using NerdStore.Core.Messages;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
  public class MediatRHandler : IMediatRHandler
  {

    private readonly IMediator _mediator;

    public MediatRHandler(IMediator mediator) => _mediator = mediator;

    public async Task PublicarEvento<T>(T evento) where T : Event
    {
      await _mediator.Publish(evento);
    }
  }
}
