using GerenciamentoFrotaVeiculo.Api.Generic;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Api.Repository
{
    public class VeiculoRepository : GenericRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(MySqlContext context) : base(context)
        {
        }

        public async Task<ICollection<Veiculo>> FindAllIncludeColaboradoresAsync()
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Include(v => v.Colaboradores)
                    .Include(v => v.ColaboradoresVeiculos)
                    .ToListAsync();

                return veiculos;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<Veiculo> FindByIdIncludeColaboradoresAsync(int id)
        {
            try
            {
                var veiculo = await _context.Veiculos
                    .Include(v => v.Colaboradores)
                    .Include(v => v.ColaboradoresVeiculos)
                    .SingleOrDefaultAsync(x => x.Id == id);

                return veiculo!;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
