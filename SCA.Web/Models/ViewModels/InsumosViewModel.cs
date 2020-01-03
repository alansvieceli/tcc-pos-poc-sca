using SCA.Shared.Entities.Inputs;
using System.Collections.Generic;

namespace SCA.Web.Models.ViewModels
{
    public class InsumosViewModel
    {
        public Insumo Insumo { get; set; }
        public IEnumerable<Marca> Marcas { get; set; }
        public IEnumerable<Tipo> Tipos { get; set; }
    }
}
