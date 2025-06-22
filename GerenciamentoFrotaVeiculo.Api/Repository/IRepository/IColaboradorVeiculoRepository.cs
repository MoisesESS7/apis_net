using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IColaboradorVeiculoRepository : IGenericRepository<ColaboradorVeiculo>
    {
        Task<ICollection<ColaboradorVeiculo>> FindByDataAsync(DateTime data);
    }
}
