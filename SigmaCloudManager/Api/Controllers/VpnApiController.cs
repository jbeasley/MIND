using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using SCM.Services;
using SCM.Api.Models;
using SCM.Models;
using SCM.Hubs;
using SCM.Api.Validators;
using SCM.Factories;
using SCM.Models.RequestModels;

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful Web API for management of VPN resources
    /// </summary>
    public class VpnApiController : BaseApiController
    {
        public VpnApiController(IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            IVpnApiValidator vpnApiValidator,
            IMapper mapper,
            IHubContext<NetworkSyncHub> context) : base(mapper)
        {
            VpnService = vpnService;
            AttachmentSetService = attachmentSetService;
            HubContext = context;

            VpnApiValidator = vpnApiValidator;
            this.Validator = vpnApiValidator;
        }

        private IVpnApiValidator VpnApiValidator { get; set; }
        private IVpnService VpnService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IHubContext<NetworkSyncHub> HubContext { get; set; }

        // GET: api/vpns
        [HttpGet("vpns")]
        public async Task<IEnumerable<VpnApiModel>> GetAll(int id)
        {
            var vpns = await VpnService.GetAllAsync();
            return Mapper.Map<List<VpnApiModel>>(vpns);
        }

        // GET api/vpns/1
        [HttpGet("vpns/{id}", Name = "GetVpn")]
        public async Task<VpnApiModel> Get(int id)
        {
            var vpn = await VpnService.GetByIDAsync(id);
            return Mapper.Map<VpnApiModel>(vpn);
        }

        // POST api/vpns
        [HttpPost("vpns")]
        public async Task<IActionResult> Create([FromBody]VpnRequestApiModel apiRequest)
        {
            await VpnApiValidator.ValidateNewAsync(apiRequest);
            if (!VpnApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var vpnRequest = Mapper.Map<VpnRequest>(apiRequest);

            try
            {
                var result = await VpnService.AddAsync(vpnRequest);
                var vpn = (Vpn)result.Item;

                // Get fully populated model from db

                var item = await VpnService.GetByIDAsync(vpn.VpnID);
                return CreatedAtRoute("GetVpn", new
                {
                    id = vpn.VpnID
                },
                Mapper.Map<VpnApiModel>(item));
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

        // PUT api/vpns/5
        [HttpPut("vpns/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]VpnUpdateApiModel updateApiModel)
        {
            if (id != updateApiModel.VpnID)
            {
                return NotFound();
            }

            var vpn = await VpnService.GetByIDAsync(id, includeProperties: false);
            if (vpn == null)
            {
                return NotFound();
            }

            // Update Vpn with values from the update model, then validate

            Mapper.Map(updateApiModel, vpn);
            await VpnApiValidator.ValidateChangesAsync(vpn);

            if (!VpnApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                await VpnService.UpdateAsync(vpn);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                // Retrieve current vpn to compare values

                var currentVpn = await VpnService.GetByIDAsync(updateApiModel.VpnID);

                ModelState.AddModelError("RowVersion", $"Current value: {Convert.ToBase64String(currentVpn.RowVersion)}");

                var proposedDescription = (string)exceptionEntry.Property("Description").CurrentValue;
                if (currentVpn.Description != proposedDescription)
                {
                    ModelState.AddModelError("Description", $"Current value: {currentVpn.Description}");
                }

                if (exceptionEntry.Property("RegionID").CurrentValue != null)
                {
                    var proposedRegionID = (int)exceptionEntry.Property("RegionID").CurrentValue;
                    if (currentVpn.RegionID != proposedRegionID)
                    {
                        ModelState.AddModelError("RegionID", $"Current value: {currentVpn.RegionID}");
                    }
                }

                if (exceptionEntry.Property("MulticastVpnDirectionTypeID").CurrentValue != null)
                {
                    var proposedMulticastVpnDirectionTypeID = (int)exceptionEntry.Property("MulticastVpnDirectionTypeID").CurrentValue;
                    if (currentVpn.MulticastVpnDirectionTypeID != proposedMulticastVpnDirectionTypeID)
                    {
                        ModelState.AddModelError("MulticastVpnDirectionTypeID", $"Current value: {currentVpn.MulticastVpnDirectionTypeID}");
                    }
                }

                var proposedTenancyTypeID = (int)exceptionEntry.Property("VpnTenancyTypeID").CurrentValue;
                if (currentVpn.VpnTenancyTypeID != proposedTenancyTypeID)
                {
                    ModelState.AddModelError("VpnTenancyTypeID", $"Current value: {currentVpn.VpnTenancyTypeID}");
                }

                var proposedIsExtranet = (bool)exceptionEntry.Property("IsExtranet").CurrentValue;
                if (currentVpn.IsExtranet != proposedIsExtranet)
                {
                    ModelState.AddModelError("IsExtranet", $"Current value: {currentVpn.IsExtranet}");
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

            var item = await VpnService.GetByIDAsync(updateApiModel.VpnID);

            return Ok(new
            {
                Item = Mapper.Map<VpnApiModel>(item)
            });
        }

        // DELETE api/vpns/5
        [HttpDelete("vpns/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await VpnService.UnitOfWork.VpnRepository.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }            

            try
            {

                if (!item.Created)
                {
                    // The Vpn is operational in the network so 
                    // delete the resource from the network first

                    await VpnService.DeleteFromNetworkAsync(item);

                }

                await VpnService.DeleteAsync(item);
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

        // POST api/vpns/1/checksync
        [HttpPost("vpns/{id}/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<ActionResult> CheckSync(int id)
        {
            var item = await VpnService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Validate the VPN

            VpnApiValidator.ValidateOkToSyncToNetwork(item);
            if (!VpnApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                var result = await VpnService.CheckNetworkSyncAsync(item);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = $"Vpn {item.Name} has been checked and is synchronised with the network."
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = $"Vpn {item.Name} has been checked and is NOT synchronised with the network."
                        + "Press the 'Sync' button to update the network."
                    });
                }
            }

            catch (Exception  /** ex **/ )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/vpns/checksync
        [HttpPost("vpns/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> CheckSyncAll()
        {
            var vpns = await VpnService.GetAllAsync();
            if (vpns.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No vpns were found."
                });
            }

            foreach (var vpn in vpns)
            {
                // Validate the VPN

                VpnApiValidator.ValidateOkToSyncToNetwork(vpn);
            }

            if (!VpnApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                var progress = new Progress<ServiceResult>(UpdateVpnClientProgress);
                var results = await VpnService.CheckNetworkSyncAsync(vpns, progress);

                // Check success for all VPNs 

                if (results.Where(q => q.IsSuccess).Count() == results.Count())
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "All vpns have been checked and are synchronised with the network."
                    });
                }
                else
                {
                    // One or more VPNs are not synchronised with the network

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

        // POST attachmentsets/1/vpns/checksync
        [HttpPost("attachmentsets/{id}/vpns/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> CheckSyncAllByAttachmentSetID(int id)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(id);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            var vpns = await VpnService.GetAllByAttachmentSetIDAsync(id);
            if (vpns.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Vpns were found."
                });
            }

            foreach (var vpn in vpns)
            {
                // Validate the VPN

                VpnApiValidator.ValidateOkToSyncToNetwork(vpn);
            }

            if (!VpnApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            try
            {
                var progress = new Progress<ServiceResult>(UpdateClientProgress);
                var results = await VpnService.CheckNetworkSyncAsync(vpns, attachmentSet, progress);
                if (results.Where(q => q.IsSuccess).Count() == results.Count())
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "All Vpns have been checked and are synchronised with the network."
                    });
                }
                else
                {
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

        // POST api/vpns/1/sync
        [HttpPost("vpns/{id}/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> Sync(int id)
        {
            var item = await VpnService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                // Validate the VPN

                VpnApiValidator.ValidateOkToSyncToNetwork(item);
                if (!VpnApiValidator.ValidationDictionary.IsValid)
                {
                    return new ValidationFailedResult(ModelState);
                }

                await VpnService.SyncToNetworkAsync(item);
                return Ok(new
                {
                    Success = true,
                    Message = $"Vpn {item.Name} is synchronised with the network."
                });
            
            }

            catch (Exception  /** ex **/   )
            {
                // Uncomment and log exception here

                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // POST api/attachmentsets/1/vpns/sync
        [HttpPost("attachmentsets/{id}/vpns/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> SyncAllByAttachmentSetID(int id)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(id);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            try
            {
                var vpns = await VpnService.GetAllByAttachmentSetIDAsync(id);
                if (vpns.Count() == 0)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "No Vpns were found."
                    });
                }

                foreach (var vpn in vpns)
                {
                    // Validate the VPN

                    VpnApiValidator.ValidateOkToSyncToNetwork(vpn);
                }

                if (!VpnApiValidator.ValidationDictionary.IsValid)
                {
                    return new ValidationFailedResult(ModelState);
                }

                var progress = new Progress<ServiceResult>(UpdateClientProgress);
                await VpnService.SyncToNetworkAsync(vpns, attachmentSet, progress);

                return Ok(new
                {
                    Success = true,
                    Message = "All Vpns are synchronised with the network."
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
        /// individual vpn has completed in the context of an attachment set.
        /// </summary>
        /// <param name="result"></param>
        private void UpdateClientProgress(ServiceResult result)
        {
            var vpn = (Vpn)result.Item;
            var attachmentSet = (AttachmentSet)result.Context;

            // Update all clients which are subscribed to attachment set group 
            // of the attachment set context supplied in the result object

            HubContext.Clients.Group($"AttachmentSet_{attachmentSet.AttachmentSetID}")
                .InvokeAsync("onSingleComplete",Mapper.Map<VpnApiModel>(vpn), result.IsSuccess);
        }

        /// <summary>
        /// Delegate method which is called when sync or checksync of an 
        /// individual vpn has completed.
        /// </summary>
        /// <param name="result"></param>
        private void UpdateVpnClientProgress(ServiceResult result)
        {
            var vpn = (Vpn)result.Item;
            
            // Update all clients which have joined the Vpns group

            HubContext.Clients.Group("Vpns")
                .InvokeAsync("onSingleComplete",Mapper.Map<VpnApiModel>(vpn), result.IsSuccess);
        }
    }
}
