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
    public class AddressOfficeNumberController : ControllerBase
    {
        private IAddressOfficeNumberLogic objAddressOfficeNumberLogic;

        public AddressOfficeNumberController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objAddressOfficeNumberLogic = new AddressOfficeNumberLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }

        [HttpGet]
        public IActionResult GetAddressOfficeNumber()
        {
            try
            {
                var result = objAddressOfficeNumberLogic.GetAddressOfficeNumber();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert(CpAddressOfficeNumber objEntity)
        {
            try
            {
                var result = objAddressOfficeNumberLogic.Insert(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{AddressOfficeNumberID}")]
        public IActionResult UpdateAddressOfficeNumber(int AddressOfficeNumberID, CpAddressOfficeNumber objEntity)
        {
            try
            {
                var result = objAddressOfficeNumberLogic.Update(AddressOfficeNumberID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{AddressOfficeNumberID}")]
        public IActionResult DeleteAddressOfficeNumber(int AddressOfficeNumberID)
        {
            try
            {
                var result = objAddressOfficeNumberLogic.Delete(AddressOfficeNumberID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAddressOfficeNumberByCustomerGenId")]
        public IActionResult GetAddressOfficeNumberByCustomerGenId(int customerGenId)
        {
            try
            {
                var result = objAddressOfficeNumberLogic.GetAddressOfficeNumberByCustomerGenId(customerGenId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}