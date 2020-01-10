using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Monitoring.Services;
using SCA.Shared.Entities.Monitoring;
using SCA.Shared.Results;

namespace SCA.Service.Monitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicoController : Controller
    {
        private readonly SensorHistoricoService _sensorHistoricoService;
        private readonly RegiaoService _regiaoService;

        public PublicoController(SensorHistoricoService sensorHistoricoService, RegiaoService regiaoService)
        {
            this._sensorHistoricoService = sensorHistoricoService;
            this._regiaoService = regiaoService;
        }

        [HttpGet]
        [Route("regiao")]
        public async Task<IEnumerable<Regiao>> Regiao()
        {
            return await this._regiaoService.SimpleFindAllAsync();
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> Create(SensorHistorico historico)
        {
            if (ModelState.IsValid)
            {

                historico.Data = DateTime.Now;

                try
                {
                    await this._sensorHistoricoService.InsertAsync(historico);
                    return Ok(new ResultApi(true));
                }
                catch (ApplicationException e)
                {
                    return Ok(new ResultApi(false, e.Message));
                }
            }

            return Ok(new ResultApi(false, "Não foi possivel inserir"));
        }
    }
}