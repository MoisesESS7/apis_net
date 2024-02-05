using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.Context;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Repository.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly ECommerceContext _context;

        public VeiculoRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> GetAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            return veiculo!;
        }

        public async Task<List<Veiculo>> GetAllAsync()
        {
            var veiculos = await _context.Veiculos.OrderBy(v => v.Id).ToListAsync();

            return veiculos;
        }

        public async Task CreateAsync(Veiculo veiculo)
        {
           await _context.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Veiculo veiculo)
        {
            _context.Remove(veiculo!);
            await _context.SaveChangesAsync();
        }
    }
}
