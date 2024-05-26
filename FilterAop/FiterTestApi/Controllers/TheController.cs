using Microsoft.AspNetCore.Mvc;

namespace FiterTestApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TheController : ControllerBase
    {
        private readonly ITheService _theService;
        private readonly ILogger<TheController> _logger;

        public TheController(ILogger<TheController> logger, ITheService theService)
        {
            _theService = theService;
            _logger = logger;
        }

        [HttpGet]
        //[Log]
        public IActionResult Get()
        {
            _logger.LogInformation("Get method called");
            return Ok(_theService.GetSomething());
        }
    }
}
