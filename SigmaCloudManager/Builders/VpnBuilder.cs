using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;
using Microsoft.EntityFrameworkCore;

namespace Mind.Builders
{
    /// <summary>
    /// Abstract base builder for all types of vpn. The builder exposes a fluent API.
    /// </summary>
    public abstract class VpnBuilder : BaseBuilder, IVpnBuilder
    {
        protected internal Vpn _vpn;

        public VpnBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpn = new Vpn
            {
                Created = true,
                ShowCreatedAlert = true,
                RouteTargets = new List<RouteTarget>(),
                VpnAttachmentSets = new List<VpnAttachmentSet>(),
                ExtranetVpnMembers = new List<ExtranetVpnMember>(),
                ExtranetVpns = new List<ExtranetVpnMember>()
            };
        }

        public virtual IVpnBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public IVpnBuilder ForVpn(int? vpnId)
        {
            if (vpnId.HasValue) _args.Add(nameof(ForVpn), vpnId);
            return this;
        }

        public virtual IVpnBuilder WithName(string name)
        {
            if (!string.IsNullOrEmpty(name)) _args.Add(nameof(WithName), name);
            return this;
        }

        public virtual IVpnBuilder WithDescription(string description)
        {
            if (!string.IsNullOrEmpty(description)) _args.Add(nameof(WithDescription), description);
            return this;
        }

        public virtual IVpnBuilder WithRegion(string regionName)
        {
            if (!string.IsNullOrEmpty(regionName)) _args.Add(nameof(WithRegion), regionName);
            return this;
        }

        public virtual IVpnBuilder WithTopologyType(string topologyName)
        {
            if (!string.IsNullOrEmpty(topologyName)) _args.Add(nameof(WithTopologyType), topologyName);
            return this;
        }

        public virtual IVpnBuilder WithPlane(string planeName)
        {
            if (!string.IsNullOrEmpty(planeName)) _args.Add(nameof(WithPlane), planeName);
            return this;
        }

        public virtual IVpnBuilder WithTenancyType(string tenancyTypeName)
        {
            if (!string.IsNullOrEmpty(tenancyTypeName)) _args.Add(nameof(WithTenancyType), tenancyTypeName);
            return this;
        }

        public IVpnBuilder AsNovaVpn(bool? isNovaVpn)
        {
            if (isNovaVpn.HasValue) _args.Add(nameof(AsNovaVpn), isNovaVpn);
            return this;
        }

        public IVpnBuilder WithAddressFamily(string addressFamilyName)
        {
            if (!string.IsNullOrEmpty(addressFamilyName)) _args.Add(nameof(WithAddressFamily), addressFamilyName);
            return this;
        }

        /// <summary>
        /// Build the vpn
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Vpn> BuildAsync()
        {
            if (_args.ContainsKey(nameof(WithName))) SetName();
            if (_args.ContainsKey(nameof(WithDescription))) SetDescription();
            if (_args.ContainsKey(nameof(AsNovaVpn))) SetNovaVpn();
            if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
            if (_args.ContainsKey(nameof(WithTenancyType))) await SetTenancyTypeAsync();
            if (_args.ContainsKey(nameof(WithTopologyType))) await SetTopologyTypeAsync();
            if (_args.ContainsKey(nameof(WithAddressFamily))) await SetAddressFamilyAsync();
            if (_args.ContainsKey(nameof(WithPlane))) await SetPlaneAsync();
            if (_args.ContainsKey(nameof(WithRegion))) await SetRegionAsync();

            return _vpn;
        }

        protected internal virtual void SetName()
        {
            _vpn.Name = _args[nameof(WithName)].ToString();
        }

        protected internal virtual void SetDescription()
        {
            _vpn.Description = _args[nameof(WithDescription)].ToString();
        }

        protected internal virtual async Task SetRegionAsync()
        {
            var regionName = _args[nameof(WithRegion)].ToString();
            if (regionName == "None")
            {
                _vpn.Region = null;
                _vpn.RegionID = null;
            }
            else
            {
                var region = (from result in await _unitOfWork.RegionRepository.GetAsync(
                    q =>
                        q.Name == regionName,
                              AsTrackable: true)
                              select result)
                              .SingleOrDefault();

                _vpn.Region = region ?? throw new BuilderBadArgumentsException("The specified region was not recognised. Check that the " +
                    $"region argument '{regionName}' is correct.");
            }
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

            _vpn.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' could not be found.");
        }

        protected internal virtual async Task SetTenancyTypeAsync()
        {
            var tenancyTypeName = _args[nameof(WithTenancyType)].ToString();
            var tenancyType = (from result in await _unitOfWork.VpnTenancyTypeRepository.GetAsync(
                    q =>
                        q.Name == tenancyTypeName,
                        AsTrackable: true)
                        select result)
                        .SingleOrDefault();

            _vpn.VpnTenancyType = tenancyType ?? throw new BuilderBadArgumentsException("The specified tenancy type was not recognised. Check that " +
                $"the tenancy type argument '{tenancyTypeName}' is correct.");
        }

        protected internal virtual async Task SetTopologyTypeAsync()
        {
            var topologyTypeName = _args[nameof(WithTopologyType)].ToString();
            var topologyType = (from result in await _unitOfWork.VpnTopologyTypeRepository.GetAsync(
                            q =>
                                q.Name == topologyTypeName,
                                query: q => 
                                       q.Include(x => x.VpnProtocolType),
                                AsTrackable: true)
                                select result)
                               .SingleOrDefault();

            _vpn.VpnTopologyType = topologyType ?? throw new BuilderBadArgumentsException("The specified topology type was not found. Check that " +
                $"the topology type argument '{topologyTypeName}' is correct.");
        }

        protected internal virtual async Task SetAddressFamilyAsync()
        {
            var addressFamilyName = _args[nameof(WithAddressFamily)].ToString();
            var addressFamily = (from result in await _unitOfWork.AddressFamilyRepository.GetAsync(
                            q =>
                                q.Name == addressFamilyName,
                                AsTrackable: true)
                                select result)
                               .SingleOrDefault();

            _vpn.AddressFamily = addressFamily ?? throw new BuilderBadArgumentsException("The specified address family was not found. Check that " +
                $"the address family argument '{addressFamilyName}' is correct.");
        }

        protected internal virtual async Task SetPlaneAsync()
        {
            var planeName = _args[nameof(WithPlane)].ToString();
            var plane = (from result in await _unitOfWork.PlaneRepository.GetAsync(
                            q =>
                                q.Name == planeName,
                                AsTrackable: true)
                                select result)
                               .SingleOrDefault();

            _vpn.Plane = plane ?? throw new BuilderBadArgumentsException("The specified plane was not found. Check that " +
                $"the plane argument '{planeName}' is correct.");
        }

        protected internal virtual void SetNovaVpn()
        {
            _vpn.IsNovaVpn = (bool)_args[nameof(AsNovaVpn)];
        }
    }
}
