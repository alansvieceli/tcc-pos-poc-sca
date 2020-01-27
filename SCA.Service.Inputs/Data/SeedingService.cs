using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Enums;
using System;
using System.Linq;

namespace SCA.Service.Inputs.Data
{
    public class SeedingService
    {
        private readonly InputsContext _context;

        public SeedingService(InputsContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            //se tiver alguma coisa no banco, sai fora
            if (_context.Insumo.Any() || _context.Tipo.Any() || _context.Marca.Any())
            {
                return; // banco já foi populado
            }

            Marca m1 = new Marca { Id = 1, Descricao = "CAT" };
            Marca m5 = new Marca { Id = 2, Descricao = "HYSTER" };
            Marca m6 = new Marca { Id = 3, Descricao = "Bristol" };
            Marca m7 = new Marca { Id = 4, Descricao = "New Holland" };
            Marca m8 = new Marca { Id = 5, Descricao = "Tramontina" };

            Tipo t1 = new Tipo { Id = 1, Descricao = "Caminhões Ariculados" };
            Tipo t2 = new Tipo { Id = 2, Descricao = "Carregadeiras de Rodas Grandes" };
            Tipo t3 = new Tipo { Id = 3, Descricao = "Draglines" };
            Tipo t4 = new Tipo { Id = 4, Descricao = "Empilhadeira" };
            Tipo t7 = new Tipo { Id = 5, Descricao = "Escavadeira" };
            Tipo t8 = new Tipo { Id = 6, Descricao = "Guindates" };
            Tipo t9 = new Tipo { Id = 7, Descricao = "Mini Carregadeiras" };
            Tipo t5 = new Tipo { Id = 8, Descricao = "Perfuratrizes" };
            Tipo t6 = new Tipo { Id = 9, Descricao = "Retroescavadeiras" };
            Tipo t10 = new Tipo { Id = 10, Descricao = "Sensores de Capacidade" };
            Tipo t11 = new Tipo { Id = 11, Descricao = "Outros" };

            Insumo i1 = new Insumo { Id = 1, Descricao = "Insumo 1", Marca = m5, Tipo = t1, Status = InsumosStatus.Ativo, DataCadastro = DateTime.Today, ManutencaoPreventiva = InsumoManutencaoPreventiva.SEMESTRAL };
            Insumo i2 = new Insumo { Id = 2, Descricao = "Insumo 2", Marca = m5, Tipo = t2, Status = InsumosStatus.Ativo, DataCadastro = DateTime.Today, ManutencaoPreventiva = InsumoManutencaoPreventiva.TRIMESTRAL };
            Insumo i3 = new Insumo { Id = 3, Descricao = "Insumo 3", Marca = m1, Tipo = t4, Status = InsumosStatus.Ativo, DataCadastro = DateTime.Today, ManutencaoPreventiva = InsumoManutencaoPreventiva.SEMESTRAL };
            Insumo i4 = new Insumo { Id = 4, Descricao = "Insumo 4", Marca = m7, Tipo = t6, Status = InsumosStatus.Inativo, DataCadastro = DateTime.Today, ManutencaoPreventiva = InsumoManutencaoPreventiva.ANUAL };

            _context.Tipo.AddRange(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);

            _context.Marca.AddRange(m1, m5, m6, m7, m8);

            _context.Insumo.AddRange(i1, i2, i3, i4);

            _context.SaveChanges();
        }
    }
}
