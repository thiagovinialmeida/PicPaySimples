using Microsoft.EntityFrameworkCore;
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
            var destinatarioUsuario = await _context.UserComum.FirstOrDefaultAsync(id => id.Id == idDestinatario);

            if (destinatarioUsuario == null)
            {
                Usuario destinatarioLojista = destinatarioUsuario;
                destinatarioLojista = await _context.Lojistas.FirstOrDefaultAsync(id => id.Id == idDestinatario);

                remetente.Retirar(valor);
                destinatarioLojista.Depositar(valor);

                remetente.TransacaoFeita(new Transacao(remetente.Nome, destinatarioLojista.Nome, valor));
            }
            else
            {
                remetente.Retirar(valor);
                destinatarioUsuario.Depositar(valor);

                remetente.TransacaoFeita(new Transacao(remetente.Nome, destinatarioUsuario.Nome, valor));
                _context.SaveChanges();
            }
        }
    }
}
