using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpCustomerSettingSearchRequest
    {
        public int TotalRows { get; set; }
        public string Column { get; set; }
        public string Sorting { get; set; }
        public List<Req_CustomerSearchRequest_ViewModel> Rows { get; set; }
    }
}
