using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessObject.Additional
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int EmployeeKey { get; set; }
        public DateTime EffDate { get; set; }
        public string EmployeeName { get; set; }
        public string BU { get; set; }
        public string DeptID { get; set; }
        public string EmployeeEmail { get; set; }
        public int Role { get; set; }
        public int SuperiorID { get; set; }
        public int DeptLeadFlag { get; set; }
        public int cocode { get; set; }
        public int IsLocked { get; set; }

    }
}
