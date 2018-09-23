using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IPortUpdateBuilder
    {
        IPortUpdateBuilder ForPort(int? portId);
        IPortUpdateBuilder WithSfp(string sfp);
        IPortUpdateBuilder WithConnector(string connector);
        IPortUpdateBuilder WithStatus(string status);
        IPortUpdateBuilder AssignToTenant(int? tenantId);
        Task<Port> UpdateAsync();
    }

}
