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
    public class InvoicingConditionController : ControllerBase
    {
        private IInvoicingConditionLogic objInvoicingConditionLogic;
        public InvoicingConditionController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objInvoicingConditionLogic = new InvoicingConditionLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }
        [HttpGet]
        public IActionResult GetInvoicingCondition()
        {
            try
            {
                var result = objInvoicingConditionLogic.GetInvoicingCondition();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult InsertInvoicingCondition(CpInvoicingCondition objEntity)
        {
            try
            {
                var result = objInvoicingConditionLogic.InsertInvoicingCondition(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{InvoicingConditionID}")]
        public IActionResult UpdateInvoicingCondition(long InvoicingConditionID, CpInvoicingCondition objEntity)
        {
            try
            {
                var result = objInvoicingConditionLogic.UpdateInvoicingCondition(InvoicingConditionID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{InvoicingConditionID}")]
        public IActionResult DeleteInvoicingCondition(long InvoicingConditionID)
        {
            try
            {
                var result = objInvoicingConditionLogic.DeleteInvoicingCondition(InvoicingConditionID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetInvoicingConditionByCustomerID")]
        public IActionResult GetInvoicingConditionByCustomerID(long customerID)
        {
            try
            {
                var result = objInvoicingConditionLogic.GetInvoicingConditionByCustomerID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
