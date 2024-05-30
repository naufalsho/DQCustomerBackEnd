using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface ICustomerCardFileLogic
    {
        ResultAction GetCustomerCardFileByCustomerGenID(long customerGenID);
        ResultAction InsertCustomerCardFile(Req_CustomerCardFileInsert_ViewModel objEntity);
        ResultAction GetByCustomerCardID(Guid customerCardID);
        ResultAction Delete(Guid customerCardID);
    }
}
