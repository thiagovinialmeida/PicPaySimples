namespace Project.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public double Saldo { get; private set; }

        public Usuario(string nome, string email, string senha, double saldo)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senha;
            Saldo = saldo;
        }
    }
}
