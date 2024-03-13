using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface ICustomerSuccessStoryLogic
    {
        ResultAction Insert(CpCustomerSuccessStory objEntity);
    }
}
