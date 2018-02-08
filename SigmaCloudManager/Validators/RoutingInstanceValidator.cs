using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for RoutingInstances
    /// </summary>
    public class RoutingInstanceValidator : BaseValidator, IRoutingInstanceValidator
    {
        public RoutingInstanceValidator(IUnitOfWork unitOfWork, IRoutingInstanceService routingInstanceService)
        {
            UnitOfWork = unitOfWork;
            RoutingInstanceService = routingInstanceService;
        }

        private IUnitOfWork UnitOfWork { get; set; }
        private IRoutingInstanceService RoutingInstanceService { get; set; }

        /// <summary>
        /// Validate changes to a RoutingInstance.
        /// </summary>
        /// <param name="routingInstance"></param>
        public async Task ValidateChangesAsync(RoutingInstanceUpdate routingInstanceUpdate)
        {
            var routingInstance = await RoutingInstanceService.GetByIDAsync(routingInstanceUpdate.RoutingInstanceID);

            // Default Routing Instance cannot be changed

            if (routingInstance.RoutingInstanceType.IsDefault)
            {
                ValidationDictionary.AddError(string.Empty, "The default routing instance cannot be changed.");
                return;
            }

            if (routingInstance.RoutingInstanceType.IsVrf)
            {
                if (routingInstanceUpdate.AdministratorSubField == null)
                {
                    ValidationDictionary.AddError(string.Empty, "An Administrator Subfield value must be specified for VRF routing instance types.");
                }
                if (routingInstanceUpdate.AssignedNumberSubField == null)
                {
                    ValidationDictionary.AddError(string.Empty, "An Assigned Number Subfield value must be specified for VRF routing instance types.");
                }
            }

            // Quit early if the request is not valid because further processing depends on certain values being present
            // such as valid route distinguisher values in the update model

            if (!ValidationDictionary.IsValid)
            {
                return;
            }

            // Routing Instance cannot be changed if VPNs are mapped onto it

            var vpnAttachmentSets = routingInstance.AttachmentSetRoutingInstances.SelectMany(x => x.AttachmentSet.VpnAttachmentSets);

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

            // Check for name change

            if (routingInstance.Name != routingInstanceUpdate.Name)
            {
                var existingRoutingInstances = await RoutingInstanceService.GetAllByDeviceIDAsync(routingInstance.DeviceID);
                if (existingRoutingInstances.Any(x => x.Name.ToUpper() == routingInstanceUpdate.Name.ToUpper() 
                    && x.RoutingInstanceID != routingInstanceUpdate.RoutingInstanceID))
                {
                    ValidationDictionary.AddError(string.Empty, $"A VRF with name "
                       + $"'{routingInstanceUpdate.Name}' already exists for Device '{routingInstance.Device.Name}'.");
                }
            }

            // Check for RD change

            if (routingInstance.AdministratorSubField != routingInstanceUpdate.AdministratorSubField)
            {
                // Check RD range exists

                var rdRange = await UnitOfWork.RouteDistinguisherRangeRepository.GetAsync(q => q.AdministratorSubField == routingInstanceUpdate.AdministratorSubField);
                if (!rdRange.Any())
                {
                    ValidationDictionary.AddError(string.Empty, $"A Route Distinguisher range for administrator sub-field "
                        + $"'{routingInstanceUpdate.AdministratorSubField}' was not found.");
                }
            }

            // Check for duplicate RD

            var duplicateRDs = await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.AdministratorSubField == routingInstanceUpdate.AdministratorSubField
            && q.AssignedNumberSubField == routingInstanceUpdate.AssignedNumberSubField, includeProperties: "Tenant", AsTrackable: false);

            // Check if any duplicate RDs are assigned which are not associated with any Tenant. If so then the requested
            // RD values cannot be used because the values are used for infrastructure purposes

            if (duplicateRDs.Any(x => x.TenantID == null && x.RoutingInstanceID != routingInstanceUpdate.RoutingInstanceID))
            {
                ValidationDictionary.AddError(string.Empty, $"Route Distinguisher "
                    + $"'{routingInstanceUpdate.AdministratorSubField}:{routingInstanceUpdate.AssignedNumberSubField}' is already in-use "
                    + "and assigned for infrastructure purposes.");

                return;
            }

            // Check if the RD values are already in use and associated with another Tenant

            if (duplicateRDs.Any(x => x.TenantID != routingInstance.TenantID))
            {
                ValidationDictionary.AddError(string.Empty, $"Route Distinguisher "
                   + $"'{routingInstanceUpdate.AdministratorSubField}:{routingInstanceUpdate.AssignedNumberSubField}' is already in-use and assigned to "
                   + $"Tenant '{duplicateRDs.First().Tenant.Name}'.");
            }

            // Check if the RD values are already in use on the same device

            if (duplicateRDs.Any(x => x.DeviceID == routingInstance.DeviceID && x.RoutingInstanceID != routingInstance.RoutingInstanceID))
            {
                ValidationDictionary.AddError(string.Empty, $"Route Distinguisher "
                   + $"'{routingInstanceUpdate.AdministratorSubField}:{routingInstanceUpdate.AssignedNumberSubField}' is already in-use "
                   + $"for Device '{routingInstance.Device.Name}'.");
            }
        }
    }
}
