using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FirstWeb
{
    [ApiController]
    [Route("[controller]")]
    public class FirstWebController:ControllerBase
    {
        private readonly IServiceScopeFactory _sp;
        //public FirstWebController(IService service) //不能用构造函数
        //{
        //    _service = service;
        //}
        //public FirstWebController()
        //{
        //    _service = new NameService();
        //}


        public FirstWebController(IServiceScopeFactory sp)
        {
            _sp = sp;
        }

        [HttpGet("first")]
        public IActionResult Get()
        {
            using var scope = ServiceLocator.Instance.CreateScope();
            var _service = scope.ServiceProvider.GetRequiredService<IService>(); //问题在这,监视scope发现是有注入的，但是获取不到，是空的,只能通过属性注入的方式来解决，最小api性能比较好，建议使用
            return Ok(_service.Name());
        }

        [HttpGet("second")]
        public IActionResult GetSecond([FromServices]TestInterface service) //问题同样
        {
        
            return Ok(service.TestMethod());
        }
    }
}
