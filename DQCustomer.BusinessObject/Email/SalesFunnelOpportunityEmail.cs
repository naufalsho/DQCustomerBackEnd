using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Email
{
    public class SalesFunnelOpportunityEmail
    {
        public long FunnelOpportunityID { get; set; }
        public string FunnelID { get; set; }
        public string Status { get; set; }
        public string EventName { get; set; }
        public string CustomerName { get; set; }
        public string Brand { get; set; }
        public string SalesName { get; set; }
        public string CreateUserID { get; set; }
        public string Note { get; set; }
    }
}
