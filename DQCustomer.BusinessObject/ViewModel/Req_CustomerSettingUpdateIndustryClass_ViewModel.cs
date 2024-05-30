using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingUpdateIndustryClass_ViewModel 
    {
        public string IndustryClass { get; set; }
        public string CustomerName { get; set; }
        public string CoorporateEmail { get; set; }
        public string NPWPNumber { get; set; }
        public string NIB { get; set; }
        public long ModifyUserID { get; set; }
    }
}
