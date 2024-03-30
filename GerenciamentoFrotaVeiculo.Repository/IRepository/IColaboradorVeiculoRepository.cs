using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository
{
    public interface IColaboradorVeiculoRepository
    {
        Task<ColaboradorVeiculo> GetByIdAsync(int colaboradorId, int veiculoId);
        Task<List<ColaboradorVeiculo>> GetAllAsync();
        Task CreateAsync(ColaboradorVeiculo colaboradorVeiculo);
        Task UpdateAsync(ColaboradorVeiculo colaboradorVeiculoRequisicao, ColaboradorVeiculo colaboradorVeiculoDb);
        Task DeleteAsync(ColaboradorVeiculo colaboradorVeiculo);
    }
}
