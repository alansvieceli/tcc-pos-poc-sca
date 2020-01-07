using System;
using System.Collections.Generic;
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

namespace SCA.Web.Controllers
{
    [Authorize(TipoRetornoAcesso.WEB, Role.ADMIN, Role.MONITOR)]
    [PegarTokenActionFilter]
    [RoleActionFilter]
    public class MonitoramentoController : ScaController
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService<Regiao> _regiaoService;

        public MonitoramentoController(IConfiguration config, IGenericService<Regiao> regiaoService)
        {
            this._configuration = config;
            this._regiaoService = regiaoService;

            Prepare();
        }

        private void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            _regiaoService.SetUrl($"http://{host}:{port}/monitoring/api/regiao");

        }

        public async Task<IActionResult> Index()
        {
            return View(await _regiaoService.FindAllAsync());
        }

        public override void SetToken(string token)
        {
            _regiaoService.SetToken(token);
        }
    }
}