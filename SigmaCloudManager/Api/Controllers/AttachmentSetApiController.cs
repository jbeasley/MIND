using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Services;
using SCM.Api.Models;
using SCM.Models;
using SCM.Api.Validators;
using SCM.Validators;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful Web API for management of Attachment Set resources
    /// </summary>
    public class AttachmentSetApiController : BaseApiController
    {
        public AttachmentSetApiController(IAttachmentSetService attachmentSetService,
                                    ITenantService tenantService,
                                    IAttachmentSetApiValidator attachmentSetApiValidator,
                                    IMapper mapper) : base(mapper)
        {
            AttachmentSetService = attachmentSetService;
            TenantService = tenantService;
            AttachmentSetApiValidator = attachmentSetApiValidator;
            this.Validator = attachmentSetApiValidator;
        }

        private ITenantService TenantService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; set; }
        private IAttachmentSetApiValidator AttachmentSetApiValidator { get; set; }

        // GET: api/tenants/1/attachment-sets
        [HttpGet("tenants/{id}/attachment-sets")]
        public async Task<IEnumerable<AttachmentSetApiModel>> GetAllByTenant(int id)
        {
            var tenant = await TenantService.GetByIDAsync(id);
            if (tenant != null)
            {
                var attachmentSets = await AttachmentSetService.GetAllByTenantAsync(tenant);
                return Mapper.Map<List<AttachmentSetApiModel>>(attachmentSets);
            }
            else
            {
                return null;
            }
        }

        // GET api/attachment-sets/1
        [HttpGet("attachment-sets/{id}", Name = "GetAttachmentSet")]
        public async Task<AttachmentSetApiModel> GetAttachmentSet(int id)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(id);
            return Mapper.Map<AttachmentSetApiModel>(attachmentSet);
        }

        // POST api/attachment-sets/1/validate
        [HttpPost("attachment-sets/{id}/validate")]
        public async Task<IActionResult> ValidateAttachmentSet(int id)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(id);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            await AttachmentSetApiValidator.ValidateAsync(attachmentSet);
            if (AttachmentSetApiValidator.ValidationDictionary.IsValid)
            {
                return Ok();
            }
            else
            {
                return new ValidationFailedResult(ModelState);
            }
        }

        // POST api/attachment-sets/values
        [HttpPost("attachment-sets")]
        public async Task<IActionResult> Create([FromBody]AttachmentSetRequestApiModel apiRequest)
        {
            await AttachmentSetApiValidator.ValidateNewAsync(apiRequest);
            if (!AttachmentSetApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var attachmentSet = Mapper.Map<AttachmentSet>(apiRequest);

            try
            {
                var result = await AttachmentSetService.AddAsync(attachmentSet);

                // Get the Attachment Set from the service - this will return a fully populated model

                var item = await AttachmentSetService.GetByIDAsync(attachmentSet.AttachmentSetID);
                return CreatedAtRoute("GetAttachmentSet", new { id = attachmentSet.AttachmentSetID }, Mapper.Map<AttachmentSetApiModel>(item));
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

        // PUT api/attachment-sets/values
        [HttpPut("attachment-sets/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]AttachmentSetUpdateApiModel updateApiModel)
        {
            if (id != updateApiModel.AttachmentSetID)
            {
                return NotFound();
            }

            // Retrieve Attachment Set directly from repository (not from the service layer) so that
            // we don't have any child entities attached that would otherwise need to be updated

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id, includeProperties: false);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            // Update AttachmentSet with values from the update model, then validate

            Mapper.Map(updateApiModel, attachmentSet);

            await AttachmentSetApiValidator.ValidateChangesAsync(attachmentSet);
            if (!AttachmentSetApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                await AttachmentSetService.UpdateAsync(attachmentSet);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                // Retrieve current Attachment Set to compare values

                var currentAttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSet.AttachmentSetID);

                ModelState.AddModelError("RowVersion", $"Current value: {Convert.ToBase64String(currentAttachmentSet.RowVersion)}");

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (currentAttachmentSet.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {currentAttachmentSet.Name}");
                }

                var proposedSubRegionID = (int?)exceptionEntry.Property("SubRegionID").CurrentValue;
                if (currentAttachmentSet.SubRegionID != proposedSubRegionID)
                {
                    ModelState.AddModelError("SubRegionID", $"Current value: {currentAttachmentSet.SubRegion.Name}");
                }

                var proposedAttachmentRedundancyID = (int)exceptionEntry.Property("AttachmentRedundancyID").CurrentValue;
                if (currentAttachmentSet.AttachmentRedundancyID != proposedAttachmentRedundancyID)
                {
                    ModelState.AddModelError("AttachmentRedundancyID", $"Current value: {currentAttachmentSet.AttachmentRedundancy.Name}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original values. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been returned.");

                return BadRequest(ModelState);
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

            // Get fully populated model from db to return to caller

            var item = await AttachmentSetService.GetByIDAsync(updateApiModel.AttachmentSetID);

            return Ok(new
            {
                Success = true,
                Item = Mapper.Map<AttachmentSetApiModel>(item)
            });
        }

        // DELETE api/attachment-sets/5
        [HttpDelete("attachment-sets/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentAttachmentSet = await AttachmentSetService.GetByIDAsync(id);
            if (currentAttachmentSet == null)
            {
                return NotFound();
            }

            await AttachmentSetApiValidator.ValidateDeleteAsync(currentAttachmentSet);
            if (!AttachmentSetApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                await AttachmentSetService.DeleteAsync(currentAttachmentSet);
                return NoContent();
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
    }
}
