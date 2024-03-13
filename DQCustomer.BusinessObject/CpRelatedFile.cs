using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject
{
    public class CpRelatedFile : BaseEntity
    {
        public long RFileID { get; set; }
        public long CustomerID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string? DocumentPath { get; set; }
    }
}
