using IBusiness;

namespace Business
{
    public class ProductBusiness : IDisposable// : IProductBusiness
    {
        public static readonly ProductBusiness Instance;
        private bool _disposed = false; 
        static ProductBusiness()
        {
            Instance = new ProductBusiness();
        }
      
        private ProductBusiness()
        {
            // 初始化资源
        }
        public async Task<int> AddProduct(string name, decimal price)
        {
            await Task.CompletedTask;
            return 1;
        }

        // 实现IDisposable接口
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 释放托管资源
            }

            // 释放非托管资源
            _disposed = true;
        }

        // 析构函数
        ~ProductBusiness()
        {
            Dispose(false);
        }
    }
}
