using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IRelatedFileLogic
    {
        ResultAction GetRelatedFile();
        ResultAction InsertRelatedFile(Req_CustomerSettingInsertRelatedFile_ViewModel objEntity);
        ResultAction UpdateRelatedFile(long Id, CpRelatedFile objEntity);
        ResultAction DeleteRelatedFile(long Id);
        ResultAction GetRelatedFileByCustomerID(long customerID);
    }
}
