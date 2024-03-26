using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpWebsiteSocialMedia : BaseEntity
    {
        public long WebsiteSocialMediaID { get; set; }
        public long? CustomerID { get; set; }
        public long? CustomerGenID { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
    }
}