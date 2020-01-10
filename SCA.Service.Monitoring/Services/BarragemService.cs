using Microsoft.EntityFrameworkCore;
using SCA.Service.Monitoring.Data;
using SCA.Shared.Entities.Monitoring;
using SCA.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Monitoring.Services
{
    public class BarragemService
    {
        private readonly MonitoramentoContext _context;

        public BarragemService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Barragem>> FindAllAsync()
        {
            return await _context.Barragem
                .Include(b => b.Regiao)
                .Include(b => b.Sensores)
                    .ThenInclude(s => s.Historico)
                .OrderBy(b => b.Id)                    
                .ToListAsync();
        }

        public async Task<Barragem> FindByIdAsync(int? id)
        {
            return (id == null) ? 
                null : 
                await _context.Barragem
                        .Include(b => b.Regiao)
                        .Include(b => b.Sensores)
                            .ThenInclude(s => s.Historico)
                        .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task InsertAsync(Barragem barragem)
        {
            _context.Add(barragem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var barragem = await _context.Barragem.FindAsync(id);

            try
            {
                _context.Barragem.Remove(barragem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Barragem barragem)
        {

            var hasAny = await _context.Barragem.AnyAsync(s => s.Id == barragem.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(barragem);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
