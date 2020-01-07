using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Monitoring.Services;
using SCA.Shared.Entities.Monitoring;

namespace SCA.Service.Monitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Role.ADMIN, Role.MONITOR)]
    public class BarragemController : Controller
    {
        private readonly BarragemService _barragemService;

        public BarragemController(BarragemService service)
        {
            _barragemService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Barragem>> Barragem()
        {
            return await _barragemService.FindAllAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Barragem> Details(int? id)
        {
            var barragem = await _barragemService.FindByIdAsync(id);
            if (barragem == null)
            {
                barragem = new Barragem();
            }
            return barragem;
        }

    }
}