using Mind.Directors;
using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class AttachmentSetRoutingInstanceDirector : IAttachmentSetRoutingInstanceDirector, IDestroyable<AttachmentSetRoutingInstance>
    {
        private readonly Func<IAttachmentSetRoutingInstanceBuilder> _builderFactory;

        public AttachmentSetRoutingInstanceDirector(Func<IAttachmentSetRoutingInstanceBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<AttachmentSetRoutingInstance> BuildAsync(int attachmentSetId, RoutingInstanceForAttachmentSetRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachmentSet(attachmentSetId)
                                 .WithRoutingInstance(request.RoutingInstanceName)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithMulticastDesignatedRouterPreference(request.MulticastDesignatedRouterPreference)
                                 .BuildAsync();
        }

        public async Task<AttachmentSetRoutingInstance> BuildAsync(AttachmentSet attachmentSet, RoutingInstanceForAttachmentSetRequest request)
        {
            var builder = _builderFactory();
            return await builder.ForAttachmentSet(attachmentSet)
                                 .WithRoutingInstance(request.RoutingInstanceName)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithMulticastDesignatedRouterPreference(request.MulticastDesignatedRouterPreference)
                                 .BuildAsync();
        }

        public async Task<List<AttachmentSetRoutingInstance>> BuildAsync(AttachmentSet attachmentSet, List<RoutingInstanceForAttachmentSetRequest> requests)
        {
            var attachmentSetRoutingInstances = new List<SCM.Models.AttachmentSetRoutingInstance>();
            var tasks = requests.Select(
                            async request =>
                                attachmentSetRoutingInstances.Add(await BuildAsync(attachmentSet, request))
                            );

            await Task.WhenAll(tasks);
            return attachmentSetRoutingInstances;
        }

        public async Task DestroyAsync(AttachmentSetRoutingInstance item, bool cleanUpNetwork = false)
        {
            var builder = _builderFactory();
            await builder.ForAttachmentSetRoutingInstance(item.AttachmentSetRoutingInstanceID)
                         .DestroyAsync();
        }

        public async Task DestroyAsync(List<AttachmentSetRoutingInstance> items, bool cleanUpNetwork = false)
        {
            var tasks = items.Select(
                async attachmentSetRoutingInstance =>
                {
                    await DestroyAsync(attachmentSetRoutingInstance);
                });

            await Task.WhenAll(tasks);
        }
    }
}
