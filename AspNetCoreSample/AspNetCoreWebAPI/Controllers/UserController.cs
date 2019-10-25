using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Options;
using AspNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
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
        private readonly IOptionsMonitor<MySubOptions> _subOptionAccessor;
        private readonly IOptionsSnapshot<MyOptions> _snapshotOptionAccessor;
        private readonly IOptionsSnapshot<MyOptions> _namedOptionsAccessor;

        public UserController(IOptionsMonitor<MyOptions> optionAccessor, IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig, IOptionsMonitor<MySubOptions> subOptionAccessor, IOptionsSnapshot<MyOptions> snapshotOptionAccessor, IOptionsSnapshot<MyOptions> namedOptionsAccessor)
        {
            _optionAccessor = optionAccessor;
            _optionsAccessorWithDelegateConfig = optionsAccessorWithDelegateConfig;
            _subOptionAccessor = subOptionAccessor;
            _snapshotOptionAccessor = snapshotOptionAccessor;
            _namedOptionsAccessor = namedOptionsAccessor;
        }

        [HttpGet]
        [Route("User")]
        public ActionResult<MyOptions> GetUser()
        {
            var option = _namedOptionsAccessor.Get("named_option_2");
            return option;
        }
    }
}