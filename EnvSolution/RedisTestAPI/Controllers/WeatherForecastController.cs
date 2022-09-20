using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEnvService _envService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEnvService envService)
        {
            _logger = logger;
            _envService = envService;
        }

        [HttpGet("{key}")]
        public async Task<string> Get(string key)
        {
            var returnModel= await _envService.GetValue<string>(key);
            return returnModel;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Post()
        {
            bool result = await _envService.SaveOrUpdate(new RedisClassLibrary.Models.AddEnvModel { Name="SiteName",Value="eraybakir.com",IsActive=1, Type="string" });
            return Ok(result);
        }
    }
}
