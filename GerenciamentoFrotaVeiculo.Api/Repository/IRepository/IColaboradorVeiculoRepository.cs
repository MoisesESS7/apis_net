using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IColaboradorVeiculoRepository
    {
        Task<ICollection<ColaboradorVeiculo>> FindByDataAsync(DateTime data);
    }
}
