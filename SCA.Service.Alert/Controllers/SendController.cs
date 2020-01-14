using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Service.Alert.Services;
using SCA.Shared.Entities.Alert;
using SCA.Shared.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Service.Alert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendController : Controller
    {
        private readonly ILogger _logger;
        public readonly CadastroService _cadastroService;

        public SendController(ILogger<SendController> logger, CadastroService cadastroService)
        {
            this._logger = logger;
            this._cadastroService = cadastroService;
        }

        [HttpPost]
        [Route("{RegiaoId}")]
        public async Task<IActionResult> Index(int? RegiaoId)
        {
            this._logger.LogInformation("##### Enviando Mensagens #####");

            IEnumerable<Cadastro> lista = await this._cadastroService.FindByRegionIdAsync(RegiaoId);

            foreach (Cadastro item in lista)
            {
                this._logger.LogWarning($" >> {item.Telefone} <<");
            }

            return Ok(new ResultApi(true));
        }
    }
}