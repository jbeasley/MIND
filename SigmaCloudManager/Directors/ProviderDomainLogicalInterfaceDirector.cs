using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainLogicalInterfaceDirector : IProviderDomainLogicalInterfaceDirector
    {
        private readonly ILogicalInterfaceBuilder _builder;

        public ProviderDomainLogicalInterfaceDirector(ILogicalInterfaceBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.LogicalInterface> BuildAsync(int routingInstanceId, ProviderDomainLogicalInterfaceRequest request)
        {
            return await _builder.ForProviderDomainRoutingInstance(routingInstanceId)
                                 .WithDescription(request.Description)
                                 .WithIpv4(request.Ipv4Address)
                                 .WithType(request.LogicalInterfaceType.ToString())
                                 .BuildAsync();
        }

        public async Task<LogicalInterface> UpdateAsync(int logicalInterfaceId, LogicalInterfaceUpdate update)
        {
            return await _builder.ForLogicalInterface(logicalInterfaceId)
                                .WithDescription(update.Description)
                                .WithIpv4(update.Ipv4Address)
                                .BuildAsync();
        }
    }
}
