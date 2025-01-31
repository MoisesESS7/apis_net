using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly GerenciamentoFrotaVeiculoContext _context;
        private readonly IParser<Colaborador, ColaboradorVO>
        private readonly IParser<ColaboradorVO, Colaborador>

        public ColaboradorRepository(GerenciamentoFrotaVeiculoContext context)
        {
            _context = context;
        }

        public async Task<Colaborador> GetByIdAsync(int id)
        {
            var colaborador = await _context.Colaboradores
                .Include(c => c.Veiculos)
                .Include(c => c.ColaboradoresVeiculos)
                .SingleOrDefaultAsync(c => c.Id == id);

            return colaborador!;
        }

        public async Task<List<Colaborador>> GetAllAsync()
        {
            var colaboradores = await _context.Colaboradores
                .OrderBy(c => c.Id)
                .Include(x => x.Veiculos)
                .Include(x => x.ColaboradoresVeiculos)
                .ToListAsync();

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

        public async Task<bool> DeleteAsync(Colaborador colaborador)
        {
            try
            {
                _context.Remove(colaborador);
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
