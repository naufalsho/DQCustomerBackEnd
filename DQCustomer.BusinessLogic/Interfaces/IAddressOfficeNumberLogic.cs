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
        ResultAction Update(int Id, CpAddressOfficeNumber objEntity);
        ResultAction Delete(int Id);
        ResultAction GetAddressOfficeNumberByCustomerGenId(int customerGenId);
    }
}