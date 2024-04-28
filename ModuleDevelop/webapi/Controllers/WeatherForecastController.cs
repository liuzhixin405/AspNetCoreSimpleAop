using IOrderService;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
       

        private readonly ILogger<OrderController> _logger;
        private readonly IService orderService;
        public OrderController(ILogger<OrderController> logger, IService orderService)
        {
            _logger = logger;
            this.orderService = orderService;
        }

        [HttpGet("PlacOrder")]
        public async Task<bool> PlaceOrder(string order)
        {
            return await orderService.PlaceOrder(order);
        }
    }
}
