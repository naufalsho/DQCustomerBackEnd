using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface ICustomerPICRepository : IRepository<CustomerPIC>
    {
        bool InsertCustomerPIC(CustomerPIC objEntity);
        CustomerPIC GetCustomerPICById(long Id);
        List<CustomerPIC> GetCustomerPICByCustomerGenId(long customerGenId);
    }
}