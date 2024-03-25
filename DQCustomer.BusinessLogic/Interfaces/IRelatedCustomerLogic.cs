using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IRelatedCustomerLogic
    {
        ResultAction GetRelatedCustomer();
        ResultAction InsertRelatedCustomer(CpRelatedCustomer objEntity);
        ResultAction UpdateRelatedCustomer(long Id, CpRelatedCustomer objEntity);
        ResultAction DeleteRelatedCustomer(long Id);
        ResultAction GetRelatedCustomerByCustomerID(long customerID);
        ResultAction GetRelatedCustomerByCustomerGenID(long customerGenID);
    }
}
