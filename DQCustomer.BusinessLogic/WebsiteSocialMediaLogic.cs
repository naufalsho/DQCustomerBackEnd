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
    public class WebsiteSocialMediaLogic : IWebsiteSocialMediaLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public WebsiteSocialMediaLogic(string connectionstring, string apiGateway)
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

        public ResultAction GetWebsiteSocialMedia()
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.WebsiteSocialMediaRepository.GetAll();
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

  
        public ResultAction GetWebsiteSocialMediaByGenID(long customerGenID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.WebsiteSocialMediaRepository.GetWebsiteSocialMediaByGenID(customerGenID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        
        public ResultAction GetWebsiteSocialMediaByCustomerID(long customerID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.WebsiteSocialMediaRepository.GetWebsiteSocialMediaByCustomerID(customerID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }


         public ResultAction Insert(CpWebsiteSocialMedia objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    objEntity.CreateDate = DateTime.Now;
                    objEntity.ModifyDate = DateTime.Now;
                    uow.WebsiteSocialMediaRepository.Add(objEntity);

                    result = MessageResult(true, "Insert Success!");

                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction UpdateWebsiteSocialMedia(CpWebsiteSocialMedia objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.WebsiteSocialMediaRepository.GetWebsiteSocialMediaByID(objEntity.WebsiteSocialMediaID);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }
                    var data = objEntity;
                    data.WebsiteSocialMediaID = existing.WebsiteSocialMediaID;
                    data.CreateUserID = existing.CreateUserID;
                    data.CreateDate = existing.CreateDate;
                    data.ModifyDate = DateTime.Now;

                    uow.WebsiteSocialMediaRepository.Update(data);
                    result = MessageResult(true, "Update Success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = MessageResult(false, ex.Message);
            }
            return result;
        }
        
        public ResultAction GetWebsiteSocialMediaByID(long WebsiteSocialMediaID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.WebsiteSocialMediaRepository.GetWebsiteSocialMediaByID(WebsiteSocialMediaID);
                    result = MessageResult(true, "Success", existing);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

          public ResultAction Delete(int WebsiteSocialMediaID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.WebsiteSocialMediaRepository.GetWebsiteSocialMediaByID(WebsiteSocialMediaID);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }
                    uow.WebsiteSocialMediaRepository.Delete(existing);
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