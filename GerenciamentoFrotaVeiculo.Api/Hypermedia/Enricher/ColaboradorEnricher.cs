using GerenciamentoFrotaVeiculo.Api.Hypermedia.Constants;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher
{
    public class ColaboradorEnricher : ContentResponseEnricher<ColaboradorVO>
    {
        private readonly object _lock = new object();

        protected override Task EnrichModel(ColaboradorVO content, IUrlHelper urlHelper)
        {
            var linkId = GetLink(true, content.Id, urlHelper);
            var link = GetLink(false, content.Id, urlHelper);

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Get,
                Href = linkId,
                Rel = RelationType.Self,
                Type = ResponseTypeFormat.DefaultGet
            });
            
            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Get,
                Href = $"{link}/incluir-veiculos/{content.Id}",
                Rel = RelationType.Self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Get,
                Href = link,
                Rel = RelationType.Collection,
                Type = ResponseTypeFormat.DefaultGet
            });
            
            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Get,
                Href = $"{link}/incluir-veiculos/",
                Rel = RelationType.Collection,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Post,
                Href = link,
                Rel = RelationType.Create,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Put,
                Href = link,
                Rel = RelationType.Update,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.Delete,
                Href = linkId,
                Rel = RelationType.Delete,
                Type = ResponseTypeFormat.DefaultDelete
            });

            return Task.CompletedTask!;
        }

        private string GetLink(bool includeId, int? id, IUrlHelper urlHelper)
        {
            ArgumentNullException.ThrowIfNull(urlHelper, nameof(urlHelper));

            try
            {
                lock (_lock)
                {
                    object url = includeId ?
                        new { controller = "colaboradores", id } :
                        new { controller = "colaboradores" };

                    var link = urlHelper.Link("DefaultApi", url);

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
