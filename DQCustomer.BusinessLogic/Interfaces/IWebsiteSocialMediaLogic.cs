using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;


namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IWebsiteSocialMediaLogic
    {
        ResultAction GetWebsiteSocialMedia();
        ResultAction GetWebsiteSocialMediaByGenID(long customerGenID);
        ResultAction Insert(CpWebsiteSocialMedia objEntity);
        ResultAction UpdateWebsiteSocialMedia(CpWebsiteSocialMedia objEntity);
        ResultAction Delete(int WebsiteSocialMediaID);
        ResultAction GetWebsiteSocialMediaByID(long WebsiteSocialMediaID);

    }
}