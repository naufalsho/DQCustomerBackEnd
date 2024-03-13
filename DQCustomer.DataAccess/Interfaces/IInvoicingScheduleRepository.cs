using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IInvoicingScheduleRepository : IRepository<CpInvoicingSchedule>
    {
        CpInvoicingSchedule GetInvoicingScheduleById(long Id);
        List<CpInvoicingSchedule> GetInvoicingScheduleByCustomerID(long customerID);
    }
}
