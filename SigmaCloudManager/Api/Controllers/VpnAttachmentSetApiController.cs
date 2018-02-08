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
    /// Defines a RESTful Web API for management of Attachment Set assocations with VPN resources
    /// </summary>
    public class VpnAttachmentSetApiController : BaseApiController
    {
        public VpnAttachmentSetApiController(IVpnAttachmentSetService vpnAttachmentSetService,
                                    IVpnAttachmentSetApiValidator vpnAttachmentSetApiValidator,
                                    IMapper mapper) : base(mapper)
        {
            VpnAttachmentSetService = vpnAttachmentSetService;
            VpnAttachmentSetApiValidator = vpnAttachmentSetApiValidator;
            this.Validator = vpnAttachmentSetApiValidator;
        }

        private IVpnAttachmentSetService VpnAttachmentSetService { get; set; }
        private IVpnAttachmentSetApiValidator VpnAttachmentSetApiValidator { get; set; }

        // GET api/vpns/1/attachment-sets/1
        [HttpGet("vpns/{vpnID}/attachment-sets/{attachmentSetID}", Name = "GetVpnAttachmentSet")]
        public async Task<VpnAttachmentSetApiModel> GetVpnAttachmentSet(int vpnID, int attachmentSetID)
        {
            var vpnAttachmentSet = await VpnAttachmentSetService.GetByVpnAndAttachmentSetAsync(vpnID, attachmentSetID);
            return Mapper.Map<VpnAttachmentSetApiModel>(vpnAttachmentSet);
        }

        // GET api/vpns/1/attachment-sets
        [HttpGet("vpns/{id}/attachment-sets", Name = "GetVpnAttachmentSets")]
        public async Task<IEnumerable<VpnAttachmentSetApiModel>> GetVpnAttachmentSets(int id)
        {
            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByVpnIDAsync(id);
            return Mapper.Map<List<VpnAttachmentSetApiModel>>(vpnAttachmentSets);
        }

        // POST api/vpns/1/attachment-sets/values
        [HttpPost("vpns/{id}/attachment-sets")]
        public async Task<IActionResult> CreateVpnAttachmentSet(int id, [FromBody]VpnAttachmentSetRequestApiModel apiRequest)
        {
            // Create the VpnAttachmentSet

            apiRequest.VpnID = id;
            await VpnAttachmentSetApiValidator.ValidateNewAsync(apiRequest);
            if (!VpnAttachmentSetApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var vpnAttachmentSet = Mapper.Map<VpnAttachmentSet>(apiRequest);

            try
            {
                var result = await VpnAttachmentSetService.AddAsync(vpnAttachmentSet);
                var item = await VpnAttachmentSetService.GetByIDAsync(vpnAttachmentSet.VpnAttachmentSetID);

                return CreatedAtRoute("GetVpnAttachmentSet", new
                {
                    vpnID = vpnAttachmentSet.VpnID,
                    attachmentSetID = vpnAttachmentSet.AttachmentSetID
                },
                Mapper.Map<VpnAttachmentSetApiModel>(item));
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

        // DELETE api/vpns/1/attachment-sets/1
        [HttpDelete("vpns/{vpnID}/attachment-sets/{attachmentSetID}")]
        public async Task<IActionResult> DeleteVpnAttachmentSet(int vpnID, int attachmentSetID)
        {
            // Get the VpnAttachmentSet to delete

            var vpnAttachmentSet = await VpnAttachmentSetService.GetByVpnAndAttachmentSetAsync(vpnID, attachmentSetID);
            if (vpnAttachmentSet == null)
            {
                return NotFound();
            }

            try
            {
                var result = await VpnAttachmentSetService.DeleteAsync(vpnAttachmentSet);
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
