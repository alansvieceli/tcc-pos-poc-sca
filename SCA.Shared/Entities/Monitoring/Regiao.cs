using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCA.Shared.Entities.Alert;

namespace SCA.Shared.Entities.Monitoring
{
    public class Regiao
    {
        public int Id { get; set; }
        [MaxLength(2)]
        public string UF { get; set; }
        public string Cidade { get; set; }
        public ICollection<Barragem> Barragens { get; set; } = new List<Barragem>();
        public ICollection<Cadastro> Alerts { get; set; } = new List<Cadastro>();

        public void AddBarragem(Barragem b)
        {
            Barragens.Add(b);
        }

        public override string ToString()
        {
            return $"{this.UF} - {this.Cidade}";
        }
    }
}
