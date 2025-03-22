using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class LojistaService : IUsuarioService
    {
        private readonly PicpaySimplesContext _context;
        public async void CriarConta(string nome, string email, string senha, double saldo, string identidade)
        {
            if (identidade.Length == 14)
            {
                _context.Add(new UserComum(nome, email, senha, saldo, identidade));
                await _context.SaveChangesAsync();
            }
        }
    }
}
