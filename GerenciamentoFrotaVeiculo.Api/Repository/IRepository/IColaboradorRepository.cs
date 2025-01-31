using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IColaboradorRepository
    {
        Task<Colaborador> FindByIdIncludeVeiculosAsync(int id);
        Task<ICollection<Colaborador>> FindAllIncludeVeiculosAsync();
    }
}
