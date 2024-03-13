using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpInvoicingCondition : BaseEntity
    {
        public long IConditionID { get; set; }
        public long CustomerID { get; set; }
        public string ProjectType { get; set; }
        public string DocumentName { get; set; }
    }
}
