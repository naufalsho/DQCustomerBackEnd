using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface ICustomerCardFileLogic
    {
        ResultAction InsertCustomerCardFile(Req_CustomerSettingInsertRelatedFile_ViewModel objEntity);
        ResultAction DeleteCustomerCardFile(long Id);
        ResultAction GetCustomerCardFileByCustomerGenID(long customerGenID);
    }
}
