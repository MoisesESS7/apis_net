namespace GerenciamentoFrotaVeiculo.Api.Hypermedia
{
    public class HyperMediaLink
    {
        private readonly object _lock = new object();
        private string _href = null!;

        public string Rel { get; set; } = null!;

        public string Href
        {
            get
            {
                lock (_lock)
                {
                    return _href.Replace("%2f", "/")!;
                }
            }

            set
            {
                lock (_lock)
                {
                    _href = value;
                }
            }
        }

        public string Type { get; set; } = null!;
        public string Action { get; set; } = null!;
    }
}
