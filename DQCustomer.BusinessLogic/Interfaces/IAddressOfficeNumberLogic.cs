using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IAddressOfficeNumberLogic
    {
        ResultAction GetAddressOfficeNumber();
        ResultAction Insert(CpAddressOfficeNumber objEntity);
        ResultAction Update(long Id, CpAddressOfficeNumber objEntity);
        ResultAction Delete(long Id);
        ResultAction DeleteAddressOfficeNumberByID(long Id, long customerID, long customerGenID);
        ResultAction GetAddressOfficeNumberByCustomerGenId(long customerGenId);
        ResultAction GetAddressOfficeNumberByCustomerId(long customerId);
        ResultAction GetAddressOfficeNumberById(long customerId, long customerGenId);
    }
}