using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Inputs;
using SCA.Shared.Entities.Maintenance;

namespace SCA.Service.Maintenance.Data
{
    public class MaintenanceContext : DbContext
    {
        public MaintenanceContext(DbContextOptions<MaintenanceContext> options) : base(options)
        {
        }

        public DbSet<Insumo> Insumo { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<Manutencao> Manutencao { get; set; }
    }
}
