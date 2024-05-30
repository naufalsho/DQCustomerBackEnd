using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IAccountActivityHistoryLogic
    {
        ResultAction GetAll();
        ResultAction GetByID(long accountActivityHistoryID);
        ResultAction Update(CpAccountActivityHistory objEntity);
        ResultAction DeleteByID(long accountActivityHistoryID);
        ResultAction GetAccountActivityHistoryByID(long customerID, long customerGenID, bool showAll);
        ResultAction Insert(Req_AccountActivityHistoryInsert_ViewModel objEntity);
    }
}