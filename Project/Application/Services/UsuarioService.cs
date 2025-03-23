using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly PicpaySimplesContext _context;

        public UsuarioService(PicpaySimplesContext context) { _context = context; }

        public void CriarConta(string nome, string email, string senha, double saldo, string identidade)
        {
            try
            {
                if (identidade.Length == 11)
                {
                    _context.UserComum.Add(new UserComum(nome, email, senha, saldo, identidade));
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeletarConta(Guid id)
        {
            try
            {
                var usuario = ProcurarUsusario(id);
                if(UsuarioExiste(id))
                {
                    _context.UserComum.Remove(usuario);
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditarConta(Guid id)
        {
            try
            {
                var usuario = ProcurarUsusario(id);
                
                if(usuario != null)
                { 
                _context.Update(usuario);
                _context.SaveChanges();
                }
            }
            catch(DbUpdateConcurrencyException dbE)
            {
                Console.WriteLine(dbE.Message);
            }
        }

        public List<UserComum> TodasContas()
        {
            return _context.UserComum.ToList();
        }
        public UserComum MostrarConta(Guid id)
        {
            return ProcurarUsusario(id);
        }
        ////
        public UserComum ProcurarUsusario(Guid id)
        {
            return _context.UserComum.Find(id);
        }
        private bool UsuarioExiste(Guid id)
        {
            return _context.Lojistas.Any(e => e.Id == id);
        }
    }
}
