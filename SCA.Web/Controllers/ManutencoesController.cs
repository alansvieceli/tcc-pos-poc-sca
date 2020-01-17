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
    [Authorize(TipoRetornoAcesso.WEB, Role.ADMIN, Role.MAINTENANCE, Role.USER_COMMON)]
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
            var lista = await this._manutencaoService.FindAllAsync("pendentes");
            return View(lista.OrderByDescending(m => m.Status).ThenBy(m => m.PrevisaoManutencao));
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
                var insumo = await this._insumoService.FindByIdAsync(manutencao.InsumoId);
                manutencao.InsumoDesc = insumo.Descricao;
                await this._manutencaoService.InsertAsync(manutencao, "pendentes");
                return RedirectToAction(nameof(Pendentes));
            }

            var insumos = await this._insumoService.FindAllAsync();
            var viewModel = new ManutencaoViewModel {
                Manutencao = manutencao,
                Insumos = insumos.Where(i => i.Status != InsumosStatus.Inativo)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Init(int? id)
        {
            Manutencao manutencao = await this._manutencaoService.FindByIdAsync(id.Value, "pendentes");

            manutencao.DataInicioManutencao = DateTime.Now;
            manutencao.Status = ManutencaoStatus.EXECUTANDO;

            return View(manutencao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Init(int? id, Manutencao manutencao)
        {
            if (id != manutencao.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this._manutencaoService.UpdateAsync(id.Value, manutencao, "pendentes");
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }

            return RedirectToAction(nameof(Pendentes));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Manutencao manutencao = await this._manutencaoService.FindByIdAsync(id.Value, "pendentes");
            return View(manutencao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Manutencao manutencao)
        {
            return RedirectToAction("Init", new { id, manutencao });
        }

        public async Task<IActionResult> Finalize(int? id)
        {
            Manutencao manutencao = await this._manutencaoService.FindByIdAsync(id.Value, "pendentes");
            manutencao.DataFimManutencao = DateTime.Now;
            manutencao.Status = ManutencaoStatus.FINALIZADA;
            return View(manutencao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalize(int? id, Manutencao manutencao)
        {
            IActionResult ac = RedirectToAction("Init", new { id, manutencao });

            var insumo = await this._insumoService.FindByIdAsync(manutencao.InsumoId);

            Manutencao m1 = new Manutencao {
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = $"Preventiva ({insumo.Descricao})",
                InsumoId = insumo.Id,
                InsumoDesc = insumo.Descricao,
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = DateTime.Today.AddDays((int)insumo.ManutencaoPreventiva)
            };

            return ac;
        }

        public async Task<IActionResult> Realizados()
        {
            var lista = await this._manutencaoService.FindAllAsync("realizadas");
            return View(lista.OrderByDescending(m => m.DataFimManutencao).ThenBy(m => m.PrevisaoManutencao));
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