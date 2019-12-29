﻿using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities;
using SCA.Service.Inputs.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Service.Inputs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [Route("detail/{id}")]
        public async Task<Insumo> Details(int? id)
        {
            var insumo = await _insumoService.FindByIdAsync(id);
            if (insumo == null)
            {
                insumo = new Insumo();
            }
                return insumo;
        }
    }
}
