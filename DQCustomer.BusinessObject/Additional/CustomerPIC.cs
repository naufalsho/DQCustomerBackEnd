using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Additional
{
    public class CustomerPIC : BaseEntity
    {
        public long CustomerPICID { get; set; }
        public long CustomerGenID { get; set; }
        public long CustomerInfoID { get; set; }
        public int SalesID { get; set; }
        public string PICName { get; set; }
        public string PICMobilePhone { get; set; }
        public string PICEmailAddr { get; set; }
        public string PICJobTitle { get; set; }
    }
}
