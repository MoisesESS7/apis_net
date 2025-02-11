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

        public async Task<ICollection<Veiculo>> BuscarVeiculoPorPlacaAsync(string placa)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.Placa.Contains(placa))
                    .ToListAsync();

                return veiculos;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorLicenciamentoAsync(bool licenciamento)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.LicenciamentoVigente == licenciamento)
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorAnoAsync(int ano)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.AnoFabricacao.Year == ano)
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorCategoriaAsync(string categoria)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.Categoria.Contains(categoria))
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorModeloAsync(string modelo)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.Modelo.Contains(modelo))
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorMarcaAsync(string marca)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.Marca.Contains(marca))
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorQuilometragemAsync(int minimo, int maximo)
        {
            try
            {
                if (minimo > maximo) return null!;

                var veiculos = await _context.Veiculos
                    .Where(v => Convert.ToInt32(v.Quilometragem) >= minimo && Convert.ToInt32(v.Quilometragem) <= maximo)
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosPorCorAsync(string cor)
        {
            try
            {
                var veiculos = await _context.Veiculos
                    .Where(v => v.Cor.Contains(cor))
                    .ToListAsync();

                return veiculos!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<Veiculo>> BuscarVeiculosAsync(string? placa, string? marca, int? quilometragemMinima,
            int? quilometragemMaxima, string? cor, int? ano, string? modelo, string? categoria, bool? licenciamento)
        {
            try
            {                
                var query = _context.Veiculos.AsQueryable();

                if (!string.IsNullOrEmpty(placa))
                    query = query.Where(v => v.Placa.Contains(placa));

                if (!string.IsNullOrEmpty(marca))
                    query = query.Where(v => v.Marca.Contains(marca));

                if (quilometragemMinima > 0)
                    query = query.Where(v => Convert.ToInt32(v.Quilometragem) >= quilometragemMinima.Value);

                if (quilometragemMaxima > 0)
                    query = query.Where(v => Convert.ToInt32(v.Quilometragem) <= quilometragemMaxima.Value);

                if (!string.IsNullOrEmpty(cor))
                    query = query.Where(v => v.Cor.Contains(cor));

                if (ano > 0)
                    query = query.Where(v => v.AnoFabricacao.Year == ano.Value);

                if (!string.IsNullOrEmpty(modelo))
                    query = query.Where(v => v.Modelo.Contains(modelo));

                if (!string.IsNullOrEmpty(categoria))
                    query = query.Where(v => v.Categoria.Contains(categoria));

                if (licenciamento.HasValue)
                    query = query.Where(v => v.LicenciamentoVigente == licenciamento.Value);

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
