namespace Project.Models
{
    sealed public class UserComum : Usuario
    {
        public string Cpf { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        public UserComum(string nome, string email, string senha, double saldo, string cpf) : base(nome, email, senha, saldo)
        {
            Cpf = cpf;
        }
    }
}
