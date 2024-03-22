namespace AopLibraryTest
{
    
    public interface IOrderBusiness
    {
        public string Get(string name);
       
    }

    public class OrderBusiness : IOrderBusiness
    {
        [Cus("xxx")]
        public string Get(string name)
        {
            return name;
        }
    }
}
