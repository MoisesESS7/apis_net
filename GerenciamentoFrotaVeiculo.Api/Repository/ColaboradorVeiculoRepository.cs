using GerenciamentoFrotaVeiculo.Api.Generic;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Api.Repository
{
    public class ColaboradorVeiculoRepository : GenericRepository<ColaboradorVeiculo>, IColaboradorVeiculoRepository
    {
        public ColaboradorVeiculoRepository(MySqlContext context) : base(context){ }

        public async Task<ICollection<ColaboradorVeiculo>> FindByDataAsync(DateTime data)
        {
            try
            {
                var colaboradoresVeiculos = await _context.ColaboradoresVeiculos
                    .Where(c => c.DataInicioVinculo.Date.Equals(data.Date))
                    .ToListAsync();

                if(colaboradoresVeiculos is null || !colaboradoresVeiculos.Any()) return null!;

                return colaboradoresVeiculos;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async override Task<ColaboradorVeiculo> CreateAsync(ColaboradorVeiculo colaboradorVeiculo)
        {
            try
            {
                var colaborador = await _context.Colaboradores
                    .FirstOrDefaultAsync(c => c.Id == colaboradorVeiculo.ColaboradorId);

                if (colaborador is null) return null!;

                var veiculo = await _context.Veiculos
                    .FirstOrDefaultAsync(v => v.Id == colaboradorVeiculo.VeiculoId);

                if (veiculo is null) return null!;

                colaboradorVeiculo.Colaborador = colaborador;
                colaboradorVeiculo.ColaboradorNomeCompleto = colaborador.Nome;
                colaboradorVeiculo.Veiculo = veiculo;
                colaboradorVeiculo.VeiculoModelo = veiculo.Modelo;
                colaboradorVeiculo.DataInicioVinculo = DateTime.Now;

                return await base.CreateAsync(colaboradorVeiculo);
            }
            catch (Exception)
            {
                return null!;
            }
        }
        
        public async override Task<ColaboradorVeiculo> UpdateAsync(ColaboradorVeiculo colaboradorVeiculo)
        {
            try
            {
                var colaborador = await _context.Colaboradores
                    .FirstOrDefaultAsync(c => c.Id == colaboradorVeiculo.ColaboradorId);

                if (colaborador is null) return null!;

                var veiculo = await _context.Veiculos
                    .FirstOrDefaultAsync(v => v.Id == colaboradorVeiculo.VeiculoId);

                if (veiculo is null) return null!;

                colaboradorVeiculo.Colaborador = colaborador;
                colaboradorVeiculo.ColaboradorNomeCompleto = colaborador.Nome;
                colaboradorVeiculo.Veiculo = veiculo;
                colaboradorVeiculo.VeiculoModelo = veiculo.Modelo;
                colaboradorVeiculo.DataInicioVinculo = DateTime.Now;

                return await base.UpdateAsync(colaboradorVeiculo);
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
