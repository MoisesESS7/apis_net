using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Api.Repository
{
    public class ColaboradorVeiculoRepository : IColaboradorVeiculoRepository
    {
        private readonly MySqlContext _context;
        private readonly IParser<ColaboradorVeiculo, ColaboradorVeiculoVO> _colaboradorVeiculoToVoParse;
        private readonly IParser<ColaboradorVeiculoVO, ColaboradorVeiculo> _voTocolaboradorVeiculoParse;

        public ColaboradorVeiculoRepository(MySqlContext context,
            IParser<ColaboradorVeiculo, ColaboradorVeiculoVO> colaboradorVeiculoToVoParse,
            IParser<ColaboradorVeiculoVO, ColaboradorVeiculo> voTocolaboradorVeiculoParse)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _colaboradorVeiculoToVoParse = colaboradorVeiculoToVoParse ?? throw new ArgumentNullException(nameof(colaboradorVeiculoToVoParse));
            _voTocolaboradorVeiculoParse = voTocolaboradorVeiculoParse ?? throw new ArgumentNullException(nameof(voTocolaboradorVeiculoParse));
        }

        public async Task<ColaboradorVeiculoVO> GetByIdAsync(int id)
        {
            var colaboradorVeiculo = await _context.ColaboradoresVeiculos.FindAsync(id);

            var vo = _colaboradorVeiculoToVoParse.Parse(colaboradorVeiculo!);

            return vo;
        }

        public async Task<ICollection<ColaboradorVeiculoVO>> GetAllAsync()
        {
            var colaboradoresVeiculos = await _context.ColaboradoresVeiculos.ToListAsync();
            
            var vo = _colaboradorVeiculoToVoParse.Parse(colaboradoresVeiculos!);

            return vo;
        }

        public async Task<ColaboradorVeiculoVO> CreateAsync(ColaboradorVeiculoVO colaboradorVeiculoVO)
        {
            try
            {
                var colaboradorVeiculo = _voTocolaboradorVeiculoParse.Parse(colaboradorVeiculoVO);
                var colaborador = await _context.Colaboradores.FindAsync(colaboradorVeiculoVO.ColaboradorId);
                var veiculo = await _context.Veiculos.FindAsync(colaboradorVeiculoVO.VeiculoId);

                colaboradorVeiculo.Colaborador = colaborador;
                colaboradorVeiculo.Veiculo = veiculo;
                colaboradorVeiculo.DataInicioVinculo = DateTime.Now;

                await _context.ColaboradoresVeiculos.AddAsync(colaboradorVeiculo);
                await _context.SaveChangesAsync();

                var vo = _colaboradorVeiculoToVoParse.Parse(colaboradorVeiculo);

                return vo;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVeiculoVO> UpdateAsync(ColaboradorVeiculoVO colaboradorVeiculoRequisicaoVO, ColaboradorVeiculoVO colaboradorVeiculoDbVO)
        {
            var colaboradorVeiculoRequisicao = _voTocolaboradorVeiculoParse.Parse(colaboradorVeiculoRequisicaoVO);
            var colaboradorVeiculoDb = _voTocolaboradorVeiculoParse.Parse(colaboradorVeiculoDbVO);
            colaboradorVeiculoDb = colaboradorVeiculoRequisicao;

            _context.ChangeTracker.Clear();
            _context.ColaboradoresVeiculos.Update(colaboradorVeiculoDb);
            await _context.SaveChangesAsync();

            var vo = _colaboradorVeiculoToVoParse.Parse(colaboradorVeiculoDb);

            return vo;
        }

        public async Task<bool> DeleteAsync(ColaboradorVeiculoVO colaboradorVeiculoVO)
        {
            try
            {
                var colaboradorVeiculo = _voTocolaboradorVeiculoParse.Parse(colaboradorVeiculoVO);

                _context.ChangeTracker.Clear();
                _context.ColaboradoresVeiculos.Remove(colaboradorVeiculo);
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
