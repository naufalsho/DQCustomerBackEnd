using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel
    {
        public string CustomerGenID { get; set; }
        public string ApprovalStatus { get; set; }
        public string Remark { get; set; }
        public long ModifyUserID { get; set; }
    }
}
