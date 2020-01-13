using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Alert;
using SCA.Shared.Entities.Monitoring;

namespace SCA.Service.Monitoring.Data
{
    public class MonitoramentoContext : DbContext
    {
        public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options) : base(options)
        {
        }

        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Barragem> Barragem { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<SensorHistorico> SensorHistorico { get; set; }
        public DbSet<Cadastro> Cadastro { get; set; }
    }
}
