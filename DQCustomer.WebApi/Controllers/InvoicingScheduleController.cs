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
    public class InvoicingScheduleController : ControllerBase
    {
        private IInvoicingScheduleLogic objInvoicingScheduleLogic;
        public InvoicingScheduleController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objInvoicingScheduleLogic = new InvoicingScheduleLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }
        [HttpGet]
        public IActionResult GetInvoicingSchedule()
        {
            try
            {
                var result = objInvoicingScheduleLogic.GetInvoicingSchedule();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult InsertInvoicingSchedule(CpInvoicingSchedule objEntity)
        {
            try
            {
                var result = objInvoicingScheduleLogic.InsertInvoicingSchedule(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{InvoicingScheduleID}")]
        public IActionResult UpdateInvoicingSchedule(long InvoicingScheduleID, CpInvoicingSchedule objEntity)
        {
            try
            {
                var result = objInvoicingScheduleLogic.UpdateInvoicingSchedule(InvoicingScheduleID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{InvoicingScheduleID}")]
        public IActionResult DeleteInvoicingSchedule(long InvoicingScheduleID)
        {
            try
            {
                var result = objInvoicingScheduleLogic.DeleteInvoicingSchedule(InvoicingScheduleID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetInvoicingScheduleByCustomerID")]
        public IActionResult GetInvoicingScheduleByCustomerID(long customerID)
        {
            try
            {
                var result = objInvoicingScheduleLogic.GetInvoicingScheduleByCustomerID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
