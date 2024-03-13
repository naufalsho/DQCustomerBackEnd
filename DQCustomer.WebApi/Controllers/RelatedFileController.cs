using System;
using DQCustomer.BusinessLogic;
using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DQCustomer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedFileController : ControllerBase
    {
        private IRelatedFileLogic objRelatedFileLogic;
        public RelatedFileController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objRelatedFileLogic = new RelatedFileLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }
        [HttpGet]
        public IActionResult GetRelatedFile()
        {
            try
            {
                var result = objRelatedFileLogic.GetRelatedFile();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult InsertRelatedFile([FromForm] Req_CustomerSettingInsertRelatedFile_ViewModel objEntity)
        {
            try
            {
                var result = objRelatedFileLogic.InsertRelatedFile(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{RelatedFileID}")]
        public IActionResult UpdateRelatedFile(long RelatedFileID, CpRelatedFile objEntity)
        {
            try
            {
                var result = objRelatedFileLogic.UpdateRelatedFile(RelatedFileID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{RelatedFileID}")]
        public IActionResult DeleteRelatedFile(long RelatedFileID)
        {
            try
            {
                var result = objRelatedFileLogic.DeleteRelatedFile(RelatedFileID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRelatedFileByCustomerID")]
        public IActionResult GetRelatedFileByCustomerID(long customerID)
        {
            try
            {
                var result = objRelatedFileLogic.GetRelatedFileByCustomerID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
