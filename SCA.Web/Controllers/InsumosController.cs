using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities;
using SCA.Shared.Entities.Enums;
using SCA.Web.Models.ViewModels;
using SCA.Web.Services;

namespace SCA.Web.Controllers
{
    public class InsumosController : Controller
    {
        private readonly IGenericService<Insumo> _insumosService;
        private readonly IGenericService<Marca> _marcaService;
        private readonly IGenericService<Tipo> _tipoService;

        public InsumosController(IGenericService<Insumo> insumosService, IGenericService<Marca> marcasService, IGenericService<Tipo> tiposService)
        {
            this._insumosService = insumosService;
            this._marcaService = marcasService;
            this._tipoService = tiposService;
            _insumosService.SetUrl("http://localhost:7000/input/api/insumos");
            _marcaService.SetUrl("http://localhost:7000/input/api/marcas");
            _tipoService.SetUrl("http://localhost:7000/input/api/tipos");
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
            var viewModel = new InsumosViewModel { Insumo = new Insumo { DataCadastro = DateTime.Today },  Marcas = marcas, Tipos = tipos };
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
                    await _insumosService.UpdateAsync(id, insumo);
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }

            return RedirectToAction(nameof(Index));

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