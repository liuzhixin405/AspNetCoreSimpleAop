using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;
using ServiceDescriptorsFactory;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Contract;

// Use a Hosting Startup Attribute to identify the IHostingStartup implementation.
[assembly: HostingStartup(typeof(StartupDiagnostics.StartupDiagnosticsHostingStartup))]

namespace StartupDiagnostics
{
    /// <summary>
    /// Class used to configure the app with the hosting startup enhancements.
    /// </summary>
    public class StartupDiagnosticsHostingStartup : IHostingStartup
    {
        /// <summary>
        /// Configure the IWebHostBuilder with the hosting startup enhancements.
        /// </summary>
        public void Configure(IWebHostBuilder builder)
        {
            Console.WriteLine("StartupDiagnostics.StartupDiagnosticsHostingStartup");

            builder.ConfigureServices(services =>
            {
             
                // Create a factory with a GetServices method that can
                // be called in middleware to obtain a list of the app's
                // services.
                Func<IServiceProvider, IServiceDescriptorsService> factory = 
                    provider => new ServiceDescriptorsService(services);

                // Register the factory in the service container.
                services.AddSingleton(factory);
                
                // Implement a startup filter that is used to register 
                // two middleware components.
                services.AddSingleton<IStartupFilter, DiagnosticMiddlewareStartupFilter>();

                services.Initialize();
                services.AddSingleton<IStartupFilter, MyControllerFilter>();
                services.AddSingleton(typeof(PluginManager));
            })
            //    .ConfigureKestrel(options =>
            //{
            //    options.Listen(new System.Net.IPAddress() , 5008);
            //})
                ;
        }
    }

    public class MyControllerFilter : IStartupFilter
    {
        private readonly PluginManager pluginManager;
        List<string> controllers = new List<string> { "First","Second" };
        public MyControllerFilter(PluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
            controllers.ForEach(x => pluginManager.LoadPlugins($"{Directory.GetCurrentDirectory()}\\lib\\", $"{x}.Impl.dll"));
        }
        Action<IApplicationBuilder> IStartupFilter.Configure(Action<IApplicationBuilder> next)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            return app =>
            {

                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    foreach (IPlugin item in pluginManager.GetPlugins())
                    {
                        foreach (MethodInfo mi in item.GetType().GetMethods(bindingFlags))
                        {
                            endpoints.MapPost($"/{item.GetType().Name.Replace("Service", "")}/{mi.Name}", async (string parameters, HttpContext cotext) =>
                            {

                                var task = (Task)mi.Invoke(item, new object[] { parameters });
                                if (task is Task apiTask)
                                {
                                    await apiTask;

                                    // ��������з��ؽ��
                                    if (apiTask is Task<object> resultTask)
                                    {
                                        var res = await resultTask;
                                        return Results.Ok(JsonConvert.SerializeObject(res));
                                    }
                                }

                                // �������û�з��� Task<ApiResult>������ NotFound
                                return Results.NotFound("Method execution did not return a result.");
                            });
                        }
                    }
                });
                next(app);
            };
        }
    }
    /// <summary>
    /// Startup filter for diagnostic middleware.
    /// </summary>
    public class DiagnosticMiddlewareStartupFilter : IStartupFilter
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Construct diagnostic middleware startup filter.
        /// </summary>
        public DiagnosticMiddlewareStartupFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Configure diagnostic middleware.
        /// </summary>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseRouting();

                // Use a terminal middleware that branches on a request for
                // /services. The middleware uses a factory to obtain the services
                // registered for the app and outputs them in a webpage.
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/services", async context =>
                    {
                        // Make sure this only runs in the Development environment.
                        if (!_env.IsDevelopment())
                        {
                            return;
                        }

                        var sb = new StringBuilder(@"
                            <!DOCTYPE html><html lang=""en""><head><title>All Services</title>
                            <style>body{font-family:Verdana,Geneva,sans-serif;font-size:.8em}
                            li{padding-bottom:10px}</style></head><body>
                            <h1>All Services</h1>
                            <ul>");

                        var serviceDescriptorService = 
                            context.RequestServices.GetService<IServiceDescriptorsService>();

                        foreach(var service in serviceDescriptorService.GetServices())
                        {
                            sb.Append($"<li><b>{service.FullName}</b> ({service.Lifetime})");
                            if (!string.IsNullOrEmpty(service.ImplementationType))
                            {
                                sb.Append($"<br>{service.ImplementationType}</li>");
                                
                            }
                            else
                            {
                                sb.Append($"</li>");
                            }
                            
                        }

                        sb.Append("</ul></body></html>");

                        await context.Response.WriteAsync(sb.ToString());
                    });
                });

                app.UseMiddleware<DiagnosticMiddleware>();

                next(app);
            };
        }
    }

    /// <summary>
    /// A middleware to write out diagnostic information from the app.
    /// </summary>
    public class DiagnosticMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IWebHostEnvironment _env;
        private string cr = Environment.NewLine;

        /// <summary>
        /// Construct the diagnostic middleware.
        /// </summary>
        public DiagnosticMiddleware(RequestDelegate next, 
                                    ILoggerFactory loggerFactory,
                                    IWebHostEnvironment env)
        {
            _next = next;
            _loggerFactory = loggerFactory;
            _env = env;
        }

        /// <summary>
        /// Invoke the diagnostic middleware.
        /// </summary>
        public async Task Invoke(HttpContext ctx)
        {
            var path = ctx.Request.Path;

            // Make sure this only runs at the /diag endpoint in the Development environment.
            if (path != "/diag" || !_env.IsDevelopment())
            {
                await _next(ctx);
            }
            else
            {
                var logger = _loggerFactory.CreateLogger("Requests");

                logger.LogDebug("Received request: {Method} {Path}", 
                    ctx.Request.Method, ctx.Request.Path);

                ctx.Response.ContentType = "text/plain";

                var sb = new StringBuilder();
                sb.Append($"{DateTimeOffset.Now}{cr}{cr}");
                sb.Append($"Address:{cr}{cr}");
                sb.Append($"Scheme: {ctx.Request.Scheme}{cr}");
                sb.Append($"Host: {ctx.Request.Headers["Host"]}{cr}");
                sb.Append($"PathBase: {ctx.Request.PathBase.Value}{cr}");
                sb.Append($"Path: {ctx.Request.Path.Value}{cr}");
                sb.Append($"Query: {ctx.Request.QueryString.Value}{cr}{cr}");
                sb.Append($"Connection:{cr}{cr}");
                sb.Append($"RemoteIp: {ctx.Connection.RemoteIpAddress}{cr}");
                sb.Append($"RemotePort: {ctx.Connection.RemotePort}{cr}");
                sb.Append($"LocalIp: {ctx.Connection.LocalIpAddress}{cr}");
                sb.Append($"LocalPort: {ctx.Connection.LocalPort}{cr}");
                sb.Append($"ClientCert: {ctx.Connection.ClientCertificate}{cr}{cr}");
                sb.Append($"Headers:{cr}{cr}");

                foreach (var header in ctx.Request.Headers)
                {
                    sb.Append($"{header.Key}: {header.Value}{cr}");
                }

                sb.Append($"{cr}Environment Variables:{cr}{cr}");

                var vars = Environment.GetEnvironmentVariables();
                foreach (var key in vars.Keys.Cast<string>()
                    .OrderBy(key => key, StringComparer.OrdinalIgnoreCase))
                {
                    sb.Append($"{key}: {vars[key]}{cr}");
                }

                await ctx.Response.WriteAsync(sb.ToString());
            }
        }
    }
}
