using GerenciamentoFrotaVeiculo.Api.Hypermedia.Constants;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher
{
    public class VeiculoEnricher : ContentResponseEnricher<VeiculoVO>
    {
        private readonly object _lock = new object();

        protected override Task EnrichModel(VeiculoVO content, IUrlHelper urlHelper)
        {
            var linkId = GetLink(true, content.Id, urlHelper);
            var link = GetLink(false, content.Id, urlHelper);

            content.Links.Add(new HyperMediaLink
            {
                Rel = HttpActionVerb.Get,
                Href = linkId,
                Action = RelationType.Self,
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

        private string GetLink(bool includId, int? id, IUrlHelper urlHelper)
        {
            ArgumentNullException.ThrowIfNull(urlHelper, nameof(urlHelper));

            try
            {
                lock (_lock)
                {
                    object url = includId 
                        ? new { controller = "Veiculos", id }
                        : new { controller = "Veiculos" };

                    var link = urlHelper.Link("DefaultApi", url);

                    return link ??
                        throw new InvalidOperationException("Falha ao gerar o link.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar link HATOEAS", ex);
            }
        }
    }
}
