using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Repository;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Business.Implamentations
{
    public class VeiculoBusiness : IVeiculoBusiness
    {
        private readonly VeiculoRepository _veiculoRepository;
        private readonly IParser<Veiculo, VeiculoVO> _veiculoToVoParser;
        private readonly IParser<VeiculoVO, Veiculo> _voToVeiculoParser;

        public VeiculoBusiness(VeiculoRepository veiculoRepository,
            IParser<Veiculo, VeiculoVO> veiculoToVoParser,
            IParser<VeiculoVO, Veiculo> voToVeiculoParser)
        {
            _veiculoRepository = veiculoRepository ?? throw new ArgumentNullException(nameof(veiculoRepository));
            _veiculoToVoParser = veiculoToVoParser ?? throw new ArgumentNullException(nameof(veiculoToVoParser));
            _voToVeiculoParser = voToVeiculoParser ?? throw new ArgumentNullException(nameof(voToVeiculoParser));
        }

        public async Task<ICollection<VeiculoVO>> FindAllAsync()
        {
            try
            {
                var veiculos = await _veiculoRepository.FindAllAsync();

                if (veiculos is null) return null!;

                var vo = _veiculoToVoParser.Parse(veiculos);

                return vo;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> FindAllIncludeColaboradoresAsync()
        {
            try
            {
                var veiculos = await _veiculoRepository.FindAllIncludeColaboradoresAsync();

                if (veiculos is null) return null!;

                var vo = _veiculoToVoParser.Parse(veiculos);

                return vo;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<VeiculoVO> FindByIdAsync(int id)
        {
            try
            {
                var veiculo = await _veiculoRepository.FindByIdAsync(id);

                if (veiculo is null) return null!;

                var vo = _veiculoToVoParser.Parse(veiculo);

                return vo;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<VeiculoVO> FindByIdIncludeColaboradoresAsync(int id)
        {
            try
            {
                var veiculo = await _veiculoRepository.FindByIdIncludeColaboradoresAsync(id);

                if (veiculo is null) return null!;

                var vo = _veiculoToVoParser.Parse(veiculo);

                return vo;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<VeiculoVO> CreateAsync(VeiculoVO veiculoVO)
        {
            try
            {
                if (veiculoVO is null) return null!;

                var veiculo = _voToVeiculoParser.Parse(veiculoVO);
                var resposta = await _veiculoRepository.CreateAsync(veiculo);

                if (resposta is null) return null!;

                return _veiculoToVoParser.Parse(resposta);

            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<VeiculoVO> UpdateAsync(VeiculoVO veiculoDbVO)
        {
            try
            {
                if (veiculoDbVO is null) return null!;

                var veiculo = _voToVeiculoParser.Parse(veiculoDbVO);
                var resposta = await _veiculoRepository.UpdateAsync(veiculo);

                if (resposta is null) return null!;

                return _veiculoToVoParser.Parse(resposta);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var resposta = await _veiculoRepository.DeleteByIdAsync(id);
                return resposta;
            }
            catch (Exception)
            {
                return false!;
            }
        }
    }
}
