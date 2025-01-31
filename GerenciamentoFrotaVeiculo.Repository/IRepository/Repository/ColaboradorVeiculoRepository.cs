using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository.Repository
{
    public class ColaboradorVeiculoRepository : IColaboradorVeiculoRepository
    {
        private readonly GerenciamentoFrotaVeiculoContext _context;

        public ColaboradorVeiculoRepository(GerenciamentoFrotaVeiculoContext context)
        {
            _context = context;
        }

        public async Task<ColaboradorVeiculo> GetByIdAsync(int id_1, int id_2)
        {
            var colaboradorVeiculo = await _context.ColaboradoresVeiculos.FindAsync(id_1, id_2);

            return colaboradorVeiculo!;
        }

        public async Task<List<ColaboradorVeiculo>> GetAllAsync()
        {
            var colaboradoresVeiculos = await _context.ColaboradoresVeiculos.ToListAsync();

            return colaboradoresVeiculos;
        }

        public async Task CreateAsync(ColaboradorVeiculo colaboradorVeiculo)
        {
            await _context.ColaboradoresVeiculos.AddAsync(colaboradorVeiculo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ColaboradorVeiculo colaboradorVeiculoRequisicao, ColaboradorVeiculo colaboradorVeiculoDb)
        {
            colaboradorVeiculoDb = colaboradorVeiculoRequisicao;
            _context.ChangeTracker.Clear();

            _context.ColaboradoresVeiculos.Update(colaboradorVeiculoDb);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ColaboradorVeiculo colaboradorVeiculo)
        {
            _context.ColaboradoresVeiculos.Remove(colaboradorVeiculo);
            await _context.SaveChangesAsync();
        }
    }
}
