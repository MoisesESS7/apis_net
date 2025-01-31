using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Data.Implementation
{
    public class ColaboradorConverter<T> : IParser<Colaborador, ColaboradorVO>, IParser<ColaboradorVO, Colaborador>
    {
        public ColaboradorVO Parse(Colaborador origin)
        {
            try
            {

                if (origin is null) return null!;

                var result = 
                    new ColaboradorVO
                    {
                        Id = origin.Id,
                        Nome = origin.Nome,
                        Idade = origin.Idade,
                        Cpf = origin.Cpf,
                        CarteiraHabilitacao = origin.CarteiraHabilitacao,
                        Endereco = origin.Endereco,
                        EstadoCivil = origin.EstadoCivil,
                        Dependente = origin.Dependente,
                        DataNascimento = origin.DataNascimento
                    };

                result.Veiculos = new List<VeiculoVO>();
                result.ColaboradoresVeiculos = new List<ColaboradorVeiculoVO>();

                foreach (var veiculo in origin!.Veiculos!)
                {
                    result.Veiculos!.Add(new VeiculoVO
                    {
                        Id = veiculo.Id,
                        Marca = veiculo.Marca,
                        Modelo = veiculo.Modelo,
                        AnoModelo = veiculo.AnoModelo,
                        Placa = veiculo.Placa,
                        DataLicenciamento = veiculo.DataLicenciamento,
                        AnoFabricacao = veiculo.AnoFabricacao,
                        Categoria = veiculo.Categoria,
                        Cor = veiculo.Cor,
                        LicenciamentoVigente = veiculo.LicenciamentoVigente,
                        Quilometragem = veiculo.Quilometragem,
                        Colaboradores = null!,
                        ColaboradoresVeiculos = null!,
                    });
                }

                foreach (var colaboradorVeiculo in origin!.ColaboradoresVeiculos!)
                {
                    result.ColaboradoresVeiculos!.Add(new ColaboradorVeiculoVO
                    {
                        Id = colaboradorVeiculo.Id,
                        VeiculoId = colaboradorVeiculo.VeiculoId,
                        ColaboradorId = colaboradorVeiculo.ColaboradorId,
                        DataInicioVinculo = colaboradorVeiculo.DataInicioVinculo,
                        ColaboradorNomeCompleto = colaboradorVeiculo.ColaboradorNomeCompleto,
                        VeiculoModelo = colaboradorVeiculo.VeiculoModelo
                    });
                }

                return result;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public Colaborador Parse(ColaboradorVO origin)
        {
            try
            {
                if (origin is null) return null!;

                var result =
                    new Colaborador
                    (
                        origin.Nome,
                        origin.Cpf,
                        origin.CarteiraHabilitacao,
                        origin.Endereco,
                        origin.EstadoCivil,
                        origin.Dependente,
                        origin.DataNascimento
                    );

                result.Id = origin!.Id;
                result.Veiculos = new List<Veiculo>();
                result.ColaboradoresVeiculos = new List<ColaboradorVeiculo>();

                foreach (var veiculo in origin!.Veiculos!)
                {
                    result.Veiculos!.Add(new Veiculo
                    {
                        Id = veiculo.Id,
                        Marca = veiculo.Marca,
                        Modelo = veiculo.Modelo,
                        AnoModelo = veiculo.AnoModelo,
                        Placa = veiculo.Placa,
                        DataLicenciamento = veiculo.DataLicenciamento,
                        AnoFabricacao = veiculo.AnoFabricacao,
                        Categoria = veiculo.Categoria,
                        Cor = veiculo.Cor,
                        LicenciamentoVigente = veiculo.LicenciamentoVigente,
                        Quilometragem = veiculo.Quilometragem,
                        Colaboradores = null!,
                        ColaboradoresVeiculos = null!,
                    });
                }

                foreach (var colaboradorVeiculo in origin!.ColaboradoresVeiculos!)
                {
                    result.ColaboradoresVeiculos!.Add(new ColaboradorVeiculo
                    {
                        VeiculoId = colaboradorVeiculo.VeiculoId,
                        ColaboradorId = colaboradorVeiculo.ColaboradorId,
                        DataInicioVinculo = colaboradorVeiculo.DataInicioVinculo,
                        ColaboradorNomeCompleto = colaboradorVeiculo.ColaboradorNomeCompleto,
                        VeiculoModelo = colaboradorVeiculo.VeiculoModelo
                    });
                }

                return result;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public ICollection<ColaboradorVO> Parse(ICollection<Colaborador> origin)
        {
            try
            {
                var result = (origin is null) ? null! :
                 origin.Select(item => Parse(item)).ToList();

                return result;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public ICollection<Colaborador> Parse(ICollection<ColaboradorVO> origin)
        {
            try
            {
                var result = (origin is null) ? null! :
                 origin.Select(item => Parse(item)).ToList();

                return result;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
