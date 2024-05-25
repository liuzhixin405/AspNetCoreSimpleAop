using Business;
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
        // 在需要的时候动态解析服务
        //using var scope = ServiceLocator.Instance.CreateScope();
        //var animalService = scope.ServiceProvider.GetRequiredService<IAnimalService>();
        //return animalService.Speak();
        return "hello";
    }

    [HttpPost]
    public async Task<int> Add()
    {
        //using var scope = ServiceLocator.Instance.CreateScope();
        //var business = scope.ServiceProvider.GetRequiredService<IProductBusiness>();
        using var business = ProductBusiness.Instance;
        return await business.AddProduct("product1",12.1m);
    }
}