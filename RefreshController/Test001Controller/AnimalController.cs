using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Test001Controller;
[ApiController]
[Route("[controller]/[action]")]
public class AnimalController : ControllerBase
{
    public AnimalController()
    {

    }

    [HttpGet]
    public int GetAge(string name)
    {
        return 1001;
    }
}