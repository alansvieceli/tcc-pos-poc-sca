using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities;
using SCA.Service.Inputs.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.Shared.Results;
using System;
using SCA.Shared.Entities.Enums;
using SCA.Shared.CustomAttributes.Enums;
using SCA.Shared.CustomAttributes;

namespace SCA.Service.Inputs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Role.ADMIN, Role.USER_COMMON)]
    public class InsumosController : Controller
    {
        private readonly InsumoService _insumoService;

        public InsumosController(InsumoService service)
        {
            _insumoService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Insumo>> Index()
        {
            return await _insumoService.FindAllAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Insumo> Details(int? id)
        {
            var insumo = await _insumoService.FindByIdAsync(id);
            if (insumo == null)
            {
                insumo = new Insumo();
            }
            return insumo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _insumoService.InsertAsync(insumo);
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
        public async Task<IActionResult> Edit(int? id, Insumo insumo)
        {
            if (id != insumo.Id)
            {
                return Ok(new ResultApi(false, "Id não encontrado"));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _insumoService.UpdateAsync(insumo);
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
            var insumo = await _insumoService.FindByIdAsync(id);
            if (insumo == null)
            {
                return Ok(new ResultApi(false, "Id não encontrado"));
            }

            insumo.Status = InsumosStatus.Inativo;
            try
            {
                await _insumoService.UpdateAsync(insumo);
            }
            catch (ApplicationException e)
            {
                return Ok(new ResultApi(false, e.Message));
            }

            return Ok(new ResultApi(true));
        }
    }
}
