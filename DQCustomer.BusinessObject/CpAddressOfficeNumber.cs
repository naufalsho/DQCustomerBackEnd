using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpAddressOfficeNumber : BaseEntity
    {
        public int? AddressOfficeNumberID { get; set; }
        public int? CustomerGenID { get; set; }
        public string? FullAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AlternateNumber {get; set;}
        public string? FaxNumber { get; set; }
    }
}