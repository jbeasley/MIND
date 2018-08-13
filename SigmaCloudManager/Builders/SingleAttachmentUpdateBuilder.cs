using SCM.Data;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class SingleAttachmentUpdateBuilder : AttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>, IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>
    {
        public SingleAttachmentUpdateBuilder(IUnitOfWork unitOfWork, Func<RoutingInstanceType, IRoutingInstanceBuilder> routingInstanceBuilderFactory) : 
            base(unitOfWork, routingInstanceBuilderFactory)
        {
            base._builder = this;
        }
    }
}
