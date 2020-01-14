using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCA.Shared.Entities.Alert;
using Microsoft.EntityFrameworkCore;
using SCA.Service.Alert.Data;

namespace SCA.Service.Alert.Services
{
    public class CadastroService
    {
        private readonly AlertContext _context;

        public CadastroService(AlertContext context)
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

        public async Task<IEnumerable<Cadastro>> FindByRegionIdAsync(int? id)
        {
            return await _context.Cadastro
                .Where(c => c.RegiaoId == id)
                .ToListAsync();
        }
    }
}
