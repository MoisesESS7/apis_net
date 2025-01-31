using GerenciamentoFrotaVeiculo.Api.Data.ValueObject.Base;
using GerenciamentoFrotaVeiculo.Api.Hypermedia;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Abstract;

namespace GerenciamentoFrotaVeiculo.Data.ValueObject
{
    public class ColaboradorVO : BaseVO, ISupportsHyperMedia
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Cpf { get; set; } = null!;
        public string? CarteiraHabilitacao { get; set; }
        public string? Endereco { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Dependente { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<ColaboradorVeiculoVO>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculoVO>();
        public ICollection<VeiculoVO>? Veiculos { get; set; } = new List<VeiculoVO>();
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
