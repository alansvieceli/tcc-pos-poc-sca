using SCA.Shared.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Web.Dto
{
    public class InsumosDto
    {
        public int Id { get; set; }
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public int MarcaId { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public int TipoId { get; set; }
        [Display(Name = "Situação")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public InsumosStatus Status { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Descricao { get; set; }
        [Display(Name = "Data de Aquisição")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public DateTime DataAquisicao { get; set; }
    }
}
