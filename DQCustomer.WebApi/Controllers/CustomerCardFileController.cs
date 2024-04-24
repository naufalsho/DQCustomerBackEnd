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
    public class CustomerCardFileController : ControllerBase
    {
        private ICustomerCardFileLogic objCustomerCardFileLogic;
        public CustomerCardFileController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objCustomerCardFileLogic = new CustomerCardFileLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }
        //[HttpGet]
        //public IActionResult GetRelatedFile()
        //{
        //    try
        //    {
        //        var result = objRelatedFileLogic.GetRelatedFile();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPost]
        //public IActionResult InsertRelatedFile([FromForm] Req_CustomerSettingInsertRelatedFile_ViewModel objEntity)
        //{
        //    try
        //    {
        //        var result = objRelatedFileLogic.InsertRelatedFile(objEntity);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPut("{RelatedFileID}")]
        //public IActionResult UpdateRelatedFile(long RelatedFileID, CpRelatedFile objEntity)
        //{
        //    try
        //    {
        //        var result = objRelatedFileLogic.UpdateRelatedFile(RelatedFileID, objEntity);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpDelete("{RelatedFileID}")]
        //public IActionResult DeleteRelatedFile(long RelatedFileID)
        //{
        //    try
        //    {
        //        var result = objRelatedFileLogic.DeleteRelatedFile(RelatedFileID);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("GetCustomerCardFileByCustomerGenID")]
        public IActionResult GetCustomerCardFileByCustomerGenID(long customerGenID)
        {
            try
            {
                var result = objCustomerCardFileLogic.GetCustomerCardFileByCustomerGenID(customerGenID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertCustomerCardFile([FromForm] Req_CustomerCardFileInsert_ViewModel objEntity)
        {
            try
            {
                var result = objCustomerCardFileLogic.InsertCustomerCardFile(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{customerCardID}")]
        public IActionResult DeleteCustomerCardFile(long customerCardID)
        {
            try
            {
                var result = objCustomerCardFileLogic.DeleteCustomerCardFile(customerCardID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
