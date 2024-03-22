using System;
using System.Collections.Generic;
using System.Text;
using DQCustomer.BusinessObject;

namespace DQCustomer.BusinessLogic.Interfaces
{
    public interface IWebsiteSocialMediaLogic
    {
        ResultAction GetWebsiteSocialMedia();
        ResultAction GetWebsiteSocialMediaByGenID(long customerGenID);
        // ResultAction Insert(CpWebsiteSocialMedia model);
        ResultAction UpdateWebsiteSocialMedia(CpWebsiteSocialMedia objEntity);
        // ResultAction DeleteWebsiteSocialMedia(long WebsiteSocialMediaID);
        ResultAction GetWebsiteSocialMediaByID(long WebsiteSocialMediaID);

    }
}