using IOrder.Repository;

namespace Order.Repository
{
    public class Repository : IRepository
    {
        public async Task<object> GetOrder(string orderId)
        {
            await Task.CompletedTask;
            return new
            {
                Id = 1,
                Price = 12.4
            };
        }
    }
}
