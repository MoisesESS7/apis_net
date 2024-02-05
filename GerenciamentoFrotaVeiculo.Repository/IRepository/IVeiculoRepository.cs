using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository
{
    public interface IVeiculoRepository
    {
        Task<Veiculo> GetAsync(int id);
        Task<List<Veiculo>> GetAllAsync();
        Task CreateAsync(Veiculo veiculo);
        Task UpdateAsync(Veiculo veiculo);
        Task DeleteAsync(Veiculo veiculo);
    }
}
