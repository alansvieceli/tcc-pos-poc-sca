using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SCA.Shared.Entities.Monitoring
{
    public class Barragem
    {
        public int Id { get; set; }
        public Regiao Regiao { get; set; }
        [Display(Name = "Região")]
        public int RegiaoId { get; set; }
        public string Descricao { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        public ICollection<Sensor> Sensores { get; set; } = new List<Sensor>();
    }
}
