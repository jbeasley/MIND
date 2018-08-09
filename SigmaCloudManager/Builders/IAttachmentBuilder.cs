using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentBuilder
    {
        IAttachmentBuilder Init(int tenantId);
        IAttachmentBuilder CreateInterfaces(string IpAddress, string subnetMask);
        IAttachmentBuilder SetNumPortsRequired(int numPortsRequired);
        IAttachmentBuilder SetPortBandwidthRequired(int portBandwidthRequired);
        Task SetAttachmentRoleAsync(string portPoolName, string attachmentRoleName);
        Task SetAttachmentBandwidthAsync(int? attachmentBandwidthGbps);
        Task SetMtuAsync();
        Task AllocatePortsAsync(string locationName, string planeName);
        Task CreateContractBandwidthPoolAsync(int tenantID, int? contractBandwidthMbps);
        Task CreateRoutingInstanceAsync();
        Attachment GetResult();

    }
}
