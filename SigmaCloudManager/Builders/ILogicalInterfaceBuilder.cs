using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface ILogicalInterfaceBuilder
    {
        ILogicalInterfaceBuilder ForDevice(int? deviceId);
        ILogicalInterfaceBuilder ForProviderDomainRoutingInstance(int? routingInstanceId);
        ILogicalInterfaceBuilder ForInfrastructureRoutingInstance(string routingInstanceName);
        ILogicalInterfaceBuilder ForLogicalInterface(int? logicalInterfaceId);
        ILogicalInterfaceBuilder WithDescription(string description);
        ILogicalInterfaceBuilder WithIpv4(Ipv4AddressAndMask ipv4AddressAndMask);
        ILogicalInterfaceBuilder WithType(string type);
        Task<LogicalInterface> BuildAsync();
    }
}
