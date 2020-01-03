using Microsoft.EntityFrameworkCore;
using SCA.Service.Inputs.Data;
using SCA.Shared.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Auth.Services
{
    public class UserService
    {
        private readonly UserContext _context;

        public UserService(UserContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            return await this._context.User.ToListAsync();
        }

        public IEnumerable<User> FindAll()
        {
            return this._context.User.ToList();
        }
    }
}
