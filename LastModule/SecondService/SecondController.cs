using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondService
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello from SecondController";
        }
    }
}
