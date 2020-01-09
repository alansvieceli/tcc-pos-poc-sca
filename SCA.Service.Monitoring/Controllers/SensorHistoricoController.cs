using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Monitoring.Services;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;

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
            _sensorHistoricoService = service;
        }

        [HttpGet]
        [Route("LastBySensor/{id}")]
        public async Task<SensorHistorico> Details(int? id)
        {
            var sensor = await _sensorHistoricoService.FindLastBySensorAsync(id);
            if (sensor == null)
            {
                sensor = new SensorHistorico();
            }
            return sensor;
        }
    }
}