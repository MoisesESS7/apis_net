using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Business
{
    public interface IVeiculoBusiness
    {
        Task<VeiculoVO> FindByIdAsync(int id);
        Task<VeiculoVO> FindByIdIncludeColaboradoresAsync(int id);
        Task<ICollection<VeiculoVO>> FindAllAsync();
        Task<ICollection<VeiculoVO>> FindAllIncludeColaboradoresAsync();
        Task<VeiculoVO> CreateAsync(VeiculoVO vo);
        Task<VeiculoVO> UpdateAsync(VeiculoVO veiculoDbVO);
        Task<bool> DeleteAsync(int id);

        Task<ICollection<VeiculoVO>> BuscarVeiculosPorAnoAsync(int ano);
        Task<ICollection<VeiculoVO>> BuscarVeiculosPorPlacaAsync(string placa);
        Task<ICollection<VeiculoVO>> BuscarVeiculosPorModeloAsync(string modelo);
        Task<ICollection<VeiculoVO>> BuscarVeiculosPorCategoriaAsync(string categoria);
        Task<ICollection<VeiculoVO>> BuscarVeiculosPorLicenciamentoAsync(bool licenciamento);

        Task<ICollection<VeiculoVO>> BuscarVeiculosPorMarcaAsync(string marca);
        Task<ICollection<VeiculoVO>> BuscarVeiculosPorQuilometragemAsync(int minimo, int maximo);
        Task<ICollection<VeiculoVO>> BuscarVeiculosPorCorAsync(string cor);

        Task<ICollection<VeiculoVO>> BuscarVeiculosAsync(string? placa, string? marca, int? quilometragemMinima,
            int? quilometragemMaxima, string? cor, int? ano, string? modelo, string? categoria, bool? licenciamento);
    }
}
