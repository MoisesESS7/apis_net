using GerenciamentoFrotaVeiculo.Api.Hypermedia.Helpers;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher
{
    public class ColaboradorVeiculoEnricher : ContentResponseEnricher<ColaboradorVeiculoVO>
    {
        private readonly HyperMediaLinkBuilder _hyperMediaLinkBuilder;

        public ColaboradorVeiculoEnricher(HyperMediaLinkBuilder hyperMediaLinkBuilder)
        {
            _hyperMediaLinkBuilder = hyperMediaLinkBuilder;
        }

        protected override Task EnrichModel(ColaboradorVeiculoVO content, ResultExecutingContext response)
        {
            var link = _hyperMediaLinkBuilder.BuildLinks(response);

            //content.Links.Add(new HyperMediaLink
            //{
            //    Rel = RelationType.Self,
            //    Href = linkId,
            //    Action = HttpActionVerb.Get,
            //    Type = ResponseTypeFormat.DefaultGet
            //});

            //content.Links.Add(new HyperMediaLink
            //{
            //    Rel = RelationType.Collection,
            //    Href = link,
            //    Action = HttpActionVerb.Get,
            //    Type = ResponseTypeFormat.DefaultGet
            //});

            //content.Links.Add(new HyperMediaLink
            //{
            //    Rel = RelationType.Create,
            //    Href = link,
            //    Action = HttpActionVerb.Post,
            //    Type = ResponseTypeFormat.DefaultPost
            //});

            //content.Links.Add(new HyperMediaLink
            //{
            //    Rel = RelationType.Update,
            //    Href = link,
            //    Action = HttpActionVerb.Put,
            //    Type = ResponseTypeFormat.DefaultPut
            //});

            //content.Links.Add(new HyperMediaLink
            //{
            //    Rel = RelationType.Delete,
            //    Href = linkId,
            //    Action = HttpActionVerb.Delete,
            //    Type = ResponseTypeFormat.DefaultDelete
            //});

            return Task.CompletedTask;
        }
    }
}
