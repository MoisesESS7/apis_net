namespace GerenciamentoFrotaVeiculo.WebUI.Models
{
    public class ColaboradorVeiculoViewModel
    {
        public int ColaboradorId { get; set; }
        public int VeiculoId { get; set; }
        public DateTimeOffset DataInicioVinculo { get; set; }

        public ColaboradorViewModel? Colaborador { get; set; }
        public VeiculoViewModel? Veiculo { get; set; }
    }
}
