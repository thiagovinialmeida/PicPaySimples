using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace PicpaySimples.Project.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LojistaController : ControllerBase
    {
        private readonly LojistaService _ls;

        public LojistaController(LojistaService ls) { _ls = ls; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetLojista()
        {
            return _ls.TodasContas();
        }

        // GET: api/Lojistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLojista(Guid id)
        {
            var lojista = _ls.MostrarConta(id);
            if (lojista == null)
            {
                return NotFound();
            }

            return Ok(lojista);
        }

        // PUT: api/Lojistas/5
        [HttpPut("{idlojista}")]
        public async Task<IActionResult> EditarLojista(Guid idlojista)
        {
            _ls.EditarConta(idlojista);
            return Ok("Lojista editado");
        }

        // POST: api/Usuarios
        [HttpPost("{nome}/{email}/{senha}/{saldo}/{identidade}")]
        public async Task<ActionResult<Usuario>> PostUsuario(string nome, string email, string senha, double saldo, string identidade)
        {
            try
            {
                switch (identidade.Length)
                {
                    case 14:
                        {
                            if (_ls.VerificarExistencia(email, identidade))
                            {
                                _ls.CriarConta(nome, email, senha, saldo, identidade);
                                Console.WriteLine("Isso é um CNPJ");
                                return Ok("Lojista criado");
                            }
                            else
                            {
                                return BadRequest("Esse CPNJ já foi registrado anteriormente");
                            }
                        }
                    case 11:
                        {
                             Console.WriteLine("Isso é um CPF");
                             return BadRequest("Não é possivel utilizar um CPF para criar um usuario Lojista");                            
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


        //DELETE: api/lojistas
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarLojista(Guid id)
        {
            _ls.DeletarConta(id);
            return Ok("Lojista deletado");
        }
    }
}
