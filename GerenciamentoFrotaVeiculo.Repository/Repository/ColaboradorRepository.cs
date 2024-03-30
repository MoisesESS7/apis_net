using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Repository.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly GerenciamentoFrotaVeiculoContext _context;

        public ColaboradorRepository(GerenciamentoFrotaVeiculoContext context)
        {
            _context = context;
        }

        public async Task<Colaborador> GetByIdAsync(int id)
        {
            var colaborador = await _context.Colaboradores
                .Include(c => c.ColaboradoresVeiculos)
                .FirstOrDefaultAsync(c => c.Id == id);

            return colaborador!;
        }

        public async Task<List<Colaborador>> GetAllAsync()
        {
            var colaboradores = await _context.Colaboradores.OrderBy(c => c.Id).ToListAsync();

            return colaboradores;
        }
        
        public async Task CreateAsync(Colaborador colaborador)
        {
            await _context.AddAsync(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Colaborador colaboradorRequisicao, Colaborador colaboradorDb)
        {
            colaboradorDb = colaboradorRequisicao;
            _context.ChangeTracker.Clear();

            _context.Update(colaboradorDb);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Colaborador colaborador)
        {
            _context.Remove(colaborador);
            await _context.SaveChangesAsync();
        }
    }
}
