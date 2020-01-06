using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCA.Shared.Entities.Monitoring
{
    public class Sensor
    {
        public int Id { get; set; }
        public Barragem Barragem { get; set; }
        [Display(Name = "Barragem")]
        public int BarragemId { get; set; }
        public string Descricao { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

    }
}
