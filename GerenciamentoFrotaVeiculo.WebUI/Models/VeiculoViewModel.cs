namespace GerenciamentoFrotaVeiculo.WebUI.Models
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public DateTime Ano { get; set; } = DateTime.Now;
        public ICollection<ColaboradorVeiculoViewModel>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculoViewModel>();
        public ICollection<ColaboradorViewModel>? Colaboradores { get; set; } = new List<ColaboradorViewModel>();
    }
}
