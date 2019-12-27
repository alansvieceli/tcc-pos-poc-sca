using SCA.Web.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Web.Services
{
    public class InsumosService
    {
        public async Task<List<InsumosDto>> FindAllAsync()
        {
            //return await _context.Department.OrderBy(x => x.Name).ToListAsync();
            return new List<InsumosDto>();
        }

    }

}
