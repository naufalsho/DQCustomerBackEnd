using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class CpCustomerRequest : BaseEntity
    {
        public long CustomerID { get; set; }
        public string TitleCustomer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTypeAddress { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerOfficeNumber { get; set; }

        public string PICName { get; set; }
    }
}
