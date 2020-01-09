using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Monitoring.Data
{
    public class SeedingService
    {
        private readonly MonitoramentoContext _context;

        public SeedingService(MonitoramentoContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            //se tiver alguma coisa no banco, sai fora
            if (_context.Regiao.Any() || _context.Barragem.Any() || _context.Sensor.Any() || _context.SensorHistorico.Any())
            {
                return; // banco já foi populado
            }

            Regiao r1 = new Regiao { Id = 1, UF = "MG", Cidade = "Outro Preto" };
            Regiao r2 = new Regiao { Id = 2, UF = "MG", Cidade = "Nova Lima" };
            Regiao r3 = new Regiao { Id = 3, UF = "MG", Cidade = "Brumadinho" };

            Barragem b1 = new Barragem { Id = 1, RegiaoId = 1, Regiao = r1, Descricao = "Barragem Forquilha I", DataCadastro = new DateTime(2019, 10, 7) };
            Barragem b2 = new Barragem { Id = 2, RegiaoId = 1, Regiao = r1, Descricao = "Barragem Forquilha II", DataCadastro = new DateTime(2019, 10, 9) };
            Barragem b3 = new Barragem { Id = 3, RegiaoId = 1, Regiao = r1, Descricao = "Barragem Forquilha III", DataCadastro = new DateTime(2019, 10, 10) };
            Barragem b4 = new Barragem { Id = 4, RegiaoId = 2, Regiao = r2, Descricao = "Barragem Capitão do Mato", DataCadastro = new DateTime(2019, 11, 5) };
            Barragem b5 = new Barragem { Id = 5, RegiaoId = 2, Regiao = r2, Descricao = "Barragem Taquaras", DataCadastro = new DateTime(2019, 11, 9) };
            Barragem b6 = new Barragem { Id = 6, RegiaoId = 3, Regiao = r3, Descricao = "Barragem Menezes II", DataCadastro = new DateTime(2019, 11, 23) };

            Sensor s1 = new Sensor { Id = 1, BarragemId = 1, Barragem = b1, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 10, 7) };
            Sensor s2 = new Sensor { Id = 2, BarragemId = 1, Barragem = b1, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 10, 7) };
            Sensor s4 = new Sensor { Id = 3, BarragemId = 1, Barragem = b1, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 10, 7) };

            Sensor s6 = new Sensor { Id = 4, BarragemId = 2, Barragem = b2, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 10, 9) };
            Sensor s7 = new Sensor { Id = 5, BarragemId = 2, Barragem = b2, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 10, 9) };
            Sensor s9 = new Sensor { Id = 6, BarragemId = 2, Barragem = b2, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 10, 9) };
            Sensor s10 = new Sensor { Id = 7, BarragemId = 2, Barragem = b2, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 10, 9) };

            Sensor s11 = new Sensor { Id = 8, BarragemId = 3, Barragem = b3, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s12 = new Sensor { Id = 9, BarragemId = 3, Barragem = b3, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s13 = new Sensor { Id = 10, BarragemId = 3, Barragem = b3, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s14 = new Sensor { Id = 11, BarragemId = 3, Barragem = b3, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s15 = new Sensor { Id = 12, BarragemId = 3, Barragem = b3, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 10, 10) };

            Sensor s16 = new Sensor { Id = 13, BarragemId = 4, Barragem = b4, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 11, 5) };
            Sensor s17 = new Sensor { Id = 14, BarragemId = 4, Barragem = b4, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 11, 5) };
            Sensor s18 = new Sensor { Id = 15, BarragemId = 4, Barragem = b4, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 11, 5) };
            Sensor s19 = new Sensor { Id = 16, BarragemId = 4, Barragem = b4, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 11, 5) };

            Sensor s21 = new Sensor { Id = 17, BarragemId = 5, Barragem = b5, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 11, 9) };
            Sensor s25 = new Sensor { Id = 18, BarragemId = 5, Barragem = b5, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 11, 9) };

            Sensor s27 = new Sensor { Id = 19, BarragemId = 6, Barragem = b6, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s28 = new Sensor { Id = 20, BarragemId = 6, Barragem = b6, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s29 = new Sensor { Id = 21, BarragemId = 6, Barragem = b6, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s30 = new Sensor { Id = 22, BarragemId = 6, Barragem = b6, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 11, 23) };

            SensorHistorico sh1 = new SensorHistorico { Id = 1, Sensor = s1, Data = new DateTime(2019, 11, 14, 10, 15, 25), Status = SensorStatus.Verde };
            SensorHistorico sh2 = new SensorHistorico { Id = 2, Sensor = s1, Data = new DateTime(2019, 11, 14, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh3 = new SensorHistorico { Id = 3, Sensor = s2, Data = new DateTime(2019, 11, 14, 9, 15, 25), Status = SensorStatus.Verde };
            SensorHistorico sh4 = new SensorHistorico { Id = 4, Sensor = s2, Data = new DateTime(2019, 11, 14, 10, 04, 13), Status = SensorStatus.Amarelo };
            SensorHistorico sh5 = new SensorHistorico { Id = 5, Sensor = s4, Data = new DateTime(2019, 11, 15, 05, 44, 01), Status = SensorStatus.Amarelo };
            SensorHistorico sh6 = new SensorHistorico { Id = 6, Sensor = s4, Data = new DateTime(2019, 11, 15, 09, 47, 9), Status = SensorStatus.Amarelo };
            SensorHistorico sh7 = new SensorHistorico { Id = 7, Sensor = s4, Data = new DateTime(2019, 11, 15, 14, 04, 19), Status = SensorStatus.Verde };
            SensorHistorico sh8 = new SensorHistorico { Id = 8, Sensor = s6, Data = new DateTime(2019, 11, 14, 07, 15, 25), Status = SensorStatus.Verde };
            SensorHistorico sh9 = new SensorHistorico { Id = 9, Sensor = s6, Data = new DateTime(2019, 11, 14, 08, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh10 = new SensorHistorico { Id = 10, Sensor = s6, Data = new DateTime(2019, 11, 14, 09, 04, 44), Status = SensorStatus.Verde };
            SensorHistorico sh11 = new SensorHistorico { Id = 11, Sensor = s7, Data = new DateTime(2019, 11, 14, 9, 15, 25), Status = SensorStatus.Verde };
            SensorHistorico sh12= new SensorHistorico { Id = 12, Sensor = s7, Data = new DateTime(2019, 11, 14, 10, 19, 44), Status = SensorStatus.Amarelo };
            SensorHistorico sh13 = new SensorHistorico { Id = 13, Sensor = s9, Data = new DateTime(2019, 11, 15, 11, 44, 01), Status = SensorStatus.Vermelho };
            SensorHistorico sh14 = new SensorHistorico { Id = 14, Sensor = s9, Data = new DateTime(2019, 11, 15, 11, 47, 9), Status = SensorStatus.Amarelo };
            SensorHistorico sh15 = new SensorHistorico { Id = 15, Sensor = s9, Data = new DateTime(2019, 11, 15, 12, 04, 19), Status = SensorStatus.Amarelo };
            SensorHistorico sh16 = new SensorHistorico { Id = 16, Sensor = s10, Data = new DateTime(2019, 11, 14, 3, 15, 25), Status = SensorStatus.Verde };
            SensorHistorico sh17 = new SensorHistorico { Id = 17, Sensor = s10, Data = new DateTime(2019, 11, 14, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh18 = new SensorHistorico { Id = 18, Sensor = s11, Data = new DateTime(2019, 11, 13, 2, 15, 25), Status = SensorStatus.Verde };
            SensorHistorico sh19 = new SensorHistorico { Id = 19, Sensor = s11, Data = new DateTime(2019, 11, 13, 10, 44, 44), Status = SensorStatus.Verde };
            SensorHistorico sh20 = new SensorHistorico { Id = 20, Sensor = s12, Data = new DateTime(2019, 11, 13, 9, 17, 25), Status = SensorStatus.Verde };
            SensorHistorico sh21 = new SensorHistorico { Id = 21, Sensor = s12, Data = new DateTime(2019, 11, 13, 10, 24, 44), Status = SensorStatus.Verde };
            SensorHistorico sh22 = new SensorHistorico { Id = 22, Sensor = s13, Data = new DateTime(2019, 11, 13, 7, 27, 25), Status = SensorStatus.Verde };
            SensorHistorico sh23 = new SensorHistorico { Id = 23, Sensor = s13, Data = new DateTime(2019, 11, 13, 10, 09, 44), Status = SensorStatus.Verde };
            SensorHistorico sh24 = new SensorHistorico { Id = 24, Sensor = s14, Data = new DateTime(2019, 11, 13, 08, 22, 25), Status = SensorStatus.Verde };
            SensorHistorico sh25 = new SensorHistorico { Id = 25, Sensor = s14, Data = new DateTime(2019, 11, 13, 10, 01, 44), Status = SensorStatus.Verde };
            SensorHistorico sh27 = new SensorHistorico { Id = 26, Sensor = s15, Data = new DateTime(2019, 11, 13, 9, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh26 = new SensorHistorico { Id = 27, Sensor = s15, Data = new DateTime(2019, 11, 13, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh28 = new SensorHistorico { Id = 28, Sensor = s16, Data = new DateTime(2019, 11, 17, 9, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh29 = new SensorHistorico { Id = 29, Sensor = s16, Data = new DateTime(2019, 11, 17, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh30 = new SensorHistorico { Id = 30, Sensor = s17, Data = new DateTime(2019, 11, 17, 9, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh31 = new SensorHistorico { Id = 31, Sensor = s17, Data = new DateTime(2019, 11, 17, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh32 = new SensorHistorico { Id = 32, Sensor = s18, Data = new DateTime(2019, 11, 17, 5, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh33 = new SensorHistorico { Id = 33, Sensor = s18, Data = new DateTime(2019, 11, 17, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh34 = new SensorHistorico { Id = 34, Sensor = s19, Data = new DateTime(2019, 11, 17, 8, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh35 = new SensorHistorico { Id = 35, Sensor = s19, Data = new DateTime(2019, 11, 17, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh36 = new SensorHistorico { Id = 36, Sensor = s21, Data = new DateTime(2019, 11, 17, 9, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh37 = new SensorHistorico { Id = 37, Sensor = s21, Data = new DateTime(2019, 11, 17, 10, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh38 = new SensorHistorico { Id = 38, Sensor = s25, Data = new DateTime(2019, 11, 15, 15, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh39 = new SensorHistorico { Id = 39, Sensor = s25, Data = new DateTime(2019, 11, 15, 18, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh40 = new SensorHistorico { Id = 40, Sensor = s27, Data = new DateTime(2019, 11, 15, 14, 00, 25), Status = SensorStatus.Verde };
            SensorHistorico sh41 = new SensorHistorico { Id = 41, Sensor = s27, Data = new DateTime(2019, 11, 15, 18, 19, 44), Status = SensorStatus.Verde };
            SensorHistorico sh42 = new SensorHistorico { Id = 42, Sensor = s28, Data = new DateTime(2019, 11, 15, 9, 50, 25), Status = SensorStatus.Verde };
            SensorHistorico sh43 = new SensorHistorico { Id = 43, Sensor = s28, Data = new DateTime(2019, 11, 15, 12, 44, 44), Status = SensorStatus.Verde };
            SensorHistorico sh44 = new SensorHistorico { Id = 44, Sensor = s29, Data = new DateTime(2019, 11, 13, 15, 43, 25), Status = SensorStatus.Verde };
            SensorHistorico sh45 = new SensorHistorico { Id = 45, Sensor = s29, Data = new DateTime(2019, 11, 13, 17, 58, 44), Status = SensorStatus.Verde };
            SensorHistorico sh46 = new SensorHistorico { Id = 46, Sensor = s30, Data = new DateTime(2019, 11, 13, 19, 10, 25), Status = SensorStatus.Verde };
            SensorHistorico sh47 = new SensorHistorico { Id = 47, Sensor = s30, Data = new DateTime(2019, 11, 13, 22, 09, 44), Status = SensorStatus.Verde };

            _context.Regiao.AddRange(r1, r2, r3);

            _context.Barragem.AddRange(b1, b2, b3, b4, b5, b6);

            _context.Sensor.AddRange(
                s1, s2, s4, s6, s7, s9, s10, s11, s12, s13, s14, s15, s11, s12, s13, s14, s15, 
                s16, s17, s18, s19, s21, s25, s27, s28, s29, s30);

            _context.SensorHistorico.AddRange(
                sh1, sh2, sh3, sh4, sh5, sh6, sh7, sh8, sh9, sh10, sh11, sh12, sh13, sh14, sh15, sh16, sh17,
                sh18, sh19, sh20, sh21, sh22, sh23, sh24, sh25, sh26, sh27, sh28, sh29, sh30, sh31, sh32, sh33,
                sh34, sh35, sh36, sh37, sh38, sh39, sh40, sh41, sh42, sh43, sh44, sh45, sh46, sh47);

            _context.SaveChanges();

        }
    }
}

