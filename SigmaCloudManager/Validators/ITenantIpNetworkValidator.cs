using System.Threading.Tasks;
using SCM.Models;

namespace SCM.Validators
{
    public interface ITenantIpNetworkValidator : IValidator
    {
        Task ValidateDeleteAsync(int tenantIpNetworkId);
        Task ValidateChangesAsync(TenantIpNetwork tenantIpNetwork);
    }
}
