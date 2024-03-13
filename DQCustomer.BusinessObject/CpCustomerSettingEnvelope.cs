using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpCustomerSettingEnvelope
    {
        public int TotalRows { get; set; }
        public string Column { get; set; }
        public string Sorting { get; set; }
        public List<CpCustomerSettingDashboard> Rows { get; set; }
    }
}
