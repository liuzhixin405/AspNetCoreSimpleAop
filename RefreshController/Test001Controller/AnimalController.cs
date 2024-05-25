using Business;
using Castle.DynamicProxy;
using Common;
using CommonFilter;
using IBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Test001Controller;

public class AnimalController : CusController
{

    private readonly IServiceProvider _serviceProvider;

    public AnimalController(IServiceProvider serviceProvider,ILogger<CusController> logger) : base(logger)
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

    [HttpGet]
    //[Log]
    public async Task<string> Add()
    {
        await base.AddBefore();
        logger.LogInformation($"AnimalController_Add,date:{DateTime.Now}");
        using var scope =_serviceProvider.CreateScope();
        //var business = scope.ServiceProvider.GetRequiredService<IProductBusiness>();

        //using var business = ProductBusiness.Instance;
        //await base.AddAfter();
        //return await business.AddProduct("product1", 12.1m);
    
       // var proxyGenerator = scope.ServiceProvider.GetRequiredService<ProxyGenerator>();  //注入有问题
        //var interceptor = scope.ServiceProvider.GetRequiredService<TransInterceptor>();

        var business = new ProxyGenerator().CreateClassProxy<OrderBusiness>(new TransInterceptor() );   //lib文件夹下面需要引用Castle.Core.dll
        await base.AddAfter();
        return await  business.GetOrder(12);
    }
}