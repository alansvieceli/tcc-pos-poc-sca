using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities;
using SCA.Service.Inputs.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Service.Inputs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposController : Controller
    {
        private readonly TipoService _insumoService;

        public TiposController(TipoService service)
        {
            _insumoService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Tipo>> Index()
        {
            return await _insumoService.FindAllAsync();
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<Tipo> Details(int? id)
        {
            var insumo = await _insumoService.FindByIdAsync(id);
            if (insumo == null)
            {
                insumo = new Tipo();
            }
                return insumo;
        }
    }
}
