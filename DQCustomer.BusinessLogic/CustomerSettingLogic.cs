using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic.Services;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess;
using DQCustomer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DQCustomer.BusinessLogic
{
    public class CustomerSettingLogic : ICustomerSettingLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public CustomerSettingLogic(string connectionstring, string apiGateway)
        {
            this._context = new DapperContext(connectionstring);
            genericAPI = new GenericAPI(apiGateway);
        }

        private ResultAction MessageResult(bool bSuccess, string message)
        {
            return MessageResult(bSuccess, message, null);
        }

        private ResultAction MessageResult(bool bSuccess, string message, object objResult)
        {
            ResultAction result = new ResultAction()
            {
                bSuccess = bSuccess,
                ErrorNumber = (bSuccess ? "0" : "666"),
                Message = message,
                ResultObj = objResult
            };

            return result;

        }

        public ResultAction GetAllCustomerSetting()
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetAll();
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetCustomerSettingBySalesID(long customerID, long SalesID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCustomerSettingBySalesID(customerID, SalesID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public CpCustomerSettingEnvelope GetCustomerSettingNoNamedAccount(int page, int pageSize, string column, string sorting, string search, bool? blacklist = null, bool? holdshipment = null)
        {
            CpCustomerSettingEnvelope result = new CpCustomerSettingEnvelope();

            if (sorting != null)
            {
                if (sorting.ToLower() == "descending")
                    sorting = "desc";
                if (sorting.ToLower() == "ascending")
                    sorting = "asc";
            }

            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);

                var noNamed = uow.CustomerSettingRepository.GetCustomerSettingNoNamedAccount(search, blacklist, holdshipment);

                var relatedLastProject = uow.CustomerSettingRepository.GetRelatedAndLast();

                var softwareDashboards = (from x in noNamed
                                          join y in relatedLastProject
                                          on x.CustomerID equals y.CustomerID into relatedProjects
                                          from y in relatedProjects.DefaultIfEmpty()
                                          select new CpCustomerSettingDashboard
                                          {
                                              CustomerID = x.CustomerID,
                                              CustomerCategory = x.CustomerCategory,
                                              CustomerName = x.CustomerName,
                                              CustomerAddress = x.CustomerAddress,
                                              LastProjectName = (y != null) ? y.LastProjectName : null,
                                              SalesName = x.SalesName,
                                              PMOCustomer = x.PMOCustomer,
                                              RelatedCustomer = (y != null) ? y.RelatedCustomer : null,
                                              Blacklist = x.Blacklist,
                                              Holdshipment = x.Holdshipment,
                                              Named = x.Named,
                                              Shareable = x.Shareable,
                                              CreatedBy = x.CreatedBy,
                                              CreatedDate = x.CreatedDate,
                                              ModifiedBy = x.ModifiedBy,
                                              ModifiedDate = x.ModifiedDate,
                                              RequestedBy = x.RequestedBy,
                                              SalesShareableID = x.SalesShareableID,
                                              ApprovalBy = x.ApprovalBy,
                                              Status = x.Status
                                          }).ToList();

                var resultSoftware = new List<CpCustomerSettingDashboard>();

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    resultSoftware = softwareDashboards;
                }

                result.TotalRows = softwareDashboards.Count();
                result.Column = column;

                if (sorting != null)
                {
                    if (sorting == "desc")
                    {
                        sorting = "descending";
                        result.Rows = resultSoftware.OrderByDescending(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }
                    if (sorting == "asc")
                    {
                        sorting = "ascending";
                        result.Rows = resultSoftware.OrderBy(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }

                    result.Sorting = sorting;
                }
                else
                {
                    result.Rows = resultSoftware.OrderByDescending(c => c.CreatedDate).ToList();
                }
            }

            return result;
        }

        public CpCustomerSettingEnvelope GetCustomerSettingNamedAccount(int page, int pageSize, string column, string sorting, string search, string salesID, long? myAccount = null, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null)
        {
            CpCustomerSettingEnvelope result = new CpCustomerSettingEnvelope();

            if (sorting != null)
            {
                if (sorting.ToLower() == "descending")
                    sorting = "desc";
                if (sorting.ToLower() == "ascending")
                    sorting = "asc";
            }

            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);

                var named = uow.CustomerSettingRepository.GetCustomerSettingNamedAccount(search, salesID, pmoCustomer, blacklist, holdshipment);

                var relatedLastProject = uow.CustomerSettingRepository.GetRelatedAndLast();

                var softwareDashboards = (from x in named
                                          join y in relatedLastProject
                                          on x.CustomerID equals y.CustomerID into relatedProjects
                                          from y in relatedProjects.DefaultIfEmpty()
                                          select new CpCustomerSettingDashboard
                                          {
                                              CustomerID = x.CustomerID,
                                              CustomerCategory = x.CustomerCategory,
                                              CustomerName = x.CustomerName,
                                              CustomerAddress = x.CustomerAddress,
                                              LastProjectName = (y != null) ? y.LastProjectName : null,
                                              SalesName = x.SalesName,
                                              PMOCustomer = x.PMOCustomer,
                                              RelatedCustomer = (y != null) ? y.RelatedCustomer : null,
                                              Blacklist = x.Blacklist,
                                              Holdshipment = x.Holdshipment,
                                              Named = x.Named,
                                              Shareable = x.Shareable,
                                              CreatedBy = x.CreatedBy,
                                              CreatedDate = x.CreatedDate,
                                              ModifiedBy = x.ModifiedBy,
                                              ModifiedDate = x.ModifiedDate,
                                              RequestedBy = x.RequestedBy,
                                              SalesShareableID = x.SalesShareableID,
                                              ApprovalBy = x.ApprovalBy,
                                              Status = x.Status
                                          }).ToList();

                var resultSoftware = new List<CpCustomerSettingDashboard>();
                if (myAccount != null)
                {
                    softwareDashboards = softwareDashboards.Where(x => x.ApprovalBy == myAccount && x.Status == "Pending").ToList();
                }

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    resultSoftware = softwareDashboards;
                }

                result.TotalRows = softwareDashboards.Count();
                result.Column = column;

                if (sorting != null)
                {
                    if (sorting == "desc")
                    {
                        sorting = "descending";
                        result.Rows = resultSoftware.OrderByDescending(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }
                    if (sorting == "asc")
                    {
                        sorting = "ascending";
                        result.Rows = resultSoftware.OrderBy(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }

                    result.Sorting = sorting;
                }
                else
                {
                    result.Rows = resultSoftware.OrderByDescending(c => c.CreatedDate).ToList();
                }
            }

            return result;
        }

        public CpCustomerSettingEnvelope GetCustomerSettingShareableAccount(int page, int pageSize, string column, string sorting, string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null)
        {
            CpCustomerSettingEnvelope result = new CpCustomerSettingEnvelope();

            if (sorting != null)
            {
                if (sorting.ToLower() == "descending")
                    sorting = "desc";
                if (sorting.ToLower() == "ascending")
                    sorting = "asc";
            }

            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);

                var shareable = uow.CustomerSettingRepository.GetCustomerSettingShareableAccount(search, salesID, pmoCustomer, blacklist, holdshipment);

                var relatedLastProject = uow.CustomerSettingRepository.GetRelatedAndLast();

                var softwareDashboards = (from x in shareable
                                          join y in relatedLastProject
                                          on x.CustomerID equals y.CustomerID into relatedProjects
                                          from y in relatedProjects.DefaultIfEmpty()
                                          select new CpCustomerSettingDashboard
                                          {
                                              CustomerID = x.CustomerID,
                                              CustomerCategory = x.CustomerCategory,
                                              CustomerName = x.CustomerName,
                                              CustomerAddress = x.CustomerAddress,
                                              LastProjectName = (y != null) ? y.LastProjectName : null,
                                              SalesName = x.SalesName,
                                              PMOCustomer = x.PMOCustomer,
                                              RelatedCustomer = (y != null) ? y.RelatedCustomer : null,
                                              Blacklist = x.Blacklist,
                                              Holdshipment = x.Holdshipment,
                                              Named = x.Named,
                                              Shareable = x.Shareable,
                                              CreatedBy = x.CreatedBy,
                                              CreatedDate = x.CreatedDate,
                                              ModifiedBy = x.ModifiedBy,
                                              ModifiedDate = x.ModifiedDate,
                                              RequestedBy = x.RequestedBy,
                                              SalesShareableID = x.SalesShareableID,
                                              ApprovalBy = x.ApprovalBy,
                                              Status = x.Status
                                          }).ToList();

                var resultSoftware = new List<CpCustomerSettingDashboard>();

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    resultSoftware = softwareDashboards;
                }

                result.TotalRows = softwareDashboards.Count();
                result.Column = column;

                if (sorting != null)
                {
                    if (sorting == "desc")
                    {
                        sorting = "descending";
                        result.Rows = resultSoftware.OrderByDescending(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }
                    if (sorting == "asc")
                    {
                        sorting = "ascending";
                        result.Rows = resultSoftware.OrderBy(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }

                    result.Sorting = sorting;
                }
                else
                {
                    result.Rows = resultSoftware.OrderByDescending(c => c.CreatedDate).ToList();
                }
            }

            return result;
        }
        public CpCustomerSettingEnvelope GetCustomerSettingAllAccount(int page, int pageSize, string column, string sorting, string search, string salesID, long? myAccount = null, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null, bool? showNoName = null, bool? showNamed = null, bool? showShareable = null)
        {
            CpCustomerSettingEnvelope result = new CpCustomerSettingEnvelope();

            if (sorting != null)
            {
                if (sorting.ToLower() == "descending")
                    sorting = "desc";
                if (sorting.ToLower() == "ascending")
                    sorting = "asc";
            }

            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);

                var allAccount = uow.CustomerSettingRepository.GetCustomerSettingAllAccount(search, salesID, pmoCustomer, blacklist, holdshipment);

                var relatedLastProject = uow.CustomerSettingRepository.GetRelatedAndLast();

                var softwareDashboards = (from x in allAccount
                                          join y in relatedLastProject
                                          on x.CustomerID equals y.CustomerID into relatedProjects
                                          from y in relatedProjects.DefaultIfEmpty()
                                          select new CpCustomerSettingDashboard
                                          {
                                              CustomerID = x.CustomerID,
                                              CustomerCategory = x.CustomerCategory,
                                              CustomerName = x.CustomerName,
                                              CustomerAddress = x.CustomerAddress,
                                              LastProjectName = (y != null) ? y.LastProjectName : null,
                                              SalesName = x.SalesName,
                                              PMOCustomer = x.PMOCustomer,
                                              RelatedCustomer = (y != null) ? y.RelatedCustomer : null,
                                              Blacklist = x.Blacklist,
                                              Holdshipment = x.Holdshipment,
                                              Named = x.Named,
                                              Shareable = x.Shareable,
                                              CreatedBy = x.CreatedBy,
                                              CreatedDate = x.CreatedDate,
                                              ModifiedBy = x.ModifiedBy,
                                              ModifiedDate = x.ModifiedDate,
                                              RequestedBy = x.RequestedBy,
                                              SalesShareableID = x.SalesShareableID,
                                              ApprovalBy = x.ApprovalBy
                                          }).ToList();

                var noName = (showNoName ?? true) ? softwareDashboards.Where(x => x.Named == false && x.Shareable == false).ToList() : new List<CpCustomerSettingDashboard>();
                var Named = (showNamed ?? true) ? softwareDashboards.Where(x => x.Named == true && x.Shareable == false).ToList() : new List<CpCustomerSettingDashboard>();
                var shareable = (showShareable ?? true) ? softwareDashboards.Where(x => x.Named == false && x.Shareable == true).ToList() : new List<CpCustomerSettingDashboard>();

                var mergedList = noName.Concat(Named).Concat(shareable).ToList();
                if (myAccount != null)
                {
                    mergedList = mergedList.Where(x => x.ApprovalBy == myAccount).ToList();
                }
                var resultSoftware = new List<CpCustomerSettingDashboard>();

                if (page > 0)
                {
                    var queryable = mergedList.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    resultSoftware = mergedList;
                }

                result.TotalRows = mergedList.Count();
                result.Column = column;

                if (sorting != null)
                {
                    if (sorting == "desc")
                    {
                        sorting = "descending";
                        result.Rows = resultSoftware.OrderByDescending(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }
                    if (sorting == "asc")
                    {
                        sorting = "ascending";
                        result.Rows = resultSoftware.OrderBy(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                    }

                    result.Sorting = sorting;
                }
                else
                {
                    result.Rows = resultSoftware.OrderByDescending(c => c.CreatedDate).ToList();
                }
            }

            return result;
        }

        public ResultAction Insert(CpCustomerSetting objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existing = uow.CustomerSettingRepository.GetCustomerSettingByCustomerID(objEntity.CustomerID);
                    var alreadyAssign = uow.SalesHistoryRepository.GetAll().FirstOrDefault(x => x.CustomerID == objEntity.CustomerID && x.SalesID == objEntity.SalesID && x.Status == "Assign");
                    if (alreadyAssign != null)
                    {
                        return result = MessageResult(false, "Already assigned");
                    }
                    CpSalesHistory newSalesHistory = new CpSalesHistory()
                    {
                        SalesID = objEntity.SalesID,
                        CustomerID = objEntity.CustomerID,
                        CreateDate = DateTime.Now,
                        RequestedDate = DateTime.Now,
                        RequestedBy = objEntity.RequestedBy,
                        CreateUserID = objEntity.CreateUserID,
                    };

                    if (existing.Count == 0)
                    {
                        CpCustomerSetting newCustomerSetting = new CpCustomerSetting()
                        {
                            CustomerID = objEntity.CustomerID,
                            SalesID = objEntity.SalesID,
                            Named = true,
                            Shareable = false,
                            CreateUserID = objEntity.CreateUserID,
                            CreateDate = DateTime.Now,
                            RequestedBy = objEntity.RequestedBy,
                            RequestedDate = DateTime.Now,
                            PMOCustomer = false,
                        };
                        uow.CustomerSettingRepository.Add(newCustomerSetting);
                        newSalesHistory.Status = "Assign";
                        uow.SalesHistoryRepository.Add(newSalesHistory);
                        result = MessageResult(true, "Insert Success!");
                    }
                    else if (existing.Count == 1)
                    {
                        var existingSalesHistory = uow.SalesHistoryRepository.GetAll().FirstOrDefault(x => x.CustomerID == objEntity.CustomerID && x.Status == "Pending");
                        if (existingSalesHistory != null)
                        {
                            return result = MessageResult(false, "Already have pending request!");
                        }
                        var approvalID = uow.CustomerSettingRepository.GetApprovalID();
                        newSalesHistory.Status = "Pending";
                        newSalesHistory.ApprovalBy = approvalID;
                        uow.SalesHistoryRepository.Add(newSalesHistory);
                        //uow.CustomerSettingRepository.SendEmailReqCustomerSetting(objEntity.CustomerID, objEntity.SalesID, approvalID);
                        result = MessageResult(true, "Wait for Approval!");
                    }
                    else if (existing.Count > 1)
                    {
                        var customerSetting = existing.FirstOrDefault(x => x.CustomerID == objEntity.CustomerID);
                        CpCustomerSetting newCustomerSetting = new CpCustomerSetting()
                        {
                            CustomerID = objEntity.CustomerID,
                            SalesID = objEntity.SalesID,
                            Named = false,
                            Shareable = true,
                            CreateUserID = customerSetting.CreateUserID,
                            CreateDate = customerSetting.CreateDate,
                            RequestedBy = objEntity.RequestedBy,
                            RequestedDate = DateTime.Now,
                            PMOCustomer = customerSetting.PMOCustomer,
                            ModifyUserID = objEntity.CreateUserID,
                            ModifyDate = DateTime.Now,
                            CustomerCategory = customerSetting.CustomerCategory
                        };
                        uow.CustomerSettingRepository.Add(newCustomerSetting);
                        uow.CustomerSettingRepository.UpdateAllCustomerSetting(objEntity.CustomerID, newCustomerSetting);
                        newSalesHistory.Status = "Assign";
                        uow.SalesHistoryRepository.Add(newSalesHistory);
                        result = MessageResult(true, "Insert Success!");
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The EXECUTE permission was denied on the object 'sp_send_dbmail', database 'msdb', schema 'dbo'.") || ex.Message.Contains("String or binary data would be truncated."))
                    result = MessageResult(true, "Success!");
                else
                    result = MessageResult(false, ex.Message);
            }

            return result;
        }
        public ResultAction ReleaseAccount(long customerID, long salesID, int? modifyUserID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existing = uow.CustomerSettingRepository.GetCustomerSettingBySalesID(customerID, salesID);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found!");
                    }

                    var salesHistory = uow.SalesHistoryRepository.GetAll().FirstOrDefault(x => x.CustomerID == customerID && x.SalesID == salesID && x.Status == "Assign");
                    salesHistory.Status = "Release";
                    salesHistory.ModifyUserID = modifyUserID;
                    salesHistory.ModifyDate = DateTime.Now;
                    uow.SalesHistoryRepository.Update(salesHistory);

                    uow.CustomerSettingRepository.DeleteCustomerSettingBySalesID(customerID, salesID);

                    var listCustomerSetting = uow.CustomerSettingRepository.GetCustomerSettingByCustomerID(customerID);

                    if (listCustomerSetting.Count == 1)
                    {
                        var cs = listCustomerSetting.First();
                        cs.Named = true;
                        cs.Shareable = false;
                        cs.ModifyDate = existing.ModifyDate;
                        cs.ModifyUserID = modifyUserID;
                        uow.CustomerSettingRepository.UpdateAllCustomerSetting(customerID, cs);
                    }

                    result = MessageResult(true, "Update Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction ApproveCustomerSetting(long customerID, long salesID, bool isApprove, string description, int? modifyUserID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.SalesHistoryRepository.GetAll().OrderByDescending(y => y.CreateDate).FirstOrDefault(x => x.CustomerID == customerID && x.SalesID == salesID);
                    if (existing == null)
                    {
                        return MessageResult(false, "Data not found!");
                    }
                    if (!isApprove)
                    {
                        existing.Status = "Rejected";
                        existing.Description = description;
                    }
                    else
                    {
                        existing.Status = "Assign";
                        var customerSetting = uow.CustomerSettingRepository.GetAll().FirstOrDefault(x => x.CustomerID == customerID);
                        CpCustomerSetting newCustomerSetting = new CpCustomerSetting()
                        {
                            CustomerID = existing.CustomerID,
                            SalesID = existing.SalesID,
                            Named = false,
                            Shareable = true,
                            CreateUserID = customerSetting.CreateUserID,
                            CreateDate = customerSetting.CreateDate,
                            RequestedBy = existing.RequestedBy,
                            RequestedDate = existing.RequestedDate,
                            PMOCustomer = customerSetting.PMOCustomer,
                            ModifyUserID = modifyUserID,
                            ModifyDate = DateTime.Now,
                            CustomerCategory = customerSetting.CustomerCategory
                        };
                        uow.CustomerSettingRepository.Add(newCustomerSetting);
                        uow.CustomerSettingRepository.UpdateAllCustomerSetting(customerID, newCustomerSetting);
                    }
                    existing.ModifyUserID = modifyUserID;
                    existing.ModifyDate = DateTime.Now;
                    uow.SalesHistoryRepository.Update(existing);
                    //uow.CustomerSettingRepository.SendEmailApproveRejectCustomerSetting(customerID, salesID, isApprove, description, modifyUserID);
                    result = MessageResult(true, "Success!");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The EXECUTE permission was denied on the object 'sp_send_dbmail', database 'msdb', schema 'dbo'.") || ex.Message.Contains("String or binary data would be truncated."))
                    result = MessageResult(true, "Success!");
                else
                    result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetCustomerPICByCustomerID(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCustomerPICByCustomerID(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetBrandSummary(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetBrandSummary(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetServiceSummary(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetServiceSummary(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetProjectHistory(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetProjectHistory(customerID);
                    List<Req_CustomerSettingGetProjectHistoryEnvelope_ViewModel> project = existing.Select(item => new Req_CustomerSettingGetProjectHistoryEnvelope_ViewModel
                    {
                        FunnelID = item.FunnelID,
                        SO = item.SO,
                        ProjectName = item.ProjectName,
                        CustomerName = item.CustomerName,
                        SalesName = item.SalesName,
                        SalesDept = item.SalesDept,
                        SOCloseDate = item.SOCloseDate,
                        SOAmount = item.SOAmount,
                        SuccessStory = item.SuccessStory,
                        ModifiedStoryBy = uow.CustomerSuccessStoryRepository.GetCustomerStoriesByCustomerID(item.FunnelID).ToList()
                    }).ToList();
                    result = MessageResult(true, "Success", project);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetCustomerDataByID(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCustomerDataByID(customerID);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found!");
                    }
                    Req_CustomerSettingCustomerDataEnvelope_ViewModel envelope = new Req_CustomerSettingCustomerDataEnvelope_ViewModel();
                    envelope.AccountStatus = existing.AccountStatus;
                    envelope.CustomerID = existing.CustomerID;
                    envelope.CustomerName = existing.CustomerName;
                    envelope.AvgAR = existing.AvgAR;
                    envelope.PMOCustomer = existing.PMOCustomer;
                    envelope.Holdshipment = existing.Holdshipment;
                    envelope.Blacklist = existing.Blacklist;
                    envelope.SalesName = existing.SalesName;
                    envelope.CustomerAddress = existing.CustomerAddress;
                    envelope.CustomerCategory = existing.CustomerCategory;
                    var shareable = uow.SalesHistoryRepository.GetShareableStatus(customerID);
                    envelope.ShareableApprovalStatus = shareable;
                    result = MessageResult(true, "Success", envelope);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetSalesData()
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetListSales();
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetCustomerCategory()
        {
            ResultAction result = new ResultAction();
            try
            {
                // var udc = genericAPI.GetByEntryKey("CustomerCategory");
                // List<string> existing = null;
                // if (udc.Count > 1)
                // {
                //     if (!string.IsNullOrEmpty(udc.First().Text1))
                //     {
                //         existing = udc.Select(x => x.Text1).ToList();
                //     }
                // }
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCustomerCategory();
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetConfigItem(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetConfigItem(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetCollectionHistory(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCollectionHistory(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetSalesByName(string salesName)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetSalesByName(salesName);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetCustomerName(string customerName)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCustomerByName(customerName);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction Update(long customerID, CpCustomerSetting objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    objEntity.ModifyDate = DateTime.Now;
                    uow.CustomerSettingRepository.UpdateSpecificCustomerSetting(customerID, objEntity);
                    result = MessageResult(true, "Update Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public CpCustomerSettingSearchRequest GetSearchRequest(int page, int pageSize, string column, string sorting, string titleCustomer, string customerName, string picName)
        {
            //ResultAction result = new ResultAction();
            //try
            //{
            //    using (_context)
            //    {
            //        IUnitOfWork uow = new UnitOfWork(_context);
            //        var existing = uow.CustomerSettingRepository.GetSearchRequest(titleCustomer, customerName, picName);
            //        result = MessageResult(true, "Success", existing);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result = MessageResult(false, ex.Message);
            //}


            CpCustomerSettingSearchRequest result = new CpCustomerSettingSearchRequest();

            if (sorting != null)
            {
                if (sorting.ToLower() == "descending")
                    sorting = "desc";
                if (sorting.ToLower() == "ascending")
                    sorting = "asc";
            }

            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existing = uow.CustomerSettingRepository.GetSearchRequest(titleCustomer, customerName, picName);


                    var data = (from x in existing
                                select new Req_CustomerSearchRequest_ViewModel
                                {
                                    CustomerID = x.CustomerID,
                                    TitleCustomer = x.TitleCustomer,
                                    CustomerName = x.CustomerName,
                                    PICName = x.PICName
                                }).ToList();

                    var resultData = new List<Req_CustomerSearchRequest_ViewModel>();

                    if (page > 0)
                    {
                        var queryable = data.AsQueryable();
                        resultData = queryable
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                    }
                    else
                    {
                        resultData = data;
                    }

                    result.TotalRows = data.Count();
                    result.Column = column;

                    if (sorting != null)
                    {
                        if (sorting == "desc")
                        {
                            sorting = "descending";
                            result.Rows = resultData.OrderByDescending(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                        }
                        if (sorting == "asc")
                        {
                            sorting = "ascending";
                            result.Rows = resultData.OrderBy(x => x.GetType().GetProperty(column).GetValue(x, null)).ToList();
                        }

                        result.Sorting = sorting;
                    }
                    else
                    {
                        result.Rows = resultData.OrderByDescending(c => c.CustomerID).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public ResultAction InsertRequestNewCustomer(Req_CustomerSettingInsertRequestCustomer_ViewModel objEntity)
        {

            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    Req_CustomerSettingInsertRequestCustomer_ViewModel newCustomer = new Req_CustomerSettingInsertRequestCustomer_ViewModel()
                    {
                        TitleCustomer = objEntity.TitleCustomer,
                        CustomerName = objEntity.CustomerName,
                        PICName = objEntity.PICName,
                        CustomerAddress = objEntity.CustomerAddress,
                        PhoneNumber = objEntity.PhoneNumber,
                        IndustryClass = objEntity.IndustryClass,
                        Website = objEntity.Website,
                        SocialMedia = objEntity.SocialMedia,
                        PICPhoneNumber = objEntity.PICPhoneNumber,
                        PICJobTitle = objEntity.PICJobTitle,
                        PICEmail = objEntity.PICEmail,
                        CreatedUserID = objEntity.CreatedUserID,
                        ModifyUserID = objEntity.ModifyUserID
                    };

                    uow.CustomerSettingRepository.InsertRequestNewCustomer(newCustomer);
                    result = MessageResult(true, "Insert Success!");

                }

            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }

            return result;

        }

        public ResultAction GetRequestNewCustomerByGenID(long customerGenID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existing = uow.CustomerSettingRepository.GetRequestNewCustomerByGenID(customerGenID);

                    // Manipulasi nilai properti "titleCustomer" dan "customerName"
                    foreach (var item in existing)
                    {
                        // Split nilai "customerName" berdasarkan ", " dan simpan ke dalam array
                        string[] customerNameParts = item.CustomerName.ToString().Split(new string[] { ", " }, StringSplitOptions.None);

                        item.TitleCustomer = customerNameParts[customerNameParts.Length - 1];
                        //item.CustomerName = customerNameParts[0]; 

                        // Konversi format singkat bulan menjadi format lengkap bulan
                        string formattedDate = item.CreateDate; 
                        DateTime date = DateTime.ParseExact(formattedDate, "dd MMM yyyy", CultureInfo.InvariantCulture);
                        string formattedDateFullMonth = date.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture);

                        item.CreateDate = formattedDateFullMonth;
                    }

                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction UpdateApprovalStatusNewCustomer(long customerGenID, Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel dataUpdate = new Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel()
                    {
                        ApprovalStatus = objEntity.ApprovalStatus,
                        Remark = objEntity.Remark
                    };

                    var responseData = new
                    {
                        approvalStatus = dataUpdate.ApprovalStatus.ToUpper(),
                        remark = dataUpdate.Remark
                    };

                    uow.CustomerSettingRepository.UpdateApprovalStatusNewCustomer(customerGenID, dataUpdate);
                    result = MessageResult(true, "Insert Success!", responseData);

                }

            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }

            return result;
        }
    }
    
}
