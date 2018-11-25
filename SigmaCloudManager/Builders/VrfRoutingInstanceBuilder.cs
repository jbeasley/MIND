using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for vrf routing instances. The builder exposes a fluent API.
    /// </summary>
    public class VrfRoutingInstanceBuilder : BaseBuilder, IVrfRoutingInstanceBuilder
    {
        private RoutingInstance _routingInstance;
        private readonly IProviderDomainBgpPeerDirector _bgpPeerDirector;
        private readonly IBgpPeerUpdateDirector _bgpPeerUpdateDirector;

        public VrfRoutingInstanceBuilder(IUnitOfWork unitOfWork, IProviderDomainBgpPeerDirector bgpPeerDirector, 
            IBgpPeerUpdateDirector bgpPeerUpdateDirector) : base (unitOfWork)
        {
            _bgpPeerDirector = bgpPeerDirector;
            _bgpPeerUpdateDirector = bgpPeerUpdateDirector;
            _routingInstance = new RoutingInstance
            {
                Name = Guid.NewGuid().ToString("N"),
                Attachments = new List<Attachment>(),
                Vifs = new List<Vif>(),
                BgpPeers = new List<BgpPeer>()
            };
        }

        public virtual IVrfRoutingInstanceBuilder ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder ForAttachment(Attachment attachment)
        {
            if (attachment != null) _args.Add(nameof(ForAttachment), attachment);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder ForRoutingInstance(int? routingInstanceId)
        {
            if (routingInstanceId.HasValue) _args.Add(nameof(ForRoutingInstance), routingInstanceId);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(WithTenant), tenantId);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithRoutingInstanceType(string routingInstanceType)
        {
            if (!string.IsNullOrEmpty(routingInstanceType)) _args.Add(nameof(WithRoutingInstanceType), routingInstanceType);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithRangeType(string rdRangeType)
        {
            if (!string.IsNullOrEmpty(rdRangeType)) _args.Add(nameof(WithRangeType), rdRangeType);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithName(string name)
        {
            if (!string.IsNullOrEmpty(name)) _args.Add(nameof(WithName), name);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithAdministratorSubField(int? rdAdministratorSubField)
        {
            if (rdAdministratorSubField.HasValue) _args.Add(nameof(WithAdministratorSubField), rdAdministratorSubField);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithAssignedNumberSubField(int? rdAssignedNumberSubField)
        {
            if (rdAssignedNumberSubField.HasValue) _args.Add(nameof(WithAssignedNumberSubField), rdAssignedNumberSubField);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithBgpPeers(List<BgpPeerRequest> bgpPeerRequests) 
        {
            if (bgpPeerRequests != null) _args.Add(nameof(WithBgpPeers), bgpPeerRequests);
            return this;
        }

        public virtual async Task<RoutingInstance> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForRoutingInstance)))
            {
                // Update an existing routing instance
                await SetRoutingInstanceAsync();
            }
            else
            {
                // Create a new routing instance
                if (_args.ContainsKey(nameof(ForDevice))) await SetDeviceAsync();
                if (_args.ContainsKey(nameof(WithTenant))) await SetTenantAsync();
                if (_args.ContainsKey(nameof(ForAttachment))) SetAttachment();
                if (_args.ContainsKey(nameof(WithRoutingInstanceType))) await SetRoutingInstanceTypeAsync();
            }

            if (_args.ContainsKey(nameof(WithName))) SetName();
            if (_args.ContainsKey(nameof(WithAdministratorSubField)) &&
                _args.ContainsKey(nameof(WithAssignedNumberSubField)))
            {
                await SetRouteDistinguisherAsync();
            }
            else if (_args.ContainsKey(nameof(WithRangeType)))
            {
                await AssignRouteDistinguisherAsync();
            }
            if (_args.ContainsKey(nameof(WithBgpPeers))) await ModifyBgpPeersAsync();

            _routingInstance.Validate();
            return _routingInstance;
        }

        /// <summary>
        /// Get the existing routing instance from the database
        /// </summary>
        /// <returns></returns>
        /// <remarks>The BgpPeers property must be included in order to perform any required updates
        /// on the BGP peers</remarks>
        protected internal virtual async Task SetRoutingInstanceAsync()
        {
            var routingInstanceId = (int)_args[nameof(ForRoutingInstance)];
            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                q =>
                                   q.RoutingInstanceID == routingInstanceId,
                                   query: q => q.IncludeValidationProperties(),                           
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _routingInstance = routingInstance ?? throw new BuilderBadArgumentsException($"The routing instance with ID '{routingInstanceId}' was not found.");
        }

        protected internal virtual async Task SetDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == deviceId,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _routingInstance.Device = device;
        }

        protected internal virtual void SetAttachment()
        {
            var attachment = (Attachment)_args[nameof(ForAttachment)];
            // Add the attachment to the Attachments collection of the routing instance here.
            // This supports validation checks such as validation of any BGP peers where the Ip addressing assigned to the attachment
            // is checked against the BGP peer address (see the BgpPeer model Validation method)
            _routingInstance.Attachments.Add(attachment);

            // Override any previous device association with the device association of the attachment. The attachment is more specific and 
            // must take precedence in order to avoid conflict (e.g. device of the attachment is not the same as the supplied device argument
            _routingInstance.Device = attachment.Device;
            _routingInstance.DeviceID = attachment.Device.DeviceID;

            // Similarly override any previous tenant association to ensure consistency
            _routingInstance.Tenant = attachment.Tenant;
            _routingInstance.TenantID = attachment.TenantID;
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(WithTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                        q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _routingInstance.Tenant = tenant;
        }

        protected internal virtual async Task SetRoutingInstanceTypeAsync()
        {
            var routingInstanceTypeName = _args[nameof(WithRoutingInstanceType)].ToString();
            var routingInstanceType = (from result in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                    q =>
                                       q.Type == Enum.Parse<RoutingInstanceTypeEnum>(routingInstanceTypeName),
                                       AsTrackable: true)
                                       select result)
                                       .SingleOrDefault();

            _routingInstance.RoutingInstanceType = routingInstanceType;
        }

        protected internal virtual void SetName()
        {
            var name = _args[nameof(WithName)].ToString();
            _routingInstance.Name = name;
        }

        protected internal virtual async Task AssignRouteDistinguisherAsync()
        {
            var rdRangeType = _args[nameof(WithRangeType)].ToString();
            var rdRange = (from result in await _unitOfWork.RouteDistinguisherRangeRepository.GetAsync(
                        q => 
                          q.Type == Enum.Parse<SCM.Models.RouteDistinguisherRangeTypeEnum>(rdRangeType),
                          AsTrackable: true)
                          select result)
                         .SingleOrDefault();

            if (rdRange == null)
            {
                throw new BuilderUnableToCompleteException($"The route distinguisher range '{rdRangeType.ToString()}' was not found. " +
                    "Please contact your system administrator to resolve this issue.");
            }

            var usedRDs = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                        q =>
                           q.RouteDistinguisherRange.Type == rdRange.Type,
                           AsTrackable: true)
                           select result.AssignedNumberSubField.Value)
                           .ToList();

            // Allocate a new unused RD from the RD range

            int? newRdAssignedNumberSubField = Enumerable.Range(rdRange.AssignedNumberSubFieldStart, rdRange.AssignedNumberSubFieldCount)
                           .Except(usedRDs).FirstOrDefault();

            if (newRdAssignedNumberSubField == null) throw new BuilderUnableToCompleteException("Failed to allocate a free route distinguisher. "
                    + "Please contact your system administrator, or try another range.");

            _routingInstance.AdministratorSubField = rdRange.AdministratorSubField;
            _routingInstance.AssignedNumberSubField = newRdAssignedNumberSubField.Value;
            _routingInstance.RouteDistinguisherRange = rdRange;
        }

        protected internal virtual async Task SetRouteDistinguisherAsync()
        {
            var rdAdministratorSubField = (int)_args[nameof(WithAdministratorSubField)];
            var rdAssignedNumberSubField = (int)_args[nameof(WithAssignedNumberSubField)];
            var rdRange = (from result in await _unitOfWork.RouteDistinguisherRangeRepository.GetAsync(
                        q =>
                          q.AdministratorSubField == rdAdministratorSubField &&
                          rdAssignedNumberSubField >= q.AssignedNumberSubFieldStart && 
                          rdAssignedNumberSubField < q.AssignedNumberSubFieldStart + q.AssignedNumberSubFieldCount,
                          AsTrackable: true)
                          select result)
                         .SingleOrDefault();

            if (rdRange == null)
            {
                throw new BuilderUnableToCompleteException($"A valid route distinguisher range was not found from the supplied administrator subfield " +
                    $"'{rdAdministratorSubField}' and assigned number subfield '{rdAssignedNumberSubField}' arguments.");
            }

            _routingInstance.AdministratorSubField = rdRange.AdministratorSubField;
            _routingInstance.AssignedNumberSubField = rdAssignedNumberSubField;
            _routingInstance.RouteDistinguisherRange = rdRange;
        }

        /// <summary>
        /// Modify the BGPPeers collection of the routing instance.
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task ModifyBgpPeersAsync() 
        {
            var requests = (List<BgpPeerRequest>)_args[nameof(WithBgpPeers)];

            // Create new BGP peers
            var newBgpRequests = requests.Where(x => !x.BgpPeerId.HasValue).ToList();
            var newBgpPeers = await _bgpPeerDirector.BuildAsync(this._routingInstance, newBgpRequests);

            // Update existing BGP peers
            var updateBgpRequests = requests.Where(x => x.BgpPeerId.HasValue).ToList();
            var updatedBgpPeers = await _bgpPeerUpdateDirector.UpdateAsync(updateBgpRequests);

            // Validate requests to delete BGP peers
            if (_routingInstance.BgpPeers.Any())
            {
                var deleteBgpPeers = _routingInstance.BgpPeers.Where(
                                                               bgpPeer =>
                                                               !requests.Select(
                                                               request =>
                                                               request.BgpPeerId)
                                                               .Contains(bgpPeer.BgpPeerID)
                );

                foreach (var deleteBgpPeer in deleteBgpPeers)
                {
                    deleteBgpPeer.ValidateDelete();
                }
            }

            // Modify the BGP peers collection on the routing instance
            var bgpPeers = newBgpPeers.Concat(updatedBgpPeers);
            this._routingInstance.BgpPeers = bgpPeers.ToList();
        }
    }
}
