namespace IOrderService
{
    public interface IService
    {
        Task<bool> PlaceOrder(string order);
    }
}
