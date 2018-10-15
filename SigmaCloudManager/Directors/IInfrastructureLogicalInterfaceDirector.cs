using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IInfrastructureLogicalInterfaceDirector
    {
        Task<LogicalInterface> BuildAsync(int deviceId, InfrastructureLogicalInterfaceRequest request);
        Task<List<LogicalInterface>> BuildAsync(int deviceId, List<InfrastructureLogicalInterfaceRequest> requests);
        Task<LogicalInterface> UpdateAsync(int deviceId, LogicalInterfaceUpdate update);
        Task<List<LogicalInterface>> UpdateAsync(List<LogicalInterfaceUpdate> updates);
    }
}
