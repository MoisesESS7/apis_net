using GerenciamentoFrotaVeiculo.Models.Base;

namespace GerenciamentoFrotaVeiculo.Models
{
    public class ColaboradorVeiculo : BaseEntity
    {
        public int ColaboradorId { get; set; }
        public int VeiculoId { get; set; }
        public string ColaboradorNomeCompleto { get; set; } = null!;
        public string VeiculoModelo { get; set; } = null!;
        public DateTimeOffset DataInicioVinculo { get; set; }
        public Colaborador? Colaborador { get; set; }
        public Veiculo? Veiculo { get; set; }
    }
}
