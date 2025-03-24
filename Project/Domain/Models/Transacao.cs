namespace Project.Models
{
    sealed public class Transacao
    {
        public int Id { get; set; }
        public DateTime Emissao { get; private set; }
        public string Remetente { get; private set; }
        public string Destinatario { get; private set; }
        public double Valor { get; set; }

        public Transacao(string remetente, string destinatario, double valor)
        {
            Emissao = DateTime.UtcNow;
            Remetente = remetente;
            Destinatario = destinatario;
            Valor = valor;
        }
    }
}
