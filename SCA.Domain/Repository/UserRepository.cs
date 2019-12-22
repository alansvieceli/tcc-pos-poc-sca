using SCA.Domain.Model;
using SCA.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Domain.Repository
{
    public class  UserRepository
    {

        private static List<User> UserList = new List<User>
{
            new User { USERID = "jsmith@email.com", PASSWORD = "test", FIRST_NAME = "John", LAST_NAME = "Smith", EMAILID = "jsmith@email.com", ACCESS_LEVEL = Roles.DIRECTOR.ToString()},
            new User { USERID = "srob@email.com", PASSWORD = "test", FIRST_NAME = "Steve", LAST_NAME = "Rob", EMAILID = "srob@email.com", ACCESS_LEVEL = Roles.SUPERVISOR.ToString()},
            new User { USERID = "dwill@email.com", PASSWORD = "test", FIRST_NAME = "DJ", LAST_NAME = "Will", EMAILID = "dwill@email.com", ACCESS_LEVEL = Roles.ANALYST.ToString()},
            new User { USERID = "JBlack@email.com", PASSWORD = "test", FIRST_NAME = "Joe", LAST_NAME = "Black", EMAILID = "JBlack@email.com", ACCESS_LEVEL = Roles.ANALYST.ToString()}
        };

        public static IEnumerable<User> GetUsers()
        {
            return UserList;
        }

    }
}
