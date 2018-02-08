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
    /// Defines a RESTful web API for management of Tenant Community resources.
    /// </summary>
    public class TenantCommunityApiController : BaseApiController
    {
        public TenantCommunityApiController(ITenantCommunityService tenantCommunityService,
            ITenantCommunityApiValidator tenantCommunityApiValidator,
            IMapper mapper) : base(mapper)
        {
            TenantCommunityApiValidator = tenantCommunityApiValidator;
            this.Validator = tenantCommunityApiValidator;
            TenantCommunityService = tenantCommunityService;
        }

        private ITenantCommunityService TenantCommunityService { get; set; }
        private ITenantCommunityApiValidator TenantCommunityApiValidator { get; set; }

        // GET: api/tenants/1/tenantCommunities
        [HttpGet("tenants/{id}/tenant-communities")]
        public async Task<IEnumerable<TenantCommunityApiModel>> GetAll(int id)
        {
            var tenants = await TenantCommunityService.GetAllByTenantIDAsync(id);
            return Mapper.Map<List<TenantCommunityApiModel>>(tenants);
        }

        // GET api/tenant-communities/1
        [HttpGet("tenant-communities/{id}", Name = "GetTenantCommunity")]
        public async Task<TenantCommunityApiModel> Get(int id)
        {
            var tenant = await TenantCommunityService.GetByIDAsync(id);
            return Mapper.Map<TenantCommunityApiModel>(tenant);
        }

        // POST api/tenants/1/tenantCommunities
        [HttpPost("tenants/{id}/tenant-communities")]
        public async Task<IActionResult> Create(int id, [FromBody]TenantCommunityRequestApiModel apiRequest)
        {
            apiRequest.TenantID = id;

            try
            {
                await TenantCommunityApiValidator.ValidateNewAsync(apiRequest);
                if (!TenantCommunityApiValidator.ValidationDictionary.IsValid)
                {
                    return new ValidationFailedResult(ModelState);
                }

                var tenantCommunity = Mapper.Map<TenantCommunity>(apiRequest);
                await TenantCommunityService.AddAsync(tenantCommunity);

                // Get fully populated model from the DB

                var item = await TenantCommunityService.GetByIDAsync(tenantCommunity.TenantCommunityID);
                return CreatedAtRoute("GetTenantCommunity", new
                {
                    id = tenantCommunity.TenantCommunityID
                }, 
                Mapper.Map<TenantCommunityApiModel>(item));
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

        // DELETE api/tenantCommunities/5
        [HttpDelete("tenant-communities/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tenantCommunity = await TenantCommunityService.GetByIDAsync(id);
            if (tenantCommunity == null)
            {
                return NotFound();
            }

            try
            {
                await TenantCommunityApiValidator.ValidateDeleteAsync(tenantCommunity);
                if (TenantCommunityApiValidator.ValidationDictionary.IsValid)
                {
                    var result = await TenantCommunityService.DeleteAsync(tenantCommunity);
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