using Microsoft.EntityFrameworkCore;
using SCA.Service.Inputs.Data;
using SCA.Shared.Entities.Inputs;
using SCA.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Inputs.Services
{
    public class InsumoService
    {
        private readonly InputsContext _context;

        public InsumoService(InputsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Insumo>> FindAllAsync()
        {
            return await _context.Insumo.Include(i => i.Marca).Include(i => i.Tipo).OrderBy(t => t.Descricao).ToListAsync();
        }


        public async Task<Insumo> FindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Insumo.Include(i => i.Marca).Include(i => i.Tipo).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Insumo insumo)
        {
            _context.Add(insumo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var insumo = await _context.Insumo.FindAsync(id);

            try
            {
                _context.Insumo.Remove(insumo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Insumo insumo)
        {

            var hasAny = await _context.Insumo.AnyAsync(s => s.Id == insumo.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(insumo);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }

}
