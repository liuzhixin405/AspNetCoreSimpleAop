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
builder.Services.InitModule(builder.Configuration);
var sp = builder.Services.BuildServiceProvider();

var modules =sp.GetServices<IModule>();
foreach (var module in modules)
{
    module.ConfigureService(builder.Services, builder.Configuration);
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//����dotnetû����Щ
builder.Services.AddControllers().ConfigureApplicationPartManager(apm =>
{
    GolbalConfiguration.Modules.Select(x => x.Assembly).ToList().ForEach(x => builder.Services.ReisterServiceFromAssembly(x));
    var context = new CollectibleAssemblyLoadContext();

    DirectoryInfo DirInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "lib"));

    FileInfo[] lib = DirInfo.GetFiles().Where(x => x.Name.EndsWith("Test001Controller.dll") || x.Name.EndsWith("Test002Controller.dll")).ToArray();
    foreach (FileInfo fileInfo in lib)
    {
        using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open))
        {
            var assembly = context.LoadFromStream(fs);
            var controllerAssemblyPart = new AssemblyPart(assembly);
            apm.ApplicationParts.Add(controllerAssemblyPart);
            ExternalContexts.Add(fileInfo.Name, context);
        }
    }
});

builder.Services.AddSingleton<IActionDescriptorChangeProvider>(ActionDescriptorChangeProvider.Instance);
builder.Services.AddSingleton(ActionDescriptorChangeProvider.Instance);



var app = builder.Build();
ServiceLocator.Instance = app.Services;
foreach (var module in modules)
{
    module.Configure(app, app.Environment);
}

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

        ExternalContexts.Add(name + ".dll", context);


        //����Controllers
        ActionDescriptorChangeProvider.Instance.HasChanged = true;
        ActionDescriptorChangeProvider.Instance.TokenSource!.Cancel();
    }
    return "���{name}controller�ɹ�";
})
.WithTags("Main")
.WithOpenApi();

app.MapGet("/Remove", ([FromServices] ApplicationPartManager _partManager, string name) =>
{
    if (ExternalContexts.Any(
        $"{name}.dll"))
    {
        var matcheditem = _partManager.ApplicationParts.FirstOrDefault(x => x.Name == name);
        if (matcheditem != null)
        {
            _partManager.ApplicationParts.Remove(matcheditem);
            matcheditem = null;
        }
        ActionDescriptorChangeProvider.Instance.HasChanged = true;
        ActionDescriptorChangeProvider.Instance.TokenSource!.Cancel();
        ExternalContexts.Remove(name + ".dll");
        return $"�ɹ��Ƴ�{name}controller";
    }
    else
    {
        return "$û��{name}controller";
    }
});
app.UseRouting(); //����dotnetû����Щ
app.MapControllers();  //����dotnetû����Щ
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
