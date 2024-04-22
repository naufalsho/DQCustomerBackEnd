using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic.Services;
using DQCustomer.BusinessObject;
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
        public ResultAction DeleteCustomerCardFile(long Id)
        {
            ResultAction result = new ResultAction();
            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);
                    var existing = uow.RelatedFileRepository.GetRelatedFileById(Id);
                    if (existing == null)
                    {
                        return result = MessageResult(false, "Data not found");
                    }
                    uow.RelatedFileRepository.Delete(existing);
                    result = MessageResult(true, "Delete Success");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
            return result;
        }

        
        public ResultAction InsertCustomerCardFile(Req_CustomerSettingInsertRelatedFile_ViewModel objEntity)
        {
            ResultAction result = new ResultAction();

            try
            {
                using (_context)
                {
                    IUnitOfWork uow = new UnitOfWork(_context);

                    //var driveLetter = "Z:";
                    //var pathFolder = Path.Combine(driveLetter, "BHP\\DataQuality\\CustomerProfileRelated");
                    //var pathFolder = "Z:";

                    var pathFolder = uow.RelatedFileRepository.PathCustomerProfileRelated();
                    if (pathFolder == null)
                    {
                        return MessageResult(false, "Path not found!");
                    }
                    var setName = objEntity.DocumentName;
                    var fileName = objEntity.File.FileName;
                    var documentType = Path.GetExtension(fileName);

                    var filePath = Path.Combine(pathFolder, setName + documentType);

                    var existing = uow.RelatedFileRepository.GetRelatedFileByDocumentPath(filePath);
                    string newFilePath = null;

                    if (existing != null)
                    {
                        int number = 1;

                        while (true)
                        {
                            var newFileName = $"{setName}({number})";
                            newFilePath = Path.Combine(pathFolder, newFileName + documentType);

                            var newExisting = uow.RelatedFileRepository.GetRelatedFileByDocumentPath(newFilePath);

                            if (newExisting == null)
                            {
                                fileName = newFileName;
                                filePath = newFilePath; // Update filePath with the new file path
                                break;
                            }
                            number++;
                        }
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        objEntity.File.CopyTo(fileStream);
                    }

                    var insertModel = new CpRelatedFile
                    {
                        RFileID = 0,
                        CustomerID = objEntity.CustomerID,
                        DocumentName = setName + documentType,
                        DocumentType = objEntity.DocumentType,
                        DocumentPath = filePath,
                        CreateDate = DateTime.Now,
                        CreateUserID = objEntity.CreateUserID,
                        ModifyDate = DateTime.Now,
                        ModifyUserID = objEntity.ModifyUserID
                    };

                    uow.RelatedFileRepository.Add(insertModel);

                    result = MessageResult(true, "Insert Data Success!");
                }
            }
            catch (Exception ex)
            {
                result = MessageResult(false, ex.Message);
            }
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
                    var existing = uow.CustomerCardFileRepository.GetCustomerCardFileByCustomerGenID(customerGenID);
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
