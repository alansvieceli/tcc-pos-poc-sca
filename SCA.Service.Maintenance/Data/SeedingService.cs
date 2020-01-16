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
            if (_context.Manutencao.Any() || _context.Tipo.Any() || _context.Marca.Any())
            {
                return; // banco já foi populado
            }

            Marca mc1 = new Marca { Id = 1, Descricao = "Marca 1" };
            Marca mc5 = new Marca { Id = 2, Descricao = "Marca 2" };

            Tipo t1 = new Tipo { Id = 1, Descricao = "Caminhões Ariculados" };
            Tipo t2 = new Tipo { Id = 2, Descricao = "Carregadeiras de Rodas Grandes" };
            Tipo t4 = new Tipo { Id = 4, Descricao = "Empilhadeira" };

            Insumo i1 = new Insumo { Id = 1, Descricao = "Insumo 1", Marca = mc5, Tipo = t1, Status = InsumosStatus.Ativo, DataCadastro = new DateTime(2019, 4, 10) };
            Insumo i2 = new Insumo { Id = 2, Descricao = "Insumo 2", Marca = mc5, Tipo = t2, Status = InsumosStatus.Ativo, DataCadastro = new DateTime(2019, 7, 15) };
            Insumo i3 = new Insumo { Id = 3, Descricao = "Insumo 3", Marca = mc1, Tipo = t4, Status = InsumosStatus.Ativo, DataCadastro = new DateTime(2019, 12, 19) };

            Manutencao m1 = new Manutencao {
                Id = 1,
                DataAgendamento = new DateTime(2020, 1, 10),
                DescricaoAgendamento = "Teste 1",
                InsumoId = 1,
                Tipo = ManutencaoTipo.PREVENTIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = new DateTime(2020, 1, 15)
            };

            Manutencao m2 = new Manutencao {
                Id = 2,
                DataAgendamento = new DateTime(2020, 1, 10),
                DescricaoAgendamento = "Teste 2",
                InsumoId = 3,
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.PENDENTE,
                PrevisaoManutencao = new DateTime(2020, 1, 14)
            };

            Manutencao m3 = new Manutencao {
                Id = 3,
                DataAgendamento = new DateTime(2019, 12, 10),
                DescricaoAgendamento = "Teste 3",
                InsumoId = 3,
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
                InsumoId = 3,
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
                InsumoId = 2,
                Tipo = ManutencaoTipo.CORRETIVA,
                Status = ManutencaoStatus.REALIZADA,
                PrevisaoManutencao = new DateTime(2020, 01, 02),
                DataInicioManutencao = new DateTime(2020, 12, 02, 10, 27, 44),
                DescricaoManutencao = "Executar alguma coisa yy",
                DataFimManutencao = new DateTime(2019, 12, 17, 10, 12, 44)
            };

            _context.Tipo.AddRange(t1, t2, t4);

            _context.Marca.AddRange(mc1, mc5);

            _context.Insumo.AddRange(i1, i2, i3);

            _context.Manutencao.AddRange(m1, m2, m3, m4, m5);

            _context.SaveChanges();
        }
    }
}
