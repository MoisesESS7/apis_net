using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IVeiculoRepository : IGenericRepository<Veiculo>
    {
        Task<Veiculo> FindByIdIncludeColaboradoresAsync(int id);

        Task<ICollection<Veiculo>> BuscarVeiculoPorPlacaAsync(string placa);
        Task<ICollection<Veiculo>> BuscarVeiculosPorMarcaAsync(string marca);
        Task<ICollection<Veiculo>> BuscarVeiculosPorQuilometragemAsync(int minimo, int maximo);
        Task<ICollection<Veiculo>> BuscarVeiculosPorCorAsync(string cor);

        Task<ICollection<Veiculo>> BuscarVeiculosPorAnoAsync(int ano);
        Task<ICollection<Veiculo>> BuscarVeiculosPorModeloAsync(string modelo);
        Task<ICollection<Veiculo>> BuscarVeiculosPorCategoriaAsync(string categoria);
        Task<ICollection<Veiculo>> BuscarVeiculosPorLicenciamentoAsync(bool licenciamento);
        Task<ICollection<Veiculo>> FindAllIncludeColaboradoresAsync();

        Task<ICollection<Veiculo>> BuscarVeiculosAsync(string? placa, string? marca, int? quilometragemMinima,
            int? quilometragemMaxima, string? cor, int? ano, string? modelo, string? categoria, bool? licenciamento);
    }
}
