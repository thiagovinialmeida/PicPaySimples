using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Data;
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
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return _us.TodasContas();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = _us.MostrarConta(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(Guid id)
        {
            _us.EditarConta(id);
            return Ok("Usuario editado");
        }

        // POST: api/Usuarios
        [HttpPost("{nome}/{email}/{senha}/{saldo}/{identidade}")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Usuario>> PostUsuario(string nome, string email, string senha, double saldo, string identidade)
        {
            try
            {
                switch (identidade.Length)
                {
                    case 11:
                        {
                            if (_us.VerificarExistencia(email, identidade))
                            {
                                _us.CriarConta(nome, email, senha, saldo, identidade);
                                Console.WriteLine("Isso é um CPF");
                                return Ok();
                            }
                            else
                            {
                            return BadRequest();
                            }
                        }
                    case 14:
                        {
                            new LojistaService().CriarConta(nome, email, senha, saldo, identidade);
                            Console.WriteLine("Isso é um CNPJ");
                            return Ok();
                        }
                    default:
                        {
                            return BadRequest();
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
            _us.DeletarConta(id);
            return Ok("Usuario deletado");
        }
    }
}
