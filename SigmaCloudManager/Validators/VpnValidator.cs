using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Data;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for vpns
    /// </summary>
    public class VpnValidator : BaseValidator, IVpnValidator
    {
        public VpnValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate deletion of a VPN.
        /// </summary>
        /// <param name="vpnId"></param>
        public async Task ValidateDeleteAsync(int vpnId)
        {
            var vpn = (from vpns in await _unitOfWork.VpnRepository.GetAsync(
                x =>
                    x.VpnID == vpnId,
                    includeProperties: "ExtranetVpnMembers",
                    AsTrackable: false)
                       select vpns)
                       .Single();

            if (vpn.IsExtranet && vpn.ExtranetVpnMembers.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Extranet vpn '{vpn.Name}' cannot be deleted because member vpns are defined. "
                    + "Remove the member vpns from the extranet first.");
            }
        }
    }
}
