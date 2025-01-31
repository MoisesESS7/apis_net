using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository
{
    public interface IColaboradorRepository
    {
        Task<Colaborador> GetByIdAsync(int id);
        Task<List<Colaborador>> GetAllAsync();
        Task CreateAsync(Colaborador colaborador);
        Task UpdateAsync(Colaborador colaboradorRequisicao, Colaborador colaboradorDb);
        Task<bool> DeleteAsync(Colaborador colaborador);
    }
}
