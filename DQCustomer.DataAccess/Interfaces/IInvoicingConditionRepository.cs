using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IInvoicingConditionRepository : IRepository<CpInvoicingCondition>
    {
        CpInvoicingCondition GetInvoicingConditionById(long Id);
        List<CpInvoicingCondition> GetInvoicingConditionByCustomerID(long customerID);
    }
}
