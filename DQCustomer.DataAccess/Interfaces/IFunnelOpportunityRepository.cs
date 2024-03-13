using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IFunnelOpportunityRepository : IRepository<SalesFunnelOpportunity>
    {
        //List<SalesFunnelCost> GetByFunnelGenID(long funnelGenID);
        //List<SalesFunnelCost> GetListExisting(SalesFunnelCost objEntity);
        //void UpdateFunnelFromCostProduct(decimal? newtotalOrderingProduct, decimal? newtotalOrdering, decimal newGPM, SalesFunnel funnelExisting, SalesFunnelCost objEntity, decimal sumCostProduct, decimal sumExpendProduct);
        //void UpdateFunnelFromCostService(decimal? newtotalOrderingService, decimal? newtotalOrdering, decimal newGPM, SalesFunnel funnelExisting, SalesFunnelCost objEntity, decimal sumCostService, decimal sumExpendService);
        //SalesFunnelCost GetByFunnelCostID(long funnelCostID);
        //List<Get_SalesFunnelCost_ViewModel> GetByFunnelGenIDForView(long funnelGenID);
        List<SalesFunnelOpportunityDashboard> GetListDashboard();
        SalesFunnelOpportunity GetOpportunityByID(long funnelOpportunityID);
        List<SalesFunnelOpportunityDashboard> GetListDashboardBySalesID(string hirarki);
        List<SalesFunnelOpportunityDashboard> Search(string hirarki, string search);
        List<SalesFunnelOpportunityDashboard> SearchMarketiing(string search);
        List<SalesFunnelOpportunity> CheckFunnelToOpportunity(Req_FunnelCheckToOpportunity_ViewModel obj);
    }
}
