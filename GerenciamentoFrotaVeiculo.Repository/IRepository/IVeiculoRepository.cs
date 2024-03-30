using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository
{
    public interface IVeiculoRepository
    {
        Task<Veiculo> GetByIdAsync(int id);
        Task<List<Veiculo>> GetAllAsync();
        Task CreateAsync(Veiculo veiculo);
        Task UpdateAsync(Veiculo veiculo, Veiculo outroVeiculo);
        Task DeleteAsync(Veiculo veiculo);
    }
}
