﻿using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class PortDirector : IPortDirector
    {
        // Factory for the port builder - the factory ensures we get a unique instance of the builder
        // for each port request which is necessary when constructing a collection of ports
        private readonly Func<IPortBuilder> _builderFactory;

        public PortDirector(Func<IPortBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Build a port for a given device.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.Port> BuildAsync(int deviceId, PortRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForDevice(deviceId)
                                .WithType(request.Type)
                                .WithName(request.Name)
                                .WithPortBandwidth(request.PortBandwidthGbps)
                                .WithConnector(request.PortConnector)
                                .WithPortRole(request.PortRole)
                                .WithPortPool(request.PortPool)
                                .WithSfp(request.PortSfp)
                                .WithStatus(request.PortStatus.ToString())
                                .AssignToTenant(request.TenantId)
                                .BuildAsync();
        }

        /// <summary>
        /// Build a port for a given device.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.Port> BuildAsync(Device device, PortRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForDevice(device)
                                 .WithType(request.Type)
                                 .WithName(request.Name)
                                 .WithPortBandwidth(request.PortBandwidthGbps)
                                 .WithConnector(request.PortConnector)
                                 .WithPortRole(request.PortRole)
                                 .WithPortPool(request.PortPool)
                                 .WithSfp(request.PortSfp)
                                 .WithStatus(request.PortStatus.ToString())
                                 .AssignToTenant(request.TenantId)
                                 .BuildAsync();
        }

        /// <summary>
        /// Build a collection of ports for a given device.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="requests"></param>
        /// <returns></returns>
        public async Task<List<SCM.Models.Port>> BuildAsync(Device device, List<PortRequest> requests)
        {
            var ports = new List<Port>();
            var tasks = requests.Select(
                                 async request =>
                                    {
                                        // Each port will be built from a distinct instance of the port builder
                                        ports.Add(await BuildAsync(device, request));
                                    }
                                 );

            await Task.WhenAll(tasks);

            return ports;
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
                                 .BuildAsync();
        }

        /// <summary>
        /// Update a collection of ports
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        public async Task<List<Port>> UpdateAsync(List<PortUpdate> updates)
        {
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
