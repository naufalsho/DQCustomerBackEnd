using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_FunnelCheckToOpportunity_ViewModel
    {
        public long? CustomerGenID { get; set; }
        public int? SalesID { get; set; }
        public string BrandID { get; set; }
    }
}
