using GerenciamentoFrotaVeiculo.Data.ValueObject;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IVeiculoRepository
    {
        Task<VeiculoVO> GetByIdAsync(int id);
        Task<ICollection<VeiculoVO>> GetAllAsync();
        Task<VeiculoVO> CreateAsync(VeiculoVO vo);
        Task<VeiculoVO> UpdateAsync(VeiculoVO veiculoDbVO, VeiculoVO veiculoRequisicaoVO);
        Task<bool> DeleteAsync(VeiculoVO vo);
    }
}
