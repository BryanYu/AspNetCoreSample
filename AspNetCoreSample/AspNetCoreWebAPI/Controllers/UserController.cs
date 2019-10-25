using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Options;
using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptionsMonitor<MyOptions> _optionAccessor;
        private readonly IOptionsMonitor<MyOptionsWithDelegateConfig> _optionsAccessorWithDelegateConfig;


        public UserController(IOptionsMonitor<MyOptions> optionAccessor, IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig)
        {
            _optionAccessor = optionAccessor;
            _optionsAccessorWithDelegateConfig = optionsAccessorWithDelegateConfig;
        }

        [HttpGet]
        [Route("User")]
        public ActionResult<MyOptionsWithDelegateConfig> GetUser()
        {
            var option = this._optionsAccessorWithDelegateConfig.CurrentValue;
            return option;
        }
    }
}