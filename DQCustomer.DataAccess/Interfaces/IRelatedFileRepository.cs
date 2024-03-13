using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IRelatedFileRepository : IRepository<CpRelatedFile>
    {
        CpRelatedFile GetRelatedFileById(long Id);
        List<CpRelatedFile> GetRelatedFileByCustomerID(long customerID);
        CpRelatedFile GetRelatedFileByDocumentPath(string documentPath);
        string PathCustomerProfileRelated();
    }
}
