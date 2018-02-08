using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Attachments
    /// </summary>
    public class AttachmentValidator : BaseValidator, IAttachmentValidator
    {
        public AttachmentValidator(IAttachmentBandwidthService attachmentBandwidthService,
            ITenantAttachmentService tenantAttachmentService,
            IAttachmentService attachmentService,
            IAttachmentRoleService attachmentRoleService,
            IRoutingInstanceService routingInstanceService,
            IInterfaceService interfaceService,
            IPortService portService,
            IPortStatusService portStatusService,
            IContractBandwidthPoolService contractBandwidthPoolService,
            IContractBandwidthPoolValidator contractBandwidthPoolValidator,
            IVifValidator vifValidator)
        {
            AttachmentBandwidthService = attachmentBandwidthService;
            TenantAttachmentService = tenantAttachmentService;
            AttachmentService = attachmentService;
            AttachmentRoleService = attachmentRoleService;
            RoutingInstanceService = routingInstanceService;
            InterfaceService = interfaceService;
            PortService = portService;
            PortStatusService = portStatusService;
            ContractBandwidthPoolService = contractBandwidthPoolService;
            ContractBandwidthPoolValidator = contractBandwidthPoolValidator;
            VifValidator = vifValidator;
        }

        private IAttachmentBandwidthService AttachmentBandwidthService { get; }
        private ITenantAttachmentService TenantAttachmentService { get; }
        private IAttachmentService AttachmentService { get; }
        private IAttachmentRoleService AttachmentRoleService { get; }
        private IInterfaceService InterfaceService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IPortService PortService { get; }
        private IPortStatusService PortStatusService { get; }
        private IContractBandwidthPoolService ContractBandwidthPoolService { get; }
        private IContractBandwidthPoolValidator ContractBandwidthPoolValidator { get; }
        private IVifValidator VifValidator { get; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                VifValidator.ValidationDictionary = value;
                ContractBandwidthPoolValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate an Attachment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentRequest request)
        {

            var attachmentRole = await AttachmentRoleService.GetByIDAsync(request.AttachmentRoleID);
            if (request.IsLayer3 != attachmentRole.IsLayer3Role)
            {
                var str = attachmentRole.IsLayer3Role ? "enabled" : "disabled";
                ValidationDictionary.AddError(string.Empty, $"The selected Role requires that the 'Layer3' option is {str}.");
            }

            if (request.IsTagged != attachmentRole.IsTaggedRole)
            {
                var str = attachmentRole.IsTaggedRole ? "enabled" : "disabled";
                ValidationDictionary.AddError(string.Empty, $"The selected Role requires that the 'Tagged' option is {str}.");
            }
            
            var bandwidth = await AttachmentBandwidthService.GetByIDAsync(request.BandwidthID);

            if (request.BundleRequired)
            {
                if (!attachmentRole.SupportedByBundle)
                {
                    ValidationDictionary.AddError(string.Empty, $"The selected Role is not supported by a Bundle Attachment.");
                }
                if (!bandwidth.SupportedByBundle)
                {
                    ValidationDictionary.AddError("BandwidthID", $"The requested bandwidth ({bandwidth.BandwidthGbps} Gbps) "
                        + "is not supported by a bundle attachment. "
                        + "Uncheck the bundle option to request this bandwidth.");
                }
            }
            else if (request.MultiPortRequired)
            {
                if (!attachmentRole.SupportedByMultiPort)
                {
                    ValidationDictionary.AddError(string.Empty, $"The selected Role is not supported by a Multiport Attachment.");
                }
                if (!bandwidth.SupportedByMultiPort)
                {
                    ValidationDictionary.AddError("BandwidthID", $"The requested bandwidth ({bandwidth.BandwidthGbps} Gbps) "
                        + "is not supported by a multi-port attachment. "
                        + "Uncheck the multi-port option to request this bandwidth.");
                }
            }
            else
            {
                if (bandwidth.MustBeBundleOrMultiPort)
                {
                    if (bandwidth.SupportedByBundle && bandwidth.SupportedByMultiPort)
                    {
                        ValidationDictionary.AddError("BandwidthID", $"The requested bandwidth ({bandwidth.BandwidthGbps} Gbps) "
                            + "is ONLY supported by a Bundle or Multi-Port Attachment. ");
                    }
                    else if (bandwidth.SupportedByBundle)
                    {
                        ValidationDictionary.AddError("BandwidthID", $"The requested bandwidth ({bandwidth.BandwidthGbps} Gbps) "
                            + "is ONLY supported by a Bundle Attachment. ");
                    }
                    else if (bandwidth.SupportedByMultiPort)
                    {
                        ValidationDictionary.AddError("BandwidthID", $"The requested bandwidth ({bandwidth.BandwidthGbps} Gbps) "
                            + "is ONLY supported by a Multi-Port Attachment. ");
                    }
                }
            }

            if (request.IsLayer3)
            {
                if (request.MultiPortRequired)
                {
                    if (bandwidth.BandwidthGbps == 20)
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
                    else if (bandwidth.BandwidthGbps == 40)
                    {
                        if (string.IsNullOrEmpty(request.IpAddress1) || string.IsNullOrEmpty(request.IpAddress2)
                            || string.IsNullOrEmpty(request.IpAddress3) || string.IsNullOrEmpty(request.IpAddress4))
                        {
                            ValidationDictionary.AddError(string.Empty, "Four IP addresses must be entered.");
                        }

                        if (string.IsNullOrEmpty(request.SubnetMask1) || string.IsNullOrEmpty(request.SubnetMask2)
                            || string.IsNullOrEmpty(request.SubnetMask3) || string.IsNullOrEmpty(request.SubnetMask4))
                        {
                            ValidationDictionary.AddError(string.Empty, "Four subnet masks must be entered.");
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(request.IpAddress1))
                    {
                        ValidationDictionary.AddError("IpAddress1", "An IP address must be specified for layer 3 attachments.");
                    }

                    if (string.IsNullOrEmpty(request.SubnetMask1))
                    {
                        ValidationDictionary.AddError("SubnetMask1", "A subnet mask must be specified for layer 3 attachments.");
                    }
                }
            }

            if (attachmentRole.RequireContractBandwidth)
            {
                if (request.ContractBandwidthID == null)
                {
                    ValidationDictionary.AddError("ContractBandwidthID", "The selected role requires that a contract bandwidth option is selected.");
                }
                else
                {
                    request.Bandwidth = bandwidth;

                    // Validate the requested Contract Bandwidth Pool

                    await ContractBandwidthPoolValidator.ValidateNewAsync(request);
                }
            }
            else
            {
                if (request.ContractBandwidthID != null)
                {
                    ValidationDictionary.AddError("ContractBandwidthID", "The selected role requires that a contract bandwidth option must not be selected.");
                }
            }
        }

        /// <summary>
        /// Validate changes to an Attachment
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(AttachmentUpdate update)
        {
            var attachment = await AttachmentService.GetByIDAsync(update.AttachmentID);
            if (attachment.RoutingInstanceID != update.RoutingInstanceID || update.CreateNewRoutingInstance)
            {
                if (attachment.RoutingInstanceID == null && update.RoutingInstanceID != null)
                {
                    ValidationDictionary.AddError(string.Empty, $"Attachment '{attachment.Name}' cannot be configured with a VRF because the "
                        + $"the Attachment Role of '{attachment.AttachmentRole.Name}' does not allow VRFs.");
                }

                var routingInstance = await RoutingInstanceService.GetByIDAsync(attachment.RoutingInstanceID.Value);
                if (update.RoutingInstanceID != null)
                {
                    var updateRoutingInstance = await RoutingInstanceService.GetByIDAsync(update.RoutingInstanceID.Value);
                    if (updateRoutingInstance.RoutingInstanceTypeID != attachment.RoutingInstance.RoutingInstanceTypeID)
                    {
                        ValidationDictionary.AddError(string.Empty, "The VRF cannot be changed because the Routing Instance Types do not match."
                            + $"The current VRF Routing Instance Type is '{routingInstance.RoutingInstanceType.Name}'. "
                            + $"The updated VRF Routing Instance Type is '{updateRoutingInstance.RoutingInstanceType.Name}'.");
                    }
                }

                // Routing Instance cannot be changed if VPNs are mapped onto it

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

            if (attachment.IsBundle)
            {
                if (update.BundleMinLinks == null)
                {
                    ValidationDictionary.AddError(string.Empty, "Min Links must specified for Bundle Attachments");
                    return;
                }

                else
                {
                    var numPorts = attachment.Interfaces.SelectMany(x => x.Ports).Count();
                    if (update.BundleMinLinks > numPorts)
                    {
                        ValidationDictionary.AddError(string.Empty, "Min Links cannot exceed the number of ports in the Bundle. "
                            + $"The number of ports is {numPorts}.");
                    }
                    if (update.BundleMaxLinks != null)
                    {
                        if (update.BundleMaxLinks.Value > numPorts)
                        {
                            ValidationDictionary.AddError(string.Empty, "Max Links cannot exceed the number of ports in the Bundle. "
                                + $"The number of ports is {numPorts}.");
                        }
                        if (update.BundleMinLinks > update.BundleMaxLinks)
                        {
                            ValidationDictionary.AddError(string.Empty, "Min Links cannot exceed Max Links.");
                        }
                    }
                }
            }
            else {

                if (update.BundleMinLinks != null)
                {
                    ValidationDictionary.AddError(string.Empty, "Min Links can only be specified for Bundle Attachments");
                }

                if (update.BundleMaxLinks != null)
                {
                    ValidationDictionary.AddError(string.Empty, "Max Links can only be specified for Bundle Attachments");
                }
            }

            if (attachment.AttachmentRole.RequireContractBandwidth)
            {
                // Validate the requested Contract Bandwidth Pool

                await ContractBandwidthPoolValidator.ValidateChangesAsync(update);
            }
        }

        /// <summary>
        /// Validate port change associated with an Attachment
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(AttachmentPortUpdate update)
        {
            var attachment = await AttachmentService.GetByIDAsync(update.AttachmentID);

            if (update.CurrentPortID != update.PortID)
            {
                var routingInstanceIDs = new List<int>();
                if (attachment.IsTagged)
                {
                    routingInstanceIDs = attachment.Vifs.Select(x => x.RoutingInstanceID.Value).ToList();
                }
                else if (attachment.RoutingInstanceID != null) 
                {
                    routingInstanceIDs = Enumerable.Range(attachment.RoutingInstanceID.Value, 1).ToList();
                }

                foreach (var routingInstanceID in routingInstanceIDs)
                {
                    var routingInstance = await RoutingInstanceService.GetByIDAsync(routingInstanceID);
                    var vpnAttachmentSets = routingInstance.AttachmentSetRoutingInstances.SelectMany(x => x.AttachmentSet.VpnAttachmentSets);

                    if (vpnAttachmentSets.Any())
                    {
                        foreach (var vpnAttachmentSet in vpnAttachmentSets)
                        {
                            ValidationDictionary.AddError(string.Empty, "The Port cannot be changed because it belongs to "
                                + $"VRF '{routingInstance.Name}' in Attachment Set '{vpnAttachmentSet.AttachmentSet.Name}' which is bound to VPN "
                                + $"'{vpnAttachmentSet.Vpn.Name}'.");
                        }
                    }
                }

                // The port status of the new port must be 'Free'

                var newPort = await PortService.GetByIDAsync(update.PortID);
                var newPortStatus = await PortStatusService.GetByIDAsync(newPort.PortStatusID);
                if (newPortStatus.PortStatusType != PortStatusType.Free)
                {
                    ValidationDictionary.AddError(string.Empty, $"Port '{newPort.FullName}' cannot be selected because the status of the port is not 'Free'.");
                }

                // The new port must support the same Port Bandwidth as the current port

                var currentPort = await PortService.GetByIDAsync(update.CurrentPortID);

                if (newPort.PortBandwidthID != currentPort.PortBandwidthID)
                {
                    ValidationDictionary.AddError(string.Empty, $"Port '{newPort.FullName}' does not support the same Port Bandwidth "
                        + $"({newPort.PortBandwidth.BandwidthGbps} Gbps) "
                        + $"as the current Port '{currentPort.FullName}' ({currentPort.PortBandwidth.BandwidthGbps} Gbps).");
                }

                // The new port must belong to the same Port Pool
                // as the current port.

                if (newPort.PortPoolID != currentPort.PortPoolID)
                {
                    ValidationDictionary.AddError(string.Empty, $"Port '{newPort.FullName}' does not belong to the same Port Pool "
                        + $"({newPort.PortPool.Name}) "
                        + $"as the current Port '{currentPort.FullName}' ({currentPort.PortPool.Name}).");
                }
            }
        }

        /// <summary>
        /// Validate changes to an Interface associated with an Attachment.
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(Interface update)
        {
            var currentInterface = await InterfaceService.GetByIDAsync(update.InterfaceID);
            if (currentInterface.Attachment.IsLayer3)
            {
                if (update.IpAddress == null)
                {
                    ValidationDictionary.AddError(string.Empty, "An IP address must be specified for a layer 3 Attachment.");
                }
                if (update.SubnetMask == null)
                {
                    ValidationDictionary.AddError(string.Empty, "An subnet mask must be specified for a layer 3 Attachment.");
                }
            }
            else
            {
                if (update.IpAddress != null)
                {
                    ValidationDictionary.AddError(string.Empty, "An IP address cannot be specified for a layer 2 Attachment.");
                }
                if (update.SubnetMask != null)
                {
                    ValidationDictionary.AddError(string.Empty, "A subnet mask cannot be specified for a layer 2 Attachment.");
                }
            }
        }

        /// <summary>
        /// Validate deletion of an Attachment
        /// </summary>
        /// <param name="attachment"></param>
        public void ValidateDelete(Attachment attachment)
        {
            if (attachment.RoutingInstance != null)
            {
                var attachmentSetRoutingInstances = attachment.RoutingInstance.AttachmentSetRoutingInstances;
                if (attachmentSetRoutingInstances.Count > 0)
                {
                    attachmentSetRoutingInstances.ToList().ForEach(x => ValidationDictionary.AddError(string.Empty, "The Attachment is a member "
                        + $"of Attachment Set '{x.AttachmentSet.Name}' and cannot be deleted."));
                }
            }

            // Validate each Vif associated with the Attachment can be deleted

            if (attachment.Vifs.Any())
            {
                foreach (var vif in attachment.Vifs)
                {
                    VifValidator.ValidateDelete(vif);
                }
            }
        }

        /// <summary>
        /// Validates if a VPN can be synchronised with the network. The VPN can be
        /// synchronised only if all Attachments which are bound to the VPN are already
        /// synchronised.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateRequiresNetworkSyncAsync(Vpn vpn)
        {
            var attachments = await TenantAttachmentService.GetAllByVpnIDAsync(vpn.VpnID);
            var attachmentsRequireSync = attachments.Where(q => q.RequiresSync).ToList();

            if (attachmentsRequireSync.Count() > 0)
            {
                attachmentsRequireSync.ForEach(a => ValidationDictionary.AddError(string.Empty, $"'{a.Name}' on Device '{a.Device.Name}' " +
                    $"for Tenant '{a.Tenant.Name}' requires synchronisation with the network."));
            }
        }

        /// <summary>
        /// Validates that the ports of a an Attachment are configured correctly.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task ValidateAttachmentPortsConfiguredCorrectlyAsync(Attachment attachment)
        {
            var ports = await PortService.GetAllByAttachmentIDAsync(attachment.AttachmentID);
            var totalPortBandwidthGbps = (ports.Where(x => x.DeviceID == attachment.DeviceID).Sum(x => x.PortBandwidth.BandwidthGbps));

            if (totalPortBandwidthGbps < attachment.AttachmentBandwidth.BandwidthGbps)
            {
                ValidationDictionary.AddError(string.Empty, $"Attachment '{attachment.Name}' is misconfigured. The total Port Bandwidth "
                    + $"({totalPortBandwidthGbps} Gbps) is less than the required Attachment Bandwidth ({attachment.AttachmentBandwidth.BandwidthGbps} Gbps). "
                    + "Check that the correct number of Ports are configured correctly and that all of the Ports are configured for the same Device.");
            }
        }
    }
}
