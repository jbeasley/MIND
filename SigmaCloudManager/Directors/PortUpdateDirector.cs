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
        private readonly IPortUpdateBuilder _builder;

        public PortUpdateDirector(IPortUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Port> UpdateAsync(int portId, PortUpdate update)
        {
            return await _builder.ForPort(portId)
                                 .WithConnector(update.PortConnector)
                                 .WithSfp(update.PortSfp)
                                 .WithStatus(update.PortStatus.ToString())
                                 .AssignToTenant(update.TenantId)
                                 .UpdateAsync();
        }
    }
}
