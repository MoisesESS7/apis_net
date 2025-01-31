using System.Text.Json.Serialization;

namespace GerenciamentoFrotaVeiculo.Api.Data.ValueObject.Base
{
    public class BaseVO
    {
        [JsonPropertyOrder(-1)]
        public int Id { get; set; }
    }
}
