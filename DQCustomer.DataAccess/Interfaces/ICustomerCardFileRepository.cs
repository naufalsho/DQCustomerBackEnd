using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface ICustomerCardFileRepository : IRepository<FileCustomerCard>
    {
        List<Req_CustomerCardFileGetByCustomerGenID_ViewModel> GetCustomerCardFileByCustomerGenID(long customerGenID);
        public bool InsertCustomerCardFile(Req_CustomerCardFileInsert_ViewModel objEntity, string extension, byte[] imageFile);
        public FileCustomerCard GetByCustomerCardID(Guid customerCardID);
    }
}
