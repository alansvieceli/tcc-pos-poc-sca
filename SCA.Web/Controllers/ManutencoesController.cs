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
using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Maintenance;
using SCA.Shared.Services;
using SCA.Web.Controllers.Filters;
using SCA.Web.Models.ViewModels;

namespace SCA.Web.Controllers
{
    [Authorize(TipoRetornoAcesso.WEB, Role.ADMIN, Role.MAINTENANCE)]
    [PegarTokenActionFilter]
    [RoleActionFilter]
    public class ManutencoesController : ScaController
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService<Manutencao> _manutencaoService;
        private readonly IGenericService<Insumo> _insumoService;

        public ManutencoesController(IConfiguration config, IGenericService<Manutencao> manutencaoService, IGenericService<Insumo> insumoService)
        {
            this._configuration = config;
            this._manutencaoService = manutencaoService;
            this._insumoService = insumoService;

            Prepare();
        }

        protected override void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            this._manutencaoService.SetUrl($"http://{host}:{port}/maintenance/api");
            this._insumoService.SetUrl($"http://{host}:{port}/input/api/insumos");

        }

        public override void SetToken(string token)
        {
            this._manutencaoService.SetToken(token);
            this._insumoService.SetToken(token);
        }

        public async Task<IActionResult> Pendentes()
        {
            return View(await this._manutencaoService.FindAllAsync("pendentes"));
        }

        public async Task<IActionResult> Create()
        {
            var insumos = await this._insumoService.FindAllAsync();
            
            var viewModel = new ManutencaoViewModel {
                Manutencao = new Manutencao {
                    DataAgendamento = DateTime.Now,
                    PrevisaoManutencao = DateTime.Today,
                    Tipo = ManutencaoTipo.CORRETIVA,
                    Status = ManutencaoStatus.PENDENTE
                },
                Insumos = insumos.Where(i => i.Status != InsumosStatus.Inativo)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manutencao manutencao)
        {
            if (ModelState.IsValid)
            {
                await this._manutencaoService.InsertAsync(manutencao);
                return RedirectToAction(nameof(Index));
            }

            var insumos = await this._insumoService.FindAllAsync();
            var viewModel = new ManutencaoViewModel {
                Manutencao = manutencao,
                Insumos = insumos.Where(i => i.Status != InsumosStatus.Inativo)
            };
            return View(viewModel);

        }

        public async Task<IActionResult> Realizados()
        {
            return View(await this._manutencaoService.FindAllAsync("realizadas"));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var manutencao = await _manutencaoService.FindByIdAsync(id.Value, "realizadas");
            if (manutencao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(manutencao);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}