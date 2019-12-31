using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SCA.Shared.CustomAttributes;
using SCA.Shared.Entities;
using SCA.Shared.Entities.Enums;

namespace SCA.Service.Inputs.Controllers
{
    [UnAuthorized]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {

        [Authorize(Role.ADMIN)]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new List<User>();
        }
    }
}
