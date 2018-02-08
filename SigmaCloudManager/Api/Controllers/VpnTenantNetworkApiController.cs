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
    public class VpnTenantNetworkInApiController : BaseApiController
    {
        public VpnTenantNetworkInApiController(IVpnTenantNetworkInService vpnTenantNetworkInService,
                                    IAttachmentSetService attachmentSetService,
                                    IVpnTenantNetworkInApiValidator vpnTenantNetworkInApiValidator,
                                    IMapper mapper) : base(mapper)
        {
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            AttachmentSetService = attachmentSetService;
            VpnTenantNetworkInApiValidator = vpnTenantNetworkInApiValidator;
            this.Validator = vpnTenantNetworkInApiValidator;
        }

        private IVpnTenantNetworkInService VpnTenantNetworkInService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IVpnTenantNetworkInApiValidator VpnTenantNetworkInApiValidator { get; set; }

        // GET attachment-sets/4/tenant-networks/1
        [HttpGet("attachment-sets/{attachmentSetID}/tenant-networks/{tenantNetworkID}", Name = "GetVpnTenantNetworkIn")]
        public async Task<VpnTenantNetworkInApiModel> GetVpnTenantNetworkIn(int attachmentSetID, int tenantNetworkID)
        {
            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetOneAsync(attachmentSetID, tenantNetworkID);
            return Mapper.Map<VpnTenantNetworkInApiModel>(vpnTenantNetworkIn);
        }

        // GET api/attachment-sets/4/tenant-networks
        [HttpGet("attachment-sets/{attachmentSetID}/tenant-networks", Name = "GetVpnTenantNetworkIns")]
        public async Task<IEnumerable<VpnTenantNetworkInApiModel>> GetVpnTenantNetworkIns(int attachmentSetID)
        {
            var vpnTenantNetworkIns = await VpnTenantNetworkInService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            return Mapper.Map<List<VpnTenantNetworkInApiModel>>(vpnTenantNetworkIns);
        }

        // POST api/attachment-sets/4/tenant-networks/values
        [HttpPost("attachment-sets/{attachmentSetID}/tenant-networks")]
        public async Task<IActionResult> CreateVpnTenantNetworkIn(int attachmentSetID, [FromBody]VpnTenantNetworkInRequestApiModel apiRequest)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetID);
            if (attachmentSet == null)
            {
                ModelState.AddModelError(string.Empty, "The Attachment Set could not be found.");
                return new ValidationFailedResult(ModelState);
            }

            apiRequest.AttachmentSetID = attachmentSetID;

            await VpnTenantNetworkInApiValidator.ValidateNewAsync(apiRequest);
            if (!VpnTenantNetworkInApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var vpnTenantNetworkIn = Mapper.Map<VpnTenantNetworkIn>(apiRequest);

            try
            {
                var result = await VpnTenantNetworkInService.AddAsync(vpnTenantNetworkIn);
                var item = await VpnTenantNetworkInService.GetByIDAsync(vpnTenantNetworkIn.VpnTenantNetworkInID);

                return CreatedAtRoute("GetVpnTenantNetworkIn", new
                {
                    attachmentSetID = vpnTenantNetworkIn.AttachmentSetID,
                    tenantNetworkID = vpnTenantNetworkIn.TenantNetworkID
                },
                Mapper.Map<VpnTenantNetworkInApiModel>(item));
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

        // DELETE api/attachment-sets/1/tenant-networks/1
        [HttpDelete("attachment-sets/{attachmentSetID}/tenant-networks/{tenantNetworkID}")]
        public async Task<IActionResult> DeleteVpnTenantNetworkIn(int attachmentSetID, int tenantNetworkID)
        {
            // Get the VpnTenantNetworkIn to delete

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetOneAsync(attachmentSetID, tenantNetworkID);
            if (vpnTenantNetworkIn == null)
            {
                return NotFound();
            }

            try
            {
                var result = await VpnTenantNetworkInService.DeleteAsync(vpnTenantNetworkIn);
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
