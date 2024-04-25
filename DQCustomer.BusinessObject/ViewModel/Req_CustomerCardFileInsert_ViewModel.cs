using System;
using DQCustomer.BusinessObject;
using System.Collections.Generic;
using DQCustomer.BusinessObject.Base;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

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



