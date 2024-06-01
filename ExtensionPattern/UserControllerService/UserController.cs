using IDynamicServiceBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserControllerService;

namespace UserControllerService
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            _logger =scope.ServiceProvider.GetRequiredService<ILogger<UserController>>();
            _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        }

        [HttpGet]
        [Route("login")]
        public bool Login()
        {
            _userService.Log("登陆失败");
            return false;
        }

         //测试方法是先 生成完整的dll,拷贝一份到lib/bak目录下，再注释掉FullName的接口,再编译运行，可以看到控制器有两个接口。然后remove掉当前控制器，刷新。然后再把lib/bak拷贝到lib下，早add删除掉的控制器，再刷新看效果。

        //结论就是扩展接口可以替代反射注入服务层

        //[HttpGet]
        //[Route("fullname")]
        //public string FullName()
        //{
        //    return _userService.GetFullName();
        //}
    }
}
