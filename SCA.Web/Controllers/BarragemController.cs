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
    public class BarragemController : ScaController
    {

        private readonly IConfiguration _configuration;
        private readonly IGenericService<Barragem> _barragemService;

        public BarragemController(IConfiguration config, IGenericService<Barragem> barragemService)
        {
            this._configuration = config;
            this._barragemService = barragemService;

            Prepare();
        }

        protected override void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            _barragemService.SetUrl($"http://{host}:{port}/monitoring/api/barragem");

        }

        public override void SetToken(string token)
        {
            _barragemService.SetToken(token);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Index()
        {

            return View(await _barragemService.FindAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {

            var barragem = await _barragemService.FindByIdAsync(id.Value);
            if (barragem == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(barragem);

        }
    }
}