using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Api.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MySqlContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MySqlContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            try
            {
                var item = await _dbSet.FindAsync(id);

                return item!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<IList<T>> FindAllAsync()
        {
            try
            {
                var itens = await _dbSet.ToListAsync();
                return itens;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async virtual Task<T> CreateAsync(T item)
        {
            try
            {
                item.Id = 0;
                _dbSet.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async virtual Task<T> UpdateAsync(T item)
        {
            try
            {
                if (item.Id < 1) return null!;
                _dbSet.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                return null!;
            }            
        }
        
        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var item = await FindByIdAsync(id);
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }        
    }
}
