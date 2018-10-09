using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for creating a device port. The builder exposes a fluent UI.
    /// </summary>
    public class PortBuilder : BaseBuilder, IPortBuilder
    {
        protected internal Port _port;

        public PortBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) => _port = new Port();

        public virtual IPortBuilder ForPort(int? portId)
        {
            if (portId.HasValue) _args.Add(nameof(ForPort), portId);
            return this;
        }

        public virtual IPortBuilder AssignToTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(AssignToTenant), tenantId);
            return this;
        }

        public virtual IPortBuilder ForDevice(Device device)
        {
            if (device != null) _args.Add(nameof(ForDevice), device);
            return this;
        }

        public virtual IPortBuilder ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual IPortBuilder WithConnector(string connector)
        {
           if (!string.IsNullOrEmpty(connector)) _args.Add(nameof(WithConnector), connector);
            return this;
        }

        public virtual IPortBuilder WithName(string name)
        {
            if (!string.IsNullOrEmpty(name)) _args.Add(nameof(WithName), name);
            return this;
        }

        public virtual IPortBuilder WithPortBandwidth(int? portBandwidthGbps)
        {
            if (portBandwidthGbps.HasValue) _args.Add(nameof(WithPortBandwidth), portBandwidthGbps);
            return this;
        }

        public virtual IPortBuilder WithPortRole(string portRole)
        {
            if (!string.IsNullOrEmpty(portRole)) _args.Add(nameof(WithPortRole), portRole);
            return this;
        }

        public virtual IPortBuilder WithPortPool(string portPool)
        {
            if (!string.IsNullOrEmpty(portPool)) _args.Add(nameof(WithPortPool), portPool);
            return this;
        }

        public virtual IPortBuilder WithSfp(string sfp)
        {
            if (!string.IsNullOrEmpty(sfp)) _args.Add(nameof(WithSfp), sfp);
            return this;
        }

        public virtual IPortBuilder WithStatus(string status)
        {
            if (!string.IsNullOrEmpty(status)) _args.Add(nameof(WithStatus), status);
            return this;
        }

        public virtual IPortBuilder WithType(string type)
        {
            if (!string.IsNullOrEmpty(type)) _args.Add(nameof(WithType), type);
            return this;
        }

        public virtual async Task<Port> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForPort)))
            {
                await SetPortAsync();
                if (_args.ContainsKey(nameof(ForPort))) await SetPortAsync();
                if (_args.ContainsKey(nameof(WithConnector))) await SetConnectorAsync();
                if (_args.ContainsKey(nameof(WithSfp))) await SetSfpAsync();
                if (_args.ContainsKey(nameof(WithStatus))) await SetStatusAsync();
                if (_args.ContainsKey(nameof(AssignToTenant))) await SetTenantAsync();
            }
            else
            {
                if (_args.ContainsKey(nameof(ForDevice))) await SetDeviceAsync();
                if (_args.ContainsKey(nameof(WithType))) SetType();
                if (_args.ContainsKey(nameof(WithName))) SetName();
                if (_args.ContainsKey(nameof(WithConnector))) await SetConnectorAsync();
                if (_args.ContainsKey(nameof(WithPortRole)) && _args.ContainsKey(nameof(WithPortPool))) await SetPortPoolAsync();
                if (_args.ContainsKey(nameof(WithPortBandwidth))) await SetPortBandwidthAsync();
                if (_args.ContainsKey(nameof(WithSfp))) await SetSfpAsync();
                if (_args.ContainsKey(nameof(WithStatus))) await SetStatusAsync();
                if (_args.ContainsKey(nameof(AssignToTenant))) await SetTenantAsync();
            }

            _port.Validate();
            return _port;
        }

        protected internal virtual async Task SetPortAsync()
        {
            var portId = (int)_args[nameof(ForPort)];
            var port = (from result in await _unitOfWork.PortRepository.GetAsync(
                     q =>
                       q.ID == portId,
                       query: q => q.IncludeValidationProperties(),
                       AsTrackable: true)
                        select result)
                       .SingleOrDefault();

            _port = port ?? throw new BuilderBadArgumentsException($"Could not find the port with ID '{portId}'.");
        }

        protected internal virtual async Task SetDeviceAsync()
        {
            if (_args[nameof(ForDevice)].GetType() == typeof(Device))
            {
                var device = (Device)_args[nameof(ForDevice)];
                _port.Device = device;
            }
            else
            {
                var deviceId = (int)_args[nameof(ForDevice)];
                var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                            q =>
                              q.DeviceID == deviceId,
                              query: q => q.IncludeValidationProperties(),
                              AsTrackable: true)
                              select result)
                              .SingleOrDefault();

                _port.Device = device;
            }
        }

        protected internal virtual void SetType()
        {
            var type = _args[nameof(WithType)].ToString();
            _port.Type = type;
        }
        
        protected internal virtual void SetName()
        {
            var name = _args[nameof(WithName)].ToString();
            _port.Name = name;
        }

        protected internal virtual async Task SetConnectorAsync()
        {
            var connectorName = _args[nameof(WithConnector)].ToString();
            var connector = (from result in await _unitOfWork.PortConnectorRepository.GetAsync(
                        q =>
                            q.Name == connectorName,
                            AsTrackable: true)
                            select result)
                            .SingleOrDefault();

            _port.PortConnector = connector;
        }

        protected internal virtual async Task SetPortPoolAsync()
        {
            var portRoleName = _args[nameof(WithPortRole)].ToString();
            var portPoolName = _args[nameof(WithPortPool)].ToString();
            var portPool = (from result in await _unitOfWork.PortPoolRepository.GetAsync(
                        q =>
                            q.PortRole.Name == portRoleName &&
                            q.Name == portPoolName,
                            AsTrackable: true,
                            query: q => q.IncludeValidationProperties())
                             select result)
                            .SingleOrDefault();

            _port.PortPool = portPool;
        }

        protected internal virtual async Task SetStatusAsync()
        {
            var statusName = _args[nameof(WithStatus)].ToString();
            var status = (from result in await _unitOfWork.PortStatusRepository.GetAsync(
                        q =>
                          q.Name == statusName,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _port.PortStatus = status;

            // If the port status is free ensure that any tenant assignment is cleared
            if (status.PortStatusType == PortStatusTypeEnum.Free)
            {
                this._port.Tenant = null;
                this._port.TenantID = null;
            }
        }

        protected internal virtual async Task SetSfpAsync()
        {
            var sfpName = _args[nameof(WithSfp)].ToString();
            var sfp = (from result in await _unitOfWork.PortSfpRepository.GetAsync(
                        q =>
                            q.Name == sfpName,
                            AsTrackable: true)
                            select result)
                            .SingleOrDefault();

            _port.PortSfp = sfp;
        }

        protected internal virtual async Task SetPortBandwidthAsync()
        {
            var portBandwidthGbps = (int)_args[nameof(WithPortBandwidth)];
            var portBandwidth = (from result in await _unitOfWork.PortBandwidthRepository.GetAsync(
                            q =>
                                q.BandwidthGbps == portBandwidthGbps,
                                AsTrackable: true)
                                select result)
                                .SingleOrDefault();

            _port.PortBandwidth = portBandwidth;
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(AssignToTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                    q =>
                        q.TenantID == tenantId,
                        AsTrackable: true)
                          select result)
                        .SingleOrDefault();

            _port.Tenant = tenant;
        }
    }
}
