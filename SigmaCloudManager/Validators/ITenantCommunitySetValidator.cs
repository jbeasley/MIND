using System.Threading.Tasks;
using SCM.Models;

namespace SCM.Validators
{
    public interface ITenantCommunitySetValidator : IValidator
    {
        Task ValidateNewAsync(TenantCommunitySet tenantCommunitySet);
        Task ValidateDeleteAsync(TenantCommunitySet tenantCommunitySet);
        Task ValidateChangesAsync(TenantCommunitySet tenantCommunitySet);
    }
}
