using System.Text.Json.Serialization;

namespace GerenciamentoFrotaVeiculo.WebUI.Models
{
    public class ColaboradorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<ColaboradorVeiculoViewModel>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculoViewModel>();
        public ICollection<VeiculoViewModel>? Veiculos { get; set; } = new List<VeiculoViewModel>();

        [JsonPropertyName("links")]
        public ICollection<string>? Links { get; set; } = new List<string>();

        public void RemoverVeiculo(VeiculoViewModel veiculo)
        {
            Veiculos!.Remove(veiculo);
        }
    }
}
