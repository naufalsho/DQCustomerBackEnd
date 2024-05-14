using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class Req_AccountActivityHistoryInsert_ViewModel
    {
        public long CustomerID { get; set; }
        public long CustomerGenID { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "UserID is required.")]
        public long UserID { get; set; }
    }
}