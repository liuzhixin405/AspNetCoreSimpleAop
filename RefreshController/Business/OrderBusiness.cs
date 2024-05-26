using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test001Controller
{
    public class OrderBusiness
    {
        public async Task<string> GetOrder(int orderId)
        {
            await Task.Delay(1);
            return "Order Id: " + orderId;
        }
    }
}
