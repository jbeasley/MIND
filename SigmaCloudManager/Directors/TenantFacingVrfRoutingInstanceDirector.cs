using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantFacingVrfRoutingInstanceDirector : IVrfRoutingInstanceDirector
    {
        private readonly IVrfRoutingInstanceBuilder _builder;

        public TenantFacingVrfRoutingInstanceDirector(IVrfRoutingInstanceBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Builds a new routing instance for the specified device and (optionally) associated with a specified tenant.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="tenantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.RoutingInstance> BuildAsync(int deviceId, int? tenantId, RoutingInstanceRequest request)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithTenant(tenantId)
                                 .WithRangeType(request?.RangeType?.ToString())
                                 .WithAdministratorSubField(request?.AdministratorSubField)
                                 .WithAssignedNumberSubField(request?.AssignedNumberSubField)
                                 .WithRoutingInstanceType(RoutingInstanceTypeEnum.TenantFacingVrf.ToString())
                                 .WithBgpPeers(request?.BgpPeers)
                                 .WithName(request?.Name)
                                 .BuildAsync();
        }

        /// <summary>
        /// Builds a new routing instance for the specified attachment.
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.RoutingInstance> BuildAsync(Attachment attachment, RoutingInstanceRequest request)
        {
            return await _builder.ForAttachment(attachment)
                                 .WithRangeType(request?.RangeType?.ToString())
                                 .WithAdministratorSubField(request?.AdministratorSubField)
                                 .WithAssignedNumberSubField(request?.AssignedNumberSubField)
                                 .WithRoutingInstanceType(RoutingInstanceTypeEnum.TenantFacingVrf.ToString())
                                 .WithBgpPeers(request?.BgpPeers)
                                 .WithName(request?.Name)
                                 .BuildAsync();
        }

        /// <summary>
        /// Builds a new routing instance for the specified vif.
        /// </summary>
        /// <param name="vif"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.RoutingInstance> BuildAsync(Vif vif, RoutingInstanceRequest request)
        {
            return await _builder.ForVif(vif)
                                 .WithRangeType(request?.RangeType?.ToString())
                                 .WithAdministratorSubField(request?.AdministratorSubField)
                                 .WithAssignedNumberSubField(request?.AssignedNumberSubField)
                                 .WithRoutingInstanceType(RoutingInstanceTypeEnum.TenantFacingVrf.ToString())
                                 .WithBgpPeers(request?.BgpPeers)
                                 .WithName(request?.Name)
                                 .BuildAsync();
        }

        /// <summary>
        /// Updates an existing routing instance.
        /// </summary>
        /// <param name="routingInstanceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.RoutingInstance> BuildAsync(int routingInstanceId, RoutingInstanceRequest request)
        {
            return await _builder.ForRoutingInstance(routingInstanceId)
                                 .WithAdministratorSubField(request?.AdministratorSubField)
                                 .WithAssignedNumberSubField(request?.AssignedNumberSubField)
                                 .WithBgpPeers(request?.BgpPeers)
                                 .WithName(request?.Name)
                                 .BuildAsync();
        }
    }
}
