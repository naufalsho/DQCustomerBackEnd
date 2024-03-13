using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetConfigItem_ViewModel
    {
        public string ProductNumber { get; set; }
        public string SONumber { get; set; }
        public string PONumber { get; set; }
        public string PODate { get; set; }
        public string ETAByPurchasing { get; set; }
        public string ETAByPMO { get; set; }
        public string DODate { get; set; }
        public string DescriptionItem { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public string WarrantyStartDate { get; set; }
    }
}
