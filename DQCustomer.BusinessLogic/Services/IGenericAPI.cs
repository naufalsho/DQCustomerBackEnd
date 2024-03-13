using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.ViewModel;
using DQGeneric.BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.BusinessLogic.Services
{
    public interface IGenericAPI
    {
        string GetCustomerName(long customerGenID);
        string GetEmployeeName(int employeeID);
        string GetStatusFunnelByID(long ID);
        List<int> GetListSalesFunnelBySalesID(int ID);
        Employee GetByDeptID(string deptID);
        Employee GetByEmployeeID(int employeeID);
        long GetIdBrandByName(string BrandName);
        long GetIdSubBrandByName(long BrandID, string SubBrandName);
        List<Employee> GetListSubordinate(string email);
        List<Employee> GetListSubordinateAll(string email);
        Udc GetPresales(string deptID);
        Employee GetPMOInfo(long salesID, long customerID);
        Employee GetSMOInfo(long salesID, string deptID);
        Employee GetPresalesLeader(long salesID, string deptID);
        Employee GetProductManager(int itemID);
        string GetPathFunnelAttachment();
		string GetDocType(int documentTypeID);
        Udc GetUdcByID(int UDCID);
        FileCustomerCard GetFileCustomerCard(string fileID);
        string GetCustomerPICName(long customerPICID);
        string GetDocumentType(long udcID);
        Employee GetByEmail(string email);
		 void UpdateCustomerPIC(CustomerPIC obj);
        public List<Tr_Initiate> GetSA(string contractNumber);
        public Udc GetPMOS(int Id);
        public List<Udc> GetListPMOS(int Id);
        Employee GetByEmployeeName(string item);
        List<Employee> GetDirectSubordinate(string email);
        SoftwareTools GetBySoftwareID(string item);
        long? GetCustomerByName(string customerName);
        int? GetSalesByLastSA(string customerName, string direktorat);
        List<Udc> GetByEntryKey(string entryKey);
        List<CollectionData> DropdownEmployee(string cust, string direktoratID);
        long GetDirektoratByName(string direktorat);
        List<EmployeeHierarcy> GetSuperior(string email);
        Employee GetAllByEmployeeID(int value);
        Customer GetCustomerByID(long customerGenID);
        string GetWorkflowID(string appsModul, string process,string userLogin, decimal amount);
        string GenerateWorkflowDefault(long workflowHeaderGenID, string creator, string appsNumber);
        string CheckCustomerBlacklist(string customerName);
        void InsertUdc(Udc obj);
        void UpdateUdc(Udc obj);
        List<Employee> DropdownSalesAdvanceSearch(string employeeEmail);
        Tr_Initiate GetSAByIDSA(string linkTo);
        Employee GetAllByEmployeeName(string pICName);
		Tr_Initiate GetSAByIDSAMSG(string linkTo);
        List<CollectionData> GetDocTypeInvoiceCategory();
    }
}
