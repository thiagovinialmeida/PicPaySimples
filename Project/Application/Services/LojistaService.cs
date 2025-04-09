using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Services
{
    public class LojistaService : IUsuarioService
    {
        private readonly PicpaySimplesContext _context;
        public LojistaService (PicpaySimplesContext context) { _context = context; }

        public async Task CriarConta<T>(T user)
        {
            var lojista = user as Lojista;
            if (lojista.CNPJ.Length == 14)
            {
                await _context.Lojistas.AddAsync(lojista);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarConta(Guid id)
        {
            try
            {
                var usuario = await ProcurarUsusario(id);
                if (await UsuarioExiste(id))
                {
                    _context.Lojistas.Remove(usuario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task EditarConta<T>(Guid id, T lojista)
        {
            try
            {
                await DeletarConta(id);
                
                if (lojista != null)
                {
                    _context.Update(lojista);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException dbE)
            {
                Console.WriteLine(dbE.Message);
            }
        }
        public async Task<bool> VerificarExistencia<T>(T user)
        {
            var lojista = user as Lojista;
            
            if (EmailExiste(lojista.Email))
            {
                //Caso o email não exista, verificar CPF
                if (IdentidadeExiste(lojista.CNPJ))
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

        public async Task<List<Lojista>> TodasContas()
        {
            return await _context.Lojistas.ToListAsync();
        }
        public async Task<Lojista> MostrarConta(Guid id)
        {
            return await ProcurarUsusario(id);
        }

        ////

        private async Task<Lojista> ProcurarUsusario(Guid id)
        {
            return await _context.Lojistas.FindAsync(id);
        }
        private async Task<bool> UsuarioExiste(Guid id)
        {
            return await _context.Lojistas.AnyAsync(e => e.Id == id);
        }
        //Retorna TRUE se o CNPJ não existir
        private bool IdentidadeExiste(string identidade)
        {
            Task<Lojista?> usuario = _context.Lojistas.FirstOrDefaultAsync(cnpj => cnpj.CNPJ == identidade);
            return usuario == null;
        }
        //Retorna TRUE se o email não existir
        private bool EmailExiste(string email)
        {
            Task<Lojista?> usuario = _context.Lojistas.FirstOrDefaultAsync(e => e.Email == email);
            return usuario == null;
        }
    }
}
