using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpCustomerSetting : BaseEntity
    {
        public long CustomerSettingID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCategory { get; set; }
        public long SalesID { get; set; }
        public bool Shareable { get; set; }
        public bool Named { get; set; }
        public bool? PMOCustomer { get; set; }
        public long RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}
