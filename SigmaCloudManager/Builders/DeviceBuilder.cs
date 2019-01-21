using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;
using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    /// <summary>
    /// Abstract builder for devices. The builder exposes a fluent API.
    /// </summary>
    public abstract class DeviceBuilder : BaseBuilder
    {
        protected internal Device _device;
        private readonly IRoutingInstanceDirector _routingInstanceDirector;
        private readonly IPortDirector _portDirector;

        public DeviceBuilder(IUnitOfWork unitOfWork, IRoutingInstanceDirector routingInstanceDirector, IPortDirector portDirector) : base(unitOfWork)
        {
            _device = new Device
            {
                Created = true,
                ShowCreatedAlert = true,
                Ports = new List<Port>(),
                RoutingInstances = new List<RoutingInstance>()
            };

            _routingInstanceDirector = routingInstanceDirector;
            _portDirector = portDirector;
        }

        public DeviceBuilder ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual DeviceBuilder WithName(string name)
        {
            if (!string.IsNullOrEmpty(name)) _args.Add(nameof(WithName), name);
            return this;
        }

        public virtual DeviceBuilder WithLocation(string locationName)
        {
            if (!string.IsNullOrEmpty(locationName)) _args.Add(nameof(WithLocation), locationName);
            return this;
        }

        public virtual DeviceBuilder WithDescription(string description)
        {
            if (!string.IsNullOrEmpty(description)) _args.Add(nameof(WithDescription), description);
            return this;
        }

        public virtual DeviceBuilder WithRole(string role)
        {
            if (!string.IsNullOrEmpty(role)) _args.Add(nameof(WithRole), role);
            return this;
        }

        public virtual DeviceBuilder WithModel(string model)
        {
            if (!string.IsNullOrEmpty(model)) _args.Add(nameof(WithModel), model);
            return this;
        }

        public virtual DeviceBuilder WithStatus(string status)
        {
            if (!string.IsNullOrEmpty(status)) _args.Add(nameof(WithStatus), status);
            return this;
        }

        public virtual DeviceBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu)
        {
            if (useLayer2InterfaceMtu.HasValue) _args.Add(nameof(UseLayer2InterfaceMtu), useLayer2InterfaceMtu);
            return this;
        }

        public virtual DeviceBuilder WithPortRequestsOrUpdates(List<PortRequestOrUpdate> ports)
        {
            if (ports != null) _args.Add(nameof(WithPortRequestsOrUpdates), ports);
            return this;
        }


        /// <summary>
        /// Build the device
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Device> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForDevice)))
            {
                // Apply updates to an existing device
                await SetDeviceAsync();
                if (_args.ContainsKey(nameof(WithName))) SetName();
                if (_args.ContainsKey(nameof(WithStatus))) await SetStatusAsync();
                if (_args.ContainsKey(nameof(WithDescription))) SetDescription();
                if (_args.ContainsKey(nameof(UseLayer2InterfaceMtu))) SetUseLayer2InterfaceMtu();
                if (_args.ContainsKey(nameof(WithPortRequestsOrUpdates))) await ModifyPortsAsync();
            }
            else
            {
                // Create a new device
                if (_args.ContainsKey(nameof(WithName))) SetName();
                if (_args.ContainsKey(nameof(WithLocation))) await SetLocationAsync();
                if (_args.ContainsKey(nameof(WithRole))) await SetRoleAsync();
                if (_args.ContainsKey(nameof(WithStatus))) await SetStatusAsync();
                if (_args.ContainsKey(nameof(WithModel))) await SetModelAsync();
                if (_args.ContainsKey(nameof(WithDescription))) SetDescription();
                if (_args.ContainsKey(nameof(UseLayer2InterfaceMtu))) SetUseLayer2InterfaceMtu();
                if (_args.ContainsKey(nameof(WithPortRequestsOrUpdates))) await ModifyPortsAsync();
                await CreateRoutingInstanceAsync();
            }

            return _device;
        }

        protected abstract internal Task SetRoleAsync();

        protected internal virtual void SetName()
        {
            var name = _args[nameof(WithName)].ToString();
            _device.Name = name;
        }

        protected internal virtual void SetDescription()
        {
            var description = _args[nameof(WithDescription)].ToString();
            _device.Description = description;
        }

        protected internal virtual void SetUseLayer2InterfaceMtu()
        {
            var useLayer2InterfaceMtu = (bool)_args[nameof(UseLayer2InterfaceMtu)];
            _device.UseLayer2InterfaceMtu = useLayer2InterfaceMtu;
        }

        protected internal virtual async Task SetLocationAsync()
        {
            var locationName = _args[nameof(WithLocation)].ToString();
            var location = (from result in await _unitOfWork.LocationRepository.GetAsync(
                         x =>
                            x.SiteName == locationName,
                            AsTrackable: true)
                            select result)
                            .SingleOrDefault();

            _device.Location = location;
        }

        protected internal virtual async Task SetModelAsync()
        {
            var modelName = _args[nameof(WithModel)].ToString();
            var model = (from result in await _unitOfWork.DeviceModelRepository.GetAsync(
                       x =>
                         x.Name == modelName,
                         AsTrackable: true)
                         select result)
                         .SingleOrDefault();

            _device.DeviceModel = model;
        }

        protected internal virtual async Task SetStatusAsync()
        {
            var statusType = _args[nameof(WithStatus)].ToString();
            var status = (from result in await _unitOfWork.DeviceStatusRepository.GetAsync(
                        x =>
                          x.DeviceStatusType == Enum.Parse<SCM.Models.DeviceStatusTypeEnum>(statusType),
                          AsTrackable: true)
                          select result)  
                          .SingleOrDefault();

            _device.DeviceStatus = status;
        }

        protected internal virtual async Task CreateRoutingInstanceAsync()
        {
            var routingInstance = await _routingInstanceDirector.BuildAsync(this._device, new RoutingInstanceRequest());
            _device.RoutingInstances.Add(routingInstance);     
        }

        protected internal virtual async Task SetDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == deviceId,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _device = device ?? throw new BuilderBadArgumentsException($"The device with ID '{deviceId}' was not found.");
        }

        /// <summary>
        /// Returns a collection of port requests for new ports.
        /// </summary>
        /// <returns>The requests.</returns>
        protected internal virtual List<PortRequestOrUpdate> PortRequests()
        {
            var requests = (List<PortRequestOrUpdate>)_args[nameof(WithPortRequestsOrUpdates)];
            return requests.Where(request => request.PortId == null).ToList();
        }

        /// <summary>
        /// Returns a collection of port requests to update existing ports.
        /// </summary>
        /// <returns>The updates.</returns>
        protected internal virtual List<PortRequestOrUpdate> PortUpdates()
        {
            var requests = (List<PortRequestOrUpdate>)_args[nameof(WithPortRequestsOrUpdates)];
            return requests.Where(request => request.PortId != null).ToList();
        }

        /// <summary>
        /// Modify the ports of the device by creating new ports, updating existing ports,
        /// and removing ports from the ports collection of the device.
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task ModifyPortsAsync()
        { 
            // Create new ports
            var newPorts = await _portDirector.BuildAsync(this._device, PortRequests());

            // Update existing ports
            var updatePortRequests = PortUpdates();
            var updatedPorts = await _portDirector.UpdateAsync(updatePortRequests);

            // Validate requests to delete ports
            if (_device.Ports.Any())
            {
                var deletePorts = _device.Ports.Where(
                                                port =>
                                                !updatePortRequests.Select(
                                                update =>
                                                update.PortId)
                                                .Contains(port.ID)
                );

                foreach (var deletePort in deletePorts)
                {
                    deletePort.ValidateDelete();
                }
            }

            // Modify the ports collection on the device
            var ports = newPorts.Concat(updatedPorts);
            this._device.Ports = ports.ToList();
        }
    }
}
