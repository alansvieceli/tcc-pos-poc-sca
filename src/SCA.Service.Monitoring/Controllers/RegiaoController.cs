using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Service.Monitoring.Services;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;

namespace SCA.Monitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Role.ADMIN, Role.MONITOR)]
    public class RegiaoController : ControllerBase
    {
        private readonly RegiaoService _regiaoService;

        public RegiaoController(RegiaoService service)
        {
            _regiaoService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Regiao>> Regiao()
        {
            return await _regiaoService.FindAllAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Regiao> Details(int? id)
        {
            var regiao = await _regiaoService.FindByIdAsync(id);
            if (regiao == null)
            {
                regiao = new Regiao();
            }
            return regiao;
        }
    }
}
