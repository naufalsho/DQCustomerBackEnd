using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IWebsiteSocialMediaRepository : IRepository<CpWebsiteSocialMedia>
    {
        bool InsertWebisteSocialMedia(CpWebsiteSocialMedia objEntity);
        CpWebsiteSocialMedia GetWebsiteSocialMediaByID(long Id);
        List<Req_CustomerMasterGetWebsiteSocialMediaByGenID_ViewModel> GetWebsiteSocialMediaByGenID(long CustomerGenID);
        List<CpWebsiteSocialMedia> GetWebsiteSocialMediaByCustomerID(long CustomerID);  
    }
}