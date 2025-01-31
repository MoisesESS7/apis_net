using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
