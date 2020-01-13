using SCA.Shared.Entities.Monitoring;
using System.ComponentModel.DataAnnotations;

namespace SCA.Shared.Entities.Alert
{
    public class Cadastro
    {
        public int Id { get; set; }
        public Regiao Regiao { get; set; }
        [Required(ErrorMessage = "{0} deve ser informado")]
        public int RegiaoId { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Telefone { get; set; }
    }
}
