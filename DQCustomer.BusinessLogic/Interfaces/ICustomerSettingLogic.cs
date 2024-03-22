using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface ICustomerSettingLogic
    {
        ResultAction GetAllCustomerSetting();
        ResultAction GetCustomerSettingBySalesID(long customerID, long SalesID);
        CpCustomerSettingEnvelope GetCustomerSettingNoNamedAccount(int page, int pageSize, string column, string sorting, string search, bool? blacklist = null, bool? holdshipment = null);
        CpCustomerSettingEnvelope GetCustomerSettingNamedAccount(int page, int pageSize, string column, string sorting, string search, string salesID, long? myAccount = null, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null);
        CpCustomerSettingEnvelope GetCustomerSettingShareableAccount(int page, int pageSize, string column, string sorting, string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null);
        CpCustomerSettingEnvelope GetCustomerSettingAllAccount(int page, int pageSize, string column, string sorting, string search, string salesID, long? myAccount = null, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null, bool? showNoName = null, bool? showNamed = null, bool? showShareable = null);
        ResultAction Insert(CpCustomerSetting objEntity);
        ResultAction ApproveCustomerSetting(long customerID, long salesID, bool isApprove, string description, int? modifyUserID);
        ResultAction ReleaseAccount(long customerID, long salesID, int? modifyUserID);
        ResultAction GetCustomerPICByCustomerID(long customerID);
        ResultAction GetBrandSummary(long customerID);
        ResultAction GetServiceSummary(long customerID);
        ResultAction GetProjectHistory(long customerID);
        ResultAction GetCustomerDataByID(long customerID);
        ResultAction GetSalesData();
        ResultAction GetConfigItem(long customerID);
        ResultAction GetCollectionHistory(long customerID);
        ResultAction GetSalesByName(string salesName);
        ResultAction GetCustomerCategory();
        ResultAction Update(long customerID, CpCustomerSetting objEntity);
        ResultAction GetCustomerName(string customerName);
        CpCustomerSettingSearchRequest GetSearchRequest(int page, int pageSize, string column, string sorting, string titleCustomer, string customerName, string picName);
        ResultAction InsertRequestNewCustomer(Req_CustomerSettingInsertRequestCustomer_ViewModel objEntity);
        ResultAction GetRequestNewCustomerByGenID(long customerGenID);
        ResultAction UpdateApprovalStatusNewCustomer(Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel objEntity);
    }
}
