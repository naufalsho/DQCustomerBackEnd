using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSearchRequest_ViewModel
    {
        public long CustomerID { get; set; }
        public string titleCustomer { get; set; }
        public string CustomerName { get; set; }
        public string PICName { get; set; }
    }
}
