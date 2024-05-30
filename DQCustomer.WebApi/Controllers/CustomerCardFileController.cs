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


        [HttpGet("{customerCardID}")]
        public IActionResult GetByCustomerCardID(Guid customerCardID)
        {
            try
            {
                var result = objCustomerCardFileLogic.GetByCustomerCardID(customerCardID);
        
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{customerCardID}")]
        public IActionResult DeleteCustomerCard(Guid customerCardID)
        {
            try
            {
                var result = objCustomerCardFileLogic.Delete(customerCardID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
