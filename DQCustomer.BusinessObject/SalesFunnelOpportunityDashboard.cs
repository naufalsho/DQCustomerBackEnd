using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class SalesFunnelOpportunityDashboard
    {
        public long FunnelOpportunityID { get; set; }
        public string FunnelID { get; set; }
        public string Status { get; set; }
        public string EventName { get; set; }
        public string CustomerName { get; set; }
        public string Brand { get; set; }
        public string SalesName { get; set; }
        public string CreateUserID { get; set; }
        public string CreateDate { get; set; }
        public string EventDate { get; set; }
        public string Direktorat { get; set; }
        public int AgingDays { get; set; }
    }
}
