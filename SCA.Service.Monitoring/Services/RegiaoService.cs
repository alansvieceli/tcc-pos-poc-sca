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
    public class RegiaoService        
    {
        private readonly MonitoramentoContext _context;

        public RegiaoService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Regiao>> FindAllAsync()
        {
            return await _context.Regiao
                .Include(r => r.Barragens)
                    .ThenInclude(b => b.Sensores)
                        .ThenInclude(s => s.Historico)
                .OrderBy(t => t.UF)
                .ToListAsync();
        }

        public async Task<IEnumerable<Regiao>> SimpleFindAllAsync()
        {
            return await _context.Regiao.OrderBy(r => r.UF).ToListAsync();
        }

        public async Task<Regiao> FindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Regiao.Include(i => i.Barragens).ThenInclude(b => b.Sensores).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Regiao>  CompleteFindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Regiao.Include(i => i.Barragens).ThenInclude(b => b.Sensores).ThenInclude(s => s.Historico).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Regiao regiao)
        {
            _context.Add(regiao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var regiao = await _context.Regiao.FindAsync(id);

            try
            {
                _context.Regiao.Remove(regiao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Regiao regiao)
        {

            var hasAny = await _context.Regiao.AnyAsync(s => s.Id == regiao.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(regiao);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
