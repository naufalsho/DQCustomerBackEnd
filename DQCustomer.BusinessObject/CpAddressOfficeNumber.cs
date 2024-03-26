using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpAddressOfficeNumber : BaseEntity
    {
        public long AddressOfficeNumberID { get; set; }
        public long? CustomerGenID { get; set; }
        public long? CustomerID { get; set; }
        public string? FullAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AlternateNumber {get; set;}
        public string? FaxNumber { get; set; }
    }
}