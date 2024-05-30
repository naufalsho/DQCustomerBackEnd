using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject.Base;

namespace DQCustomer.BusinessObject
{
    public class CpSalesHistory : BaseEntity
    {
        public long SalesHistoryID { get; set; }
        public long CustomerID { get; set; }
        public long SalesID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public long RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool? IsApprovedByDirectorate { get; set; }
        public DateTime? DirectorateApprovedDate { get; set; }
        public long? DirectorateApprovedBy { get; set; }
        public bool? IsApprovedByAdmin { get; set; }
        public DateTime? AdminApprovedDate { get; set; }
        public long? ApprovalBy { get; set; }
        public string? ClaimRemark { get; set; }
    }
}
