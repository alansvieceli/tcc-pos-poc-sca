using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCA.Shared.Exceptions;
using SCA.Service.Maintenance.Data;

namespace SCA.Service.Maintenance.Services
{
    public class TipoService
    {

        private readonly MaintenanceContext _context;

        public TipoService(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipo>> FindAllAsync()
        {
            return await _context.Tipo.OrderBy(t => t.Descricao).ToListAsync();
        }


        public async Task<Tipo> FindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Tipo.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task InsertAsync(Tipo department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var department = await _context.Tipo.FindAsync(id);

            try
            {
                _context.Tipo.Remove(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Tipo department)
        {

            var hasAny = await _context.Tipo.AnyAsync(s => s.Id == department.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
