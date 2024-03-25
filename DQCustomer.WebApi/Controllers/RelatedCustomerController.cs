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
    public class RelatedCustomerController : ControllerBase
    {
        private IRelatedCustomerLogic objRelatedCustomerLogic;
        public RelatedCustomerController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objRelatedCustomerLogic = new RelatedCustomerLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }

        [HttpGet]
        public IActionResult GetRelatedCustomer()
        {
            try
            {
                var result = objRelatedCustomerLogic.GetRelatedCustomer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertRelatedCustomer(CpRelatedCustomer objEntity)
        {
            try
            {
                var result = objRelatedCustomerLogic.InsertRelatedCustomer(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{RCustomerID}")]
        public IActionResult UpdateRelatedCustomer(long RCustomerID, CpRelatedCustomer objEntity)
        {
            try
            {
                var result = objRelatedCustomerLogic.UpdateRelatedCustomer(RCustomerID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{RCustomerID}")]
        public IActionResult DeleteRelatedCustomer(long RCustomerID)
        {
            try
            {
                var result = objRelatedCustomerLogic.DeleteRelatedCustomer(RCustomerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRelatedCustomerByCustomerID")]
        public IActionResult GetRelatedCustomerByCustomerID(long customerID)
        {
            try
            {
                var result = objRelatedCustomerLogic.GetRelatedCustomerByCustomerID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRelatedCustomerByCustomerGenId")]
        public IActionResult GetRelatedCustomerByCustomerGenID(long customerGenID)
        {
            try
            {
                var result = objRelatedCustomerLogic.GetRelatedCustomerByCustomerGenID(customerGenID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
