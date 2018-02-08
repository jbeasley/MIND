using System.Threading.Tasks;
using SCM.Models;

namespace SCM.Validators
{
    public interface ITenantMulticastGroupValidator : IValidator
    {
        Task ValidateNewAsync(TenantMulticastGroup tenantMulticastGroup);
        Task ValidateDeleteAsync(TenantMulticastGroup tenantMulticastGroup);
        Task ValidateChangesAsync(TenantMulticastGroup tenantMulticastGroup);
    }
}
