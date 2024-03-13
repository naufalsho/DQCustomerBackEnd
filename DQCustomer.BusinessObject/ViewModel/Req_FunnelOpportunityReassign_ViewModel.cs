using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_FunnelOpportunityReassign_ViewModel
    {
        public long FunnelOpportunityID { get; set; }
        public int? SalesID { get; set; }
        public int UserLoginID { get; set; }
    }
}
