namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Helpers
{
    public class HyperMediaLinkFactory
    {
        public static HyperMediaLink Create(string action, string href, string rel, string type)
        {
            return new HyperMediaLink
            {
                Action = action,
                Href = href,
                Rel = rel,
                Type = type
            };
        }
    }
}
