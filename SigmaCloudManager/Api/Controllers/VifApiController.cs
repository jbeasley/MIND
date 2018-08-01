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
using SCM.Api.Validators;
using SCM.Services;
using SCM.Api.Models;
using SCM.Models;
using SCM.Models.RequestModels;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful Web API for management of Vif resources
    /// </summary>
    public class VifApiController : BaseApiController
    {
        public VifApiController(IVifService vifService,
            IAttachmentService attachmentService,
            IVifApiValidator vifApiValidator,
            IMapper mapper,
             IHubContext<NetworkSyncHub> context) : base(mapper)
        {
            AttachmentService = attachmentService;
            VifService = vifService;
            HubContext = context;

            VifApiValidator = vifApiValidator;
            this.Validator = vifApiValidator;
        }

        private IAttachmentService AttachmentService { get; set; }
        private IVifApiValidator VifApiValidator { get; set; }
        private IVifService VifService { get; set; }
        private IHubContext<NetworkSyncHub> HubContext { get; set; }

        // GET: api/attachments/1/vifs
        [HttpGet("attachments/{id}/vifs")]
        public async Task<IEnumerable<VifApiModel>> GetAllByAttachmentID(int id)
        {
            var vifs = await VifService.GetAllByAttachmentIDAsync(id);
            return Mapper.Map<List<VifApiModel>>(vifs);
        }

        // GET api/vifs/1
        [HttpGet("vifs/{id}", Name = "GetVif")]
        public async Task<VifApiModel> Get(int id)
        {
            var vif = await VifService.GetByIDAsync(id);
            return Mapper.Map<VifApiModel>(vif);
        }

        // POST api/vifs/values
        [HttpPost("vifs")]
        public async Task<IActionResult> Create([FromBody]VifRequestApiModel apiRequest)
        {
            await VifApiValidator.ValidateNewAsync(apiRequest);
            if (!VifApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                var result = await VifService.AddAsync(Mapper.Map<VifRequest>(apiRequest));
                var item = (Vif)result.Item;

                // Get fully populated VIF from the service

                var vif = await VifService.GetByIDAsync(item.VifID);
                return CreatedAtRoute("GetVif", new { id = vif.VifID }, Mapper.Map<VifApiModel>(vif));
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

        // DELETE api/vifs/5
        [HttpDelete("vifs/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await VifService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Check if the Vif can be deleted. 

            VifApiValidator.ValidateDelete(item);
            if (!VifApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                if (!item.Created)
                {
                    // The Vif is operational in the network so 
                    // delete the resource from the network first

                    await VifService.DeleteFromNetworkAsync(item);
                }

                await VifService.DeleteAsync(item);
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

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/vifs/1/checksync
        [HttpPost("vifs/{id}/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<ActionResult> CheckSync(int id)
        {
            var item = await VifService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                var result = await VifService.CheckNetworkSyncAsync(item);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = $"Vif {item.Name} has been checked and is synchronised with the network."
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = $"Vif {item.Name} has been checked and is NOT synchronised with the network."
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

        // POST /api/attachments/1/vifs/checksync
        [HttpPost("attachments/{id}/vifs/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> CheckSyncAllByAttachmentID(int id)
        {
            var attachment = await AttachmentService.GetByIDAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            var vifs = await VifService.GetAllByAttachmentIDAsync(id, roleRequireSyncToNetwork: true);
            if (vifs.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Vifs supporting check-sync to network were found."
                });
            }

            try
            {
                var progress = new Progress<ServiceResult>(UpdateClientProgress);
                var results = await VifService.CheckNetworkSyncAsync(vifs, progress);
                if (results.Where(q => q.IsSuccess).Count() == results.Count())
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "All Vifs have been checked and are synchronised with the network."
                    });
                }
                else
                {
                    // One or more VIFs are not synchronised with the network

                    return Ok(new
                    {
                        Success = false,
                        Message = results.SelectMany(q => q.GetMessageList())
                    });
                }
            }

            catch (Exception /* ex */  )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/vifs/1/sync
        [HttpPost("vifs/{id}/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> Sync(int id)
        {
            var item = await VifService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                await VifService.SyncToNetworkAsync(item);

                return Ok(new
                {
                    Success = true,
                    Message = $"Vif {item.Name} is synchronised with the network."
                });    
            }

            catch (UnstartableServiceException ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new { Message = ex.Message });
            }

            catch (Exception /** ex **/ )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/attachments/1/vifs/sync
        [HttpPost("attachments/{id}/vifs/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> SyncAllByAttachmentID(int id)
        {
            var attachment = await AttachmentService.GetByIDAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            var vifs = await VifService.GetAllByAttachmentIDAsync(id, roleRequireSyncToNetwork: true);
            if (vifs.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Vifs supporting sync to network were found."
                });
            }

            try
            {
                var progress = new Progress<ServiceResult>(UpdateClientProgress);
                await VifService.SyncToNetworkAsync(vifs, progress);

                return Ok(new
                {
                    Success = true,
                    Message = "All Vifs are synchronised with the network."
                });
            }

            catch (UnstartableServiceException ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new { Message = ex.Message });
            }

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        /// <summary>
        /// Delegate method which is called when sync or checksync of an 
        /// individual vif has completed.
        /// </summary>
        /// <param name="result"></param>
        private void UpdateClientProgress(ServiceResult result)
        {
            var vif = (Vif)result.Item;
            var attachment = (Attachment)result.Context;

            // Update all clients which are subscribed to the attachment context
            // supplied in the result object

            HubContext.Clients.Group($"Attachment_{attachment.AttachmentID}")
                .SendAsync("onSingleComplete",Mapper.Map<VifApiModel>(vif), result.IsSuccess);
        }
    }
}
