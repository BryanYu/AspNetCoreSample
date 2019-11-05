using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSwaggerSample.Controllers
{
    /// <summary>
    /// 天氣控制器
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 取得天氣資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Test1), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Test2),StatusCodes.Status201Created)]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///     POST /Todo
        ///     {
        ///         "id":1,
        ///         "name":"Item1",
        ///         "isComplete":true
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">編號</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            return NoContent();
        }
    }

    /// <summary>
    /// 測試1
    /// </summary>
    public class Test1
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 測試2
    /// </summary>
    public class Test2
    {
        /// <summary>
        /// 手機
        /// </summary>
        public string Phone { get; set; }
    }
}
