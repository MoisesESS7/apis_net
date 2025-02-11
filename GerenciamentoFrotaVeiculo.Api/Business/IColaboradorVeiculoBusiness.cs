using GerenciamentoFrotaVeiculo.Data.ValueObject;

namespace GerenciamentoFrotaVeiculo.Api.Business
{
    public interface IColaboradorVeiculoBusiness
    {
        Task<ColaboradorVeiculoVO> FindByIdAsync(int id);
        Task<ICollection<ColaboradorVeiculoVO>> FindByDataAsync(DateTime data);
        Task<ICollection<ColaboradorVeiculoVO>> FindAllAsync();
        Task<ColaboradorVeiculoVO> CreateAsync(ColaboradorVeiculoVO colaborador);
        Task<ColaboradorVeiculoVO> UpdateAsync(ColaboradorVeiculoVO colaboradorVO);
        Task<bool> DeleteByIdAsync(int id);
    }
}
