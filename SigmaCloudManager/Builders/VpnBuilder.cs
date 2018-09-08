using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using SCM.Data;

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
                RouteTargets = new List<RouteTarget>()
            };
        }

        public virtual IVpnBuilder ForTenant(int tenantId)
        {
            _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public virtual IVpnBuilder WithName(string name)
        {
            _args.Add(nameof(WithName), name);
            return this;
        }

        public virtual IVpnBuilder WithDescription(string description)
        {
            _args.Add(nameof(WithDescription), description);
            return this;
        }

        public virtual IVpnBuilder WithRegion(string regionName)
        {
            if (!string.IsNullOrEmpty(regionName)) _args.Add(nameof(WithRegion), regionName);
            return this;
        }

        public IVpnBuilder WithTopologyType(string topologyName)
        {
            _args.Add(nameof(WithTopologyType), topologyName);
            return this;
        }

        public IVpnBuilder WithPlane(string planeName)
        {
            if (!string.IsNullOrEmpty(planeName)) _args.Add(nameof(WithPlane), planeName);
            return this;
        }

        public IVpnBuilder WithTenancyType(string tenancyTypeName)
        {
             _args.Add(nameof(WithTenancyType), tenancyTypeName);
            return this;
        }

        public IVpnBuilder AsNovaVpn(bool? isNovaVpn)
        {
            _args.Add(nameof(AsNovaVpn), isNovaVpn);
            return this;
        }

        public IVpnBuilder WithAddressFamily(string addressFamilyName)
        {
            _args.Add(nameof(WithAddressFamily), addressFamilyName);
            return this;
        }

        /// <summary>
        /// Build the vpn
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Vpn> BuildAsync()
        {
            SetName();
            SetDescription();
            SetNovaVpn();
            await SetTenantAsync();
            await SetTenancyTypeAsync();
            await SetTopologyTypeAsync();
            await SetAddressFamilyAsync();
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
            var region = (from result in await _unitOfWork.RegionRepository.GetAsync(
                q =>
                    q.Name == regionName,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _vpn.Region = region ?? throw new BuilderBadArgumentsException("The specified region was not recognised. Check that the " +
                $"region argument '{regionName}' is correct.");
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .Single();

            _vpn.Tenant = tenant;
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

            _vpn.VpnTenancyType = tenancyType ?? throw new BuilderBadArgumentsException("The specified tenant type was not recognised. Check that " +
                $"the tenancy type argument '{tenancyTypeName}' is correct.");
        }

        protected internal virtual async Task SetTopologyTypeAsync()
        {
            var topologyTypeName = _args[nameof(WithTopologyType)].ToString();
            var topologyType = (from result in await _unitOfWork.VpnTopologyTypeRepository.GetAsync(
                            q =>
                                q.Name == topologyTypeName,
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
