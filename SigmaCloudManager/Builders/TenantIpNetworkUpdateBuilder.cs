using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updates to Tenant IP networks. The builder exposes a fluent UI.
    /// </summary>
    public class TenantIpNetworkUpdateBuilder : TenantIpNetworkBuilder, ITenantIpNetworkUpdateBuilder
    {
        public TenantIpNetworkUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual ITenantIpNetworkUpdateBuilder ForTenantIpNetwork(int? tenantIpNetworkId)
        {
            if (tenantIpNetworkId.HasValue) _args.Add(nameof(ForTenantIpNetwork), tenantIpNetworkId);
            return this;
        }

        ITenantIpNetworkUpdateBuilder ITenantIpNetworkUpdateBuilder.WithAllowExtranet(bool? allowExtranet)
        {
            base.WithAllowExtranet(allowExtranet);
            return this;
        }

        ITenantIpNetworkUpdateBuilder ITenantIpNetworkUpdateBuilder.WithIpRoutingBehaviour(string ipRoutingBehaviour)
        {
            base.WithIpRoutingBehaviour(ipRoutingBehaviour);
            return this;
        }

        ITenantIpNetworkUpdateBuilder ITenantIpNetworkUpdateBuilder.WithIpv4Length(int? ipv4Length)
        {
            base.WithIpv4Length(ipv4Length);
            return this;
        }

        ITenantIpNetworkUpdateBuilder ITenantIpNetworkUpdateBuilder.WithIpv4LessThanOrEqualToLength(int? ipv4LessThanOrEqualToLength)
        {
            base.WithIpv4LessThanOrEqualToLength(ipv4LessThanOrEqualToLength);
            return this;
        }

        ITenantIpNetworkUpdateBuilder ITenantIpNetworkUpdateBuilder.WithIpv4Prefix(string ipv4Prefix)
        {
            base.WithIpv4Prefix(ipv4Prefix);
            return this;
        }

        public virtual async Task<TenantIpNetwork> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForTenantIpNetwork))) await SetTenantIpNetwork();
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

        protected internal virtual async Task SetTenantIpNetwork()
        {
            var tenantIpNetworkId = (int)_args[nameof(ForTenantIpNetwork)];
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                                q =>
                                   q.TenantIpNetworkID == tenantIpNetworkId,
                                   query: x => x.IncludeValidationProperties(),
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _tenantIpNetwork = tenantIpNetwork ?? throw new BuilderBadArgumentsException($"The tenant IP network with ID '{tenantIpNetworkId}' was not found.");
        }
    }
}
