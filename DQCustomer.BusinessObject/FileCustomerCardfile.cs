using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class FileCustomerCardfile
    {
        public string CustomerCardID { get; set; }
        public long CustomerGenID { get; set; }
        public string ImageFile { get; set; }
        public string Extension { get; set; }
        public string Creator { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime LastModifyDate { get; set;}
        public string StreamID { get; set;}
        public string FileDownload { get; set;}
        public string FileType { get; set; }
    }
}
