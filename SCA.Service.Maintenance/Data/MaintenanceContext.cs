using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Maintenance;

namespace SCA.Service.Maintenance.Data
{
    public class MaintenanceContext : DbContext
    {
        public MaintenanceContext(DbContextOptions<MaintenanceContext> options) : base(options)
        {
        }

        public DbSet<Manutencao> Manutencao { get; set; }
    }
}
