using eCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCommerce.Infrastructure;

namespace eCommerce.Tests
{
    [TestClass]
    public class CustomerOrderTest
    {
        private ICustomerOrder customerOrder = new CustomerOrderService();

        [TestMethod]
        public void CustomerNotFoundTest()
        {
            // Arrange
            var req = new eCommerce.Core.CustomerOrderModel.CustomerOrderRequest
            {
                customerId = "123456",
                user = "uchenna.nnamani@@mmtdigital.co.uk"
            };
            //
            var resp = customerOrder.getCustomerOrder(req);
            // Assert
            Assert.IsNull(resp);
        }

        [TestMethod]
        public void CustomerOrderHasRecordTest()
        {
            // Arrange
            var req = new eCommerce.Core.CustomerOrderModel.CustomerOrderRequest
            {
                customerId = "C34454",
                user = "cat.owner@mmtdigital.co.uk"
            };
            //
            var resp = customerOrder.getCustomerOrder(req);
            // Assert
            Assert.IsNotNull(resp);
            Assert.AreEqual(2, resp.order.orderItems.Count());
        }

        [TestMethod]
        public void CustomerOrderIsAGiftTest()
        {
            // Arrange
            var req = new eCommerce.Core.CustomerOrderModel.CustomerOrderRequest
            {
                customerId = "XM45001",
                user = "santa@north-pole.lp.com"
            };
            //
            var resp = customerOrder.getCustomerOrder(req);
            // Assert
            Assert.IsNotNull(resp);
            Assert.AreEqual(5, resp.order.orderItems.Count());
            Assert.AreEqual(Core.Constants.GIFT, resp.order.orderItems.FirstOrDefault().product);
        }
    }
}
