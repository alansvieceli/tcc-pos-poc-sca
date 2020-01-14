using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Alert;
using SCA.Shared.Entities.Monitoring;

namespace SCA.Service.Alert.Data
{
    public class AlertContext : DbContext
    {
        public AlertContext(DbContextOptions<AlertContext> options) : base(options)
        {
        }

        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Cadastro> Cadastro { get; set; }
    }
}
