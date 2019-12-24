using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Model;
using SCA.Shared.Model.Enums;
using SCA.Shared.Repository;

namespace SCA.Service.Inputs.Controllers
{
    [UnAuthorized]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {

        [Authorize(Roles.DIRECTOR)]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UserRepository.GetUsers();
        }
    }
}
