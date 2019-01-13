using Mind.Models;
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
    /// Builder for tenant communities. The builder exposes a fluent UI.
    /// </summary>
    public class TenantCommunityBuilder : BaseBuilder, ITenantCommunityBuilder
    {
        protected internal TenantCommunity _tenantCommunity;
        public TenantCommunityBuilder(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _tenantCommunity = new TenantCommunity()
            {
                VpnTenantCommunitiesIn = new List<VpnTenantCommunityIn>(),
                VpnTenantCommunitiesOut = new List<VpnTenantCommunityOut>(),
                VpnTenantCommunityRoutingInstancePoliciesIn = new List<VpnTenantCommunityRoutingInstance>(),
                VpnTenantIpNetworkCommunitiesIn = new List<VpnTenantIpNetworkCommunityIn>(),
                TenantCommunitySets = new List<TenantCommunitySet>()
            };
        }

        public virtual ITenantCommunityBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual ITenantCommunityBuilder ForTenantCommunity(int? tenantCommunityId)
        {
            if (tenantCommunityId.HasValue) _args.Add(nameof(ForTenantCommunity), tenantCommunityId);
            return this;
        }

        public virtual ITenantCommunityBuilder WithAllowExtranet(bool? allowExtranet)
        {
            if (allowExtranet.HasValue) _args.Add(nameof(WithAllowExtranet), allowExtranet);
            return this;
        }

        public virtual ITenantCommunityBuilder WithAsNumber(int? asNumber)
        {
            if (asNumber.HasValue) _args.Add(nameof(WithAsNumber), asNumber);
            return this;
        }

        public virtual  ITenantCommunityBuilder WithNumber(int? number)
        {
            if (number.HasValue) _args.Add(nameof(WithNumber), number);
            return this;
        }

        public virtual ITenantCommunityBuilder WithIpRoutingBehaviour(string ipRoutingBehaviour)
        {
            if (!string.IsNullOrEmpty(ipRoutingBehaviour)) _args.Add(nameof(WithIpRoutingBehaviour), ipRoutingBehaviour);
            return this;
        }

        public virtual ITenantCommunityBuilder WithTenantEnvironment(string environment)
        {
            if (!string.IsNullOrEmpty(environment)) _args.Add(nameof(WithTenantEnvironment), environment);
            return this;
        }

        public virtual async Task<TenantCommunity> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
            if (_args.ContainsKey(nameof(ForTenantCommunity))) await SetTenantCommunityAsync();
            if (_args.ContainsKey(nameof(WithAsNumber))) SetAsNumber();
            if (_args.ContainsKey(nameof(WithNumber))) SetNumber();
            if (_args.ContainsKey(nameof(WithAllowExtranet))) _tenantCommunity.AllowExtranet = (bool)_args[nameof(WithAllowExtranet)];
            if (_args.ContainsKey(nameof(WithIpRoutingBehaviour)))
            {
                if (Enum.TryParse(_args[nameof(WithIpRoutingBehaviour)].ToString(), out TenantIpRoutingBehaviourEnum ipRoutingBehaviour))
                {
                    _tenantCommunity.IpRoutingBehaviour = ipRoutingBehaviour;
                }
            }

            if (_args.ContainsKey(nameof(WithTenantEnvironment)))
            {
                if (Enum.TryParse(_args[nameof(WithTenantEnvironment)].ToString(), out TenantEnvironmentEnum environment))
                {
                    _tenantCommunity.TenantEnvironment = environment;
                }
            }

            _tenantCommunity.Validate();
            return _tenantCommunity;
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

            _tenantCommunity.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' was not found.");
        }

        protected internal virtual void SetAsNumber()
        {
            var asNumber = (int)_args[nameof(WithAsNumber)];
            _tenantCommunity.AutonomousSystemNumber = asNumber;
        }

        protected internal virtual void SetNumber()
        {
            var number = (int)_args[nameof(WithNumber)];
            _tenantCommunity.Number = number;
        }

        protected internal virtual async Task SetTenantCommunityAsync()
        {
            var tenantCommunityId = (int)_args[nameof(ForTenantCommunity)];
            var tenantCommunity = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                                q =>
                                   q.TenantCommunityID == tenantCommunityId,
                                   query: x => x.IncludeValidationProperties(),
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _tenantCommunity = tenantCommunity ?? throw new BuilderBadArgumentsException($"The tenant community with ID '{tenantCommunityId}' " +
                $"was not found.");
        }
    }
}
