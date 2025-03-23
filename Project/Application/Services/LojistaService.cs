using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class LojistaService : IUsuarioService
    {
        private readonly PicpaySimplesContext _context;
        public void CriarConta(string nome, string email, string senha, double saldo, string identidade)
        {
            if (identidade.Length == 14)
            {
                _context.Lojistas.Add(new Lojista(nome, email, senha, saldo, identidade));
                _context.SaveChanges();
            }
        }

        public void DeletarConta(Guid Id)
        {

        }

        public void EditarConta(Guid id)
        {

        }
    }
}
