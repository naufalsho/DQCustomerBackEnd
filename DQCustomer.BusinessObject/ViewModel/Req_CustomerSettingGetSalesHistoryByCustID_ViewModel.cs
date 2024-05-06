using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetSalesHistoryByCustID_ViewModel
    {
        public long SalesHistoryID { get; set; }
        public long CustomerID { get; set; }
        public long SalesShareableID { get; set; } 
        public long ApprovalBy { get; set; } 
        public string Status { get; set; }
        public string RequestedBy { get; set; }
    }
}
