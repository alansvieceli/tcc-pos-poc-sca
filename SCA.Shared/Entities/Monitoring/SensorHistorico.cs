using SCA.Shared.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SCA.Shared.Entities.Monitoring
{
    public class SensorHistorico
    {
        public int Id { get; set; }
        public Sensor Sensor { get; set; }
        [Display(Name = "Sensor")]
        public int SensorId { get; set; }
        public SensorStatus Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        [Display(Name = "Data de Recebimento")]
        public DateTime Data { get; set; }
    }
}
