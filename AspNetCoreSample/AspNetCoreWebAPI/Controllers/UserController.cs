using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAUserInterface _aUser;
        private readonly IBUserInterface _bUser;
        
        public UserController(IAUserInterface aUser, IBUserInterface bUser)
        {
            _aUser = aUser;
            _bUser = bUser;
        }

        [HttpGet]
        [Route("User")]
        public string GetUser()
        {
            return this._aUser.HelloA() + this._bUser.HelloB();
        }
    }
}