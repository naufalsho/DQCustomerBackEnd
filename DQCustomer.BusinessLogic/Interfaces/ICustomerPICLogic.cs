using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface ICustomerPICLogic
    {
        ResultAction GetCustomerPIC();
        ResultAction Insert(CustomerPIC objEntity);
        ResultAction Update(long Id, CustomerPIC objEntity);
        ResultAction Delete(long Id);
        ResultAction GetCustomerPICByCustomerGenId(long customerGenId);
    }
}