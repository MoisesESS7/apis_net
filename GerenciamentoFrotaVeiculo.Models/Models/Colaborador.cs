using GerenciamentoFrotaVeiculo.Models.Models.Base;

namespace GerenciamentoFrotaVeiculo.Models
{
    public class Colaborador : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public ICollection<ColaboradorVeiculo>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculo>();
        public ICollection<Veiculo>? Veiculos { get; set; } = new List<Veiculo>();

        public void RemoverVeiculo(Veiculo veiculo)
        {
            Veiculos!.Remove(veiculo);
        }
    }
}
