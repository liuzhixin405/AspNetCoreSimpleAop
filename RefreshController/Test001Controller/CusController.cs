using Business;
using CommonFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test001Controller
{
    [ApiController]
    [Route("[controller]/[action]")]

    public abstract class CusController: ControllerBase
    {
        protected readonly ILogger logger;
        public CusController(ILogger<CusController> logger)
        {
            this.logger = logger;
        }     

        protected  Task AddBefore()
        {
            return Task.CompletedTask;
        }

        protected  Task AddAfter()
        {
            return Task.CompletedTask;
        }
    }
}
