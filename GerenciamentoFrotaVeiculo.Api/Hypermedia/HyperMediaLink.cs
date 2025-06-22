namespace GerenciamentoFrotaVeiculo.Api.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; } = null!;
        public string Href { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Action { get; set; } = null!;
    }
}
