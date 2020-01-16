using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Maintenance.Services;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Maintenance;
using SCA.Shared.Results;

namespace SCA.Service.Maintenance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Role.ADMIN, Role.MAINTENANCE)]
    public class PendentesController : Controller
    {
        private readonly ManutencaoService _manutencaoService;

        public PendentesController(ManutencaoService service)
        {
            this._manutencaoService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Manutencao>> Index()
        {
            var param = new ManutencaoStatus[] { ManutencaoStatus.PENDENTE, ManutencaoStatus.EXECUTANDO };
            return await this._manutencaoService.FindByStatusAsync(param);
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

        [HttpPost]
        public async Task<IActionResult> Create(Manutencao manutencao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this._manutencaoService.InsertAsync(manutencao);
                    return Ok(new ResultApi(true));
                }
                catch (ApplicationException e)
                {
                    return Ok(new ResultApi(false, e.Message));
                }
            }

            return Ok(new ResultApi(false, "Não foi possivel inserir"));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(int? id, Manutencao manutencao)
        {
            if (id != manutencao.Id)
            {
                return Ok(new ResultApi(false, "Id não encontrado"));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this._manutencaoService.UpdateAsync(manutencao);
                }
                catch (ApplicationException e)
                {
                    return Ok(new ResultApi(false, e.Message));
                }
            }

            return Ok(new ResultApi(true));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var manutencao = await this._manutencaoService.FindByIdAsync(id);
            if (manutencao == null)
            {
                return Ok(new ResultApi(false, "Id não encontrado"));
            }
            try
            {
                await this._manutencaoService.DeleteAsync(id);
            }
            catch (ApplicationException e)
            {
                return Ok(new ResultApi(false, e.Message));
            }

            return Ok(new ResultApi(true));
        }
    }
}