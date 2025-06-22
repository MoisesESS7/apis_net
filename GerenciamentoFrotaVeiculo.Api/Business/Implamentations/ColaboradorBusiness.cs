using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Business.Implamentations
{
    public class ColaboradorBusiness : IColaboradorBusiness
    {
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IParser<Colaborador, ColaboradorVO> _colaboradorToVoParser;
        private readonly IParser<ColaboradorVO, Colaborador> _voToColaboradorParser;
        private readonly IParser<Veiculo, VeiculoVO> _veiculoToVoParser;

        public ColaboradorBusiness(IColaboradorRepository colaboradorRepository,
            IParser<Colaborador, ColaboradorVO> colaboradorToVoParser,
            IParser<ColaboradorVO, Colaborador> voToColaboradorParser,
            IParser<Veiculo, VeiculoVO> veiculoToVoParser)
        {
            _colaboradorRepository = colaboradorRepository ?? throw new ArgumentNullException(nameof(colaboradorRepository));
            _colaboradorToVoParser = colaboradorToVoParser ?? throw new ArgumentNullException(nameof(colaboradorToVoParser));
            _voToColaboradorParser = voToColaboradorParser ?? throw new ArgumentNullException(nameof(voToColaboradorParser));
            _veiculoToVoParser = veiculoToVoParser ?? throw new ArgumentNullException(nameof(veiculoToVoParser));
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

        public async Task<ColaboradorVO> FindByCpfAsync(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf) || string.IsNullOrWhiteSpace(cpf)) return null!;

                var colaborador = await _colaboradorRepository.FindByCpfAsync(cpf);

                if (colaborador is null) return null!;

                return _colaboradorToVoParser.Parse(colaborador);
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
                var colaborador = await _colaboradorRepository.BuscaCompletaAsync(id);

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

        public async Task<ICollection<VeiculoVO>> FindAllVeiculosByIdColaboradorAsync(int id)
        {
            try
            {
                var veiculos = await _colaboradorRepository.FindAllVeiculosByIdColaboradorAsync(id);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
