/*
 * MIND API
 *
 * This is the Master Inventory Network Database (MIND) API. MIND provides automated allocation of technical attributes needed to create IP and Ethernet VPNs on the global Sigma network. MIND supports the 'Nova' services specfication which defines the collection of connectivity services supported by ENT. Go to https://thehub.thomsonreuters.com/docs/DOC-2193014 to learn more.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: jonathan.beasley@thomsonreuters.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Mind.Api.Attributes;
using Mind.Api.Models;
using Mind.Services;
using AutoMapper;
using SCM.Data;
using Microsoft.EntityFrameworkCore;
using Mind.Builders;
using Mind.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// Create and manage the lifecycle of infrastructure devices
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Infrastructure Devices")]
    public class InfrastructureDeviceApiController : BaseApiController
    { 
        private readonly IInfrastructureDeviceService _infrastructureDeviceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infrastructureDeviceService"></param>
        /// <param name="mapper"></param>
        public InfrastructureDeviceApiController(IInfrastructureDeviceService infrastructureDeviceService, IMapper mapper) : base(infrastructureDeviceService, mapper)
        {
            _infrastructureDeviceService = infrastructureDeviceService;
        }

        /// <summary>
        /// Create a new infrastructure device
        /// </summary>

        /// <param name="body">infrastructure device request object that generates a new infrastructure device</param>
        /// <response code="201">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/infrastructure-devices")]
        [ValidateModelState]
        [SwaggerOperation("CreateInfrastructureDevice")]
        [SwaggerResponse(statusCode: 201, type: typeof(InfrastructureDevice), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> CreateInfrastructureDevice([FromBody]Mind.Api.Models.InfrastructureDeviceRequest body)
        {
            try
            {
                var request = Mapper.Map<Mind.Models.RequestModels.InfrastructureDeviceRequest>(body);
                var device = await _infrastructureDeviceService.AddAsync(request);
                var deviceApiModel = Mapper.Map<Mind.Api.Models.InfrastructureDevice>(device);
                return CreatedAtRoute("GetInfrastructureDevice", new { deviceId = device.DeviceID }, deviceApiModel);
            }

            catch (BuilderBadArgumentsException ex) 
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (IllegalStateException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Bulk create new infrastructure devices
        /// </summary>

        /// <param name="body">A collection of infrastructure device request objects that generate a collection of new infrastructure devices</param>
        /// <response code="200">Successful operation</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPost]
        [Route("/v{version:apiVersion}/infrastructure-devices/bulk")]
        [ValidateModelState]
        [SwaggerOperation("BulkCreateInfrastructureDevice")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<InfrastructureDevice>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> BulkCreateInfrastructureDevices([FromBody]List<Mind.Api.Models.InfrastructureDeviceRequest> body)
        {
            try
            {
                var requests = Mapper.Map<List<Mind.Models.RequestModels.InfrastructureDeviceRequest>>(body);
                var devices = await _infrastructureDeviceService.AddAsync(requests);
                var deviceApiModels = Mapper.Map<List<Mind.Api.Models.InfrastructureDevice>>(devices);
                return Ok(deviceApiModels);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (IllegalStateException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Deletes an infrastructure device
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="422">Validation failed</response>
        /// <response code="500">Error while updating the database</response>
        [HttpDelete]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("DeleteInfrastructureDevice")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation failed")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> DeleteInfrastructureDevice([FromRoute][Required]int? deviceId)
        {
            try
            {
                await _infrastructureDeviceService.DeleteAsync(deviceId.Value);
                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (IllegalDeleteAttemptException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }

        /// <summary>
        /// Find an infrastructure device by ID
        /// </summary>
        /// <remarks>Returns a single infrastructure device</remarks>
        /// <param name="deviceId">ID of the device</param>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="304">The specified resource has not been modified</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}", Name = "GetInfrastructureDevice")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("GetInfrastructureDeviceById")]
        [SwaggerResponse(statusCode: 200, type: typeof(InfrastructureDevice), description: "Successful operation")]
        [SwaggerResponse(statusCode: 304, description: "The specified resource has not been modified")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual async Task<IActionResult> GetInfrastructureDeviceById([FromRoute][Required]int? deviceId, [FromQuery]bool? deep)
        {
            var device = await _infrastructureDeviceService.GetByIDAsync(deviceId.Value, deep: deep);
            if (device.HasBeenModified(Request))
            {
                device.SetModifiedHttpHeaders(Response);
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            return Ok(Mapper.Map<InfrastructureDevice>(device));
        }

        /// <summary>
        /// Find all infrastructure devices
        /// </summary>
        /// <remarks>Returns all infrastructure devices</remarks>
        /// <param name="deep">Perform a deep query on the resource</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v{version:apiVersion}/infrastructure-devices")]
        [ValidateModelState]
        [SwaggerOperation("GetInfrastructureDevices")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<InfrastructureDevice>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public async virtual Task<IActionResult> GetInfrastructureDevices([FromQuery]bool? deep)
        {
            var devices = await _infrastructureDeviceService.GetAllAsync(deep: deep);
            return Ok(Mapper.Map<List<InfrastructureDevice>>(devices));
        }

        /// <summary>
        /// Update an existing infrastructure device
        /// </summary>

        /// <param name="deviceId">ID of the device</param>
        /// <param name="body">Infrastructure device update object that updates an existing device</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        /// <response code="412">Precondition failed</response>
        /// <response code="422">Validation error</response>
        /// <response code="500">Error while updating the database</response>
        [HttpPatch]
        [Route("/v{version:apiVersion}/infrastructure-devices/{deviceId}")]
        [ValidateModelState]
        [ValidateInfrastructureDeviceExists]
        [SwaggerOperation("UpdateInfrastructureDevice")]
        [SwaggerResponse(statusCode: 204, description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        [SwaggerResponse(statusCode: 412, type: typeof(ApiResponse), description: "Precondition failed")]
        [SwaggerResponse(statusCode: 422, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 500, type: typeof(ApiResponse), description: "Error while updating the database")]
        public virtual async Task<IActionResult> UpdateInfrastructureDevice([FromRoute][Required]int? deviceId, [FromBody]Mind.Api.Models.InfrastructureDeviceUpdate body)
        {
            try
            {
                var item = await _infrastructureDeviceService.GetByIDAsync(deviceId.Value);
                if (item.HasPreconditionFailed(Request))
                {
                    return new PreconditionFailedResult();
                }

                var update = Mapper.Map<Mind.Models.RequestModels.InfrastructureDeviceUpdate>(body);
                var device = await _infrastructureDeviceService.UpdateAsync(deviceId.Value, update);
                device.SetModifiedHttpHeaders(Response);

                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (BuilderBadArgumentsException ex)
            {
                return new BadArgumentsResult(ex.Message);
            }

            catch (BuilderUnableToCompleteException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (IllegalStateException ex)
            {
                return new ValidationFailedResult(ex.Message);
            }

            catch (DbUpdateException)
            {
                return new DatabaseUpdateFailedResult();
            }
        }
    }
}
