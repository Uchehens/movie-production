using eCommerce.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure
{
    public class CustomerOrderService : ICustomerOrder
    {
        private CustomerResponce getCustomerDetail(CustomerRequest _params)
        {

            try
            {
                using (var client = new WebClient())
                {
                    string dataString = JsonConvert.SerializeObject(_params);
                    string endpoint = System.Configuration.ConfigurationManager.AppSettings["CustomerEndpoint"].ToString();
                    string key = System.Configuration.ConfigurationManager.AppSettings["apiKey"].ToString();
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string s = string.Format("{0}/api/GetUserDetails?code={1}", endpoint, key);
                    string resp = client.UploadString(new Uri(s), dataString);
                    var respVal = JsonConvert.DeserializeObject<CustomerResponce>(resp);
                    respVal.responceCode = "00";
                    respVal.responceMessage = "Success";
                    return respVal;
                }
            }
            catch (Exception ex)
            {
                //Log Exception on file
                return new CustomerResponce
                {
                    responceMessage = "Record Not Found",
                    responceCode = "99"
                };
            }

        }


        public CustomerOrderModel.CustomerOrderResponce getCustomerOrder(CustomerOrderModel.CustomerOrderRequest _params)
        {

            try
            {
                var ret = new CustomerOrderModel.CustomerOrderResponce();
                var customerDetails = getCustomerDetail(new CustomerRequest
                {
                    email = _params.user
                });

                if (customerDetails.responceCode != "00") return null;
                ret.customer = new CustomerOrderModel.Customer
                {
                    firstName = customerDetails.firstName,
                    lastName = customerDetails.lastName
                };

                using (var db = new eCommerce.eCommerceDB())
                {
                    var lastedOrder = db.Fetch<ORDER>("WHERE CUSTOMERID = @0 ORDER BY ORDERDATE DESC", customerDetails.customerId).FirstOrDefault();
                    var listOfOrderItems = db.Fetch<ORDERITEM>("WHERE ORDERID = @0", lastedOrder.ORDERID);
                    List<CustomerOrderModel.OrderItem> orderItems = new List<CustomerOrderModel.OrderItem>();
                    foreach (var s in listOfOrderItems)
                    {
                        var product = db.Fetch<PRODUCT>("WHERE PRODUCTID = @0", s.PRODUCTID).FirstOrDefault();
                        if (product != null)
                        {
                            orderItems.Add(new CustomerOrderModel.OrderItem
                            {
                                product = (lastedOrder.CONTAINSGIFT == true) ? Core.Constants.GIFT : product.PRODUCTNAME,
                                priceEach = s.PRICE,
                                quantity = s.QUANTITY
                            });
                        }

                    }
                    ret.order = new CustomerOrderModel.Order
                    {
                        orderNumber = lastedOrder.ORDERID,
                        orderDate = lastedOrder.ORDERDATE,
                        deliveryExpected = lastedOrder.DELIVERYEXPECTED,
                        deliveryAddress = string.Format("{0}, {1}, {2}", customerDetails.street, customerDetails.town, customerDetails.postcode),
                        orderItems = orderItems
                    };
                }
                return ret;
            }
            catch (Exception ex)
            {
                //Log exception to file
                return new CustomerOrderModel.CustomerOrderResponce();
            }

        }
    }
}
