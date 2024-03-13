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
                    result = MessageResult(true, "Delete Success");
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
                    result = MessageResult(true, "Success");
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
                    existing = objEntity;
                    existing.RCustomerID = Id;
                    existing.ModifyDate = DateTime.Now;
                    uow.RelatedCustomerRepository.Update(existing);
                    result = MessageResult(true, "Update Success");
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

    }
}
