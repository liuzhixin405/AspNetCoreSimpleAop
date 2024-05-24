using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Test001Controller;
[ApiController]
[Route("[controller]/[action]")]
public class AnimalController : ControllerBase
{

    private readonly IServiceProvider _serviceProvider;
    public AnimalController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public int GetAge(string name)
    {

        return 1001;
    }
    [HttpGet]
    public string Speak()
    {
        // 在需要的时候动态解析服务
        var animalService = ServiceLocator.Instance.GetRequiredService<IAnimalService>();
        return animalService.Speak();
    }
}