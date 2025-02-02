using GerenciamentoFrotaVeiculo.Data.ValueObject;

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
    }
}
