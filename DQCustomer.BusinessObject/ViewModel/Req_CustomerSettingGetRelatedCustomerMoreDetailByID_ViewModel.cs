using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetRelatedCustomerMoreDetailByID_ViewModel
    {
        public long RCustomerID { get; set; }
        public long CustomerID { get; set; }
        public long CustomerGenID { get; set; }
        public long RelatedCustomerID { get; set; }
        public string RelatedCustomerName { get; set; }
    }
}
