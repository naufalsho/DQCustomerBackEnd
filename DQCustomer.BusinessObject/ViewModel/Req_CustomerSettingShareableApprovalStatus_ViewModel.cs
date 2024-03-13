using System;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingShareableApprovalStatus_ViewModel
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedUserID { get; set; }
        public string RequestedDate { get; set; }
        public string ApprovalBy { get; set; }
        public string ApprovalDate { get; set; }
    }
}
