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
    /// Builder for updating  device port. The builder exposes a fluent UI.
    /// </summary>
    public class PortUpdateBuilder : PortBuilder, IPortUpdateBuilder
    {

        public PortUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPortUpdateBuilder ForPort(int? portId)
        {
            if (portId.HasValue) _args.Add(nameof(ForPort), portId);
            return this;
        }

        IPortUpdateBuilder IPortUpdateBuilder.AssignToTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(AssignToTenant), tenantId);
            return this;
        }


        IPortUpdateBuilder IPortUpdateBuilder.WithConnector(string connector)
        {
            base.WithConnector(connector);
            return this;
        }

        IPortUpdateBuilder IPortUpdateBuilder.WithSfp(string sfp)
        {
            base.WithSfp(sfp);
            return this;
        }

        IPortUpdateBuilder IPortUpdateBuilder.WithStatus(string status)
        {
            base.WithStatus(status);
            return this;
        }

        public virtual async Task<Port> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForPort))) await SetPortAsync();
            if (_args.ContainsKey(nameof(WithConnector))) await SetConnectorAsync();
            if (_args.ContainsKey(nameof(WithSfp))) await SetSfpAsync();
            if (_args.ContainsKey(nameof(WithStatus))) await SetStatusAsync();
            if (_args.ContainsKey(nameof(AssignToTenant))) await SetTenantAsync();

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

        protected override internal async Task SetStatusAsync()
        {
            await base.SetStatusAsync();
            if (this._port.PortStatus != null)
            {
                // If the port status is free ensure that any tenant assignment is cleared
                if (this._port.PortStatus.PortStatusType == PortStatusTypeEnum.Free)
                {
                    this._port.Tenant = null;
                    this._port.TenantID = null;
                }
            }
        }
    }
}
