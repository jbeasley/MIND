using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Microsoft.EntityFrameworkCore;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating attachments. The builder exposes a fluent API.
    /// </summary>
    public class AttachmentSetUpdateBuilder : AttachmentSetBuilder, IAttachmentSetUpdateBuilder
    {
        public AttachmentSetUpdateBuilder(IUnitOfWork unitOfWork, IAttachmentSetRoutingInstanceDirector attachmentSetRoutingInstanceDirector) : 
            base(unitOfWork, attachmentSetRoutingInstanceDirector)
        {
        }

        public IAttachmentSetUpdateBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId.HasValue) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        IAttachmentSetUpdateBuilder IAttachmentSetUpdateBuilder.WithAttachmentRedundancy(string attachmentRedundancy)
        {
            base.WithAttachmentRedundancy(attachmentRedundancy);
            return this;
        }

        IAttachmentSetUpdateBuilder IAttachmentSetUpdateBuilder.WithMulticastVpnDomainType(string multicastVpnDomainTypeName)
        {
            base.WithMulticastVpnDomainType(multicastVpnDomainTypeName);
            return this;
        }

        IAttachmentSetUpdateBuilder IAttachmentSetUpdateBuilder.WithSubRegion(string subregion)
        {
            base.WithSubRegion(subregion);
            return this;
        }

        public async Task<AttachmentSet> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet))) await SetAttachmentSetAsync();
            if (_args.ContainsKey(nameof(WithMulticastVpnDomainType))) await base.SetMulticastVpnDomainTypeAsync();
            if (_args.ContainsKey(nameof(WithSubRegion))) await base.SetSubRegionAsync();
            if (_args.ContainsKey(nameof(WithAttachmentRedundancy))) await base.SetAttachmentRedundancyAsync();

            _attachmentSet.Validate();
            return _attachmentSet;
        }

        private async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                        x =>
                            x.AttachmentSetID == attachmentSetId,
                            AsTrackable: true,
                            query: q => q.IncludeValidationProperties())
                            select result)
                            .SingleOrDefault();

            base._attachmentSet = attachmentSet ?? throw new BuilderBadArgumentsException($"The attachment set with ID '{attachmentSetId}' was not found.");
               
        }
    }
}
