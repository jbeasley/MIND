using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class AttachmentSetUpdateDirector : IAttachmentSetUpdateDirector
    {
        private readonly IAttachmentSetUpdateBuilder _builder;

        public AttachmentSetUpdateDirector(IAttachmentSetUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.AttachmentSet> UpdateAsync(int attachmentSetId, AttachmentSetUpdate update)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithAttachmentRedundancy(update.AttachmentRedundancy.ToString())
                                 .WithSubRegion(update.SubRegion)
                                 .WithMulticastVpnDomainType(update.MulticastVpnDomainType.ToString())
                                 .UpdateAsync();
        }
    }
}
