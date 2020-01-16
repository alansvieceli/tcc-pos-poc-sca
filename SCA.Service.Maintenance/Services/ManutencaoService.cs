using Microsoft.EntityFrameworkCore;
using SCA.Service.Maintenance.Data;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Maintenance;
using SCA.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Maintenance.Services
{
    public class ManutencaoService
    {
        private readonly MaintenanceContext _context;

        public ManutencaoService(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manutencao>> FindAllAsync()
        {
            return await _context.Manutencao.Include(m => m.Insumo).OrderByDescending(m => m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Manutencao>> FindByStatusAsync(params ManutencaoStatus[] status)
        {
            return await _context.Manutencao.Where(m => status.Contains(m.Status)).Include(m => m.Insumo).OrderByDescending(m => m.Status).ToListAsync();
        }

        public async Task<Manutencao> FindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Manutencao.Include(m => m.Insumo).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Manutencao manutencao)
        {
            _context.Add(manutencao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var manutencao = await _context.Manutencao.FindAsync(id);

            try
            {
                _context.Manutencao.Remove(manutencao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Manutencao manutencao)
        {

            var hasAny = await _context.Manutencao.AnyAsync(s => s.Id == manutencao.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(manutencao);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
