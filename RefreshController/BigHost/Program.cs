using BigHost.AssemblyExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//最新dotnet没有这些
builder.Services.AddControllers().ConfigureApplicationPartManager(apm =>
{
    var context = new CollectibleAssemblyLoadContext();
    DirectoryInfo DirInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "lib"));
    FileInfo[] lib = DirInfo.GetFiles();
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

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
