using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class SalesFunnelOpportunity : BaseEntity
    {
        public long FunnelOpportunityID { get; set; }
        public long? FunnelID { get; set; }
        public string Status { get; set; }
        public string EventName { get; set; }
        public long? CustomerGenID { get; set; }
        public long? BrandID { get; set; }
        public int? SalesID { get; set; }
        public string Notes { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
