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
    public class CustomerSuccessStoryLogic : ICustomerSuccessStoryLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public CustomerSuccessStoryLogic(string connectionstring, string apiGateway)
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

        public ResultAction Insert(CpCustomerSuccessStory objEntity)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    objEntity.CreateDate = DateTime.Now;
                    uow.CustomerSuccessStoryRepository.Add(objEntity);
                    result = MessageResult(true, "Insert Success");
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
