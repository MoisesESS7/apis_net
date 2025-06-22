using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Repository.IRepository
{
    public interface IColaboradorRepository : IGenericRepository<Colaborador>
    {
        Task<Colaborador> BuscaCompletaAsync(int id);
        Task<ICollection<Veiculo>> FindAllVeiculosByIdColaboradorAsync(int id);
        Task<ICollection<Colaborador>> FindAllIncludeVeiculosAsync();
        Task<ICollection<Colaborador>> FindByNameAsync(string nome);
        Task<Colaborador> FindByCpfAsync(string cpf);
    }
}
