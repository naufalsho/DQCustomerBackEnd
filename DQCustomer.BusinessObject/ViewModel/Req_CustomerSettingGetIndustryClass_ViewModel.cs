using System;
using DQCustomer.BusinessObject;
using System.Collections.Generic;
using DQCustomer.BusinessObject.Base;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace DQCustomer.BusinessObject.ViewModel
{
    public class Req_CustomerSettingGetIndustryClass_ViewModel
    {
        public string IndustryClassID { get; set; }
        public string IndustryClassName { get; set; }
    }   
}



