using System;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetCustomerDataByName_ViewModel
    {
        public long CustomerID { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PMOCustomer { get; set; }
        public bool Blacklist { get; set; }
        public bool Holdshipment { get; set; }
        public float AvgAR { get; set; }
    }
}
