using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IFunnelOpportunityLogic
    {
        //ResultAction Insert(SalesFunnelCost model);
        SalesFunnelOpportunityEnvelope GetByFunnelGenID(int page, int pageSize, string column, string sorting);
        ResultAction Insert(SalesFunnelOpportunity model);
        ResultAction Update(Req_FunnelOpportunityUpdate_ViewModel ObjEntity);
        ResultAction InsertUpload(Req_FunnelOpportunityInsertUpload_ViewModel ObjEntity);
        SalesFunnelOpportunityView GetByOpportunityID(long funnelOpportunityID);
        SalesFunnelOpportunityEnvelope GetListOpportunityByUserID(int page, int pageSize, int userLoginID);
        SalesFunnelOpportunityEnvelope Search(int page, int pageSize, int userLoginID, string search);
        SalesFunnelOpportunityEnvelope SearchMarketing(int page, int pageSize, string search);
        ResultAction Reassign(Req_FunnelOpportunityReassign_ViewModel objEntity);
        ResultAction Delete(long funnelOpportunityID);
        ResultAction Cancel(long funnelOpportunityID);
        ResultAction ResendEmailInsertOpportunity(long funnelOpportunityID);
        //ResultAction Update(SalesFunnelCost model);
        //ResultAction Delete(long funnelCostID);
        //Get_ExistingExpendCost_ViewModel CheckExistingExpend(long funnelGenID);
    }
}
