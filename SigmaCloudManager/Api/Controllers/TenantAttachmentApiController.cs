using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using SCM.Hubs;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Api.Models;
using SCM.Models;
using SCM.Validators;
using SCM.Api.Validators;
using SCM.Factories;
using Microsoft.Extensions.Options;

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful Web API for management of Attachment resources
    /// </summary>
    public class TenantAttachmentApiController : BaseApiController
    {
        public TenantAttachmentApiController(ITenantAttachmentService tenantAttachmentService,
                                    IRoutingInstanceService vrfService,
                                    ITenantService tenantService,
                                    IAttachmentApiValidator attachmentApiValidator,
                                    IMapper mapper,
                                    IHubContext<NetworkSyncHub> context) : base(mapper)
        {
            TenantAttachmentService = tenantAttachmentService;
            RoutingInstanceService = vrfService;
            TenantService = tenantService;
            HubContext = context;

            AttachmentApiValidator = attachmentApiValidator;
            AttachmentApiValidator.ValidationDictionary = new ModelStateWrapper(this.ModelState);
        }

        private ITenantAttachmentService TenantAttachmentService { get; set; }
        private IAttachmentApiValidator AttachmentApiValidator { get; set; }
        private IRoutingInstanceService RoutingInstanceService { get; set; }
        private ITenantService TenantService { get; set; }
        private IHubContext<NetworkSyncHub> HubContext { get; set; }

        // GET: api/tenant/1/attachments
        [HttpGet("tenant/{id}/attachments")]
        public async Task<IEnumerable<AttachmentApiModel>> GetAllByTenant(int id)
        {
            var tenant = await TenantService.GetByIDAsync(id);
            if (tenant != null)
            {
                var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(tenant.TenantID);
                return Mapper.Map<List<AttachmentApiModel>>(attachments);
            }
            else
            {
                return null;
            }
        }

        // GET api/attachments/1
        [HttpGet("attachments/{id}", Name ="GetAttachment")]
        public async Task<AttachmentApiModel> Get(int id)
        {
            var attachment = await TenantAttachmentService.GetByIDAsync(id);
            return Mapper.Map<AttachmentApiModel>(attachment);
        }

        // POST api/attachments/values
        [HttpPost("attachments")]
        public async Task<IActionResult> Create([FromBody]AttachmentRequestApiModel apiRequest)
        {
            await AttachmentApiValidator.ValidateNewAsync(apiRequest);
            if (!AttachmentApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {  
                var attachmentRequest = Mapper.Map<AttachmentRequest>(apiRequest);
                var result = await TenantAttachmentService.AddAsync(attachmentRequest);
                var item = (Attachment)result.Item;

                // Get the attachment from the service - this will return a fully populated model

                var attachment = await TenantAttachmentService.GetByIDAsync(item.AttachmentID);
                return CreatedAtRoute("GetAttachment", new
                {
                    id = attachment.AttachmentID
                }, 
                Mapper.Map<AttachmentApiModel>(attachment));
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

            catch (FactoryFailureException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = ex.Message
                });
            }
        }

        // DELETE api/attachments/5
        [HttpDelete("attachments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await TenantAttachmentService.GetByIDAsync(id);
            if (item == null)
            {
                ModelState.AddModelError("id", "The Attachment was not found.");
                return NotFound(ModelState);
            }

            // Check if the Attachment can be deleted. 

            AttachmentApiValidator.ValidateDelete(item);
            if (!AttachmentApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try {

                if (!item.Created || item.Vifs.Any(x => !x.Created))
                {
                    // The Attachment is operational in the network so 
                    // delete the resource from the network first

                    await TenantAttachmentService.DeleteFromNetworkAsync(item);
                }

                await TenantAttachmentService.DeleteAsync(item);
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

            catch (Exception /* ex */ )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/attachments/1/checksync
        [HttpPost("attachments/{id}/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<ActionResult> CheckSync(int id)
        {
            var item = await TenantAttachmentService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                var result = await TenantAttachmentService.CheckNetworkSyncAsync(item);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = $"Attachment {item.Name} has been checked and is synchronised with the network."
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = $"Attachment {item.Name} has been checked and is NOT synchronised with the network."
                        + "Press the 'Sync' button to update the network."
                    });
                }
            }
            catch (Exception /* ex */ )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/tenants/1/attachments/checksync
        [HttpPost("tenants/{id}/attachments/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> CheckSyncAllByTenantID(int id)
        {
            var tenant = await TenantService.GetByIDAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(id, roleRequireSyncToNetwork: true);
            if (attachments.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Attachments supporting check-sync to network were found."
                });
            }

            try
            {

                var progress = new Progress<ServiceResult>(async x => await UpdateClientProgress(x));
                var results = await TenantAttachmentService.CheckNetworkSyncAsync(attachments, progress);
                if (results.Where(q => q.IsSuccess).Count() == results.Count())
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "All Attachments have been checked and are synchronised with the network."
                    });
                }
                else
                {
                    // One or more Attachments are not synchronised with the network

                    return Ok(new
                    {
                        Success = false,
                        Message = results.SelectMany(q => q.GetMessageList())
                    });
                }
            }
            catch (Exception /* ex */ )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/attachments/1/sync
        [HttpPost("attachments/{id}/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> Sync(int id)
        {
            var item = await TenantAttachmentService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                await TenantAttachmentService.SyncToNetworkAsync(item);
                return Ok(new
                {
                    Success = true,
                    Message = $"Attachment {item.Name} is synchronised with the network."
                });
            }

            catch (Exception /* ex */ )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/tenants/1/attachments/sync
        [HttpPost("tenants/{id}/attachments/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> SyncAllByTenantID(int id)
        {
            var tenant = await TenantService.GetByIDAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            var attachments = await TenantAttachmentService.GetAllByTenantIDAsync(id, roleRequireSyncToNetwork: true);
            if (attachments.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Attachments supporting sync to network were found."
                });
            }

            try
            {
                var progress = new Progress<ServiceResult>(async x => await UpdateClientProgress(x));
                await TenantAttachmentService.SyncToNetworkAsync(attachments, progress);
                return Ok(new
                {
                    Success = true,
                    Message = "All Attachments are synchronised with the network."
                });
            }

            catch (Exception /* ex */ )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }


        /// <summary>
        /// Delegate method which is called when sync or checksync of an 
        /// individual Attachment has completed.
        /// </summary>
        /// <param name="result"></param>
        private Task UpdateClientProgress(ServiceResult result)
        {
            var attachment = (Attachment)result.Item;
            var tenant = (Tenant)result.Context;

            // Update all clients which are subscribed to the Tenant context
            // supplied in the result object

            var apiModel = Mapper.Map<AttachmentApiModel>(attachment);
            return HubContext.Clients.Group($"TenantAttachment_{tenant.TenantID}")
                .SendAsync("onSingleComplete", apiModel, result.IsSuccess, string.Empty);
        }
    }
}
