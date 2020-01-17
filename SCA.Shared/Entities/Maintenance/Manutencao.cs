using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Inputs;
using System;
using System.ComponentModel.DataAnnotations;

namespace SCA.Shared.Entities.Maintenance
{
    public class Manutencao
    {
        public int Id { get; set; }

        [Display(Name = "Insumo")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public int InsumoId { get; set; }

        public string InsumoDesc { get; set; }

        [Display(Name = "Descrição Agendamento")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string DescricaoAgendamento { get; set; }

        [Display(Name = "Dt.Agendamento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public DateTime DataAgendamento { get; set; }

        [Display(Name = "Previsão Manutenção")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public DateTime PrevisaoManutencao { get; set; }

        [Display(Name = "Tipo de  Manutenção")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public ManutencaoTipo Tipo { get; set; }

        [Display(Name = "Início Manutenção")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataInicioManutencao { get; set; }

        [Display(Name = "Fim Manutenção")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataFimManutencao { get; set; }

        [Display(Name = "Descrição Manutenção")]
        public string DescricaoManutencao { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado")]
        public ManutencaoStatus Status { get; set; }
    }
}
