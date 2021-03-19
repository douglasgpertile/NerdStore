using Microsoft.Extensions.DependencyInjection;

using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.Bus;

namespace NerdStore.WebApp.Mvc.Setup
{
  public static class DependencyInjection
  {

    public static void RegisterServices(this IServiceCollection services)
    {
      // Domain Bus (Mediator)
      services.AddScoped<IMediatRHandler, MediatRHandler>();

      // Catalogo
      services.AddScoped<IProdutoRepository, ProdutoRepository>();
      services.AddScoped<IProdutoAppService, ProdutoAppService>();
      services.AddScoped<IEstoqueService, EstoqueService>();
      services.AddScoped<CatalogoContext>();


    }
  }
}
