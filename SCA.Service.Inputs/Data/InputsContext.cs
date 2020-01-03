using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Inputs.Data
{
    public class InputsContext : DbContext
    {
        public InputsContext(DbContextOptions<InputsContext> options) : base(options)
        {
        }

        public DbSet<Insumo> Insumo { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
    }
}
