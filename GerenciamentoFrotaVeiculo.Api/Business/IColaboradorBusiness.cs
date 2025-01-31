using GerenciamentoFrotaVeiculo.Data.ValueObject;

namespace GerenciamentoFrotaVeiculo.Api.Business
{
    public interface IColaboradorBusiness
    {
        Task<ColaboradorVO> FindByIdAsync(int id);
        Task<ColaboradorVO> FindByIdIncludeVeiculosAsync(int id);
        Task<ICollection<ColaboradorVO>> FindAllAsync();
        Task<ICollection<ColaboradorVO>> FindAllIncludeVeiculosAsync();
        Task<ColaboradorVO> CreateAsync(ColaboradorVO colaborador);
        Task<ColaboradorVO> UpdateAsync(ColaboradorVO colaboradorVO);
        Task<bool> DeleteByIdAsync(int id);
    }
}
