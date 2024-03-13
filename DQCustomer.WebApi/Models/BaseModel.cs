using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DQCustomer.WebApi.Models
{
    public abstract class BaseModels
    {
        public DateTime CreateDate { get; set; }
        public int CreateUserID { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyUserID { get; set; }
    }
}
