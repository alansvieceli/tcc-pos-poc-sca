using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Monitoring.Services;
using SCA.Shared.Entities.Alert;
using SCA.Shared.Entities.Enums;
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
        private readonly CadastroService _cadastroService;
        private readonly SensorService _sensorService;

        public PublicoController(SensorHistoricoService sensorHistoricoService, RegiaoService regiaoService, CadastroService cadastroService, SensorService sensorService)
        {
            this._sensorHistoricoService = sensorHistoricoService;
            this._regiaoService = regiaoService;
            this._cadastroService = cadastroService;
            this._sensorService = sensorService;
        }

        [HttpGet]
        [Route("regiao")]
        public async Task<IEnumerable<Regiao>> Regiao()
        {
            return await this._regiaoService.SimpleFindAllAsync();
        }

        [HttpGet]
        [Route("regiao/completo/{id}")]
        public async Task<Regiao> RegiaoDetail(int? id)
        {
            return await this._regiaoService.CompleteFindByIdAsync(id);
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

                    try
                    {
                        Sensor sensor = await this._sensorService.FindByIdAsync(historico.SensorId);
                        this._cadastroService.EnviarAlertas(historico.Status, sensor.Barragem.RegiaoId);
                    }
                    catch (Exception e)
                    {
                        //ignorar execeção
                    }

                    return Ok(new ResultApi(true));
                }
                catch (ApplicationException e)
                {
                    return Ok(new ResultApi(false, e.Message));
                }
            }

            return Ok(new ResultApi(false, "Não foi possivel inserir"));
        }

        [HttpPost]
        [Route("cadastro")]
        public async Task<IActionResult> Cadastro(Cadastro cadastro)
        {
            await this._cadastroService.InsertAsync(cadastro);
            return Ok(new ResultApi(true));
        }



    }
}