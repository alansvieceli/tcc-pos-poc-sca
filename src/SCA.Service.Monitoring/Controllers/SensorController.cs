using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Service.Monitoring.Services;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;

namespace SCA.Monitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Role.ADMIN, Role.MONITOR)]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _sensorService;

        public SensorController(SensorService service)
        {
            _sensorService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Sensor>> Sensor()
        {
            return await _sensorService.FindAllAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Sensor> Details(int? id)
        {
            var sensor = await _sensorService.FindByIdAsync(id);
            if (sensor == null)
            {
                sensor = new Sensor();
            }
            return sensor;
        }
    }
}
