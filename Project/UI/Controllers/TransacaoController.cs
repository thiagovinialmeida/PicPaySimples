using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace PicpaySimples.Project.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _ts;
        public TransacaoController(TransacaoService ts) { _ts = ts; }

        [HttpPut("{valor}/{idRemetente}/{idDestinatario}")]
        public async Task<IActionResult> Transferir(double valor, Guid idRemetente, Guid idDestinatario)
        {
            try
            {
                await _ts.FazerTransferencia(valor, idRemetente, idDestinatario);
                return Ok("Transição concluida");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
