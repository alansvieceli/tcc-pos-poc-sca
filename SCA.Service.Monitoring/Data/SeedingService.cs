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
//            Sensor s3 = new Sensor { Id = 3, BarragemId = 1, Barragem = b1, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 10, 7) };
            Sensor s4 = new Sensor { Id = 4, BarragemId = 1, Barragem = b1, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 10, 7) };
//            Sensor s5 = new Sensor { Id = 5, BarragemId = 1, Barragem = b1, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 10, 7) };

            Sensor s6 = new Sensor { Id = 6, BarragemId = 2, Barragem = b2, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 10, 9) };
            Sensor s7 = new Sensor { Id = 7, BarragemId = 2, Barragem = b2, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 10, 9) };
//            Sensor s8 = new Sensor { Id = 8, BarragemId = 2, Barragem = b2, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 10, 9) };
            Sensor s9 = new Sensor { Id = 9, BarragemId = 2, Barragem = b2, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 10, 9) };
            Sensor s10 = new Sensor { Id = 10, BarragemId = 2, Barragem = b2, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 10, 9) };

            Sensor s11 = new Sensor { Id = 11, BarragemId = 3, Barragem = b3, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s12 = new Sensor { Id = 12, BarragemId = 3, Barragem = b3, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s13 = new Sensor { Id = 13, BarragemId = 3, Barragem = b3, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s14 = new Sensor { Id = 14, BarragemId = 3, Barragem = b3, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 10, 10) };
            Sensor s15 = new Sensor { Id = 15, BarragemId = 3, Barragem = b3, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 10, 10) };

            Sensor s16 = new Sensor { Id = 16, BarragemId = 4, Barragem = b4, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 11, 5) };
            Sensor s17 = new Sensor { Id = 17, BarragemId = 4, Barragem = b4, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 11, 5) };
            Sensor s18 = new Sensor { Id = 18, BarragemId = 4, Barragem = b4, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 11, 5) };
            Sensor s19 = new Sensor { Id = 19, BarragemId = 4, Barragem = b4, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 11, 5) };
//            Sensor s20 = new Sensor { Id = 20, BarragemId = 4, Barragem = b4, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 11, 5) };

            Sensor s21 = new Sensor { Id = 21, BarragemId = 5, Barragem = b5, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 11, 9) };
//            Sensor s22 = new Sensor { Id = 22, BarragemId = 5, Barragem = b5, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 11, 9) };
//            Sensor s23 = new Sensor { Id = 23, BarragemId = 5, Barragem = b5, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 11, 9) };
//            Sensor s24 = new Sensor { Id = 24, BarragemId = 5, Barragem = b5, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 11, 9) };
            Sensor s25 = new Sensor { Id = 25, BarragemId = 5, Barragem = b5, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 11, 9) };

//            Sensor s26 = new Sensor { Id = 26, BarragemId = 6, Barragem = b6, Descricao = "Nível de Água", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s27 = new Sensor { Id = 27, BarragemId = 6, Barragem = b6, Descricao = "Pressão da Água", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s28 = new Sensor { Id = 28, BarragemId = 6, Barragem = b6, Descricao = "Piezómetro (Compressibilidade dos líquidos)", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s29 = new Sensor { Id = 29, BarragemId = 6, Barragem = b6, Descricao = "Inclinômetros", DataCadastro = new DateTime(2019, 11, 23) };
            Sensor s30 = new Sensor { Id = 30, BarragemId = 6, Barragem = b6, Descricao = "Sensores termais (Vazamento)", DataCadastro = new DateTime(2019, 11, 23) };

            _context.Regiao.AddRange(r1, r2, r3);

            _context.Barragem.AddRange(b1, b2, b3, b4, b5, b6);

            _context.Sensor.AddRange(
                s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s11, s12, s13, s14, s15, 
                s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30);                

            _context.SaveChanges();

        }
    }
}

