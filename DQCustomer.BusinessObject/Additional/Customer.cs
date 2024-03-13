using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Additional
{
    public class Customer
    {
        public int CustomerGenID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Addr3 { get; set; }
        public string Addr4 { get; set; }
        public string City { get; set; }
        public string IndustryClass { get; set; }
        public string EndUserFlag { get; set; }
        public string Creator { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public int? CustomerIDC { get; set; }

        //public int CustomerGenID { get; set; }
        //public int CustomerID { get; set; }
        //public string CustomerName { get; set; }
        //public string Addr1 { get; set; }
        //public string Addr2 { get; set; }
        //public string Addr3 { get; set; }
        //public string Addr4 { get; set; }
        //public string City { get; set; }
        //public string PhoneNumber { get; set; }
        //public string IndustryClass { get; set; }
        //public string Website { get; set; }
        //public string SocialMedia { get; set; }
        //public string EndUserFlag { get; set; }
        //public string MemberGroup { get; set; }
        public string NPWPNumber { get; set; }
        //public string NPWPAddress { get; set; }
        //public string FinanceCPName { get; set; }
        //public string FinanceCPPhone { get; set; }
        //public string FinanceCPEmail { get; set; }
        //public string FinanceDirName { get; set; }
        //public string FinanceDirPhone { get; set; }
        //public string FinanceDirEmail { get; set; }
        //public string ExecutiveDirName { get; set; }
        //public string ExecutiveDirPhone { get; set; }
        //public string ExecutiveDirEmail { get; set; }
    }
}
