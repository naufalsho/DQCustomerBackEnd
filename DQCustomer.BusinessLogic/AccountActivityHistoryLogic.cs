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
    public class AccountActivityHistoryLogic : IAccountActivityHistoryLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public AccountActivityHistoryLogic(string connectionstring, string apiGateway)
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

        public ResultAction GetAll()
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AccountHistoryActivityRepository.GetAll();
                    if(existing.Count() <= 0)
                    {
                        return result = MessageResult(false, "Data Count : " + existing.Count() );
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

        public ResultAction GetByID(long accountActivityHistoryID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.AccountHistoryActivityRepository.GetByID(accountActivityHistoryID);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
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

        public ResultAction Insert(CpAccountActivityHistory objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    objEntity.CreateDate = DateTime.Now;
                    objEntity.ModifyDate = DateTime.Now;
                    var data = objEntity;
                    uow.AccountHistoryActivityRepository.Add(data);
                    result = MessageResult(true, "Insert Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction Update(CpAccountActivityHistory objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existingID = uow.AccountHistoryActivityRepository.GetByID(objEntity.AccountActivityHistoryID);
                    if (existingID == null)
                    {
                        return result = MessageResult(false, "ID not found");
                    }

                    objEntity.ModifyDate = DateTime.Now;
                    var data = objEntity;
                    uow.AccountHistoryActivityRepository.Update(data);
                    result = MessageResult(true, "Update Success");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction DeleteByID(long accountActivityHistoryID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    var existingID = uow.AccountHistoryActivityRepository.GetByID(accountActivityHistoryID);
                    if (existingID == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }

                    uow.AccountHistoryActivityRepository.DeleteByID(accountActivityHistoryID);
                    result = MessageResult(true, "Delete Success");
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