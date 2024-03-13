using System;
using Microsoft.AspNetCore.Http;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingInsertRelatedFile_ViewModel
    {
        public int CustomerID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public IFormFile File { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CreateUserID { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyUserID { get; set; }
    }
}
