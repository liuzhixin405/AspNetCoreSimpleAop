namespace IOrder.Repository
{
    public interface IRepository
    {
        Task<object> GetOrder(string orderId);
    }
}
