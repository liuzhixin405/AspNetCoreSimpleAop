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
       
        private readonly IOrderBusiness orderBusiness;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRootServiceFactory<IWeatherForecastService> cusServiceFactory, IOrderBusiness orderBusiness)
        {
            _logger = logger;
            _cusServiceFactory = cusServiceFactory;
            _cusServiceFactory.AddAop(new DefaultAOP())
                .AddAop(new LogAop());
            this.orderBusiness = orderBusiness;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _cusServiceFactory.Invoke<IEnumerable<WeatherForecast>>(nameof(IWeatherForecastService.Get),null); //�첽
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
            return _cusServiceFactory.Invoke(nameof(IWeatherForecastService.GetEmpty), null);    //ͬ��
        }
        [HttpGet("Order")]
        public string Order()
        {
            return orderBusiness.Get("hello");
        }
    }
}