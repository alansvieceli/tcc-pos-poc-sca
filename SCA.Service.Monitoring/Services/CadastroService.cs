using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCA.Service.Monitoring.Data;
using SCA.Shared.Entities.Alert;
using Microsoft.EntityFrameworkCore;

namespace SCA.Service.Monitoring.Services
{
    public class CadastroService
    {
        private readonly MonitoramentoContext _context;

        public CadastroService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<Cadastro> FindByIdAsync(int? id)
        {
            return (id == null) ?
                null :
                await _context.Cadastro
                        .Include(b => b.Regiao)
                        .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task InsertAsync(Cadastro cadastro)
        {
            _context.Add(cadastro);
            await _context.SaveChangesAsync();
        }
    }
}
