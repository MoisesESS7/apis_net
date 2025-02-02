using GerenciamentoFrotaVeiculo.Models.Base;

namespace GerenciamentoFrotaVeiculo.Models
{
    public class Colaborador : BaseEntity
    {
        private string _nome = string.Empty;
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new InvalidOperationException("Nome inválido.");
                }
                _nome = value;
            }
        }
        private int _idade;
        public int Idade
        {
            get { return _idade; }
            private set
            {
                if (value > (DateTime.Now.Year - 60)
                    && value <= (DateTime.Now.Year - 18))
                {
                    int idade = DateTime.Now.Year - value;

                    _idade =
                        (DateTime.Now.Date < DataNascimento.AddYears(idade)) ? --idade : idade;
                }
                else
                {
                    //throw new InvalidOperationException("Idade inválida.");
                }
            }
        }
        public string Cpf { get; set; } = null!;
        public string? CarteiraHabilitacao { get; set; }
        public string? Endereco { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Dependente { get; set; }
        private DateTime _dataNascimento;
        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            private set
            {
                if (value.Year > (DateTime.Now.Year - 60)
                    && value.Year <= (DateTime.Now.Year - 18))
                {
                    _dataNascimento = value.Date;
                }
            }
        }
        public ICollection<ColaboradorVeiculo>? ColaboradoresVeiculos { get; set; } = new List<ColaboradorVeiculo>();
        public ICollection<Veiculo>? Veiculos { get; set; } = new List<Veiculo>();

        public Colaborador(string nome, string cpf, string? carteiraHabilitacao,
            string? endereco, string? estadoCivil, string? dependente, DateTime dataNascimento)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            CarteiraHabilitacao = carteiraHabilitacao;
            Endereco = endereco;
            EstadoCivil = estadoCivil;
            Dependente = dependente;
            DataNascimento = dataNascimento;
            Idade = dataNascimento.Year;
        }

        public void AddVeiculo(Veiculo veiculo)
        {
            Veiculos!.Add(veiculo);
        }
        
        public void RemoverVeiculo(Veiculo veiculo)
        {
            Veiculos!.Remove(veiculo);
        }
    }
}
