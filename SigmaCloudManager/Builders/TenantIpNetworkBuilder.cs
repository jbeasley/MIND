using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for Tenant IP networks. The builder exposes a fluent UI.
    /// </summary>
    public class TenantIpNetworkBuilder : BaseBuilder, ITenantIpNetworkBuilder
    {
        protected internal TenantIpNetwork _tenantIpNetwork;
        public TenantIpNetworkBuilder(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _tenantIpNetwork = new TenantIpNetwork()
            {
                VpnTenantIpNetworksIn = new List<VpnTenantIpNetworkIn>(),
                VpnTenantIpNetworksOut = new List<VpnTenantIpNetworkOut>(),
                VpnTenantIpNetworkRoutingInstancePoliciesIn = new List<VpnTenantIpNetworkRoutingInstance>()
            };
        }

        public virtual ITenantIpNetworkBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual ITenantIpNetworkBuilder WithAllowExtranet(bool? allowExtranet)
        {
            if (allowExtranet.HasValue) _args.Add(nameof(WithAllowExtranet), allowExtranet);
            return this;
        }

        public virtual ITenantIpNetworkBuilder WithIpv4Length(int? ipv4Length)
        {
            if (ipv4Length.HasValue) _args.Add(nameof(WithIpv4Length), ipv4Length);
            return this;
        }

        public virtual  ITenantIpNetworkBuilder WithIpv4LessThanOrEqualToLength(int? ipv4LessThanOrEqualToLength)
        {
            if (ipv4LessThanOrEqualToLength.HasValue) _args.Add(nameof(WithIpv4LessThanOrEqualToLength), ipv4LessThanOrEqualToLength);
            return this;
        }

        public virtual ITenantIpNetworkBuilder WithIpv4Prefix(string ipv4Prefix)
        {
            if (!string.IsNullOrEmpty(ipv4Prefix)) _args.Add(nameof(WithIpv4Prefix), ipv4Prefix);
            return this;
        }

        public virtual ITenantIpNetworkBuilder WithIpRoutingBehaviour(string ipRoutingBehaviour)
        {
            if (!string.IsNullOrEmpty(ipRoutingBehaviour)) _args.Add(nameof(WithIpRoutingBehaviour), ipRoutingBehaviour);
            return this;
        }

        public virtual async Task<TenantIpNetwork> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
            if (_args.ContainsKey(nameof(WithIpv4Prefix)) && _args.ContainsKey(nameof(WithIpv4Length))) SetPrefixAndLength();
            if (_args.ContainsKey(nameof(WithIpv4Length))) _tenantIpNetwork.Ipv4Length = (int)_args[nameof(WithIpv4Length)];
            if (_args.ContainsKey(nameof(WithIpv4LessThanOrEqualToLength))) 
                _tenantIpNetwork.Ipv4LessThanOrEqualToLength = (int)_args[nameof(WithIpv4LessThanOrEqualToLength)];
            if (_args.ContainsKey(nameof(WithAllowExtranet))) _tenantIpNetwork.AllowExtranet = (bool)_args[nameof(WithAllowExtranet)];
            if (_args.ContainsKey(nameof(WithIpRoutingBehaviour)))
            {
                if (Enum.TryParse(_args[nameof(WithIpRoutingBehaviour)].ToString(), out TenantIpRoutingBehaviourEnum ipRoutingBehaviour))
                {
                    _tenantIpNetwork.IpRoutingBehaviour = ipRoutingBehaviour;
                }
            }

            _tenantIpNetwork.Validate();
            return _tenantIpNetwork;
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _tenantIpNetwork.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' was not found.");
        }

        protected internal virtual void SetPrefixAndLength()
        {
            var ipv4Prefix = _args[nameof(WithIpv4Prefix)].ToString();
            var ipv4Length = (int)_args[nameof(WithIpv4Length)];

            // Normalise the IP Prefix according to the Cidr length.
            // e.g. - 10.1.1.0/16 becomes 10.1.0.0/16
            var network = IPNetwork.Parse($"{ipv4Prefix}/{ipv4Length}");
            _tenantIpNetwork.Ipv4Prefix = network.Network.ToString();
        }
    }
}
