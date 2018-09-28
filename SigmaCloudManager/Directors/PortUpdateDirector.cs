using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class PortUpdateDirector : IPortUpdateDirector
    {
        // Factory for the port builder - the factory ensures we get a unique instance of the builder
        // for each port request which is necessary when performing updates on a collection of ports
        private readonly Func<IPortUpdateBuilder> _builderFactory;

        public PortUpdateDirector(Func<IPortUpdateBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Update a single port
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Port> UpdateAsync(int portId, PortUpdate update)
        {
            var builder = _builderFactory();
            return await builder.ForPort(portId)
                                 .WithConnector(update.PortConnector)
                                 .WithSfp(update.PortSfp)
                                 .WithStatus(update.PortStatus.ToString())
                                 .AssignToTenant(update.TenantId)
                                 .UpdateAsync();
        }

        /// <summary>
        /// Update a collection of ports
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        public async Task<List<Port>> UpdateAsync(List<PortUpdate> updates)
        {
            var builder = _builderFactory();
            var ports = new List<Port>();
            var tasks = updates.Select(
                                    async update =>
                                    {
                                        if (update.PortId.HasValue) ports.Add(await UpdateAsync(update.PortId.Value, update));
                                    });

            await Task.WhenAll(tasks);
            return ports;
        }
    }
}
