using Microsoft.EntityFrameworkCore;
using SCA.Service.Inputs.Data;
using SCA.Service.Inputs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCA.Shared.Exceptions;

namespace SCA.Service.Inputs.Services
{
    public class TipoService
    {

        private readonly InputsContext _context;

        public TipoService(InputsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipo>> FindAll()
        {
            return await _context.Tipo.OrderBy(t => t.Descricao).ToListAsync();
        }


        public async Task<Tipo>  FindById(int? id)
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
