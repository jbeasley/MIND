using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IVrfRoutingInstanceBuilder : IRoutingInstanceBuilder
    {
        IVrfRoutingInstanceBuilder ForRoutingInstance(int? routingInstanceId);
        IVrfRoutingInstanceBuilder ForDevice(int? deviceId);
        IVrfRoutingInstanceBuilder ForAttachment(Attachment attachment);
        IVrfRoutingInstanceBuilder ForVif(Vif vif);
        IVrfRoutingInstanceBuilder WithTenant(int? tenantId);
        IVrfRoutingInstanceBuilder WithRangeType(string rdRangeType);
        IVrfRoutingInstanceBuilder WithName(string name);
        IVrfRoutingInstanceBuilder WithAdministratorSubField(int? rdAdministratorNumberSubField);
        IVrfRoutingInstanceBuilder WithAssignedNumberSubField(int? rdAssignedNumberSubField);
        IVrfRoutingInstanceBuilder WithRoutingInstanceType(string routingInstanceTypeEnum);
        IVrfRoutingInstanceBuilder WithBgpPeers(List<BgpPeerRequest> bgpPeerRequests);
    }
}
