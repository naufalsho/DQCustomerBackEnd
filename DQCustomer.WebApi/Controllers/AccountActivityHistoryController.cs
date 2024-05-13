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
    public class AccountActivityHistoryController : ControllerBase
    {
        private IAccountActivityHistoryLogic objAccountActivityHistoryLogic;

        public AccountActivityHistoryController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objAccountActivityHistoryLogic = new AccountActivityHistoryLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = objAccountActivityHistoryLogic.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{accountActivityHistoryID}")]
        public IActionResult GetByID(long accountActivityHistoryID)
        {
            try
            {
                var result = objAccountActivityHistoryLogic.GetByID(accountActivityHistoryID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert(CpAccountActivityHistory objEntity)
        {
            try
            {
                var result = objAccountActivityHistoryLogic.Insert(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{accountActivityHistoryID}")]
        public IActionResult Update(CpAccountActivityHistory objEntity)
        {
            try
            {
                var result = objAccountActivityHistoryLogic.Update(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{accountActivityHistoryID}")]
        public IActionResult DeleteByID(long accountActivityHistoryID)
        {
            try
            {
                var result = objAccountActivityHistoryLogic.DeleteByID(accountActivityHistoryID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAccountActivityHistoryByID")]
        public IActionResult GetAccountActivityHistoryByID(long customerID, long customerGenID)
        {
            try
            {
                var result = objAccountActivityHistoryLogic.GetAccountActivityHistoryByID(customerID, customerGenID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}