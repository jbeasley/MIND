using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IPortBuilder
    {
        IPortBuilder ForDevice(Device device);
        IPortBuilder ForDevice(int? deviceId);
        IPortBuilder WithType(string type);
        IPortBuilder WithName(string name);
        IPortBuilder WithPortBandwidth(int? portBandwidth);
        IPortBuilder WithSfp(string sfp);
        IPortBuilder WithConnector(string connector);
        IPortBuilder WithPortRole(string portRole);
        IPortBuilder WithPortPool(string portPool);
        IPortBuilder WithStatus(string status);
        IPortBuilder AssignToTenant(int? tenantId);
        Task<Port> BuildAsync();
    }
}
