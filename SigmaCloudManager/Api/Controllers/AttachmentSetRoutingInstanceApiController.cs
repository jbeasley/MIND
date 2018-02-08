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

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful Web API for management of Attachment Set VRF resources
    /// </summary>
    public class AttachmentSetRoutingInstanceApiController : BaseApiController
    {
        public AttachmentSetRoutingInstanceApiController(IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
                                    IAttachmentSetRoutingInstanceApiValidator attachmentSetRoutingInstanceApiValidator,
                                    IMapper mapper) : base(mapper)
        {
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            AttachmentSetRoutingInstanceApiValidator = attachmentSetRoutingInstanceApiValidator;
            this.Validator = attachmentSetRoutingInstanceApiValidator;
        }

        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; set; }
        private IAttachmentSetApiValidator AttachmentSetApiValidator { get; set; }
        private IAttachmentSetRoutingInstanceApiValidator AttachmentSetRoutingInstanceApiValidator { get; set; }


        // GET api/attachment-sets/1/vrfs/1
        [HttpGet("attachment-sets/{attachmentSetID}/vrfs/{vrfID}", Name = "GetAttachmentSetRoutingInstance")]
        public async Task<AttachmentSetRoutingInstanceApiModel> GetAttachmentSetRoutingInstance(int attachmentSetID, int vrfID)
        {
            var attachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByAttachmenSetAndRoutingInstanceAsync(attachmentSetID, vrfID);
            return Mapper.Map<AttachmentSetRoutingInstanceApiModel>(attachmentSetRoutingInstance);
        }

        // GET api/attachment-sets/1/vrfs
        [HttpGet("attachment-sets/{id}/vrfs", Name = "GetAttachmentSetRoutingInstances")]
        public async Task<IEnumerable<AttachmentSetRoutingInstanceApiModel>> GetAttachmentSetRoutingInstances(int id)
        {
            var attachmentSetRoutingInstances = await AttachmentSetRoutingInstanceService.GetAllByAttachmentSetIDAsync(id);
            return Mapper.Map<List<AttachmentSetRoutingInstanceApiModel>>(attachmentSetRoutingInstances);
        }

        // POST api/attachment-sets/1/vrfs/values
        [HttpPost("attachment-sets/{id}/vrfs")]
        public async Task<IActionResult> CreateAttachmentSetRoutingInstance(int id, [FromBody]AttachmentSetRoutingInstanceRequestApiModel apiRequest)
        {
            // Create the Attachment Set VRF

            apiRequest.AttachmentSetID = id;
            await AttachmentSetRoutingInstanceApiValidator.ValidateNewAsync(apiRequest);
            if (!AttachmentSetRoutingInstanceApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var attachmentSetRoutingInstance = Mapper.Map<AttachmentSetRoutingInstance>(apiRequest);

            try
            {
                var result = await AttachmentSetRoutingInstanceService.AddAsync(attachmentSetRoutingInstance);
                var item = await AttachmentSetRoutingInstanceService.GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID);

                return CreatedAtRoute("GetAttachmentSetRoutingInstance", new
                {
                    attachmentSetID = attachmentSetRoutingInstance.AttachmentSetID,
                    vrfID = attachmentSetRoutingInstance.RoutingInstanceID
                },
                Mapper.Map<AttachmentSetRoutingInstanceApiModel>(item));
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

        // DELETE api/attachment-sets/1/vrfs/1
        [HttpDelete("attachment-sets/{attachmentSetID}/vrfs/{vrfID}")]
        public async Task<IActionResult> DeleteAttachmentSetRoutingInstance(int attachmentSetID, int vrfID)
        {
            // Get the VRF to delete

            var attachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByAttachmenSetAndRoutingInstanceAsync(attachmentSetID, vrfID);
            if (attachmentSetRoutingInstance == null)
            {
                return NotFound();
            }

            try
            {
                var result = await AttachmentSetRoutingInstanceService.DeleteAsync(attachmentSetRoutingInstance);
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
        // PUT api/attachment-sets/1/vrfs/5/values
        [HttpPut("attachment-set/{attachmentSetID}/vrfs/{vrfID}")]
        public async Task<IActionResult> Update(int attachmentSetID, int vrfID, [FromBody]AttachmentSetRoutingInstanceUpdateApiModel updateApiModel)
        {
            if (attachmentSetID != updateApiModel.AttachmentSetID)
            {
                return NotFound();
            }

            if (vrfID != updateApiModel.RoutingInstanceID)
            {
                return NotFound();
            }

            var attachmentSetRoutingInstance = await AttachmentSetRoutingInstanceService.GetByAttachmenSetAndRoutingInstanceAsync(attachmentSetID,vrfID, includeProperties: false);
            if (attachmentSetRoutingInstance == null)
            {
                return NotFound();
            }

            // Update with values from the update model

            Mapper.Map(updateApiModel, attachmentSetRoutingInstance);
            
            try
            {
                await AttachmentSetRoutingInstanceService.UpdateAsync(attachmentSetRoutingInstance);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                // Retrieve current Attachment Set VRF to compare values

                var current = await AttachmentSetRoutingInstanceService.GetByAttachmenSetAndRoutingInstanceAsync(attachmentSetID, vrfID);

                ModelState.AddModelError("RowVersion", $"Current value: {Convert.ToBase64String(current.RowVersion)}");

                var proposedAdvertisedIpRoutingPreference = (int?)exceptionEntry.Property("AdverisedIpRoutingPreference").CurrentValue;
                if (current.AdvertisedIpRoutingPreference != proposedAdvertisedIpRoutingPreference)
                {
                    ModelState.AddModelError("AdvertisedIpRoutingPreference", $"Current value: {current.AdvertisedIpRoutingPreference}");
                }

                var proposedLocalIpRoutingPreference = (int?)exceptionEntry.Property("LocalIpRoutingPreference").CurrentValue;
                if (current.LocalIpRoutingPreference != proposedLocalIpRoutingPreference)
                {
                    ModelState.AddModelError("LocalIpRoutingPreference", $"Current value: {current.LocalIpRoutingPreference}");
                }

                var proposedMulticastDesignatedRouterPreference = (int?)exceptionEntry.Property("MulticastDesignatedRouterPreference").CurrentValue;
                if (current.MulticastDesignatedRouterPreference != proposedMulticastDesignatedRouterPreference)
                {
                    ModelState.AddModelError("MulticastDesignatedRouterPreference", $"Current value: {current.MulticastDesignatedRouterPreference}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original values. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been returned.");

                return BadRequest(ModelState);
            }

            catch (DbUpdateException /** ex **/)
            {
                // Add logging for the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Something went wrong during the database update. "
                   + "The issue has been logged. "
                   + "Please try again, and contact your system admin if the problem persists."
                });
            }

            // Get fully populated model from db to return to caller

            var item = await AttachmentSetRoutingInstanceService.GetByAttachmenSetAndRoutingInstanceAsync(attachmentSetID, vrfID);

            return Ok(new
            {
                Item = Mapper.Map<AttachmentSetRoutingInstanceApiModel>(item)
            });
        }
    }
}
