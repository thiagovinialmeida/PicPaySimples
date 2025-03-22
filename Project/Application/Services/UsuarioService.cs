using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class UsuarioService : IUsuarioService
    {
        private PicpaySimplesContext _context;

        public async void CriarConta(string nome, string email, string senha, double saldo, string identidade)
        {
            if (identidade.Length == 11)
            {          
                    _context.Add(new UserComum(nome, email, senha, saldo, identidade));
                    await _context.SaveChangesAsync();
            }
        }
    }
}
