using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpRelatedCustomer : BaseEntity
    {
        public long RCustomerID { get; set; }
        public long CustomerID { get; set; }
        public long RelatedCustomerID { get; set; }
        public long CustomerGenID { get; set; } 
    }
}
