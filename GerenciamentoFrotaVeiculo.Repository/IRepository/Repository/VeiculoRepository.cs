using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly GerenciamentoFrotaVeiculoContext _context;

        public VeiculoRepository(GerenciamentoFrotaVeiculoContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> GetByIdAsync(int id)
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

        public async Task UpdateAsync(Veiculo veiculoDb, Veiculo veiculoRequisicao)
        {
            veiculoDb = veiculoRequisicao;

            _context.ChangeTracker.Clear();

            _context.Veiculos.Update(veiculoDb);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Veiculo veiculo)
        {
            _context.Remove(veiculo!);
            await _context.SaveChangesAsync();
        }
    }
}
