using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface ICustomerRequestRepository : IRepository<CpCustomerRequest>
    {
        List<Req_CustomerSearchRequest_ViewModel> GetCustomerSearchRequest(Req_CustomerSearchRequest_ViewModel customerSearchRequest_ViewModel, long customerID);
    }
}
