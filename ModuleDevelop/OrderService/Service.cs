using IOrderService;

namespace OrderService
{
    public class Service : IService
    {
        public Task<bool> PlaceOrder(string order)
        {
            return Task.FromResult(true);   
        }
    }
}
