using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Data;
using Project.Services;

namespace Project.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly PicpaySimplesContext _context;
        private readonly IUsuarioService _us;

        public UsuariosController(PicpaySimplesContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Lojistas.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _context.Lojistas.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost("{nome}/{email}/{senha}/{saldo}/{cpfCnpj}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Usuario>> PostUsuario(string nome, string email, string senha, double saldo, string identidade)
        {
            if (identidade.Length == 11)
            {
                new UsuarioService().CriarConta(nome, email, senha, saldo, identidade);
                Ok();
            }
            else if (identidade.Length == 14)
            {
                new LojistaService().CriarConta(nome, email, senha, saldo, identidade); 
                Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Lojistas.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Lojistas.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Lojistas.Any(e => e.Id == id);
        }
    }
}
