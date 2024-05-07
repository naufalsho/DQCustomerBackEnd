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
using System.Security.Principal;

namespace DQCustomer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSettingController : ControllerBase
    {
        private ICustomerSettingLogic objCustomerSettingLogic;

        public CustomerSettingController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objCustomerSettingLogic = new CustomerSettingLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }

        [HttpPost]
        public IActionResult Insert(CpCustomerSetting objEntity)
        {
            try
            {
                var result = objCustomerSettingLogic.Insert(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(long customerID, CpCustomerSetting objEntity)
        {
            try
            {
                var result = objCustomerSettingLogic.Update(customerID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = objCustomerSettingLogic.GetAllCustomerSetting();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{customerID}")]
        public IActionResult GetCustomerSettingBySalesID(long customerID, long SalesID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerSettingBySalesID(customerID, SalesID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerSettingNoNamedAccount")]
        public IActionResult GetCustomerSettingNoNamedAccount(int page, int pageSize, string column, string sorting, string search, bool? blacklist = null, bool? holdshipment = null, long? myAccount = null, bool? showNoName = null, bool? showNamed = null, bool? showShareable = null)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerSettingNoNamedAccount(page, pageSize, column, sorting, search, blacklist, holdshipment, myAccount, showNoName, showNamed, showShareable);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerSettingNamedAccount")]
        public IActionResult GetCustomerSettingNamedAccount(int page, int pageSize, string column, string sorting, string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null, long? myAccount = null, bool? showNoName = null, bool? showNamed = null, bool? showShareable = null)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerSettingNamedAccount(page, pageSize, column, sorting, search, salesID, pmoCustomer, blacklist, holdshipment, myAccount, showNoName, showNamed, showShareable);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerSettingShareableAccount")]
        public IActionResult GetCustomerSettingShareableAccount(int page, int pageSize, string column, string sorting, string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null, long? myAccount = null, bool? showNoName = null, bool? showNamed = null, bool? showShareable = null)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerSettingShareableAccount(page, pageSize, column, sorting, search, salesID, pmoCustomer, blacklist, holdshipment, myAccount, showNoName, showNamed, showShareable);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCustomerSettingAllAccount")]
        public IActionResult GetCustomerSettingAllAccount(int page, int pageSize, string column, string sorting, string search, string salesID, bool? pmoCustomer = null, bool? blacklist = null, bool? holdshipment = null, long? myAccount = null, bool? showNoName = null, bool? showNamed = null, bool? showShareable = null, bool? isNew = null)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerSettingAllAccount(page, pageSize, column, sorting, search, salesID, pmoCustomer, blacklist, holdshipment, myAccount, showNoName, showNamed, showShareable, isNew);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("ApproveCustomerSetting")]
        public IActionResult Update(long customerID, long salesID, bool isApprove, long? directorateApprovedBy, long? adminApprovedBy,  string? description, int? modifyUserID)
        {
            try
            {
                var result = objCustomerSettingLogic.ApproveCustomerSetting(customerID, salesID, isApprove, directorateApprovedBy, adminApprovedBy, description, modifyUserID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("ReleaseAccount")]
        public IActionResult ReleaseAccount(long customerID, long salesID, int? modifyUserID)
        {
            try
            {
                var result = objCustomerSettingLogic.ReleaseAccount(customerID, salesID, modifyUserID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSalesData")]
        public IActionResult GetSalesData()
        {
            try
            {
                var result = objCustomerSettingLogic.GetSalesData();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCustomerPICByCustomerID")]
        public IActionResult GetCustomerPICByCustomerID(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerPICByCustomerID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBrandSummary")]
        public IActionResult GetBrandSummary(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetBrandSummary(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetServiceSummary")]
        public IActionResult GetServiceSummary(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetServiceSummary(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCustomerData")]
        public IActionResult GetCustomerData(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerDataByID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProjectHistory")]
        public IActionResult GetProjectHistory(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetProjectHistory(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetConfigItem")]
        public IActionResult GetConfigItem(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetConfigItem(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCollectionHistory")]
        public IActionResult GetCollectionHistory(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCollectionHistory(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSalesDataByName")]
        public IActionResult GetSalesDataByName(string salesName)
        {
            try
            {
                var result = objCustomerSettingLogic.GetSalesByName(salesName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCustomerByName")]
        public IActionResult GetCustomerByName(string customerName)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerName(customerName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCustomerCategory")]
        public IActionResult GetCustomerCategory()
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerCategory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Customer Master
        [HttpGet("GetCustomerSearchRequest")]
        public IActionResult GetSearchRequest(int page, int pageSize, string column, string sorting, string customerName, string picName)
        {
            try
            {
                var result = objCustomerSettingLogic.GetSearchRequest(page, pageSize, column, sorting, customerName, picName);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertRequestNewCustomer")]
        public IActionResult InsertRequestNewCustomer([FromForm] Req_CustomerSettingInsertRequestCustomer_ViewModel objEntity)
        {
            try
            {
                var result = objCustomerSettingLogic.InsertRequestNewCustomer(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRequestNewCustomerByGenID")]
        public IActionResult GetRequestNewCustomerByGenID(long customerGenID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetRequestNewCustomerByGenID(customerGenID);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateApprovalStatusNewCustomer")]
        public IActionResult UpdateApprovalStatusNewCustomer(Req_CustomerSettingUpdateAprrovalStatusNewCustomer_ViewModel objEntity)
        {
            try
            {
                var result = objCustomerSettingLogic.UpdateApprovalStatusNewCustomer(objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerDetailsByCustID")]
        public IActionResult GetCustomerDetailsByCustID(long customerID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerDetailsByCustID(customerID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        
        [HttpGet("GetCustomerDetailsByGenID")]
        public IActionResult GetCustomerDetailsByGenID(long customerGenID)
        {
            try
            {
                var result = objCustomerSettingLogic.GetCustomerDetailsByGenID(customerGenID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateIndustryClassByID")]
        public IActionResult UpdateIndustryClassByID(long customerID, long customerGenID, Req_CustomerSettingUpdateIndustryClass_ViewModel objEntity)
        {
            try
            {
                var result = objCustomerSettingLogic.UpdateIndustryClassByID(customerID, customerGenID, objEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetIndustryClass")]
        public IActionResult GetIndustryClass()
        {
            try
            {
                var result = objCustomerSettingLogic.GetIndustryClass();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
