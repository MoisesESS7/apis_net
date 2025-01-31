using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

        public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions ?? throw new ArgumentNullException(nameof(hyperMediaFilterOptions));
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult okObjectResult)
            {
                var enricher = _hyperMediaFilterOptions
                    .ContentResponseEnricherList.FirstOrDefault(x => x.CanEnrich(context));

                if(enricher is not null)
                {
                    enricher.Enrich(context).Wait();
                }
            }

            base.OnResultExecuting(context);
        }
    }
}
