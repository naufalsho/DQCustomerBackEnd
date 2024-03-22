using System;
using DQCustomer.BusinessLogic;
using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessObject;
using DQCustomer.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DQCustomer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteSocialMediaController : ControllerBase
    {
        private IWebsiteSocialMediaLogic objWebsiteSocialMediaLogic;
        public WebsiteSocialMediaController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objWebsiteSocialMediaLogic = new WebsiteSocialMediaLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }
    
        // [HttpPost]
        // public IActionResult InsertWebsiteSocialMedia([FromForm] Req_CustomerMasterGetWebsiteSocialMediaByGenID_ViewModel objEntity)
        // {
        //     try
        //     {
        //         var result = objWebsiteSocialMediaLogic.InsertWebsiteSocialMedia(objEntity);
        //         return Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpPut]
        public IActionResult UpdateWebsiteSocialMedia(CpWebsiteSocialMedia objEntity)
        {
            try
            {
                var result = objWebsiteSocialMediaLogic.UpdateWebsiteSocialMedia(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpDelete("{WebsiteSocialMediaID}")]
        // public IActionResult DeleteWebsiteSocialMedia(long WebsiteSocialMediaID)
        // {
        //     try
        //     {
        //         var result = objWebsiteSocialMediaLogic.DeleteWebsiteSocialMedia(WebsiteSocialMediaID);
        //         return Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpGet]
        public IActionResult GetWebsiteSocialMedia()
        {
            try
            {
                var result = objWebsiteSocialMediaLogic.GetWebsiteSocialMedia();
                return Ok(result);
            }
             catch (Exception ex)
            {
            return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWebsiteSocialMediaByGenID")]
        public IActionResult GetWebsiteSocialMediaByGenID(long customerGenID)
        {
            try
            {
                var result = objWebsiteSocialMediaLogic.GetWebsiteSocialMediaByGenID(customerGenID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}