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
                int status = await _ts.FazerTransferencia(valor, idRemetente, idDestinatario);

                switch(status)
                {
                    case 202:
                    {
                        return Accepted();
                    }
                    case 401:
                    {
                        return Unauthorized("Transação não autorizada");
                    }
                    default:
                    {
                        return NotFound("Não foi possivel processar a transação");
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }   
    }
}
