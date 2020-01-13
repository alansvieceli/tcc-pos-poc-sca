using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCA.Shared.Dto;
using SCA.Shared.Entities.Alert;
using SCA.Shared.Entities.Monitoring;
using SCA.Shared.Services;

namespace SCA.Web.Controllers
{
    public class PublicoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService<Regiao> _regiaoService;
        private readonly IGenericService<Cadastro> _cadastroService;

        public PublicoController(IConfiguration config, IGenericService<Regiao> regiaoService, IGenericService<Cadastro> cadastroService)
        {
            this._configuration = config;
            this._regiaoService = regiaoService;
            this._cadastroService = cadastroService;

            Prepare();
        }

        protected void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            
            _regiaoService.SetUrl($"http://{host}:{port}/monitoring/api/publico/regiao");
            _cadastroService.SetUrl($"http://{host}:{port}/monitoring/api/publico/cadastro");

        }

        public async Task<IActionResult> Index()
        {
            return View(await _regiaoService.FindAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Barragens(int RegiaoId)
        {
            ViewBag.RegiaoId = RegiaoId;
            return View(await _regiaoService.CompleteFindByIdAsync(RegiaoId));
        }

        [HttpPost]
        public async Task<IActionResult> Alerta(int regiaoId, string telefone)
        {
            await _cadastroService.InsertAsync( new Cadastro { RegiaoId = regiaoId, Telefone = telefone });
            return View();
        }
    }
}