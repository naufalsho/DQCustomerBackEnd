using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Base
{
    public abstract class BaseEntity
    {
        public DateTime? CreateDate { get; set; }
        public int CreateUserID { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyUserID { get; set; }
    }
}
