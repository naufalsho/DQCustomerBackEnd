using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using ExcelDataReader.Log;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IInvoicingConditionLogic
    {
        ResultAction GetInvoicingCondition();
        ResultAction InsertInvoicingCondition(CpInvoicingCondition objEntity);
        ResultAction UpdateInvoicingCondition(long Id, CpInvoicingCondition objEntity);
        ResultAction DeleteInvoicingCondition(long Id);
        ResultAction GetInvoicingConditionByCustomerID(long customerID);
    }
}
