using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Maintenance.Data
{
    public class SeedingService
    {
        private readonly MaintenanceContext _context;

        public SeedingService(MaintenanceContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Manutencao.Any())
            {
                return; // banco já foi populado
            }

            Manutencao m1 = new Manutencao {
                Id = 1,
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = "Preventiva (Insumo 1)",
                InsumoId = 1,
                InsumoDesc = "Insumo 1",
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = DateTime.Today.AddDays(182)
            };

            Manutencao m2 = new Manutencao {
                Id = 2,
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = "Preventiva (Insumo 2)",
                InsumoId = 2,
                InsumoDesc = "Insumo 2",
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = DateTime.Today.AddDays(91)
            };

            Manutencao m3 = new Manutencao {
                Id = 3,
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = "Preventiva (Insumo 3)",
                InsumoId = 3,
                InsumoDesc = "Insumo 3",
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = DateTime.Today.AddDays(182)
            };

            Manutencao m4 = new Manutencao {
                Id = 4,
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = "Preventiva (Insumo 4)",
                InsumoId = 4,
                InsumoDesc = "Insumo 4",
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = DateTime.Today.AddDays(365)
            };

            Manutencao m5 = new Manutencao {
                Id = 5,
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = "Corretiva (Insumo 3)",
                InsumoId = 3,
                InsumoDesc = "Insumo 3",
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.EXECUTANDO,
                PrevisaoManutencao = DateTime.Today,
                DataInicioManutencao = DateTime.Now.AddMinutes(17)
            };

            Manutencao m6 = new Manutencao {
                Id = 6,
                DataAgendamento = DateTime.Now,
                DescricaoAgendamento = "Preventiva (Insumo 2)",
                InsumoId = 2,
                InsumoDesc = "Insumo 2",
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.FINALIZADA,
                PrevisaoManutencao = DateTime.Today,
                DataInicioManutencao = DateTime.Now.AddMinutes(27),
                DataFimManutencao = DateTime.Now.AddMinutes(48)
            };


            _context.Manutencao.AddRange(m1, m2, m3, m4, m5, m6);

            _context.SaveChanges();
        }
    }
}
