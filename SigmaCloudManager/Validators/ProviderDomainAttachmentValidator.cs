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
                              && q.AttachmentRole.PortPool.PortRole.PortRoleType == SCM.Models.PortRoleTypeEnum.TenantFacing,
                includeProperties: "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet," +
                "Vifs.RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet," +
                "Vifs.Attachment.Interfaces.Ports", AsTrackable: false)
                              select attachments)
                .Single();

            if (attachment.RoutingInstance != null)
            {
                (from result in attachment.RoutingInstance.AttachmentSetRoutingInstances
                 select result)
                 .ToList()
                 .ForEach(
                    x =>
                        ValidationDictionary.AddError(string.Empty, $"The attachment cannot be deleted because it belongs to routing instance" +
                        $" '{x.RoutingInstance.Name}' which is a member of attachment set '{x.AttachmentSet.Name}'. Remove the routing instanc from " +
                        "the attachment set first.")
                 );
            }

            // Validate each Vif associated with the Attachment can be deleted
            foreach (var vif in attachment.Vifs)
            {
                (from attachmentSetRoutingInstances in vif.RoutingInstance?.AttachmentSetRoutingInstances
                 select attachmentSetRoutingInstances)
                        .Where(x => x != null)
                        .ToList()
                        .ForEach(
                            x =>
                                ValidationDictionary.AddError(string.Empty, $"Vif '{vif.Name}' cannot be deleted because it belong to routing instance" +
                                $" '{x.RoutingInstance.Name}' which is a member of attachment set '{x.AttachmentSet.Name}'. " +
                                $"Remove the routing instance from the attachment set first.")
                        );
            }
        }
    }
}
