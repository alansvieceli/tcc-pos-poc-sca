﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Monitoring.Services;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;
using SCA.Shared.Results;

namespace SCA.Service.Monitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Role.ADMIN, Role.MONITOR)]
    public class SensorHistoricoController : Controller
    {
        private readonly SensorHistoricoService _sensorHistoricoService;

        public SensorHistoricoController(SensorHistoricoService service)
        {
            this._sensorHistoricoService = service;
        }

        [HttpGet]
        [Route("LastBySensor/{id}")]
        public async Task<SensorHistorico> Details(int? id)
        {
            var sensor = await this._sensorHistoricoService.FindLastBySensorAsync(id);
            if (sensor == null)
            {
                sensor = new SensorHistorico();
            }
            return sensor;
        }

        [HttpPost]
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