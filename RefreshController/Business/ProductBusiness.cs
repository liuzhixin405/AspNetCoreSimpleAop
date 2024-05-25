using IBusiness;

namespace Business
{
    public class ProductBusiness : IProductBusiness
    {
        public async Task<int> AddProduct(string name, decimal price)
        {
            await Task.CompletedTask;
            return 1;
        }
    }
}
