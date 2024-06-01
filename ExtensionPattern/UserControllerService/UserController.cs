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
            _userService.Log("��½ʧ��");
            return false;
        }

         //���Է������� ����������dll,����һ�ݵ�lib/bakĿ¼�£���ע�͵�FullName�Ľӿ�,�ٱ������У����Կ����������������ӿڡ�Ȼ��remove����ǰ��������ˢ�¡�Ȼ���ٰ�lib/bak������lib�£���addɾ�����Ŀ���������ˢ�¿�Ч����

        //���۾�����չ�ӿڿ����������ע������

        //[HttpGet]
        //[Route("fullname")]
        //public string FullName()
        //{
        //    return _userService.GetFullName();
        //}
    }
}
