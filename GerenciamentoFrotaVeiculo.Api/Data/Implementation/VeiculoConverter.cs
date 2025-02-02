using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Data.Implementation
{
    public class VeiculoConverter<T> : IParser<Veiculo, VeiculoVO>, IParser<VeiculoVO, Veiculo>
    {
        public VeiculoVO Parse(Veiculo origin)
        {
            var result = origin is null ? null! :
                new VeiculoVO
                {
                    Id = origin.Id,
                    Marca = origin.Marca,
                    Modelo = origin.Modelo,
                    AnoModelo = origin.AnoModelo,
                    Placa = origin.Placa,
                    DataLicenciamento = origin.DataLicenciamento,
                    AnoFabricacao = origin.AnoFabricacao,
                    Categoria = origin.Categoria,
                    Cor = origin.Cor,
                    LicenciamentoVigente = origin.LicenciamentoVigente,
                    Quilometragem = origin.Quilometragem,
                    Colaboradores = new List<ColaboradorVO>(),
                    ColaboradoresVeiculos = new List<ColaboradorVeiculoVO>()!,
                };

            foreach (var colaborador in origin!.Colaboradores!)
            {
                result.Colaboradores!.Add(new ColaboradorVO
                {
                    Id = colaborador.Id,
                    Nome = colaborador.Nome,
                    Idade = colaborador.Idade,
                    Cpf = colaborador.Cpf,
                    CarteiraHabilitacao = colaborador.CarteiraHabilitacao,
                    Endereco = colaborador.Endereco,
                    EstadoCivil = colaborador.EstadoCivil,
                    Dependente = colaborador.Dependente,
                    DataNascimento = colaborador.DataNascimento
                });
            }

            foreach (var colaboradorVeiculo in origin.ColaboradoresVeiculos!)
            {
                result.ColaboradoresVeiculos!.Add(new ColaboradorVeiculoVO
                {
                    Id = colaboradorVeiculo.Id,
                    ColaboradorId = colaboradorVeiculo.ColaboradorId,
                    VeiculoId = colaboradorVeiculo.VeiculoId,
                    ColaboradorNomeCompleto = colaboradorVeiculo.ColaboradorNomeCompleto,
                    VeiculoModelo = colaboradorVeiculo.VeiculoModelo,
                    DataInicioVinculo = colaboradorVeiculo.DataInicioVinculo,
                    Colaborador = null,
                    Veiculo = null
                });
            }

            return result;
        }

        public Veiculo Parse(VeiculoVO origin)
        {
            var result = origin is null ? null! :
                new Veiculo
                {
                    Id = origin.Id,
                    Marca = origin.Marca,
                    Modelo = origin.Modelo,
                    AnoModelo = origin.AnoModelo,
                    Placa = origin.Placa,
                    DataLicenciamento = origin.DataLicenciamento,
                    AnoFabricacao = origin.AnoFabricacao,
                    Categoria = origin.Categoria,
                    Cor = origin.Cor,
                    LicenciamentoVigente = origin.LicenciamentoVigente,
                    Quilometragem = origin.Quilometragem,
                    Colaboradores = new List<Colaborador>(),
                    ColaboradoresVeiculos = new List<ColaboradorVeiculo>()!,
                };

            foreach (var colaborador in result.Colaboradores!)
            {
                result.Colaboradores.Add(new Colaborador
                (
                    colaborador.Nome,
                    colaborador.Cpf,
                    colaborador.CarteiraHabilitacao,
                    colaborador.Endereco,
                    colaborador.EstadoCivil,
                    colaborador.Dependente,
                    colaborador.DataNascimento
                ));
            }

            foreach (var colaboradorVeiculo in result.ColaboradoresVeiculos!)
            {
                result.ColaboradoresVeiculos.Add(new ColaboradorVeiculo
                {
                    Id = colaboradorVeiculo.Id,
                    ColaboradorId = colaboradorVeiculo.ColaboradorId,
                    VeiculoId = colaboradorVeiculo.VeiculoId,
                    ColaboradorNomeCompleto = colaboradorVeiculo.ColaboradorNomeCompleto,
                    VeiculoModelo = colaboradorVeiculo.VeiculoModelo,
                    DataInicioVinculo = colaboradorVeiculo.DataInicioVinculo,
                    Colaborador = null,
                    Veiculo = null
                });
            }

            return result;
        }

        public ICollection<Veiculo> Parse(ICollection<VeiculoVO> origin)
        {
            var result = origin is null ? null! : new List<Veiculo>();

            foreach (var item in origin!)
            {
                result.Add(Parse(item));
            }

            return result;
        }

        public ICollection<VeiculoVO> Parse(ICollection<Veiculo> origin)
        {
            var result = origin is null ? null! : new List<VeiculoVO>();

            foreach (var item in origin!)
            {
                result.Add(Parse(item));
            }

            return result;
        }
    }
}
