using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureLogicalInterfaceDirector : IInfrastructureLogicalInterfaceDirector
    {
        // Factory for the logical interface builder - the factory ensures we get a unique instance of the builder
        // for each logical interface request which is necessary when constructing a collection of logical interfaces
        private readonly Func<ILogicalInterfaceBuilder> _builderFactory;

        public InfrastructureLogicalInterfaceDirector(Func<ILogicalInterfaceBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<SCM.Models.LogicalInterface> BuildAsync(int deviceId, InfrastructureLogicalInterfaceRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForDevice(deviceId)
                                .ForInfrastructureRoutingInstance(request.RoutingInstanceName)
                                .WithDescription(request.Description)
                                .WithIpv4(request.Ipv4Address)
                                .WithType(request.LogicalInterfaceType.ToString())
                                .BuildAsync();
        }

        public async Task<List<LogicalInterface>> BuildAsync(int deviceId, List<InfrastructureLogicalInterfaceRequest> requests)
        {
            var logicalInterfaces = new List<LogicalInterface>();
            var tasks = requests.Select(
                                 async request =>
                                 {
                                     // Each logical interface will be built from a distinct instance of the logical interface  builder
                                     logicalInterfaces.Add(await BuildAsync(deviceId, request));
                                 });

            await Task.WhenAll(tasks);
            return logicalInterfaces;
        }

        public async Task<LogicalInterface> UpdateAsync(int logicalInterfaceId, LogicalInterfaceUpdate update)
        {
            var builder = _builderFactory();
            return await builder.ForLogicalInterface(logicalInterfaceId)
                                .WithDescription(update.Description)
                                .WithIpv4(update.Ipv4Address)
                                .BuildAsync();
        }

        public async Task<List<LogicalInterface>> UpdateAsync(List<LogicalInterfaceUpdate> updates)
        {
            var logicalInterfaces = new List<LogicalInterface>();
            var tasks = updates.Select(
                                 async update =>
                                 {
                                     if (update.LogicalInterfaceId.HasValue)
                                     {
                                         // Each logical interface will be updated from a distinct instance of the logical interface  builder
                                         logicalInterfaces.Add(await UpdateAsync(update.LogicalInterfaceId.Value, update));
                                     }
                                 });

            await Task.WhenAll(tasks);
            return logicalInterfaces;
        }
    }
}
