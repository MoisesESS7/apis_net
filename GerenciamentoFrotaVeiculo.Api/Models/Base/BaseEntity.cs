using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFrotaVeiculo.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
