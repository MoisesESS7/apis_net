using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Api.Data.Implementation
{
    public class ColaboradorVeiculoConverter<T> : IParser<ColaboradorVeiculo, ColaboradorVeiculoVO>, IParser<ColaboradorVeiculoVO, ColaboradorVeiculo>
    {
        public ColaboradorVeiculo Parse(ColaboradorVeiculoVO origin)
        {
            var result = (origin is null) ? null! :
                new ColaboradorVeiculo
                {
                    Id = origin.Id,
                    ColaboradorId = origin.ColaboradorId,
                    VeiculoId = origin.VeiculoId,
                    ColaboradorNomeCompleto = origin.ColaboradorNomeCompleto,
                    VeiculoModelo = origin.VeiculoModelo,
                    DataInicioVinculo = origin.DataInicioVinculo,
                    Colaborador = null,
                    //Colaborador = new Colaborador
                    //{
                    //    Id = origin.ColaboradorId,
                    //    Nome = origin.Colaborador!.Nome,
                    //    Dependente = origin.Colaborador!.Dependente,
                    //    Cpf = origin.Colaborador!.Cpf,
                    //    CarteiraHabilitacao = origin.Colaborador!.CarteiraHabilitacao,
                    //    Endereco = origin.Colaborador!.Endereco,
                    //    EstadoCivil = origin.Colaborador!.EstadoCivil,
                    //    Veiculos = new List<Veiculo>(),
                    //    ColaboradoresVeiculos = new List<ColaboradorVeiculo>()
                    //},
                    Veiculo = null,
                    //Veiculo = new Veiculo
                    //{
                    //    Id = origin.Veiculo!.Id,
                    //    Marca = origin.Veiculo.Marca,
                    //    Modelo = origin.Veiculo.Modelo,
                    //    Ano = origin.Veiculo.Ano,
                    //    Placa = origin.Veiculo.Placa,
                    //    Colaboradores = new List<Colaborador>(),
                    //    ColaboradoresVeiculos = new List<ColaboradorVeiculo>(),
                    //}
                };

            //foreach (var item in origin!.Colaborador!.Veiculos!)
            //{
            //    result!.Colaborador!.Veiculos!.Add(new Veiculo
            //    {
            //        Id = item.Id,
            //        Marca = item.Marca,
            //        Modelo = item.Modelo,
            //        Ano = item.Ano,
            //        Colaboradores = new List<Colaborador>(),
            //        ColaboradoresVeiculos = new List<ColaboradorVeiculo>(),
            //    });
            //}

            //foreach(var item in origin.Colaborador.ColaboradoresVeiculos!)
            //{
            //    colaboradorVeiculo.Colaborador.ColaboradoresVeiculos.Add(new ColaboradorVeiculo
            //    {

            //    });
            //}

            return result;
        }

        public ColaboradorVeiculoVO Parse(ColaboradorVeiculo origin)
        {
            var result = (origin is null) ? null! :
                new ColaboradorVeiculoVO
                {
                    Id = origin.Id,
                    ColaboradorId = origin.ColaboradorId,
                    VeiculoId = origin.VeiculoId,
                    ColaboradorNomeCompleto = origin.ColaboradorNomeCompleto,
                    VeiculoModelo = origin.VeiculoModelo,
                    DataInicioVinculo = origin.DataInicioVinculo,
                    Colaborador = null!,
                    //Colaborador = new ColaboradorVO
                    //{
                    //    Id = origin.ColaboradorId,
                    //    Nome = origin.Colaborador!.Nome,
                    //    Veiculos = new List<VeiculoVO>(),
                    //    ColaboradoresVeiculos = new List<ColaboradorVeiculoVO>()
                    //},
                    Veiculo = null!
                    //Veiculo = new VeiculoVO
                    //{
                    //    Id = origin.Veiculo!.Id,
                    //    Marca = origin.Veiculo.Marca,
                    //    Modelo = origin.Veiculo.Modelo,
                    //    Ano = origin.Veiculo.Ano,
                    //    Placa = origin.Veiculo.Placa,
                    //    Colaboradores = new List<ColaboradorVO>(),
                    //    ColaboradoresVeiculos = new List<ColaboradorVeiculoVO>(),
                    //}
                };

            //foreach (var item in origin!.Colaborador!.Veiculos!)
            //{
            //    result!.Colaborador!.Veiculos!.Add(new VeiculoVO
            //    {
            //        Id = item.Id,
            //        Marca = item.Marca,
            //        Modelo = item.Modelo,
            //        Ano = item.Ano,
            //        Colaboradores = new List<ColaboradorVO>(),
            //        ColaboradoresVeiculos = new List<ColaboradorVeiculoVO>()
            //    });
            //}

            return result;
        }

        public ICollection<ColaboradorVeiculo> Parse(ICollection<ColaboradorVeiculoVO> origin)
        {
            var result = (origin is null) ? null :
                new List<ColaboradorVeiculo>();

            foreach (var item in origin!)
            {
                result!.Add(Parse(item));
            }

            return result!;
        }

        public ICollection<ColaboradorVeiculoVO> Parse(ICollection<ColaboradorVeiculo> origin)
        {
            var result = (origin is null) ? null :
                new List<ColaboradorVeiculoVO>();

            foreach (var item in origin!)
            {
                result!.Add(Parse(item));
            }

            return result!;
        }
    }
}
