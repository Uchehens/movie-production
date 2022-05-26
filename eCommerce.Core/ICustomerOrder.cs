using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{ 
    public interface ICustomerOrder
    {
        CustomerOrderModel.CustomerOrderResponce getCustomerOrder(CustomerOrderModel.CustomerOrderRequest _params);
    }
}
