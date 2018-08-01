using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Hubs;
using SCM.Api.Validators;
using SCM.Api.Models;
using SCM.Models;
using SCM.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SCM.Api.Controllers
{
    /// <summary>
    /// Defines a RESTful web API for management of Device resources.
    /// </summary>
    public class DeviceApiController : BaseApiController
    {
        public DeviceApiController(IDeviceService deviceService, 
            IDeviceApiValidator deviceApiValidator,
            IMapper mapper, IHubContext<NetworkSyncHub> context) : base(mapper)
        {
            DeviceService = deviceService;
            DeviceApiValidator = deviceApiValidator;
            this.Validator = deviceApiValidator;
            HubContext = context;
        }

        private IDeviceService DeviceService { get; set; }
        private IDeviceApiValidator DeviceApiValidator { get; set; }
        private IHubContext<NetworkSyncHub> HubContext { get; set; }

        // GET: api/devices
        [HttpGet("devices")]
        public async Task<IEnumerable<DeviceApiModel>> GetAll(int id)
        {
            var devices = await DeviceService.GetAllAsync();
            return Mapper.Map<List<DeviceApiModel>>(devices);
        }

        // GET api/devices/1
        [HttpGet("devices/{id:int}", Name = "GetDevice")]
        public async Task<DeviceApiModel> Get(int id)
        {
            var device = await DeviceService.GetByIDAsync(id);
            return Mapper.Map<DeviceApiModel>(device);
        }

        // GET api/devices/name
        [HttpGet("devices/{name}")]
        public async Task<DeviceApiModel> GetByName(string name)
        {
            var device = await DeviceService.GetByNameAsync(name);
            return Mapper.Map<DeviceApiModel>(device);
        }

        // POST api/devices
        [HttpPost("devices")]
        public async Task<IActionResult> Create([FromBody]DeviceRequestApiModel apiRequest)
        {
            await DeviceApiValidator.ValidateNewAsync(apiRequest);
            if (!DeviceApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var device = Mapper.Map<Device>(apiRequest);

            try
            {
                var result = await DeviceService.AddAsync(device);

                // Get fully populated Device from the DB

                var item = await DeviceService.GetByIDAsync(device.DeviceID);
                return CreatedAtRoute("GetDevice", new { id = device.DeviceID }, Mapper.Map<DeviceApiModel>(item));
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

        // DELETE api/devices/5
        [HttpDelete("devices/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await DeviceService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                DeviceApiValidator.ValidateDelete(item);
                if (DeviceApiValidator.ValidationDictionary.IsValid)
                {
                    if (!item.Created)
                    {
                        // The Device is operational in the network so 
                        // delete the resource from the network first

                        await DeviceService.DeleteFromNetworkAsync(item);
                    }

                    await DeviceService.DeleteAsync(item);
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

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        // PUT api/devices/5
        [HttpPut("devices/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]DeviceUpdateApiModel updateApiModel)
        {
            if (id != updateApiModel.ID)
            {
                return NotFound();
            }

            // Retrieve Device directly from repository (not from the service layer) so that
            // we don't have any child entities attached that would otherwise need to be updated

            var device = await DeviceService.UnitOfWork.DeviceRepository.GetByIDAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            // Update Device with values from the update model, then validate

            Mapper.Map(updateApiModel, device);

            try
            {
                await DeviceService.UpdateAsync(device);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                // Retrieve current Device to compare values

                var currentDevice = await DeviceService.GetByIDAsync(updateApiModel.ID);

                ModelState.AddModelError("RowVersion", $"Current value: {Convert.ToBase64String(currentDevice.RowVersion)}");

                var proposedDescription = (string)exceptionEntry.Property("Description").CurrentValue;
                if (currentDevice.Description != proposedDescription)
                {
                    ModelState.AddModelError("Description", $"Current value: {currentDevice.Description}");
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

            var item = await DeviceService.GetByIDAsync(updateApiModel.ID);

            return Ok(new
            {
                Success = true,
                Item = Mapper.Map<DeviceApiModel>(item)
            });
        }


        [HttpPost("devices/{id}/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<ActionResult> CheckSync(int id)
        {
            var item = await DeviceService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                var result = await DeviceService.CheckNetworkSyncAsync(item);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = $"Device {item.Name} has been checked and is synchronised with the network."
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = $"Device {item.Name} has been checked and is NOT synchronised with the network."
                        + "Press the 'Sync' button to update the network."
                    });
                }
            }

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        [HttpPost("devices/checksync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> CheckSyncAll()
        {
            var devices = await DeviceService.GetAllAsync();
            if (devices.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Devices were found."
                });
            }

            try
            {
                var progress = new Progress<ServiceResult>(UpdateClientProgress);
                var results = await DeviceService.CheckNetworkSyncAsync(devices, progress);
                if (results.Where(q => q.IsSuccess).Count() == results.Count())
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "All Devices have been checked and are synchronised with the network."
                    });
                }
                else
                {
                    // Some Devices require sync to network - indicate with Success = False

                    return Ok(new
                    {
                        Success = false,
                        Message = results.SelectMany(q => q.GetMessageList())
                    });
                }
            }

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        [HttpPost("devices/{id}/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> Sync(int id)
        {
            var item = await DeviceService.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                await DeviceService.SyncToNetworkAsync(item);
  
                return Ok(new
                {
                    Success = true,
                    Message = $"Device {item.Name} is synchronised with the network."
                });
            }

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        [HttpPost("devices/sync")]
        [ServiceFilter(typeof(ValidateNetworkServiceRequestAttribute))]
        public async Task<IActionResult> SyncAll()
        {
            var devices = await DeviceService.GetAllAsync();
            if (devices.Count() == 0)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "No Devices were found."
                });
            }

            try
            {
                var progress = new Progress<ServiceResult>(UpdateClientProgress);
                var results = await DeviceService.SyncToNetworkAsync(devices, progress);

                return Ok(new
                {
                    Success = true,
                    Message = "All devices are synchronised with the network."
                });
            }

            catch (Exception /** ex **/)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new NetworkServiceFailureApiModel());
            }
        }

        /// <summary>
        /// Delegate method which is called when sync or checksync of an 
        /// individual device has completed.
        /// </summary>
        /// <param name="result"></param>
        private void UpdateClientProgress(ServiceResult result)
        {
            var device = (Device)result.Item;

            // Update all clients which are subscribed to the attachment context
            // supplied in the result object
         
            HubContext.Clients.Group("Devices")
                .SendAsync("onSingleComplete", Mapper.Map<DeviceApiModel>(device), result.IsSuccess);
        }
    }
}