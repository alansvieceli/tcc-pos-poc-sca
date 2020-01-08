using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCA.Shared.CustomAttributes;
using SCA.Shared.CustomAttributes.Enums;
using SCA.Shared.CustomController;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;
using SCA.Shared.Services;
using SCA.Web.Controllers.Filters;
using SCA.Web.Models.ViewModels;

namespace SCA.Web.Controllers
{
    [Authorize(TipoRetornoAcesso.WEB, Role.ADMIN, Role.MONITOR)]
    [PegarTokenActionFilter]
    [RoleActionFilter]
    public class SensorController : ScaController
    {

        private readonly IConfiguration _configuration;
        private readonly IGenericService<Sensor> _sensorService;

        public SensorController(IConfiguration config, IGenericService<Sensor> sensorService)
        {
            this._configuration = config;
            this._sensorService = sensorService;

            Prepare();
        }

        protected override void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            _sensorService.SetUrl($"http://{host}:{port}/monitoring/api/sensor");

        }

        public override void SetToken(string token)
        {
            _sensorService.SetToken(token);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var sensor = await _sensorService.FindByIdAsync(id.Value);
            if (sensor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(sensor);
        }

    }
}