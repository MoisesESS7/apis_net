using GerenciamentoFrotaVeiculo.Api.Generic;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Api.Repository
{
    public class ColaboradorRepository(MySqlContext context) : GenericRepository<Colaborador>(context), IColaboradorRepository
    {
        public async Task<ICollection<Colaborador>> FindByNameAsync(string nome)
        {
            try
            {
                var colaboradores = await _context.Colaboradores
                    .Where(c => c.Nome.Contains(nome)).ToListAsync();

                return colaboradores!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<Colaborador> BuscaCompletaAsync(int id)
        {
            try
            {
                var colaborador = await _context.Colaboradores
                .Include(c => c.Veiculos)
                .Include(c => c.ColaboradoresVeiculos)
                .SingleOrDefaultAsync(c => c.Id == id);

                return colaborador!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Colaborador>> FindAllIncludeVeiculosAsync()
        {
            try
            {
                var colaboradores = await _context.Colaboradores
                .OrderBy(c => c.Id)
                .Include(x => x.Veiculos)
                .Include(x => x.ColaboradoresVeiculos)
                .ToListAsync();

                return colaboradores;
            }
            catch (Exception)
            {
                return null!; ;
            }
        }

        public async Task<Colaborador> FindByCpfAsync(string cpf)
        {
            try
            {
                var colaborador = await _context.Colaboradores
                    .Where(c => c.Cpf.Equals(cpf))
                    .SingleOrDefaultAsync();

                return colaborador!;
            }
            catch (Exception)
            {
                return null!; ;
            }
        }

        public async Task<ICollection<Veiculo>> FindAllVeiculosByIdColaboradorAsync(int id)
        {
            try
            {
                var veiculos = await _context.Colaboradores
                    .Where(c => c.Id == id)
                    .SelectMany(v => v.Veiculos!)
                    .ToListAsync();

                if (!veiculos.Any()) return null!;

                return veiculos;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
