using Microsoft.EntityFrameworkCore;
using SCA.Service.Inputs.Data;
using SCA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCA.Shared.Exceptions;

namespace SCA.Service.Inputs.Services
{
    public class MarcaService
    {
        private readonly InputsContext _context;

        public MarcaService(InputsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> FindAllAsync()
        {
            return await _context.Marca.OrderBy(t => t.Descricao).ToListAsync();
        }

        public async Task<Marca> FindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Marca.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task InsertAsync(Marca department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var department = await _context.Marca.FindAsync(id);

            try
            {
                _context.Marca.Remove(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Marca department)
        {

            var hasAny = await _context.Marca.AnyAsync(s => s.Id == department.Id);
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
