namespace IBusiness
{
    public interface IProductBusiness
    {
        Task<int> AddProduct(string name, decimal price);
    }
}
