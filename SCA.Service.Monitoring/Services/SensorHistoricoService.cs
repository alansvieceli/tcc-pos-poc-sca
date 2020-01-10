using Microsoft.EntityFrameworkCore;
using SCA.Service.Monitoring.Data;
using SCA.Shared.Entities.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Monitoring.Services
{
    public class SensorHistoricoService
    {
        private readonly MonitoramentoContext _context;

        public SensorHistoricoService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<SensorHistorico> FindLastBySensorAsync(int? sensorId)
        {
            return await _context.SensorHistorico
                .OrderByDescending(x => x.Data)
                .FirstOrDefaultAsync(s => s.SensorId == sensorId);

        }

        public async Task InsertAsync(SensorHistorico historico)
        {
            _context.Add(historico);
            await _context.SaveChangesAsync();
        }
    }
}
