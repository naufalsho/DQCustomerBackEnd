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
    public class AddressOfficeNumberLogic : IAddressOfficeNumberLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public AddressOfficeNumberLogic(string connectionstring, string apiGateway)
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

        public ResultAction GetAddressOfficeNumber()
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAll();
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction Insert(CpAddressOfficeNumber objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existingID = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberById((long)objEntity.CustomerID, (long)objEntity.CustomerGenID);

                    objEntity.Type = objEntity.Type.ToUpper();
                    objEntity.CreateDate = DateTime.Now;
                    objEntity.ModifyDate = DateTime.Now;

                    if (objEntity.CreateUserID == null || objEntity.CreateUserID == 0 ){
                        return MessageResult(false, "CreateUserID field is required!");
                    }

                    if(existingID == null) 
                    {
                        return MessageResult(false, "Data not found!");
                    }

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = (long)objEntity.CustomerID,
                        CustomerGenID = (long)objEntity.CustomerGenID,
                        UserID = objEntity.CreateUserID,
                        Description = "Add new Address"
                    };

                    uow.AddressOfficeNumberRepository.Add(objEntity);
                    var accountActivity = uow.AccountActivityHistoryRepository.InsertAccountActivityHistory(dataAccountActivity);

                    result = MessageResult(true, "Insert Success!", "data : "+ accountActivity);

                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction Update(long Id, CpAddressOfficeNumber objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberById((long)objEntity.CustomerID, (long)objEntity.CustomerGenID).Where(ao => ao.AddressOfficeNumberID == Id).SingleOrDefault();
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }

                    var data = objEntity;
                    data.AddressOfficeNumberID = Id;
                    data.CreateUserID = existing.CreateUserID;
                    data.CreateDate = existing.CreateDate;
                    data.ModifyDate = DateTime.Now;

                    if (data.ModifyUserID == null || data.ModifyUserID == 0)
                    {
                        return MessageResult(false, "ModifyUserID field is required!");
                    }
                    if(data.Type.ToUpper() != "MAIN" && data.Type.ToUpper() != "BRANCH")
                    {
                        return MessageResult(false, "field 'Type' must be MAIN/BRANCH");
                    }

                    string descriptionActivity = "";
                    if(existing.Type.ToUpper() == "MAIN")
                    {
                        descriptionActivity = "Main address change";
                    }
                    else
                    {
                        descriptionActivity = "Branch address change";
                    }

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = (long)data.CustomerID,
                        CustomerGenID = (long)data.CustomerGenID,
                        UserID = (long)data.ModifyUserID,
                        Description = descriptionActivity
                    };
                    uow.AddressOfficeNumberRepository.Update(data);
                    var accountActivity = uow.AccountActivityHistoryRepository.InsertAccountActivityHistory(dataAccountActivity);

                    result = MessageResult(true, "Update Success", "data :"+accountActivity);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction Delete(long Id)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberById(Id);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }

                    string descriptionActivity = "";
                    if(existing.Type.ToUpper() == "MAIN")
                    {
                        descriptionActivity = "Delete main address & main address change";
                    }
                    else
                    {
                        descriptionActivity = "Delete address";
                    }

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = (long)existing.CustomerID,
                        CustomerGenID = (long)existing.CustomerGenID,
                        UserID = (long)existing.ModifyUserID,
                        Description = descriptionActivity
                    };
                    uow.AddressOfficeNumberRepository.Delete(existing);
                    var accountActivity = uow.AccountActivityHistoryRepository.InsertAccountActivityHistory(dataAccountActivity);

                    result = MessageResult(true, "Delete Success","data :"+accountActivity);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction DeleteAddressOfficeNumberByID(long Id, long customerID, long customerGenID, long modifyUserID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberById(Id);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }

                    string descriptionActivity = "";
                    if (existing.Type.ToUpper() == "MAIN")
                    {
                        descriptionActivity = "Delete main address & main address change";
                    }
                    else
                    {
                        descriptionActivity = "Delete address";
                    }

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = customerID,
                        CustomerGenID = customerGenID,
                        UserID = modifyUserID,
                        Description = descriptionActivity
                    };
                    uow.AddressOfficeNumberRepository.DeleteAddressOfficeNumberByID(Id, customerID, customerGenID);
                    var accountActivity = uow.AccountActivityHistoryRepository.InsertAccountActivityHistory(dataAccountActivity);

                    result = MessageResult(true, "Delete Success", "data :" + accountActivity);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetAddressOfficeNumberByCustomerGenId(long customerGenId)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberByCustomerGenId(customerGenId);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetAddressOfficeNumberByCustomerId(long customerId)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberByCustomerId(customerId);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetAddressOfficeNumberById(long customerId, long customerGenId)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberById(customerId, customerGenId);
                    result = MessageResult(true, "Success", existing);
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