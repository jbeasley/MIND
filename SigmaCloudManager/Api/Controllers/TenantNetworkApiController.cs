using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Services;
using SCM.Api.Models;
using SCM.Models;
using SCM.Api.Validators;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful web API for management of Tenant Network resources.
    /// </summary>
    public class TenantNetworkApiController : BaseApiController
    {
        public TenantNetworkApiController(ITenantNetworkService tenantNetworkService,
            ITenantNetworkApiValidator tenantNetworkApiValidator,
            IMapper mapper) : base(mapper)
        {
            TenantNetworkApiValidator = tenantNetworkApiValidator;
            this.Validator = tenantNetworkApiValidator;
            TenantNetworkService = tenantNetworkService;
        }

        private ITenantNetworkService TenantNetworkService { get; set; }
        private ITenantNetworkApiValidator TenantNetworkApiValidator { get; set; }

        // GET: api/tenants/{id}/tenant-networks
        [HttpGet("tenants/{id}/tenant-networks")]
        public async Task<IEnumerable<TenantNetworkApiModel>> GetAll(int id)
        {
            var tenants = await TenantNetworkService.GetAllByTenantIDAsync(id);
            return Mapper.Map<List<TenantNetworkApiModel>>(tenants);
        }

        // GET api/tenant-networks/1
        [HttpGet("tenantNetworks/{id}", Name = "GetTenantNetwork")]
        public async Task<TenantNetworkApiModel> Get(int id)
        {
            var tenant = await TenantNetworkService.GetByIDAsync(id);
            return Mapper.Map<TenantNetworkApiModel>(tenant);
        }

        // POST api/tenants/1/tenant-networks
        [HttpPost("tenants/{id}/tenant-networks")]
        public async Task<IActionResult> Create(int id, [FromBody]TenantNetworkRequestApiModel apiRequest)
        {
            apiRequest.TenantID = id;

            try
            {
                await TenantNetworkApiValidator.ValidateNewAsync(apiRequest);
                if (!TenantNetworkApiValidator.ValidationDictionary.IsValid)
                {
                    return new ValidationFailedResult(ModelState);
                }

                var tenantNetwork = Mapper.Map<TenantNetwork>(apiRequest);
                await TenantNetworkService.AddAsync(tenantNetwork);

                // Get fully populated model from the DB

                var item = await TenantNetworkService.GetByIDAsync(tenantNetwork.TenantNetworkID);
                return CreatedAtRoute("GetTenantNetwork", new
                {
                    id = tenantNetwork.TenantNetworkID
                }, 
                Mapper.Map<TenantNetworkApiModel>(item));
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator."
                });
            }
        }

        // DELETE api/tenant-networks/5
        [HttpDelete("tenant-networks/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tenantNetwork = await TenantNetworkService.GetByIDAsync(id);
            if (tenantNetwork == null)
            {
                return NotFound();
            }

            try
            {
                await TenantNetworkApiValidator.ValidateDeleteAsync(tenantNetwork);
                if (TenantNetworkApiValidator.ValidationDictionary.IsValid)
                {
                    var result = await TenantNetworkService.DeleteAsync(tenantNetwork);
                    return NoContent();
                }
                else
                {
                    return new ValidationFailedResult(ModelState);
                }
            }

            catch (DbUpdateException /* ex */)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "The database operation failed. Please try again. "
                   + "If the problem persists please contact your administrator."
                });
            }
        }
    }
}