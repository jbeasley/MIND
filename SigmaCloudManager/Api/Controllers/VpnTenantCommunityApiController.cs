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
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful Web API for management of Tenant Nework associations with VPN Attachment Set resources
    /// </summary>
    public class VpnTenantCommunityInApiController : BaseApiController
    {
        public VpnTenantCommunityInApiController(IVpnTenantCommunityInService vpnTenantCommunityInService,
                                    IAttachmentSetService attachmentSetService,
                                    IVpnTenantCommunityInApiValidator vpnTenantCommunityInApiValidator,
                                    IMapper mapper) : base(mapper)
        {
            VpnTenantCommunityInService = vpnTenantCommunityInService;
            AttachmentSetService = attachmentSetService;
            VpnTenantCommunityInApiValidator = vpnTenantCommunityInApiValidator;
            this.Validator = vpnTenantCommunityInApiValidator;
        }

        private IVpnTenantCommunityInService VpnTenantCommunityInService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IVpnTenantCommunityInApiValidator VpnTenantCommunityInApiValidator { get; set; }

        // GET api/attachment-sets/4/tenant-communities/1
        [HttpGet("attachment-sets/{attachmentSetID}/tenant-communities/{tenantCommunityID}", Name = "GetVpnTenantCommunityIn")]
        public async Task<VpnTenantCommunityInApiModel> GetVpnTenantCommunityIn(int attachmentSetID, int tenantCommunityID)
        {
            var vpnTenantCommunityIn = await VpnTenantCommunityInService.GetOneAsync(attachmentSetID, tenantCommunityID);
            return Mapper.Map<VpnTenantCommunityInApiModel>(vpnTenantCommunityIn);
        }

        // GET api/attachment-sets/4/tenant-communities
        [HttpGet("attachment-sets/{attachmentSetID}/tenant-communities", Name = "GetVpnTenantCommunitiesIn")]
        public async Task<IEnumerable<VpnTenantCommunityInApiModel>> GetVpnTenantCommunitiesIn(int attachmentSetID)
        {
            var vpnTenantCommunityIns = await VpnTenantCommunityInService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            return Mapper.Map<List<VpnTenantCommunityInApiModel>>(vpnTenantCommunityIns);
        }

        // POST api/attachment-sets/4/tenant-communities/values
        [HttpPost("attachment-sets/{attachmentSetID}/tenant-communities")]
        public async Task<IActionResult> CreateVpnTenantCommunityIn(int attachmentSetID, [FromBody]VpnTenantCommunityInRequestApiModel apiRequest)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetID);
            if (attachmentSet == null)
            {
                ModelState.AddModelError(string.Empty, "The Attachment Set could not be found.");
                return new ValidationFailedResult(ModelState);
            }

            apiRequest.AttachmentSetID = attachmentSetID;

            await VpnTenantCommunityInApiValidator.ValidateNewAsync(apiRequest);
            if (!VpnTenantCommunityInApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var vpnTenantCommunityIn = Mapper.Map<VpnTenantCommunityIn>(apiRequest);

            try
            {
                var result = await VpnTenantCommunityInService.AddAsync(vpnTenantCommunityIn);
                var item = await VpnTenantCommunityInService.GetByIDAsync(vpnTenantCommunityIn.VpnTenantCommunityInID);

                return CreatedAtRoute("GetVpnTenantCommunityIn", new
                {
                    attachmentSetID = vpnTenantCommunityIn.AttachmentSetID,
                    tenantCommunityID = vpnTenantCommunityIn.TenantCommunityID
                },
                Mapper.Map<VpnTenantCommunityInApiModel>(item));
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

        // DELETE api/attachment-sets/1/tenant-communities/1
        [HttpDelete("attachment-sets/{attachmentSetID}/tenant-communities/{tenantCommunityID}")]
        public async Task<IActionResult> DeleteVpnTenantCommunityIn(int attachmentSetID, int tenantCommunityID)
        {
            // Get the VpnTenantCommunityIn to delete

            var vpnTenantCommunityIn = await VpnTenantCommunityInService.GetOneAsync(attachmentSetID, tenantCommunityID);
            if (vpnTenantCommunityIn == null)
            {
                return NotFound();
            }

            try
            {
                var result = await VpnTenantCommunityInService.DeleteAsync(vpnTenantCommunityIn);
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
