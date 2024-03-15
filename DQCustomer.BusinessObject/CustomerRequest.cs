using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CustomerRequest : BaseEntity
    {
        public long CustomerGenID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Addr3 { get; set; }
        public string Addr4 { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string IndustryClass { get; set; }
    }
}
