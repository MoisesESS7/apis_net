namespace GerenciamentoFrotaVeiculo.Api.Data.Contract
{
    public interface IParser<Origin, Fate>
    {
        Fate Parse(Origin origin);
        ICollection<Fate> Parse(ICollection<Origin> origin);
    }
}
