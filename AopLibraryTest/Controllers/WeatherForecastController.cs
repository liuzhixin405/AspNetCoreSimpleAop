using AopLibrary;
using AopLibrary.CusImplement;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Xml.Linq;

namespace AopLibraryTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRootServiceFactory<IWeatherForecastService> _cusServiceFactory;
       
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRootServiceFactory<IWeatherForecastService> cusServiceFactory)
        {
            _logger = logger;
            _cusServiceFactory = cusServiceFactory;
            _cusServiceFactory.AddAop(new DefaultAOP())
                .AddAop(new LogAop());

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _cusServiceFactory.Invoke<IEnumerable<WeatherForecast>>(nameof(IWeatherForecastService.Get),null); //“Ï≤Ω
        }

        [HttpGet("greeting")]
        public async Task<string> GreetingAsync(string name)
        {
            var result =await _cusServiceFactory.Invoke<string>(nameof(IWeatherForecastService.GreetingAsync), new object[] { name });
            return result;
        }

        [HttpGet("empty")]
        public Task GetEmpty()
        {
            return _cusServiceFactory.Invoke(nameof(IWeatherForecastService.GetEmpty), null);    //Õ¨≤Ω
        }
    }
}