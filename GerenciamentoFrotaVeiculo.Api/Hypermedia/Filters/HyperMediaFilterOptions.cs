using GerenciamentoFrotaVeiculo.Api.Hypermedia.Abstract;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
