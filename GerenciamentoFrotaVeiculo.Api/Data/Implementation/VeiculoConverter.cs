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
                    Colaboradores = null!,
                    ColaboradoresVeiculos = null!,
                };

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
                    Colaboradores = null!,
                    ColaboradoresVeiculos = null!,
                };
            return result;
        }

        public ICollection<Veiculo> Parse(ICollection<VeiculoVO> origin)
        {
            var result = origin is null ? null! : new List<Veiculo>();

            foreach(var item in origin!)
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
