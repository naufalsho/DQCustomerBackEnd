using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IAccountActivityHistoryRepository : IRepository<CpAccountActivityHistory>
    {
        CpAccountActivityHistory GetByID(long accountActivityHistoryID);
        bool DeleteByID(long accountActivityHistoryID);
        List<Req_AccountActivityHistoryGetByID> GetAccountActivityHistoryByID(long customerID, long customerGenID);
    }
}