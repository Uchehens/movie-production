using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{

    public class CustomerResponce : BaseModel
    {
        public string email { get; set; }
        public string customerId { get; set; }
        public bool website { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string lastLoggedIn { get; set; }
        public string houseNumber { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string postcode { get; set; }
        public string preferredLanguage { get; set; }
    }

    public class CustomerRequest
    {
        public string email { get; set; }
    }


}
