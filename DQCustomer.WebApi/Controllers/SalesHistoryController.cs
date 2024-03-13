using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic;
using DQCustomer.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using DQCustomer.BusinessObject;

namespace DQCustomer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesHistoryController : ControllerBase
    {
        private ISalesHistoryLogic objSalesHistoryLogic;

        public SalesHistoryController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objSalesHistoryLogic = new SalesHistoryLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = objSalesHistoryLogic.GetAllSalesHistory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{salesHistoryID}")]
        public IActionResult Get(long salesHistoryID)
        {
            try
            {
                var result = objSalesHistoryLogic.GetSalesHistoryByCustomerID(salesHistoryID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(CpSalesHistory objEntity)
        {
            try
            {
                var result = objSalesHistoryLogic.InsertSalesHistory(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{salesHistoryID}")]
        public IActionResult Update(long salesHistoryID, CpSalesHistory objEntity)
        {
            try
            {
                var result = objSalesHistoryLogic.UpdateSalesHistory(salesHistoryID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{salesHistoryID}")]
        public IActionResult Delete(long salesHistoryID)
        {
            try
            {
                var result = objSalesHistoryLogic.DeleteSalesHistory(salesHistoryID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAccountOwner")]
        public IActionResult GetAccountOwner(long customerID)
        {
            try
            {
                var result = objSalesHistoryLogic.GetAccountOwner(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSalesAssignHistory")]
        public IActionResult GetSalesAssignHistory(long customerID)
        {
            try
            {
                var result = objSalesHistoryLogic.GetSalesAssignHistory(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
