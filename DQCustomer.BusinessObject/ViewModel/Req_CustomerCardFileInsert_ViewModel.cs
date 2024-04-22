using System;
using DQCustomer.BusinessObject.Base;
using Microsoft.AspNetCore.Http;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerCardFileInsert_ViewModel : BaseEntity
    {
        public int CustomerID { get; set; }
        public int CustomerGenID { get; set; }
        public IFormFile File { get; set; }
    }
}
