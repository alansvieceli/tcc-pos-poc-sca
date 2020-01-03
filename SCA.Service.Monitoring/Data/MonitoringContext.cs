using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Monitoring;

namespace SCA.Service.Monitoring.Data
{
    public class MonitoringContext : DbContext
    {
        public MonitoringContext(DbContextOptions<MonitoringContext> options) : base(options)
        {
        }

        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Barreira> Barreira { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<SensorHistorico> SensorHistorico { get; set; }
    }
}
