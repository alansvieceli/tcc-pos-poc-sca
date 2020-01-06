using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCA.Shared.Entities.Monitoring
{
    public class Regiao
    {
        public int Id { get; set; }
        [MaxLength(2)]
        public string UF { get; set; }
        public string Cidade { get; set; }
        public ICollection<Barragem> Barragens { get; set; } = new List<Barragem>();

        public void AddBarragem(Barragem b)
        {
            Barragens.Add(b);
        }
    }
}
