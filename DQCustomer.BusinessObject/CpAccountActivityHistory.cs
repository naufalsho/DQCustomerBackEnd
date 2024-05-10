using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpAccountActivityHistory : BaseEntity
    {
        public long AccountActivityHistoryID { get; set; }
        public string Description { get; set; }
        public long UserID { get; set; }
    }
}