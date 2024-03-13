using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Additional
{
    public class FileCustomerCard
    {
        public Guid CustomerCardID { get; set; }
        public long CustomerGenID { get; set; }
        //public byte[] ImageFile { get; set; }
        public string Creator { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string FileDownload { get; set; }
    }
}
