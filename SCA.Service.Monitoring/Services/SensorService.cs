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
    public class SensorService        
    {
        private readonly MonitoramentoContext _context;

        public SensorService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sensor>> FindAllAsync()
        {
            return await _context.Sensor
                .Include(s => s.Historico)
                .OrderBy(s => s.DataCadastro)
                .ToListAsync();
        }

        public async Task<Sensor> FindByIdAsync(int? id)
        {
            return (id == null) ? null : await _context.Sensor.Include(s => s.Historico).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task InsertAsync(Sensor sensor)
        {
            _context.Add(sensor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var sensor = await _context.Sensor.FindAsync(id);

            try
            {
                _context.Sensor.Remove(sensor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Sensor sensor)
        {

            var hasAny = await _context.Sensor.AnyAsync(s => s.Id == sensor.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(sensor);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
