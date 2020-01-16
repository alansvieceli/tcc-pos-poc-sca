using System.Collections.Generic;

namespace SCA.Shared.Entities.Inputs
{
    public class Marca
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();
    }
}
