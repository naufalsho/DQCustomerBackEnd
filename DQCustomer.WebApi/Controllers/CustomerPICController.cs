using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessLogic;
using DQCustomer.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Drawing.Printing;

namespace DQCustomer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPICController : ControllerBase
    {
        private ICustomerPICLogic objCustomerPICLogic;

        public CustomerPICController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objCustomerPICLogic = new CustomerPICLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }

        [HttpGet]
        public IActionResult GetCustomerPIC()
        {
            try
            {
                var result = objCustomerPICLogic.GetCustomerPIC();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert(CustomerPIC objEntity)
        {
            try
            {
                var result = objCustomerPICLogic.Insert(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{CustomerPICID}")]
        public IActionResult UpdateCustomerPIC(long CustomerPICID, CustomerPIC objEntity)
        {
            try
            {
                var result = objCustomerPICLogic.Update(CustomerPICID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{CustomerPICID}")]
        public IActionResult DeleteCustomerPIC(long CustomerPICID)
        {
            try
            {
                var result = objCustomerPICLogic.Delete(CustomerPICID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerPICByCustomerGenId")]
        public IActionResult GetCustomerByCustomerGenId(long customerGenId)
        {
            try
            {
                var result = objCustomerPICLogic.GetCustomerPICByCustomerGenId(customerGenId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}