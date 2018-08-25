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
    public class VpnApiController : Controller
    { 
        /// <summary>
        /// Create a new virtual private network
        /// </summary>
        
        /// <param name="body">vpn request object that generates a new vpn</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation error</response>
        [HttpPost]
        [Route("/v1/vpn")]
        [ValidateModelState]
        [SwaggerOperation("AddVpn")]
        [SwaggerResponse(statusCode: 200, type: typeof(Vpn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        public virtual IActionResult AddVpn([FromBody]VpnRequest body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Vpn));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Vpn>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <region>aeiou</region>\n  <plane>aeiou</plane>\n  <tenancyType>aeiou</tenancyType>\n  <topologyType>aeiou</topologyType>\n  <addressFamily>aeiou</addressFamily>\n  <isNovaVpn>true</isNovaVpn>\n</Vpn>";
            exampleJson = "{\n  \"plane\" : \"plane\",\n  \"topologyType\" : \"topologyType\",\n  \"name\" : \"name\",\n  \"tenancyType\" : \"tenancyType\",\n  \"description\" : \"description\",\n  \"isNovaVpn\" : true,\n  \"region\" : \"region\",\n  \"addressFamily\" : \"addressFamily\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Vpn>(exampleJson)
            : default(Vpn);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Add an attachment set to a virtual private network
        /// </summary>
        
        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v1/vpn/{vpnId}/attachment-set/{attachmentSetId}")]
        [ValidateModelState]
        [SwaggerOperation("AddVpnAttachmentSet")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult AddVpnAttachmentSet([FromRoute][Required]int? vpnId, [FromRoute][Required]int? attachmentSetId)
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
        /// Deletes a vpn
        /// </summary>
        
        /// <param name="vpnId">ID of the vpn</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v1/vpn/{vpnId}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteVpn")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult DeleteVpn([FromRoute][Required]int? vpnId)
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
        /// Return all vpns
        /// </summary>
        /// <remarks>Returns all vpns</remarks>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/vpn")]
        [ValidateModelState]
        [SwaggerOperation("GetAllVpns")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Vpn>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetAllVpns()
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Vpn>));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Vpn>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <region>aeiou</region>\n  <plane>aeiou</plane>\n  <tenancyType>aeiou</tenancyType>\n  <topologyType>aeiou</topologyType>\n  <addressFamily>aeiou</addressFamily>\n  <isNovaVpn>true</isNovaVpn>\n</Vpn>";
            exampleJson = "[ {\n  \"plane\" : \"plane\",\n  \"topologyType\" : \"topologyType\",\n  \"name\" : \"name\",\n  \"tenancyType\" : \"tenancyType\",\n  \"description\" : \"description\",\n  \"isNovaVpn\" : true,\n  \"region\" : \"region\",\n  \"addressFamily\" : \"addressFamily\"\n}, {\n  \"plane\" : \"plane\",\n  \"topologyType\" : \"topologyType\",\n  \"name\" : \"name\",\n  \"tenancyType\" : \"tenancyType\",\n  \"description\" : \"description\",\n  \"isNovaVpn\" : true,\n  \"region\" : \"region\",\n  \"addressFamily\" : \"addressFamily\"\n} ]";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Vpn>>(exampleJson)
            : default(List<Vpn>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find vpn by ID
        /// </summary>
        /// <remarks>Returns a single vpn</remarks>
        /// <param name="vpnId">ID of the vpn</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/vpn/{vpnId}")]
        [ValidateModelState]
        [SwaggerOperation("GetVpnById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Vpn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetVpnById([FromRoute][Required]int? vpnId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Vpn));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Vpn>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <region>aeiou</region>\n  <plane>aeiou</plane>\n  <tenancyType>aeiou</tenancyType>\n  <topologyType>aeiou</topologyType>\n  <addressFamily>aeiou</addressFamily>\n  <isNovaVpn>true</isNovaVpn>\n</Vpn>";
            exampleJson = "{\n  \"plane\" : \"plane\",\n  \"topologyType\" : \"topologyType\",\n  \"name\" : \"name\",\n  \"tenancyType\" : \"tenancyType\",\n  \"description\" : \"description\",\n  \"isNovaVpn\" : true,\n  \"region\" : \"region\",\n  \"addressFamily\" : \"addressFamily\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Vpn>(exampleJson)
            : default(Vpn);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Remove an attachment set from a vpn
        /// </summary>
        
        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="attachmentSetId">ID of the attachment set</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v1/vpn/{vpnId}/attachment-set/{attachmentSetId}")]
        [ValidateModelState]
        [SwaggerOperation("RemoveAttachmentSet")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult RemoveAttachmentSet([FromRoute][Required]int? vpnId, [FromRoute][Required]int? attachmentSetId)
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
        /// Update an existing vpn
        /// </summary>
        
        /// <param name="vpnId">ID of the vpn</param>
        /// <param name="body">vpn update object that updates an existing vpn</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPut]
        [Route("/v1/vpn/{vpnId}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateVpn")]
        [SwaggerResponse(statusCode: 200, type: typeof(Vpn), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult UpdateVpn([FromRoute][Required]int? vpnId, [FromBody]VpnUpdate body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Vpn));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Vpn>\n  <name>aeiou</name>\n  <description>aeiou</description>\n  <region>aeiou</region>\n  <plane>aeiou</plane>\n  <tenancyType>aeiou</tenancyType>\n  <topologyType>aeiou</topologyType>\n  <addressFamily>aeiou</addressFamily>\n  <isNovaVpn>true</isNovaVpn>\n</Vpn>";
            exampleJson = "{\n  \"plane\" : \"plane\",\n  \"topologyType\" : \"topologyType\",\n  \"name\" : \"name\",\n  \"tenancyType\" : \"tenancyType\",\n  \"description\" : \"description\",\n  \"isNovaVpn\" : true,\n  \"region\" : \"region\",\n  \"addressFamily\" : \"addressFamily\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Vpn>(exampleJson)
            : default(Vpn);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
