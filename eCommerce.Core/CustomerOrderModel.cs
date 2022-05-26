using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    public class CustomerOrderModel
    {
        public class Customer
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
        }

        public class OrderItem
        {
            public string product { get; set; }
            public int? quantity { get; set; }
            public decimal? priceEach { get; set; }
        }

        public class Order
        {
            public int orderNumber { get; set; }
            public DateTime? orderDate { get; set; }
            public string deliveryAddress { get; set; }
            public List<OrderItem> orderItems { get; set; }
            public DateTime? deliveryExpected { get; set; }
        }

        public class CustomerOrderResponce
        {
            public Customer customer { get; set; }
            public Order order { get; set; }
        }
        public class CustomerOrderRequest
        {
            public string user { get; set; }
            public string customerId { get; set; }
        }
    }
}
