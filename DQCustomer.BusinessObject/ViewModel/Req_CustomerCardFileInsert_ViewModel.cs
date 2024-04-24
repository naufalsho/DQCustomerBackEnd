using System;
using DQCustomer.BusinessObject.Base;
using Microsoft.AspNetCore.Http;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerCardFileInsert_ViewModel 
    {
        public long CustomerGenID { get; set; }
        //public byte[] ImageFile { get; set; }
        //public string Extension { get; set; }
        public string LastModifyUserID { get; set; }
        public IFormFile File { get; set; }
    }
}
