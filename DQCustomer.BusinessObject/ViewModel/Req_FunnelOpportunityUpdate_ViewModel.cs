using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_FunnelOpportunityUpdate_ViewModel
    {
        public long FunnelOpportunityID { get; set; }
        public long? FunnelID { get; set; }
        public string Status { get; set; }
        public string EventName { get; set; }
        public long? CustomerGenID { get; set; }
        public long? BrandID { get; set; }
        public int? SalesID { get; set; }
        public int UserLoginID { get; set; }
        public string Notes { get; set; }
        public DateTime EventDate { get; set; }
    }
}
