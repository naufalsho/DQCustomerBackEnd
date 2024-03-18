using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingInsertRequestCustomer_ViewModel 
    {
        public string TitleCustomer { get; set; }
        public string CustomerName { get; set; }
        public string PICName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string IndustryClass { get; set; }
        public string Website { get; set; }
        public string SocialMedia { get; set; }
        public string PICPhoneNumber { get; set; }
        public string PICJobTitle { get; set; }
        public string PICEmail { get; set; }
        public int CreatedUserID { get; set; }
        public int ModifyUserID { get; set; }
    }
}
