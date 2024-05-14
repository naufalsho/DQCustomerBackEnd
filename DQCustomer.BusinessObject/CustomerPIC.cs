using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CustomerPIC : BaseEntity
    {
        public long CustomerPICID { get; set; }
        [Required(ErrorMessage = "CustomerGenID is required.")]
        public long CustomerGenID { get; set; }
        public string? PICName { get; set; }
        public string? PICJobTitle { get; set; }
        public string? PICEmailAddr { get; set; }
        public string? PICMobilePhone { get; set; }
        public string? PICAddress { get; set; }
        public bool? PINFlag { get; set; }
        public bool? CAPFlag { get; set; }
    }
}