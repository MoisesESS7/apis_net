using GerenciamentoFrotaVeiculo.Api.Hypermedia.Constants;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher
{
    public class ColaboradorVeiculoEnricher : ContentResponseEnricher<ColaboradorVeiculoVO>
    {
        private readonly object _lock = new object();

        protected override Task EnrichModel(ColaboradorVeiculoVO content, IUrlHelper urlHelper)
        {
            var link = GetLink(false, content.Id, urlHelper);
            var linkId = GetLink(true, content.Id, urlHelper);

            content.Links.Add(new HyperMediaLink
            {
                Rel = RelationType.Self,
                Href = linkId,
                Action = HttpActionVerb.Get,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Rel = RelationType.Collection,
                Href = link,
                Action = HttpActionVerb.Get,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Rel = RelationType.Create,
                Href = link,
                Action = HttpActionVerb.Post,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Rel = RelationType.Update,
                Href = linkId,
                Action = HttpActionVerb.Put,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink
            {
                Rel = RelationType.Delete,
                Href = linkId,
                Action = HttpActionVerb.Delete,
                Type = ResponseTypeFormat.DefaultDelete
            });

            return Task.CompletedTask;
        }

        private string GetLink(bool incluirId, int? id, IUrlHelper urlHelper)
        {
            ArgumentNullException.ThrowIfNull(urlHelper, nameof(urlHelper));
            
            try
            {
                lock (_lock)
                {
                    object url = incluirId ?
                        new { controller = $"ColaboradoresVeiculos", id } :
                        new { controlle = "" };

                    var link = (url is "") ? "ColaboradoresVeiculos" :
                        urlHelper.Link("DefaultApi", url)!.Replace("%2F", "/");

                    return link ??
                        throw new InvalidOperationException("Falha ao gerar o link.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar link HATEOAS.", ex);
            }
        }
    }
}
