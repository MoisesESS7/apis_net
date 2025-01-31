using GerenciamentoFrotaVeiculo.Api.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Concurrent;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {
        public ContentResponseEnricher()
        {
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        public bool CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                var contentType = okObjectResult.Value!.GetType();

                return contentType == typeof(T) || contentType == typeof(List<T>);
            }

            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is IEnumerable<T> collection)
                { 
                    var bag = new ConcurrentBag<T>(collection);

                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }
            await Task.FromResult<object>(null!);
        }
    }
}
