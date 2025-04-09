using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class TransacaoService
    {
        private readonly PicpaySimplesContext _context;
        private readonly UsuarioService _us;
        public TransacaoService(PicpaySimplesContext context, UsuarioService us) { _context = context; _us = us; }

        public async Task FazerTransferencia(double valor,Guid idRemetente, Guid idDestinatario)
        {
            UserComum? remetente = await _context.UserComum.FindAsync(idRemetente);
            var destinatarioUsuario = await _context.UserComum.FirstOrDefaultAsync(id => id.Id == idDestinatario);

            if (destinatarioUsuario == null)
            {
                Lojista? destinatarioLojista = await _context.Lojistas.FirstOrDefaultAsync(id => id.Id == idDestinatario);

                remetente?.Retirar(valor);
                destinatarioLojista?.Depositar(valor);

                remetente?.TransacaoFeita(new Transacao(remetente.Nome, destinatarioLojista?.Nome, valor));

                await _us.EditarConta(idRemetente, remetente);
                await _context.SaveChangesAsync();
            }
            else
            {
                remetente?.Retirar(valor);
                destinatarioUsuario.Depositar(valor);

                remetente?.TransacaoFeita(new Transacao(remetente.Nome, destinatarioUsuario.Nome, valor));
                
                await _us.EditarConta(idRemetente, remetente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
