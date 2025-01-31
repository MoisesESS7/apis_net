using GerenciamentoFrotaVeiculo.Data.ValueObject;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IColaboradorVeiculoRepository
    {
        Task<ColaboradorVeiculoVO> GetByIdAsync(int id);
        Task<ICollection<ColaboradorVeiculoVO>> GetAllAsync();
        Task<ColaboradorVeiculoVO> CreateAsync(ColaboradorVeiculoVO colaboradorVeiculo);
        Task<ColaboradorVeiculoVO> UpdateAsync(ColaboradorVeiculoVO colaboradorVeiculoRequisicao, ColaboradorVeiculoVO colaboradorVeiculoDb);
        Task<bool> DeleteAsync(ColaboradorVeiculoVO colaboradorVeiculo);
    }
}
