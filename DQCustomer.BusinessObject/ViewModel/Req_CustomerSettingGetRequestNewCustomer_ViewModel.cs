using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetRequestNewCustomer_ViewModel : BaseEntity
    {
        public long CustomerGenID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string IndustryClass { get; set; }
        public string CustomerBusinessName { get; set; }
        public string HoldingCompName { get; set; }
        public string CustomerAddress { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string NIB { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string CoorporateEmail { get; set; }
        public string NPWPNumber { get; set; }
        public string Requestor { get; set; }
        public List<Req_CustomerCardFileGetByCustomerGenID_ViewModel> req_CustomerCardFileGetByCustomerGenID_ViewModels { get; set; }
        public string PICName { get; set; }
        public string PICMobilePhone { get; set; }
        public string PICJobTitle { get; set; }
        public string PICEmailAddr { get; set; }
        public string IsNew { get; set; }
        public string ApprovalStatus { get; set; }

    }
}
