using BigHost;
using BigHost.AssemblyExtensions;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using ModuleLib;
using System.Xml.Linq;
using DependencyInjectionAttribute;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.Modules.json", optional: false, reloadOnChange: true);
//builder.Services.InitModule(builder.Configuration);
//var sp = builder.Services.BuildServiceProvider();
//var modules = sp.GetServices<IModule>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//最新dotnet没有这些
builder.Services.AddControllers().ConfigureApplicationPartManager(apm =>
{
    var context = new CollectibleAssemblyLoadContext();
    DirectoryInfo DirInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "lib"));
    foreach (var file in DirInfo.GetFiles("*.dll"))
    {
        //if(!(file.Name.Contains("Test001Controller") || file.Name.Contains("Test002Controller")))
        //{
        //    continue;
        //}//不能屏蔽掉依赖引用
        var assembly = context.LoadFromAssemblyPath(file.FullName);
        var controllerAssemblyPart = new AssemblyPart(assembly);
        apm.ApplicationParts.Add(controllerAssemblyPart);
        ExternalContexts.Add(file.Name, context);
    }
});
    //builder.Services.AddTransient<IProductBusiness, ProductBusiness>();
    //foreach (var module in modules)
    //{
    //    module.ConfigureService(builder.Services, builder.Configuration);
    //}
    //GolbalConfiguration.Modules.Select(x => x.Assembly).ToList().ForEach(x =>
    //{
    //    builder.Services.ReisterServiceFromAssembly(x);
    //    var controllerAssemblyPart = new AssemblyPart(x);
    //    apm.ApplicationParts.Add(controllerAssemblyPart);
    //    ExternalContexts.Add(x.GetName().Name, context);
    //});
//});
//GolbalConfiguration.Modules.Select(x => x.Assembly).ToList().ForEach(x => builder.Services.ReisterServiceFromAssembly(x));
builder.Services.AddSingleton<IActionDescriptorChangeProvider>(ActionDescriptorChangeProvider.Instance);
builder.Services.AddSingleton(ActionDescriptorChangeProvider.Instance);



var app = builder.Build();
ServiceLocator.Instance = app.Services;
//foreach (var module in modules)
//{
//    module.Configure(app, app.Environment);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.MapGet("/Add", ([FromServices] ApplicationPartManager _partManager, string name) =>
{

    FileInfo FileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "lib/" + name + ".dll"));
    using (FileStream fs = new FileStream(FileInfo.FullName, FileMode.Open))
    {
        var context = new CollectibleAssemblyLoadContext();
        var assembly = context.LoadFromStream(fs);
        var controllerAssemblyPart = new AssemblyPart(assembly);

        _partManager.ApplicationParts.Add(controllerAssemblyPart);

        //ExternalContexts.Add(name + ".dll", context);
        ExternalContexts.Add(name, context);

        //更新Controllers
        ActionDescriptorChangeProvider.Instance.HasChanged = true;
        ActionDescriptorChangeProvider.Instance.TokenSource!.Cancel();
    }
    return "添加{name}controller成功";
})
.WithTags("Main")
.WithOpenApi();

app.MapGet("/Remove", ([FromServices] ApplicationPartManager _partManager, string name) =>
{
    //if (ExternalContexts.Any(
    //    $"{name}.dll"))
    if (ExternalContexts.Any(
   $"{name}"))
    {
        var matcheditem = _partManager.ApplicationParts.FirstOrDefault(x => x.Name == name);
        if (matcheditem != null)
        {
            _partManager.ApplicationParts.Remove(matcheditem);
            matcheditem = null;
        }
        ActionDescriptorChangeProvider.Instance.HasChanged = true;
        ActionDescriptorChangeProvider.Instance.TokenSource!.Cancel();
        //ExternalContexts.Remove(name + ".dll");
        ExternalContexts.Remove(name);
        return $"成功移除{name}controller";
    }
    else
    {
        return "$没有{name}controller";
    }
});
app.UseRouting(); //最新dotnet没有这些
app.MapControllers();  //最新dotnet没有这些
app.Run();