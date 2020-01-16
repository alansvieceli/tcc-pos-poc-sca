using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCA.Shared.CustomController;
using SCA.Shared.CustomAttributes;
using SCA.Shared.CustomAttributes.Enums;
using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Exceptions;
using SCA.Shared.Services;
using SCA.Web.Controllers.Filters;
using SCA.Web.Models.ViewModels;

namespace SCA.Web.Controllers
{
    [Authorize(TipoRetornoAcesso.WEB, Role.ADMIN, Role.USER_COMMON)]
    [PegarTokenActionFilter]
    [RoleActionFilter]
    public class InsumosController : ScaController
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService<Insumo> _insumosService;
        private readonly IGenericService<Marca> _marcaService;
        private readonly IGenericService<Tipo> _tipoService;

        public InsumosController(IConfiguration config, IGenericService<Insumo> insumosService, IGenericService<Marca> marcasService, IGenericService<Tipo> tiposService)
        {
            this._configuration = config;
            this._insumosService = insumosService;
            this._marcaService = marcasService;
            this._tipoService = tiposService;

            Prepare();
        }

        protected override void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            _insumosService.SetUrl($"http://{host}:{port}/input/api/insumos");
            _marcaService.SetUrl($"http://{host}:{port}/input/api/marcas");
            _tipoService.SetUrl($"http://{host}:{port}/input/api/tipos");
        }

        public override void SetToken(string token)
        {
            _insumosService.SetToken(token);
            _marcaService.SetToken(token);
            _tipoService.SetToken(token);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.StatusInativo = InsumosStatus.Inativo;
            ViewBag.StatusManutencao = InsumosStatus.Manutencao;
            return View(await _insumosService.FindAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            var marcas = await _marcaService.FindAllAsync();
            var tipos = await _tipoService.FindAllAsync();
            var viewModel = new InsumosViewModel { Insumo = new Insumo { DataCadastro = DateTime.Today }, Marcas = marcas, Tipos = tipos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                await _insumosService.InsertAsync(insumo);
                return RedirectToAction(nameof(Index));
            }
            return View(insumo);
        }

        public async Task<IActionResult> Details(int? id)
        {

            var insumo = await _insumosService.FindByIdAsync(id.Value);
            if (insumo == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(insumo);

        }

        public async Task<IActionResult> Edit(int? id)
        {

            var insusmo = await _insumosService.FindByIdAsync(id.Value);
            if (insusmo == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var marcas = await _marcaService.FindAllAsync();
            var tipos = await _tipoService.FindAllAsync();
            var viewModel = new InsumosViewModel { Insumo = insusmo, Marcas = marcas, Tipos = tipos };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Insumo insumo)
        {

            if (id != insumo.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _insumosService.UpdateAsync(id.Value, insumo);
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {

            var insumo = await _insumosService.FindByIdAsync(id.Value);
            if (insumo == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(insumo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                await _insumosService.DeleteAsync(id.Value);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
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