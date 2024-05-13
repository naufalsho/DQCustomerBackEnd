using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class Req_AccountActivityHistoryGetByID
    {
        public long AccountActivityHistoryID { get; set; }
        public long CustomerID { get; set; }
        public long CustomerGenID { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
    }
}