using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface ICustomerSuccessStoryRepository : IRepository<CpCustomerSuccessStory>
    {
        List<string> GetCustomerStoriesByCustomerID(long funnelID);
    }
}
