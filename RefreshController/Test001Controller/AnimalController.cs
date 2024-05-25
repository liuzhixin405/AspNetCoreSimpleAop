using Common;
using IBusiness;
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
        using var scope = ServiceLocator.Instance.CreateScope();
        var animalService = scope.ServiceProvider.GetRequiredService<IAnimalService>();
        return animalService.Speak();
    }

    [HttpPost]
    public async Task<int> Add()
    {
        using var scope = ServiceLocator.Instance.CreateScope();
        var business = scope.ServiceProvider.GetRequiredService<IProductBusiness>();
        return await business.AddProduct("product1",12.1m);
    }
}