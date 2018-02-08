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
    /// Defines a RESTful web API for management of Tenant resources.
    /// </summary>
    public class TenantApiController : BaseApiController
    {
        public TenantApiController(ITenantService tenantService,
            ITenantApiValidator tenantApiValidator,
            IMapper mapper) : base(mapper)
        {
            TenantApiValidator = tenantApiValidator;
            this.Validator = tenantApiValidator;
            TenantService = tenantService;
        }

        private ITenantService TenantService { get; set; }
        private ITenantApiValidator TenantApiValidator { get; set; }

        // GET: api/tenants
        [HttpGet("tenants")]
        public async Task<IEnumerable<TenantApiModel>> GetAll(int id)
        {
            var tenants = await TenantService.GetAllAsync();
            return Mapper.Map<List<TenantApiModel>>(tenants);
        }

        // GET api/tenants/1
        [HttpGet("tenants/{id}", Name = "GetTenant")]
        public async Task<TenantApiModel> Get(int id)
        {
            var tenant = await TenantService.GetByIDAsync(id);
            return Mapper.Map<TenantApiModel>(tenant);
        }

        // POST api/tenants
        [HttpPost("tenants")]
        public async Task<IActionResult> Create([FromBody]TenantRequestApiModel apiModel)
        {
            var tenant = Mapper.Map<Tenant>(apiModel);

            try
            {
                await TenantService.AddAsync(tenant);

                // Get fully populated model from the db

                var item = await TenantService.GetByIDAsync(tenant.TenantID);
                return CreatedAtRoute("GetTenant", new { id = tenant.TenantID }, Mapper.Map<TenantApiModel>(item));
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

        // DELETE api/tenants/5
        [HttpDelete("tenants/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tenant = await TenantService.GetByIDAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            try
            {
                await TenantApiValidator.ValidateDeleteAsync(tenant);
                if (TenantApiValidator.ValidationDictionary.IsValid)
                {
                    var result = await TenantService.DeleteAsync(tenant);
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