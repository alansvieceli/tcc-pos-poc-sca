using SCA.Shared.Entities.Auth;
using SCA.Shared.Entities.Enums;
using System.Linq;

namespace SCA.Service.Inputs.Data
{
    public class SeedingService
    {
        private UserContext _context;

        public SeedingService(UserContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            //se tiver alguma coisa no banco, sai fora
            if ( _context.User.Any() )
            {
                return; // banco já foi populado
            }

            User u1 = new User { Id = 1, UserId = "admin@company.com", Email = "admin@company.com", FirtName = "Alberto", LastName = "Roberto", Password = "admin", AcessLevel = Role.ADMIN };
            User u2 = new User { Id = 2, UserId = "monitor@company.com", Email = "monitor@company.com", FirtName = "Vanessa", LastName = "Roberts", Password = "monitor", AcessLevel = Role.MONITOR };
            User u3 = new User { Id = 3, UserId = "user@company.com", Email = "user@company.com", FirtName = "João", LastName = "da Silva", Password = "user", AcessLevel = Role.USER_COMMON };
            
            _context.User.AddRange(u1, u2, u3);

            _context.SaveChanges();
        }
    }
}
