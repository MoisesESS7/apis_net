using GerenciamentoFrotaVeiculo.Api.Data.ValueObject.Base;
using GerenciamentoFrotaVeiculo.Api.Hypermedia;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Abstract;

namespace GerenciamentoFrotaVeiculo.Data.ValueObject
{
    public class VeiculoVO : BaseVO, ISupportsHyperMedia
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Quilometragem { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public bool LicenciamentoVigente { get; set; }
        public DateTime DataLicenciamento { get; set; } = DateTime.Now;
        public DateTime AnoFabricacao { get; set; } = DateTime.Now;
        public DateTime AnoModelo { get; set; } = DateTime.Now;
        public ICollection<ColaboradorVeiculoVO>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculoVO>();
        public ICollection<ColaboradorVO>? Colaboradores { get; set; } = new List<ColaboradorVO>();
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
