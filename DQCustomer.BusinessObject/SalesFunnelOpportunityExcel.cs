using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class SalesFunnelOpportunityExcel
    {
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string CustomerName { get; set; }
        public string Direktorat { get; set; }
        public string Brand { get; set; }
        public string Notes { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
