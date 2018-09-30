using Microsoft.EntityFrameworkCore;
using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Bulder for attachment set routing instances. The builder exposes a fluent API.
    /// </summary>
    public class AttachmentSetRoutingInstanceBuilder : BaseBuilder, IAttachmentSetRoutingInstanceBuilder
    {
        private AttachmentSetRoutingInstance _attachmentSetRoutingInstance;

        public AttachmentSetRoutingInstanceBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _attachmentSetRoutingInstance = new AttachmentSetRoutingInstance();
        }

        public virtual IAttachmentSetRoutingInstanceBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId.HasValue) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual IAttachmentSetRoutingInstanceBuilder ForAttachmentSet(AttachmentSet attachmentSet)
        {
            if (attachmentSet != null) _args.Add(nameof(ForAttachmentSet), attachmentSet);
            return this;
        }

        public virtual IAttachmentSetRoutingInstanceBuilder WithRoutingInstance(string routingInstanceName)
        {
            if (!string.IsNullOrEmpty(routingInstanceName)) _args.Add(nameof(WithRoutingInstance), routingInstanceName);
            return this;
        }
       
        public virtual IAttachmentSetRoutingInstanceBuilder WithAdvertisedIpRoutingPreference(int? advertisedIpRoutingPreference)
        {
            if (advertisedIpRoutingPreference != null) _args.Add(nameof(WithAdvertisedIpRoutingPreference), advertisedIpRoutingPreference);
            return this;
        }

        public virtual IAttachmentSetRoutingInstanceBuilder WithMulticastDesignatedRouterPreference(int? multicastDesignatedRouterPreference)
        {
            if (multicastDesignatedRouterPreference != null) _args.Add(nameof(WithMulticastDesignatedRouterPreference), multicastDesignatedRouterPreference);
            return this;
        }

        public virtual IAttachmentSetRoutingInstanceBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference)
        {
            if (localIpRoutingPreference != null) _args.Add(nameof(WithLocalIpRoutingPreference), localIpRoutingPreference);
            return this;
        }

        public virtual async Task<AttachmentSetRoutingInstance> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                var arg = _args[nameof(ForAttachmentSet)];
                if (arg.GetType() == typeof(AttachmentSet))
                {
                    _attachmentSetRoutingInstance.AttachmentSet = (arg as AttachmentSet);
                }
                else
                {
                    await SetAttachmentSetAsync();
                }
            }
            if (_args.ContainsKey(nameof(WithRoutingInstance))) await SetRoutingInstanceAsync();
            if (_args.ContainsKey(nameof(WithAdvertisedIpRoutingPreference))) SetAdvertisedIpRoutingPreference();
            if (_args.ContainsKey(nameof(WithLocalIpRoutingPreference))) SetLocalIpRoutingPreference();
            if (_args.ContainsKey(nameof(WithMulticastDesignatedRouterPreference))) SetMulticastDesignatedRouterPreference();

            _attachmentSetRoutingInstance.Validate();
            return _attachmentSetRoutingInstance;
        }

        protected internal virtual async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                            q => 
                                 q.AttachmentSetID == attachmentSetId, 
                                 AsTrackable: true)
                                 select result)
                                 .SingleOrDefault();

            _attachmentSetRoutingInstance.AttachmentSet = attachmentSet;
        }

        protected internal virtual async Task SetRoutingInstanceAsync()
        {
            var routingInstanceName = _args[nameof(WithRoutingInstance)].ToString();
            var attachmentSet = _attachmentSetRoutingInstance.AttachmentSet;

            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(q =>
                                  q.Name == routingInstanceName
                                  && q.RoutingInstanceType.IsTenantFacingVrf,
                                  query: q => q.IncludeValidationProperties(),
                                  AsTrackable: true)
                                   select result)
                                  .SingleOrDefault();

            _attachmentSetRoutingInstance.RoutingInstance = routingInstance;
        }

        protected internal virtual void SetLocalIpRoutingPreference()
        {
            _attachmentSetRoutingInstance.LocalIpRoutingPreference = (int)_args[nameof(WithLocalIpRoutingPreference)];
        }

        protected internal virtual void SetAdvertisedIpRoutingPreference()
        {
            _attachmentSetRoutingInstance.AdvertisedIpRoutingPreference = (int)_args[nameof(WithAdvertisedIpRoutingPreference)];
        }

        protected internal virtual void SetMulticastDesignatedRouterPreference()
        {
            _attachmentSetRoutingInstance.MulticastDesignatedRouterPreference = (int)_args[nameof(WithMulticastDesignatedRouterPreference)];
        }
    }
}
