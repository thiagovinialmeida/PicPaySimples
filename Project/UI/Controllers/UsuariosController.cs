using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _us;

        public UsuariosController(UsuarioService us) { _us = us; }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario?>>> GetUsuario()
        {
            return await _us.TodasContas();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _us.MostrarConta(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(Guid id, UserComum usuario)
        {
            
            await _us.EditarConta(id, usuario);
            return Ok("Usuario editado");
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UserComum usuario)
        {
            try
            {
                switch (usuario.Cpf.Length)
                {
                    case 11:
                        {
                            if (await _us.VerificarExistencia(usuario))
                            {
                                await _us.CriarConta(usuario);
                                Console.WriteLine("Isso é um CPF");
                                return Ok("Usuario criado");
                            }
                            else
                            {
                                return BadRequest("Esse CPF ou email já foi registrado anteriormente");
                            }
                        }
                    case 14:
                        {
                             Console.WriteLine("Isso é um CNPJ");
                             return BadRequest();
                        }
                    default:
                        {
                            return NotFound();
                        }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(Guid id)
        {
            await _us.DeletarConta(id);
            return Ok("Usuario deletado");
        } 
    }
}
