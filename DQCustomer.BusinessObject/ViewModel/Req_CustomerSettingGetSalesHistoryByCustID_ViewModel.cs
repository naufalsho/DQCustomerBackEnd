﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetSalesHistoryByCustID_ViewModel
    {
        public long SalesHistoryID { get; set; }
        public long CustomerID { get; set; }
        public long SalesID { get; set; }
        public long SalesKey { get; set; }
        public long ApprovalBy { get; set; }
        public long DirectorateApprovedBy { get; set; }
        public string DirectorateApprovedDate { get; set; }
        public string Status { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedDate { get; set; }
        public string ApprovedAdminBy { get; set; }
        public string AdminApprovedDate { get; set; }
        public string RejectedDate { get; set; }
        public string RejectedBy { get; set; }
        public string WaitingAdminApproveBy { get; set; }

    }
}
