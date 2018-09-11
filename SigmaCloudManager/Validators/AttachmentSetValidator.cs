using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Attachment Sets
    /// </summary>
    public class AttachmentSetValidator : BaseValidator, IAttachmentSetValidator
    {
        public AttachmentSetValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate that an attachment set can be deleted.
        /// </summary>
        /// <param name="attachmentSetId"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(int attachmentSetId)
        {
            var vpnAttachmentSets = (from attachmentSets in await _unitOfWork.VpnAttachmentSetRepository.GetAsync(q =>
                                     q.AttachmentSet.AttachmentSetID == attachmentSetId)
                                     select attachmentSets)
                                     .ToList();

            if (vpnAttachmentSets.Any()) vpnAttachmentSets.ForEach(q =>
            ValidationDictionary.AddError(string.Empty, $"First remove the Attachment Set from VPN '{q.Vpn.Name}'"));
        }
    }
}
