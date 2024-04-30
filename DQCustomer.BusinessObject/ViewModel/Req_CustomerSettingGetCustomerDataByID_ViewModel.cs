using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetCustomerDataByID_ViewModel
    {
        public string AccountStatus { get; set; }
        public long JDECustomerID { get; set; }
        public long CustomerID { get; set; }
        public long CustomerGenID { get; set; }
        public string IndustryClassID { get; set; }
        public string IndustryClass { get; set; }
        public string IndustryClassBusiness { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PMOCustomer { get; set; }
        public bool Blacklist { get; set; }
        public bool Holdshipment { get; set; }
        public float AvgAR { get; set; }
        public string SalesName { get; set; }
    }
}
