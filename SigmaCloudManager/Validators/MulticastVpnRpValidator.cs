using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Multicast VPN Rendezvous-Points.
    /// </summary>
    public class MulticastVpnRpValidator : BaseValidator, IMulticastVpnRpValidator
    {
        public MulticastVpnRpValidator(IMulticastVpnRpService multicastVpnRpService,
            IAttachmentSetService attachmentSetService,
            IMulticastVpnDomainTypeService multicastVpnDomainTypeService,
            IVpnService vpnService,
            IVpnTenantMulticastGroupService vpnTenantMulticastGroupService)
        {
            MulticastVpnRpService = multicastVpnRpService;
            AttachmentSetService = attachmentSetService;
            MulticastVpnDomainTypeService = multicastVpnDomainTypeService;
            VpnService = vpnService;
            VpnTenantMulticastGroupService = vpnTenantMulticastGroupService; 
        }

        private IMulticastVpnRpService MulticastVpnRpService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private IMulticastVpnDomainTypeService MulticastVpnDomainTypeService { get; }
        private IVpnService VpnService { get; }
        private IVpnTenantMulticastGroupService VpnTenantMulticastGroupService { get; }

        /// <summary>
        /// Validate a new Mulicast VPN RP
        /// </summary>
        /// <param name="multicastVpnRp"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(MulticastVpnRp multicastVpnRp)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(multicastVpnRp.AttachmentSetID);
            if (attachmentSet.MulticastVpnDomainTypeID == null)
            {
                ValidationDictionary.AddError(string.Empty, "A Rendezvous-Point cannot be configured because a Multicast Domain Type " 
                    + $"has not been configured for Attachment Set '{attachmentSet.Name}'.");
                return;
            }

            var multicastVpnDomainType = await MulticastVpnDomainTypeService.GetByIDAsync(attachmentSet.MulticastVpnDomainTypeID.Value);
            if (multicastVpnDomainType.Name == "Receiver Only")
            {
                ValidationDictionary.AddError(string.Empty, "A Rendezvous-Point cannot be configured because Attachment Set "
                    + $"'{attachmentSet.Name}' is configured as a Receiver-Only Multicast Domain.");
            }
        }

        /// <summary>
        /// Validate deletion of a Multicast RP. The RP cannot be deleted if 
        /// any Multicast Groups are bound to the RP.
        /// </summary>
        /// <param name="multicastVpnRp"></param>
        public async Task ValidateDeleteAsync(MulticastVpnRp multicastVpnRp)
        {
            var vpnTenantMulticastGroups = await VpnTenantMulticastGroupService.GetAllByMulticastVpnRpIDAsync(multicastVpnRp.MulticastVpnRpID);
            foreach (var vpnTenantMulticastGroup in vpnTenantMulticastGroups)
            {
                ValidationDictionary.AddError(string.Empty, $"Multicast RP '{multicastVpnRp.IpAddress}' "
                    + "cannot be deleted because Multicast Group Range "
                    + $"'{vpnTenantMulticastGroup.TenantMulticastGroup.GroupAddress}/{vpnTenantMulticastGroup.TenantMulticastGroup.GroupMask}' "
                    + "is bound to the RP.");
            }
        }
    }
}
