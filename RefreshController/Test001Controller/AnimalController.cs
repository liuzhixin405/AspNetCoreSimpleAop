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
        // ����Ҫ��ʱ��̬��������
        var animalService = ServiceLocator.Instance.GetRequiredService<IAnimalService>();
        return animalService.Speak();
    }
}