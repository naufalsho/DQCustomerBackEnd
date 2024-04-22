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

                    var existingCustGenID = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberByCustomerGenId((long)objEntity.CustomerGenID);
                    var existingCustID = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberByCustomerId((long)objEntity.CustomerID);

                    objEntity.Type = objEntity.Type.ToUpper();
                    objEntity.CreateDate = DateTime.Now;
                    objEntity.ModifyDate = DateTime.Now;
                    if(existingCustGenID != null & existingCustID != null) 
                    {
                        uow.AddressOfficeNumberRepository.Add(objEntity);
                    }

                    result = MessageResult(true, "Insert Success!");

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
                    var existing = uow.AddressOfficeNumberRepository.GetAddressOfficeNumberById(Id);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }

                    var data = objEntity;
                    data.AddressOfficeNumberID = Id;
                    data.CreateUserID = existing.CreateUserID;
                    data.CreateDate = existing.CreateDate;
                    data.ModifyDate = DateTime.Now;

                    uow.AddressOfficeNumberRepository.Update(data);
                    result = MessageResult(true, "Update Success");
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
                    uow.AddressOfficeNumberRepository.Delete(existing);
                    result = MessageResult(true, "Delete Success");
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