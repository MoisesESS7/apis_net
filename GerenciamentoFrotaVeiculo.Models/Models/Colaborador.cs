namespace GerenciamentoFrotaVeiculo.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<ColaboradorVeiculo>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculo>();
        public ICollection<Veiculo>? Veiculos { get; private set; } = new List<Veiculo>();

        public void RemoverVeiculo(Veiculo veiculo)
        {
            Veiculos!.Remove(veiculo);
        }
    }
}
