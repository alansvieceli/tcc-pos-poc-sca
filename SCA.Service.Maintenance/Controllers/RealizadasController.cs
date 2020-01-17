using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Maintenance.Services;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Maintenance;

namespace SCA.Service.Maintenance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Role.ADMIN, Role.MAINTENANCE)]
    public class RealizadasController : Controller
    {
        private readonly ManutencaoService _manutencaoService;

        public RealizadasController(ManutencaoService service)
        {
            this._manutencaoService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Manutencao>> Index()
        {
            return await this._manutencaoService.FindByStatusAsync(ManutencaoStatus.FINALIZADA);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Manutencao> Details(int? id)
        {
            var manutencao = await this._manutencaoService.FindByIdAsync(id);
            if (manutencao == null)
            {
                manutencao = new Manutencao();
            }
            return manutencao;
        }
    }
}