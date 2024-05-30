using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpCustomerSettingDashboard
    {
        public long CustomerID { get; set; }
        public long JDECustomerID { get; set; }
        public long CustomerGenID { get; set; } 
        public string IndustryClassID { get; set; }
        public string IndustryClass { get; set; }
        public string IndustryClassBusiness { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public bool IsNew { get; set; }
        public string LastProjectName { get; set; }
        public string SalesName { get; set; }
        public bool PMOCustomer { get; set; }
        public string RelatedCustomer { get; set; }
        public bool Blacklist { get; set; }
        public bool Holdshipment { get; set; }
        public bool Named { get; set; }
        public bool Shareable { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public List<Req_CustomerSettingGetSalesHistoryByCustID_ViewModel> SalesHistory { get; set; }
    }
}
