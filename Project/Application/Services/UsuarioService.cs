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
                _context.UserComum.Add(new UserComum(nome, email, senha, saldo, identidade));
                _context.SaveChanges();
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
                if (UsuarioExiste(id))
                {
                    _context.UserComum.Remove(usuario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditarConta(Guid id)
        {
            try
            {
                var usuario = ProcurarUsusario(id);

                if (usuario != null)
                {
                    _context.Update(usuario);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException dbE)
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
        private UserComum ProcurarUsusario(Guid id)
        {
            return _context.UserComum.Find(id);
        }
        public bool VerificarExistencia(string email,string identidade)
        {
            if (EmailExiste(email))
            {
                //Caso o email não exista, verificar CPF
                if (IdentidadeExiste(identidade))
                {
                    //Caso o CPF não exista, pode criar novo usuario
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private bool UsuarioExiste(Guid id)
        {
            return _context.UserComum.Any(e => e.Id == id);
        }
        //Retorna TRUE se o cpf não existir
        private bool IdentidadeExiste(string identidade)
        {
            UserComum usuario = _context.UserComum.FirstOrDefault(cpf => cpf.Cpf == identidade);
            return usuario == null;
        }
        //Retorna TRUE se o email não existir
        private bool EmailExiste(string email)
        {
            UserComum usuario = _context.UserComum.FirstOrDefault(e => e.Email == email);
            return usuario == null;
        }
    }
}
