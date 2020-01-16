using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Maintenance;
using System.Collections.Generic;

namespace SCA.Web.Models.ViewModels
{
    public class ManutencaoViewModel
    {
        public Manutencao Manutencao { get; set; }
        public IEnumerable<Insumo> Insumos { get; set; }
    }
}
