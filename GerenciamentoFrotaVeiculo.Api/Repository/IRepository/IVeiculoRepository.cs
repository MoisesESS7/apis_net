using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IVeiculoRepository
    {
        Task<Veiculo> FindByIdIncludeColaboradoresAsync(int id);
        Task<ICollection<Veiculo>> FindAllIncludeColaboradoresAsync();
    }
}
