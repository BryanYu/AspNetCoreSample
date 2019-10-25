using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Options;
using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAUserInterface _aUser;
        private readonly IBUserInterface _bUser;
        private readonly IConfiguration _config;

        public UserController(IAUserInterface aUser, IBUserInterface bUser, IConfiguration config)
        {
            _aUser = aUser;
            _bUser = bUser;
            _config = config;
        }

        [HttpGet]
        [Route("User")]
        public ActionResult<StarShip> GetUser()
        {
            var starShip = new StarShip();
            _config.GetSection("starShip").Bind(starShip);
            return starShip;
        }
    }
}