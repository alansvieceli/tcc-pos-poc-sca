using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities.Inputs;
using SCA.Service.Inputs.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.Shared.Entities.Enums;
using SCA.Shared.CustomAttributes;

namespace SCA.Service.Inputs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Role.ADMIN, Role.USER_COMMON)]
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
