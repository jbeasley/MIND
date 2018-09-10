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
