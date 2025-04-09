using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly PicpaySimplesContext _context;

        public UsuarioService(PicpaySimplesContext context) { _context = context; }

        public async Task CriarConta<T>(T user)
        {
            try
            {
                await _context.UserComum.AddAsync(user as UserComum);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeletarConta(Guid id)
        {
            try
            {
                var usuario = await ProcurarUsusario(id);
                if (UsuarioExiste(id))
                {
                    _context.UserComum.Remove(usuario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task EditarConta<T>(Guid id, T usuarioAtualizado)
        {
            try
            {
                await DeletarConta(id);
                
                await _context.AddAsync(usuarioAtualizado as UserComum);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbE)
            {
                Console.WriteLine(dbE.Message);
            }
        }

        public async Task<List<UserComum?>> TodasContas()
        {
            return await _context.UserComum.ToListAsync();
        }
        public async Task<UserComum?> MostrarConta(Guid id)
        {
            return await ProcurarUsusario(id);
        }
        ////
        private async Task<UserComum?> ProcurarUsusario(Guid id)
        {
            return await _context.UserComum.FindAsync(id);
        }
        public async Task<bool> VerificarExistencia<T>(T user)
        {
            var usuario = user as UserComum;
            if (EmailExiste(usuario.Email))
            {
                //Caso o email não exista, verificar CPF
                if (IdentidadeExiste(usuario.Cpf))
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
            UserComum? usuario = _context.UserComum?.FirstOrDefault(cpf => cpf.Cpf == identidade);
            return usuario == null;
        }
        //Retorna TRUE se o email não existir
        private bool EmailExiste(string email)
        {
            UserComum? usuario = _context.UserComum?.FirstOrDefault(e => e.Email == email);
            return usuario == null;
        }
    }
}
