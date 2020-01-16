using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCA.Shared.CustomAttributes;
using SCA.Shared.CustomAttributes.Enums;
using SCA.Shared.CustomController;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Maintenance;
using SCA.Shared.Services;
using SCA.Web.Controllers.Filters;
using SCA.Web.Models.ViewModels;

namespace SCA.Web.Controllers
{
    [Authorize(TipoRetornoAcesso.WEB, Role.ADMIN, Role.MAINTENANCE)]
    [PegarTokenActionFilter]
    [RoleActionFilter]
    public class ManutencaoController : ScaController
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService<Manutencao> _manutencaoService;

        private static List<Manutencao> lista = new List<Manutencao>();

        public ManutencaoController(IConfiguration config, IGenericService<Manutencao> manutencaoService)
        {
            this._configuration = config;
            this._manutencaoService = manutencaoService;

            Prepare();
        }

        protected override void Prepare()
        {
            string host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            int port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
            this._manutencaoService.SetUrl($"http://{host}:{port}/maintenance/api/manutencao");

            Manutencao m1 = new Manutencao {
                Id = 1,
                DataAgendamento = new DateTime(2020, 1, 10),
                DescricaoAgendamento = "Teste 1",
                InsumoId = 1,
                Insumo = new Shared.Entities.Inputs.Insumo { Id = 1, Descricao = "Insumo Teste" },
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = new DateTime(2020, 1, 15)
            };

            Manutencao m2 = new Manutencao {
                Id = 2,
                DataAgendamento = new DateTime(2020, 1, 10),
                DescricaoAgendamento = "Teste 2",
                InsumoId = 6,
                Insumo = new Shared.Entities.Inputs.Insumo { Id = 6, Descricao = "Insumo Teste 6" },
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = new DateTime(2020, 1, 14)
            };

            Manutencao m3 = new Manutencao {
                Id = 3,
                DataAgendamento = new DateTime(2019, 12, 10),
                DescricaoAgendamento = "Teste 3",
                InsumoId = 6,
                Insumo = new Shared.Entities.Inputs.Insumo { Id = 6, Descricao = "Insumo Teste 6" },
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.REALIZADA,
                PrevisaoManutencao = new DateTime(2019, 12, 14),
                DataInicioManutencao = new DateTime(2019, 12, 14, 10, 05, 23),
                DescricaoManutencao = "Executar alguma coisa",
                DataFimManutencao = new DateTime(2019, 12, 17, 10, 26, 44)
            };

            Manutencao m4 = new Manutencao {
                Id = 4,
                DataAgendamento = new DateTime(2020, 01, 01),
                DescricaoAgendamento = "Teste 3",
                InsumoId = 6,
                Insumo = new Shared.Entities.Inputs.Insumo { Id = 6, Descricao = "Insumo Teste 6" },
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.EXECUTANDO,
                PrevisaoManutencao = new DateTime(2020, 01, 03),
                DataInicioManutencao = new DateTime(2020, 12, 04, 07, 02, 44),
                DescricaoManutencao = "Executar alguma coisa xx",
            };


            Manutencao m5 = new Manutencao {
                Id = 5,
                DataAgendamento = new DateTime(2020, 01, 01),
                DescricaoAgendamento = "Teste 3",
                InsumoId = 4,
                Insumo = new Shared.Entities.Inputs.Insumo { Id = 4, Descricao = "Insumo Teste 4" },
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.REALIZADA,
                PrevisaoManutencao = new DateTime(2020, 01, 02),
                DataInicioManutencao = new DateTime(2020, 12, 02, 10, 27, 44),
                DescricaoManutencao = "Executar alguma coisa yy",
                DataFimManutencao = new DateTime(2019, 12, 17, 10, 12, 44)
            };


            lista.Add(m1);
            lista.Add(m2);
            lista.Add(m3);
            lista.Add(m4);
            lista.Add(m5);
        }

        public override void SetToken(string token)
        {
            this._manutencaoService.SetToken(token);
        }

        public async Task<IActionResult> Pendentes()
        {

            var lista = ManutencaoController.lista.Where(m => m.Status != ManutencaoStatus.REALIZADA).OrderByDescending(m => m.Status);

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var insumos = new List<Insumo>();
            insumos.Add(new Insumo { Id = 1, Descricao = "Insumo 1" });
            insumos.Add(new Insumo { Id = 4, Descricao = "Insumo 4" });
            insumos.Add(new Insumo { Id = 6, Descricao = "Insumo 6" });
            var viewModel = new ManutencaoViewModel {
                Manutencao = new Manutencao {
                    DataAgendamento = DateTime.Now,
                    Status = ManutencaoStatus.PENDENTE,
                    PrevisaoManutencao = DateTime.Today,
                    Tipo = ManutencaoTipo.CORRETIVA
                }, 
                Insumos = insumos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manutencao manutencao)
        {

            if (ModelState.IsValid)
            {
                //await _insumosService.InsertAsync(insumo);
                ManutencaoController.lista.Add(manutencao);
                return RedirectToAction(nameof(Pendentes));
            }
            return View(manutencao);

        }

        public IActionResult Realizados()
        {

            var lista = ManutencaoController.lista.Where(m => m.Status == ManutencaoStatus.REALIZADA).OrderByDescending(m => m.DataFimManutencao);

            return View(lista);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {

            var manutencao = ManutencaoController.lista.Where(m => m.Id == id.Value).FirstOrDefault();
            if (manutencao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(manutencao);

        }
    }
}