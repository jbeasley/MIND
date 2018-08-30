using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models.RequestModels;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Vifs
    /// </summary>
    public class VifValidator : BaseValidator, IVifValidator
    {
        public VifValidator(IContractBandwidthPoolValidator contractBandwidthPoolValidator,
            IAttachmentService attachmentService,
            IVifService vifService,
            IRoutingInstanceService routingInstanceService,
            IVifRoleService vifRoleService)
        {
            AttachmentService = attachmentService;
            VifService = vifService;
            VifRoleService = vifRoleService;
            RoutingInstanceService = routingInstanceService;
            ContractBandwidthPoolValidator = contractBandwidthPoolValidator;
        }

        private IAttachmentService AttachmentService { get; }
        private IVifService VifService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IContractBandwidthPoolValidator ContractBandwidthPoolValidator { get; }
        private IVifRoleService VifRoleService { get; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                ContractBandwidthPoolValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validates a request to create a new Vif
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VifRequest request)
        {

            var attachment = await AttachmentService.GetByIDAsync(request.AttachmentID);

            if (!attachment.IsTagged)
            {
                ValidationDictionary.AddError(string.Empty, "A Vif cannot be created for an untagged Attachment.");
            }

            var vifRole = await VifRoleService.GetByIDAsync(request.VifRoleID);
            if (request.IsLayer3 != vifRole.IsLayer3Role)
            {
                var str = vifRole.IsLayer3Role ? "enabled" : "disabled";
                ValidationDictionary.AddError(string.Empty, $"The selected Role requires that the 'Layer3' option is {str}.");
            }

            if (request.AttachmentIsMultiPort && request.IsLayer3)
            {
                if (attachment.AttachmentBandwidth.BandwidthGbps == 20)
                {
                    if (string.IsNullOrEmpty(request.IpAddress1) || string.IsNullOrEmpty(request.IpAddress2))
                    {
                        ValidationDictionary.AddError(string.Empty, "Two IP addresses must be specified.");
                    }

                    if (string.IsNullOrEmpty(request.SubnetMask1) || string.IsNullOrEmpty(request.SubnetMask2))
                    {
                        ValidationDictionary.AddError(string.Empty, "Two subnet masks must be specified.");
                    }
                }
                else if (attachment.AttachmentBandwidth.BandwidthGbps == 40)
                {
                    if (string.IsNullOrEmpty(request.IpAddress1) || string.IsNullOrEmpty(request.IpAddress2)
                        || string.IsNullOrEmpty(request.IpAddress3) || string.IsNullOrEmpty(request.IpAddress4))
                    {
                        ValidationDictionary.AddError(string.Empty, "Four IP addresses must be specified.");
                    }

                    if (string.IsNullOrEmpty(request.SubnetMask1) || string.IsNullOrEmpty(request.SubnetMask2)
                        || string.IsNullOrEmpty(request.SubnetMask3) || string.IsNullOrEmpty(request.SubnetMask4))
                    {
                        ValidationDictionary.AddError(string.Empty, "Four subnet masks must be specified.");
                    }
                }
            }

            if (vifRole.RequireContractBandwidth)
            {
                if (request.ContractBandwidthID == null && request.ContractBandwidthPoolID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "The selected role requires that either an existing Contract Bandwidth Pool or "
                        + "a Contract Bandwidth value must be selected.");
                }
                else
                {
                    // Validate the requested Contract Bandwidth Pool

                    await ContractBandwidthPoolValidator.ValidateNewAsync(request);
                }
            }
        }

        /// <summary>
        /// Validates changes to a Vif
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(VifUpdate update)
        {
            var vif = await VifService.GetByIDAsync(update.VifID);
            if (vif.RoutingInstanceID != update.RoutingInstanceID || update.CreateNewRoutingInstance)
            {
                if (vif.RoutingInstanceID == null && update.RoutingInstanceID != null)
                {
                    ValidationDictionary.AddError(string.Empty, $"VIF '{vif.Name}' cannot be configured with a VRF because the "
                        + $"the VIF Role '{vif.VifRole.Name}' does not allow VRFs.");
                }

                var routingInstance = await RoutingInstanceService.GetByIDAsync(vif.RoutingInstanceID.Value);
                if (update.RoutingInstanceID != null)
                {
                    var updateRoutingInstance = await RoutingInstanceService.GetByIDAsync(update.RoutingInstanceID.Value);
                    if (updateRoutingInstance.RoutingInstanceTypeID != vif.RoutingInstance.RoutingInstanceTypeID)
                    {
                        ValidationDictionary.AddError(string.Empty, "The VRF cannot be changed because the Routing Instance Types do not match."
                            + $"The current VRF Routing Instance Type is '{routingInstance.RoutingInstanceType.Type.ToString()}'. "
                            + $"The updated VRF Routing Instance Type is '{updateRoutingInstance.RoutingInstanceType.Type.ToString()}'.");
                    }
                }

                // VRF cannot be changed if VPNs are mapped onto it

                var vpnAttachmentSets = routingInstance.AttachmentSetRoutingInstances.SelectMany(x => x.AttachmentSet.VpnAttachmentSets);
                {
                    if (vpnAttachmentSets.Any())
                    {
                        foreach (var vpnAttachmentSet in vpnAttachmentSets)
                        {
                            ValidationDictionary.AddError(string.Empty, "The VRF cannot be changed because it belongs to "
                                + $"Attachment Set '{vpnAttachmentSet.AttachmentSet.Name}' which is bound to VPN "
                                + $"'{vpnAttachmentSet.Vpn.Name}'.");
                        }

                        return;
                    }
                }
            }

            if (vif.VifRole.RequireContractBandwidth)
            {
                if (update.ContractBandwidthID == null && update.ContractBandwidthPoolID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "The VIF role requires that either an existing Contract Bandwidth Pool or "
                        +"a Contract Bandwidth value must be selected.");
                }
                else
                {
                    // Validate the Contract Bandwidth Pool

                    await ContractBandwidthPoolValidator.ValidateChangesAsync(update);
                }
            }
        }
    
        /// <summary>
        /// Validate deletion of a Vif
        /// </summary>
        /// <param name="vif"></param>
        public void ValidateDelete(Vif vif)
        {
            if (vif.RoutingInstance != null)
            {
                var attachmentSetRoutingInstances = vif.RoutingInstance.AttachmentSetRoutingInstances;
                if (attachmentSetRoutingInstances != null)
                {
                    if (attachmentSetRoutingInstances.Any())
                    {
                        attachmentSetRoutingInstances.ToList().ForEach(x => ValidationDictionary.AddError(string.Empty, $"Vif '{vif.Name}' is a member "
                            + $"of Attachment Set '{x.AttachmentSet.Name}' and cannot be deleted."));
                    }
                }
            }
        }

        /// <summary>
        /// Validates if a VPN can be synchronised with the network. The VPN can be
        /// synchronised only if all Vifs which are bound to the VPN are already
        /// synchronised.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateRequiresNetworkSyncAsync(Vpn vpn)
        {
            var vifs = await VifService.GetAllByVpnIDAsync(vpn.VpnID);
            var vifsRequireSync = vifs.Where(q => q.RequiresSync).ToList();

            if (vifsRequireSync.Count() > 0)
            {
                vifsRequireSync.ForEach(a => ValidationDictionary.AddError(string.Empty, $"'{a.Name}' on Device '{a.Attachment.Device.Name}' "
                    + $"for tenant '{a.Tenant.Name}' "
                    + "requires synchronisation with the network."));
            }
        }
    } 
}
