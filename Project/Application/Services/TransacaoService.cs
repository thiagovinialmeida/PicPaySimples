using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class TransacaoService
    {
        private readonly PicpaySimplesContext _context;
        public TransacaoService(PicpaySimplesContext context) { _context = context; }

        public async Task FazerTransferencia(double valor,Guid idRemetente, Guid idDestinatario)
        {
            UserComum remetente = await _context.UserComum.FindAsync(idRemetente);
            UserComum destinatario = await _context.UserComum.FindAsync(idDestinatario);

            remetente.Retirar(valor);
            destinatario.Depositar(valor);

            remetente.TransacaoFeita(new Transacao(remetente.Nome, destinatario.Nome, valor));
            _context.SaveChanges();
        }
    }
}
