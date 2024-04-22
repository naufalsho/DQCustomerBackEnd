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
        CpRelatedFile GetRelatedFileById(long Id);
        List<Req_CustomerCardFileGetByCustomerGenID_ViewModel> GetCustomerCardFileByCustomerGenID(long customerGenID);
        CpRelatedFile GetRelatedFileByDocumentPath(string documentPath);
        string PathCustomerProfileRelated();
    }
}
