using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for default routing instances. The builder exposes a fluent API.
    /// </summary>
    public class DefaultRoutingInstanceBuilder : BaseBuilder, IDefaultRoutingInstanceBuilder
    {
        private RoutingInstance _routingInstance;
        private readonly IBgpPeerDirector _bgpPeerDirector;

        public DefaultRoutingInstanceBuilder(IUnitOfWork unitOfWork, IBgpPeerDirector bgpPeerDirector) : base (unitOfWork)
        {
            _bgpPeerDirector = bgpPeerDirector;
            _routingInstance = new RoutingInstance
            {
                Name = "Default"                 
            };
        }

        public virtual IDefaultRoutingInstanceBuilder ForDevice(Device device)
        {
            if (device != null) _args.Add(nameof(ForDevice), device);
            return this;
        }

        public virtual IDefaultRoutingInstanceBuilder ForRoutingInstance(int? routingInstanceId)
        {
            if (routingInstanceId.HasValue) _args.Add(nameof(ForRoutingInstance), routingInstanceId);
            return this;
        }

        public virtual IDefaultRoutingInstanceBuilder WithBgpPeers(List<BgpPeerRequest> bgpPeerRequests)
        {
            if (bgpPeerRequests != null) _args.Add(nameof(WithBgpPeers), bgpPeerRequests);
            return this;
        }

        public virtual async Task<RoutingInstance> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForRoutingInstance)))
            {
                await SetRoutingInstanceAsync();
            }
            else if (_args.ContainsKey(nameof(ForDevice)))
            {
                SetDevice(); 
                await SetRoutingInstanceTypeAsync();
            }

            if (_args.ContainsKey(nameof(WithBgpPeers)))
            {
                await ModifyBgpPeersAsync();
            }

            _routingInstance.Validate();

            return _routingInstance;
        }

        protected internal virtual void SetDevice()
        {
            var device = (Device)_args[nameof(ForDevice)];
            _routingInstance.Device = device;

        }

        /// <summary>
        /// Get the existing default routing instance from the database
        /// </summary>
        /// <returns></returns>
        /// <remarks>The BgpPeers property must be included in order to perform any required updates
        /// on the BGP peers</remarks>
        protected internal virtual async Task SetRoutingInstanceAsync()
        {
            var routingInstanceId = (int)_args[nameof(ForRoutingInstance)];
            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                q =>
                                   q.RoutingInstanceID == routingInstanceId &&
                                   q.RoutingInstanceType.IsDefault,
                                   query: q => q.IncludeValidationProperties(),
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _routingInstance = routingInstance ?? throw new BuilderBadArgumentsException($"The default routing instance with ID '{routingInstanceId}' was not found.");
        }

        protected internal virtual async Task SetRoutingInstanceTypeAsync()
        {
            var routingInstanceType = (from result in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                    q =>
                                       q.IsDefault == true,
                                       AsTrackable: true)
                                       select result)
                                       .SingleOrDefault();

            _routingInstance.RoutingInstanceType = routingInstanceType;
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
            var updatedBgpPeers = await _bgpPeerDirector.UpdateAsync(updateBgpRequests);

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
