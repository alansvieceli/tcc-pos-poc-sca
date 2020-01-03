using System;
using System.ComponentModel.DataAnnotations;
using SCA.Shared.Entities.Enums;

namespace SCA.Shared.Entities.Inputs
{
    public class Insumo
    {
        public int Id { get; set; }      
        public Marca Marca { get; set; }
        [Required(ErrorMessage = "Marca deve ser informada")]
        public int MarcaId { get; set; }       
        public Tipo Tipo { get; set; }
        [Required(ErrorMessage = "Tipo deve ser informado")]
        public int TipoId { get; set; }
        [Display(Name = "Situação")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public InsumosStatus Status { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Descricao { get; set; }
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public DateTime DataCadastro { get; set; }
    }
}
