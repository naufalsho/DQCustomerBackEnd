using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CustomerPIC : BaseEntity
    {
        public long CustomerPICID { get; set; }
        public long CustomerGenID { get; set; }
        public string? PICName { get; set; }
        public string? PICJobTitle { get; set; }
        public string? PICEmailAddr { get; set; }
        public string? PICMobilePhone { get; set; }
        public string? PICAddress { get; set; }
    }
}