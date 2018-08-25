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
using Swashbuckle.AspNetCore.Annotations;

namespace Mind.Api.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    public class TenantDeviceApiController : Controller
    { 
        /// <summary>
        /// Create a new tenant device
        /// </summary>
        
        /// <param name="tenantId">ID of the tenant</param>
        /// <param name="body">device request object that generates a new device</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v1/tenant/{tenantId}/device")]
        [ValidateModelState]
        [SwaggerOperation("AddTenantDevice")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDevice), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult AddTenantDevice([FromRoute][Required]int? tenantId, [FromBody]TenantDeviceRequest body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(TenantDevice));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Device>\n  <deviceID>123</deviceID>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <useLayer2InterfaceMtu>true</useLayer2InterfaceMtu>\n  <deviceModel>aeiou</deviceModel>\n  <deviceStatus>aeiou</deviceStatus>\n</Device>";
            exampleJson = "{\n  \"useLayer2InterfaceMtu\" : true,\n  \"name\" : \"name\",\n  \"description\" : \"description\",\n  \"deviceModel\" : \"deviceModel\",\n  \"ports\" : [ {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  }, {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  } ],\n  \"deviceID\" : 1,\n  \"deviceStatus\" : \"deviceStatus\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<TenantDevice>(exampleJson)
            : default(TenantDevice);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Create a new port
        /// </summary>
        
        /// <param name="deviceId">ID of the device</param>
        /// <param name="body">Created port object</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v1/tenant/device/{deviceId}/port")]
        [ValidateModelState]
        [SwaggerOperation("CreateTenantPort")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Port>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult CreateTenantPort([FromRoute][Required]int? deviceId, [FromBody]PortRequest body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Port>));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <portId>123</portId>\n  <type>aeiou</type>\n  <name>aeiou</name>\n  <portSfp>aeiou</portSfp>\n  <portStatus>aeiou</portStatus>\n  <portRole>aeiou</portRole>\n  <portPool>aeiou</portPool>\n</null>";
            exampleJson = "[ {\n  \"portPool\" : \"portPool\",\n  \"name\" : \"name\",\n  \"portStatus\" : \"portStatus\",\n  \"portId\" : 1,\n  \"type\" : \"type\",\n  \"portSfp\" : \"portSfp\",\n  \"portRole\" : \"portRole\"\n}, {\n  \"portPool\" : \"portPool\",\n  \"name\" : \"name\",\n  \"portStatus\" : \"portStatus\",\n  \"portId\" : 1,\n  \"type\" : \"type\",\n  \"portSfp\" : \"portSfp\",\n  \"portRole\" : \"portRole\"\n} ]";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Port>>(exampleJson)
            : default(List<Port>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Deletes a device
        /// </summary>
        
        /// <param name="deviceId">ID of the device</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v1/tenant/device/{deviceId}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteTenantDevice")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult DeleteTenantDevice([FromRoute][Required]int? deviceId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <code>123</code>\n  <type>aeiou</type>\n  <message>aeiou</message>\n</null>";
            exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Deletes a port
        /// </summary>
        
        /// <param name="portId">ID of the port</param>
        /// <param name="apiKey"></param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v1/tenant/device/port/{portId}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteTenantPort")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult DeleteTenantPort([FromRoute][Required]int? portId, [FromHeader]string apiKey)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <code>123</code>\n  <type>aeiou</type>\n  <message>aeiou</message>\n</null>";
            exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find all devices for a given tenant
        /// </summary>
        /// <remarks>Returns all infrastructure devices</remarks>
        /// <param name="tenantId">ID of the tenant</param>
        /// <response code="200">successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/tenant/{tenantId}/device")]
        [ValidateModelState]
        [SwaggerOperation("GetAllTenantDevices")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<TenantDevice>), description: "successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetAllTenantDevices([FromRoute][Required]int? tenantId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<TenantDevice>));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Device>\n  <deviceID>123</deviceID>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <useLayer2InterfaceMtu>true</useLayer2InterfaceMtu>\n  <deviceModel>aeiou</deviceModel>\n  <deviceStatus>aeiou</deviceStatus>\n</Device>";
            exampleJson = "[ {\n  \"useLayer2InterfaceMtu\" : true,\n  \"name\" : \"name\",\n  \"description\" : \"description\",\n  \"deviceModel\" : \"deviceModel\",\n  \"ports\" : [ {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  }, {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  } ],\n  \"deviceID\" : 1,\n  \"deviceStatus\" : \"deviceStatus\"\n}, {\n  \"useLayer2InterfaceMtu\" : true,\n  \"name\" : \"name\",\n  \"description\" : \"description\",\n  \"deviceModel\" : \"deviceModel\",\n  \"ports\" : [ {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  }, {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  } ],\n  \"deviceID\" : 1,\n  \"deviceStatus\" : \"deviceStatus\"\n} ]";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<TenantDevice>>(exampleJson)
            : default(List<TenantDevice>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find all ports for a given tenant device
        /// </summary>
        /// <remarks>Returns all ports for a given tenant device</remarks>
        /// <param name="deviceId">ID of the device</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/tenant/device/{deviceId}/port")]
        [ValidateModelState]
        [SwaggerOperation("GetPortsByTenantDevice")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Port>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetPortsByTenantDevice([FromRoute][Required]int? deviceId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Port>));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <portId>123</portId>\n  <type>aeiou</type>\n  <name>aeiou</name>\n  <portSfp>aeiou</portSfp>\n  <portStatus>aeiou</portStatus>\n  <portRole>aeiou</portRole>\n  <portPool>aeiou</portPool>\n</null>";
            exampleJson = "[ {\n  \"portPool\" : \"portPool\",\n  \"name\" : \"name\",\n  \"portStatus\" : \"portStatus\",\n  \"portId\" : 1,\n  \"type\" : \"type\",\n  \"portSfp\" : \"portSfp\",\n  \"portRole\" : \"portRole\"\n}, {\n  \"portPool\" : \"portPool\",\n  \"name\" : \"name\",\n  \"portStatus\" : \"portStatus\",\n  \"portId\" : 1,\n  \"type\" : \"type\",\n  \"portSfp\" : \"portSfp\",\n  \"portRole\" : \"portRole\"\n} ]";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Port>>(exampleJson)
            : default(List<Port>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find device by ID
        /// </summary>
        /// <remarks>Returns a single device</remarks>
        /// <param name="deviceId">ID of the device</param>
        /// <response code="200">successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/tenant/device/{deviceId}")]
        [ValidateModelState]
        [SwaggerOperation("GetTenantDeviceById")]
        [SwaggerResponse(statusCode: 200, type: typeof(TenantDevice), description: "successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetTenantDeviceById([FromRoute][Required]int? deviceId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(TenantDevice));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Device>\n  <deviceID>123</deviceID>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <useLayer2InterfaceMtu>true</useLayer2InterfaceMtu>\n  <deviceModel>aeiou</deviceModel>\n  <deviceStatus>aeiou</deviceStatus>\n</Device>";
            exampleJson = "{\n  \"useLayer2InterfaceMtu\" : true,\n  \"name\" : \"name\",\n  \"description\" : \"description\",\n  \"deviceModel\" : \"deviceModel\",\n  \"ports\" : [ {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  }, {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  } ],\n  \"deviceID\" : 1,\n  \"deviceStatus\" : \"deviceStatus\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<TenantDevice>(exampleJson)
            : default(TenantDevice);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find port by ID
        /// </summary>
        /// <remarks>Returns a single port</remarks>
        /// <param name="portId">ID of the port</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/tenant/device/port/{portId}")]
        [ValidateModelState]
        [SwaggerOperation("GetTenantPortById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Port), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetTenantPortById([FromRoute][Required]int? portId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Port));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<null>\n  <portId>123</portId>\n  <type>aeiou</type>\n  <name>aeiou</name>\n  <portSfp>aeiou</portSfp>\n  <portStatus>aeiou</portStatus>\n  <portRole>aeiou</portRole>\n  <portPool>aeiou</portPool>\n</null>";
            exampleJson = "{\n  \"portPool\" : \"portPool\",\n  \"name\" : \"name\",\n  \"portStatus\" : \"portStatus\",\n  \"portId\" : 1,\n  \"type\" : \"type\",\n  \"portSfp\" : \"portSfp\",\n  \"portRole\" : \"portRole\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Port>(exampleJson)
            : default(Port);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Update an existing device
        /// </summary>
        
        /// <param name="deviceId">ID of the device</param>
        /// <param name="body">device update object that updates an existing device</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPut]
        [Route("/v1/tenant/device/{deviceId}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateTenantDevice")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<TenantDevice>), description: "successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult UpdateTenantDevice([FromRoute][Required]int? deviceId, [FromBody]TenantDeviceUpdate body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<TenantDevice>));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Device>\n  <deviceID>123</deviceID>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <useLayer2InterfaceMtu>true</useLayer2InterfaceMtu>\n  <deviceModel>aeiou</deviceModel>\n  <deviceStatus>aeiou</deviceStatus>\n</Device>";
            exampleJson = "[ {\n  \"useLayer2InterfaceMtu\" : true,\n  \"name\" : \"name\",\n  \"description\" : \"description\",\n  \"deviceModel\" : \"deviceModel\",\n  \"ports\" : [ {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  }, {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  } ],\n  \"deviceID\" : 1,\n  \"deviceStatus\" : \"deviceStatus\"\n}, {\n  \"useLayer2InterfaceMtu\" : true,\n  \"name\" : \"name\",\n  \"description\" : \"description\",\n  \"deviceModel\" : \"deviceModel\",\n  \"ports\" : [ {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  }, {\n    \"portPool\" : \"portPool\",\n    \"name\" : \"name\",\n    \"portStatus\" : \"portStatus\",\n    \"portId\" : 1,\n    \"type\" : \"type\",\n    \"portSfp\" : \"portSfp\",\n    \"portRole\" : \"portRole\"\n  } ],\n  \"deviceID\" : 1,\n  \"deviceStatus\" : \"deviceStatus\"\n} ]";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<TenantDevice>>(exampleJson)
            : default(List<TenantDevice>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
