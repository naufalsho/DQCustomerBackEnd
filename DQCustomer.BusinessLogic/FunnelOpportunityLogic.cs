using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic.Services;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.Email;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess;
using DQCustomer.DataAccess.Interfaces;
//using DQGeneric.BusinessObject;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DQCustomer.BusinessLogic
{
    public class FunnelOpportunityLogic : IFunnelOpportunityLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public FunnelOpportunityLogic(string connectionstring, string apiGateway)
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

        public SalesFunnelOpportunityEnvelope GetByFunnelGenID(int page, int pageSize, string column, string sorting)
        {
            SalesFunnelOpportunityEnvelope result = new SalesFunnelOpportunityEnvelope();
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
                var softwareDashboards = uow.FunnelOpportunityRepository.GetListDashboard();

                var resultSoftware = new List<SalesFunnelOpportunityDashboard>();

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
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
                    result.Rows = resultSoftware.OrderByDescending(c => c.CreateDate).ToList();
                }
            }
            return result;

        }


        public ResultAction Update(Req_FunnelOpportunityUpdate_ViewModel objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.FunnelOpportunityRepository.GetOpportunityByID(objEntity.FunnelOpportunityID);
                    var funnelOpp = MappingUpdate(objEntity, existing);
                    uow.FunnelOpportunityRepository.Update(funnelOpp);
                    result = MessageResult(true, "Update Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        private SalesFunnelOpportunity MappingUpdate(Req_FunnelOpportunityUpdate_ViewModel objEntity, SalesFunnelOpportunity existing)
        {
            existing.EventName = objEntity.EventName;
            existing.CustomerGenID = objEntity.CustomerGenID;
            //existing.Status = objEntity.Status;
            existing.Status = "NEW";
            existing.BrandID = objEntity.BrandID;
            existing.Notes = objEntity.Notes;
            existing.SalesID = objEntity.SalesID;
            existing.ModifyUserID = objEntity.UserLoginID;
            existing.ModifyDate = DateTime.Now;
            existing.CreateDate = DateTime.Now;
            existing.EventDate = objEntity.EventDate;

            return existing;
        }

        public ResultAction Insert(SalesFunnelOpportunity objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    string sValidate = ValidateFunnelOpportunity(objEntity, uow);
                    if (string.IsNullOrEmpty(sValidate))
                    {
                        objEntity.CreateDate = DateTime.Now;
                        objEntity.Status = "NEW";
                        if (objEntity.FunnelID == 0)
                            objEntity.FunnelID = null;
                        //GetItemName(objEntity);
                        uow.FunnelOpportunityRepository.Add(objEntity);
                        //var email = MappingEmailInsert(uow, objEntity);
                        //_funnelCommandLogic.SendEmailInsertOpportunity(email);
                        result = MessageResult(true, "Update Success!");
                    }
                    else
                    {
                        result = MessageResult(false, sValidate);
                    }

                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        //private SalesFunnelOpportunityEmailMapperCommand MappingEmailInsert(IUnitOfWork uow, SalesFunnelOpportunity objEntity)
        //{
        //    SalesFunnelOpportunityEmailMapperCommand result = new SalesFunnelOpportunityEmailMapperCommand();
        //    result.SalesFunnelOpportunityEmail = MappingOpportunityEmail(objEntity);
        //    result.Email = MappingEmailAddr(objEntity);

        //    return result;
        //}

        //private EmailAddr MappingEmailAddr(SalesFunnelOpportunity objEntity)
        //{
        //    EmailAddr result = new EmailAddr();
        //    result.CreatorEmail = genericAPI.GetByEmployeeID(objEntity.CreateUserID).EmployeeEmail;
        //    result.PresalesEmail = genericAPI.GetByEmployeeID(objEntity.SalesID.Value).EmployeeEmail;

        //    return result;
        //}

        private SalesFunnelOpportunityEmail MappingOpportunityEmail(SalesFunnelOpportunity objEntity)
        {
            SalesFunnelOpportunityEmail result = new SalesFunnelOpportunityEmail();
            result.Brand = genericAPI.GetUdcByID(Convert.ToInt32(objEntity.BrandID.Value)).Text1;
            result.CreateUserID = genericAPI.GetEmployeeName(objEntity.CreateUserID);

            if (objEntity.CustomerGenID != null || objEntity.CustomerGenID > 0)
                result.CustomerName = genericAPI.GetCustomerName(objEntity.CustomerGenID.Value);

            result.EventName = objEntity.EventName;
            result.FunnelID = objEntity.FunnelID.ToString();
            result.FunnelOpportunityID = objEntity.FunnelOpportunityID;
            result.Note = objEntity.Notes;

            if (objEntity.SalesID != null || objEntity.SalesID > 0)
                result.SalesName = genericAPI.GetEmployeeName(objEntity.SalesID.Value);

            result.Status = objEntity.Status;

            return result;
        }

        private string ValidateFunnelOpportunity(SalesFunnelOpportunity objEntity, IUnitOfWork uow)
        {
            if (objEntity.CustomerGenID == 0)
                return "Harap pilih Customer sesuai dengan pilihan yang muncul di kolom pencarian Customer!";

            return string.Empty;
        }

        public ResultAction InsertUpload(Req_FunnelOpportunityInsertUpload_ViewModel objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                List<SalesFunnelOpportunityExcel> salesOpp = new List<SalesFunnelOpportunityExcel>();
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    SaveTemp(objEntity.File);

                    var filePath = Path.Combine(Path.GetTempPath(), objEntity.File.FileName);
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    FileStream stream = File.Open(filePath, FileMode.Open);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    var res = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTable dataTable = res.Tables[0];
                    //var json = JsonConvert.SerializeObject(dataTable);
                    List<SalesFunnelOpportunityExcel> salesOppExcel = MappingSalesOppExcel(dataTable);
                    salesOpp = SaveSalesOpportunity(uow, salesOppExcel, objEntity.UserID);
                    stream.Close();
                    excelReader.Close();

                    File.Delete(filePath);
                    if (salesOpp.Count > 0)
                    {
                        result = MessageResult(true, "Insert From Upload Success and Error!", salesOpp);
                    }
                    else
                    {
                        result = MessageResult(true, "Insert From Upload Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                SalesFunnelOpportunityExcel salesOpp = new SalesFunnelOpportunityExcel();
                salesOpp.ErrorMessage.Add(ex.Message);
                result = MessageResult(false, ex.Message, salesOpp);
            }
            return result;
        }

        private List<SalesFunnelOpportunityExcel> SaveSalesOpportunity(IUnitOfWork uow, List<SalesFunnelOpportunityExcel> salesOppExcel, int userID)
        {
            List<SalesFunnelOpportunityExcel> result = new List<SalesFunnelOpportunityExcel>();
            foreach (var item in salesOppExcel)
            {
                SalesFunnelOpportunity salesOpp = new SalesFunnelOpportunity();
                salesOpp.FunnelOpportunityID = 0;
                salesOpp.CreateDate = DateTime.Now;
                salesOpp.CreateUserID = userID;
                salesOpp.EventName = item.EventName;
                try
                {
                    salesOpp.EventDate = DateTime.Parse(item.EventDate);
                }
                catch
                {
                    item.ErrorMessage = new List<string>();
                    item.ErrorMessage.Add("Format EvenDate tidak sesuai! Format yang disarankan adalah 'dd/mm/yyyy'!");
                }
                salesOpp.CustomerGenID = genericAPI.GetCustomerByName(item.CustomerName);
                salesOpp.Status = "NEW";
                salesOpp.Notes = item.Notes;
                if (salesOpp.CustomerGenID != 0)
                {
                    var direktorat = genericAPI.GetDirektoratByName(item.Direktorat);
                    var sales = genericAPI.GetSalesByLastSA(item.CustomerName, direktorat.ToString());
                    var checkSales = genericAPI.GetByEmployeeID(sales.Value);
                    if (checkSales != null)
                    {
                        salesOpp.SalesID = checkSales.EmployeeID;
                    }
                    else
                    {
                        if (sales > 0)
                        {
                            var checkSalesResign = genericAPI.GetAllByEmployeeID(sales.Value);
                            var getSuperior = genericAPI.GetSuperior(checkSalesResign.EmployeeEmail);
                            var checkSup = genericAPI.GetByEmail(getSuperior.First().EmailAddr);
                            if (checkSup == null)
                                checkSup = genericAPI.GetByEmail(getSuperior[1].EmailAddr);

                            salesOpp.SalesID = checkSup.EmployeeID;
                        }
                        else
                        {
                            salesOpp.SalesID = 3825;
                        }
                    }
                }
                else
                {
                    salesOpp.SalesID = 3825;
                }
                var checkBrand = genericAPI.GetIdBrandByName(item.Brand);
                salesOpp.BrandID = checkBrand;
                if (salesOpp.CustomerGenID == 0 || salesOpp.BrandID == 0)
                {
                    if (item.ErrorMessage != null || item.ErrorMessage.Count == 0)
                        item.ErrorMessage = new List<string>();

                    if (salesOpp.CustomerGenID == 0)
                        item.ErrorMessage.Add("Nama Customer Tidak Ada!");
                    if (salesOpp.BrandID == 0)
                        item.ErrorMessage.Add("Brand tidak ada!");
                    result.Add(item);
                }
                else
                {
                    uow.FunnelOpportunityRepository.Add(salesOpp);
                }

            }

            return result;
        }

        private List<SalesFunnelOpportunityExcel> MappingSalesOppExcel(DataTable dataTable)
        {
            var result = (from row in dataTable.AsEnumerable()
                          select new SalesFunnelOpportunityExcel()
                          {
                              EventName = Convert.ToString(row["EventName"]),
                              EventDate = Convert.ToString(row["EventDate"]),
                              CustomerName = Convert.ToString(row["CustomerName"]),
                              Direktorat = Convert.ToString(row["Direktorat"]),
                              Brand = Convert.ToString(row["Brand"]),
                              Notes = Convert.ToString(row["Notes"])
                          }).ToList();
            return result;
        }

        private void SaveTemp(IFormFile obj)
        {
            var filePath = Path.Combine(Path.GetTempPath(), obj.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                obj.CopyTo(fileStream);
            }
        }

        public SalesFunnelOpportunityView GetByOpportunityID(long funnelOpportunityID)
        {
            SalesFunnelOpportunityView result = new SalesFunnelOpportunityView();
            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);
                var opportunity = uow.FunnelOpportunityRepository.GetOpportunityByID(funnelOpportunityID);
                result = MappingOpportunityView(opportunity, uow);
            }

            return result;
        }

        private SalesFunnelOpportunityView MappingOpportunityView(SalesFunnelOpportunity opportunity, IUnitOfWork uow)
        {
            SalesFunnelOpportunityView result = new SalesFunnelOpportunityView();
            result.BrandID = opportunity.BrandID;
            result.CreateDate = opportunity.CreateDate;
            result.CreateUserID = opportunity.CreateUserID;
            result.CustomerGenID = opportunity.CustomerGenID;
            result.Direktorat = genericAPI.GetByEmployeeID(opportunity.SalesID.Value).DeptID.Substring(0, 3) + "0000000";
            result.EventDate = opportunity.EventDate;
            result.EventName = opportunity.EventName;
            result.FunnelID = opportunity.FunnelID;
            result.FunnelOpportunityID = opportunity.FunnelOpportunityID;
            result.ModifyDate = opportunity.ModifyDate;
            result.ModifyUserID = opportunity.ModifyUserID;
            result.Notes = opportunity.Notes;
            result.SalesID = opportunity.SalesID;
            result.Status = opportunity.Status;

            return result;
        }

        public SalesFunnelOpportunityEnvelope GetListOpportunityByUserID(int page, int pageSize, int userLoginID)
        {
            SalesFunnelOpportunityEnvelope result = new SalesFunnelOpportunityEnvelope();
            List<SalesFunnelOpportunityDashboard> softwareDashboards = new List<SalesFunnelOpportunityDashboard>();
            var hirarki = "";
            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);
                if (userLoginID == 807)
                {
                    hirarki = userLoginID.ToString();
                    softwareDashboards = uow.FunnelOpportunityRepository.GetListDashboardBySalesID(hirarki);
                }
                else
                {
                    hirarki = GetHirarki(userLoginID);
                    softwareDashboards = uow.FunnelOpportunityRepository.GetListDashboardBySalesID(hirarki);
                }

                var resultSoftware = new List<SalesFunnelOpportunityDashboard>();

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
                }
                else
                {
                    resultSoftware = softwareDashboards;
                }

                result.TotalRows = softwareDashboards.Count();
                result.Rows = resultSoftware.OrderByDescending(c => c.CreateDate).ToList();
            }
            return result;
        }

        private string GetHirarki(int salesID)
        {
            var emp = genericAPI.GetByEmployeeID(salesID);
            var listEmp = genericAPI.GetListSubordinate(emp.EmployeeEmail.Substring(0, emp.EmployeeEmail.IndexOf("@")));
            var hirarki = salesID.ToString();

            if (listEmp.Count > 0)
                hirarki = string.Join(",", listEmp.Select(c => c.EmployeeID));

            return hirarki;
        }

        public SalesFunnelOpportunityEnvelope Search(int page, int pageSize, int userLoginID, string search)
        {
            SalesFunnelOpportunityEnvelope result = new SalesFunnelOpportunityEnvelope();
            var hirarki = GetHirarki(userLoginID);
            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);
                var softwareDashboards = uow.FunnelOpportunityRepository.Search(hirarki, search);

                var resultSoftware = new List<SalesFunnelOpportunityDashboard>();

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
                }
                else
                {
                    resultSoftware = softwareDashboards;
                }

                result.TotalRows = softwareDashboards.Count();
                result.Rows = resultSoftware.OrderByDescending(c => c.CreateDate).ToList();
            }
            return result;
        }

        public SalesFunnelOpportunityEnvelope SearchMarketing(int page, int pageSize, string search)
        {
            SalesFunnelOpportunityEnvelope result = new SalesFunnelOpportunityEnvelope();
            using (_context)
            {
                IUnitOfWork uow = new UnitOfWork(_context);
                var softwareDashboards = uow.FunnelOpportunityRepository.SearchMarketiing(search);

                var resultSoftware = new List<SalesFunnelOpportunityDashboard>();

                if (page > 0)
                {
                    var queryable = softwareDashboards.AsQueryable();
                    resultSoftware = queryable
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
                }
                else
                {
                    resultSoftware = softwareDashboards;
                }

                result.TotalRows = softwareDashboards.Count();
                result.Rows = resultSoftware.OrderByDescending(c => c.CreateDate).ToList();
            }
            return result;
        }

        public ResultAction Reassign(Req_FunnelOpportunityReassign_ViewModel objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.FunnelOpportunityRepository.GetOpportunityByID(objEntity.FunnelOpportunityID);
                    existing.SalesID = objEntity.SalesID;
                    existing.ModifyDate = DateTime.Now;
                    existing.ModifyUserID = objEntity.UserLoginID;
                    uow.FunnelOpportunityRepository.Update(existing);

                    //var email = MappingEmailReassign(uow, objEntity, existing);
                    //_funnelCommandLogic.SendEmailReassignOpportunity(email);
                    result = MessageResult(true, "Update Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        //private SalesFunnelOpportunityEmailMapperCommand MappingEmailReassign(IUnitOfWork uow, Req_FunnelOpportunityReassign_ViewModel objEntity, SalesFunnelOpportunity existing)
        //{
        //    SalesFunnelOpportunityEmailMapperCommand result = new SalesFunnelOpportunityEmailMapperCommand();
        //    result.SalesFunnelOpportunityEmail = MappingOpportunityEmail(existing);
        //    result.Email = MappingEmailAddrReassign(objEntity);

        //    return result;
        //}

        //private EmailAddr MappingEmailAddrReassign(Req_FunnelOpportunityReassign_ViewModel objEntity)
        //{
        //    EmailAddr result = new EmailAddr();
        //    result.CreatorEmail = genericAPI.GetByEmployeeID(objEntity.UserLoginID).EmployeeEmail;
        //    result.PresalesEmail = genericAPI.GetByEmployeeID(objEntity.SalesID.Value).EmployeeEmail;

        //    return result;
        //}

        public ResultAction Delete(long funnelOpportunityID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var opp = uow.FunnelOpportunityRepository.GetOpportunityByID(funnelOpportunityID);
                    if (opp != null)
                    {
                        if (opp.FunnelID == null)
                        {
                            uow.FunnelOpportunityRepository.Delete(opp);
                            result = MessageResult(true, "Delete success!");
                        }
                        else
                        {
                            result = MessageResult(true, "Already have Funnel ID!");
                        }
                    }
                    else
                        result = MessageResult(false, "Data not found!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction Cancel(long funnelOpportunityID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.FunnelOpportunityRepository.GetOpportunityByID(funnelOpportunityID);
                    existing.Status = "CANCEL";
                    uow.FunnelOpportunityRepository.Update(existing);
                    result = MessageResult(true, "Update Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction ResendEmailInsertOpportunity(long funnelOpportunityID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var objEntity = uow.FunnelOpportunityRepository.GetOpportunityByID(funnelOpportunityID);
                    //var email = MappingEmailInsert(uow, objEntity);
                    //_funnelCommandLogic.SendEmailInsertOpportunity(email);
                    result = MessageResult(true, "Resend Email Success!");
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
