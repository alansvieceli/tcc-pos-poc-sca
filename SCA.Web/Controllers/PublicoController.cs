using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCA.Shared.Dto;
using SCA.Shared.Entities.Monitoring;
using SCA.Shared.Services;

namespace SCA.Web.Controllers
{
    public class PublicoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService<Regiao> _regiaoService;

        public PublicoController(IConfiguration config, IGenericService<Regiao> regiaoService)
        {
            this._configuration = config;
            this._regiaoService = regiaoService;

            Prepare();
        }

        protected void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            _regiaoService.SetUrl($"http://{host}:{port}/monitoring/api/publico/regiao");

        }

        public async Task<IActionResult> Index()
        {
            return View(await _regiaoService.FindAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Barragens(int RegiaoId)
        {
            return View(await _regiaoService.CompleteFindByIdAsync(RegiaoId));
        }
    }
}