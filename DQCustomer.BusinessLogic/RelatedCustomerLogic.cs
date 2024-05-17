using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic.Services;
using DQCustomer.BusinessObject;
using DQCustomer.DataAccess;
using DQCustomer.DataAccess.Interfaces;

namespace DQCustomer.BusinessLogic
{
    public class RelatedCustomerLogic : IRelatedCustomerLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public RelatedCustomerLogic(string connectionstring, string apiGateway)
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
        public ResultAction DeleteRelatedCustomer(long Id)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.RelatedCustomerRepository.GetRelatedCustomerById(Id);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }
                    uow.RelatedCustomerRepository.DeleteRelatedCustomer(Id);

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = (long)existing.CustomerID,
                        CustomerGenID = (long)existing.CustomerGenID,
                        UserID = (long)existing.ModifyUserID,
                        Description = "Related customer delete"
                    };

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

        public ResultAction GetRelatedCustomer()
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.RelatedCustomerRepository.GetAll();
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction InsertRelatedCustomer(CpRelatedCustomer objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    objEntity.CreateDate = DateTime.Now;
                    objEntity.ModifyDate = null;
                    objEntity.ModifyUserID = null;
                    uow.RelatedCustomerRepository.Add(objEntity);

                    CpRelatedCustomer newData = new CpRelatedCustomer
                    {
                        CustomerID = objEntity.RelatedCustomerID,
                        RelatedCustomerID = objEntity.CustomerID,
                        CreateUserID = objEntity.CreateUserID,
                        CreateDate = DateTime.Now
                    };
                    uow.RelatedCustomerRepository.Add(newData);

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = (long)newData.CustomerID,
                        CustomerGenID = (long)objEntity.CustomerGenID,
                        UserID = newData.CreateUserID,
                        Description = "Add new related customer"
                    };

                    var accountActivity = uow.AccountActivityHistoryRepository.InsertAccountActivityHistory(dataAccountActivity);

                    result = MessageResult(true, "Insert Success!", "data : " + accountActivity);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction UpdateRelatedCustomer(long Id, CpRelatedCustomer objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.RelatedCustomerRepository.GetRelatedCustomerById(Id);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }

                    if (objEntity.ModifyUserID == null || objEntity.ModifyUserID == 0)
                    {
                        return MessageResult(false, "ModifyUserID field is required!");
                    }

                    existing = objEntity;
                    existing.RCustomerID = Id;
                    existing.ModifyDate = DateTime.Now;

                    uow.RelatedCustomerRepository.Update(existing);

                    Req_AccountActivityHistoryInsert_ViewModel dataAccountActivity = new Req_AccountActivityHistoryInsert_ViewModel
                    {
                        CustomerID = (long)objEntity.CustomerID,
                        CustomerGenID = (long)objEntity.CustomerGenID,
                        UserID = (long)objEntity.ModifyUserID,
                        Description = "Related customer change"
                    };

                    var accountActivity = uow.AccountActivityHistoryRepository.InsertAccountActivityHistory(dataAccountActivity);

                    result = MessageResult(true, "Update Success", "data :" + accountActivity);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        public ResultAction GetRelatedCustomerByCustomerID(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.RelatedCustomerRepository.GetRelatedCustomerByCustomerID(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction GetRelatedCustomerMoreDetailsByID(long customerID, long customerGenID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.RelatedCustomerRepository.GetRelatedCustomerMoreDetailsByID(customerID, customerGenID);
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
