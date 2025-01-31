using GerenciamentoFrotaVeiculo.Models.Base;

namespace GerenciamentoFrotaVeiculo.Models
{
    public class Veiculo : BaseEntity
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
        public ICollection<ColaboradorVeiculo>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculo>();
        public ICollection<Colaborador>? Colaboradores { get; set; } = new List<Colaborador>();
    }
}
