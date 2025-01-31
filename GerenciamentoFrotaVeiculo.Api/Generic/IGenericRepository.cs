using GerenciamentoFrotaVeiculo.Models.Base;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IList<T>> FindAllAsync();
    Task<T> FindByIdAsync(int id);
    Task<T> CreateAsync(T item);
    Task<T> UpdateAsync(T item);
    Task<bool> DeleteByIdAsync(int id);
}