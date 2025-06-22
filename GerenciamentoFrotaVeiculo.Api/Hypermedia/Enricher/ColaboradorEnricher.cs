using GerenciamentoFrotaVeiculo.Api.Hypermedia.Constants;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Helpers;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher
{
    public class ColaboradorEnricher : ContentResponseEnricher<ColaboradorVO>
    {
        private readonly HyperMediaLinkBuilder _hyperMediaLinkBuilder;

        public ColaboradorEnricher(HyperMediaLinkBuilder hyperMediaLinkBuilder)
        {
            _hyperMediaLinkBuilder = hyperMediaLinkBuilder;
        }

        protected override Task EnrichModel(ColaboradorVO content, ResultExecutingContext response)
        {
            var links = _hyperMediaLinkBuilder.BuildLinks(response);

            // GET único (por id)
            if (links.TryGetValue("GET {Id}", out var getByIdLink))
            {
                var get = HyperMediaLinkFactory.Create(HttpActionVerb.Get, getByIdLink, RelationType.Self, ResponseTypeFormat.DefaultGet);
                content.Links.Add(get);
            }

            // GET todos
            if (links.TryGetValue("GET ", out var getAllLink))
            {
                var getAll = HyperMediaLinkFactory.Create(HttpActionVerb.Get, getAllLink, RelationType.Collection, ResponseTypeFormat.DefaultGet);
                content.Links.Add(getAll);
            }
            
            // GET
            if (links.TryGetValue("GET buscar-por-nome", out var getByName))
            {
                var getAll = HyperMediaLinkFactory.Create(HttpActionVerb.Get, getByName, RelationType.Collection, ResponseTypeFormat.DefaultGet);
                content.Links.Add(getAll);
            }
            
            //GET
            if (links.TryGetValue("GET buscar-por-cpf/{cpf}", out var getByCpf))
            {
                var getAll = HyperMediaLinkFactory.Create(HttpActionVerb.Get, getByCpf, RelationType.Collection, ResponseTypeFormat.DefaultGet);
                content.Links.Add(getAll);
            }
            
            //GET
            if (links.TryGetValue("GET buscar-veiculos/{colaboradorId}", out var getVeicle))
            {
                var getAll = HyperMediaLinkFactory.Create(HttpActionVerb.Get, getVeicle, RelationType.Collection, ResponseTypeFormat.DefaultGet);
                content.Links.Add(getAll);
            }

            // GET incluir veículos
            if (links.TryGetValue("GET incluir-veiculos/{Id}", out var incluirVeiculosLink))
            {
                var getIncludeVeicle = HyperMediaLinkFactory.Create(HttpActionVerb.Get, incluirVeiculosLink, "incluir-veiculos", ResponseTypeFormat.DefaultGet);
                content.Links.Add(getIncludeVeicle);
            }

            // PUT
            if (links.TryGetValue("PUT ", out var putLink))
            {
                var update = HyperMediaLinkFactory.Create(HttpActionVerb.Put, putLink, RelationType.Update, ResponseTypeFormat.DefaultPut);
                content.Links.Add(update);
            }

            // DELETE
            if (links.TryGetValue("DELETE {Id}", out var deleteLink))
            {
                var delete = HyperMediaLinkFactory.Create(HttpActionVerb.Delete, deleteLink, RelationType.Delete, ResponseTypeFormat.DefaultDelete);
                content.Links.Add(delete);
            }

            // POST
            if (links.TryGetValue("POST ", out var postLink))
            {
                var create = HyperMediaLinkFactory.Create(HttpActionVerb.Post, postLink, RelationType.Create, ResponseTypeFormat.DefaultPost);
                content.Links.Add(create);
            }

            return Task.CompletedTask;
        }
    }
}
