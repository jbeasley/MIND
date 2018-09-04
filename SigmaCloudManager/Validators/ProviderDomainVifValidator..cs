using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Validators
{
    public class ProviderDomainVifValidator : BaseValidator, IProviderDomainVifValidator
    {
        public ProviderDomainVifValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validates changes to a vif
        /// </summary>
        /// <param name="vifId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(int vifId, ProviderDomainVifUpdate update)
        {
            var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                    q =>
                        q.VifID == vifId,
                        includeProperties: "VifRole," +
                        "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet",
                        AsTrackable: false)
                        select result)
                        .Single();

            if (!string.IsNullOrEmpty(update.ExistingRoutingInstanceName))
            {
                if (update.ExistingRoutingInstanceName != vif.RoutingInstance.Name)
                {
                    var existingRoutingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                        x => 
                            x.Name == update.ExistingRoutingInstanceName)
                                                   select routingInstances)
                                                   .SingleOrDefault();

                    if (existingRoutingInstance == null)
                    {

                        ValidationDictionary.AddError(nameof(update.ExistingRoutingInstanceName),
                        $"Routing instance '{update.ExistingRoutingInstanceName}' was not found.");
                    }

                    else if (vif.RoutingInstance.RoutingInstanceTypeID != existingRoutingInstance.RoutingInstanceTypeID)
                    {
                        ValidationDictionary.AddError(string.Empty, "The routing instance cannot be changed because the routing instance type of the " +
                            "specified routing instance is different to the routing instance type of the current routing instance. "
                            + $"The current routing instance type is '{vif.RoutingInstance.RoutingInstanceType.Type.ToString()}'. "
                            + $"The updated routing instance type is '{existingRoutingInstance.RoutingInstanceType.Type.ToString()}'.");
                    }
                }
            }

            if (vif.RoutingInstance != null)
            {
                // Routing Instance cannot be changed if it belongs to an attachment set
                var attachmentSets = vif.RoutingInstance.AttachmentSetRoutingInstances.Select(x => x.AttachmentSet);
                {
                    (from attachmentSet in attachmentSets
                     select attachmentSet)
                    .ToList()
                    .ForEach(x => ValidationDictionary.AddError(string.Empty, "The routing instance cannot be changed because it belongs to "
                            + $"attachment set '{x.Name}'. Remove the routing instance from the attachment set first."));
                }
            }

            if (vif.VifRole.RequireContractBandwidth)
            {
                if (update.ContractBandwidthMbps == null && string.IsNullOrEmpty(update.ExistingContractBandwidthPoolName))
                {
                    ValidationDictionary.AddError(string.Empty, "Either an existing Contract Bandwidth Pool which is asociated " +
                        "with another vif under the same attachment as the current vif, or a Contract Bandwidth value, must be specified.");
                }
            }
        }

        /// <summary>
        /// Validate deletion of a vif
        /// </summary>
        /// <param name="vifId"></param>
        public async Task ValidateDeleteAsync(int vifId)
        {
            (from result in await _unitOfWork.VifRepository.GetAsync(
                    q =>
                        q.VifID == vifId,
                        includeProperties: "VifRole,RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet",
                        AsTrackable: false)
                        select result)
                       .Single()
                       .RoutingInstance?.AttachmentSetRoutingInstances
                       .ToList()
                       .ForEach(x => ValidationDictionary.AddError(string.Empty, $"The vif cannot be deleted because it belongs to routing instance " +
                       $"'{x.RoutingInstance.Name}' which belongs to attachment set '{x.AttachmentSet.Name}. Remove the routing instance from the " +
                       $"attachment set first."));
        }
    }
}
