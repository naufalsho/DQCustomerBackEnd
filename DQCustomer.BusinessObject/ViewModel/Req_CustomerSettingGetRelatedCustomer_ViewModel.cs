using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetRelatedCustomer_ViewModel
    {
        public long RCustomerID { get; set; }
        public long CustomerID { get; set; }
        public string RelatedCustomerName { get; set; }
        public string Address { get; set; }
        public float AvgAR { get; set; }
        public bool Blacklist { get; set; }
        public bool Holdshipment { get; set; }
    }
}
