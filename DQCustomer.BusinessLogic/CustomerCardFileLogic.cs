using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic.Services;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess;
using DQCustomer.DataAccess.Interfaces;

namespace DQCustomer.BusinessLogic
{
    public class CustomerCardFileLogic : ICustomerCardFileLogic
    {
        private DapperContext _context;
        private IGenericAPI genericAPI;
        public CustomerCardFileLogic(string connectionstring, string apiGateway)
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
        public ResultAction GetCustomerCardFileByCustomerGenID(long customerGenID)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.CustomerSettingRepository.GetCustomerDetailsByGenID(customerGenID);

                    if (existing.Count() == 0)
                    {
                        return MessageResult(false, "ID not found!");
                    }

                    var responseData = uow.CustomerCardFileRepository.GetCustomerCardFileByCustomerGenID(customerGenID);

                    if (responseData.Count() == 0)
                    {
                        return MessageResult(false, "Data not found!");
                    }


                    result = MessageResult(true, "Success", responseData);
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction InsertCustomerCardFile(Req_CustomerCardFileInsert_ViewModel objEntity)
        {
            ResultAction result = new ResultAction();

            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    // Mendapatkan ekstensi dari file
                    byte[] imageFile;
                    string extension = objEntity.File.ContentType;

                    var existing = uow.CustomerSettingRepository.GetCustomerDetailsByGenID(objEntity.CustomerGenID);
                    if (existing != null)
                    {

                        // Mengonversi file stream menjadi nilai hexadesimal
                        using (var memoryStream = new MemoryStream())
                        {
                            objEntity.File.CopyTo(memoryStream);
                            imageFile = memoryStream.ToArray();
                        }

                        var insertModel = new Req_CustomerCardFileInsert_ViewModel()
                        {
                            CustomerGenID = objEntity.CustomerGenID,
                            LastModifyUserID = objEntity.LastModifyUserID
                        };

                        uow.CustomerCardFileRepository.InsertCustomerCardFile(insertModel, extension, imageFile);
                        result = MessageResult(true, "Insert Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        public ResultAction DeleteCustomerCardFile(long customerCardID)
        {
            throw new NotImplementedException();
        }
    }
}
