using System;

namespace SCA.Shared.Entities.Monitoring
{
    public class Sensor
    {
        public int Id { get; set; }
        public Barreira Barreria { get; set; }
        public int BarreriaId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
