using System;
using DQCustomer.BusinessObject.Base;
using Microsoft.AspNetCore.Http;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerCardFileGetByCustomerGenID_ViewModel
    {
        public Guid CustomerCardID { get; set; }
        public long CustomerGenID { get; set; }
        public byte[] ImageFile { get; set; }
        public string Extension { get; set; }
        public string Creator { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string StreamID { get; set; }
        public string FileDownload { get; set; }
        public string FileType { get; set; }
    }
}
