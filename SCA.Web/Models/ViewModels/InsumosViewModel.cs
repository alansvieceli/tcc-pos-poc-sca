using SCA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Web.Models.ViewModels
{
    public class InsumosViewModel
    {
        public Insumo Insumo { get; set; }
        public IEnumerable<Marca> Marcas { get; set; }
        public IEnumerable<Tipo> Tipos { get; set; }
    }
}
