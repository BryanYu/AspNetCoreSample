using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EnvironmentController : Controller
    {
        private readonly IHostEnvironment _host;

        public EnvironmentController(IHostEnvironment host)
        {
            _host = host;
        }
        // GET api/<controller>/5
        [HttpGet("")]
        public string Get()
        {
            return this._host.EnvironmentName;
        }
    }
}
