using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace MainHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {

        private readonly IEnumerable<Type> _controllerTypes;
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger, ApplicationPartManager manager)
        {
            _logger = logger;
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            _controllerTypes = feature.Controllers;
        }

        [HttpGet(Name = "main")]
        public string Get()
        {
            var lines = _controllerTypes.Select(it => it.Name);
            return string.Join(Environment.NewLine, lines.ToArray());
        }
    }
}
