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

            Validate();
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
                                  includeProperties: "RoutingInstanceType," +
                                  "Device.Location.SubRegion.Region")
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

        /// <summary>
        /// Validate the state of the attachment set routing instance
        /// </summary>
        protected internal virtual void Validate()
        {
            if (_attachmentSetRoutingInstance.AttachmentSet == null && _args.ContainsKey(nameof(ForAttachmentSet)) && 
                    _args[nameof(ForAttachmentSet)].GetType() == typeof(AttachmentSet)) 
                throw new BuilderIllegalStateException($"The attachment set with ID '{(int)_args[nameof(ForAttachmentSet)]}' was not found.");

            if (_attachmentSetRoutingInstance.RoutingInstance == null) throw new BuilderBadArgumentsException($"The routing instance " +
                $"'{_attachmentSetRoutingInstance.RoutingInstance.Name}' was not found or is invalid.");

            if (_attachmentSetRoutingInstance.AttachmentSet.IsLayer3 != _attachmentSetRoutingInstance.RoutingInstance.RoutingInstanceType.IsLayer3)
                throw new BuilderIllegalStateException($"Routing instance '{_attachmentSetRoutingInstance.RoutingInstance.Name}' cannot be added to attachment set " +
                    $"'{_attachmentSetRoutingInstance.AttachmentSet.Name}'. The protocol layer of the attachment set and the routing instance do not match. " +
                    $"Attachment set 'IsLayer3' property is '{_attachmentSetRoutingInstance.AttachmentSet.IsLayer3}'. Routing instance 'IsLayer3' " +
                    $"property is '{_attachmentSetRoutingInstance.RoutingInstance.RoutingInstanceType.IsLayer3}'.");

            // The routing instance must belong to the same tenant as the attachment set
            if (_attachmentSetRoutingInstance.RoutingInstance.TenantID != _attachmentSetRoutingInstance.AttachmentSet.TenantID)
                throw new BuilderIllegalStateException($"Routing instance '{_attachmentSetRoutingInstance.RoutingInstance.Name}' "
                   + $" does not belong to the same tenant as the attachment set. The tenant for that attachment set is " +
                   $"'{_attachmentSetRoutingInstance.AttachmentSet.Tenant.Name}'.");

            // The routing instance must be associated with a device in the same region as the attachment set
            if (_attachmentSetRoutingInstance.RoutingInstance.Device.Location.SubRegion.Region.RegionID != _attachmentSetRoutingInstance.AttachmentSet.RegionID)
                throw new BuilderIllegalStateException($"Routing instance '{_attachmentSetRoutingInstance.RoutingInstance.Name}' is not associated with "
                     + $"a device in region {_attachmentSetRoutingInstance.AttachmentSet.Region.Name}.");
        }
    }
}
