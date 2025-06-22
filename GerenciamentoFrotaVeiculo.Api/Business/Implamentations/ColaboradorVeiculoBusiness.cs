using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Business.Implamentations
{
    public class ColaboradorVeiculoBusiness : IColaboradorVeiculoBusiness
    {
        private readonly IColaboradorVeiculoRepository _colaboradorVeiculoRepository;
        private readonly IParser<ColaboradorVeiculo, ColaboradorVeiculoVO> _colaboradorVeiculoToVoParser;
        private readonly IParser<ColaboradorVeiculoVO, ColaboradorVeiculo> _voToColaboradorVeiculoParser;

        public ColaboradorVeiculoBusiness(
            IColaboradorVeiculoRepository colaboradorVeiculoRepository,
            IParser<ColaboradorVeiculo, ColaboradorVeiculoVO> colaboradorToVoParser,
            IParser<ColaboradorVeiculoVO, ColaboradorVeiculo> voToColaboradorParser)
        {
            _colaboradorVeiculoRepository = colaboradorVeiculoRepository ?? throw new ArgumentNullException(nameof(colaboradorVeiculoRepository));
            _colaboradorVeiculoToVoParser = colaboradorToVoParser ?? throw new ArgumentNullException(nameof(colaboradorToVoParser));
            _voToColaboradorVeiculoParser = voToColaboradorParser ?? throw new ArgumentNullException(nameof(voToColaboradorParser));
        }

        public async Task<ICollection<ColaboradorVeiculoVO>> FindByDataAsync(DateTime data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.ToString())) return null!;

                var colaboradorVeiculo = await _colaboradorVeiculoRepository.FindByDataAsync(data);

                if (colaboradorVeiculo is null) return null!;

                return _colaboradorVeiculoToVoParser.Parse(colaboradorVeiculo);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<ColaboradorVeiculoVO>> FindAllAsync()
        {
            try
            {
                var colaboradoresVeiculos = await _colaboradorVeiculoRepository.FindAllAsync();

                if (colaboradoresVeiculos is null) return null!;

                return _colaboradorVeiculoToVoParser.Parse(colaboradoresVeiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVeiculoVO> FindByIdAsync(int id)
        {
            try
            {
                var colaboradorVeiculo = await _colaboradorVeiculoRepository.FindByIdAsync(id);

                if (colaboradorVeiculo is null) return null!;

                return _colaboradorVeiculoToVoParser.Parse(colaboradorVeiculo);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVeiculoVO> CreateAsync(ColaboradorVeiculoVO colaboradorVeiculoVO)
        {
            try
            {
                if (colaboradorVeiculoVO is null) return null!;

                var colaboradorVeiculo = _voToColaboradorVeiculoParser.Parse(colaboradorVeiculoVO);
                var resposta = await _colaboradorVeiculoRepository.CreateAsync(colaboradorVeiculo);

                if (resposta is null) return null!;

                return _colaboradorVeiculoToVoParser.Parse(colaboradorVeiculo);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVeiculoVO> UpdateAsync(ColaboradorVeiculoVO colaboradorVeiculoVO)
        {
            try
            {
                if (colaboradorVeiculoVO is null) return null!;

                var colaboradorVeiculo = _voToColaboradorVeiculoParser.Parse(colaboradorVeiculoVO);
                var resposta = await _colaboradorVeiculoRepository.UpdateAsync(colaboradorVeiculo);

                if (resposta is null) return null!;

                return _colaboradorVeiculoToVoParser.Parse(resposta);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _colaboradorVeiculoRepository.DeleteByIdAsync(id);
        }
    }
}
