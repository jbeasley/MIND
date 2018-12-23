using SCM.Models;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantDomainDeviceDirector
    {
        Task<Device> BuildAsync(int tenantId, TenantDomainDeviceRequest request);
        Task<Device> UpdateAsync(int deviceId, TenantDomainDeviceUpdate update);
    }
}
