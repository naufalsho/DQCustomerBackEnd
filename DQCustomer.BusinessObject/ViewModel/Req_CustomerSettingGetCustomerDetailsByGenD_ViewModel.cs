using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel

{
    public class Req_CustomerSettingGetCustomerDetailsByGenID_ViewModel : BaseEntity
    {
        public long CustomerGenID { get; set; }
        public string CustomerName { get; set; }
        public string IndustryClass { get; set; }
        public string Requestor { get; set; }
        public List<CpAddressOfficeNumber> CpAddressOfficeNumbers { get; set; }
        public List<CustomerPIC> CustomerPICs { get; set; }
        public List<CpRelatedCustomer> CpRelatedCustomers { get; set; }
    }
}