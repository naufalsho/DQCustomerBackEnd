using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DQCustomer.BusinessLogic;
using DQCustomer.BusinessLogic.Interfaces;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DQCustomer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunnelOpportunityController : ControllerBase
    {
        private IFunnelOpportunityLogic objLogic;

        public FunnelOpportunityController(IOptions<DatabaseConfiguration> appSettings, IOptions<ApiGatewayConfig> apiGateway)
        {
            string apiGatewayURL = string.Format("{0}:{1}", apiGateway.Value.IP, apiGateway.Value.Port);
            objLogic = new FunnelOpportunityLogic(appSettings.Value.OMSProd, apiGatewayURL);
        }

        [HttpGet]
        [Route("GetListOpportunity")]
        //[Authorize]
        public IActionResult GetListOpportunity(int page, int pageSize, string column, string sorting)
        {
            try
            {
                var result = objLogic.GetByFunnelGenID(page, pageSize, column, sorting);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetListOpportunityByUserID")]
        //[Authorize]
        public IActionResult GetListOpportunityByUserID(int page, int pageSize, int UserLoginID)
        {
            try
            {
                var result = objLogic.GetListOpportunityByUserID(page, pageSize, UserLoginID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("SearchSales")]
        //[Authorize]
        public IActionResult Search(int page, int pageSize, int UserLoginID, string search)
        {
            try
            {
                var result = objLogic.Search(page, pageSize, UserLoginID, search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("SearchMarketing")]
        //[Authorize]
        public IActionResult SearchMarketing(int page, int pageSize, string search)
        {
            try
            {
                var result = objLogic.SearchMarketing(page, pageSize, search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByOpportunityID")]
        public IActionResult GetByOpportunityID(long FunnelOpportunityID)
        {
            try
            {
                var result = objLogic.GetByOpportunityID(FunnelOpportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Insert(SalesFunnelOpportunity model)
        {
            try
            {
                var result = objLogic.Insert(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Update(Req_FunnelOpportunityUpdate_ViewModel ObjEntity)
        {
            try
            {
                var result = objLogic.Update(ObjEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertUpload/")]
        public IActionResult InsertUpload([FromForm] Req_FunnelOpportunityInsertUpload_ViewModel model)
        {
            try
            {
                var result = objLogic.InsertUpload(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Reassign")]
        //[Authorize]
        public IActionResult Reassign(Req_FunnelOpportunityReassign_ViewModel ObjEntity)
        {
            try
            {
                var result = objLogic.Reassign(ObjEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Cancel")]
        //[Authorize]
        public IActionResult Cancel(long FunnelOpportunityID)
        {
            try
            {
                var result = objLogic.Cancel(FunnelOpportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        //[Authorize]
        public IActionResult Delete(long FunnelOpportunityID)
        {
            try
            {
                var result = objLogic.Delete(FunnelOpportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ResendEmailInsertOpportunity")]
        public IActionResult ResendEmailInsertOpportunity(long FunnelOpportunityID)
        {
            try
            {
                var result = objLogic.ResendEmailInsertOpportunity(FunnelOpportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
