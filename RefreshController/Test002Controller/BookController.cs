using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Test001Controller;
[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{
    public BookController()
    {
        //业务可以通过反射dll做一个解耦，其他项目有示例，整个项目就串起来了
    }

    [HttpGet]
    public string Show()
    {
        return "叽叽咕唧";
    }
}