using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface ICustomerSettingRepository : IRepository<CpCustomerSetting>
    {
        List<CpCustomerSettingDashboard> GetCustomerSettingNoNamedAccount(string search, bool? blacklist = null, bool? holdshipment = null);
        List<CpCustomerSettingDashboard> GetCustomerSettingNamedAccount(string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null);
        List<CpCustomerSettingDashboard> GetCustomerSettingShareableAccount(string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null);
        List<CpCustomerSettingDashboard> GetCustomerSettingAllAccount(string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null);
        CpCustomerSetting GetCustomerSettingBySalesID(long customerID, long SalesID);
        bool UpdateAllCustomerSetting(long id, CpCustomerSetting objEntity);
        List<CpCustomerSetting> GetCustomerSettingByCustomerID(long customerID);
        bool DeleteCustomerSettingBySalesID(long customerID, long SalesID);
        List<Req_CustomerSettingGetPIC_ViewModel> GetCustomerPICByCustomerID(long customerID);
        List<Req_CustomerSettingGetBrandSummary_ViewModel> GetBrandSummary(long customerID);
        List<Req_CustomerSettingGetServiceSummary_ViewModel> GetServiceSummary(long customerID);
        Req_CustomerSettingGetCustomerDataByID_ViewModel GetCustomerDataByID(long customerID);
        List<Req_CustomerSettingGetProjectHistory_ViewModel> GetProjectHistory(long customerID);
        List<Req_CustomerSettingGetSalesData_ViewModel> GetListSales();
        List<Req_CustomerSettingGetConfigItem_ViewModel> GetConfigItem(long customerID);
        List<Req_CustomerSettingGetCollectionHistory_ViewModel> GetCollectionHistory(long customerID);
        List<Req_CustomerSettingGetSalesData_ViewModel> GetSalesByName(string salesName);
        List<string> GetCustomerCategory();
        bool UpdateSpecificCustomerSetting(long id, CpCustomerSetting objEntity);
        List<Req_CustomerSettingGetCustomerDataByName_ViewModel> GetCustomerByName(string customerName);
        long GetApprovalID();
        List<Req_CustomerSettingGetRelatedCustomerAndLastProject_ViewModel> GetRelatedAndLast();
        void SendEmailReqCustomerSetting(long customerID, long reqSalesID, long approverID);
        void SendEmailApproveRejectCustomerSetting(long customerID, long reqSalesID, bool isApprove, string description, int? modifyUserID);
        List<Req_CustomerSearchRequest_ViewModel> GetSearchRequest(string titleCustomer, string customerName, string picName);
        bool InsertRequestNewCustomer(Req_CustomerSettingInsertRequestCustomer_ViewModel objEntity);
        List<Req_CustomerSettingGetRequestNewCustomer_ViewModel> GetRequestNewCustomerByGenID(long customerGenID);
        bool UpdateApprovalStatusNewCustomer(Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel objEntity);
        IEnumerable<Req_CustomerSettingGetCustomerDetailsByCustID_ViewModel> GetCustomerDetailsByCustID(long customerID);
        IEnumerable<Req_CustomerSettingGetCustomerDetailsByGenID_ViewModel> GetCustomerDetailsByGenID(long customerGenID);
        bool UpdateIndustryClassByID(long customerID, long customerGenID, Req_CustomerSettingUpdateIndustryClass_ViewModel objEntity);

    }
}
