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
    /// Defines a RESTful Web API for management of Attachment Set VRF resources
    /// </summary>
    public class PortApiController : BaseApiController
    {
        public PortApiController(IPortService portService,
                                    IPortApiValidator portApiValidator,
                                    IMapper mapper) : base(mapper)
        {
            PortService = portService;
            PortApiValidator = portApiValidator;
            this.Validator = portApiValidator;
        }

        private IPortService PortService { get; set; }
        private IPortApiValidator PortApiValidator { get; set; }

        // GET api/ports/1
        [HttpGet("ports/{id}", Name = "GetPort")]
        public async Task<PortApiModel> GetPort(int id)
        {
            var port = await PortService.GetByIDAsync(id);
            return Mapper.Map<PortApiModel>(port);
        }

        // GET api/devices/1/ports
        [HttpGet("devices/{id}/ports", Name = "GetPorts")]
        public async Task<IEnumerable<PortApiModel>> GetPorts(int id)
        {
            var ports = await PortService.GetAllByDeviceIDAsync(id);
            return Mapper.Map<List<PortApiModel>>(ports);
        }

        // POST api/devices/1/ports/values
        [HttpPost("devices/{id}/ports")]
        public async Task<IActionResult> CreatePort(int id, [FromBody]PortRequestApiModel apiRequest)
        {
            // Create the Port

            apiRequest.ID = id;
            await PortApiValidator.ValidateNewAsync(apiRequest);
            if (!PortApiValidator.ValidationDictionary.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var port = Mapper.Map<Port>(apiRequest);

            try
            {
                var result = await PortService.AddAsync(port);
                var item = await PortService.GetByIDAsync(port.ID);

                return CreatedAtRoute("GetPort", new
                {
                    id = port.ID
                },
                Mapper.Map<PortApiModel>(item));
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

        // DELETE api/ports/1
        [HttpDelete("ports/{id}")]
        public async Task<IActionResult> DeletePort(int id)
        {
            // Get the Port to delete

            var port = await PortService.GetByIDAsync(id);
            if (port == null)
            {
                return NotFound();
            }

            try
            {
                var result = await PortService.DeleteAsync(port);
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
