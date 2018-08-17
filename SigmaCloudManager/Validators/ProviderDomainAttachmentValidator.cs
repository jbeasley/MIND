using SCM.Data;
using SCM.Models.RequestModels;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Validators
{
    public class ProviderDomainAttachmentValidator : BaseValidator, IProviderDomainAttachmentValidator
    {
        public ProviderDomainAttachmentValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate deletion of a provider domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        public async Task ValidateDeleteAsync(int attachmentId)
        {
            var attachment = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == attachmentId 
                              && q.AttachmentRole.PortPool.PortRole.PortRoleType == SCM.Models.PortRoleType.TenantFacing, 
                includeProperties: "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet," +
                "Vifs.RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet", AsTrackable: false)
                select attachments)
                .Single();
       
            if (attachment.RoutingInstance != null)
            {
                var attachmentSetRoutingInstances = attachment.RoutingInstance.AttachmentSetRoutingInstances;
                if (attachmentSetRoutingInstances.Any())
                {
                    attachmentSetRoutingInstances.ToList().ForEach(x => ValidationDictionary.AddError(string.Empty, "The attachment is a member "
                        + $"of attachment set '{x.AttachmentSet.Name}' and cannot be deleted."));
                }
            }

            // Validate each Vif associated with the Attachment can be deleted

            if (attachment.Vifs.Any())
            {
                foreach (var vif in attachment.Vifs)
                {
                    var attachmentSetRoutingInstances = attachment.Vifs.SelectMany(x => x.RoutingInstance.AttachmentSetRoutingInstances);
                    if (attachmentSetRoutingInstances.Any())
                    {
                        attachmentSetRoutingInstances.ToList().ForEach(x => ValidationDictionary.AddError(string.Empty, $"Vif '{vif.Name}' is a member "
                            + $"of attachment set '{x.AttachmentSet.Name}' and cannot be deleted."));
                    }
                }
            }
        }

        /// <summary>
        /// Validate changes to a provider domain attachment
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(int attachmentId, ProviderDomainAttachmentUpdate update)
        {
            var attachment = (from attachments in await _unitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == attachmentId
                             && q.AttachmentRole.PortPool.PortRole.PortRoleType == SCM.Models.PortRoleType.TenantFacing,
               includeProperties: "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.VpnAttachmentSets.Vpn," +
               "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.VpnAttachmentSets.AttachmentSet," +
               "Vifs.RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet", AsTrackable: false)
                              select attachments)
               .Single();

            if (!string.IsNullOrEmpty(update.ExistingRoutingInstanceName))
            {
                if (update.ExistingRoutingInstanceName != attachment.RoutingInstance.Name)
                {
                    var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(x => x.Name == update.ExistingRoutingInstanceName)
                                                   select routingInstances)
                                                   .SingleOrDefault();

                    if (existingRoutingInstance == null)
                    {

                        ValidationDictionary.AddError(nameof(update.ExistingRoutingInstanceName),
                        $"Routing instance '{update.ExistingRoutingInstanceName}' was not found.");
                    }

                    else if (attachment.RoutingInstance.RoutingInstanceTypeID != existingRoutingInstance.RoutingInstanceTypeID)
                    {
                        ValidationDictionary.AddError(string.Empty, "The routing instance cannot be changed because the routing instance type of the " +
                            "specified routing instance is different to the routing instance type of the current routing instance. "
                            + $"The current routing instance type is '{attachment.RoutingInstance.RoutingInstanceType.Name}'. "
                            + $"The updated routing instance type is '{existingRoutingInstance.RoutingInstanceType.Name}'.");
                    }
                }
            }

            if (attachment.RoutingInstance != null)
            {
                // Routing Instance cannot be changed if it belongs to an attachment set
                var attachmentSets = attachment.RoutingInstance.AttachmentSetRoutingInstances.Select(x => x.AttachmentSet);
                {
                    (from attachmentSet in attachmentSets
                     select attachmentSet)
                    .ToList()
                    .ForEach(x => ValidationDictionary.AddError(string.Empty, "The routing instance cannot be changed because it belongs to "
                            + $"attachment set '{x.Name}'"));
                }
            }
        }
    }
}
