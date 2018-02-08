using System.Threading.Tasks;
using SCM.Models;

namespace SCM.Validators
{
    public interface ITenantNetworkValidator : IValidator
    {
        Task ValidateNewAsync(TenantNetwork tenantNetwork);
        Task ValidateDeleteAsync(TenantNetwork tenantNetwork);
        Task ValidateChangesAsync(TenantNetwork tenantNetwork);
    }
}
