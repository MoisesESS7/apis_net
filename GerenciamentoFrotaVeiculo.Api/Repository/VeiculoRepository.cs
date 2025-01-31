using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Api.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly MySqlContext _context;
        private readonly IParser<Veiculo, VeiculoVO> _veiculoToVOParser;
        private readonly IParser<VeiculoVO, Veiculo> _voToVeiculoParser;

        public VeiculoRepository(MySqlContext context, IParser<Veiculo,
            VeiculoVO> veiculoToVO, IParser<VeiculoVO, Veiculo> voToVeiculo)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _veiculoToVOParser = veiculoToVO ?? throw new ArgumentNullException(nameof(veiculoToVO));
            _voToVeiculoParser = voToVeiculo ?? throw new ArgumentNullException(nameof(voToVeiculo));
        }

        public async Task<VeiculoVO> GetByIdAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            var vo = _veiculoToVOParser.Parse(veiculo!);

            return vo!;
        }

        public async Task<ICollection<VeiculoVO>> GetAllAsync()
        {
            var veiculos = await _context.Veiculos.OrderBy(v => v.Id).ToListAsync();
            var vo = _veiculoToVOParser?.Parse(veiculos);

            return vo!;
        }

        public async Task<VeiculoVO> CreateAsync(VeiculoVO veiculoVO)
        {
            var veiculo = _voToVeiculoParser.Parse(veiculoVO);

            await _context.AddAsync(veiculo);
            await _context.SaveChangesAsync();

            var vo = _veiculoToVOParser.Parse(veiculo);

            return vo;
        }

        public async Task<VeiculoVO> UpdateAsync(VeiculoVO veiculoDbVO, VeiculoVO veiculoRequisicaoVO)
        {
            var veiculoDb = _voToVeiculoParser.Parse(veiculoDbVO);
            var veiculoRequisicao = _voToVeiculoParser.Parse(veiculoRequisicaoVO);
            veiculoDb = veiculoRequisicao;

            _context.ChangeTracker.Clear();
            _context.Veiculos.Update(veiculoDb);
            await _context.SaveChangesAsync();

            var vo = _veiculoToVOParser.Parse(veiculoDb);

            return vo;
        }

        public async Task<bool> DeleteAsync(VeiculoVO veiculoVO)
        {
            try
            {
                var veiculo = _voToVeiculoParser?.Parse(veiculoVO);

                _context.ChangeTracker.Clear();
                _context.Veiculos.Remove(veiculo!);
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
