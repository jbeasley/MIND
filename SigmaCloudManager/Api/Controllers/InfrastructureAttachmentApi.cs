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
    public class InfrastructureAttachmentApiController : Controller
    { 
        /// <summary>
        /// Create a new network attachment
        /// </summary>
        
        /// <param name="deviceId">ID of the infrastructure device</param>
        /// <param name="body">attachment request object that generates a new attachment</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPost]
        [Route("/v1/infrastructure/{deviceId}/attachment")]
        [ValidateModelState]
        [SwaggerOperation("AddInfrastructureAttachment")]
        [SwaggerResponse(statusCode: 200, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult AddInfrastructureAttachment([FromRoute][Required]int? deviceId, [FromBody]InfrastructureAttachmentRequest body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Attachment));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Attachment>\n  <attachmentId>123</attachmentId>\n  <trustReceivedCosDscp>true</trustReceivedCosDscp>\n  <isLayer3>true</isLayer3>\n  <isBundle>true</isBundle>\n  <isMultiport>true</isMultiport>\n  <isTagged>true</isTagged>\n  <attachmentBandwidthGbps>123</attachmentBandwidthGbps>\n</Attachment>";
            exampleJson = "{\n  \"isBundle\" : false,\n  \"isTagged\" : false,\n  \"contractBandwidthPool\" : {\n    \"name\" : \"name\",\n    \"contractBandwidthMbps\" : 5\n  },\n  \"isMultiport\" : false,\n  \"trustReceivedCosDscp\" : true,\n  \"attachmentId\" : 0,\n  \"isLayer3\" : false,\n  \"attachmentBandwidthGbps\" : 5,\n  \"infrastructureDevice\" : {\n    \"useLayer2InterfaceMtu\" : true,\n    \"planeName\" : \"planeName\",\n    \"locationName\" : \"locationName\",\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"deviceModel\" : \"deviceModel\",\n    \"ports\" : [ {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    }, {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    } ],\n    \"deviceID\" : 6,\n    \"deviceStatus\" : \"deviceStatus\"\n  },\n  \"routingInstance\" : {\n    \"routingInstanceId\" : 0,\n    \"name\" : \"name\"\n  },\n  \"tenant\" : {\n    \"tenantId\" : 0,\n    \"name\" : \"name\"\n  }\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Attachment>(exampleJson)
            : default(Attachment);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Deletes an attachment
        /// </summary>
        
        /// <param name="attachmentId">ID of the attachment</param>
        /// <response code="204">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpDelete]
        [Route("/v1/infrastructure/attachment/{attachmentId}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteInfrastructureAttachment")]
        [SwaggerResponse(statusCode: 204, type: typeof(ApiResponse), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult DeleteInfrastructureAttachment([FromRoute][Required]int? attachmentId)
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
        /// Find attachment by ID
        /// </summary>
        /// <remarks>Returns a single attachment</remarks>
        /// <param name="attachmentId">ID of the attachment</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/infrastructure/attachment/{attachmentId}")]
        [ValidateModelState]
        [SwaggerOperation("GetInfrastructureAttachmentById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetInfrastructureAttachmentById([FromRoute][Required]int? attachmentId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Attachment));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Attachment>\n  <attachmentId>123</attachmentId>\n  <trustReceivedCosDscp>true</trustReceivedCosDscp>\n  <isLayer3>true</isLayer3>\n  <isBundle>true</isBundle>\n  <isMultiport>true</isMultiport>\n  <isTagged>true</isTagged>\n  <attachmentBandwidthGbps>123</attachmentBandwidthGbps>\n</Attachment>";
            exampleJson = "{\n  \"isBundle\" : false,\n  \"isTagged\" : false,\n  \"contractBandwidthPool\" : {\n    \"name\" : \"name\",\n    \"contractBandwidthMbps\" : 5\n  },\n  \"isMultiport\" : false,\n  \"trustReceivedCosDscp\" : true,\n  \"attachmentId\" : 0,\n  \"isLayer3\" : false,\n  \"attachmentBandwidthGbps\" : 5,\n  \"infrastructureDevice\" : {\n    \"useLayer2InterfaceMtu\" : true,\n    \"planeName\" : \"planeName\",\n    \"locationName\" : \"locationName\",\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"deviceModel\" : \"deviceModel\",\n    \"ports\" : [ {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    }, {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    } ],\n    \"deviceID\" : 6,\n    \"deviceStatus\" : \"deviceStatus\"\n  },\n  \"routingInstance\" : {\n    \"routingInstanceId\" : 0,\n    \"name\" : \"name\"\n  },\n  \"tenant\" : {\n    \"tenantId\" : 0,\n    \"name\" : \"name\"\n  }\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Attachment>(exampleJson)
            : default(Attachment);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find all attachments for a given device
        /// </summary>
        /// <remarks>Returns all attachments for a given tenant</remarks>
        /// <param name="deviceId">ID of the infrastructure device</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpGet]
        [Route("/v1/infrastructure/{deviceId}/attachment")]
        [ValidateModelState]
        [SwaggerOperation("GetInfrastructureAttachmentsByDeviceId")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Attachment>), description: "Successful operation")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult GetInfrastructureAttachmentsByDeviceId([FromRoute][Required]int? deviceId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Attachment>));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Attachment>\n  <attachmentId>123</attachmentId>\n  <trustReceivedCosDscp>true</trustReceivedCosDscp>\n  <isLayer3>true</isLayer3>\n  <isBundle>true</isBundle>\n  <isMultiport>true</isMultiport>\n  <isTagged>true</isTagged>\n  <attachmentBandwidthGbps>123</attachmentBandwidthGbps>\n</Attachment>";
            exampleJson = "[ {\n  \"isBundle\" : false,\n  \"isTagged\" : false,\n  \"contractBandwidthPool\" : {\n    \"name\" : \"name\",\n    \"contractBandwidthMbps\" : 5\n  },\n  \"isMultiport\" : false,\n  \"trustReceivedCosDscp\" : true,\n  \"attachmentId\" : 0,\n  \"isLayer3\" : false,\n  \"attachmentBandwidthGbps\" : 5,\n  \"infrastructureDevice\" : {\n    \"useLayer2InterfaceMtu\" : true,\n    \"planeName\" : \"planeName\",\n    \"locationName\" : \"locationName\",\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"deviceModel\" : \"deviceModel\",\n    \"ports\" : [ {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    }, {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    } ],\n    \"deviceID\" : 6,\n    \"deviceStatus\" : \"deviceStatus\"\n  },\n  \"routingInstance\" : {\n    \"routingInstanceId\" : 0,\n    \"name\" : \"name\"\n  },\n  \"tenant\" : {\n    \"tenantId\" : 0,\n    \"name\" : \"name\"\n  }\n}, {\n  \"isBundle\" : false,\n  \"isTagged\" : false,\n  \"contractBandwidthPool\" : {\n    \"name\" : \"name\",\n    \"contractBandwidthMbps\" : 5\n  },\n  \"isMultiport\" : false,\n  \"trustReceivedCosDscp\" : true,\n  \"attachmentId\" : 0,\n  \"isLayer3\" : false,\n  \"attachmentBandwidthGbps\" : 5,\n  \"infrastructureDevice\" : {\n    \"useLayer2InterfaceMtu\" : true,\n    \"planeName\" : \"planeName\",\n    \"locationName\" : \"locationName\",\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"deviceModel\" : \"deviceModel\",\n    \"ports\" : [ {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    }, {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    } ],\n    \"deviceID\" : 6,\n    \"deviceStatus\" : \"deviceStatus\"\n  },\n  \"routingInstance\" : {\n    \"routingInstanceId\" : 0,\n    \"name\" : \"name\"\n  },\n  \"tenant\" : {\n    \"tenantId\" : 0,\n    \"name\" : \"name\"\n  }\n} ]";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Attachment>>(exampleJson)
            : default(List<Attachment>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Update an existing attachment
        /// </summary>
        
        /// <param name="attachmentId">ID of the attachment</param>
        /// <param name="body">attachment update object that updates an existing attachment</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">The specified resource was not found</response>
        [HttpPut]
        [Route("/v1/infrastructure/attachment/{attachmentId}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateInfrastructureAttachment")]
        [SwaggerResponse(statusCode: 200, type: typeof(Attachment), description: "Successful operation")]
        [SwaggerResponse(statusCode: 400, type: typeof(ApiResponse), description: "Validation error")]
        [SwaggerResponse(statusCode: 404, type: typeof(ApiResponse), description: "The specified resource was not found")]
        public virtual IActionResult UpdateInfrastructureAttachment([FromRoute][Required]int? attachmentId, [FromBody]InfrastructureAttachmentUpdate body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Attachment));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(ApiResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(ApiResponse));

            string exampleJson = null;
            exampleJson = "<Attachment>\n  <attachmentId>123</attachmentId>\n  <trustReceivedCosDscp>true</trustReceivedCosDscp>\n  <isLayer3>true</isLayer3>\n  <isBundle>true</isBundle>\n  <isMultiport>true</isMultiport>\n  <isTagged>true</isTagged>\n  <attachmentBandwidthGbps>123</attachmentBandwidthGbps>\n</Attachment>";
            exampleJson = "{\n  \"isBundle\" : false,\n  \"isTagged\" : false,\n  \"contractBandwidthPool\" : {\n    \"name\" : \"name\",\n    \"contractBandwidthMbps\" : 5\n  },\n  \"isMultiport\" : false,\n  \"trustReceivedCosDscp\" : true,\n  \"attachmentId\" : 0,\n  \"isLayer3\" : false,\n  \"attachmentBandwidthGbps\" : 5,\n  \"infrastructureDevice\" : {\n    \"useLayer2InterfaceMtu\" : true,\n    \"planeName\" : \"planeName\",\n    \"locationName\" : \"locationName\",\n    \"name\" : \"name\",\n    \"description\" : \"description\",\n    \"deviceModel\" : \"deviceModel\",\n    \"ports\" : [ {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    }, {\n      \"portPool\" : \"portPool\",\n      \"name\" : \"name\",\n      \"portStatus\" : \"portStatus\",\n      \"portId\" : 1,\n      \"type\" : \"type\",\n      \"portSfp\" : \"portSfp\",\n      \"portRole\" : \"portRole\"\n    } ],\n    \"deviceID\" : 6,\n    \"deviceStatus\" : \"deviceStatus\"\n  },\n  \"routingInstance\" : {\n    \"routingInstanceId\" : 0,\n    \"name\" : \"name\"\n  },\n  \"tenant\" : {\n    \"tenantId\" : 0,\n    \"name\" : \"name\"\n  }\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Attachment>(exampleJson)
            : default(Attachment);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
