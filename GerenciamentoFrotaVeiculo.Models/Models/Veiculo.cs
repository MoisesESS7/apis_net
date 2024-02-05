namespace GerenciamentoFrotaVeiculo.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public DateTime Ano { get; set; } = DateTime.Now;
        public ICollection<ColaboradorVeiculo>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculo>();
        public ICollection<Colaborador>? Colaboradores { get; set; } = new List<Colaborador>();
    }
}
