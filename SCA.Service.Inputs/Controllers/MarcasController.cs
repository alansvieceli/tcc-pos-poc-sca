using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities;
using SCA.Service.Inputs.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Service.Inputs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : Controller
    {
        private readonly MarcaService _marcaService;

        public MarcasController(MarcaService service)
        {
            _marcaService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Marca>> Index()
        {
            return await _marcaService.FindAllAsync();
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<Marca> Details(int? id)
        {
            var insumo = await _marcaService.FindByIdAsync(id);
            if (insumo == null)
            {
                insumo = new Marca();
            }
                return insumo;
        }
    }
}
