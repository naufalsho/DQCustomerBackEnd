using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IAddressOfficeNumberRepository : IRepository<CpAddressOfficeNumber>
    {
        bool InsertAddressOfficeNumber(CpAddressOfficeNumber objEntity);
        CpAddressOfficeNumber GetAddressOfficeNumberById(long Id);
        List<CpAddressOfficeNumber> GetAddressOfficeNumberByCustomerGenId(long customerGenId);
        List<CpAddressOfficeNumber> GetAddressOfficeNumberByCustomerId(long customerId);
        List<CpAddressOfficeNumber> GetAddressOfficeNumberById(long customerId, long customerGenId);
    }
}