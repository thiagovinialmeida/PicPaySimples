namespace Project.Models
{
    sealed public class Lojista : Usuario
    {
        public string CNPJ { get; private set; }

        public Lojista(string nome, string email, string senha, double saldo, string cNPJ) : base(nome, email, senha, saldo)
        {
            CNPJ = cNPJ;
        }
    }
}
