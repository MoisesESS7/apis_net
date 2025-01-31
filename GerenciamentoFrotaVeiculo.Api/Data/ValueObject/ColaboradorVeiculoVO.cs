using GerenciamentoFrotaVeiculo.Api.Data.ValueObject.Base;
using GerenciamentoFrotaVeiculo.Api.Hypermedia;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Abstract;

namespace GerenciamentoFrotaVeiculo.Data.ValueObject
{
    public class ColaboradorVeiculoVO : BaseVO, ISupportsHyperMedia
    {
        public int ColaboradorId { get; set; }
        public int VeiculoId { get; set; }
        public string ColaboradorNomeCompleto { get; set; } = null!;
        public string VeiculoModelo { get; set; } = null!;
        public DateTimeOffset DataInicioVinculo { get; set; }
        public ColaboradorVO? Colaborador { get; set; }
        public VeiculoVO? Veiculo { get; set; }
        public List<HyperMediaLink> Links { get ; set ; } = new List<HyperMediaLink>();
    }
}
