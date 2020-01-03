using SCA.Shared.Entities.Enums;
using System;

namespace SCA.Shared.Entities.Monitoring
{
    public class SensorHistorico
    {
        public int Id { get; set; }
        public Sensor Sensor { get; set; }
        public int SensorId { get; set; }
        public SensorStatus Status { get; set; }
        public DateTime Data { get; set; }
    }
}
