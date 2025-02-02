using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Repository;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Business.Implamentations
{
    public class ColaboradorBusiness : IColaboradorBusiness
    {
        private readonly ColaboradorRepository _colaboradorRepository;
        private readonly IParser<Colaborador, ColaboradorVO> _colaboradorToVoParser;
        private readonly IParser<ColaboradorVO, Colaborador> _voToColaboradorParser;

        public ColaboradorBusiness(ColaboradorRepository colaboradorRepository,
            IParser<Colaborador, ColaboradorVO> colaboradorToVoParser,
            IParser<ColaboradorVO, Colaborador> voToColaboradorParser)
        {
            _colaboradorRepository = colaboradorRepository ?? throw new ArgumentNullException(nameof(colaboradorRepository));
            _colaboradorToVoParser = colaboradorToVoParser ?? throw new ArgumentNullException(nameof(colaboradorToVoParser));
            _voToColaboradorParser = voToColaboradorParser ?? throw new ArgumentNullException(nameof(voToColaboradorParser));
        }

        public async Task<ICollection<ColaboradorVO>> FindByNameAsync(string nome)
        {
            try
            {
                if (string.IsNullOrEmpty(nome) || string.IsNullOrWhiteSpace(nome)) return null!;

                var colaboradores = await _colaboradorRepository.FindByNameAsync(nome);

                if (colaboradores is null) return null!;

                return _colaboradorToVoParser.Parse(colaboradores);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<ColaboradorVO>> FindAllAsync()
        {
            try
            {
                var colaboradores = await _colaboradorRepository.FindAllAsync();

                if (colaboradores is null) return null!;

                return _colaboradorToVoParser.Parse(colaboradores);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<ColaboradorVO>> FindAllIncludeVeiculosAsync()
        {
            try
            {
                var colaboradores = await _colaboradorRepository.FindAllIncludeVeiculosAsync();

                if (colaboradores is null) return null!;

                return _colaboradorToVoParser.Parse(colaboradores);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVO> FindByIdAsync(int id)
        {
            try
            {
                var colaborador = await _colaboradorRepository.FindByIdAsync(id);

                if (colaborador is null) return null!;

                return _colaboradorToVoParser.Parse(colaborador);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVO> FindByIdIncludeVeiculosAsync(int id)
        {
            try
            {
                var colaborador = await _colaboradorRepository.FindByIdIncludeVeiculosAsync(id);

                if (colaborador is null) return null!;

                return _colaboradorToVoParser.Parse(colaborador);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVO> CreateAsync(ColaboradorVO colaboradorVO)
        {
            try
            {
                if (colaboradorVO is null) return null!;

                var colaborador = _voToColaboradorParser.Parse(colaboradorVO);
                var resposta = await _colaboradorRepository.CreateAsync(colaborador);

                if (resposta is null) return null!;

                return _colaboradorToVoParser.Parse(resposta);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ColaboradorVO> UpdateAsync(ColaboradorVO colaboradorVO)
        {
            try
            {
                if (colaboradorVO is null) return null!;

                var colaborador = _voToColaboradorParser.Parse(colaboradorVO);
                var resposta = await _colaboradorRepository.UpdateAsync(colaborador);

                if (resposta is null) return null!;

                return _colaboradorToVoParser.Parse(resposta);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var resposta = await _colaboradorRepository.DeleteByIdAsync(id);

            return resposta;
        }
    }
}
