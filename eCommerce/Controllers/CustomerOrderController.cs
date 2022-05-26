using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eCommerce.Infrastructure;
using eCommerce.Core;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CustomerOrderController : ApiController
    {
        private ICustomerOrder customerOrder = new CustomerOrderService();


        [HttpPost]
        public IHttpActionResult getCustomerOrder(CustomerOrderModel.CustomerOrderRequest _params)
        {
            var custOrder = customerOrder.getCustomerOrder(_params);
            if (custOrder == null) return BadRequest("Customer Not Found");
            return Ok(custOrder);
        }

 
    }
}
