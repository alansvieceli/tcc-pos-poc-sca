using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Web.Services;

namespace SCA.Web.Controllers
{
    public class InsumosController : Controller
    {
        private readonly InsumosService _insumosService;

        public InsumosController(InsumosService insumosService)
        {
            this._insumosService = insumosService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _insumosService.FindAllAsync());
        }
    }
}