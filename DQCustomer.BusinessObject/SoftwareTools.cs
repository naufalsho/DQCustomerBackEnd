using DQCustomer.BusinessObject.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQGeneric.BusinessObject
{
    public class SoftwareTools : BaseEntity
    {
        public long SoftwareToolID { get; set; }
        public long? SoftwareID { get; set; }
        //public long? SubSoftwareID { get; set; }
        public string SoftwareToolName { get; set; }
        public long? SoftwareToolType { get; set; }
    }
}
