using SCA.Shared.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Inputs.Entities
{
    public class Insumo
    {
        public int Id { get; set; }
        public Marca Marca { get; set; }
        public Tipo Tipo { get; set; }
        public InsumosStatus Status { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAquisicao { get; set; }
    }
}
