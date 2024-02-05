using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository
{
    public interface IColaboradorVeiculoRepository
    {
        Task<ColaboradorVeiculo> GetAsync(int id, int otherId);
        Task<List<ColaboradorVeiculo>> GetAllAsync();
        Task CreateAsync(ColaboradorVeiculo colaboradorVeiculo);
        Task UpdateAsync(ColaboradorVeiculo colaboradorVeiculoRequisicao, ColaboradorVeiculo colaboradorVeiculoDb);
        Task DeleteAsync(ColaboradorVeiculo colaboradorVeiculo);
    }
}
