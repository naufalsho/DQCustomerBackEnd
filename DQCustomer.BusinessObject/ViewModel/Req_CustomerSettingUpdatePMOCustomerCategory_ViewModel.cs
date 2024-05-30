using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingUpdatePMOCustomerCategory_ViewModel
    {
        public long CustomerID { get; set; }
        public string CustomerCategory { get; set; }
        public bool? PMOCustomer { get; set; }
        public bool CAPFlag { get; set; }
        public string IndustryClass { get; set; }
        public int? ModifyUserID { get; set; }
    }
}
