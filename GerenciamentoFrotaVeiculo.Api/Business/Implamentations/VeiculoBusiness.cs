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

                return _veiculoToVoParser.Parse(veiculos);
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

                return _veiculoToVoParser.Parse(veiculos);
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

                return _veiculoToVoParser.Parse(veiculo);
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

                return _veiculoToVoParser.Parse(veiculo);
            }
            catch (Exception)
            {
                return null!;
            }
        }


        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorAnoAsync(int ano)
        {
            try
            {
                if (ano < 1960) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculosPorAnoAsync(ano);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorModeloAsync(string modelo)
        {
            try
            {
                if (string.IsNullOrEmpty(modelo) || string.IsNullOrWhiteSpace(modelo)) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculosPorModeloAsync(modelo);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorPlacaAsync(string placa)
        {
            try
            {
                if (string.IsNullOrEmpty(placa) || string.IsNullOrWhiteSpace(placa)) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculoPorPlacaAsync(placa);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorCategoriaAsync(string categoria)
        {
            try
            {
                if (string.IsNullOrEmpty(categoria) || string.IsNullOrWhiteSpace(categoria)) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculosPorCategoriaAsync(categoria);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);

            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorLicenciamentoAsync(bool licenciamento)
        {
            try
            {
                var veiculos = await _veiculoRepository.BuscarVeiculosPorLicenciamentoAsync(licenciamento);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
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
                if (veiculoVO is null || veiculoVO.AnoFabricacao.Year < 1960 || veiculoVO.AnoModelo.Year < 1960) return null!;

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

        public async Task<VeiculoVO> UpdateAsync(VeiculoVO veiculoVO)
        {
            try
            {
                if (veiculoVO is null || veiculoVO.AnoFabricacao.Year < 1960 || veiculoVO.AnoModelo.Year < 1960) return null!;

                var veiculo = _voToVeiculoParser.Parse(veiculoVO);
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

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorMarcaAsync(string marca)
        {
            try
            {
                if (string.IsNullOrEmpty(marca) || string.IsNullOrWhiteSpace(marca)) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculosPorMarcaAsync(marca);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorQuilometragemAsync(int minimo, int maximo)
        {
            try
            {
                var veiculos = await _veiculoRepository.BuscarVeiculosPorQuilometragemAsync(minimo, maximo);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosPorCorAsync(string cor)
        {
            try
            {
                if (string.IsNullOrEmpty(cor) || string.IsNullOrWhiteSpace(cor)) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculosPorCorAsync(cor);

                if (veiculos is null) return null!;

                return _veiculoToVoParser.Parse(veiculos);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ICollection<VeiculoVO>> BuscarVeiculosAsync(string? placa, string? marca,
            int? quilometragemMinima, int? quilometragemMaxima,string? cor, int? ano, string? modelo,
            string? categoria, bool? licenciamento)
        {
            try
            {
                if (quilometragemMinima > quilometragemMaxima) return null!;

                var veiculos = await _veiculoRepository.BuscarVeiculosAsync(placa, marca, quilometragemMinima,
                    quilometragemMaxima, cor, ano, modelo, categoria, licenciamento);

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
