using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel

{
    public class Req_CustomerSettingGetCustomerDetailsByGenID_ViewModel
    {
        public long CustomerGenID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string IndustryClass { get; set; }
        public string CustomerBusinessName { get; set; }
        public string HoldingCompName { get; set; }
        public string CustomerAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string NIB { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string CoorporateEmail { get; set; }
        public string NPWPNumber { get; set; }
        public bool CAPFlag { get; set; }
        public string Requestor { get; set; }
        public List<Req_CustomerCardFileGetByCustomerGenID_ViewModel> req_CustomerCardFileGetByCustomerGenID_ViewModels { get; set; }
        public List<CpAddressOfficeNumber> CpAddressOfficeNumbers { get; set; }
        public List<CustomerPIC> CustomerPICs { get; set; }
        public List<Req_CustomerSettingGetRelatedCustomerMoreDetailByID_ViewModel> CpRelatedCustomers { get; set; }
        public string? CreateDate { get; set; }
        public int CreateUserID { get; set; }
        public string? ModifyDate { get; set; }
        public int? ModifyUserID { get; set; }
    }
}