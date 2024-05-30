using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface ISalesHistoryRepository : IRepository<CpSalesHistory>
    {
        CpSalesHistory GetSalesHistoryByCustomerID(long SalesHistoryID);
        List<Req_CustomerSettingShareableApprovalStatus_ViewModel> GetShareableStatus(long customerID);
        List<Req_CustomerSettingGetAccountOwner_ViewModel> GetAccountOwner(long customerID);
        List<Req_CustomerSettingGetSalesAssignHistory_ViewModel> GetSalesAssignHistory(long customerID);
        List<Req_CustomerSettingGetSalesHistoryByCustID_ViewModel> GetSalesHistoryByID(long customerID);
    }
}
